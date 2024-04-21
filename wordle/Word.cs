namespace wordle
{
    public class Word
    {
        const int MAX_WORDS = 6000;
        string[] colorWordOne = new string[5];
        string[] colorWordTwo = new string[5];
        string[] colorWordThree = new string[5];
        string[] colorWordFour = new string[5];
        string[] colorWordFive = new string[5];
        string[] colorWordSix = new string[5];
        string correctWord;

        public Word()
        {
            correctWord = " ";
        }
        public void Wordle(string correctWord)
        {
            this.correctWord = correctWord;
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
            words[randomIndex].ToUpper();
            BeganWordle(ref words, randomIndex);
        }

        public void BeganWordle(ref string[] words, int randomIndex)
        {
            Console.Clear();
            string correctWord = words[randomIndex];
            System.Console.WriteLine("Guess a 5 letter word in 6 attempts! Press ENTER to begin");
            Console.ReadLine();
            Console.Clear();
            AttemptOne(ref words, ref correctWord, ref randomIndex);
        }

        public void AttemptOne(ref string[] words, ref string correctWord, ref int randomIndex)
        {
            Console.Clear();
            System.Console.Write(@"
        •••••••••••••••••••••
        •   •   •   •   •   •
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••
        •   •   •   •   •   •
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••
        •   •   •   •   •   •
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••
        •   •   •   •   •   •
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••
        •   •   •   •   •   •
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••
        •   •   •   •   •   •
        •••••••••••••••••••••");
            System.Console.WriteLine(correctWord);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            System.Console.Write(@"
        Attempt 1");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            System.Console.Write($@"
        Write Here: "); Console.ResetColor();
            string userGuess = Console.ReadLine().ToUpper();
            if (userGuess.Length != 5)
            {
                AttemptOne(ref words, ref correctWord, ref randomIndex);
            }
            else
            {
                char[] chars = new char[10];
                chars = userGuess.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    char c = chars[i];
                    if (char.IsLetter(c))
                    {
                        for (int j = 0; j < words.Length; j++)
                        {
                            if (words.Any(word => word == userGuess))
                            {
                                if (userGuess.ToUpper() == correctWord)
                                {
                                    Console.Clear();
                                    Check(ref correctWord, ref userGuess);
                                    DisplayAttemptOneCorrect(ref userGuess, ref randomIndex);
                                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                                    System.Console.Write(@"
        Wow! You got it in 1 try! Tap ENTER to continue the game  ");
                                    Console.ReadLine();
                                    Console.Clear();
                                    Console.ResetColor();
                                    // PlayAgain();
                                }
                                else
                                {
                                    Console.Clear();
                                    Check(ref correctWord, ref userGuess);
                                    DisplayAttemptOne(ref correctWord, ref userGuess, ref words, ref randomIndex);
                                }
                            }
                            else
                            {
                                AttemptOne(ref words, ref correctWord, ref randomIndex);
                            }
                        }
                    }
                    else
                    {
                        AttemptOne(ref words, ref correctWord, ref randomIndex);
                    }
                }
            }
        }

        private void Check(ref string correctWord, ref string userGuess)
        {
            // Dictionary to store letter counts in the answer word
            Dictionary<char, int> answerCharCounts = new Dictionary<char, int>();
            foreach (char letter in correctWord)
            {
                if (answerCharCounts.ContainsKey(letter))
                {
                    answerCharCounts[letter]++;
                }
                else
                {
                    answerCharCounts[letter] = 1;
                }
            }

            for (int i = 0; i < correctWord.Length; i++)
            {
                char guessLetter = userGuess[i];
                char answerLetter = correctWord[i];

                // Handle correct position (Green)
                if (guessLetter == answerLetter)
                {
                    colorWordOne[i] = "G";
                    answerCharCounts[guessLetter]--;
                }
                // Handle correct letter in wrong position (Yellow) with repeated letters
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 0)
                {
                    // Check if there's already a green for this letter (meaning it's in the correct position)
                    bool hasGreen = false;
                    for (int index = 0; index < colorWordOne.Length; index++)
                    {
                        if (colorWordOne[index] == "G" && index != i && userGuess[index] == guessLetter)
                        {
                            hasGreen = true;
                            break;
                        }
                    }
                    if (hasGreen)
                    {
                        colorWordOne[i] = "Y"; // Second occurrence becomes yellow
                    }
                    else
                    {
                        colorWordOne[i] = "Y"; // First occurrence becomes yellow
                    }
                    answerCharCounts[guessLetter]--;
                }
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 1)
                {
                    // Check if there's already a green for this letter (meaning it's in the correct position)
                    bool hasGreen = false;
                    bool hasSecondGreen = false;
                    for (int index = 0; index < colorWordOne.Length; index++)
                    {
                        if (colorWordOne[index] == "G" && index != i && userGuess[index] == guessLetter)
                        {
                            if (!hasGreen)
                            {
                                hasGreen = true;
                            }
                            else if (!hasSecondGreen)
                            {
                                hasSecondGreen = true;
                            }
                            else
                            {
                                colorWordOne[i] = "Y"; // Third occurrence becomes yellow
                                return; // Exit the loop and function
                            }
                        }
                    }
                    if (hasSecondGreen)
                    {
                        colorWordOne[i] = "Y"; // Second occurrence becomes yellow
                    }
                    else if (hasGreen)
                    {
                        colorWordOne[i] = "G"; // First occurrence becomes green
                    }
                }
                else
                {
                    colorWordOne[i] = "W"; // White (gray) for not in the word
                }
            }
        }
        private ConsoleColor GetColorLetter(string colorWordOneChar)
        {
            if (colorWordOneChar == "G")
            {
                return ConsoleColor.Green;
            }
            else if (colorWordOneChar == "Y")
            {
                return ConsoleColor.DarkYellow;
            }
            else
            {
                return ConsoleColor.White;
            }
        }

        public void DisplayAttemptOneCorrect(ref string userGuess, ref int randomIndex)
        {
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        •••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 2);
            for (int i = 0; i < userGuess.Length; i++)
            {
                Console.ForegroundColor = GetColorLetter(colorWordOne[i]);
                Console.Write(userGuess[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ResetColor();
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
        }

        public void DisplayAttemptOne(ref string correctWord, ref string userGuess, ref string[] words, ref int randomIndex)
        {
            Console.Clear();
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        •••••••••••••••••••••               
        ");

            Console.SetCursorPosition(10, 2);
            for (int i = 0; i < userGuess.Length; i++)
            {
                Console.ForegroundColor = GetColorLetter(colorWordOne[i]);
                Console.Write(userGuess[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ResetColor();
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            AttemptTwo(ref correctWord, ref userGuess, ref words, ref randomIndex);
        }

        public void AttemptTwo(ref string correctWord, ref string userGuess, ref string[] words, ref int randomIndex)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            System.Console.Write(@"
        Attempt 2");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            System.Console.Write($@"
        Write Here: "); Console.ResetColor();
            string userGuessTwo = Console.ReadLine().ToUpper();
            if (userGuessTwo.Length != 5)
            {
                DisplayAttemptOne(ref correctWord, ref userGuess, ref words, ref randomIndex);
                AttemptTwo(ref correctWord, ref userGuess, ref words, ref randomIndex);
            }
            else
            {
                char[] chars = new char[10];
                chars = userGuessTwo.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    char c = chars[i];
                    if (char.IsLetter(c))
                    {
                        for (int j = 0; j < words.Length; j++)
                        {
                            if (words.Any(word => word == userGuessTwo))
                            {
                                if (userGuessTwo == userGuess)
                                {
                                    DisplayAttemptOne(ref correctWord, ref userGuess, ref words, ref randomIndex);
                                    AttemptTwo(ref correctWord, ref userGuess, ref words, ref randomIndex);
                                }
                                else
                                {
                                    if (userGuessTwo.ToUpper() == correctWord)
                                    {
                                        Console.Clear();
                                        DisplayAttemptTwoCorrect(ref correctWord, ref userGuess, ref userGuessTwo, ref randomIndex);
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                        System.Console.Write(@"
        Wow! You got it in 2 try's! Tap ENTER to exit the game  ");
                                        Console.ReadLine();
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.Write("Would you like to play again? (1. Yes\t2. No (Exit))");
                                        Console.ResetColor();
                                        string userInput = Console.ReadLine();
                                        while (userInput != "2")
                                        {
                                            if (userInput == "1")
                                            {
                                                Console.Clear();
                                                Game.Start();
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                Console.WriteLine("Good Bye");
                                                Environment.Exit(0);
                                            }
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.Write("Would you like to play again? (1. Yes\t2. No (Exit))");
                                            Console.ResetColor();
                                            userInput = Console.ReadLine();
                                        }
                                        if (userInput == "2")
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.WriteLine("Good Bye");
                                            Environment.Exit(0);
                                        }
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        DisplayAttemptTwo(ref correctWord, ref words, ref userGuess, ref userGuessTwo, ref randomIndex);
                                    }
                                }
                            }
                            else
                            {
                                DisplayAttemptOne(ref correctWord, ref userGuess, ref words, ref randomIndex);
                                AttemptTwo(ref correctWord, ref userGuess, ref words, ref randomIndex);
                            }
                        }
                    }
                    else
                    {
                        DisplayAttemptOne(ref correctWord, ref userGuess, ref words, ref randomIndex);
                        AttemptTwo(ref correctWord, ref userGuess, ref words, ref randomIndex);
                    }
                }
            }
        }

        public void CheckTwo(ref string correctWord, ref string userGuessTwo)
        {
            Dictionary<char, int> answerCharCounts = new Dictionary<char, int>();
            foreach (char letter in correctWord)
            {
                if (answerCharCounts.ContainsKey(letter))
                {
                    answerCharCounts[letter]++;
                }
                else
                {
                    answerCharCounts[letter] = 1;
                }
            }

            for (int i = 0; i < correctWord.Length; i++)
            {
                char guessLetter = userGuessTwo[i];
                char answerLetter = correctWord[i];

                // Handle correct position (Green)
                if (guessLetter == answerLetter)
                {
                    colorWordTwo[i] = "G";
                    answerCharCounts[guessLetter]--;
                }
                // Handle correct letter in wrong position (Yellow) with repeated letters
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 0)
                {
                    // Check if there's already a green for this letter (meaning it's in the correct position)
                    bool hasGreen = false;
                    for (int index = 0; index < colorWordTwo.Length; index++)
                    {
                        if (colorWordTwo[index] == "G" && index != i && userGuessTwo[index] == guessLetter)
                        {
                            hasGreen = true;
                            break;
                        }
                    }

                    if (hasGreen)
                    {
                        colorWordTwo[i] = "Y"; // Second occurrence becomes yellow
                    }
                    else
                    {
                        colorWordTwo[i] = "Y"; // First occurrence becomes yellow
                    }


                    answerCharCounts[guessLetter]--;
                }
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 1)
                {
                    // Check if there's already a green for this letter (meaning it's in the correct position)
                    bool hasGreen = false;
                    bool hasSecondGreen = false;
                    for (int index = 0; index < colorWordTwo.Length; index++)
                    {
                        if (colorWordTwo[index] == "G" && index != i && userGuessTwo[index] == guessLetter)
                        {
                            if (!hasGreen)
                            {
                                hasGreen = true;
                            }
                            else if (!hasSecondGreen)
                            {
                                hasSecondGreen = true;
                            }
                            else
                            {
                                colorWordTwo[i] = "Y"; // Third occurrence becomes yellow
                                return; // Exit the loop and function
                            }
                        }
                    }

                    if (hasSecondGreen)
                    {
                        colorWordTwo[i] = "Y"; // Second occurrence becomes yellow
                    }
                    else if (hasGreen)
                    {
                        colorWordTwo[i] = "G"; // First occurrence becomes green
                    }

                }
                else
                {
                    colorWordTwo[i] = "W"; // White (gray) for not in the word
                }
            }
        }

        private ConsoleColor GetColorLetterTwo(ref string colorWordTwoChar)
        {
            if (colorWordTwoChar == "G")
            {
                return ConsoleColor.Green;
            }
            else if (colorWordTwoChar == "Y")
            {
                return ConsoleColor.DarkYellow;
            }
            else
            {
                return ConsoleColor.White;
            }
        }

        public void DisplayAttemptTwoCorrect(ref string correctWord, ref string userGuess, ref string userGuessTwo, ref int randomIndex)
        {
            Console.Clear();
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 2);
            for (int i = 0; i < userGuess.Length; i++)
            {
                Check(ref correctWord, ref userGuess);
                Console.ForegroundColor = GetColorLetter(colorWordOne[i]);
                Console.Write(userGuess[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        •••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 4);
            for (int i = 0; i < userGuessTwo.Length; i++)
            {
                CheckTwo(ref correctWord, ref userGuessTwo);
                Console.ForegroundColor = GetColorLetterTwo(ref colorWordTwo[i]);
                Console.Write(userGuessTwo[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
        }

        public void DisplayAttemptTwo(ref string correctWord, ref string[] words, ref string userGuess, ref string userGuessTwo, ref int randomIndex)
        {
            Console.Clear();
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        •••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 2);
            for (int i = 0; i < userGuess.Length; i++)
            {
                Check(ref correctWord, ref userGuess);
                Console.ForegroundColor = GetColorLetter(colorWordOne[i]);
                Console.Write(userGuess[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        •••••••••••••••••••••               
        ");
            // CheckTwo(ref colorWordTwo, ref words, ref correctWord, ref userGuessTwo);
            Console.SetCursorPosition(10, 4);
            for (int i = 0; i < userGuessTwo.Length; i++)
            {
                CheckTwo(ref correctWord, ref userGuessTwo);
                Console.ForegroundColor = GetColorLetterTwo(ref colorWordTwo[i]);
                Console.Write(userGuessTwo[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            AttemptThree(ref correctWord, ref words, ref userGuess, ref userGuessTwo, ref randomIndex);
        }

        public void AttemptThree(ref string correctWord, ref string[] words, ref string userGuess, ref string userGuessTwo, ref int randomIndex)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            System.Console.Write(@"
        Attempt 3");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            System.Console.Write($@"
        Write Here: "); Console.ResetColor();
            string userGuessThree = Console.ReadLine().ToUpper();
            if (userGuessThree.Length != 5)
            {
                DisplayAttemptTwo(ref correctWord, ref words, ref userGuess, ref userGuessTwo, ref randomIndex);
                AttemptThree(ref correctWord, ref words, ref userGuess, ref userGuessTwo, ref randomIndex);
            }
            else
            {
                char[] chars = new char[10];
                chars = userGuessThree.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    char c = chars[i];
                    if (char.IsLetter(c))
                    {
                        for (int j = 0; j < words.Length; j++)
                        {
                            if (words.Any(word => word == userGuessThree))
                            {
                                if (userGuessThree == userGuess || userGuessThree == userGuessTwo)
                                {
                                    DisplayAttemptTwo(ref correctWord, ref words, ref userGuess, ref userGuessTwo, ref randomIndex);
                                    AttemptThree(ref correctWord, ref words, ref userGuess, ref userGuessTwo, ref randomIndex);
                                }
                                else
                                {
                                    if (userGuessThree.ToUpper() == correctWord)
                                    {
                                        Console.Clear();
                                        DisplayAttemptThreeCorrect(ref correctWord, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref randomIndex);
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                        System.Console.Write(@"
        Wow! You got it in 3 try's! Tap ENTER to exit the game  ");
                                        Console.ReadLine();
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.Write("Would you like to play again? (1. Yes\t2. No (Exit))");
                                        Console.ResetColor();
                                        string userInput = Console.ReadLine();
                                        while (userInput != "2")
                                        {
                                            if (userInput == "1")
                                            {
                                                Console.Clear();
                                                Game.Start();
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                Console.WriteLine("Good Bye");
                                                Environment.Exit(0);
                                            }
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.Write("Would you like to play again? (1. Yes\t2. No (Exit))");
                                            Console.ResetColor();
                                            userInput = Console.ReadLine();
                                        }
                                        if (userInput == "2")
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.WriteLine("Good Bye");
                                            Environment.Exit(0);
                                        }
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        DisplayAttemptThree(ref correctWord, ref words,
        ref userGuess, ref userGuessTwo, ref userGuessThree, ref randomIndex);
                                    }
                                }
                            }
                            else
                            {
                                DisplayAttemptTwo(ref correctWord, ref words, ref userGuess, ref userGuessTwo, ref randomIndex);
                                AttemptThree(ref correctWord, ref words, ref userGuess, ref userGuessTwo, ref randomIndex);
                            }
                        }
                    }
                    else
                    {
                        DisplayAttemptTwo(ref correctWord, ref words, ref userGuess, ref userGuessTwo, ref randomIndex);
                        AttemptThree(ref correctWord, ref words, ref userGuess, ref userGuessTwo, ref randomIndex);
                    }
                }
            }
        }

        public void CheckThree(ref string correctWord, ref string userGuessThree)
        {
            Dictionary<char, int> answerCharCounts = new Dictionary<char, int>();
            foreach (char letter in correctWord)
            {
                if (answerCharCounts.ContainsKey(letter))
                {
                    answerCharCounts[letter]++;
                }
                else
                {
                    answerCharCounts[letter] = 1;
                }
            }

            for (int i = 0; i < correctWord.Length; i++)
            {
                char guessLetter = userGuessThree[i];
                char answerLetter = correctWord[i];

                // Handle correct position (Green)
                if (guessLetter == answerLetter)
                {
                    colorWordThree[i] = "G";
                    answerCharCounts[guessLetter]--;
                }
                // Handle correct letter in wrong position (Yellow) with repeated letters
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 0)
                {
                    // Check if there's already a green for this letter (meaning it's in the correct position)
                    bool hasGreen = false;
                    for (int index = 0; index < colorWordThree.Length; index++)
                    {
                        if (colorWordThree[index] == "G" && index != i && userGuessThree[index] == guessLetter)
                        {
                            hasGreen = true;
                            break;
                        }
                    }

                    if (hasGreen)
                    {
                        colorWordThree[i] = "Y"; // Second occurrence becomes yellow
                    }
                    else
                    {
                        colorWordThree[i] = "Y"; // First occurrence becomes yellow
                    }


                    answerCharCounts[guessLetter]--;
                }
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 1)
                {
                    // Check if there's already a green for this letter (meaning it's in the correct position)
                    bool hasGreen = false;
                    bool hasSecondGreen = false;
                    for (int index = 0; index < colorWordThree.Length; index++)
                    {
                        if (colorWordThree[index] == "G" && index != i && userGuessThree[index] == guessLetter)
                        {
                            if (!hasGreen)
                            {
                                hasGreen = true;
                            }
                            else if (!hasSecondGreen)
                            {
                                hasSecondGreen = true;
                            }
                            else
                            {
                                colorWordThree[i] = "Y"; // Third occurrence becomes yellow
                                return; // Exit the loop and function
                            }
                        }
                    }

                    if (hasSecondGreen)
                    {
                        colorWordThree[i] = "Y"; // Second occurrence becomes yellow
                    }
                    else if (hasGreen)
                    {
                        colorWordThree[i] = "G"; // First occurrence becomes green
                    }

                }
                else
                {
                    colorWordThree[i] = "W"; // White (gray) for not in the word
                }
            }
        }

        private ConsoleColor GetColorLetterThree(ref string colorWordThreeChar)
        {
            if (colorWordThreeChar == "G")
            {
                return ConsoleColor.Green;
            }
            else if (colorWordThreeChar == "Y")
            {
                return ConsoleColor.DarkYellow;
            }
            else
            {
                return ConsoleColor.White;
            }
        }

        public void DisplayAttemptThreeCorrect(ref string correctWord, ref string userGuess,
        ref string userGuessTwo, ref string userGuessThree, ref int randomIndex)
        {
            Console.Clear();
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 2);
            for (int i = 0; i < userGuess.Length; i++)
            {
                Check(ref correctWord, ref userGuess);
                Console.ForegroundColor = GetColorLetter(colorWordOne[i]);
                Console.Write(userGuess[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        •••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 4);
            for (int i = 0; i < userGuessTwo.Length; i++)
            {
                CheckTwo(ref correctWord, ref userGuessTwo);
                Console.ForegroundColor = GetColorLetterTwo(ref colorWordTwo[i]);
                Console.Write(userGuessTwo[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"                        
        •••••••••••••••••••••                         
        •                        
        •••••••••••••••••••••");
            Console.SetCursorPosition(10, 6);
            for (int i = 0; i < userGuessThree.Length; i++)
            {
                CheckThree(ref correctWord, ref userGuessThree);
                Console.ForegroundColor = GetColorLetterThree(ref colorWordThree[i]);
                Console.Write(userGuessThree[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"
        ••••••••••••••••••••• 
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
        }

        public void DisplayAttemptThree(ref string correctWord, ref string[] words,
        ref string userGuess, ref string userGuessTwo, ref string userGuessThree, ref int randomIndex)
        {
            Console.Clear();
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 2);
            for (int i = 0; i < userGuess.Length; i++)
            {
                Check(ref correctWord, ref userGuess);
                Console.ForegroundColor = GetColorLetter(colorWordOne[i]);
                Console.Write(userGuess[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        •••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 4);
            for (int i = 0; i < userGuessTwo.Length; i++)
            {
                CheckTwo(ref correctWord, ref userGuessTwo);
                Console.ForegroundColor = GetColorLetterTwo(ref colorWordTwo[i]);
                Console.Write(userGuessTwo[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"                        
        •••••••••••••••••••••                         
        •                        
        •••••••••••••••••••••");
            Console.SetCursorPosition(10, 6);
            for (int i = 0; i < userGuessThree.Length; i++)
            {
                CheckThree(ref correctWord, ref userGuessThree);
                Console.ForegroundColor = GetColorLetterThree(ref colorWordThree[i]);
                Console.Write(userGuessThree[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"
        ••••••••••••••••••••• 
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            AttemptFour(ref correctWord, ref words,
        ref userGuess, ref userGuessTwo, ref userGuessThree, ref randomIndex);
        }

        public void AttemptFour(ref string correctWord, ref string[] words,
        ref string userGuess, ref string userGuessTwo, ref string userGuessThree, ref int randomIndex)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            System.Console.Write(@"
        Attempt 4");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            System.Console.Write($@"
        Write Here: "); Console.ResetColor();
            string userGuessFour = Console.ReadLine().ToUpper();
            if (userGuessFour.Length != 5)
            {
                DisplayAttemptThree(ref correctWord, ref words, ref userGuess, ref userGuessTwo, ref userGuessThree, ref randomIndex);
                AttemptFour(ref correctWord, ref words,
        ref userGuess, ref userGuessTwo, ref userGuessThree, ref randomIndex);
            }
            else
            {
                char[] chars = new char[10];
                chars = userGuessFour.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    char c = chars[i];
                    if (char.IsLetter(c))
                    {
                        for (int j = 0; j < words.Length; j++)
                        {
                            if (words.Any(word => word == userGuessFour))
                            {
                                if (userGuessFour == userGuess || userGuessFour == userGuessTwo || userGuessFour == userGuessThree)
                                {
                                    DisplayAttemptThree(ref correctWord, ref words, ref userGuess, ref userGuessTwo, ref userGuessThree, ref randomIndex);
                                    AttemptFour(ref correctWord, ref words,
                                    ref userGuess, ref userGuessTwo, ref userGuessThree, ref randomIndex);
                                }
                                else
                                {
                                    if (userGuessFour.ToUpper() == correctWord)
                                    {
                                        Console.Clear();
                                        DisplayAttemptFourCorrect(ref correctWord, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref randomIndex);
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                        System.Console.Write(@"
        Wow! You got it in 3 try's! Tap ENTER to exit the game  ");
                                        Console.ReadLine();
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.Write("Would you like to play again? (1. Yes\t2. No (Exit))");
                                        Console.ResetColor();
                                        string userInput = Console.ReadLine();
                                        while (userInput != "2")
                                        {
                                            if (userInput == "1")
                                            {
                                                Console.Clear();
                                                Game.Start();
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                Console.WriteLine("Good Bye");
                                                Environment.Exit(0);
                                            }
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.Write("Would you like to play again? (1. Yes\t2. No (Exit))");
                                            Console.ResetColor();
                                            userInput = Console.ReadLine();
                                        }
                                        if (userInput == "2")
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.WriteLine("Good Bye");
                                            Environment.Exit(0);
                                        }
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        DisplayAttemptFour(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref randomIndex);
                                    }
                                }
                            }
                            else
                            {
                                DisplayAttemptThree(ref correctWord, ref words, ref userGuess, ref userGuessTwo, ref userGuessThree, ref randomIndex);
                                AttemptFour(ref correctWord, ref words,
                                ref userGuess, ref userGuessTwo, ref userGuessThree, ref randomIndex);
                            }
                        }
                    }
                    else
                    {
                        DisplayAttemptThree(ref correctWord, ref words, ref userGuess, ref userGuessTwo, ref userGuessThree, ref randomIndex);
                        AttemptFour(ref correctWord, ref words,
                        ref userGuess, ref userGuessTwo, ref userGuessThree, ref randomIndex);
                    }
                }
            }
        }

        public void CheckFour(ref string correctWord, ref string userGuessFour)
        {
            Dictionary<char, int> answerCharCounts = new Dictionary<char, int>();
            foreach (char letter in correctWord)
            {
                if (answerCharCounts.ContainsKey(letter))
                {
                    answerCharCounts[letter]++;
                }
                else
                {
                    answerCharCounts[letter] = 1;
                }
            }

            for (int i = 0; i < correctWord.Length; i++)
            {
                char guessLetter = userGuessFour[i];
                char answerLetter = correctWord[i];
                // Handle correct position (Green)
                if (guessLetter == answerLetter)
                {
                    colorWordFour[i] = "G";
                    answerCharCounts[guessLetter]--;
                }
                // Handle correct letter in wrong position (Yellow) with repeated letters
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 0)
                {
                    // Check if there's already a green for this letter (meaning it's in the correct position)
                    bool hasGreen = false;
                    for (int index = 0; index < colorWordFour.Length; index++)
                    {
                        if (colorWordFour[index] == "G" && index != i && userGuessFour[index] == guessLetter)
                        {
                            hasGreen = true;
                            break;
                        }
                    }
                    if (hasGreen)
                    {
                        colorWordFour[i] = "Y"; // Second occurrence becomes yellow
                    }
                    else
                    {
                        colorWordFour[i] = "Y"; // First occurrence becomes yellow
                    }
                    answerCharCounts[guessLetter]--;
                }
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 1)
                {
                    // Check if there's already a green for this letter (meaning it's in the correct position)
                    bool hasGreen = false;
                    bool hasSecondGreen = false;
                    for (int index = 0; index < colorWordFour.Length; index++)
                    {
                        if (colorWordFour[index] == "G" && index != i && userGuessFour[index] == guessLetter)
                        {
                            if (!hasGreen)
                            {
                                hasGreen = true;
                            }
                            else if (!hasSecondGreen)
                            {
                                hasSecondGreen = true;
                            }
                            else
                            {
                                colorWordFour[i] = "Y"; // Third occurrence becomes yellow
                                return; // Exit the loop and function
                            }
                        }
                    }
                    if (hasSecondGreen)
                    {
                        colorWordFour[i] = "Y"; // Second occurrence becomes yellow
                    }
                    else if (hasGreen)
                    {
                        colorWordFour[i] = "G"; // First occurrence becomes green
                    }
                }
                else
                {
                    colorWordFour[i] = "W"; // White (gray) for not in the word
                }
            }
        }

        private ConsoleColor GetColorLetterFour(ref string colorWordFourChar)
        {
            if (colorWordFourChar == "G")
            {
                return ConsoleColor.Green;
            }
            else if (colorWordFourChar == "Y")
            {
                return ConsoleColor.DarkYellow;
            }
            else
            {
                return ConsoleColor.White;
            }
        }

        public void DisplayAttemptFourCorrect(ref string correctWord, ref string userGuess,
        ref string userGuessTwo, ref string userGuessThree, ref string userGuessFour, ref int randomIndex)
        {
            Console.Clear();
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 2);
            for (int i = 0; i < userGuess.Length; i++)
            {
                Check(ref correctWord, ref userGuess);
                Console.ForegroundColor = GetColorLetter(colorWordOne[i]);
                Console.Write(userGuess[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        •••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 4);
            for (int i = 0; i < userGuessTwo.Length; i++)
            {
                CheckTwo(ref correctWord, ref userGuessTwo);
                Console.ForegroundColor = GetColorLetterTwo(ref colorWordTwo[i]);
                Console.Write(userGuessTwo[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"                        
        •••••••••••••••••••••                         
        •                        
        •••••••••••••••••••••");
            Console.SetCursorPosition(10, 6);
            for (int i = 0; i < userGuessThree.Length; i++)
            {
                CheckThree(ref correctWord, ref userGuessThree);
                Console.ForegroundColor = GetColorLetterThree(ref colorWordThree[i]);
                Console.Write(userGuessThree[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"                        
        •••••••••••••••••••••                         
        •                        
        •••••••••••••••••••••");
            Console.SetCursorPosition(10, 8);
            for (int i = 0; i < userGuessFour.Length; i++)
            {
                CheckFour(ref correctWord, ref userGuessFour);
                Console.ForegroundColor = GetColorLetterFour(ref colorWordFour[i]);
                Console.Write(userGuessFour[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"
        •••••••••••••••••••••
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
        }

        public void DisplayAttemptFour(ref string correctWord, ref string[] words, ref string userGuess,
        ref string userGuessTwo, ref string userGuessThree, ref string userGuessFour, ref int randomIndex)
        {
            Console.Clear();
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 2);
            for (int i = 0; i < userGuess.Length; i++)
            {
                Check(ref correctWord, ref userGuess);
                Console.ForegroundColor = GetColorLetter(colorWordOne[i]);
                Console.Write(userGuess[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        •••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 4);
            for (int i = 0; i < userGuessTwo.Length; i++)
            {
                CheckTwo(ref correctWord, ref userGuessTwo);
                Console.ForegroundColor = GetColorLetterTwo(ref colorWordTwo[i]);
                Console.Write(userGuessTwo[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"                        
        •••••••••••••••••••••                         
        •                        
        •••••••••••••••••••••");
            Console.SetCursorPosition(10, 6);
            for (int i = 0; i < userGuessThree.Length; i++)
            {
                CheckThree(ref correctWord, ref userGuessThree);
                Console.ForegroundColor = GetColorLetterThree(ref colorWordThree[i]);
                Console.Write(userGuessThree[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"                        
        •••••••••••••••••••••                         
        •                        
        •••••••••••••••••••••");
            Console.SetCursorPosition(10, 8);
            for (int i = 0; i < userGuessFour.Length; i++)
            {
                CheckFour(ref correctWord, ref userGuessFour);
                Console.ForegroundColor = GetColorLetterFour(ref colorWordFour[i]);
                Console.Write(userGuessFour[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"
        •••••••••••••••••••••
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            System.Console.Write(@"
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            AttemptFive(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref randomIndex);
        }

        public void AttemptFive(ref string correctWord, ref string[] words, ref string userGuess,
        ref string userGuessTwo, ref string userGuessThree, ref string userGuessFour, ref int randomIndex)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            System.Console.Write(@"
        Attempt 5");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            System.Console.Write($@"
        Write Here: "); Console.ResetColor();
            string userGuessFive = Console.ReadLine().ToUpper();
            if (userGuessFive.Length != 5)
            {
                DisplayAttemptFour(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref randomIndex);
                AttemptFive(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref randomIndex);
            }
            else
            {
                char[] chars = new char[10];
                chars = userGuessFive.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    char c = chars[i];
                    if (char.IsLetter(c))
                    {
                        for (int j = 0; j < words.Length; j++)
                        {
                            if (words.Any(word => word == userGuessFive))
                            {
                                if (userGuessFive == userGuess || userGuessFive == userGuessTwo || userGuessFive == userGuessThree
                                || userGuessFive == userGuessFour)
                                {
                                    DisplayAttemptFour(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref randomIndex);
                                    AttemptFive(ref correctWord, ref words, ref userGuess,
                            ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref randomIndex);
                                }
                                else
                                {
                                    if (userGuessFive.ToUpper() == correctWord)
                                    {
                                        Console.Clear();
                                        DisplayAttemptFiveCorrect(ref correctWord, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive, ref randomIndex);
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                        System.Console.Write(@"
        Wow! You got it in 5 try's! Tap ENTER to continue the game  ");
                                        Console.ReadLine();
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.Write("Would you like to play again? (1. Yes\t2. No (Exit))");
                                        Console.ResetColor();
                                        string userInput = Console.ReadLine();
                                        while (userInput != "2")
                                        {
                                            if (userInput == "1")
                                            {
                                                Console.Clear();
                                                Game.Start();
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                Console.WriteLine("Good Bye");
                                                Environment.Exit(0);
                                            }
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.Write("Would you like to play again? (1. Yes\t2. No (Exit))");
                                            Console.ResetColor();
                                            userInput = Console.ReadLine();
                                        }
                                        if (userInput == "2")
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.WriteLine("Good Bye");
                                            Environment.Exit(0);
                                        }
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        DisplayAttemptFive(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive, ref randomIndex);
                                    }
                                }
                            }
                            else
                            {
                                DisplayAttemptFour(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref randomIndex);
                                AttemptFive(ref correctWord, ref words, ref userGuess,
                        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref randomIndex);
                            }
                        }
                    }
                    else
                    {
                        DisplayAttemptFour(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref randomIndex);
                        AttemptFive(ref correctWord, ref words, ref userGuess,
                ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref randomIndex);
                    }
                }
            }
        }

        public void CheckFive(ref string correctWord, ref string userGuessFive)
        {
            Dictionary<char, int> answerCharCounts = new Dictionary<char, int>();
            foreach (char letter in correctWord)
            {
                if (answerCharCounts.ContainsKey(letter))
                {
                    answerCharCounts[letter]++;
                }
                else
                {
                    answerCharCounts[letter] = 1;
                }
            }

            for (int i = 0; i < correctWord.Length; i++)
            {
                char guessLetter = userGuessFive[i];
                char answerLetter = correctWord[i];
                // Handle correct position (Green)
                if (guessLetter == answerLetter)
                {
                    colorWordFive[i] = "G";
                    answerCharCounts[guessLetter]--;
                }
                // Handle correct letter in wrong position (Yellow) with repeated letters
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 0)
                {
                    // Check if there's already a green for this letter (meaning it's in the correct position)
                    bool hasGreen = false;
                    for (int index = 0; index < colorWordFive.Length; index++)
                    {
                        if (colorWordFive[index] == "G" && index != i && userGuessFive[index] == guessLetter)
                        {
                            hasGreen = true;
                            break;
                        }
                    }
                    if (hasGreen)
                    {
                        colorWordFive[i] = "Y"; // Second occurrence becomes yellow
                    }
                    else
                    {
                        colorWordFive[i] = "Y"; // First occurrence becomes yellow
                    }
                    answerCharCounts[guessLetter]--;
                }
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 1)
                {
                    // Check if there's already a green for this letter (meaning it's in the correct position)
                    bool hasGreen = false;
                    bool hasSecondGreen = false;
                    for (int index = 0; index < colorWordFive.Length; index++)
                    {
                        if (colorWordFive[index] == "G" && index != i && userGuessFive[index] == guessLetter)
                        {
                            if (!hasGreen)
                            {
                                hasGreen = true;
                            }
                            else if (!hasSecondGreen)
                            {
                                hasSecondGreen = true;
                            }
                            else
                            {
                                colorWordFive[i] = "Y"; // Third occurrence becomes yellow
                                return; // Exit the loop and function
                            }
                        }
                    }
                    if (hasSecondGreen)
                    {
                        colorWordFive[i] = "Y"; // Second occurrence becomes yellow
                    }
                    else if (hasGreen)
                    {
                        colorWordFive[i] = "G"; // First occurrence becomes green
                    }
                }
                else
                {
                    colorWordFive[i] = "W"; // White (gray) for not in the word
                }
            }
        }

        private ConsoleColor GetColorLetterFive(ref string colorWordFiveChar)
        {
            if (colorWordFiveChar == "G")
            {
                return ConsoleColor.Green;
            }
            else if (colorWordFiveChar == "Y")
            {
                return ConsoleColor.DarkYellow;
            }
            else
            {
                return ConsoleColor.White;
            }
        }

        public void DisplayAttemptFiveCorrect(ref string correctWord, ref string userGuess,
        ref string userGuessTwo, ref string userGuessThree, ref string userGuessFour, ref string userGuessFive, ref int randomIndex)
        {
            Console.Clear();
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 2);
            for (int i = 0; i < userGuess.Length; i++)
            {
                Check(ref correctWord, ref userGuess);
                Console.ForegroundColor = GetColorLetter(colorWordOne[i]);
                Console.Write(userGuess[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        •••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 4);
            for (int i = 0; i < userGuessTwo.Length; i++)
            {
                CheckTwo(ref correctWord, ref userGuessTwo);
                Console.ForegroundColor = GetColorLetterTwo(ref colorWordTwo[i]);
                Console.Write(userGuessTwo[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"                        
        •••••••••••••••••••••                         
        •                        
        •••••••••••••••••••••");
            Console.SetCursorPosition(10, 6);
            for (int i = 0; i < userGuessThree.Length; i++)
            {
                CheckThree(ref correctWord, ref userGuessThree);
                Console.ForegroundColor = GetColorLetterThree(ref colorWordThree[i]);
                Console.Write(userGuessThree[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"                        
        •••••••••••••••••••••                         
        •                        
        •••••••••••••••••••••");
            Console.SetCursorPosition(10, 8);
            for (int i = 0; i < userGuessFour.Length; i++)
            {
                CheckFour(ref correctWord, ref userGuessFour);
                Console.ForegroundColor = GetColorLetterFour(ref colorWordFour[i]);
                Console.Write(userGuessFour[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"                        
        •••••••••••••••••••••                         
        •                        
        •••••••••••••••••••••");
            Console.SetCursorPosition(10, 10);
            for (int i = 0; i < userGuessFive.Length; i++)
            {
                CheckFive(ref correctWord, ref userGuessFive);
                Console.ForegroundColor = GetColorLetterFive(ref colorWordFive[i]);
                Console.Write(userGuessFive[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"
        •••••••••••••••••••••
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
        }

        public void DisplayAttemptFive(ref string correctWord, ref string[] words, ref string userGuess,
        ref string userGuessTwo, ref string userGuessThree, ref string userGuessFour, ref string userGuessFive, ref int randomIndex)
        {
            Console.Clear();
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 2);
            for (int i = 0; i < userGuess.Length; i++)
            {
                Check(ref correctWord, ref userGuess);
                Console.ForegroundColor = GetColorLetter(colorWordOne[i]);
                Console.Write(userGuess[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        •••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 4);
            for (int i = 0; i < userGuessTwo.Length; i++)
            {
                CheckTwo(ref correctWord, ref userGuessTwo);
                Console.ForegroundColor = GetColorLetterTwo(ref colorWordTwo[i]);
                Console.Write(userGuessTwo[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"                        
        •••••••••••••••••••••                         
        •                        
        •••••••••••••••••••••");
            Console.SetCursorPosition(10, 6);
            for (int i = 0; i < userGuessThree.Length; i++)
            {
                CheckThree(ref correctWord, ref userGuessThree);
                Console.ForegroundColor = GetColorLetterThree(ref colorWordThree[i]);
                Console.Write(userGuessThree[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"                        
        •••••••••••••••••••••                         
        •                        
        •••••••••••••••••••••");
            Console.SetCursorPosition(10, 8);
            for (int i = 0; i < userGuessFour.Length; i++)
            {
                CheckFour(ref correctWord, ref userGuessFour);
                Console.ForegroundColor = GetColorLetterFour(ref colorWordFour[i]);
                Console.Write(userGuessFour[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"                        
        •••••••••••••••••••••                         
        •                        
        •••••••••••••••••••••");
            Console.SetCursorPosition(10, 10);
            for (int i = 0; i < userGuessFour.Length; i++)
            {
                CheckFive(ref correctWord, ref userGuessFive);
                Console.ForegroundColor = GetColorLetterFive(ref colorWordFive[i]);
                Console.Write(userGuessFive[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"
        •••••••••••••••••••••
        •••••••••••••••••••••                         
        •   •   •   •   •   •                         
        •••••••••••••••••••••");
            AttemptSix(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive, ref randomIndex);
        }

        public void AttemptSix(ref string correctWord, ref string[] words, ref string userGuess,
        ref string userGuessTwo, ref string userGuessThree, ref string userGuessFour, ref string userGuessFive, ref int randomIndex)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            System.Console.Write(@"
        Attempt 6");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            System.Console.Write($@"
        Write Here: "); Console.ResetColor();
            string userGuessSix = Console.ReadLine().ToUpper();
            if (userGuessSix.Length != 5)
            {
                DisplayAttemptFive(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive, ref randomIndex);
                AttemptSix(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive, ref randomIndex);
            }
            else
            {
                char[] chars = new char[10];
                chars = userGuessSix.ToCharArray();
                for (int i = 0; 1 < chars.Length; i++)
                {
                    char c = chars[i];
                    if (char.IsLetter(c))
                    {
                        for (int j = 0; j < words.Length; j++)
                        {
                            if (words.Any(word => word == userGuessSix))
                            {
                                if (userGuessSix == userGuess || userGuessSix == userGuessTwo || userGuessSix == userGuessThree
                                || userGuessSix == userGuessFour || userGuessSix == userGuessFive)
                                {
                                    DisplayAttemptFive(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive, ref randomIndex);
                                    AttemptSix(ref correctWord, ref words, ref userGuess,
                            ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive, ref randomIndex);
                                }
                                else
                                {
                                    if (userGuessSix.ToUpper() == correctWord)
                                    {
                                        Console.Clear();
                                        DisplayAttemptSixCorrect(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive, ref userGuessSix);
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                        System.Console.Write(@"
        Wow! You got it in 6 try's! Tap ENTER to continue  ");
                                        Console.ReadLine();
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.Write("Would you like to play again? (1. Yes\t2. No (Exit))");
                                        Console.ResetColor();
                                        string userInput = Console.ReadLine();
                                        while (userInput != "2")
                                        {
                                            if (userInput == "1")
                                            {
                                                Console.Clear();
                                                Game.Start();
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                Console.WriteLine("Good Bye");
                                                Environment.Exit(0);
                                            }
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.Write("Would you like to play again? (1. Yes\t2. No (Exit))");
                                            Console.ResetColor();
                                            userInput = Console.ReadLine();
                                        }
                                        if (userInput == "2")
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.WriteLine("Good Bye");
                                            Environment.Exit(0);
                                        }
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        DisplayAttemptSix(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive, ref userGuessSix);
                                        System.Console.Write(@"
        You failed! Tap ENTER to continue  ");
                                        Console.ReadLine();
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.Write("Would you like to play again? (1. Yes\t2. No (Exit))");
                                        Console.ResetColor();
                                        string userInput = Console.ReadLine();
                                        while (userInput != "2")
                                        {
                                            if (userInput == "1")
                                            {
                                                Console.Clear();
                                                Game.Start();
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                Console.WriteLine("Good Bye");
                                                Environment.Exit(0);
                                            }
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.Write("Would you like to play again? (1. Yes\t2. No (Exit))");
                                            Console.ResetColor();
                                            userInput = Console.ReadLine();
                                        }
                                        if (userInput == "2")
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.WriteLine("Good Bye");
                                            Environment.Exit(0);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                DisplayAttemptFive(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive, ref randomIndex);
                                AttemptSix(ref correctWord, ref words, ref userGuess,
                        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive, ref randomIndex);
                            }
                        }
                    }
                    else
                    {
                        DisplayAttemptFive(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive, ref randomIndex);
                        AttemptSix(ref correctWord, ref words, ref userGuess,
                ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive, ref randomIndex);
                    }
                }
            }
        }

        public void CheckSix(ref string correctWord, ref string userGuessSix)
        {
            Dictionary<char, int> answerCharCounts = new Dictionary<char, int>();
            foreach (char letter in correctWord)
            {
                if (answerCharCounts.ContainsKey(letter))
                {
                    answerCharCounts[letter]++;
                }
                else
                {
                    answerCharCounts[letter] = 1;
                }
            }

            for (int i = 0; i < correctWord.Length; i++)
            {
                char guessLetter = userGuessSix[i];
                char answerLetter = correctWord[i];
                // Handle correct position (Green)
                if (guessLetter == answerLetter)
                {
                    colorWordSix[i] = "G";
                    answerCharCounts[guessLetter]--;
                }
                // Handle correct letter in wrong position (Yellow) with repeated letters
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 0)
                {
                    // Check if there's already a green for this letter (meaning it's in the correct position)
                    bool hasGreen = false;
                    for (int index = 0; index < colorWordSix.Length; index++)
                    {
                        if (colorWordSix[index] == "G" && index != i && userGuessSix[index] == guessLetter)
                        {
                            hasGreen = true;
                            break;
                        }
                    }
                    if (hasGreen)
                    {
                        colorWordSix[i] = "Y"; // Second occurrence becomes yellow
                    }
                    else
                    {
                        colorWordSix[i] = "Y"; // First occurrence becomes yellow
                    }
                    answerCharCounts[guessLetter]--;
                }
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 1)
                {
                    // Check if there's already a green for this letter (meaning it's in the correct position)
                    bool hasGreen = false;
                    bool hasSecondGreen = false;
                    for (int index = 0; index < colorWordSix.Length; index++)
                    {
                        if (colorWordSix[index] == "G" && index != i && userGuessSix[index] == guessLetter)
                        {
                            if (!hasGreen)
                            {
                                hasGreen = true;
                            }
                            else if (!hasSecondGreen)
                            {
                                hasSecondGreen = true;
                            }
                            else
                            {
                                colorWordSix[i] = "Y"; // Third occurrence becomes yellow
                                return; // Exit the loop and function
                            }
                        }
                    }
                    if (hasSecondGreen)
                    {
                        colorWordSix[i] = "Y"; // Second occurrence becomes yellow
                    }
                    else if (hasGreen)
                    {
                        colorWordSix[i] = "G"; // First occurrence becomes green
                    }
                }
                else
                {
                    colorWordSix[i] = "W"; // White (gray) for not in the word
                }
            }
        }

        private ConsoleColor GetColorLetterSix(ref string colorWordSixChar)
        {
            if (colorWordSixChar == "G")
            {
                return ConsoleColor.Green;
            }
            else if (colorWordSixChar == "Y")
            {
                return ConsoleColor.DarkYellow;
            }
            else
            {
                return ConsoleColor.White;
            }
        }


        public void DisplayAttemptSixCorrect(ref string correctWord, ref string[] words, ref string userGuess,
ref string userGuessTwo, ref string userGuessThree, ref string userGuessFour, ref string userGuessFive, ref string userGuessSix)
        {
            Console.Clear();
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 2);
            for (int i = 0; i < userGuess.Length; i++)
            {
                Check(ref correctWord, ref userGuess);
                Console.ForegroundColor = GetColorLetter(colorWordOne[i]);
                Console.Write(userGuess[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        •••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 4);
            for (int i = 0; i < userGuessTwo.Length; i++)
            {
                CheckTwo(ref correctWord, ref userGuessTwo);
                Console.ForegroundColor = GetColorLetterTwo(ref colorWordTwo[i]);
                Console.Write(userGuessTwo[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"                        
        •••••••••••••••••••••                         
        •                        
        •••••••••••••••••••••");
            Console.SetCursorPosition(10, 6);
            for (int i = 0; i < userGuessThree.Length; i++)
            {
                CheckThree(ref correctWord, ref userGuessThree);
                Console.ForegroundColor = GetColorLetterThree(ref colorWordThree[i]);
                Console.Write(userGuessThree[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"                        
        •••••••••••••••••••••                         
        •                        
        •••••••••••••••••••••");
            Console.SetCursorPosition(10, 8);
            for (int i = 0; i < userGuessFour.Length; i++)
            {
                CheckFour(ref correctWord, ref userGuessFour);
                Console.ForegroundColor = GetColorLetterFour(ref colorWordFour[i]);
                Console.Write(userGuessFour[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"                        
        •••••••••••••••••••••                         
        •                        
        •••••••••••••••••••••");
            Console.SetCursorPosition(10, 10);
            for (int i = 0; i < userGuessFive.Length; i++)
            {
                CheckFive(ref correctWord, ref userGuessFive);
                Console.ForegroundColor = GetColorLetterFive(ref colorWordFive[i]);
                Console.Write(userGuessFive[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"                        
        •••••••••••••••••••••                         
        •                        
        •••••••••••••••••••••");
            Console.SetCursorPosition(10, 12);
            for (int i = 0; i < userGuessSix.Length; i++)
            {
                CheckSix(ref correctWord, ref userGuessSix);
                Console.ForegroundColor = GetColorLetterSix(ref colorWordSix[i]);
                Console.Write(userGuessSix[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"
        •••••••••••••••••••••");
        }

        public void DisplayAttemptSix(ref string correctWord, ref string[] words, ref string userGuess,
ref string userGuessTwo, ref string userGuessThree, ref string userGuessFour, ref string userGuessFive, ref string userGuessSix)
        {
            Console.Clear();
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 2);
            for (int i = 0; i < userGuess.Length; i++)
            {
                Check(ref correctWord, ref userGuess);
                Console.ForegroundColor = GetColorLetter(colorWordOne[i]);
                Console.Write(userGuess[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.WriteLine(@"                             
        •••••••••••••••••••••                        
        •                                              
        •••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 4);
            for (int i = 0; i < userGuessTwo.Length; i++)
            {
                CheckTwo(ref correctWord, ref userGuessTwo);
                Console.ForegroundColor = GetColorLetterTwo(ref colorWordTwo[i]);
                Console.Write(userGuessTwo[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"                        
        •••••••••••••••••••••                         
        •                        
        •••••••••••••••••••••");
            Console.SetCursorPosition(10, 6);
            for (int i = 0; i < userGuessThree.Length; i++)
            {
                CheckThree(ref correctWord, ref userGuessThree);
                Console.ForegroundColor = GetColorLetterThree(ref colorWordThree[i]);
                Console.Write(userGuessThree[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"                        
        •••••••••••••••••••••                         
        •                        
        •••••••••••••••••••••");
            Console.SetCursorPosition(10, 8);
            for (int i = 0; i < userGuessFour.Length; i++)
            {
                CheckFour(ref correctWord, ref userGuessFour);
                Console.ForegroundColor = GetColorLetterFour(ref colorWordFour[i]);
                Console.Write(userGuessFour[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"                        
        •••••••••••••••••••••                         
        •                        
        •••••••••••••••••••••");
            Console.SetCursorPosition(10, 10);
            for (int i = 0; i < userGuessFive.Length; i++)
            {
                CheckFive(ref correctWord, ref userGuessFive);
                Console.ForegroundColor = GetColorLetterFive(ref colorWordFive[i]);
                Console.Write(userGuessFive[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"                        
        •••••••••••••••••••••                         
        •                        
        •••••••••••••••••••••");
            Console.SetCursorPosition(10, 12);
            for (int i = 0; i < userGuessSix.Length; i++)
            {
                CheckSix(ref correctWord, ref userGuessSix);
                Console.ForegroundColor = GetColorLetterSix(ref colorWordSix[i]);
                Console.Write(userGuessSix[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"
        •••••••••••••••••••••");
        }

        private async void PlayAgain()
        {
            Console.Write("Would you like to play again? (1. Yes\t2. No (Exit)): ");
            string userInput = Console.ReadLine();
            if (userInput == "1")
            {
                Game.Start();
            }
            else if (userInput == "2")
            {
                Console.Clear();
                Console.WriteLine("Goodbye!");
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter 1 or 2.");
                PlayAgain();
            }
        }
    }
}



