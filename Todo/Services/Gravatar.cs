using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Services
{
    public static class Gravatar
    {
        public static string GetHash(string emailAddress)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.Default.GetBytes(emailAddress.Trim().ToLowerInvariant());
                var hashBytes = md5.ComputeHash(inputBytes);

                var builder = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("X2"));
                }
                return builder.ToString().ToLowerInvariant();
            }
        }

        public static string GetUserName(string emailAddress)
        {
            var hash = GetHash(emailAddress);
            var url = $"https://en.gravatar.com/";

            try
            {
                var client = new RestClient(url);
                var request = new RestRequest($"{hash}.json");
                var response = client.GetAsync(request).Result;
                Root root = JsonConvert.DeserializeObject<Root>(response.Content);

                return root.entry.First().name.formatted;
            } catch (Exception e)
            {
                return "-"; 
            }
        }
    }
}