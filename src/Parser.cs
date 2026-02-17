using System;

class Parser
{
    // Holds all valid command words
    private readonly CommandLibrary commandLibrary;

    // Constructor
    public Parser()
    {
        commandLibrary = new CommandLibrary();
    }

    // Ask and interpret the user input. Return a Command object.
    public Command GetCommand()
    {
        Console.Write("> ");

        string word1 = null;
        string word2 = null;
        string word3 = null;

        string input = Console.ReadLine();
        string[] words = input.Split(' ');

        if (words.Length > 0)
            word1 = words[0];

        if (words.Length > 1)
            word2 = words[1];

        if (words.Length > 2)
            word3 = words[2];

        // Check if command is valid
        if (commandLibrary.IsValidCommandWord(word1))
        {
            return new Command(word1, word2, word3);
        }

        // Unknown command
        return new Command(null, null, null);
    }

    // Prints a list of valid command words
    public void PrintValidCommands()
    {
        Console.WriteLine("Your command words are:");
        Console.WriteLine(commandLibrary.GetCommandsString());
    }
}
