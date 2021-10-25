using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Utillities;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Server
{
    class ServerProgram
    {

        static void Main(string[] args)
        {
            
            List<Category> categories = new List<Category>{
                new Category(1,"Beverages"),
                new Category(2,"Condiments"),
                new Category(3,"Confections")
            };

            var server = new TcpListener(IPAddress.Loopback, 5000);
            server.Start();
            Console.WriteLine("Server started");

            while (true)
            {
                var client = new NetworkClient(server.AcceptTcpClient());
                Console.WriteLine("Client accepted");

                var message = client.Read();
                
                Console.WriteLine($"Client message '{message}'");
                var deMessage = JsonSerializer.Deserialize<Request>(message, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                Console.WriteLine($"det her er deMessage {deMessage.Method}");
                if(deMessage.Method =="create"){
                var response = new
            {
                Status = "3",
                Body = JsonSerializer.Serialize("Rene")
            };
                //request.FromJson<>();
                //string jsonString = JsonSerializer.Serialize(response);
                //client.Write(ToJson(response));
                var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                client.Write(JsonResponse);
                Console.WriteLine(JsonResponse);
                }
                
            }


        }
        /*public class responseClass{
            public int status {get; set;}
            public String body {get; set;}
        }*/
    }

    public class Category
        {
            [JsonPropertyName("cid")]
            public int cid { get; set; }
            [JsonPropertyName("name")]
            public string name { get; set; }
            public Category( int cid, String name){

            }

        }

    public static class Util{
        public static string ToJson(this object data)
        {
            return JsonSerializer.Serialize(data, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        public static T FromJson<T>(this string element)
        {
            return JsonSerializer.Deserialize<T>(element, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        public static Request ReadRequest(this TcpClient client)
        {
            var strm = client.GetStream();
            //strm.ReadTimeout = 250;
            byte[] resp = new byte[2048];
            using (var memStream = new MemoryStream())
            {
                int bytesread = 0;
                do
                {
                    bytesread = strm.Read(resp, 0, resp.Length);
                    memStream.Write(resp, 0, bytesread);

                } while (bytesread == 2048);

                var requestData = Encoding.UTF8.GetString(memStream.ToArray());
                //Console.WriteLine($"Server says: {responseData}");
                return JsonSerializer.Deserialize<Request>(requestData, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                
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
    public class Request{
        public string Method{get; set;}
        public string Path{get; set;}
        public string Date{get; set;}
        public string Body {get; set;}
    }
}

