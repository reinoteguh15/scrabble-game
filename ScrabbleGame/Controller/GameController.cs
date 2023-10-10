using System.Data.Common;
using System.Threading.Channels;
using ScrabbleGame.Classes;
using ScrabbleGame.Enums;
using ScrabbleGame.Interface;

namespace ScrabbleGame.Controller;

class GameController
{
	private int _gameTime;
	private Bag? _wordsBag;
	private IPlayer? _currentPlayer;
	private Dictionary<IPlayer, int> _playerScore;
	private Dictionary<IPlayer, Rack> _playerRack;
	private Dictionary<IPlayer, PlayerTurn> _playerTurn;
	
	private Dictionary<string, int> _tileQuantity;
	private Dictionary<string, int> _tileScore;
	private Dictionary<Position, Bonus> _letterMultiplier;
	
	public GameController()
	{
		_wordsBag = new();
		_playerScore = new();
		_playerRack = new();
		_playerTurn = new();
		_tileQuantity = new();
		_tileScore = new();
		_letterMultiplier = new();
	}
	
	public void SetUpBag()
	{
		foreach(var letter in Enum.GetNames(typeof(Letter)))
		{
			if (letter == "E")
			{
				_tileQuantity.Add(letter, 12);
			}
			else if ((letter == "A") || (letter == "I"))
			{
				_tileQuantity.Add(letter, 9);
			}
			else if (letter == "O")
			{
				_tileQuantity.Add(letter, 8);
			}
			else if ((letter == "N") || (letter == "R") || (letter == "T"))
			{
				_tileQuantity.Add(letter, 6);
			}
			else if ((letter == "D") || (letter == "L") || (letter == "S") || (letter == "U"))
			{
				_tileQuantity.Add(letter, 4);
			}
			else if (letter == "G")
			{
				_tileQuantity.Add(letter, 3);
			}
			else if(
				(letter == "B") || (letter == "C") || (letter == "F") || (letter == "H") || (letter == "M") ||
				(letter == "P") || (letter == "V") || (letter == "W") || (letter == "Y") || (letter == "Blank")
			)
			{
				_tileQuantity.Add(letter, 2);
			}
			else if((letter == "J") || (letter == "K") || (letter == "Q") || (letter == "X") || (letter == "Z"))
			{
				_tileQuantity.Add(letter, 1);
			}
			else
			{
				return;
			}
		}
		
		foreach(var letter in Enum.GetNames(typeof(Letter)))
		{
			if ((letter == "Q") || (letter == "Z"))
			{
				_tileScore.Add(letter, 10);
			}
			else if ((letter == "J") || (letter == "X"))
			{
				_tileScore.Add(letter, 8);
			}
			else if (letter == "K")
			{
				_tileScore.Add(letter, 5);
			}
			else if ((letter == "F") || (letter == "H") || (letter == "V") || (letter == "W") || (letter == "Y"))
			{
				_tileScore.Add(letter, 4);
			}
			else if ((letter == "B") || (letter == "C") || (letter == "M") || (letter == "P"))
			{
				_tileScore.Add(letter, 3);
			}
			else if ((letter == "G") || (letter == "D"))
			{
				_tileScore.Add(letter, 2);
			}
			else if(
				(letter == "A") || (letter == "E") || (letter == "I") || (letter == "L") || (letter == "N") ||
				(letter == "O") || (letter == "R") || (letter == "S") || (letter == "T") || (letter == "U")
			)
			{
				_tileScore.Add(letter, 1);
			}
			else if(letter == "Blank")
			{
				_tileScore.Add(letter, 0);
			}
			else
			{
				return;
			}
		}
		
		foreach (var keyValuePair in _tileQuantity)
		{
			for(int count = 0; count < keyValuePair.Value; count++)
			{
				_wordsBag!.AddLetterToBag(keyValuePair.Key);
			}
		}
	}
	public Dictionary<string, int> GetRemainingTile()
	{
		return _tileQuantity;
	}
	public void AddPlayer(IPlayer player)
	{
		_playerScore.Add(player,0);
	}
	
	public void AddRackToPlayer(IPlayer player)
	{
		Rack letterRack = new();
		Random rnd = new();
		
		for (int i = 0; i < letterRack.GetRackSize(); i++)
		{
			List<string> letters = _wordsBag!.GetLettersFromBag();
			string letter = letters[rnd.Next(1,letters.Count)];
			letterRack.AddLetterToRack(letter, i);
			
			_wordsBag.RemoveLetter(letter);
			_tileQuantity[letter] -= 1;
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