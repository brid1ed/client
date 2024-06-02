using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Network {

    public class Packet
    {
        private SendType type;

        public Packet(SendType type)
        {
            this.type = type;
        }
        
        
        
        
    }
    public class ClientLogin {


        private IPAddress ip;
        private int port;
        private static TcpClient client;


        private bool logined;
        public ClientLogin(IPAddress ip, int port) {
            client = new TcpClient();
            logined = false;
            this.ip = ip;
            this.port = port;
        }
        
        
        public void Connect() {
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            try {
                client.Connect(endPoint);
                Debug.Log("Login TCP Connected...");
                Debug.Log(client.Connected);
            }
            catch (SocketException se)
            {
                Debug.Log(se.Message);
            }
        }
        
        
        
        public IEnumerator Receive() {
            while (true) {
                yield return null;
                
                if (!client.Connected) continue;
                if (logined) break;
                try {
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                        if (bytes <= 0) continue;
                        string message = Encoding.UTF8.GetString(buffer, 0, bytes);
                        Debug.Log("[Other] " + message);
                        
                } catch (Exception ex) { Debug.Log(ex.ToString()); }
                
                
            }
        }

        public void Send(JObject json)
        {
            string message = json.ToString();
            if (message.Length <= 0) return;

            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);

                Debug.Log("[Me] " + message);
            }
            catch (Exception e)
            {
                
            }
        }
        
        
        
        
    }
    
    
    
    public class Client {
        
        
        public static int PORT = 6163;
        public static string ADDRESS = "118.221.195.32";
        
        
        
        private Socket socket;
        private byte[] data;
        IPAddress server_address;
        private IPEndPoint sender;

        public ClientLogin login;
        
        
        public Client() {
            data = new byte[2028];
            socket  = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            server_address = IPAddress.Parse(ADDRESS);
            sender = new IPEndPoint(server_address, PORT);
            login = new ClientLogin(server_address, PORT+1);

        }


        private string GetId(SendType type)
        {
            switch (type) {
                case SendType.JOIN:
                    return "join";
                case SendType.MOVE:
                    return "move";
                case SendType.ATTACK:
                    return "attack";
                case SendType.CHROOM:
                    return "change_room";
                case SendType.LOGIN:
                    return "login";
                default:
                    return "null";
            }
        }
        
        private JObject MakeJson(SendType type, string token, long player_id, int channel_id) {
            JObject obj =  new JObject(){
                {"type", GetId(type)},
                {"token", token},
                {"player_id", player_id.ToString()},
                {"channel_id", channel_id.ToString()}
            };
            Debug.Log(obj.ToString()+"\nend");
            return obj;
        }
        
        
        
        public void Parser(ref byte[] req) {
            Debug.Log(req);
            // {"type": "join", "token": "token", "player_id": "id", "channel_id": "1"}

        }

        public void Receive(int port) {
            
            // IPEndPoint server - new IPEndPoint()
        }
        
        public void Join() {
            JObject json = MakeJson(SendType.JOIN, "token", 123, 123);
            
            byte[] send = Encoding.UTF8.GetBytes(json.ToString());
            
            try {
                socket.SendTo(send, send.Length, SocketFlags.None, sender);
                
            }
            catch ( Exception e ){
                Console.WriteLine(e.ToString());	
            }
            Debug.Log("send this");

        }

        public void Move() {
            // {"type": "move", "player_id": "id", "x": 10, "y": 10} // 현제 x y위치 쓰는거
            

        }



        public void Login(string name, string passwd) {
            
            login.Connect();
            login.Send(new JObject()
                {
                    {"type", GetId(SendType.JOIN)},
                    {"name", name},
                    {"passwd", passwd}
                }
            );
            
            
        }

    }
    
    
}