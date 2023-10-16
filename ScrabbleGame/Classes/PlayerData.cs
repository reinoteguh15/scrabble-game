namespace ScrabbleGame.Classes;

using ScrabbleGame.Enums;

public class PlayerData
{
	private int _score;
	private char _startingTile;
	private List<char> _rack;
	
	public PlayerData()
	{
		_score = 0;
		
		int startingTileScore = new Random().Next(26);
		if (startingTileScore == 0)
		{
			_startingTile = '?';
		}
		else
		{
			_startingTile = char.Parse(Enum.GetName(typeof(Letter), startingTileScore));
		}
		
		_rack = new();
	}
	
	public int GetScore()
	{
		return _score;
	}
	public char GetStartingTile()
	{
		return _startingTile;
	}
	public List<char> GetRack()
	{
		return _rack;
	}
	public void AddLetter(char letter)
	{
		_rack.Add(letter);
	}
}
