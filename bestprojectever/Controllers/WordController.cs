using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bestprojectever.Models;
using System.Text.Json;
using Newtonsoft.Json;
using Azure.Core.Serialization;

namespace bestprojectever.Controllers
{
    [Route("api/word")]
    [ApiController]
    public class WordController
    {

        
        [HttpGet]
        public string GetTodoItems()
        {
            const int MAX_WORDS = 6000;
            Console.Clear();
            string[] words = new string[MAX_WORDS];
            int count = 0;
            StreamReader inFile = new StreamReader("wordle.txt");
            string line = inFile.ReadLine();
            while (line != null)
            {
                string[] temp = line.Split(' ');
                words[count] = temp[0];
                count++;
                line = inFile.ReadLine();
            }
            inFile.Close();

            Random rnd = new Random();
            int randomIndex = rnd.Next(0, count);
            return words[randomIndex].ToUpper();
        }
    }
}
