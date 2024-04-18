using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace wordle
{
    public class Game
    {
        public static async Task<string> Start()
        {
            GameHandler gameHandler = new GameHandler() { Words = "HELLOS" };

            using (HttpClient client = new HttpClient())
            {
                var wordssss = await client.GetStringAsync("http://localhost:5088/api/word");
                Console.WriteLine($"Word: {wordssss}");
                string correctWord = wordssss;
                await GetWords(correctWord);
                return correctWord;
            }
        }

        private static async Task GetWords(string correctWord)
        {
            Console.WriteLine(correctWord);
            Console.WriteLine("Press Enter to get a new word");
            Console.ReadLine();
            Word word = new Word();
            word.Wordle(correctWord);
        }
    }
}







// using System.Net.Http;
// using System.Threading.Tasks;

// namespace wordle
// {
//     public class Game
//     {
//         public async Task Start()
//         {
//             // static async Task<string> 
//             GameHandler gameHandler = new GameHandler() { Words = "HELLOS" };

//             using (HttpClient client = new HttpClient())
//             {
//                 var wordssss = await client.GetStringAsync("http://localhost:5088/api/word");
//                 System.Console.WriteLine($"Word: {wordssss}");
//             }
//         }
//     }
// }









// using System.Net.Http.Json;

// namespace wordle
// {
//     public class Game
//     {
//         public static async Task Main()
//         {
//             // static async Task<string> 
//             GameHandler gameHandler = new GameHandler() { Words = "HELLOS" };

//             using (HttpClient client = new HttpClient())
//             {
//                 var wordssss = await client.GetStringAsync("http://localhost:5088/api/word");
//                 System.Console.WriteLine($"Word: {wordssss}");
//             }
//         }
//     }
// }










