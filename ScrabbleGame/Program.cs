using ScrabbleGame;

class Program
{
	static void Main()
	{
		GameController game = new();
		int boardSize = game.GetBoardSize();
		
		Console.WriteLine("-------------------------------------------------------------");
		for (int y = 0; y < boardSize; y++)
		{
			for (int x = 0; x < boardSize; x++)
			{
				Console.Write("| a ");
			}
			Console.WriteLine("|");
			Console.WriteLine("-------------------------------------------------------------");
		}
		
		
	}
}
