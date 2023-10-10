using System.ComponentModel.Design.Serialization;
using ScrabbleGame.Classes;
using ScrabbleGame.Controller;
using ScrabbleGame.Interface;
using Spectre.Console;

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
			
		scrabbleGame.SetUpBag();
		
		scrabbleGame.AddRackToPlayer(player1);
		scrabbleGame.AddRackToPlayer(player2);
		
		List<string> player1Rack = scrabbleGame.GetPlayerRack(player1);
		Console.WriteLine($"{player1.GetPlayerName()} Rack:");
		var table = new Table();
		foreach(var letter in player1Rack)
		{
			table.AddColumn(letter);
		}
		AnsiConsole.Write(table);
		
		List<string> player2Rack = scrabbleGame.GetPlayerRack(player2);
		Console.WriteLine($"\n{player2.GetPlayerName()} Rack:");
		var table2 = new Table();
		foreach(string letter in player2Rack)
		{
			table2.AddColumn(letter);
		}
		AnsiConsole.Write(table2);
		
		var letterBag = scrabbleGame.GetRemainingTile();
		Console.WriteLine("\nLetter Bag:");
		foreach(var keyVP in letterBag)
		{
			Console.Write($"{keyVP.Key}({keyVP.Value}), ");
		}
		
		
	}
}