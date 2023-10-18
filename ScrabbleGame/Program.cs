namespace ScrabbleGame;

using ScrabbleGame;
using ScrabbleGame.Classes;
using ScrabbleGame.Controller;
using ScrabbleGame.Enums;
using ScrabbleGame.Interface;
using System.Text;


public partial class Program
{
	static IPlayer currentPlayer;
	static List<Position> positions = new();
	static StringBuilder sb = new();
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

		// Initialize Bag and Player Rack
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
			while(!scrabbleGame.IsPlayerTurnFinish(scrabbleGame.GetCurrentPlayer()))
			{
				currentPlayer = scrabbleGame.GetCurrentPlayer();
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
					scrabbleGame.SetPlayerStatus(currentPlayer, PlayerStatus.SkipTurn);
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
					scrabbleGame.SetPlayerStatus(currentPlayer, PlayerStatus.Resign);
					
					// TODO: if player resign, the next player wins
					throw new NotImplementedException();
				}
				else
				{
					Console.WriteLine("\nNo option, please choose the right option!");
				}
				
				scrabbleGame.SetPlayerStatus(currentPlayer, PlayerStatus.EndTurn);		
			}
			
		}

	}
	
	static void PlacingLetter(GameController gameController, IPlayer player)
	{
		bool isFinishedPlacing = false;
		while (!isFinishedPlacing)
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
				isFinishedPlacing = true;
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
			currentPlayer = gameController.GetNextPlayer();
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

			bool letterPlaced = gameController.PlaceLetter(player, xCoord - 1, yCoord - 1, letterToPlace);
			if (letterPlaced == true)
			{
				Console.WriteLine($"Letter {letterToPlace} has been placed at ({xCoord},{yCoord}).");
				Position position = new Position(xCoord, yCoord);
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
		Console.WriteLine($"\nPlayer {player.GetPlayerID()} has submitted the word");
		Console.WriteLine("Evaluating the word...");
		
		while (!gameController.IsValidMove(positions))
		{
			Console.WriteLine("Move invalid! Please check the placement of your letter tile\n");
			foreach (Position pos in positions)
			{
				gameController.ReturnLetterToRack(player, positions);
				
			}			
			PutLetter(gameController, player);
		}
				
		Console.WriteLine("Move is valid !");
		
		int playerPoint = gameController.EvaluateWords(player, positions, sb.ToString());
		Console.WriteLine($"\nPlayer {player.GetPlayerID()}'s score: {playerPoint}");
		gameController.SetPlayerStatus(player, PlayerStatus.EndTurn);
	}
}