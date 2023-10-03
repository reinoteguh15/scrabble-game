namespace ScrabbleGame.Interface;

public interface IPlayer
{
	public string? GetPlayerName();
	public int GetPlayerID();
	public void SetPlayerName(string? name);
	public void SetPlayerID(int id);
}
