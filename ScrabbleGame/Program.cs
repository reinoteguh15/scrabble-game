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

		// Input player ID & Name
		for (int i = 0; i < scrabbleGame.PlayersCount; i++)
		{
			Console.Write($"Input Player {i + 1} name: ");
			string? name = Console.ReadLine();
			IPlayer player = new Player(name, i);
			scrabbleGame.AddPlayer(player);
		}

		// First Turn
		// Each player take one letter tile randomly, who's the closest to "A" will start first
		scrabbleGame.SetUpBag();
		foreach (IPlayer player in scrabbleGame.GetPlayerList())
		{
			scrabbleGame.SetPlayerRack(player);
		}

		Console.WriteLine("\n~ GAME STARTED ~");
		Console.WriteLine("(GC): Each player please take a random tile from bag");
		IPlayer firstPlayer = scrabbleGame.GetFirstPlayer();
		foreach (IPlayer player in scrabbleGame.GetPlayerList())
		{
			string playerStartingTile = scrabbleGame.GetPlayerStartingTile(player);
			Console.WriteLine($"Player {player.GetPlayerID() + 1} get tile {playerStartingTile}");
		}
		Console.WriteLine($"(GC): Player {firstPlayer.GetPlayerID() + 1} has the tile closest to A, starts first!");


		// TO DO: While game is not finished, looping through this
		IPlayer currentPlayer = scrabbleGame.GetCurrentPlayer();
		Console.WriteLine($"\nPlayer {currentPlayer.GetPlayerID() + 1}'s Turn");
		ShowPlayerRack(scrabbleGame, currentPlayer);
		Console.WriteLine();
		DisplayBoard(scrabbleGame);
		ShowLetterBag(scrabbleGame);

		// TO DO: While submit is not pressed
		PlacingLetter(scrabbleGame, currentPlayer);

		// while end
	}

	public static void ShowPlayerRack(GameController game, IPlayer player)
	{
		Rack playerRack = game.GetPlayerRack(player);
		Console.WriteLine($"Player {player.GetPlayerID() + 1} Rack:");
		var table = new Table();
		foreach (var letter in playerRack.GetRack())
		{
			table.AddColumn(letter);
		}
		AnsiConsole.Write(table);
	}
	public static void ShowLetterBag(GameController gameController)
	{
		var letterBag = gameController.GetRemainingTile();
		int total = 0;
		Console.WriteLine($"\nLetter Bag: ");
		foreach (var keyVP in letterBag)
		{
			Console.Write($"{keyVP.Key} ({keyVP.Value}), ");
			total += keyVP.Value;
		}
		Console.WriteLine();
		Console.WriteLine($"Total: {total} tiles");
	}
	public static void DisplayBoard(GameController gameController)
	{
		Board board = gameController.GetBoard();
		int boardSize = board.GetBoardSize();
		Console.WriteLine("  1   2   3   4   5   6   7   8   9   10  11  12  13  14  15 ");
		Console.WriteLine("+-----------------------------------------------------------+");
		for (int y = 0; y < boardSize; y++)
		{
			for (int x = 0; x < boardSize; x++)
			{
				Position position = new(x, y);
				string letter = board.GetLetter(position) ?? " ";
				Console.Write($"| {letter} ");
			}
			Console.WriteLine($"| {y+1}");
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

				bool letterPlaced = gameController.PlaceLetterToBoard(player, xCoord - 1, yCoord - 1, letterToPlace!);
				if (letterPlaced == true)
				{
					Console.WriteLine($"Letter {letterToPlace} has been placed at ({xCoord},{yCoord}).");
				}
				else
				{
					Console.WriteLine($"Letter {letterToPlace} doesn't exist in your Rack, please choose another letter");
				}

				DisplayBoard(gameController); Console.WriteLine();
				ShowPlayerRack(gameController, player);
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
				Console.Write("Do you want to skip turn? (y/n)");
				string? skipTurn = Console.ReadLine();
				if (skipTurn == "y" || skipTurn == "Y")
				{
					isContinue = false;
				}
				else if (skipTurn == "n" || skipTurn == "N")
				{
					return;
				}
			}

		}
	}
}