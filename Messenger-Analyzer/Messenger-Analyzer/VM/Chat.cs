using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger_Analyzer.VM
{
    class Chat
    {
        public List<Participant> participants { get; set; }
        public List<Message> messages { get; set; }
        public string title { get; set; }
        public bool is_still_participant { get; set; }
        public string thread_type { get; set; }
        public string thread_path { get; set; }
    }
}
