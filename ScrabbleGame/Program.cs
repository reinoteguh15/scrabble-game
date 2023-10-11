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
			Console.Write($"Input Player {i+1} name: ");
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
		Console.WriteLine("GC: Each player please take a random tile from bag");
		IPlayer firstPlayer = scrabbleGame.GetFirstPlayer();
		foreach (IPlayer player in scrabbleGame.GetPlayerList())
		{
			string playerStartingTile = scrabbleGame.GetPlayerStartingTile(player);
			Console.WriteLine($"Player {player.GetPlayerID() + 1} get tile {playerStartingTile}");
		}
		Console.WriteLine($"GC: Player {firstPlayer.GetPlayerID() + 1} has the tile closest to A, starts first!");
		
		
		// while()
		IPlayer currentPlayer = scrabbleGame.GetCurrentPlayer();
		Console.WriteLine($"\nPlayer {currentPlayer.GetPlayerID() + 1}'s Turn");
		ShowPlayerRack(scrabbleGame, currentPlayer);
				
		Console.WriteLine();
		DisplayBoard(scrabbleGame);
		ShowLetterBag(scrabbleGame);
		 
		
	}
	
	public static void ShowPlayerRack(GameController game, IPlayer player)
	{
		List<string> playerRack = game.GetPlayerRack(player);
		Console.WriteLine($"Player {player.GetPlayerID()+1} Rack:");
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
		Board board = gameController.GetBoard();
		int boardSize = board.GetBoardSize();
		Console.WriteLine("+-----------------------------------------------------------+");
		for (int y = 0; y < boardSize; y++)
		{
			for (int x = 0; x < boardSize; x++)
			{
				Position position = new(x, y);
				string letter = board.GetLetter(position) ?? " ";
				Console.Write($"| {letter} ");
			}
			Console.WriteLine("|");
			Console.WriteLine("+-----------------------------------------------------------+");
		}
	}
}