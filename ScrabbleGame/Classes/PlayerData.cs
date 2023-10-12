namespace ScrabbleGame.Classes;

using ScrabbleGame.Enums;

public class PlayerData
{
	private int _score;
	private string? _startingTile;
	private List<string> _rack;
	
	public PlayerData()
	{
		_score = 0;
		
		int startingTileScore = new Random().Next(26);
		_startingTile = Enum.GetName(typeof(Letter), startingTileScore);
		
		_rack = new();
	}
	
	public int GetScore()
	{
		return _score;
	}
	public string? GetStartingTile()
	{
		return _startingTile;
	}
	public List<string> GetRack()
	{
		return _rack;
	}
	public void AddLetter(string letter)
	{
		_rack.Add(letter);
	}
}
