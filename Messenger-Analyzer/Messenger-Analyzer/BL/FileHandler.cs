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
        public FileHandler(IEnumerable<string> filepaths)
        {
            this.Chats = new List<Chat>();
            this.ProcessMultiple(filepaths);
        }

        public static void ExportMostFrequentWords(IEnumerable<Word> words, string path)
        {
            File.WriteAllText(
                path.EndsWith(".json") ? path : string.Format(path + ".json"),
                JsonConvert.SerializeObject(words, Formatting.Indented));
        }

        public static void ExportAllMessages(IEnumerable<Message> messages, string path)
        {
            StreamWriter sw = new StreamWriter(path.EndsWith(".txt") ? path : string.Format(path + ".txt"));
            foreach (Message item in messages)
            {
                sw.WriteLine(item);
            }
            sw.Close();
        }

        private void ProcessOne(string path)
        {
            string json = File.ReadAllText(path, Encoding.UTF8);

            this.FixString(ref json);

            Chat content = JsonConvert.DeserializeObject<Chat>(json);
            this.Chats.Add(content);
        }

        private void ProcessMultiple(IEnumerable<string> paths)
        {
            foreach (string item in paths)
            {
                this.ProcessOne(item);
            }
        }
        
        private void FixString(ref string x)
        {
            x = x.Replace(@"\u00c3\u00a1", "á")
                .Replace(@"\u00c3\u00a9", "é")
                .Replace(@"\u00c3\u00ad", "í")
                .Replace(@"\u00c3\u00b3", "ó")
                .Replace(@"\u00c3\u00b6", "ö")
                .Replace(@"\u00c5\u0091", "ő")
                .Replace(@"\u00c3\u00ba", "ú")
                .Replace(@"\u00c3\u00bc", "ü")
                .Replace(@"\u00c5\u00b1", "ű")
                .Replace(@"\u00c3\u0081", "Á")
                .Replace(@"\u00c3\u0089", "É")
                .Replace(@"\u00c3\u008d", "Í")
                .Replace(@"\u00c3\u0093", "Ó")
                .Replace(@"\u00c3\u0096", "Ö")
                .Replace(@"\u00c5\u0090", "Ő")
                .Replace(@"\u00c3\u009a", "Ú")
                .Replace(@"\u00c3\u009c", "Ü")
                .Replace(@"\u00c5\u00b0", "Ű")
                .Replace(@"\u00c3\u00a4", "ä");
        }

    }
}
