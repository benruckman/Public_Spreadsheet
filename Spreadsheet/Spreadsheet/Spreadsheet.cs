using SpreadsheetUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override IList<string> SetCellContents(string name, Formula formula)
        {
            //make sure provided arguments are valid
            if (formula is null)
            {
                throw new ArgumentNullException("Formula cannot be null");
            }
            if (name is null || !Regex.IsMatch(name, "^[a-dA-D_]([a-dA-D_]|\\d)*$"))
            {
                throw new InvalidNameException();
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
            //
            else
            {
                namedCells[name].setContents(formula);
                dependencyGraph.ReplaceDependees(name, formula.GetVariables());
            }

            return null;


        }


        protected override IEnumerable<string> GetDirectDependents(string name)
        {

        }
    }
}
