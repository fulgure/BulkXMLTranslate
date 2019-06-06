using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkXMLTranslate
{
    class TranslatedString
    {
        public TranslatedString(string text, string original, string file)
        {
            Text = text;
            OriginalText = original;
            File = file;
        }

        public string Text { get; set; }
        public string OriginalText { get; }
        public string File { get; }

        public override string ToString()
        {
            return Text;
        }
    }
}
