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
            // string correctWord = words[randomIndex];
            string correctWord;
            System.Console.WriteLine("Guess a 5 letter word in 6 attempts! Press ENTER to begin");
            Console.ReadLine();
            Console.Clear();
            AttemptOne(ref words);
        }

        public void AttemptOne(ref string[] words)
        {
            Console.Clear();
            System.Console.Write(@"
        ••••••••••••••••••••••
        •    •   •   •   •   •
        ••••••••••••••••••••••");
            System.Console.Write(@"
        ••••••••••••••••••••••
        •    •   •   •   •   •
        ••••••••••••••••••••••");
            System.Console.Write(@"
        ••••••••••••••••••••••
        •    •   •   •   •   •
        ••••••••••••••••••••••");
            System.Console.Write(@"
        ••••••••••••••••••••••
        •    •   •   •   •   •
        ••••••••••••••••••••••");
            System.Console.Write(@"
        ••••••••••••••••••••••
        •    •   •   •   •   •
        ••••••••••••••••••••••");
            System.Console.Write(@"
        ••••••••••••••••••••••
        •    •   •   •   •   •
        ••••••••••••••••••••••");
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
                AttemptOne(ref words);
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
                                    DisplayAttemptOneCorrect(ref userGuess);
                                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                                    System.Console.Write(@"
        Wow! You got it in first try! Tap ENTER to exit the game  ");
                                    Console.ReadLine();
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    System.Console.WriteLine("Good Bye");
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    Console.Clear();
                                    Check(ref correctWord, ref userGuess);
                                    DisplayAttemptOne(ref correctWord, ref userGuess, ref words);
                                }
                            }
                            else
                            {
                                AttemptOne(ref words);
                            }
                        }
                    }
                    else
                    {
                        AttemptOne(ref words);
                    }
                }
            }
        }

        private void Check(ref string correctWord, ref string userGuess)
        {
            for (int i = 0; i < correctWord.Length; i++)
            {
                if (correctWord[i] == userGuess[i])
                {
                    colorWordOne[i] = "G";
                }
                else if (correctWord.Contains(userGuess[i])) //This is the part i researched, I did not know about Contains
                {
                    colorWordOne[i] = "Y";
                }
                else if (correctWord[i] == userGuess[i] && correctWord.Contains(userGuess[i]))
                {
                    if (correctWord[i] == userGuess[i])
                    {
                        colorWordOne[i] = "G";
                    }
                    else if (correctWord.Contains(userGuess[i]))
                    {
                        colorWordOne[i] = "W";
                    }
                }
                else
                {
                    colorWordOne[i] = "W";
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

        public void DisplayAttemptOneCorrect(ref string userGuess)
        {
            Console.Clear();
            Console.WriteLine(@"                             
        ••••••••••••••••••••••                        
        •                                              
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(11, 2);
            for (int i = 0; i < userGuess.Length; i++)
            {
                Console.ForegroundColor = GetColorLetter(colorWordOne[i]);
                Console.Write(userGuess[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ResetColor();
            System.Console.Write(@"
        ••••••••••••••••••••••                         
        ••••••••••••••••••••••                         
        •    •   •   •   •   •                         
        ••••••••••••••••••••••");
            System.Console.Write(@"
        ••••••••••••••••••••••                         
        •    •   •   •   •   •                         
        ••••••••••••••••••••••");
            System.Console.Write(@"
        ••••••••••••••••••••••                         
        •    •   •   •   •   •                         
        ••••••••••••••••••••••");
            System.Console.Write(@"
        ••••••••••••••••••••••                         
        •    •   •   •   •   •                         
        ••••••••••••••••••••••");
            System.Console.Write(@"
        ••••••••••••••••••••••                         
        •    •   •   •   •   •                         
        ••••••••••••••••••••••");
        }

        public void DisplayAttemptOne(ref string correctWord, ref string userGuess, ref string[] words)
        {
            Console.Clear();
            Console.WriteLine(@"                             
        ••••••••••••••••••••••                        
        •                                              
        ••••••••••••••••••••••               
        ");

            Console.SetCursorPosition(11, 2);
            for (int i = 0; i < userGuess.Length; i++)
            {
                Console.ForegroundColor = GetColorLetter(colorWordOne[i]);
                Console.Write(userGuess[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.ResetColor();
            System.Console.Write(@"
        ••••••••••••••••••••••                         
        ••••••••••••••••••••••                         
        •    •   •   •   •   •                         
        ••••••••••••••••••••••");
            System.Console.Write(@"
        ••••••••••••••••••••••                         
        •    •   •   •   •   •                         
        ••••••••••••••••••••••");
            System.Console.Write(@"
        ••••••••••••••••••••••                         
        •    •   •   •   •   •                         
        ••••••••••••••••••••••");
            System.Console.Write(@"
        ••••••••••••••••••••••                         
        •    •   •   •   •   •                         
        ••••••••••••••••••••••");
            System.Console.Write(@"
        ••••••••••••••••••••••                         
        •    •   •   •   •   •                         
        ••••••••••••••••••••••");
            AttemptTwo(ref correctWord, ref userGuess, ref words);
        }

        public void AttemptTwo(ref string correctWord, ref string userGuess, ref string[] words)
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
                DisplayAttemptOne(ref correctWord, ref userGuess, ref words);
                AttemptTwo(ref correctWord, ref userGuess, ref words);
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
                                    DisplayAttemptOne(ref correctWord, ref userGuess, ref words);
                                    AttemptTwo(ref correctWord, ref userGuess, ref words);
                                }
                                else
                                {
                                    if (userGuessTwo.ToUpper() == correctWord)
                                    {
                                        Console.Clear();
                                        DisplayAttemptTwoCorrect(ref correctWord, ref userGuess, ref userGuessTwo);
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                        System.Console.Write(@"
        Wow! You got it in two try's! Tap ENTER to exit the game  ");
                                        Console.ReadLine();
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        System.Console.WriteLine("Good Bye");
                                        Environment.Exit(0);
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        DisplayAttemptTwo(ref correctWord, ref words, ref userGuess, ref userGuessTwo);
                                    }
                                }
                            }
                            else
                            {
                                DisplayAttemptOne(ref correctWord, ref userGuess, ref words);
                                AttemptTwo(ref correctWord, ref userGuess, ref words);
                            }
                        }
                    }
                    else
                    {
                        DisplayAttemptOne(ref correctWord, ref userGuess, ref words);
                        AttemptTwo(ref correctWord, ref userGuess, ref words);
                    }
                }
            }
        }

        public void CheckTwo(ref string correctWord, ref string userGuessTwo)
        {
            for (int i = 0; i < correctWord.Length; i++)
            {
                if (correctWord[i] == userGuessTwo[i])
                {
                    colorWordTwo[i] = "G";
                }
                else if (correctWord.Contains(userGuessTwo[i]))
                {
                    int firstIndex = correctWord.IndexOf(userGuessTwo[i]); //IndexOf was googled
                    int lastIndex = correctWord.LastIndexOf(userGuessTwo[i]); //LastIndexOf was googled

                    if (firstIndex != lastIndex && lastIndex == i)
                    {
                        colorWordTwo[i] = "Y";
                    }
                    else
                    {
                        colorWordTwo[i] = "W";
                    }
                }
                else
                {
                    colorWordTwo[i] = "W";
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

        public void DisplayAttemptTwoCorrect(ref string correctWord, ref string userGuess, ref string userGuessTwo)
        {
            Console.Clear();
            Console.WriteLine(@"                             
        ••••••••••••••••••••••                        
        •                                              
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(11, 2);
            for (int i = 0; i < userGuess.Length; i++)
            {
                Check(ref correctWord, ref userGuess);
                Console.ForegroundColor = GetColorLetter(colorWordOne[i]);
                Console.Write(userGuess[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.WriteLine(@"                             
        ••••••••••••••••••••••                        
        •                                              
        ••••••••••••••••••••••               
        ");
            // CheckTwo(ref colorWordTwo, ref words, ref correctWord, ref userGuessTwo);
            Console.SetCursorPosition(11, 4);
            for (int i = 0; i < userGuessTwo.Length; i++)
            {
                CheckTwo(ref correctWord, ref userGuessTwo);
                Console.ForegroundColor = GetColorLetterTwo(ref colorWordTwo[i]);
                Console.Write(userGuessTwo[i]);
                Console.ResetColor();
                Console.Write(" • "); //laxly
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"
        ••••••••••••••••••••••                         
        ••••••••••••••••••••••                         
        •    •   •   •   •   •                         
        ••••••••••••••••••••••");
            System.Console.Write(@"
        ••••••••••••••••••••••                         
        •    •   •   •   •   •                         
        ••••••••••••••••••••••");
            System.Console.Write(@"
        ••••••••••••••••••••••                         
        •    •   •   •   •   •                         
        ••••••••••••••••••••••");
            System.Console.Write(@"
        ••••••••••••••••••••••                         
        •    •   •   •   •   •                         
        ••••••••••••••••••••••");
        }

        public void DisplayAttemptTwo(ref string correctWord, ref string[] words, ref string userGuess, ref string userGuessTwo)
        {
            Console.Clear();
            Console.WriteLine(@"                             
        ••••••••••••••••••••••                        
        •                                              
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(11, 2);
            for (int i = 0; i < userGuess.Length; i++)
            {
                Check(ref correctWord, ref userGuess);
                Console.ForegroundColor = GetColorLetter(colorWordOne[i]);
                Console.Write(userGuess[i]);
                Console.ResetColor();
                Console.Write(" • ");
            }
            Console.WriteLine(@"                             
        ••••••••••••••••••••••                        
        •                                              
        ••••••••••••••••••••••               
        ");
            // CheckTwo(ref colorWordTwo, ref words, ref correctWord, ref userGuessTwo);
            Console.SetCursorPosition(11, 4);
            for (int i = 0; i < userGuessTwo.Length; i++)
            {
                CheckTwo(ref correctWord, ref userGuessTwo);
                Console.ForegroundColor = GetColorLetterTwo(ref colorWordTwo[i]);
                Console.Write(userGuessTwo[i]);
                Console.ResetColor();
                Console.Write(" • "); //laxly
            }
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write(@"
        ••••••••••••••••••••••                         
        ••••••••••••••••••••••                         
        •    •   •   •   •   •                         
        ••••••••••••••••••••••");
            System.Console.Write(@"
        ••••••••••••••••••••••                         
        •    •   •   •   •   •                         
        ••••••••••••••••••••••");
            System.Console.Write(@"
        ••••••••••••••••••••••                         
        •    •   •   •   •   •                         
        ••••••••••••••••••••••");
            System.Console.Write(@"
        ••••••••••••••••••••••                         
        •    •   •   •   •   •                         
        ••••••••••••••••••••••");
            AttemptThree(ref correctWord, ref words, ref userGuess, ref userGuessTwo);
        }

        public void AttemptThree(ref string correctWord, ref string[] words, ref string userGuess, ref string userGuessTwo)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            System.Console.Write(@"
        Attempt 2");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            System.Console.Write($@"
        Write Here: "); Console.ResetColor();
            string userGuessThree = Console.ReadLine();
            static async Task Main(string[] args)
            {
                await Game.Start();
            }
        }
    }
}


