using System.Windows;
using System;
using System.IO;

namespace MashApp
{
    /// <summary>
    /// Interaction logic for ChangeName.xaml
    /// </summary>
    public partial class ChangeName : Window
    {
        public ChangeName()
        {
            InitializeComponent(); 
            nameEntered.Click += NameEntered;
        }

        public void NameEntered(Object obj, RoutedEventArgs e)
        {
            if(barName.Text.Length == 0)
            {
                return;
            }
            if (File.Exists("name.txt"))
            {
                File.Delete("name.txt");
            }
            StreamWriter newFile = File.AppendText("name.txt");
            newFile.Write(barName.Text);
            newFile.Close();
            System.Windows.Forms.Application.Restart();
            Environment.Exit(Environment.ExitCode);
        }
    }
}
