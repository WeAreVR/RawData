using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Utillities;
using System.Text.Json;
using System.IO;


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
                Status = "3",
                Body = JsonSerializer.Serialize("Rene")
            };
                //ToJson(response);
                //string jsonString = JsonSerializer.Serialize(response);
                //client.Write(ToJson(response));
                var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                client.Write(JsonResponse);
                Console.WriteLine(JsonResponse);
                }
                
            }


        }
    }

    public static class util{
        public static string ToJson(this object data)
        {
            return JsonSerializer.Serialize(data, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
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
