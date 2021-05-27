using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger_Analyzer.VM
{
    public class Video
    {
        public string uri { get; set; }
        public int creation_timestamp { get; set; }
        public Thumbnail thumbnail { get; set; }
    }
}
