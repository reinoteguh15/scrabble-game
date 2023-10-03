namespace ScrabbleGame;

public class Board
{
	private int _boardSize;
	private char[,]? _board;
	
	public Board(int size)
	{
		_boardSize = size;
		_board = new char[size,size];
	}
	public char[,]? CreateBoard()
	{
		return _board;
	}
}
