using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MashApp
{
    class RequestListener
    {
        static int port = 1337;

        TcpListener tcpListener;
        IPAddress localIP;

        public static String LIST_REQUEST = "LIST";
        public static String SONG_REQUEST = "SONG:";
        public static String IN_QUEUE_ERROR = "ERROR";
        public static String SONG_ADDED = "ADDED";
        public static String LUCK = "LUCK";
        public static int LUCK_BARRIER = 95; //[0, 100]

        public MainWindow mainRef;

        public void SetUpRequestListener()
        {
            Logger.Log("Setting up TCP Listener");
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress addr in localIPs)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = addr;
                }
            }
            tcpListener = new TcpListener(IPAddress.Any, port);
            new Thread(WaitForConnections).Start();
        }

        public void WaitForConnections()
        {
            Logger.Log("Listening on " + IPAddress.Any + ":" + port);
            tcpListener.Start();
            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                Thread requestHandlerInstance = new Thread(new ParameterizedThreadStart(HandleRequest));
                Logger.Log("New connection, IP not logged for privacy reasons");
                requestHandlerInstance.Start(client);
            }
        }

        public void HandleRequest(Object tcpClient)
        {
            Random rnd = new Random();
            TcpClient client = (TcpClient)tcpClient;
            Byte[] bytes = new Byte[512];
            String data = "";
            int i = 0;
            NetworkStream stream = client.GetStream();
            i = stream.Read(bytes, 0, bytes.Length);
            data = Encoding.UTF8.GetString(bytes, 0, i);
            if (data.Equals(LIST_REQUEST))
            {
                Logger.Log("Received a LIST_REQUEST");
                Byte[] name = Encoding.UTF8.GetBytes(mainRef.displayName + "\n");
                stream.Write(name, 0, name.Length);
                stream.Flush();
                foreach (String song in mainRef.songs)
                {
                    if (song == null)
                    {
                        break;
                    }
                    String tmp = song;
                    tmp += "\n";
                    Byte[] msg = Encoding.UTF8.GetBytes(tmp);
                    stream.Write(msg, 0, msg.Length);
                    stream.Flush();
                }
            }
            if (data.StartsWith(SONG_REQUEST))
            {
                String song = data.Substring(SONG_REQUEST.Length);
                Logger.Log("Received a SONG_REQUEST for song: " + song);
                if (mainRef.songs.Contains(song))
                {
                    if (mainRef.SONG_QUEUE.Contains(song) || mainRef.curPlaying.Equals(song))
                    {
                        Byte[] msg = Encoding.UTF8.GetBytes(IN_QUEUE_ERROR);
                        stream.Write(msg, 0, msg.Length);
                    }
                    else
                    {
                        mainRef.Dispatcher.Invoke(() =>
                        {
                            mainRef.inQueue.Items.Add(song);
                        });

                        mainRef.SONG_QUEUE.Enqueue(song);
                        Byte[] msg;
                        int luck = rnd.Next(0, 101);
                        if (luck >= LUCK_BARRIER)
                        {
                            msg = Encoding.UTF8.GetBytes(SONG_ADDED + ";" + LUCK);
                        }
                        else
                        {
                            msg = Encoding.UTF8.GetBytes(SONG_ADDED);
                        }
                        stream.Write(msg, 0, msg.Length);
                    }
                }
            }
            stream.Close();
            client.Close();
        }
    }
}
