using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkXMLTranslate
{
    class Language
    {
        public Language(string label, string code)
        {
            Label = label;
            Code = code;
        }

        public string Label { get; }
        public string Code { get; }

        public override string ToString()
        {
            return Label;
        }
    }
}
