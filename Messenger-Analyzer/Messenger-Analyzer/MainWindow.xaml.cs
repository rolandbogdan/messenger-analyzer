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
        MainLogic logic;
        public MainWindow()
        {
            InitializeComponent();
            this.logic = new MainLogic();
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
                else if (this.logic.CanChatsBeCombined(fh.Chats))
                {
                    this.MainChat = this.logic.CombineChats(fh.Chats);
                }
                else
                {
                    MessageBox.Show("Error! Dont load different people/groups' messages!");
                }

                this.MainChat.participants = this.logic.MessagesToParticipants(this.MainChat.messages, this.MainChat.participants);
                this.Refresh();
            }
        }

        

        private void Refresh_DGrid(object sender, RoutedEventArgs e)
        {
            this.Refresh();
        }

        private void Refresh()
        {
            DGrid.ItemsSource = null;
            if (this.MainChat != null && this.MainChat.participants != null)
                DGrid.ItemsSource = this.MainChat.participants;
        }
    }
}
