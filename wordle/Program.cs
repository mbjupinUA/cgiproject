using System;
using System.Threading.Tasks;
using wordle;

namespace YourNamespace
{
    class Program
    {
        static async Task Main()
        {
            await Game.Start();
        }
    }
}

//Only c# was used for this project
/*
    Please make sure bestprojectever is running, this game will fail if
    bestprojectever is not running
    *** Make sure to do dotnet watch run for bestprojectever
    *** Make sure to do dotnet run for wordle
    *** Also if your local host is not http://localhost:5088/api/word in wordle change in Game class line 15
    from http://localhost:5088/api/word to what ever your localhost is provided in swagger
*/

/*
    Possible need to install in the terminal:
    * dotnet tool install -g dotnet-aspnet-codegenerator
    * dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
    
*/

/*
    Stuff I googled or got help from a TA:
    * I got help from Helene & Chase to help me create my personl API, and I used Pokemon example from class to help me
    to get api to work with Wordle program. I also used MIS 321 PowerPoint on Basic Web Project.
    * Chase helped me with words.Any(word => word == userGuess), i originally used words.Contain(userGuess) but it failed 
    to work as intented.
    * Using Dictinoary in Check methods. I originally was able to do it myself but i relized that
    English language had 5 letter words that had 3 of the same letters like "ERROR", with made it extra hard to do the coloring 
    based on real NewYork Times Wordle rules. 
    * All the words that i used for api and wordle.txt are from google (I tried to verify all the words, to make sure they are actually English).
    * I used ASCII to make wordle box
*/




