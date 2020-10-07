using SpreadsheetUtilities;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

//Test Michael 10/7/2020

namespace SS
{
    public class Spreadsheet : AbstractSpreadsheet
    {
        private DependencyGraph dependencyGraph;
        private Dictionary<string, Cell> namedCells;
        private bool changedBool;

        public override bool Changed { get => changedBool; protected set => changedBool = value; }

        public Spreadsheet() : base(s => true, s => s, "default")
        {
            dependencyGraph = new DependencyGraph();
            namedCells = new Dictionary<string, Cell>();
        }

        public Spreadsheet(Func<string, bool> IsValid, Func<string, string> normalize, string version) : base(IsValid, normalize, version)
        {
            dependencyGraph = new DependencyGraph();
            namedCells = new Dictionary<string, Cell>();
        }

        public Spreadsheet(string pathToFile, Func<string, bool> IsValid, Func<string, string> normalize, string version) : base(IsValid, normalize, version)
        {
            GetSavedVersion(pathToFile);
        }

        public override object GetCellContents(string name)
        {
            IsValidName(ref name);
            if (namedCells.ContainsKey(name))
            {
                return namedCells[name].GetContents();
            }
            else return "";
        }

        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            foreach (string s in namedCells.Keys)
            {
                if (!GetCellContents(s).Equals(""))
                {
                    yield return s;
                }
            }
        }

        protected override IList<string> SetCellContents(string name, double number)
        {
            return SetCellHelper(name, number);
        }

        protected override IList<string> SetCellContents(string name, string text)
        {
            return SetCellHelper(name, text);
        }

        /// <summary>
        /// Helper method for assigning Cell contents when contents will not introduce new dependency relationships
        /// This method handles removing existing dependencies
        /// </summary>
        private IList<string> SetCellHelper(string name, object contents)
        {
            //check for valid name and content
            IsValidNameAndContents(ref name, contents);
            //intialize list that will be returned
            List<string> cellsToRecalculate = new List<string>();

            if (!namedCells.ContainsKey(name))
            {
                namedCells.Add(name, new Cell(name, contents));
            }
            else
            {
                namedCells[name].SetContents(contents);
                dependencyGraph.ReplaceDependees(name, new List<string>());
            }

            //check for circular dependencies, 
            //iterate through each cell name that needs to be recalculated and add it to list
            foreach (string s in GetCellsToRecalculate(name))
            {
                cellsToRecalculate.Add(s);
            }

            return cellsToRecalculate;
        }

        protected override IList<string> SetCellContents(string name, Formula formula)
        {

            IsValidNameAndContents(ref name, formula);
            //intialize list that will be returned
            List<string> cellsToRecalculate = new List<string>();

            //if the cell has not already been named (and thus does not exist in namedCells)
            //Add new cell containing a formula
            //Dependencies will also be added to the dependency graph
            IEnumerable<string> prevDependees = new List<string>();
            prevDependees = dependencyGraph.GetDependees(name);
            IEnumerable<string> prevDependents = new List<string>();
            prevDependents = dependencyGraph.GetDependents(name);

            object prevCellContent = GetCellContents(name);

            if (!namedCells.ContainsKey(name))
            {
                namedCells.Add(name, new Cell(name, formula));
                foreach (string n in formula.GetVariables())
                {

                    dependencyGraph.AddDependency(n, name);
                }
            }
            //If cell already exists, reassign its contents
            else
            {
                namedCells[name].SetContents(formula);
                dependencyGraph.ReplaceDependees(name, formula.GetVariables());
            }

            try
            {
                //check for circular dependencies, 
                //iterate through each cell name that needs to be recalculated and add it to list
                foreach (string s in GetCellsToRecalculate(name))
                {
                    cellsToRecalculate.Add(s);
                }

                return cellsToRecalculate;
            }
            catch (CircularException)
            {

                //if cell contents were empty before, replace old dependees with new ones
                dependencyGraph.ReplaceDependees(name, prevDependees);
                dependencyGraph.ReplaceDependents(name, prevDependents);
                namedCells[name].SetContents(prevCellContent);

                foreach (string n in formula.GetVariables())
                {
                    dependencyGraph.RemoveDependency(n, name);
                }

                throw new CircularException();
            }
        }

        /// <summary>
        /// Helper Method, throws if invalid name or contents are provided
        /// Also normalizes name;
        /// </summary>
        private void IsValidNameAndContents(ref string name, Object obj)
        {
            //make sure provided arguments are valid
            if (obj is null)
            {
                throw new ArgumentNullException("Formula cannot be null");
            }
            IsValidName(ref name);
        }

        private void IsValidName(ref string name)
        {
            if (name is null)
            {
                throw new InvalidNameException();
            }

            name = Normalize(name);

            if (!Regex.IsMatch(name, "^[a-zA-Z]+\\d+$") || !IsValid(name))
            {
                throw new InvalidNameException();
            }
        }


        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            IsValidName(ref name);
            return dependencyGraph.GetDependents(name);
        }

        public override string GetSavedVersion(string filename)
        {
            Changed = false;

            namedCells = new Dictionary<string, Cell>();
            dependencyGraph = new DependencyGraph();


            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    string currentName = null;
                    string currentContent = null;

                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name.ToLower())
                            {
                                case "spreadsheet":
                                    Version = reader["version"];
                                    break;

                                case "cell":
                                    currentName = null;
                                    currentContent = null;
                                    break;

                                case "name":
                                    reader.Read();
                                    currentName = reader.Value;
                                    break;

                                case "content":
                                    reader.Read();
                                    currentContent = reader.Value;
                                    break;
                            }
                        }
                        else
                        {
                            if (reader.Name.ToLower() == "cell")
                            {
                                SetContentsOfCell(currentName, currentContent);
                                currentName = null;
                                currentContent = null;
                            }
                        }
                    }
                }
            }
            catch
            {
                throw new SpreadsheetReadWriteException("Error Reading File");
            }

            return Version;
        }

        public override void Save(string filename)
        {
            Changed = false;

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  ";

            using (XmlWriter writer = XmlWriter.Create(filename, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Spreadsheet");
                writer.WriteAttributeString("version", Version);

                foreach (string n in GetNamesOfAllNonemptyCells())
                {
                    namedCells[n].WriteXml(writer);
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

        }

        public override object GetCellValue(string name)
        {
            IsValidName(ref name);

            if (namedCells.ContainsKey(name))
            {
                return namedCells[name].GetValue();
            }
            else
            {
                return "";
            }
        }

        public override IList<string> SetContentsOfCell(string name, string content)
        {
            IsValidNameAndContents(ref name, content);

            Changed = true;

            IList<string> evaluationList = new List<string>();

            if (Double.TryParse(content, out _))
            {
                evaluationList = SetCellContents(name, Double.Parse(content));
            }
            else if (content.StartsWith("="))
            {
                evaluationList = SetCellContents(name, new Formula(content.Substring(1), Normalize, IsValid));
            }
            else
            {
                evaluationList = SetCellContents(name, content);
            }

            object currentContent;
            foreach (string s in evaluationList)
            {
                currentContent = namedCells[s].GetContents();
                if (currentContent is Formula)
                {
                    Formula currentFormula = (Formula)currentContent;

                    Func<string, double> lookup = GetCellFormulaValue;

                    namedCells[s].SetValue(currentFormula.Evaluate(lookup));
                }
                else if (Double.TryParse(currentContent.ToString(), out _))
                {
                    namedCells[s].SetValue(Double.Parse(currentContent.ToString()));
                }
                else
                {
                    namedCells[s].SetValue(currentContent.ToString());
                }
            }


            return evaluationList;
        }


        /// <summary>
        /// Helper method used when cell content is guarnteed by outside code to be a formula
        /// </summary>
        private double GetCellFormulaValue(string name)
        {
            return (double)namedCells[name].GetValue();
        }
    }
}
