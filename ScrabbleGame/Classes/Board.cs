namespace ScrabbleGame.Classes;

using ScrabbleGame.Enums;

public class Board
{
	private int _boardSize;
	private char[,] _boardLetters;
	private Dictionary<Position, Bonus> _boardBonus;
	
	public Board(int boardSize)
	{
		_boardSize = boardSize;
		_boardLetters = new char[_boardSize, _boardSize];
		_boardBonus = new();
	}
	
	public int GetSize()
	{
		return _boardSize;
	}
	
	public void SetUp()
	{
		List<Position> tripleWord = new()
		{
			new Position(1, 1), new Position(1, 8), new Position(1, 15),
			new Position(8, 1), new Position(8, 15),
			new Position(15, 1), new Position(15, 8), new Position(15, 15)
		};
		foreach (Position pos in tripleWord)
		{
			_boardBonus.Add(pos, Bonus.TripleWord);
		}
		
		List<Position> doubleWord = new()
		{
			new Position(2, 2), new Position(3, 3), new Position(4, 4), new Position(5, 5), new Position(8, 8),
			new Position(2, 14), new Position(3, 13), new Position(4, 12), new Position(5, 11),
			new Position(11, 11), new Position(12, 12),new Position(13, 13), new Position(14, 14),
			new Position(11, 5), new Position(12, 4), new Position(13, 3), new Position(14, 2)
		};
		foreach (Position pos in doubleWord)
		{
			_boardBonus.Add(pos, Bonus.DoubleWord);
		}
		
		List<Position> tripleLetter = new()
		{
			new Position(2, 6), new Position(2, 10), new Position(14, 6), new Position(14, 10),
			new Position(6, 2), new Position(6, 6), new Position(6, 10), new Position(6, 14),
			new Position(10, 2), new Position(10, 6), new Position(10, 10), new Position(10, 14)
		};
		foreach (Position pos in tripleLetter)
		{
			_boardBonus.Add(pos, Bonus.TripleLetter);
		}
		
		List<Position> doubleLetter = new()
		{
			new Position(1, 4), new Position(1, 12), new Position(15, 4), new Position(15, 12),
			new Position(3, 7), new Position(3, 9), new Position(13, 7), new Position(13, 9),
			new Position(4, 1), new Position(4, 8), new Position(4, 15), new Position(12, 1), new Position(12, 8), new Position(12, 15),
			new Position(7, 3), new Position(7, 7), new Position(7, 9), new Position(7, 13),
			new Position(9, 3), new Position(9, 7), new Position(9, 9), new Position(9, 13),
			new Position(8, 4), new Position(8,12)
		};
		foreach (Position pos in doubleLetter)
		{
			_boardBonus.Add(pos, Bonus.DoubleLetter);
		}
	}
	public char GetLetter(Position position)
	{
		return _boardLetters[position.GetCoordinateX(), position.GetCoordinateY()];
	}
	
	public void PlaceLetter(char letter, Position position)
	{
		_boardLetters[position.GetCoordinateX(), position.GetCoordinateY()] = letter;
	}
	public void RemoveLetter(Position position)
	{
		_boardLetters[position.GetCoordinateX(), position.GetCoordinateY()] = (char)32;
	}
	
	public Bonus GetPositionBonus(Position position)
	{
		return _boardBonus[position];
	}
}
