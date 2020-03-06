using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace afv1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            users = new ObservableCollection<User>();

           
        }
        public ObservableCollection<User> users { get; }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == true)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            string[] text = File.ReadAllLines(openFileDialog1.FileName);
                            foreach (string str in text)
                            {
                                users.Add(new User(str));
                                listbox1.ItemsSource = users;
                                LinesOnBar.Content = $"Number of lines: {users.Count}";
                                TimeLoadedOnBar.Content = $"Lasted upload at: {DateTime.Now}";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void listbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listbox1.SelectedIndex == -1) return;
            idTextBlock.DataContext = listbox1.SelectedItem;
            nameTextBlock.DataContext = listbox1.SelectedItem;
            ageTextBlock.DataContext = listbox1.SelectedItem;
            scoreTextBlock.DataContext = listbox1.SelectedItem;
           
        }

        private void selectedUserBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void numberOfLines_SelectionChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }
    }

    public class User : INotifyPropertyChanged
    {
        public User(string data)
        {
            // Name, Age, Weight, Score
            var L = data.Split(';');
            Id = int.Parse(L[0]);
            Name = L[1];
            Age = int.Parse(L[2]);
            Score = int.Parse(L[3]);
            
        }
        public int Id { get; set; }
        public String  Name { get; set; }

        public int Age { get; set; }

        public int Score { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return String.Format("ID " + Id + " Name " + Name);
        }
    }   
}
