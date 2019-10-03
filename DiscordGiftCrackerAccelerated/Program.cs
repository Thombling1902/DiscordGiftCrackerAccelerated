using FSharp;

using System;
using System.Web;
using System.Web.Security;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Alea;
using Alea.CSharp;
using System.Threading;

namespace DiscordGiftCrackerAccelerated
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int thing1 = Device.Default.Attributes.MaxBlockDimX;
            int thing2 = Device.Default.Attributes.MaxGridDimX;
            Gpu.Default.EvalAction(new Action(Cracker));
        }

        public static void Cracker()
        {
            string[] proxyLines = File.ReadAllLines("./proxies.txt");
            string CreatePassword(int length)
            {
                const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                StringBuilder res = new StringBuilder();
                Random rnd = new Random();
                while (0 < length--)
                {
                    res.Append(valid[rnd.Next(valid.Length)]);
                }
                return res.ToString();
            }

            string randomGiftId = CreatePassword(16);
            string apiURL = $"https://discordapp.com/api/v6/entitlements/gift-codes/{randomGiftId}?with_application=false&with_subscription_plan=false";
            string url = $"https://discord.gift/{randomGiftId}";

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(apiURL);
            HttpWebResponse webResponse;

            try
            {
                webResponse = (HttpWebResponse)webRequest.GetResponse();

                Console.WriteLine($"Code: {randomGiftId} is valid. Writing to file.");
                File.AppendAllText("CorrectCodes.txt", $"{url}\n");
            }
            catch (WebException we)
            {
                Console.WriteLine($"Code: {randomGiftId} is invalid. Continuing...");
            }
            Thread.Sleep(TimeSpan.FromSeconds(5));
            Cracker();
        }
    }
}