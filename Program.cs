using System;
using System.Collections.Generic;

class Hangman
{
    // List of words to be guessed
    private static List<string> WordList;

    // The word to be guessed
    private readonly string secretWord;

    // Set of guessed letters
    private HashSet<char> guessedLetters;

    // Maximum number of attempts allowed
    private readonly int maxAttempts;

    // Number of remaining attempts
    private int remainingAttempts;

    // Constructor to initialize the game
    public Hangman(int attempts, string filePath) // Modify the constructor to accept a file path
    {
        // Read words from the text file
        WordList = new List<string>(File.ReadAllLines(filePath));

        Random random = new Random();
        secretWord = WordList[random.Next(WordList.Count)]; // Select a random word from the WordList
        maxAttempts = attempts;
        guessedLetters = new HashSet<char>();
        remainingAttempts = maxAttempts;
    }

    // Method to handle a guess
    public void Guess(char letter)
    {
        letter = char.ToLower(letter); // Convert the guessed letter to lowercase
        if (!guessedLetters.Contains(letter)) // Check if the letter has already been guessed
        {
            guessedLetters.Add(letter); // Add the guessed letter to the set of guessed letters
            if (!secretWord.Contains(letter)) // Check if the guessed letter is not in the secret word
            {
                remainingAttempts--; // Decrement the remaining attempts if the guessed letter is not in the secret word
            }
        }
        else
        {
            Console.WriteLine($"You have already guessed the letter '{letter}'. Please enter a different letter."); // Notify the user if the letter has already been guessed
        }
    }

    // Method to check if the game is over
    public bool IsGameOver()
    {
        return remainingAttempts <= 0 || GetDisplayWord() == secretWord; // Check if the player has run out of attempts or has guessed the word
    }

    // Method to get the display word (revealed and hidden letters)
    public string GetDisplayWord()
    {
        string displayWord = "";
        foreach (char letter in secretWord)
        {
            if (guessedLetters.Contains(letter) || guessedLetters.Contains(char.ToLower(letter))) // Check if the letter has been guessed
            {
                displayWord += letter; // Add the letter if it has been guessed
            }
            else if (char.IsLetter(letter))
            {
                displayWord += "_ "; // Add an underscore for hidden letters
            }
            else
            {
                displayWord += letter; // Keep non-letter characters (like spaces) unchanged
            }
        }
        return displayWord; // Return the display word
    }

    // Method to display the current game status
    public void DisplayGameStatus()
    {
        Console.WriteLine("Current word: " + GetDisplayWord()); // Display the current state of the word
        Console.WriteLine($"Attempts remaining: {remainingAttempts}/{maxAttempts}"); // Display the remaining attempts
    }

    // Method to display the outcome of the game
    public void DisplayOutcome()
    {
        if (GetDisplayWord() == secretWord) // Check if the player has guessed the word
        {
            Console.WriteLine("Congratulations! You've guessed the word: " + secretWord); // Display a congratulatory message
        }
        else
        {
            Console.WriteLine("You lose! The word was: " + secretWord); // Display the secret word if the player has run out of attempts
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        int maxAttempts = 6; // Set the maximum number of attempts

        string filePath = "C:/Users/slevi/OneDrive/Desktop/wordlist.txt";

        Hangman game = new Hangman(maxAttempts, filePath); // Create a new Hangman game

        Console.WriteLine("Welcome to Hangman!"); // Display a welcome message

        while (!game.IsGameOver()) // Continue the game until it's over
        {
            Console.WriteLine();
            game.DisplayGameStatus(); // Display the current game status

            Console.Write("Enter a letter to guess: ");
            char guess = Console.ReadKey().KeyChar; // Get a letter guess from the user
            Console.WriteLine();

            game.Guess(guess); // Process the guess
        }

        Console.WriteLine();
        game.DisplayOutcome(); // Display the outcome of the game
    }
}
