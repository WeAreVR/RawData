using System;
using System.Net.Sockets;
using System.Text;
using Utillities;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Client
{


    class ClientProgram
    {
        static void Main(string[] args)
        {

            Console.WriteLine("TestMain");
            
            //var client = new NetworkClient();

            //client.Connect("localhost", 3500);
            
            Request_DeleteCategoryWithValidId_RemoveCategory();
            //var message = "hello";
            //client.Write(message);
            
            //client.Write("test1");

            //var response = client.Read();

            //Console.WriteLine($"Server response '{response}'");
            
        }
        public class Category
        {
            [JsonPropertyName("cid")]
            public int Id { get; set; }
            [JsonPropertyName("name")]
            public string Name { get; set; }
        }
        private static string UnixTimestamp()
        {
            return DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        }
        static public void Request_DeleteCategoryWithValidId_RemoveCategory()
        {
            Console.WriteLine("test1");
            var client = Connect();
            var request = new
            {
                Method = "create",
                Path = "/api/categories",
                Date = UnixTimestamp(),
                Body = (new { name = "TestingDeleteCategory" }).ToJson()
            };
            Console.WriteLine(request);
            
            client.SendRequest(request.ToJson());
            var response = client.ReadResponse();

            //client = Connect();
            var verifyRequest = new
            {
                Method = "delete",
                Path = "/api/categories/" + response.Body.FromJson<Category>().Id,
                Date = UnixTimestamp()
            };

            client.SendRequest(verifyRequest.ToJson());
            response = client.ReadResponse();

            //Assert.Contains("1 ok", response.Status.ToLower());
            static TcpClient Connect()
            {
                var client = new TcpClient();
                Console.WriteLine("5000");
                client.Connect(IPAddress.Loopback, 5000);
                return client;
            }
        }

    }
    public class Response
    {
        public string Status { get; set; }
        public string Body { get; set; }
    }
    public static class Util
    {
        
        public static string ToJson(this object data)
        {
            return JsonSerializer.Serialize(data, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        public static T FromJson<T>(this string element)
        {
            return JsonSerializer.Deserialize<T>(element, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        public static void SendRequest(this TcpClient client, string request)
        {
            var msg = Encoding.UTF8.GetBytes(request);
            client.GetStream().Write(msg, 0, msg.Length);
            Console.WriteLine("test2");
        }

        public static Response ReadResponse(this TcpClient client)
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

                var responseData = Encoding.UTF8.GetString(memStream.ToArray());
                return JsonSerializer.Deserialize<Response>(responseData, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
        }
    }
}