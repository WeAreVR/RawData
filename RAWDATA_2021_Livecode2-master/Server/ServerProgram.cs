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
                //var temp = deMessage.Body.FromJson<Category>();
                //Console.WriteLine(deMessage.Body);
                //Console.WriteLine(deMessage.Body.GetType());
                //Console.WriteLine(DateTimeOffset.Now.ToUnixTimeSeconds());

                if (deMessage.Date == null && deMessage.Method == null)
                {
                    var response = new
                    {
                        Status = "4 Bad request Missing date Missing method",
                        // Body = JsonSerializer.Serialize("Missing date")
                    };
                    Console.WriteLine(response);
                    var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                    client.Write(JsonResponse);
                    Console.WriteLine(JsonResponse);
                }


                else if (deMessage.Date == null)
                {
                    var response = new
                    {
                        Status = "4 Bad request Missing date",
                        // Body = JsonSerializer.Serialize("Missing date")
                    };
                    Console.WriteLine(response);
                    var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                    client.Write(JsonResponse);
                    Console.WriteLine(JsonResponse);
                }


                else if (deMessage.Method == null)
                {
                    var response = new
                    {
                        Status = "4 Bad request Missing method",
                        //Body = JsonSerializer.Serialize("Missing method")
                    };
                    Console.WriteLine(response);
                    var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                    client.Write(JsonResponse);
                    Console.WriteLine(JsonResponse);
                }
                else if (deMessage.Date == null)
                {
                    var response = new
                    {
                        Status = "4 Bad request Missing Date",
                      //  Body = JsonSerializer.Serialize("Missing date")
                    };
                    Console.WriteLine(response);
                    var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                    client.Write(JsonResponse);
                    Console.WriteLine(JsonResponse);

                }

                else if (deMessage.Method == "echo")
                {
                    if (deMessage.Body == null)
                    {
                        var response = new
                        {
                            Status = "4 Bad request Missing body",
                            //Body = JsonSerializer.Serialize("Missing body")
                        };
                        Console.WriteLine(response);
                        var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                        client.Write(JsonResponse);
                        Console.WriteLine(JsonResponse);
                    }
                     else {
                     var response = new
                    {
                        Status = "1 Ok",
                        Body =deMessage.Body
                        
                    };
                    Console.WriteLine(response);
                        var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                        client.Write(JsonResponse);
                        Console.WriteLine(JsonResponse);
                    };

                }

                if (deMessage.Path == null)
                {
                    var response = new
                    {
                        Status = "4 Bad request missing resource",
                        //Body = JsonSerializer.Serialize("Missing path")
                    };
                    Console.WriteLine(response);
                        var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                        client.Write(JsonResponse);
                        Console.WriteLine(JsonResponse);
                }
                else if(!deMessage.Path.StartsWith("/api/categories") && deMessage.Path != null && deMessage.Path != "testing"){
                    var response = new
                    {
                        Status = "4 Bad request invalid path missing body",
                        //Body = JsonSerializer.Serialize("Invalid path")
                    };
                    Console.WriteLine(response);
                        var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                        client.Write(JsonResponse);
                        Console.WriteLine(JsonResponse);
                }
                else if (deMessage.Method == "read")
                {
                    if (deMessage.Path == "/api/categories")
                    {
                        var response = new
                        {
                            Status = "1 Ok",
                            Body = JsonSerializer.Serialize(categories)
                        };
                        //Console.WriteLine("her sker der noget" + categories[1]);
                        //Console.WriteLine(list_category);
                        Console.WriteLine(response);
                        var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                        client.Write(JsonResponse);
                        Console.WriteLine(JsonResponse);
                    }
                    else
                    {
                        var catiId = new List<string>(deMessage.Path.Split('/'));
                        var sub = catiId[catiId.Count - 1];
                        var lastValue = int.Parse(sub);
                        if (lastValue >= 1 && lastValue <= categories.Count - 1)
                        {
                            var response = new
                            {
                                Status = "1 Ok",
                                Body = JsonSerializer.Serialize(categories[lastValue - 1])
                            };

                            Console.WriteLine(response);
                            var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                            client.Write(JsonResponse);
                            Console.WriteLine(JsonResponse);
                        }
                        else
                        {
                            var response = new
                            {
                                Status = "5 Not found Invalid id",
                               // Body = JsonSerializer.Serialize("Invalid Id")
                            };
                            Console.WriteLine(response);
                            var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                            client.Write(JsonResponse);
                            Console.WriteLine(JsonResponse);
                        }
                    }



                    /*
                    var response = new
                    {
                        Status = "3",
                        Body = JsonSerializer.Serialize("Rene")
                    };
                    */
                    //request.FromJson<>();
                    //string jsonString = JsonSerializer.Serialize(response);
                    //client.Write(ToJson(response));
                    /*var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                    client.Write(JsonResponse);
                    Console.WriteLine(JsonResponse);
                    */
                }
                else if (deMessage.Method == "create")
                {
                    if (deMessage.Body == null)
                    {
                        var response = new
                        {
                            Status = "4 Bad request Missing body",
                           // Body = JsonSerializer.Serialize("Missing body")
                        };
                        Console.WriteLine(response);
                        var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                        client.Write(JsonResponse);
                        Console.WriteLine(JsonResponse);
                    }
                    else{
                        var temp = deMessage.Body.FromJson<Bodybody>();
                        int length = categories.Count;
                       // categories.Add(new Category{2,"temp.name"});
                         var response = new
                        {
                            Status = "4 Bad request",
                            //Body = JsonSerializer.Serialize("YEs")
                        };
                        Console.WriteLine(response);
                        var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                        client.Write(JsonResponse);
                        Console.WriteLine(JsonResponse);

                    }
                }
                else if (deMessage.Method == "update")
                {
                    if (deMessage.Body == null)
                    {
                        var response = new
                        {
                            Status = "4 Bad request Missing body",
                            //Body = JsonSerializer.Serialize("Missing body")
                        };
                        Console.WriteLine(response);
                        var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                        client.Write(JsonResponse);
                        Console.WriteLine(JsonResponse);
                    }
                    else if (deMessage.Path == "/api/categories" )
                    {
                         var response = new
                        {
                            Status = "4 Bad request",
                           // Body = JsonSerializer.Serialize("Missing Path Id")
                        };
                        Console.WriteLine(response);
                        var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                        client.Write(JsonResponse);
                        Console.WriteLine(JsonResponse);

                    }
                    
                    
                    else {
                        var catiId = new List<string>(deMessage.Path.Split('/'));
                        var sub = catiId[catiId.Count - 1];
                        var lastValue = int.Parse(sub);
                        var temp = deMessage.Body.FromJson<Category>();
                        if (lastValue >= 1 && lastValue <= categories.Count - 1)
                        {
                         
                        categories[temp.cid-1].name = temp.name;
                        Console.WriteLine("update 32432432");

                        var response = new
                        {
                            Status = "3 Updated",
                            Body = JsonSerializer.Serialize(categories[temp.cid-1])
                        };

                        Console.WriteLine(response);
                        var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                        client.Write(JsonResponse);
                        Console.WriteLine(JsonResponse);
                        }
                        else
                        {
                            var response = new
                            {
                                Status = "5 Not found Invalid Id",
                               // Body = JsonSerializer.Serialize("Invalid Id")
                            };
                            Console.WriteLine(response);
                            var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                            client.Write(JsonResponse);
                            Console.WriteLine(JsonResponse);

                        
                    }
                    }
 
                }
                else if (deMessage.Method == "delete")
                {
                    if (deMessage.Path == "/api/categories" )
                    {
                         var response = new
                        {
                            Status = "4 Bad request",
                           // Body = JsonSerializer.Serialize("Missing Path Id")
                        };
                        Console.WriteLine(response);
                        var JsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                        client.Write(JsonResponse);
                        Console.WriteLine(JsonResponse);
                    }
                }
                else
                {
                    var response = new
                    {
                        Status = "4 Illegal method",
                       // Body = JsonSerializer.Serialize("Illegal method")
                    };
                    Console.WriteLine(response);
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
        //[JsonPropertyName("cid")]
        public int cid { get; set; }
        //[JsonPropertyName("name")]
        public string name { get; set; }
        public Category(int cid1, String name1)
        {
            cid = cid1;
            name = name1;
        }
        public override string ToString()
        {
            return cid + " " + name;
        }

    }

    public class Bodybody{
        public string name;
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
        public static Request ReadRequest(this TcpClient client)
        {
            var strm = client.GetStream();
            strm.ReadTimeout = 250;
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
    public class Request
    {
        public string Method { get; set; }
        public string Path { get; set; }
        public string Date { get; set; }
        public string Body { get; set; }
    }
}

