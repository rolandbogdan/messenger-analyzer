using Messenger_Analyzer.VM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger_Analyzer.BL
{
    class FileHandler
    {
        public List<Chat> Chats { get; set; }
        public FileHandler(List<string> filepaths)
        {
            this.Chats = new List<Chat>();
            this.ProcessMultiple(filepaths);
        }

        private void ProcessOne(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            string json = sr.ReadToEnd();
            sr.Close();

            // this.FixString(ref json);

            Chat content = JsonConvert.DeserializeObject<Chat>(json);
            this.Chats.Add(content);
        }

        private void ProcessMultiple(List<string> paths)
        {
            foreach (string item in paths)
            {
                this.ProcessOne(item);
            }
        }
        
        private void FixString(ref string x)
        {
            x.Replace(@"\u00c3\u00a1", "á");
            x.Replace(@"\u00c3\u00a9", "é");
            x.Replace(@"\u00c3\u00ad", "í");
            x.Replace(@"\u00c3\u00b3", "ó");
            x.Replace(@"\u00c3\u00b6", "ö");
            x.Replace(@"\u00c5\u0091", "ő");
            x.Replace(@"\u00c3\u00ba", "ú");
            x.Replace(@"\u00c3\u00bc", "ü");
            x.Replace(@"\u00c5\u00b1", "ű");

            x.Replace(@"\u00c3\u0081", "Á");
            x.Replace(@"\u00c3\u0089", "É");
            x.Replace(@"\u00c3\u008d", "Í");
            x.Replace(@"\u00c3\u0093", "Ó");
            x.Replace(@"\u00c3\u0096", "Ö");
            x.Replace(@"\u00c5\u0090", "Ő");
            x.Replace(@"\u00c3\u009a", "Ú");
            x.Replace(@"\u00c3\u009c", "Ü");
            x.Replace(@"\u00c5\u00b0", "Ű");

            x.Replace(@"\u00c3\u00a4", "ä");
        }
    }
}
