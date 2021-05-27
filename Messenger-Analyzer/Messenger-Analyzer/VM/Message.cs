using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger_Analyzer.VM
{
    class Message
    {
        public string sender_name { get; set; }
        public object timestamp_ms { get; set; }
        public string content { get; set; }
        public List<Reaction> reactions { get; set; }
        public string type { get; set; }
        public List<Video> videos { get; set; }
        public List<Photo> photos { get; set; }
    }
}
