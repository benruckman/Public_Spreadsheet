using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadsheetUtilities
{
    class Cell
    {
        private string name;
        private object contents;
        private string value;

        public Cell (string name, object contents)
        {
            this.name = name;
            this.contents = contents;
        }

        public void SetContents (object contents)
        {
            this.contents = contents;
        }

        public object GetContents ()
        {
            return contents;
        }

    }
}
