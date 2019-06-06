using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkXMLTranslate
{
    class StringFromFile
    {
        public StringFromFile(string text, string file)
        {
            Text = text;
            File = file;
        }

        public string Text { get; set; }
        public string File { get; }

        public override string ToString()
        {
            return Text;
        }
    }
}
