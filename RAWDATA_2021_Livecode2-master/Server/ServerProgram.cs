using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Utillities;


namespace Server
{
    class ServerProgram
    {
        static void Main(string[] args)
        {
            var server = new TcpListener(IPAddress.Loopback, 5000);
            server.Start();
            Console.WriteLine("Server started");

            while (true)
            {
                var client = new NetworkClient(server.AcceptTcpClient());
                Console.WriteLine("Client accepted");

                var message = client.Read();
                if(message != "hello"){
                Console.WriteLine($"Client message '{message}'");

                var response = new
            {
                Status = 3,
                Body = "SPurgT"
            };
                client.Write(response.Body);
                }
                Console.WriteLine(Response.Body);
            }


        }
        /*public static void SendResponse(this TcpClient client, string response)
        {
            var msg = Encoding.UTF8.GetBytes(response);
            client.GetStream().Write(msg, 0, msg.Length);
            Console.WriteLine("vi har sendt response!!!!");
        }*/
        
    }
    public class Response
    {
        public string Status { get; set; }
        public string Body { get; set; }
    }
}
