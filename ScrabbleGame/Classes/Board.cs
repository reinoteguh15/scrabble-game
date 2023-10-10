namespace ScrabbleGame.Classes;

using ScrabbleGame.Enums;

public class Board
{
	private int _boardSize;
	private string[,] _boardLetters;
	private Dictionary<Position, Bonus> _boardBonus;
	
	public Board(int boardSize)
	{
		_boardSize = boardSize;
		_boardLetters = new string[_boardSize, _boardSize];
		_boardBonus = new();
	}
	
	public int GetBoardSize()
	{
		return _boardSize;
	}
	
	public string GetLetter(Position position)
	{
		return _boardLetters[position.GetCoordinateX(), position.GetCoordinateY()];
	}
	
	public void PlaceLetter(string letter, Position position)
	{
		_boardLetters[position.GetCoordinateX(), position.GetCoordinateY()] = letter;
	}
}
