namespace ScrabbleGame;

using ScrabbleGame.Classes;
using ScrabbleGame.Controller;
using ScrabbleGame.Enums;
using ScrabbleGame.Interface;
using System.Text;
using Spectre.Console;

public partial class Program
{
	static List<Position> positions = new();
	static StringBuilder sb = new();

	static void ShowRack(GameController gameController, IPlayer player)
	{
		List<char> playerRack = gameController.GetPlayerRack(player);
		Console.WriteLine($"Player {player.GetPlayerID()} Rack:");
		var table = new Table();
		foreach (var letter in playerRack)
		{
			table.AddColumn(letter.ToString());
		}
		AnsiConsole.Write(table);
	}
	static void ShowLetterBag(GameController gameController)
	{
		var letterBag = gameController.GetRemainingTile();
		Console.WriteLine("\nLetter Bag:");
		foreach (var keyVP in letterBag)
		{
			Console.Write($"{keyVP.Key} ({keyVP.Value}), ");
		}
		Console.WriteLine("\n");
	}
	static void DisplayBoard(GameController gameController)
	{
		int boardSize = gameController.GetBoardSize();
		Console.WriteLine("\t  1   2   3   4   5   6   7   8   9  10  11  12  13  14  15  ");
		Console.WriteLine("\t+-----------------------------------------------------------+");
		for (int y = 0; y < boardSize; y++)
		{
			Console.Write($"{y + 1}\t");
			for (int x = 0; x < boardSize; x++)
			{
				Position position = new(x, y);

				char letter;
				if (gameController.GetBoardLetter(position) == '\0')
				{
					letter = (char)32;
				}
				else
				{
					letter = gameController.GetBoardLetter(position);
				}

				Console.Write($"| {letter} ");
			}
			Console.WriteLine("|");
			Console.WriteLine("\t+-----------------------------------------------------------+");
		}
	}

	static void PutLetter(GameController gameController, IPlayer player)
	{
		Console.Write($"(GC): Player {player.GetPlayerID()}, please enter the letter you want to place: ");
		char letterToPlace = Console.ReadKey().KeyChar;
		Console.WriteLine();

		if (gameController.GetPlayerRack(player).Contains(letterToPlace))
		{

			Console.Write($"(GC): Enter the x coordinate: ");
			bool parseX = int.TryParse(Console.ReadLine(), out int xCoord);

			Console.Write($"(GC): Enter the y coordinate: ");
			bool parseY = int.TryParse(Console.ReadLine(), out int yCoord);

			bool letterPlaced = gameController.PlaceLetter(player, (xCoord - 1), (yCoord - 1), letterToPlace);
			if (letterPlaced == true)
			{
				Console.WriteLine($"Letter {letterToPlace} has been placed at ({xCoord},{yCoord}).");
				Position position = new Position(xCoord - 1, yCoord - 1);
				positions.Add(position);
				sb.Append(letterToPlace);

				DisplayBoard(gameController);
			}
			else
			{
				Console.WriteLine($"Tile ({xCoord},{yCoord}) has any letter in it.");
			}
		}
		else
		{
			Console.WriteLine($"Letter {letterToPlace} doesn't exist in your Rack, please choose another letter");
		}
	}

	static void SubmitLetter(GameController gameController, IPlayer player)
	{
		// TODO: evaluate the words
		Console.WriteLine($"Player {player.GetPlayerID()} has submitted the word");
		Console.WriteLine("Evaluating the word...");

		Console.WriteLine(gameController.IsValidMove(positions));

		int playerPoint = gameController.EvaluateWords(positions, sb.ToString());

		throw new NotImplementedException();
	}
	static void PlacingLetter(GameController gameController, IPlayer player)
	{
		while (!gameController.IsGameFinish())
		{
			bool isTurnFinished = false;

			while (!isTurnFinished)
			{
			PlaceLetter:
				PutLetter(gameController, player);
				
			PlaceLetterAgain:
				Console.Write("\nDo you want to place the letter again? (y/n): ");
				string? playerContinue = Console.ReadLine();

				if (playerContinue == "y" || playerContinue == "Y")
				{
					goto PlaceLetter;
				}
				else if (playerContinue == "n" || playerContinue == "N")
				{
					isTurnFinished = true;
					Console.Write("Do you want to submit the words? (y/n): ");
					string? isSubmit = Console.ReadLine();
					if (isSubmit == "y" || isSubmit == "Y")
					{
						SubmitLetter(gameController, player);
					}
					else if (isSubmit == "n" || isSubmit == "N")
					{
						goto PlaceLetterAgain;
					}
				}
				else
				{
					Console.WriteLine("No option, please choose y/n");
				}

			}

		}
	}
}
