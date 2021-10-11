using System;
using System.Collections.Generic;
using System.IO;
using AsciiArt;

namespace HangManGame
{
    class Program
    {   
        static void ShowWord(string word, List<char> letters)
        {
            for(int i=0; i<word.Length; i++)
            {
                if (letters.Contains(word[i]))
                {
                    Console.Write(word[i]+" ");
                }
                else if(i == word.Length - 1)
                {
                    Console.Write('_');
                }
                else
                {
                    Console.Write("_ ");
                }
            }
            Console.WriteLine("");
        }

        static char AskALetter(string sentence ="Please add a letter: ")
        {
            // Add a letter
            // if more than a letter
            // return => Capital letter
            Console.Write(sentence);
            var inputUser = Console.ReadLine();
            if (inputUser.Length > 1 ||inputUser.Length<1)
            {
                Console.WriteLine("Error: you need to add only one letter");
                return AskALetter();
            }
            else
            {
                char letter = Convert.ToChar(inputUser.ToUpper());
                return letter;
            }

        }

        static bool allGuessedLetters(string word, List<char> letters)
        {
            int count = 0;
            for(int i=0; i<word.Length; i++)
            {
                if (letters.Contains(word[i]))
                {
                    count++;
                }
            }

            if(count == word.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void guessWord(string word, List<char> letters)
        {
            // loop(true)
            // show the word
            // ask a letter to the user
            // letter inside the word
            // letter not in the word
            // add life

            const int LIFES_NUMBER = 6;
            int lifeLeft = LIFES_NUMBER;
            var wrongLetters = new List<char> {};

            while (lifeLeft>0)
            {
                Console.WriteLine(Ascii.PENDU[LIFES_NUMBER-lifeLeft]);
                Console.WriteLine();
                Console.WriteLine($"Life(s) left: {lifeLeft}");
                ShowWord(word, letters);
                char letter = AskALetter();
                
                Console.Clear();
                
                if (word.Contains(letter))
                {
                    letters.Add(letter);
                    Console.WriteLine("This letter is inside the word");
                    bool gameCompleted = allGuessedLetters(word, letters);
                    if (gameCompleted)
                    {
                        Console.Clear();
                        Console.WriteLine($"Life(s) left: {lifeLeft}");
                        ShowWord(word, letters);
                        Console.WriteLine();
                        Console.WriteLine("---- Congratulation, you guessed the word ----");
                        return;
                    }
                }
                else
                {
                    
                    if (!wrongLetters.Contains(letter))
                    {
                        Console.WriteLine("This letter is not inside the word");
                        wrongLetters.Add(letter);
                        lifeLeft--;
                    }
                       
                }

                if (lifeLeft == 0)
                {
                    Console.WriteLine($"YOU LOOSE, the word to find was: {word}");
                }

                if (wrongLetters.Count > 0)
                {
                    Console.WriteLine("Letters tested: "+String.Join(", ", wrongLetters));
                }
                

                
            }
        }

        static string[] LoadTheWords(string fileName)
        {
            try
            {
                return File.ReadAllLines(fileName);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR: Failed to read the file {fileName} : {ex.Message}");
            }
            return null;
        }


        static string RandomWord(string[] wordList)
        {
            var rand = new Random();
            int randomNumber = rand.Next(wordList.Length);
            return wordList[randomNumber].Trim().ToUpper();
        }

        static char TryAgain()
        {
            Console.Write("Try again ? ");
            char tryAgain = AskALetter("");
            if((tryAgain == 'Y')||(tryAgain == 'N'))
            {
                return tryAgain;
            }else
            {
                Console.WriteLine("You need to enter Y or N");
                return TryAgain();
            }
        }
        static void Main(string[] args)
        {
            while (true)
            {
                var wordsList = LoadTheWords("mots.txt");
                if ((wordsList == null) || (wordsList.Length == 0))
                {
                    Console.WriteLine("The word list is empty");
                }
                else
                {
                    string word = RandomWord(wordsList);

                    var letterLists = new List<char> { };
                    guessWord(word, letterLists);
                }

                // Would you continue ? y/n ?
                char answer = TryAgain();
                if (answer == 'N')
                {
                    Console.WriteLine("Thank you for the game, see you next time !");
                    break;

                }
                else
                {
                    Console.Clear();
                }
            }
            
        
        }
    }
}
