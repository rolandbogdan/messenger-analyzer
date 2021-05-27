using Messenger_Analyzer.BL;
using Messenger_Analyzer.VM;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
            {
                DGrid.ItemsSource = this.MainChat.participants;
                allMessageCount.Content = this.MainChat.messages.Count;
                nameLabel.Content = this.MainChat.title;
            }

        }

        private void Filter_ButtonClick(object sender, RoutedEventArgs e)
        {
            MainLogic ml = new MainLogic();
            SmallDgrid.ItemsSource = null;
            if (this.MainChat != null && this.MainChat.participants != null && filterBox.Text != string.Empty)
            {
                foreach (Participant participant in MainChat.participants)
                {
                    participant.FilteredWordCount = 0;
                }
                this.MainChat.participants = ml.FilteredWordCounter(MainChat.participants, filterBox.Text);
                SmallDgrid.ItemsSource = this.MainChat.participants;
            }
        }

        private void Export_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                StreamWriter sw = new StreamWriter((DGrid.SelectedItem as Participant).name + ".txt");
                foreach (Message msg in (DGrid.SelectedItem as Participant).Messages)
                {
                    sw.WriteLine(msg.content);
                }
                sw.Close();
                MessageBox.Show("Export ok");
            }
            else
            {
                MessageBox.Show("Export wasnt ok");
            }
        }
    }
}
