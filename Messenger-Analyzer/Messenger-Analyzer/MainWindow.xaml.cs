using Messenger_Analyzer.BL;
using Messenger_Analyzer.VM;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Messenger_Analyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Chat MainChat { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".json";
            dlg.Filter = "Json File (*.json)|*.json";
            dlg.FileName = "*.json";
            dlg.Multiselect = true;

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                List<string> filenames = new List<string>();
                foreach (string item in dlg.FileNames)
                {
                    filenames.Add(item);
                }
                FileHandler fh = new FileHandler(filenames);

                if (fh.Chats.Count == 1)
                {
                    this.MainChat = fh.Chats[0];
                }
                else if (this.CanChatsBeCombined(fh.Chats))
                {
                    this.MainChat = this.CombineChats(fh.Chats);
                }
                else
                {
                    MessageBox.Show("Error! Dont load different people/groups' messages!");
                }
            }
        }

        private bool CanChatsBeCombined(List<Chat> chats)
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

        private Chat CombineChats(List<Chat> chats)
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
    }
}
