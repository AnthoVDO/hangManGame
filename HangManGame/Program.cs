using System;
using System.Collections.Generic;
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

        static char AskALetter()
        {
            // Add a letter
            // if more than a letter
            // return => Capital letter
            Console.Write("Please add a letter: ");
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
                    Console.WriteLine("This letter is not inside the word");
                    if (!wrongLetters.Contains(letter))
                    {
                        wrongLetters.Add(letter);
                    }
                    
                    
                    lifeLeft--;
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
        static void Main(string[] args)
        {
            string word = "Elephant";
            word = word.ToUpper();
            var letterLists = new List<char> {};
            guessWord(word, letterLists);
        
        }
    }
}
