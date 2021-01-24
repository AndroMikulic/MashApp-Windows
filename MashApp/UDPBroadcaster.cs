using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MashApp
{
    class UDPBroadcaster
    {
        static int port = 1337;

        UdpClient udpServer;
        IPEndPoint ip;
        String localIP;
        int updateInterval = 1000;
        
        public String curPlaying = "";
        
        public void BroadcasterSetup()
        {
            Logger.Log("Starting UDP broadcaster");
            udpServer = new UdpClient(port);
            ip = new IPEndPoint(IPAddress.Broadcast, port);
            new Thread(BroadcastAddress).Start();
            new Thread(BroadcastCurrentlyPlaying).Start();
        }

        public void BroadcastAddress()
        {
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress addr in localIPs)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = addr.ToString();
                }
            }
            byte[] bytes = Encoding.UTF8.GetBytes("MAIP:" + localIP);
            Logger.Log("Broadcasting message: " + "MAIP:" + localIP);
            while (true)
            {
                udpServer.Send(bytes, bytes.Length, ip);
                Thread.Sleep(updateInterval);
            }
        }

        public void BroadcastCurrentlyPlaying()
        {
            while (true)
            {
                byte[] bytes = Encoding.UTF8.GetBytes("CURPL:" + curPlaying);
                if (!curPlaying.Equals("") && curPlaying != null)
                {
                    udpServer.Send(bytes, bytes.Length, ip);
                }
                Thread.Sleep(updateInterval / 2);
            }
        }
    }
}
