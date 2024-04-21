using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace wordle
{
    public class Game
    {
        public static async Task<string> Start()
        {
            GameHandler gameHandler = new GameHandler() { Words = "HELLO" };

            using (HttpClient client = new HttpClient())
            {
                var wordssss = await client.GetStringAsync("http://localhost:5088/api/word");
                string correctWord = wordssss;
                await GetWords(correctWord);
                return correctWord;
            }
        }

        private static async Task GetWords(string correctWord)
        {
            Word word = new Word();
            word.Wordle(correctWord);
        }
    }
}

