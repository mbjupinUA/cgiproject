// // // using System;
// // // using System.Net.Http;
// // // using System.Threading.Tasks;

// // // namespace wordle
// // // {
// // //     public class APItest
// // //     {
// // //         string correctWord;

// // //         static async Task Main(string[] args)
// // //         {
// // //             var apiTest = new APItest();
// // //             await apiTest.RunAsync();
// // //         }

// // //         public async Task RunAsync()
// // //         {
// // //             Console.Clear();
// // //             string apiUrl = "http://localhost:5088/api/word";
// // //             try
// // //             {
// // //                 using (HttpClient client = new HttpClient())
// // //                 {
// // //                     HttpResponseMessage response = await client.GetAsync(apiUrl);

// // //                     if (response.IsSuccessStatusCode)
// // //                     {
// // //                         string jsonResponse = await response.Content.ReadAsStringAsync();

// // //                         // Log the raw response
// // //                         Console.WriteLine("Raw Response: " + jsonResponse);
// // //                         correctWord = jsonResponse;
// // //                         System.Console.WriteLine("Guess a 5 letter word in 6 attempts! Press ENTER to begin");
// // //                         Console.ReadLine();
// // //                         Console.Clear();
// // //                         AttemptOne();
// // //                     }
// // //                     else
// // //                     {
// // //                         Console.WriteLine("Failed to retrieve word. Status code: " + response.StatusCode);
// // //                     }
// // //                 }
// // //             }
// // //             catch (Exception ex)
// // //             {
// // //                 Console.WriteLine("Error: " + ex.Message);
// // //             }
// // //         }

// // //         public void AttemptOne()
// // //         {
// // //             Console.Clear();
// // //             System.Console.Write(@"
// // //         ••••••••••••••••••••••
// // //         •    •   •   •   •   •
// // //         ••••••••••••••••••••••");
// // //             System.Console.Write(@"
// // //         ••••••••••••••••••••••
// // //         •    •   •   •   •   •
// // //         ••••••••••••••••••••••");
// // //             System.Console.Write(@"
// // //         •••••••••••••••••••••
// // //                 •    •   •   •   •   •
// // //         ••••••••••••••••••••••");
// // //             System.Console.Write(@"
// // //         ••••••••••••••••••••••
// // //         •    •   •   •   •   •
// // //         ••••••••••••••••••••••");
// // //             System.Console.Write(@"
// // //         ••••••••••••••••••••••
// // //         •    •   •   •   •   •
// // //         •••••••••••••••••••••
// // //                 ••••••••••••••••••••••");
// // //             System.Console.Write(@"
// // //         •    •   •   •   •   •
// // //         ••••••••••••••••••••••");
// // //             System.Console.WriteLine(correctWord);
// // //             Console.ForegroundColor = ConsoleColor.DarkGreen;
// // //             System.Console.Write(@"
// // //         Attempt 1");
// // //             Console.ForegroundColor = ConsoleColor.DarkRed;
// // //             System.Console.Write($@"
// // //         Write Here: ");
// // //             Console.ResetColor();
// // //             Console.ReadLine();
// // //         }
// // //     }
// // // }



// // using System;
// // using System.Net.Http;
// // using System.Threading.Tasks;

// // namespace wordle
// // {
// //     public class APItest
// //     {
// //         string correctWord;

// //         static async Task Main(string[] args)
// //         {
// //             var apiTest = new APItest();
// //             await apiTest.RunAsync();
// //         }

// //         public async Task RunAsync()
// //         {
// //             bool continueLoop = true;

// //             while (continueLoop)
// //             {
// //                 Console.Clear();
// //                 string apiUrl = "http://localhost:5088/api/word";
// //                 try
// //                 {
// //                     using (HttpClient client = new HttpClient())
// //                     {
// //                         HttpResponseMessage response = await client.GetAsync(apiUrl);

// //                         if (response.IsSuccessStatusCode)
// //                         {
// //                             string jsonResponse = await response.Content.ReadAsStringAsync();

// //                             // Log the raw response
// //                             Console.WriteLine("Raw Response: " + jsonResponse);
// //                             correctWord = jsonResponse;
// //                             System.Console.WriteLine("Guess a 5 letter word in 6 attempts! Press ENTER to begin");
// //                             Console.ReadLine();
// //                             Console.Clear();
// //                             await AttemptOne();
// //                         }
// //                         else
// //                         {
// //                             Console.WriteLine("Failed to retrieve word. Status code: " + response.StatusCode);
// //                         }
// //                     }
// //                 }
// //                 catch (Exception ex)
// //                 {
// //                     Console.WriteLine("Error: " + ex.Message);
// //                 }

// //                 Console.WriteLine("Do you want to play again? (Y/N)");
// //                 string playAgain = Console.ReadLine();

// //                 if (playAgain?.Trim().ToUpper() != "Y")
// //                 {
// //                     continueLoop = false;
// //                 }
// //             }
// //         }

// //         public async Task AttemptOne()
// //         {
// //             Console.Clear();
// //             System.Console.Write(@"
// //         ••••••••••••••••••••••
// //         •    •   •   •   •   •
// //         ••••••••••••••••••••••");
// //             System.Console.Write(@"
// //         ••••••••••••••••••••••
// //         •    •   •   •   •   •
// //         ••••••••••••••••••••••");
// //             System.Console.Write(@"
// //         •••••••••••••••••••••
// //                 •    •   •   •   •   •
// //         ••••••••••••••••••••••");
// //             System.Console.Write(@"
// //         ••••••••••••••••••••••
// //         •    •   •   •   •   •
// //         ••••••••••••••••••••••");
// //             System.Console.WriteLine(correctWord);
// //             Console.ForegroundColor = ConsoleColor.DarkGreen;
// //             System.Console.Write(@"
// //         Attempt 1");
// //             Console.ForegroundColor = ConsoleColor.DarkRed;
// //             System.Console.Write($@"
// //         Write Here: ");
// //             Console.ResetColor();
// //             string userInput = Console.ReadLine();
            
// //             if (!string.IsNullOrEmpty(userInput))
// //             {
// //                 // If user input something, loop back to Task Main
// //                 await RunAsync();
// //             }
// //             else
// //             {
// //                 // Otherwise, loop back to AttemptOne
// //                 await AttemptOne();
// //             }
// //         }
// //     }
// // }






// // using System;
// // using System.Collections.Generic;
// // using System.Net.Http;
// // using System.Threading.Tasks;

// // namespace wordle
// // {
// //     public class APItest
// //     {
// //         static async Task Main(string[] args)
// //         {
// //             var apiTest = new APItest();
// //             await apiTest.RunAsync();
// //         }

// //         public async Task RunAsync()
// //         {
// //             List<string> words = await GetWordsFromApi();

// //             if (words == null || words.Count == 0)
// //             {
// //                 Console.WriteLine("Failed to retrieve words from the API.");
// //                 return;
// //             }

// //             bool continueLoop = true;

// //             while (continueLoop)
// //             {
// //                 string correctWord = GetRandomWord(words);
// //                 Console.Clear();
// //                 Console.WriteLine("Guess a 5 letter word in 6 attempts! Press ENTER to begin");
// //                 Console.ReadLine();
// //                 Console.Clear();
// //                 bool wordGuessed = AttemptOne(correctWord, words);

// //                 if (wordGuessed)
// //                 {
// //                     Console.WriteLine("Congratulations! You guessed the word!");
// //                     Console.WriteLine("Press ENTER to play again.");
// //                     Console.ReadLine();
// //                 }
// //                 else
// //                 {
// //                     Console.WriteLine("Sorry! You didn't guess the word.");
// //                     Console.WriteLine("Press ENTER to play again.");
// //                     Console.ReadLine();
// //                 }

// //                 // Loop back to RunAsync
// //                 await RunAsync();
// //             }
// //         }

// //         private async Task<List<string>> GetWordsFromApi()
// //         {
// //             List<string> words = new List<string>();

// //             string apiUrl = "http://localhost:5088/api/word";

// //             try
// //             {
// //                 using (HttpClient client = new HttpClient())
// //                 {
// //                     HttpResponseMessage response = await client.GetAsync(apiUrl);

// //                     if (response.IsSuccessStatusCode)
// //                     {
// //                         string jsonResponse = await response.Content.ReadAsStringAsync();
// //                         // Split the response into individual words
// //                         string[] wordArray = jsonResponse.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
// //                         words.AddRange(wordArray);
// //                         return words;
// //                     }
// //                     else
// //                     {
// //                         Console.WriteLine("Failed to retrieve words. Status code: " + response.StatusCode);
// //                         return null;
// //                     }
// //                 }
// //             }
// //             catch (Exception ex)
// //             {
// //                 Console.WriteLine("Error: " + ex.Message);
// //                 return null;
// //             }
// //         }

// //         private string GetRandomWord(List<string> words)
// //         {
// //             Random random = new Random();
// //             int index = random.Next(words.Count);
// //             return words[index];
// //         }

// //         public bool AttemptOne(string correctWord, List<string> words)
// //         {
// //             Console.Clear();
// //             System.Console.Write(@"
// //         ••••••••••••••••••••••
// //         •    •   •   •   •   •
// //         ••••••••••••••••••••••");
// //             System.Console.Write(@"
// //         ••••••••••••••••••••••
// //         •    •   •   •   •   •
// //         ••••••••••••••••••••••");
// //             System.Console.Write(@"
// //         •••••••••••••••••••••
// //                 •    •   •   •   •   •
// //         ••••••••••••••••••••••");
// //             System.Console.WriteLine(correctWord);
// //             Console.ForegroundColor = ConsoleColor.DarkGreen;
// //             System.Console.Write(@"
// //         Attempt 1");
// //             Console.ForegroundColor = ConsoleColor.DarkRed;
// //             System.Console.Write($@"
// //         Write Here: ");
// //             Console.ResetColor();
// //             string userInput = Console.ReadLine();
            
// //             if (!string.IsNullOrEmpty(userInput) && words.Contains(userInput.ToUpper()))
// //             {
// //                 return userInput.ToUpper() == correctWord.ToUpper();
// //             }
// //             else
// //             {
// //                 Console.WriteLine("Sorry! That's not the word.");
// //                 Console.WriteLine("Press ENTER to try again.");
// //                 Console.ReadLine();
// //                 return AttemptOne(correctWord, words);
// //             }
// //         }
// //     }
// // }



// // using System;
// // using System.Net.Http;
// // using System.Threading.Tasks;



// // namespace wordle
// // {
// //     public class Game
// //     {
// //         string correctWord;
// //         static async Task Main()
// //         {
// //             var gameAPI = new Game();
// //             await gameAPI.RunAsync();
// //         }

// //         public async Task RunAsync()
// //         {
// //             bool continueLoop = true;
// //             while (continueLoop)
// //             {
// //                 Console.Clear();
// //                 string apiUrl = "http://localhost:5088/api/word";
// //                 try
// //                 {
// //                     using (HttpClient client = new HttpClient())
// //                     {
// //                         HttpResponseMessage response = await client.GetAsync(apiUrl);
// //                         if (response.IsSuccessStatusCode)
// //                         {
// //                             string jsonResponse = await response.Content.ReadAsStringAsync();
// //                             System.Console.WriteLine("Raw Response:" + jsonResponse);
// //                             correctWord = jsonResponse;
// //                             System.Console.WriteLine("Guess a 5 letter word in 6 attempts! Press ENTER to begin");
// //                             Console.ReadLine();
// //                             Console.Clear();
// //                             await AttemptOne();
// //                         }
// //                         else
// //                         {
// //                             Console.WriteLine("Failed to retrieve word. Status code: " + response.StatusCode);
// //                         }
// //                     }
// //                 }
// //                 catch (Exception ex)
// //                 {
// //                     System.Console.WriteLine("Error: " + ex.Message);
// //                 }
// //                 Console.WriteLine("Do you want to play again? (Y/N)");
// //                 string playAgain = Console.ReadLine();
// //                 if (playAgain?.Trim().ToUpper() != "Y")
// //                 {
// //                     continueLoop = false;
// //                 }
// //             }
// //         }

// //         private async GetList()
// //         {
// //             List<string> words = await GetWordsFromApi();

// //             if (words == null || words.Count == 0)
// //             {
// //                 Console.WriteLine("Failed to retrieve words from the API.");
// //                 return;
// //             }
// //         }

// //         private async Task<List<string>> GetWordsFromApi()
// //         {
// //             List<string> words = new List<string>();

// //             string apiUrl = "http://localhost:5088/api/word";

// //             try
// //             {
// //                 using (HttpClient client = new HttpClient())
// //                 {
// //                     HttpResponseMessage response = await client.GetAsync(apiUrl);

// //                     if (response.IsSuccessStatusCode)
// //                     {
// //                         string jsonResponse = await response.Content.ReadAsStringAsync();
// //                         // Split the response into individual words
// //                         string[] wordArray = jsonResponse.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
// //                         words.AddRange(wordArray);
// //                         return words;
// //                     }
// //                     else
// //                     {
// //                         Console.WriteLine("Failed to retrieve words. Status code: " + response.StatusCode);
// //                         return null;
// //                     }
// //                 }
// //             }
// //             catch (Exception ex)
// //             {
// //                 Console.WriteLine("Error: " + ex.Message);
// //                 return null;
// //             }
// //         }

// //         public void AttemptOne(string correctWord, List<string> words)
// //         {
// //             Console.Clear();
// //             System.Console.Write(@"
// //         ••••••••••••••••••••••
// //         •    •   •   •   •   •
// //         ••••••••••••••••••••••");
// //             System.Console.Write(@"
// //         ••••••••••••••••••••••
// //         •    •   •   •   •   •
// //         ••••••••••••••••••••••");
// //             System.Console.Write(@"
// //         •••••••••••••••••••••
// //                 •    •   •   •   •   •
// //         ••••••••••••••••••••••");
// //             System.Console.WriteLine(correctWord);
// //             Console.ForegroundColor = ConsoleColor.DarkGreen;
// //             System.Console.Write(@"
// //         Attempt 1");
// //             Console.ForegroundColor = ConsoleColor.DarkRed;
// //             System.Console.Write($@"
// //         Write Here: ");
// //             Console.ResetColor();
// //             string userInput = Console.ReadLine();
// //             if(words.Any(word => word == userInput))
// //             {
// //                 if(userInput == correctWord)
// //                 {
// //                     System.Console.WriteLine("Good Job, tap enter");
// //                     Console.ReadLine();
// //                     await RunAsync();
// //                 }
// //                 else
// //                 {
// //                     await AttemptOne();
// //                 }
// //             }
// //             else
// //             {
// //                 await AttemptOne();
// //             }
// //         }
// //     }
// // }




// using System;
// using System.Collections.Generic;
// using System.Net.Http;
// using System.Linq;
// using System.Threading.Tasks;

// namespace wordle
// {
//     public class Game
//     {
//         string correctWord;
//         List<string> words = new List<string>();

//         public static async Task Main()
//         {
//             var gameAPI = new Game();
//             await gameAPI.RunAsync();
//         }

//         public async Task RunAsync()
//         {
//             bool continueLoop = true;
//             while (continueLoop)
//             {
//                 Console.Clear();
//                 string apiUrl = "http://localhost:5088/api/word";
//                 try
//                 {
//                     using (HttpClient client = new HttpClient())
//                     {
//                         HttpResponseMessage response = await client.GetAsync(apiUrl);
//                         if (response.IsSuccessStatusCode)
//                         {
//                             string jsonResponse = await response.Content.ReadAsStringAsync();
//                             System.Console.WriteLine("Raw Response:" + jsonResponse);
//                             correctWord = jsonResponse;
//                             System.Console.WriteLine("Guess a 5 letter word in 6 attempts! Press ENTER to begin");
//                             Console.ReadLine();
//                             Console.Clear();
//                             await GetList();
//                             await AttemptOne();
//                         }
//                         else
//                         {
//                             Console.WriteLine("Failed to retrieve word. Status code: " + response.StatusCode);
//                         }
//                     }
//                 }
//                 catch (Exception ex)
//                 {
//                     System.Console.WriteLine("Error: " + ex.Message);
//                 }
//                 Console.WriteLine("Do you want to play again? (Y/N)");
//                 string playAgain = Console.ReadLine();
//                 if (playAgain?.Trim().ToUpper() != "Y")
//                 {
//                     continueLoop = false;
//                 }
//             }
//         }

//         private async Task GetList()
//         {
//             words = await GetWordsFromApi();

//             if (words == null || words.Count == 0)
//             {
//                 Console.WriteLine("Failed to retrieve words from the API.");
//                 return;
//             }
//         }

//         private async Task<List<string>> GetWordsFromApi()
//         {
//             string apiUrl = "http://localhost:5088/api/word";

//             try
//             {
//                 using (HttpClient client = new HttpClient())
//                 {
//                     HttpResponseMessage response = await client.GetAsync(apiUrl);

//                     if (response.IsSuccessStatusCode)
//                     {
//                         string jsonResponse = await response.Content.ReadAsStringAsync();
//                         // Split the response into individual words
//                         string[] wordArray = jsonResponse.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
//                         return wordArray.ToList();
//                     }
//                     else
//                     {
//                         Console.WriteLine("Failed to retrieve words. Status code: " + response.StatusCode);
//                         return null;
//                     }
//                 }
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine("Error: " + ex.Message);
//                 return null;
//             }
//         }

//         public async Task AttemptOne(List<string> words)
//         {
//             Console.Clear();
//             System.Console.Write(@"
//     ••••••••••••••••••••••
//     •    •   •   •   •   •
//     ••••••••••••••••••••••");
//             System.Console.Write(@"
//     ••••••••••••••••••••••
//     •    •   •   •   •   •
//     ••••••••••••••••••••••");
//             System.Console.Write(@"
//     •••••••••••••••••••••
//                 •    •   •   •   •   •
//     ••••••••••••••••••••••
//             •    •   •   •   •   •
//     ••••••••••••••••••••••");
//             System.Console.WriteLine(correctWord);
//             Console.ForegroundColor = ConsoleColor.DarkGreen;
//             System.Console.Write(@"
//     Attempt 1");
//             Console.ForegroundColor = ConsoleColor.DarkRed;
//             System.Console.Write($@"
//     Write Here: ");
//             Console.ResetColor();
//             string userInput = Console.ReadLine().ToUpper();
//             if (words.Contains(userInput))
//             {
//                 if (userInput == correctWord)
//                 {
//                     System.Console.WriteLine("Good Job, tap enter");
//                     Console.ReadLine();
//                     await RunAsync();
//                 }
//                 else
//                 {
//                     // Word found but incorrect, loop back to AttemptOne
//                     System.Console.WriteLine("Incorrect guess. Press ENTER to try again.");
//                     Console.ReadLine();
//                     await AttemptOne(words);
//                 }
//             }
//             else
//             {
//                 // Word not found in API response, ask user to enter again
//                 System.Console.WriteLine("Word not found. Press ENTER to try again.");
//                 Console.ReadLine();
//                 await AttemptOne(words);
//             }
//         }

//     }
// }



