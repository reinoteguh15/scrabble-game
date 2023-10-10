using ScrabbleGame;
using ScrabbleGame.Enum;
using ScrabbleGame.Interface;

public class GameController
{
	private Board scrabbleBoard;
	
	public GameController()
	{
		scrabbleBoard = new Board(15);
	}
	
	public int GetBoardSize()
	{
		return scrabbleBoard.GetBoardSize();
	} 
}