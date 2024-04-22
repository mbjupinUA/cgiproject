namespace wordle
{
    public class Game
    {
        public static async Task<string> Start()
        {
            using (HttpClient client = new HttpClient())
            {
                var word = await client.GetStringAsync("http://localhost:5088/api/word");
                return word;
            }
        }
    }
}

