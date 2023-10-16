namespace ScrabbleGame;

using ScrabbleGame;
using ScrabbleGame.Classes;
using ScrabbleGame.Controller;
using ScrabbleGame.Enums;
using ScrabbleGame.Interface;


public partial class Program
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

		//Initialize Bag and Player Rack
		scrabbleGame.InitializeBag();
		foreach (IPlayer player in scrabbleGame.GetPlayerList())
		{
			scrabbleGame.InitializeRack(player);
		}

		// Deciding initial player
		// Each player take one letter tile randomly, who's the closest to "A" will start first
		Console.WriteLine("\n~ GAME STARTED ~");
		Console.WriteLine("Each player please take a random tile from bag");
		foreach (IPlayer player in scrabbleGame.GetPlayerList())
		{
			Console.WriteLine($"Player {player.GetPlayerID()} get tile {scrabbleGame.GetInitialTile(player)}");
		}
		IPlayer initialPlayer = scrabbleGame.GetInitialPlayer();
		Console.WriteLine($"Player {initialPlayer.GetPlayerID()} has the tile closest to A, starts first!\n");
		
		DisplayBoard(scrabbleGame);
		ShowLetterBag(scrabbleGame);

		// On-going Game
		while (!scrabbleGame.IsGameFinish())
		{
			PlayerTurn:
			IPlayer currentPlayer = scrabbleGame.GetCurrentPlayer();
			Console.WriteLine($"\nPlayer {currentPlayer.GetPlayerID()}'s Turn");
			ShowRack(scrabbleGame, currentPlayer);

			Console.WriteLine($"Player {currentPlayer.GetPlayerID()}, please select your action: ");
			Console.WriteLine("1) Place Letter");
			Console.WriteLine("2) Skip Turn");
			Console.WriteLine("3) Swap Letter");
			Console.WriteLine("4) Resign");
			Console.Write("Action: ");
			bool actionStatus = int.TryParse(Console.ReadLine(), out int playerAction);
			
			if (playerAction == 1)
			{
				PlacingLetter(scrabbleGame, currentPlayer);
			}
			else if (playerAction == 2)
			{
				Console.WriteLine($"Player {currentPlayer.GetPlayerID()} has skipped the turn");
				currentPlayer = scrabbleGame.GetNextPlayer();
				goto PlayerTurn;
			}
			else if (playerAction == 3)
			{
				Console.WriteLine($"Player {currentPlayer.GetPlayerID()} requests to swap letter");
				
				// TODO: if player resign, the next player wins
				throw new NotImplementedException();
			}
			else if (playerAction == 4)
			{
				Console.WriteLine($"Player {currentPlayer.GetPlayerID()} has resigned");
				
				// TODO: if player resign, the next player wins
				throw new NotImplementedException();
			}
			else
			{
				Console.WriteLine("\nNo option, please choose the right option!");
			}

		}

	}
}