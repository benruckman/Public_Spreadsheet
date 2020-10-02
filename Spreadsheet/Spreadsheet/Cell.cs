using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SpreadsheetUtilities
{
    class Cell
    {
        private string name;
        private object contents;
        private object value;

        public Cell (string name, object contents)
        {
            this.name = name;
            this.contents = contents;
            this.value = null;
        }

        public void SetContents (object contents)
        {
            this.contents = contents;
        }

        public object GetContents ()
        {
            return contents;
        }

        public void SetValue (object value)
        {
            this.value = value;
        }

        public object GetValue ()
        {
            return value;
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Cell");
            writer.WriteElementString("name", name);
            writer.WriteElementString("contents", contents.ToString());
            writer.WriteEndElement();
        }
    }
}
