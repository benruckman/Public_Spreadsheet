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

        public void setName (string name)
        {
            this.name = name;
        }

        public string getName()
        {
            return name;
        }

        public void setContents (object contents)
        {
            this.contents = contents;
        }

        public object getContents ()
        {
            return contents;
        }

    }
}
