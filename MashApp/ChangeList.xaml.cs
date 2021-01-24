using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MashApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ChangeList : Window
    {
        bool selected = false;

        public ChangeList()
        {
            InitializeComponent();
            selectFolder.MouseDown += SelectFolder;
            selectFolder.MouseEnter += ZoomIn;
            selectFolder.MouseLeave += ZoomOut;
            selectYoutube.MouseDown += SelectYoutube;
            selectYoutube.MouseEnter += ZoomIn;
            selectYoutube.MouseLeave += ZoomOut;
        }

        public void ZoomIn(Object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                for (int i = 0; i < 32; i++)
                {
                    bool flag = false;
                    this.Dispatcher.Invoke(() =>
                    {
                        if (!((System.Windows.Controls.Image)sender).IsMouseOver)
                        {
                            flag = true;
                        }
                        if (((System.Windows.Controls.Image)sender).Width == 96)
                        {
                            flag = true;
                        }
                        else
                        {
                            ((System.Windows.Controls.Image)sender).Width++;
                            ((System.Windows.Controls.Image)sender).Height++;
                        }

                    });
                    if (flag)
                    {
                        break;
                    }
                    Thread.Sleep(1);
                }
            }).Start();
        }

        public void ZoomOut(Object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                for (int i = 0; i < 32; i++)
                {
                    bool flag = false;
                    this.Dispatcher.Invoke(() =>
                    {
                        if (((System.Windows.Controls.Image)sender).IsMouseOver)
                        {
                            flag = true;
                        }
                        if (((System.Windows.Controls.Image)sender).Width == 64)
                        {
                            flag = true;
                        }
                        else
                        {
                            ((System.Windows.Controls.Image)sender).Width--;
                            ((System.Windows.Controls.Image)sender).Height--;
                        }
                    });
                    if (flag)
                    {
                        break;
                    }
                    Thread.Sleep(1);
                }
            }).Start();
        }

        private void SelectYoutube(object sender, RoutedEventArgs e)
        {
            if (File.Exists("music_dir.txt"))
            {
                File.Delete("music_dir.txt");
            }
            StreamWriter newFile = File.AppendText("music_dir.txt");
            newFile.Write("YOUTUBE");
            newFile.Close();
            var fbd = new OpenFileDialog();
            fbd.DefaultExt = "txt";
            fbd.Title = "Select your youtube list file";
            DialogResult result = fbd.ShowDialog();
            if (fbd.FileName != null && !fbd.FileName.Equals(""))
            {
                if (File.Exists("youtubeListLocation.txt"))
                {
                    File.Delete("youtubeListLocation.txt");
                }
                StreamWriter youtubeListLocationFile = File.AppendText("youtubeListLocation.txt");
                youtubeListLocationFile.Write(fbd.FileName);
                youtubeListLocationFile.Close();
                selected = true;
            }
            SelectionCheck();
        }

        public void SelectFolder(Object obj, RoutedEventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.Description = "Select your music folder";
            DialogResult result = fbd.ShowDialog();
            if (fbd.SelectedPath != null && !fbd.SelectedPath.Equals(""))
            {
                if (File.Exists("music_dir.txt"))
                {
                    File.Delete("music_dir.txt");
                }
                StreamWriter newFile = File.AppendText("music_dir.txt");
                newFile.Write(fbd.SelectedPath);
                newFile.Close();
                selected = true;
            }
            SelectionCheck();
        }

        void SelectionCheck()
        {
            if (selected)
            {
                System.Windows.Forms.Application.Restart();
                Environment.Exit(Environment.ExitCode);
            }
        }
    }
}
