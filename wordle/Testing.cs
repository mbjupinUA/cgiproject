// namespace wordle
// {
//     public class Testing
//     {
//         // class Program
// // {
// //     static async Task Main(string[] args)
// //     {
// //         string apiUrl = "http://localhost:5088/api/word";

// //         try
// //         {
// //             using (HttpClient client = new HttpClient())
// //             {
// //                 HttpResponseMessage response = await client.GetAsync(apiUrl);

// //                 if (response.IsSuccessStatusCode)
// //                 {
// //                     string jsonResponse = await response.Content.ReadAsStringAsync();

// //                     // Log the raw response
// //                     Console.WriteLine("Raw Response: " + jsonResponse);

// //                     // Your code to handle the JSON response
// //                 }
// //                 else
// //                 {
// //                     Console.WriteLine("Failed to retrieve word. Status code: " + response.StatusCode);
// //                 }
// //             }
// //         }
// //         catch (Exception ex)
// //         {
// //             Console.WriteLine("Error: " + ex.Message);
// //         }
// //     }
// // }

// using System;
// using System.Net.Http;
// using System.Threading.Tasks;

// class Program
// {
//     static async Task Main(string[] args)
//     {
//         string apiUrl = "http://localhost:5088/api/word";
//         int numberOfWords = 10; // Number of words to retrieve

//         try
//         {
//             using (HttpClient client = new HttpClient())
//             {
//                 for (int j = 0; j < 10; j++) // Loop 10 times
//                 {
//                     Console.WriteLine($"Iteration {j + 1}:");

//                     for (int i = 0; i < numberOfWords; i++) // Loop to retrieve 10 words in each iteration
//                     {
//                         HttpResponseMessage response = await client.GetAsync(apiUrl);

//                         // Print response status code
//                         Console.WriteLine($"Response Status Code: {response.StatusCode}");

//                         if (response.IsSuccessStatusCode)
//                         {
//                             string word = await response.Content.ReadAsStringAsync();

//                             // Display the word
//                             Console.WriteLine($"Word {i + 1}: {word}");
//                         }
//                         else
//                         {
//                             Console.WriteLine($"Failed to retrieve word {i + 1}. Status code: {response.StatusCode}");
//                         }
//                     }
//                     Console.WriteLine();
//                 }
//             }
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine("Error: " + ex.Message);
//         }
//     }
// }





//     }
// }