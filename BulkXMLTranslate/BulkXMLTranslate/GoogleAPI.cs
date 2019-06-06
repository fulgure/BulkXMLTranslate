using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace BulkXMLTranslate
{
    static class GoogleTranslate
    {
        private const int CHAR_LIMIT = 4500;
        private const int REQUEST_LIMIT = 1000;
        private const int REQUEST_LIMIT_RESET = 100;
        private static readonly char[] DELIMITER = new char[] { '{', 'r', '}' };
        private static int CurrentRequestCount { get; set; } = 0;

        public static string[] Translate(string[] text, string sourceLanguageCode, string destLanguageCode, ToolStripProgressBar tspb, ToolStripLabel tsl)
        {
            List<List<string>> textChunks = new List<List<string>>();
            List<string> currChunk = new List<string>();
            int currChunkLength = 0;

            foreach (string str in text)
            {
                if (currChunkLength + str.Length > CHAR_LIMIT)
                {
                    textChunks.Add(currChunk);
                    currChunk = new List<string>();
                }
                currChunk.Add(str);
                currChunkLength += str.Length;
            }
            if (currChunk.Count > 0)
                textChunks.Add(currChunk);
            List<string> result = new List<string>();
            int cpt = 0;
            int max = textChunks.Sum((x) => x.Count);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach (List<string> chunk in textChunks)
            {
                    tspb.Value = (cpt / max) * 100;
                    tsl.Text = $"Translated {cpt}/{max}";
                tspb.Invalidate();
                tsl.Invalidate();
                string chunkText = Uri.EscapeDataString(chunk.Aggregate((left, right) => left += DELIMITER.ToString() + right));
                Console.WriteLine(GetTranslationURL(sourceLanguageCode, destLanguageCode, chunkText));
                if(CurrentRequestCount >= REQUEST_LIMIT - 1)
                {
                    int timeToSleep = Convert.ToInt32((REQUEST_LIMIT_RESET - (sw.ElapsedMilliseconds / 1000)) * 1000);
                    MessageBox.Show($"Request limit reached, sleeping for {timeToSleep / 1000} seconds");
                    Thread.Sleep(timeToSleep + 1000);
                    CurrentRequestCount = 0;
                    sw.Restart();
                }
                WebRequest rq = WebRequest.Create(GetTranslationURL(sourceLanguageCode, destLanguageCode, chunkText));
                WebResponse wr = rq.GetResponse();
                CurrentRequestCount++;
                string tmpTxt = wr.GetResponseStream().ReadToEnd();
                /**
                 * Sample return :
                 * [[["Hello","hola",null,null,1]],null,"es",null,null,null,0.91015625,null,[["es"],null,[0.91015625],["es"]]]
                 *     ^^^^^ What we need
                 */
                IEnumerable<char> tmp = tmpTxt.Split(',')[0].Skip(4);
                string resultTxt = new string(tmp.Take(tmp.Count() - 1).ToArray());
                string[] splitDelimiter = new[] { "" };
                foreach (char c in DELIMITER)
                {
                    splitDelimiter[0] += c;
                }
                result.AddRange(resultTxt.Split(splitDelimiter, StringSplitOptions.None));
                cpt += chunk.Count;
            }
            return result.ToArray();
        }
        /**
         * Arguments order in the query matters 
         **/
        private static string GetTranslationURL(string source, string dest, string text)
        {
            string urlEncodedText = Uri.EscapeDataString(text);
            return $"{Urls.TRANSLATE}?{Args.CLIENT_AND_VAL}&{Args.SOURCE}={source}&{Args.DEST}={dest}&{Args.DT_AND_VAL}&q={text}";
        }


        #region Enums & shit
        private static class Args
        {
            public const string CLIENT_AND_VAL = "client=gtx";
            public const string SOURCE = "sl";
            public const string DEST = "tl";
            //Argument non documenté mais nécssaire
            public const string DT_AND_VAL = "dt=t";
            public const string USE_JSON = "dj=1";
            public const string TEXT = "q";
        }

        private static class Urls
        {
            public const string TRANSLATE = @"https://translate.googleapis.com/translate_a/single";
        }

        private static class ReturnArgs
        {
            public const string TEXT = "text";
        }
        #endregion
    }
}
