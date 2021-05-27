using Messenger_Analyzer.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger_Analyzer.BL
{
    public class MainLogic
    {
        public List<Participant> MessagesToParticipants(List<Message> messages, List<Participant> participants)
        {
            foreach (Participant person in participants)
                person.Messages = new List<Message>();

            foreach (Message msg in messages)
            {
                var p = participants.Find(x => x.name == msg.sender_name);
                if (p != null)
                {
                    p.Messages.Add(msg);
                }
            }

            participants.Sort();
            return participants;
        }

        public bool CanChatsBeCombined(List<Chat> chats)
        {
            if (chats.Count == 1)
                return true;

            bool canBeCombined = true;
            string oneTitle = chats[0].title;

            foreach (Chat item in chats)
                if (item.title != oneTitle)
                    canBeCombined = false;

            return canBeCombined;
        }

        public Chat CombineChats(List<Chat> chats)
        {
            Chat outp = new Chat();
            outp.title = chats[0].title;
            outp.participants = chats[0].participants;
            outp.thread_path = chats[0].thread_path;
            outp.thread_type = chats[0].thread_type;
            outp.participants = chats[0].participants;
            outp.messages = new List<Message>();

            foreach (Chat item in chats)
            {
                outp.messages.AddRange(item.messages);
            }

            return outp;
        }

        public List<Participant> FilteredWordCounter(List<Participant> p, string msg)
        {
            foreach (Participant item in p)
            {
                foreach (Message message in item.Messages)
                {
                    if (message.content != null)
                    {
                        if (message.content.ToLower().Contains(msg.ToLower()))
                        {
                            item.FilteredWordCount++;
                        }
                    }
                }
            }

            return p;
        }
    }
}
