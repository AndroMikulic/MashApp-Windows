using System;
using System.Windows;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Navigation;

namespace MashApp


{
    public partial class MainWindow : Window
    {
        UDPBroadcaster udpBroadcaster;
        RequestListener requestListener;
        public String displayName = "";
        static String musicDir = "";
        static Thread musicPlayer;
        public Queue<String> SONG_QUEUE = new Queue<String>();
        public String[] songs;
        public String curPlaying;

        //Youtube Specific
        public List<Tuple<String, String>> youtubeLinks = new List<Tuple<String, String>>();
        public bool youtubeMode = false;
        static String youtubeListLocation = "";
        public bool shouldReload = true;
        public bool showToggle = false;

        public string websiteURL = "http://mashapp.eu";

        public MainWindow()
        {
            InitializeComponent();
            Closing += DataWindow_Closing;
            changeName.Click += OpenChangeNameWindow;
            changeList.Click += OpenChangeListWindow;
            if (!File.Exists("music_dir.txt") || !File.Exists("name.txt"))
            {
                selectFolder.MouseDown += SelectFolder;
                selectFolder.MouseEnter += ZoomIn;
                selectFolder.MouseLeave += ZoomOut;
                selectYoutube.MouseDown += SelectYoutube;
                selectYoutube.MouseEnter += ZoomIn;
                selectYoutube.MouseLeave += ZoomOut;
                nameEntered.Click += NameEntered;
                this.Dispatcher.Invoke(() =>
                {
                    firstTimeGrid.Visibility = Visibility.Visible;
                });
                Logger.Log("Entering first time setup");
                new Thread(FirstTimeSetup).Start();
            }
            else
            {
                Logger.Log("Loading startup files");
                StreamReader musicDirFile = new StreamReader("music_dir.txt");
                StreamReader nameFile = new StreamReader("name.txt");
                musicDir = musicDirFile.ReadLine();
                if (musicDir.Equals("YOUTUBE"))
                {
                    Logger.Log("Entering YouTube mode");
                    youtubeMode = true;
                    showVideo.Visibility = Visibility.Visible;
                    StreamReader youtubeListFile = new StreamReader("youtubeListLocation.txt");
                    youtubeListLocation = youtubeListFile.ReadLine();
                    youtubeListFile.Close();
                }
                displayName = nameFile.ReadLine();
                musicDirFile.Close();
                nameFile.Close();
                this.Dispatcher.Invoke(() =>
                {
                    startupGrid.Visibility = Visibility.Visible;
                });
                new Thread(InitializeApp).Start();
            }
        }

        public void FirstTimeSetup()
        {
            FadeIn(new UIElement[] { introText }, 150);
            Thread.Sleep(1500);
            FadeOut(new UIElement[] { introText }, 150);

            this.Dispatcher.Invoke(() =>
            {
                introText.Content = "Welcome to MashApp";
            });
            FadeIn(new UIElement[] { introText }, 150);
            Thread.Sleep(2500);
            FadeOut(new UIElement[] { introText }, 150);

            this.Dispatcher.Invoke(() =>
            {
                introText.Content = "Please enter your place's name, you can change it any time.";
                placeName.Visibility = Visibility.Visible;
                nameEntered.Visibility = Visibility.Visible;

            });
            FadeIn(new UIElement[] { introText }, 150);
            FadeIn(new UIElement[] { placeName, nameEntered }, 150);
            while (displayName.Equals("")) { }
            FadeOut(new UIElement[] { introText, placeName, nameEntered }, 150);

            this.Dispatcher.Invoke(() =>
            {
                introText.Content = "Would you like to play music from YouTube or your local folder?";
                placeName.Visibility = Visibility.Collapsed;
            });
            FadeIn(new UIElement[] { introText }, 150);
            this.Dispatcher.Invoke(() =>
            {
                selectFolder.Visibility = Visibility.Visible;
                selectYoutube.Visibility = Visibility.Visible;
            });
            FadeIn(new UIElement[] { selectFolder, selectYoutube }, 150);
            while (musicDir.Equals("")) { }
            Logger.Log("Selected music source is: " + musicDir);
            FadeOut(new UIElement[] { introText, selectFolder, selectYoutube }, 150);
            this.Dispatcher.Invoke(() =>
            {
                startupGrid.Visibility = Visibility.Visible;
            });
            new Thread(InitializeApp).Start();
            return;
        }

        public void InitializeApp()
        {
            Logger.Log("Initializing the app");
            udpBroadcaster = new UDPBroadcaster();
            requestListener = new RequestListener();
            requestListener.mainRef = this;
            new Thread(PostConnectionSetup).Start();
        }

        public void PostConnectionSetup()
        {
            Logger.Log("Entering post connection setup");
            this.Dispatcher.Invoke(() =>
            {
                connectionLabel.Content = "Connected!";
                loadingBar.Spin = false;
                loadingBar.Icon = FontAwesome.WPF.FontAwesomeIcon.Globe;
            });
            Thread.Sleep(1500);
            int opacity = 100;
            FadeOut(new UIElement[] { connectionLabel, loadingBar }, 250);
            FadeIn(new UIElement[] { logoHolder }, 500);
            Thread.Sleep(1500);
            while (true)
            {
                this.Dispatcher.Invoke(() =>
                {
                    logoHolder.Opacity -= 0.04;
                    logoHolder.Height += 8;
                    logoHolder.Width += 8;
                    opacity -= 4;
                });
                Thread.Sleep(10);
                if (opacity == 0)
                {
                    break;
                }
            }
            this.Dispatcher.Invoke(() =>
            {
                startupGrid.Visibility = Visibility.Collapsed;
                if (youtubeMode)
                {
                    showVideo.Visibility = Visibility.Visible;
                    StreamReader file = new StreamReader(youtubeListLocation);
                    while (true)
                    {
                        String songName = file.ReadLine();
                        if (songName == null)
                        {
                            break;
                        }
                        String songLink = file.ReadLine();
                        Tuple<String, String> t = new Tuple<String, String>(songName, songLink);
                        youtubeLinks.Add(t);
                    }
                    youtubeLinks.Sort();
                    songs = new String[youtubeLinks.Count];
                    int i = 0;
                    foreach (Tuple<String, String> t in youtubeLinks)
                    {
                        songs[i] = t.Item1;
                        allSongs.Items.Add(t.Item1);
                        i++;
                    }
                    file.Close();
                    youtubeLinks.Sort();
                }
                else
                {
                    String[] tempSongs = Directory.GetFiles(musicDir);
                    songs = new String[tempSongs.Length];
                    int i = 0;
                    foreach (String song in tempSongs)
                    {
                        if (song.EndsWith("mp3") || song.EndsWith("MP3"))
                        {
                            songs[i] = Path.GetFileName(song);
                            i++;
                            allSongs.Items.Add(Path.GetFileNameWithoutExtension(song));
                        }
                    }
                }
                dataGrid.Visibility = Visibility.Visible;
            });
            new Thread(requestListener.SetUpRequestListener).Start();
            new Thread(udpBroadcaster.BroadcasterSetup).Start();
            if (youtubeMode)
            {
                this.Dispatcher.Invoke(() =>
                {
                    VideoPlayer.LoadCompleted += VideoLoaded;
                    PlayYoutubeVideo();
                });
            }
            else
            {
                musicPlayer = new Thread(MusicPlayer);
                musicPlayer.Start();
            }
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

        public void FadeIn(UIElement[] elements, int milis)
        {
            double opacity = 0.0;
            while (opacity < 1.0)
            {
                foreach (UIElement e in elements)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        e.Opacity += 1.0 / milis;
                    });
                }
                opacity += 1.0 / milis;
                Math.Round(opacity, 4);
                Thread.Sleep(1);
            }
        }

        public void FadeOut(UIElement[] elements, int milis)
        {
            double opacity = 1.0;
            while (opacity > 0.0)
            {
                foreach (UIElement e in elements)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        e.Opacity -= 1.0 / milis;
                    });
                }
                opacity -= 1.0 / milis;
                Math.Round(opacity, 4);
                Thread.Sleep(1);
            }
        }

        public void SelectYoutube(object sender, RoutedEventArgs e)
        {
            youtubeMode = true;
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
                youtubeListLocation = fbd.FileName;
                if (File.Exists("youtubeListLocation.txt"))
                {
                    File.Delete("youtubeListLocation.txt");
                }
                StreamWriter youtubeListLocationFile = File.AppendText("youtubeListLocation.txt");
                youtubeListLocationFile.Write(youtubeListLocation);
                youtubeListLocationFile.Close();
                musicDir = "YOUTUBE";
            }
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
                musicDir = fbd.SelectedPath.ToString();
            }
        }

        public void NameEntered(Object obj, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                displayName = placeName.Text.ToString();
                if (File.Exists("name.txt"))
                {
                    File.Delete("name.txt");
                }
                StreamWriter newFile = File.AppendText("name.txt");
                newFile.Write(displayName);
                newFile.Close();
            });
        }

        //**********YOUTUBE PLAYER**************
        public void PlayYoutubeVideo()
        {
            Random rnd = new Random();
            String song = "";
            String youtubeLink = "";
            if (SONG_QUEUE.Count != 0)
            {
                song = SONG_QUEUE.Dequeue();
                foreach (Tuple<String, String> t in youtubeLinks)
                {
                    if (t.Item1.Equals(song))
                    {
                        youtubeLink = t.Item2;
                        youtubeLink += "?autplay=1&showinfo=0";
                        VideoPlayer.Navigate(youtubeLink);
                        break;
                    }
                }
                this.Dispatcher.Invoke(() =>
                {
                    inQueue.Items.RemoveAt(0);
                });
                curPlaying = song;
                currentlyPlaying.Text = song;
                udpBroadcaster.curPlaying = song;
                Logger.Log("Playing \"" + song + "\" from Youtube");
            }
            else
            {
                int nextIndex = rnd.Next(0, songs.Length);
                this.Dispatcher.Invoke(() =>
                {
                    inQueue.Items.Add(songs[nextIndex]);
                });
                SONG_QUEUE.Enqueue(songs[nextIndex]);
                PlayYoutubeVideo();
            }
        }

        private void VideoLoaded(object sender, NavigationEventArgs e)
        {
            mshtml.HTMLDocument document = VideoPlayer.Document as mshtml.HTMLDocument;
            mshtml.IHTMLElementCollection divCol;
            new Thread(() =>
            {
                while (true)
                {
                    /*
                    *  Check if a video ad was loaded, if so: reload
                    */
                    divCol = document.getElementsByTagName("div");
                    foreach(mshtml.IHTMLElement element in divCol)
                    {
                        if (element.className != null)
                        {
                            //Console.WriteLine(element.className);
                            if (element.className.Equals("ytp-ad-player-overlay"))
                            {
                                Console.WriteLine("Contains video ads, reloading");
                                this.Dispatcher.Invoke(() =>
                                {
                                    VideoPlayer.Navigate(VideoPlayer.Source);
                                });
                                return;
                            }
                        }
                    }

                    /*
                    * Check if video slider's scale is 1, if so: load next video
                    */
                    //Thread.Sleep(0);
                    mshtml.HTMLDocument tdocument;
                    this.Dispatcher.Invoke(() =>
                    {
                        tdocument = VideoPlayer.Document as mshtml.HTMLDocument;
                    });
                    mshtml.IHTMLElementCollection tcol = document.getElementsByTagName("div");
                    foreach (mshtml.IHTMLElement el in tcol)
                    {
                        try
                        {
                            String classname = el.getAttribute("className");
                            if (classname.Equals("ytp-play-progress ytp-swatch-background-color"))
                            {
                                String scale = el.style.getAttribute("transform");
                                if (scale.StartsWith("scaleX(1"))
                                {
                                    this.Dispatcher.Invoke(() =>
                                    {
                                        PlayYoutubeVideo();
                                    });
                                    return;
                                }
                                break;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }).Start();
        }
        //**************************************

        //**********LOCAL MUSIC PLAYER**********
        public void MusicPlayer()
        {
            mediaPlayer.MediaEnded += SongFinished;
            this.Dispatcher.Invoke(() =>
            {
                mediaPlayer.LoadedBehavior = System.Windows.Controls.MediaState.Manual;
            });
            SongFinished(null, null);
        }

        public void SongFinished(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            String song = "";
            if (SONG_QUEUE.Count != 0)
            {
                song = SONG_QUEUE.Dequeue();
                this.Dispatcher.Invoke(() =>
                {
                    mediaPlayer.Source = new Uri(musicDir + "\\" + song);
                    curPlaying = song;
                    currentlyPlaying.Text = song;
                    udpBroadcaster.curPlaying = song;
                    inQueue.Items.RemoveAt(0);
                    mediaPlayer.Play();
                    Logger.Log("Playing song \"" + song + "\" from storage");
                });
            }
            else
            {
                int nextIndex = rnd.Next(0, songs.Length - 1);
                this.Dispatcher.Invoke(() =>
                {
                    inQueue.Items.Add(songs[nextIndex]);
                });
                SONG_QUEUE.Enqueue(songs[nextIndex]);
                SongFinished(null, null);
            }
        }
        //**************************************

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        void OpenChangeNameWindow(object sender, RoutedEventArgs e)
        {
            ChangeName changeName = new ChangeName();
            changeName.barName.Text = displayName;
            changeName.Show();
        }

        void OpenChangeListWindow(object sender, RoutedEventArgs e)
        {
            ChangeList changeList = new ChangeList();
            changeList.Show();
        }

        private void showVideo_Click(object sender, RoutedEventArgs e)
        {
            showToggle = !showToggle;
            if (showToggle)
            {
                VideoPlayer.Visibility = Visibility.Visible;
                showVideo.Content = "Hide Video";
            }
            else
            {
                VideoPlayer.Visibility = Visibility.Collapsed;
                showVideo.Content = "Show Video";
            }
        }

        private void OpenWebsite(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(websiteURL);
        }
    }
}
