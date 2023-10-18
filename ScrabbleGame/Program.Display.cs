namespace ScrabbleGame;

using ScrabbleGame.Classes;
using ScrabbleGame.Controller;
using ScrabbleGame.Enums;
using ScrabbleGame.Interface;
using Spectre.Console;

public partial class Program
{
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
				Position position = new (x, y);

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
}
