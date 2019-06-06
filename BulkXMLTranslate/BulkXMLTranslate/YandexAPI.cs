using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace BulkXMLTranslate
{
    static class YandexAPI
    {
        public const string DETECT_LANG_CODE = "dtct";

        private const string API_KEY = @"trnsl.1.1.20190605T231152Z.d08f785e8eadfae8.5414731b87f25f844d4e3611e997ebfb38f98422";
        private static string LANG_KEY = $"{Urls.LANGS}?{Args.KEY}={API_KEY}";
        private static string TRANSLATE_KEY = $"{Urls.TRANSLATE}?{Args.KEY}={API_KEY}";
        private const int CHAR_LIMIT = 9500;


        public static List<Language> GetLanguages(string returnLangCode = "en")
        {
            string rawXML = GetLanguagesToXml(returnLangCode);
            XElement parsedXML = XElement.Parse(rawXML);
            List<Language> result = new List<Language>();
            foreach (XElement langs in parsedXML.Descendants(ReturnArgs.LANGS))
            {
                foreach (XElement lang in langs.Elements(ReturnArgs.ITEMS))
                {
                    string label = lang.Attribute(ReturnArgs.VAL).Value;
                    string key = lang.Attribute(ReturnArgs.KEY).Value;
                    result.Add(new Language(label, key));
                }
            }
            return result;
        }

        private static string GetLanguagesToXml(string returnLangCode)
        {
            string url = $"{LANG_KEY}&{Args.RETURN_LANG}={returnLangCode}";
            WebRequest rq = WebRequest.Create(url);
            WebResponse wr = rq.GetResponse();
            return wr.GetResponseStream().ReadToEnd();
        }

        private static string[] TranslateToXml(string[] text, string sourceLanguageCode, string destLanguageCode)
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
            foreach (List<string> chunk in textChunks)
            {
                string direction = (sourceLanguageCode == DETECT_LANG_CODE) ? destLanguageCode : $"{sourceLanguageCode}-{destLanguageCode}";
                string url = $"{TRANSLATE_KEY}&{Args.LANG}={direction}";
                foreach (string txt in chunk)
                {
                    url += '&';
                    url += Uri.EscapeDataString(Args.TEXT);
                    url += '=';
                    url += Uri.EscapeDataString(txt);
                }

                WebRequest rq = WebRequest.Create(url);
                WebResponse wr = rq.GetResponse();
                result.Add(wr.GetResponseStream().ReadToEnd());
            }
            return result.ToArray();
        }

        public static string[] Translate(string[] text, string sourceLanguageCode, string destLanguageCode)
        {
            string[] rawXML = TranslateToXml(text, sourceLanguageCode, destLanguageCode);
            List<string> result = new List<string>();
            foreach (string xml in rawXML)
            {
                XElement parsedXML = XElement.Parse(xml);
                foreach(XElement txt in parsedXML.Descendants(ReturnArgs.TEXT))
                {
                    result.Add(txt.Value);
                }

            }
            return result.ToArray();
        }

        #region Enums & shit
        private static class Args
        {
            public const string KEY = "key";
            public const string RETURN_LANG = "ui";
            public const string TEXT = "text";
            public const string LANG = "lang";
        }

        private static class Urls
        {
            public const string LANGS = @"https://translate.yandex.net/api/v1.5/tr/getLangs";
            public const string TRANSLATE = @"https://translate.yandex.net/api/v1.5/tr/translate";
        }

        private static class ReturnArgs
        {
            public const string LANGS = "langs";
            public const string ITEMS = "Item";
            public const string KEY = "key";
            public const string VAL = "value";
            public const string TRANSLATION = "Translation";
            public const string TEXT = "text";
        }
        #endregion
    }
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
