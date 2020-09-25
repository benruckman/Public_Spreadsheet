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
            throw new NotImplementedException();
        }

        public override IList<string> SetCellContents(string name, double number)
        {
            throw new NotImplementedException();
        }

        public override IList<string> SetCellContents(string name, string text)
        {
            IsValidNameAndContents(name, text);
            //intialize list that will be returned
            List<string> cellsToRecalculate = new List<string>();
            //check for circular dependencies, 
            //iterate through each cell name that needs to be recalculated and add it to list
            foreach (string s in GetCellsToRecalculate(name))
            {
                cellsToRecalculate.Add(s);
            }

            if (!namedCells.ContainsKey(name))
            {
                namedCells.Add(name, new Cell(name, text));
            }
            else
            {
                namedCells[name].SetContents(text);
                dependencyGraph.ReplaceDependees(name, new List<string>());
            }

            return cellsToRecalculate;
        }

        public override IList<string> SetCellContents(string name, Formula formula)
        {
            IsValidNameAndContents(name, formula);
            //intialize list that will be returned
            List<string> cellsToRecalculate = new List<string>();
            //check for circular dependencies, 
            //iterate through each cell name that needs to be recalculated and add it to list
            foreach (string s in GetCellsToRecalculate(name))
            {
                cellsToRecalculate.Add(s);
            }

            //if the cell has not already been named (and thus does not exist in namedCells)
            //Add new cell containing a formula
            //Dependencies will also be added to the dependency graph
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

            return cellsToRecalculate;
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
