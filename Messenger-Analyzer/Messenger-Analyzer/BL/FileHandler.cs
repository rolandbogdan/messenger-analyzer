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
            StreamReader sr = new StreamReader(path);
            string json = sr.ReadToEnd();
            sr.Close();

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
    }
}
