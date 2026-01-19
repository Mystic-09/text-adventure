using System;

class Game
{
	// Private fields
	private Parser parser;
	private Player player;

	// Constructor
	public Game()
	{
		parser = new Parser();
		player = new Player();
		CreateRooms();
	}

	// Initialise the Rooms (and the Items)
	private void CreateRooms()
	{
		// Create the rooms
		Room outside = new Room("outside the main entrance of the university");
		Room theatre = new Room("in a lecture theatre");
		Room pub = new Room("in the campus pub");
		Room lab = new Room("in a computing lab");
		Room office = new Room("in the computing admin office");
		Room test = new Room("in a test room");
		Room theatreUpstairs = new Room("upstairs in the lecture theatre");
		Room labBasement = new Room("in the lab basement");

		// Initialise room exits
		outside.AddExit("east", theatre);
		outside.AddExit("south", lab);
		outside.AddExit("west", pub);
		test.AddExit("weg", outside);

		//up down exits
		theatre.AddExit("up", theatreUpstairs);
		theatreUpstairs.AddExit("down", theatre);

		lab.AddExit("down", labBasement);
		labBasement.AddExit("up", lab);
		//other exits

		theatre.AddExit("west", outside);

		pub.AddExit("east", outside);

		lab.AddExit("north", outside);
		lab.AddExit("east", office);

		office.AddExit("west", lab);

		// Create your Items here
		Item key = new Item(1, "a key");
		Item sword = new Item(5, "a sword");
		Item potion = new Item(2, "a healing potion");

		// And add them to the Rooms
		outside.Chest.Put("key", key);
		lab.Chest.Put("sword", sword);
		theatre.Chest.Put("potion", potion);

		
		// Start game outside
		player.CurrentRoom = outside;
	}

	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		bool finished = false;
		while (!finished)
		{
			// Check if player is alive
			if (!player.IsAlive())
			{
				Console.WriteLine("You have died!");
				break;
			}

			Command command = parser.GetCommand();
			finished = ProcessCommand(command);
		}
		Console.WriteLine("Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	}

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if(command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
		}

		switch (command.CommandWord)
		{
			case "help":
				PrintHelp();
				break;
			case "go":
				GoRoom(command);
				break;
			case "quit":
				wantToQuit = true;
				break;
			case "look":
				PrintLook();
				break;
			case "status":
				PrintStatus();
				break;
			case "take":
    			player.TakeFromChest(command.SecondWord);
    			break;
			case "drop":
    			player.DropToChest(command.SecondWord);
    			break;

		}

		return wantToQuit;
	}

	// ######################################
	// implementations of user commands:
	// ######################################
	
	// Print out some help information.
	// Here we print the mission and a list of the command words.


	private void PrintLook()
	{
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}

	private void PrintStatus()
	{
		Console.WriteLine("You have " + player.GetHealth() + " health.");
		Console.WriteLine("Health: " + player.GetHealth());
    	Console.WriteLine("Inventory: " + player.ShowInventory());
	}

	private void PrintHelp()
	{
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You wander around at the university.");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
	private void GoRoom(Command command)
	{
		if(!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;

		// Try to go to the next room.
		Room nextRoom = player.CurrentRoom.GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to "+direction+"!");
			return;
		}

		player.CurrentRoom = nextRoom;
		Console.WriteLine(player.CurrentRoom.GetLongDescription());


		// Player damage
		player.Damage(10);


		// Kijk of de speler het einde heeft bereikt
		if (player.CurrentRoom.GetLongDescription() == "You are in the office.")
		{
    	Console.WriteLine("You have reached the office. You win!");
    	Environment.Exit(0);
		}
	}
}
