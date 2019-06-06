using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkXMLTranslate
{
    internal static class Extensions
    {
        public static string ReadToEnd(this Stream sm)
        {
            using (StreamReader str = new StreamReader(sm, Encoding.UTF8))
            {
                return str.ReadToEnd();
            }
        }
    }
}
