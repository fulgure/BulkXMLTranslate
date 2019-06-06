using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BulkXMLTranslate
{
    public partial class Form1 : Form
    {
        public string CurrentFile { get; set; }
        const int DETECT_INDEX = 0;
        const string MATCH_TEXT = @"<text>(.+?)<\/text>";
        const string NEW_FILE_SUFFIX = "-translated";
        public readonly Encoding DEFAULT = Encoding.GetEncoding("windows-1251");
        public bool UseGoogleAPI { get; set; } = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadLanguages();
        }

        private void saveChangesToFile(TranslatedString singleChange = null)
        {
            tstProgress.Value = 0;
            tstLblInfo.Text = $"Saving";
            int total = 0;

            List<TranslatedString> changes = new List<TranslatedString>();
            if (singleChange != null)
                changes.Add(singleChange);
            else
            {
                if (lstDest.Items.Count > 0)
                    changes.AddRange(lstDest.Items.Cast<TranslatedString>());
            }
            Dictionary<string, List<TranslatedString>> changesPerFile = new Dictionary<string, List<TranslatedString>>();

            foreach (TranslatedString str in changes)
            {
                if (!changesPerFile.ContainsKey(str.File))
                    changesPerFile.Add(str.File, (new TranslatedString[] { str }).ToList());
                else
                    changesPerFile[str.File].Add(str);
                total++;
            }
            int cpt;
            foreach (List<TranslatedString> diffs in changesPerFile.Values)
            {
                cpt = 0;
                string currFile = diffs[0].File;
                string[] file = currFile.Split('.');
                string newFile = $"{file[0] + NEW_FILE_SUFFIX}.{file[1]}";
                string newText = File.ReadAllText(currFile, DEFAULT);
                foreach (TranslatedString str in diffs)
                {
                    newText = newText.Replace(str.OriginalText, str.Text);
                    cpt++;
                }
                File.WriteAllText(newFile, newText, DEFAULT);

                tstProgress.Value = Convert.ToInt32((diffs.Count / (float)total) * 100);
                tstLblInfo.Text = $"Saved {cpt}/{total}";
            }

            tstProgress.Value = 100;
            tstLblInfo.Text = $"Done saving";
        }

        private void lst_SelectChanged(object sender, EventArgs e)
        {
            if ((ListBox)sender != null)
                tbxLonger.Text = ((ListBox)sender).SelectedItem.ToString();
        }

        private void LoadLanguages()
        {
            List<Language> langs = YandexAPI.GetLanguages();
            cmbSource.Items.Add(new Language("Detect language", YandexAPI.DETECT_LANG_CODE));
            cmbSource.Items.AddRange(langs.ToArray());
            cmbDest.Items.AddRange(langs.ToArray());

            cmbSource.SelectedIndex = 0;
            cmbDest.SelectedIndex = 0;

        }

        private void LoadText(string file)
        {
            Regex rx = new Regex(MATCH_TEXT);
            MatchCollection matches = rx.Matches(File.ReadAllText(file, DEFAULT));
            foreach (Match mc in matches)
            {
                lstSource.Items.Add(new StringFromFile(mc.Groups[1].Value, file));
            }
            if (lstSource.Items.Count > 0)
                lstSource.SelectedIndex = 0;
        }

        private void btnTranslateSingle_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            tstProgress.Value = 0;
            tstLblInfo.Text = "Translating";

            TranslatedString result = GetTranslatedStringFromSourceIndex(lstSource.SelectedIndex)[0];
            lstDest.Items.Add(result);

            tstProgress.Value = 50;
            tstLblInfo.Text = "Saving";

            saveChangesToFile(result);


            tstProgress.Value = 100;
            tstLblInfo.Text = "Save complete";
            Cursor.Current = Cursors.Default;
        }

        private TranslatedString[] GetTranslatedStringFromSourceIndex(int index = -1)
        {
            Func<string[], string, string, ToolStripProgressBar, ToolStripLabel, string[]> Translate;
            if (UseGoogleAPI)
                Translate = GoogleTranslate.Translate;
            else
                Translate = YandexAPI.Translate;

            List<TranslatedString> result = new List<TranslatedString>();
            string destText = cmbDest.Text;
            Language currentDest = cmbDest.Items.Cast<Language>().Where(x => (x.Label == destText || x.Code == destText)).First();
            string selectedSourceLangCode = (cmbSource.SelectedItem as Language).Code;
            string[] translatedText;

            if (index == -1)
            {
                List<string> chunks = new List<string>();
                for (int i = 0; i < lstSource.Items.Count; i++)
                    chunks.Add((lstSource.Items[i] as StringFromFile).Text);

                translatedText = Translate(chunks.ToArray(), selectedSourceLangCode, currentDest.Code, tstProgress, tstLblInfo);
            }
            else
                translatedText = Translate(new string[] { lstSource.Items[index].ToString() }, selectedSourceLangCode, currentDest.Code, tstProgress, tstLblInfo);

            int cpt = 0;
            foreach (string str in translatedText)
            {
                StringFromFile og = lstSource.Items[cpt] as StringFromFile;
                result.Add(new TranslatedString(str, og.Text, og.File));
                cpt++;
            }
            return result.ToArray();
        }

        private void btnTranslateAll_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            lstDest.Items.Clear();
            tstLblInfo.Text = $"Translating";
            TranslatedString[] results = GetTranslatedStringFromSourceIndex();
            tstLblInfo.Text = $"Done Translating";

            lstDest.Items.AddRange(results.ToArray());
            saveChangesToFile();
            Cursor.Current = Cursors.Default;
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML files (*.xml)|*.xml";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(ofd.FileName))
                {
                    CurrentFile = ofd.FileName;
                    LoadText(CurrentFile);
                }
            }
        }
    }
}
