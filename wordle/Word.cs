namespace wordle
{
    public class Word
    {
        const int MAX_WORDS = 6000; //max amount of words
        string[] colorWordOne = new string[5]; //will be used to coler words for each user guess
        string[] colorWordTwo = new string[5];
        string[] colorWordThree = new string[5];
        string[] colorWordFour = new string[5];
        string[] colorWordFive = new string[5];
        string[] colorWordSix = new string[5];
        string correctWord;



        public async Task GetCorrectWord()
        {
            correctWord = await Game.Start();
            Wordle(correctWord);
        }

        /* 
            the method below is a process to store all the 5 letter words from a file called wordle.txt with has all the words as api,
            but it will be used to check if user guess word "exist as a 5 letter word in English"
        */

        public void Wordle(string correctWord)
        {
            this.correctWord = correctWord;
            Console.Clear();
            string[] words = new string[MAX_WORDS]; //set the array max to 6000
            int count = 0;
            StreamReader inFile = new StreamReader("wordle.txt"); // open file
            string line = inFile.ReadLine(); // read file
            while (line != null)
            {
                string[] temp = line.Split(' '); // process it 
                words[count] = temp[0];
                count++;
                line = inFile.ReadLine();
            }
            inFile.Close(); //Close file
            AttemptOne(ref words, ref correctWord);
        }

        // This is were real wordle begans
        public void AttemptOne(ref string[] words, ref string correctWord)
        {
            Console.Clear();
            System.Console.WriteLine(@" __        __            _ _      
 \ \      / /__  _ __ __| | | ___ 
  \ \ /\ / / _ \| '__/ _` | |/ _ \
   \ V  V / (_) | | | (_| | |  __/
    \_/\_/ \___/|_|  \__,_|_|\___|
        •••••••••••••••••••••
        •   •   •   •   •   •
        •••••••••••••••••••••
        •••••••••••••••••••••
        •   •   •   •   •   •
        •••••••••••••••••••••
        •••••••••••••••••••••
        •   •   •   •   •   •
        •••••••••••••••••••••
        •••••••••••••••••••••
        •   •   •   •   •   •
        •••••••••••••••••••••
        •••••••••••••••••••••
        •   •   •   •   •   •
        •••••••••••••••••••••
        •••••••••••••••••••••
        •   •   •   •   •   •
        •••••••••••••••••••••");
            System.Console.WriteLine(correctWord);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            System.Console.Write(@"
        Attempt 1");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            //Asks user for a word
            System.Console.Write($@"
        Write Here: "); Console.ResetColor();
            string userGuess = Console.ReadLine().ToUpper(); //sets userGuess to all Capital letters to match api word and wordle.txt
            if (userGuess.Length != 5) //if userGuess is not 5 letters 
            {
                AttemptOne(ref words, ref correctWord);
            }
            else //if userGuess is a 5 letter word
            {
                char[] chars = new char[10];
                chars = userGuess.ToCharArray(); //userGuess is not split into characters
                for (int i = 0; i < chars.Length; i++) // checks each character (letter)
                {
                    char c = chars[i];
                    if (char.IsLetter(c)) //if all characters in userGuess is a letter then it goes to next check
                    {
                        for (int j = 0; j < words.Length; j++)
                        {
                            if (words.Any(word => word == userGuess)) //if userGuess exist in wordle.txt to make sure it is a real word
                            {
                                if (userGuess.ToUpper() == correctWord) // check if userGuess matches correct word
                                {
                                    Console.Clear();
                                    Check(ref correctWord, ref userGuess); //check for what letters are in correct possition or not, follows real Wordle rules
                                    DisplayAttemptOneCorrect(ref userGuess); // displays user the result
                                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                                    System.Console.Write(@"
        Genius! You got it in 1 try! Tap ENTER to continue the game  ");
                                    Console.ReadLine();
                                    Console.Clear();
                                    PlayAgain();
                                }
                                else // if not then user will go to next available attempt
                                {
                                    Console.Clear();
                                    Check(ref correctWord, ref userGuess);
                                    DisplayAttemptOne(ref correctWord, ref userGuess, ref words); // displays attempt relsut and ask for next attempt
                                }
                            }
                            else
                            {
                                AttemptOne(ref words, ref correctWord); //if userGuess word does not exist in English language
                            }
                        }
                    }
                    else // if one of the characters in userGuess is not a letter
                    {
                        AttemptOne(ref words, ref correctWord);
                    }
                }
            }
        }

        //method that checks each letter and matches it against correct letter positions and colors it according to real Wordle rules
        private void Check(ref string correctWord, ref string userGuess)
        {
            //*colorWordOne (or Two ... etc) will be used to color letters in future method
            // Dictionary to store letter counts in the answer word
            Dictionary<char, int> answerCharCounts = new Dictionary<char, int>();
            //counts numer of letters 
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
            //Checks each userGuess letter compared to correct word letter positions and if it is there
            for (int i = 0; i < correctWord.Length; i++)
            {
                char guessLetter = userGuess[i];
                char answerLetter = correctWord[i];
                if (guessLetter == answerLetter) // if letter match position and actual letter for userGuess and correctWord
                {
                    colorWordOne[i] = "G"; // will be set to G for green colot later
                    answerCharCounts[guessLetter]--; // minus guess letter 
                }
                // Handle correct letter in wrong position with repeated letters
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
                    if (hasGreen) //If there's a green for this letter, mark the guessed letter as yellow (second occurrence)
                    {
                        colorWordOne[i] = "Y"; // Second occurrence becomes yellow
                    }
                    else //Otherwise, mark the guessed letter as yellow (first occurrence)
                    {
                        colorWordOne[i] = "Y"; // First occurrence becomes yellow
                    }
                    answerCharCounts[guessLetter]--; //minus guess letter
                }
                //Below handles repeated letters with occur multiple times in correctWord
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 1) //if letter repeats
                {
                    // Check if there's already a green for this letter (meaning it's in the correct position)
                    bool hasGreen = false;
                    bool hasSecondGreen = false;
                    for (int index = 0; index < colorWordOne.Length; index++)
                    {
                        if (colorWordOne[index] == "G" && index != i && userGuess[index] == guessLetter)
                        {
                            if (!hasGreen) //If the first occurrence of the guessed letter is already marked as green
                            {
                                hasGreen = true;
                            }
                            else if (!hasSecondGreen) //If the second occurrence of the guessed letter is already marked as green
                            {
                                hasSecondGreen = true;
                            }
                            else //If there are more than two green occurrences for the same letter, mark it as yellow (third occurrence)
                            {
                                colorWordOne[i] = "Y"; // Third occurrence becomes yellow
                                return;
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
                else // if userGuess letter is not in the correctWord
                {
                    colorWordOne[i] = "W"; // W for white color
                }
            }
        }

        //This method below colors letters in a color set by the method above using G (green) Y (yellow) W (white)
        private ConsoleColor GetColorLetter(string colorWordOneChar)
        {
            if (colorWordOneChar == "G") //if a letter in colorWordOne was set to G it will become green
            {
                return ConsoleColor.Green;
            }
            else if (colorWordOneChar == "Y") //if a letter in colorWordOne was set to Y it will become yellow
            {
                return ConsoleColor.DarkYellow;
            }
            else //if a letter in colorWordOne was set to W it will become white
            {
                return ConsoleColor.White;
            }
        }

        //method below will show updated box with userGuess in it
        public void DisplayAttemptOneCorrect(ref string userGuess)
        {
            Console.Clear();
            System.Console.WriteLine(@" __        __            _ _      
 \ \      / /__  _ __ __| | | ___ 
  \ \ /\ / / _ \| '__/ _` | |/ _ \
   \ V  V / (_) | | | (_| | |  __/
    \_/\_/ \___/|_|  \__,_|_|\___|
        •••••••••••••••••••••
        •
");
            System.Console.WriteLine("\n\n\n\n");
            System.Console.WriteLine(@"                                                                                                 
        •••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 6); //sets the location (cordinates) in the terminal to fit it into ASCII 
            for (int i = 0; i < userGuess.Length; i++)
            {
                Console.ForegroundColor = GetColorLetter(colorWordOne[i]); // goes to GetColorMethod
                Console.Write(userGuess[i]);
                Console.ResetColor();
                Console.Write(" • "); //add a bullet point between each letter
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

        public void DisplayAttemptOne(ref string correctWord, ref string userGuess, ref string[] words)
        {
            Console.Clear();
            System.Console.WriteLine(@" __        __            _ _      
 \ \      / /__  _ __ __| | | ___ 
  \ \ /\ / / _ \| '__/ _` | |/ _ \
   \ V  V / (_) | | | (_| | |  __/
    \_/\_/ \___/|_|  \__,_|_|\___|
        •••••••••••••••••••••
        •
");
            // System.Console.WriteLine("\n\n\n\n");
            Console.WriteLine(@"                                              
        •••••••••••••••••••••               
        ");

            Console.SetCursorPosition(10, 6);
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
            AttemptTwo(ref correctWord, ref userGuess, ref words); //Displays attempt 2 while not clearing the console to make it feel like a real Wordle
        }

        //All things are very similar to AttemptOne stuff except in few places (with i comment) including AttemptSix
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
                                if (userGuessTwo == userGuess) //checks if userGuess is same as one of the previous guesses, to ensure that that will not count as an attempt
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
        Magnificent! You got it in 2 try's! Tap ENTER to continue the game  ");
                                        Console.ReadLine();
                                        Console.Clear();
                                        PlayAgain();
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

        private void CheckTwo(ref string correctWord, ref string userGuessTwo)
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
                if (guessLetter == answerLetter)
                {
                    colorWordTwo[i] = "G";
                    answerCharCounts[guessLetter]--;
                }
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 0)
                {
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
                        colorWordTwo[i] = "Y";
                    }
                    else
                    {
                        colorWordTwo[i] = "Y";
                    }
                    answerCharCounts[guessLetter]--;
                }
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 1)
                {
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
                                colorWordTwo[i] = "Y";
                                return;
                            }
                        }
                    }
                    if (hasSecondGreen)
                    {
                        colorWordTwo[i] = "Y";
                    }
                    else if (hasGreen)
                    {
                        colorWordTwo[i] = "G";
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
            System.Console.WriteLine(@" __        __            _ _      
 \ \      / /__  _ __ __| | | ___ 
  \ \ /\ / / _ \| '__/ _` | |/ _ \
   \ V  V / (_) | | | (_| | |  __/
    \_/\_/ \___/|_|  \__,_|_|\___|
        •••••••••••••••••••••
        •
");
            System.Console.WriteLine("\n\n\n\n");
            Console.WriteLine(@"                                             
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 6);
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
            Console.SetCursorPosition(10, 8);
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

        public void DisplayAttemptTwo(ref string correctWord, ref string[] words, ref string userGuess, ref string userGuessTwo)
        {
            Console.Clear();
            System.Console.WriteLine(@" __        __            _ _      
 \ \      / /__  _ __ __| | | ___ 
  \ \ /\ / / _ \| '__/ _` | |/ _ \
   \ V  V / (_) | | | (_| | |  __/
    \_/\_/ \___/|_|  \__,_|_|\___|
        •••••••••••••••••••••
        •
");
            System.Console.WriteLine("\n\n\n\n");
            Console.WriteLine(@"                                        
        •••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 6);
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
            Console.SetCursorPosition(10, 8);
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
            AttemptThree(ref correctWord, ref words, ref userGuess, ref userGuessTwo);
        }

        public void AttemptThree(ref string correctWord, ref string[] words, ref string userGuess, ref string userGuessTwo)
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
                DisplayAttemptTwo(ref correctWord, ref words, ref userGuess, ref userGuessTwo);
                AttemptThree(ref correctWord, ref words, ref userGuess, ref userGuessTwo);
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
                                    DisplayAttemptTwo(ref correctWord, ref words, ref userGuess, ref userGuessTwo);
                                    AttemptThree(ref correctWord, ref words, ref userGuess, ref userGuessTwo);
                                }
                                else
                                {
                                    if (userGuessThree.ToUpper() == correctWord)
                                    {
                                        Console.Clear();
                                        DisplayAttemptThreeCorrect(ref correctWord, ref userGuess,
        ref userGuessTwo, ref userGuessThree);
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                        System.Console.Write(@"
        Impressive! You got it in 3 try's! Tap ENTER to continue the game  ");
                                        Console.ReadLine();
                                        Console.Clear();
                                        PlayAgain();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        DisplayAttemptThree(ref correctWord, ref words,
        ref userGuess, ref userGuessTwo, ref userGuessThree);
                                    }
                                }
                            }
                            else
                            {
                                DisplayAttemptTwo(ref correctWord, ref words, ref userGuess, ref userGuessTwo);
                                AttemptThree(ref correctWord, ref words, ref userGuess, ref userGuessTwo);
                            }
                        }
                    }
                    else
                    {
                        DisplayAttemptTwo(ref correctWord, ref words, ref userGuess, ref userGuessTwo);
                        AttemptThree(ref correctWord, ref words, ref userGuess, ref userGuessTwo);
                    }
                }
            }
        }

        private void CheckThree(ref string correctWord, ref string userGuessThree)
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
                if (guessLetter == answerLetter)
                {
                    colorWordThree[i] = "G";
                    answerCharCounts[guessLetter]--;
                }
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 0)
                {
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
                        colorWordThree[i] = "Y";
                    }
                    else
                    {
                        colorWordThree[i] = "Y";
                    }
                    answerCharCounts[guessLetter]--;
                }
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 1)
                {
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
                                colorWordThree[i] = "Y";
                                return;
                            }
                        }
                    }
                    if (hasSecondGreen)
                    {
                        colorWordThree[i] = "Y";
                    }
                    else if (hasGreen)
                    {
                        colorWordThree[i] = "G";
                    }
                }
                else
                {
                    colorWordThree[i] = "W";
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
        ref string userGuessTwo, ref string userGuessThree)
        {
            Console.Clear();
            System.Console.WriteLine(@" __        __            _ _      
 \ \      / /__  _ __ __| | | ___ 
  \ \ /\ / / _ \| '__/ _` | |/ _ \
   \ V  V / (_) | | | (_| | |  __/
    \_/\_/ \___/|_|  \__,_|_|\___|
        •••••••••••••••••••••
        •
");
            System.Console.WriteLine("\n\n\n\n");
            Console.WriteLine(@"                                   
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 6);
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
            Console.SetCursorPosition(10, 8);
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
            Console.SetCursorPosition(10, 10);
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
        ref string userGuess, ref string userGuessTwo, ref string userGuessThree)
        {
            Console.Clear();
            System.Console.WriteLine(@" __        __            _ _      
 \ \      / /__  _ __ __| | | ___ 
  \ \ /\ / / _ \| '__/ _` | |/ _ \
   \ V  V / (_) | | | (_| | |  __/
    \_/\_/ \___/|_|  \__,_|_|\___|
        •••••••••••••••••••••
        •
");
            System.Console.WriteLine("\n\n\n\n");
            Console.WriteLine(@"                                             
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 6);
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
            Console.SetCursorPosition(10, 8);
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
            Console.SetCursorPosition(10, 10);
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
        ref userGuess, ref userGuessTwo, ref userGuessThree);
        }

        public void AttemptFour(ref string correctWord, ref string[] words,
        ref string userGuess, ref string userGuessTwo, ref string userGuessThree)
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
                DisplayAttemptThree(ref correctWord, ref words, ref userGuess, ref userGuessTwo, ref userGuessThree);
                AttemptFour(ref correctWord, ref words,
        ref userGuess, ref userGuessTwo, ref userGuessThree);
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
                                    DisplayAttemptThree(ref correctWord, ref words, ref userGuess, ref userGuessTwo, ref userGuessThree);
                                    AttemptFour(ref correctWord, ref words,
                                    ref userGuess, ref userGuessTwo, ref userGuessThree);
                                }
                                else
                                {
                                    if (userGuessFour.ToUpper() == correctWord)
                                    {
                                        Console.Clear();
                                        DisplayAttemptFourCorrect(ref correctWord, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour);
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                        System.Console.Write(@"
        Splendid! You got it in 4 try's! Tap ENTER to exit the game  ");
                                        Console.ReadLine();
                                        Console.Clear();
                                        PlayAgain();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        DisplayAttemptFour(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour);
                                    }
                                }
                            }
                            else
                            {
                                DisplayAttemptThree(ref correctWord, ref words, ref userGuess, ref userGuessTwo, ref userGuessThree);
                                AttemptFour(ref correctWord, ref words,
                                ref userGuess, ref userGuessTwo, ref userGuessThree);
                            }
                        }
                    }
                    else
                    {
                        DisplayAttemptThree(ref correctWord, ref words, ref userGuess, ref userGuessTwo, ref userGuessThree);
                        AttemptFour(ref correctWord, ref words,
                        ref userGuess, ref userGuessTwo, ref userGuessThree);
                    }
                }
            }
        }

        private void CheckFour(ref string correctWord, ref string userGuessFour)
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
                if (guessLetter == answerLetter)
                {
                    colorWordFour[i] = "G";
                    answerCharCounts[guessLetter]--;
                }
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 0)
                {
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
                        colorWordFour[i] = "Y";
                    }
                    else
                    {
                        colorWordFour[i] = "Y";
                    }
                    answerCharCounts[guessLetter]--;
                }
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 1)
                {
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
                                colorWordFour[i] = "Y";
                                return;
                            }
                        }
                    }
                    if (hasSecondGreen)
                    {
                        colorWordFour[i] = "Y";
                    }
                    else if (hasGreen)
                    {
                        colorWordFour[i] = "G";
                    }
                }
                else
                {
                    colorWordFour[i] = "W";
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
        ref string userGuessTwo, ref string userGuessThree, ref string userGuessFour)
        {
            Console.Clear();
            System.Console.WriteLine(@" __        __            _ _      
 \ \      / /__  _ __ __| | | ___ 
  \ \ /\ / / _ \| '__/ _` | |/ _ \
   \ V  V / (_) | | | (_| | |  __/
    \_/\_/ \___/|_|  \__,_|_|\___|
        •••••••••••••••••••••
        •
");
            System.Console.WriteLine("\n\n\n\n");
            Console.WriteLine(@"                                           
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 6);
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
            Console.SetCursorPosition(10, 8);
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
            Console.SetCursorPosition(10, 10);
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
            Console.SetCursorPosition(10, 12);
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
        ref string userGuessTwo, ref string userGuessThree, ref string userGuessFour)
        {
            Console.Clear();
            System.Console.WriteLine(@" __        __            _ _      
 \ \      / /__  _ __ __| | | ___ 
  \ \ /\ / / _ \| '__/ _` | |/ _ \
   \ V  V / (_) | | | (_| | |  __/
    \_/\_/ \___/|_|  \__,_|_|\___|
        •••••••••••••••••••••
        •
");
            System.Console.WriteLine("\n\n\n\n");
            Console.WriteLine(@"                                        
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 6);
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
            Console.SetCursorPosition(10, 8);
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
            Console.SetCursorPosition(10, 10);
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
            Console.SetCursorPosition(10, 12);
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
        ref userGuessTwo, ref userGuessThree, ref userGuessFour);
        }

        public void AttemptFive(ref string correctWord, ref string[] words, ref string userGuess,
        ref string userGuessTwo, ref string userGuessThree, ref string userGuessFour)
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
        ref userGuessTwo, ref userGuessThree, ref userGuessFour);
                AttemptFive(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour);
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
        ref userGuessTwo, ref userGuessThree, ref userGuessFour);
                                    AttemptFive(ref correctWord, ref words, ref userGuess,
                            ref userGuessTwo, ref userGuessThree, ref userGuessFour);
                                }
                                else
                                {
                                    if (userGuessFive.ToUpper() == correctWord)
                                    {
                                        Console.Clear();
                                        DisplayAttemptFiveCorrect(ref correctWord, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive);
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                        System.Console.Write(@"
        Great! You got it in 5 try's! Tap ENTER to continue the game  ");
                                        Console.ReadLine();
                                        Console.Clear();
                                        PlayAgain();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        DisplayAttemptFive(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive);
                                    }
                                }
                            }
                            else
                            {
                                DisplayAttemptFour(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour);
                                AttemptFive(ref correctWord, ref words, ref userGuess,
                        ref userGuessTwo, ref userGuessThree, ref userGuessFour);
                            }
                        }
                    }
                    else
                    {
                        DisplayAttemptFour(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour);
                        AttemptFive(ref correctWord, ref words, ref userGuess,
                ref userGuessTwo, ref userGuessThree, ref userGuessFour);
                    }
                }
            }
        }

        private void CheckFive(ref string correctWord, ref string userGuessFive)
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
                if (guessLetter == answerLetter)
                {
                    colorWordFive[i] = "G";
                    answerCharCounts[guessLetter]--;
                }
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 0)
                {
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
                        colorWordFive[i] = "Y";
                    }
                    else
                    {
                        colorWordFive[i] = "Y";
                    }
                    answerCharCounts[guessLetter]--;
                }
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 1)
                {
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
                                colorWordFive[i] = "Y";
                                return;
                            }
                        }
                    }
                    if (hasSecondGreen)
                    {
                        colorWordFive[i] = "Y";
                    }
                    else if (hasGreen)
                    {
                        colorWordFive[i] = "G";
                    }
                }
                else
                {
                    colorWordFive[i] = "W";
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
        ref string userGuessTwo, ref string userGuessThree, ref string userGuessFour, ref string userGuessFive)
        {
            Console.Clear();
            System.Console.WriteLine(@" __        __            _ _      
 \ \      / /__  _ __ __| | | ___ 
  \ \ /\ / / _ \| '__/ _` | |/ _ \
   \ V  V / (_) | | | (_| | |  __/
    \_/\_/ \___/|_|  \__,_|_|\___|
        •••••••••••••••••••••
        •
");
            System.Console.WriteLine("\n\n\n\n");
            Console.WriteLine(@"                                       
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 6);
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
            Console.SetCursorPosition(10, 8);
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
            Console.SetCursorPosition(10, 10);
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
            Console.SetCursorPosition(10, 12);
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
            Console.SetCursorPosition(10, 14);
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
        ref string userGuessTwo, ref string userGuessThree, ref string userGuessFour, ref string userGuessFive)
        {
            Console.Clear();
            System.Console.WriteLine(@" __        __            _ _      
 \ \      / /__  _ __ __| | | ___ 
  \ \ /\ / / _ \| '__/ _` | |/ _ \
   \ V  V / (_) | | | (_| | |  __/
    \_/\_/ \___/|_|  \__,_|_|\___|
        •••••••••••••••••••••
        •
");
            System.Console.WriteLine("\n\n\n\n");
            Console.WriteLine(@"                                   
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 6);
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
            Console.SetCursorPosition(10, 8);
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
            Console.SetCursorPosition(10, 10);
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
            Console.SetCursorPosition(10, 12);
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
            Console.SetCursorPosition(10, 14);
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
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive);
        }
        //In this method there will be sligth difference from other attempts 
        public void AttemptSix(ref string correctWord, ref string[] words, ref string userGuess,
        ref string userGuessTwo, ref string userGuessThree, ref string userGuessFour, ref string userGuessFive)
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
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive);
                AttemptSix(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive);
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
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive);
                                    AttemptSix(ref correctWord, ref words, ref userGuess,
                            ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive);
                                }
                                else
                                {
                                    if (userGuessSix.ToUpper() == correctWord)
                                    {
                                        Console.Clear();
                                        DisplayAttemptSixCorrect(ref correctWord, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive, ref userGuessSix);
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                        System.Console.Write(@"
        Phew! You got it in 6 try's! Tap ENTER to continue the game ");
                                        Console.ReadLine();
                                        Console.Clear();
                                        PlayAgain();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        DisplayAttemptSix(ref correctWord, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive, ref userGuessSix);
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        System.Console.Write(@"
        You failed! Tap ENTER to continue  ");
                                        Console.ResetColor();
                                        Console.ReadLine();
                                        Console.Clear();
                                        PlayAgain();
                                    }
                                }
                            }
                            else
                            {
                                DisplayAttemptFive(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive);
                                AttemptSix(ref correctWord, ref words, ref userGuess,
                        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive);
                            }
                        }
                    }
                    else
                    {
                        DisplayAttemptFive(ref correctWord, ref words, ref userGuess,
        ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive);
                        AttemptSix(ref correctWord, ref words, ref userGuess,
                ref userGuessTwo, ref userGuessThree, ref userGuessFour, ref userGuessFive);
                    }
                }
            }
        }

        private void CheckSix(ref string correctWord, ref string userGuessSix)
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
                if (guessLetter == answerLetter)
                {
                    colorWordSix[i] = "G";
                    answerCharCounts[guessLetter]--;
                }
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 0)
                {
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
                        colorWordSix[i] = "Y";
                    }
                    else
                    {
                        colorWordSix[i] = "Y";
                    }
                    answerCharCounts[guessLetter]--;
                }
                else if (answerCharCounts.ContainsKey(guessLetter) && answerCharCounts[guessLetter] > 1)
                {
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
                                colorWordSix[i] = "Y";
                                return;
                            }
                        }
                    }
                    if (hasSecondGreen)
                    {
                        colorWordSix[i] = "Y";
                    }
                    else if (hasGreen)
                    {
                        colorWordSix[i] = "G";
                    }
                }
                else
                {
                    colorWordSix[i] = "W";
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

        public void DisplayAttemptSixCorrect(ref string correctWord, ref string userGuess,
ref string userGuessTwo, ref string userGuessThree, ref string userGuessFour, ref string userGuessFive, ref string userGuessSix)
        {
            Console.Clear();
            System.Console.WriteLine(@" __        __            _ _      
 \ \      / /__  _ __ __| | | ___ 
  \ \ /\ / / _ \| '__/ _` | |/ _ \
   \ V  V / (_) | | | (_| | |  __/
    \_/\_/ \___/|_|  \__,_|_|\___|
        •••••••••••••••••••••
        •
");
            System.Console.WriteLine("\n\n\n\n");
            Console.WriteLine(@"                           
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 6);
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
            Console.SetCursorPosition(10, 8);
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
            Console.SetCursorPosition(10, 10);
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
            Console.SetCursorPosition(10, 12);
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
            Console.SetCursorPosition(10, 14);
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
            Console.SetCursorPosition(10, 16);
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

        public void DisplayAttemptSix(ref string correctWord, ref string userGuess,
ref string userGuessTwo, ref string userGuessThree, ref string userGuessFour, ref string userGuessFive, ref string userGuessSix)
        {
            Console.Clear();
            System.Console.WriteLine(@" __        __            _ _      
 \ \      / /__  _ __ __| | | ___ 
  \ \ /\ / / _ \| '__/ _` | |/ _ \
   \ V  V / (_) | | | (_| | |  __/
    \_/\_/ \___/|_|  \__,_|_|\___|
        •••••••••••••••••••••
        •
");
            System.Console.WriteLine("\n\n\n\n");
            Console.WriteLine(@"                                 
        ••••••••••••••••••••••               
        ");
            Console.SetCursorPosition(10, 6);
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
            Console.SetCursorPosition(10, 8);
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
            Console.SetCursorPosition(10, 10);
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
            Console.SetCursorPosition(10, 12);
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
            Console.SetCursorPosition(10, 14);
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
            Console.SetCursorPosition(10, 16);
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

        //Method below is used to ask users if they want to play again
        public async Task PlayAgain()
        {
            Console.Clear();
            Console.ResetColor();
            Console.WriteLine("Do you want to play again? (1. Yes\t2. No)");
            string userChoice = Console.ReadLine();
            while (userChoice != "2")
            {
                if (userChoice == "1")
                {
                    Console.Clear();
                    GetCorrectWord().Wait(); // Start a new game without exiting the game
                }
                else
                {
                    Console.Clear();
                    PlayAgain();
                }
                Console.Clear();
                Console.ResetColor();
                Console.WriteLine("Do you want to play again? (1. Yes\t2. No)");
                userChoice = Console.ReadLine();
            }
            if (userChoice == "2")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Thank you for playing!");
                Environment.Exit(0);
            }
        }
    }
}



