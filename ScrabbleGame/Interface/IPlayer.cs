namespace ScrabbleGame.Interface;

public interface IPlayer
{
	string? GetPlayerName();
	int GetPlayerID();
	void SetPlayerName(string name);
	void SetPlayerID(int id);
}