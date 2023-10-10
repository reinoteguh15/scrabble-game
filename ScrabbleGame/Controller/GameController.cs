using ScrabbleGame.Classes;
using ScrabbleGame.Enums;
using ScrabbleGame.Interface;

namespace ScrabbleGame.Controller;

class GameController
{
	private Dictionary<IPlayer, int>? _playerScore;
	private Dictionary<IPlayer, Rack>? _playerRack;
	
	public GameController()
	{
		_playerScore = new Dictionary<IPlayer, int>();
		_playerRack = new Dictionary<IPlayer, Rack>();
	}
	
	public void AddPlayer(IPlayer player)
	{
		_playerScore!.Add(player,0);
	}
	
	public void AddRackToPlayer(IPlayer player)
	{
		Rack letterRack = new();
		Random rnd = new();
		
		for (int i = 0; i < letterRack.GetRackSize(); i++)
		{
			Letter letter = (Letter) rnd.Next(1,26);
			letterRack.AddLetterToRack(letter.ToString(), i);
		}
				
		_playerRack!.Add(player,letterRack);
	}
	
	public List<string> GetPlayerRack(IPlayer player)
	{
		List<string> listRack = new List<string>();
		string[] playerRack = _playerRack![player].GetRack();
		for(int i = 0; i < playerRack.Length; i++)
		{
			listRack.Add(playerRack[i]);
		}
		
		return listRack;
	}
}