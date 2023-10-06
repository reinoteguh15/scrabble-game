namespace ScrabbleGame;

public class Board
{
	private int _boardSize;
	private char[,]? _boardLetters;
	
	public Board(int size)
	{
		_boardSize = size;
		_boardLetters = new char[size,size];
	}
	public int GetBoardSize()
	{
		return _boardSize;
	}
}
