using ScrabbleGame.Classes;
using ScrabbleGame.Controller;
using ScrabbleGame.Enums;
using ScrabbleGame.Interface;
using Spectre.Console;

class Program
{
	static void Main()
	{
		Console.WriteLine("Welcome to C# Scrabble Console Game!");
		Console.WriteLine("====================================");
		GameController scrabbleGame = new(2);	
		
		// Input player Name, ID is set by GameController
		foreach (IPlayer player in scrabbleGame.GetPlayerList())
		{
			Console.Write($"Input Player {player.GetPlayerID()} name: ");
			string? name = Console.ReadLine();
			player.SetPlayerName(name);
		}
		
		// Deciding initial player
		// Each player take one letter tile randomly, who's the closest to "A" will start first
		scrabbleGame.InitializeBag();
		foreach (IPlayer player in scrabbleGame.GetPlayerList())
		{
			scrabbleGame.InitializeRack(player);
		}
		
		Console.WriteLine("\n~ GAME STARTED ~");
		Console.WriteLine("GC: Each player please take a random tile from bag");
		foreach (IPlayer player in scrabbleGame.GetPlayerList())
		{
			Console.WriteLine($"Player {player.GetPlayerID()} get tile {scrabbleGame.GetInitialTile(player)}");
		}
		IPlayer initialPlayer = scrabbleGame.GetInitialPlayer();
		Console.WriteLine($"GC: Player {initialPlayer.GetPlayerID()} has the tile closest to A, starts first!");
		
		
		// while()
		IPlayer currentPlayer = scrabbleGame.GetCurrentPlayer();
		Console.WriteLine($"\nPlayer {currentPlayer.GetPlayerID()}'s Turn");
		ShowRack(scrabbleGame, currentPlayer);
		DisplayBoard(scrabbleGame);
		ShowLetterBag(scrabbleGame);
		
		PlacingLetter(scrabbleGame, currentPlayer);
		
	}
	
	public static void ShowRack(GameController gameController, IPlayer player)
	{
		List<string> playerRack = gameController.GetPlayerRack(player);
		Console.WriteLine($"Player {player.GetPlayerID()} Rack:");
		var table = new Table();
		foreach(var letter in playerRack)
		{
			table.AddColumn(letter);
		}
		AnsiConsole.Write(table);
		Console.WriteLine();
	}
	public static void ShowLetterBag(GameController gameController)
	{
		var letterBag = gameController.GetRemainingTile();
		Console.WriteLine("\nLetter Bag:");
		foreach(var keyVP in letterBag)
		{
			Console.Write($"{keyVP.Key} ({keyVP.Value}), ");
		}
	}
	public static void DisplayBoard(GameController gameController)
	{
		int boardSize = gameController.GetBoardSize();
		Console.WriteLine("+-----------------------------------------------------------+");
		for (int y = 0; y < boardSize; y++)
		{
			for (int x = 0; x < boardSize; x++)
			{
				Position position = new(x, y);
				string letter = gameController.GetBoardLetter(position) ?? " ";
				Console.Write($"| {letter} ");
			}
			Console.WriteLine("|");
			Console.WriteLine("+-----------------------------------------------------------+");
		}
	}
	
	public static void PlacingLetter(GameController gameController, IPlayer player)
	{
		bool isContinue = true;
		while (isContinue)
		{
			Console.WriteLine();
			Console.Write($"(GC): Player {player.GetPlayerID() + 1}, do you want to place the letter to board? (y/n): ");
			string? wantToPlace = Console.ReadLine();
			if (wantToPlace == "y")
			{
				PlaceLetter:
				Console.Write($"(GC): Player {player.GetPlayerID() + 1}, please enter the letter you want to place: ");
				string? letterToPlace = Console.ReadLine();
				Console.Write($"(GC): Enter the x coordinate: ");
				bool parseX = int.TryParse(Console.ReadLine(), out int xCoord);
				Console.Write($"(GC): Enter the y coordinate: ");
				bool parseY = int.TryParse(Console.ReadLine(), out int yCoord);

				bool letterPlaced = gameController.PlaceLetter(player, xCoord - 1, yCoord - 1, letterToPlace!);
				if (letterPlaced == true)
				{
					Console.WriteLine($"Letter {letterToPlace} has been placed at ({xCoord},{yCoord}).");
				}
				else
				{
					Console.WriteLine($"Letter {letterToPlace} doesn't exist in your Rack, please choose another letter");
				}

				DisplayBoard(gameController); Console.WriteLine();
				ShowRack(gameController, player);
				ShowLetterBag(gameController);

				Console.Write("\nDo you want to place the letter again? (y/n): ");
				string? playerContinue = Console.ReadLine();
				if (playerContinue == "y" || playerContinue == "Y")
				{
					goto PlaceLetter;
				}
				else if (playerContinue == "n" || playerContinue == "N")
				{
					isContinue = false;
					Console.Write("Do you want to submit the words? (y/n): ");
					string? isSubmit = Console.ReadLine();
					if (isSubmit == "y" || isSubmit == "Y")
					{
						throw new NotImplementedException();
					}
					else if (isSubmit == "n" || isSubmit == "N")
					{
						throw new NotImplementedException();
					}
				}
				else
				{
					Console.WriteLine("No option, please choose y/n");
					return;
				}
			}
			else
			{
				Console.Write("Do you want to skip turn? (y/n): ");
				string? skipTurn = Console.ReadLine();
				if (skipTurn == "y" || skipTurn == "Y")
				{
					Console.WriteLine($"\nPlayer {player.GetPlayerID()+1} has skipped turn");
					return;
				}
				else if (skipTurn == "n" || skipTurn == "N")
				{
					return;
				}
			}

		}
	}
}