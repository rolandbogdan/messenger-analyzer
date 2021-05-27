using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger_Analyzer.VM
{
    public class Participant : IComparable
    {
        public double ReactionsPerMessage
        {
            get
            {
                if (MessageCount != 0)
                    return Math.Round((double)ReactionCount / (double)MessageCount, 2);
                else
                    return 0;
            }
        }

        public int ReactionCount
        {
            get
            {
                int i = 0;

                foreach (Message item in this.Messages)
                    if (item.reactions != null)
                        i += item.reactions.Count();

                return i;
            }
        }

        public int MessageCount
        {
            get { return this.Messages.Count; }
        }

        public int FilteredWordCount { get; set; }

        public string name { get; set; }

        public List<Message> Messages { get; set; }

        public int CompareTo(object obj)
        {
            if (this.MessageCount > (obj as Participant).MessageCount)
            {
                return -1;
            }
            else if (this.MessageCount == (obj as Participant).MessageCount)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
