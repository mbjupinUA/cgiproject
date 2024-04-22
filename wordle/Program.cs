using wordle;

namespace YourNamespace
{
    class Program
    {
        public static async Task Main()
        {
            Word word = new Word();
            await word.GetCorrectWord();
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
    to get api to work with Wordle program. I also used MIS 321 PowerPoint on Basic Web Project
    * Chase helped me with words.Any(word => word == userGuess), i originally used words.Contain(userGuess) but it failed
    to work as intented
    * Using Dictinoary in Check methods. I originally was able to do it myself but i relized that
    English language had 5 letter words that had 3 of the same letters like "ERROR", with made it extra hard to do the coloring
    based on real NewYork Times Wordle rules
    * All the words that i used for api and wordle.txt are from google (I tried to verify all the words, to make sure they are actually English)
    * I used ASCII to make wordle box
*/

/*
    Summary on how the program works:

    ---bestprojectever:
    * Program.cs: creates ASP.NET Core web application with Swagger and OpenAPI
    * Controllers: WordController.cs is reading words from a text file called wordle.txt (contains 5 letter words) and then stroring
    it in array and then it randomly picks a word from array that will display it later in the Wordle
    * wordle.txt contains 5 letter words in English language (i am not 100% sure if that is all the 5 letter words in English language)
    * words.json is my original attempt to use it instead of wordle.txt, but Chase (TA) and I could not figure it out why it did not work
    I left it there to work on it at a later time
    * Same with Models (Wordle.cs)
    * GetWords.cs was intended for words.json
    
    ---wordle:
    * Program.cs: it just calls the Game.cs class and its method Start (in Game.cs)
    * Game.cs: It calls the my API and extracts one word from it by putting it in the wordssss, then correctWord is equals to wordssss.
    then it goes to GetWords method and it takes us to Word.cs
    * GameHandler.cs: it just used to get and set the word from api in Game.cs
    * wordle.txt: it is exact same file as it is in bestprojectever, there reason for this is that i used it to verify userGuess guess to make sure
    it is a valid English word like real wordle, to prevent userGuess like "AAAAA" count as an actual guess. There reason i did not just extract all
    the words from API, is because i could not figure it out with out straight up copying the so;ution to my problem from internet. I tried to use List
    method but i still could not come up with a proper solution to ensure game play was good.
    * Word.cs: this is were user gets to interact with the program. I will comment all the important stuff in the class itself to make it easier to
    understand my comments
*/

/*
    How to play:

    * Please set your terminal background color to black
    * Open bestprojectever and wordle
    * Open a terminal and make sure it has bestprojectever as the directory, and then type this: dotnet watch run. It will take you to a link 
    in a search browser with you will need to click on an down arrow on the right side in blue box.
    * Click Try it out, then click Execute
    * Scroll down to a part that says Request URL, take a look at it and verify it if it matches the same URL as in wordle Game.cs line 15
    var wordssss = await client.GetStringAsync("http://localhost:5088/api/word"); <--- this
    * if it is different then chage the link you got in a web and swap it with the one in the code. If you do not do this, the game is unlikely to work
    * if everything matches or you swaped it with a proper link then
    *** DO NOT CLOSE OR STOP bestprojectever
    * Open a new terminal and make sure it has wordle as the directory, and then type this: dotnet run
    * And now the game began
    * So now you follow instructions in the game
    * In the first attempt you will be asked for a first guess
    * if first attempt is correct then you will get congrats (or something like that) and should be asked if you want to play again
    or exit the game (if this does not do, it means i forgot to change this comment and i could not fix a problem to restart the game properly)
    * If user get attempt one wrong they will move on to level two
    * All things are the same as level one (except user can never guess same word as previous guesses), level six is different
    * after user is done with level six not matter if they are correct or incorrect they will be asked if the want to play again or exit the game
    (if i fixed the problem)
*/

/*
    Real NewYork Times Wordle rules:

    As soon as you’ve submitted your guess, the game will color-code each letter in your guess to tell you how close it was to the letters in the hidden word.
    A gray or black square means that this letter does not appear in the secret word at all
    A yellow square means that this letter appears in the secret word, but it’s in the wrong spot within the word
    A green square means that this letter appears in the secret word, and it’s in exactly the right place
    Getting a green square or yellow square will get you closer to guessing the real secret word, since it means you’ve guessed a correct letter.
    For example, let’s say you guess “WRITE” and get two green squares on the W and the R, and gray squares for the I, T, and E. 
    Your next guess might be WRONG, WRACK, or WRUNG, since these words start with WR and don’t contain the letters I, T, or E.
    Alternatively, let’s say you guess “WRITE” and get two green squares on the T and the E, and gray squares for the W, R, and I. 
    In that case, your next guess might be BASTE, ELATE, or LATTE, since these words end with TE and don’t contain the letters W, R, or I.
    Remember that the same letter can appear multiple times in the secret word, and there’s no special color coding for letters that appear repeatedly. 
    For example, if the secret word is BELLE and you guess a word with one L and one E, Wordle won’t tell you that both those letters actually appear twice.
    You get a maximum of six tries to guess the secret word.

    Source: https://prowritingaid.com/what-is-wordle 
*/






