using ScrabbleGame.Interface;

namespace ScrabbleGame;

public class Player : IPlayer
{
	private string? _playerName;
	private int _playerID;
	
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
