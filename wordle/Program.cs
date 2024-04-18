using System;
using System.Threading.Tasks;
using wordle;

namespace YourNamespace
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Game.Start();
        }
    }
}










// using System;
// using System.Threading.Tasks;
// using wordle;

// namespace YourNamespace
// {
//     class Program
//     {
//         static async Task Main(string[] args)
//         {
//             Game game = new Game();
//             await game.Start();
//         }
//     }
// }

