using ScrabbleGame.Interface;
namespace ScrabbleGame.Classes;

public class Player : IPlayer
{
	private string? _playerName;
	private int _playerID;
	
	// Constructor
	public Player(string? name, int id)
	{
		_playerName = name;
		_playerID = id;
	}
	
	// Method
	public void SetPlayerName(string? playerName)
	{
		_playerName = playerName;
	}
	public void SetPlayerID(int playerID)
	{
		_playerID = playerID;
	}
	public string? GetPlayerName()
	{
		return _playerName;
	}
	public int GetPlayerID()
	{
		return _playerID;
	}
}
