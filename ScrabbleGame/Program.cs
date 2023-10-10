using System.ComponentModel.Design.Serialization;
using ScrabbleGame.Classes;
using ScrabbleGame.Controller;
using ScrabbleGame.Interface;

class Program
{
	static void Main()
	{
		Console.WriteLine("Welcome to C# Scrabble Console Game!");
		Console.WriteLine("====================================");
		GameController scrabbleGame = new();	
		
		IPlayer player1 = new Player("Player 1", 1);
		IPlayer player2 = new Player("Player 2", 2);
		scrabbleGame.AddPlayer(player1);
		scrabbleGame.AddPlayer(player2);
		
		scrabbleGame.AddRackToPlayer(player1);
		scrabbleGame.AddRackToPlayer(player2);
		
		List<string> player1Rack = scrabbleGame.GetPlayerRack(player1);
		Console.WriteLine($"{player1.GetPlayerName()} Rack:");
		foreach(string letter in player1Rack)
		{
			Console.Write($"{letter} ");
		}
		
		List<string> player2Rack = scrabbleGame.GetPlayerRack(player2);
		Console.WriteLine($"\n\n{player2.GetPlayerName()} Rack:");
		foreach(string letter in player2Rack)
		{
			Console.Write($"{letter} ");
		}
		
		
	}
}