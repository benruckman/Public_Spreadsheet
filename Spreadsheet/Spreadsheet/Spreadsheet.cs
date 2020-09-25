using SpreadsheetUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace SS
{
    public class Spreadsheet : AbstractSpreadsheet
    {
        private DependencyGraph dependencyGraph;
        private Dictionary<string, Cell> namedCells;

        public Spreadsheet ()
        {
            dependencyGraph = new DependencyGraph();
            namedCells = new Dictionary<string, Cell>();
        }

        public override object GetCellContents(string name)
        {
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

        public override IList<string> SetCellContents(string name, double number)
        {
            return SetCellHelper(name, number);
        }

        public override IList<string> SetCellContents(string name, string text)
        {
            return SetCellHelper(name, text);
        }

        /// <summary>
        /// Helper method for assigning Cell contents when contents will not introduce new dependency relationships
        /// This method handles removing existing dependencies
        /// </summary>
        private IList<string> SetCellHelper (string name, object contents)
        {
            //check for valid name and content
            IsValidNameAndContents(name, contents);
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

        public override IList<string> SetCellContents(string name, Formula formula)
        {
     
            IsValidNameAndContents(name, formula);
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
                if (prevCellContent.Equals(""))
                {
                    namedCells.Remove(name);
                    dependencyGraph.ReplaceDependees(name, prevDependees);
                    dependencyGraph.ReplaceDependents(name, prevDependents);
                }
                else
                {
                    //if cell contents were empty before, replace old dependees with new ones
                    dependencyGraph.ReplaceDependees(name, prevDependees);
                    dependencyGraph.ReplaceDependents(name, prevDependents);
                    namedCells[name].SetContents(prevCellContent);
                }

                foreach (string n in formula.GetVariables())
                {
                    dependencyGraph.RemoveDependency(n, name);
                }

                throw new CircularException();
            }
        }

        /// <summary>
        /// Helper Method, throws if invalid name or contents are provided
        /// </summary>
        private void IsValidNameAndContents(string name, Object obj)
        {
            //make sure provided arguments are valid
            if (obj is null)
            {
                throw new ArgumentNullException("Formula cannot be null");
            }
            IsValidName(name);
        }

        private void IsValidName (string name)
        { 
            if (name is null || !Regex.IsMatch(name, "^[a-zA-Z_]([a-zA-Z_]|\\d)*$"))
            {
                throw new InvalidNameException();
            }
        }


        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            return dependencyGraph.GetDependents(name);
        }
    }
}
