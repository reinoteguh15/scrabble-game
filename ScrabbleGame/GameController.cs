using ScrabbleGame;
using ScrabbleGame.Enum;
using ScrabbleGame.Interface;

public class GameController
{
	private List<Letters>? _listLetters;
	private int _time;
	private int _wordsValue;
	private int _bonus;
	private int _scorePlayer1;
	private int _scorePlayer2;
	
	
	public Board GetBoard()
	{
		Board board = new Board(15);
		return board;
	}
}
