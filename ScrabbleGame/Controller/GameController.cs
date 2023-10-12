using ScrabbleGame.Classes;
using ScrabbleGame.Enums;
using ScrabbleGame.Interface;
using System;
using System.Xml.Serialization;


namespace ScrabbleGame.Controller;

class GameController
{
	public int PlayersCount {get; private set;}
	private Bag? _wordsBag;
	private Board _board;
	private IPlayer? _currentPlayer;
	private List<IPlayer> _playerList;
	private Dictionary<IPlayer, string> _playerStartingTile;
	private Dictionary<IPlayer, int> _playerScore;
	private Dictionary<IPlayer, Rack> _playerRack;
	private Dictionary<IPlayer, PlayerTurn> _playerTurn;
	
	private Dictionary<string, int> _tileQuantity;
	private Dictionary<string, int> _tileScore;
	private Dictionary<Position, Bonus> _letterMultiplier;
	
	public GameController(int numPlayers)
	{
		PlayersCount = numPlayers;
		_wordsBag = new();
		_board = new(15);
		_playerList = new();
		_playerStartingTile = new();
		_playerScore = new();
		_playerRack = new();
		_playerTurn = new();
		_tileQuantity = new();
		_tileScore = new();
		_letterMultiplier = new();
	}
	
	public Board GetBoard()
	{
		return _board;
	}
	public Bag GetBag()
	{
		return _wordsBag!;
	}
	public void SetUpBoard()
	{
		_board.SetUpBoard();
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
	public bool AddPlayer(IPlayer player)
	{
		if(! _playerScore.ContainsKey(player))
		{
			_playerScore.Add(player,0);
			_playerList.Add(player);
			return true;	
		}
		else
		{
			return false;
		}
	}
	public void SetPlayerRack(IPlayer player)
	{
		Rack letterRack = new();
		Random rnd = new();
		
		for (int i = 0; i < 7; i++)
		{
			List<string> letters = _wordsBag!.GetLettersFromBag();
			string letter = letters[rnd.Next(1,letters.Count)];
			
			_wordsBag.RemoveLetter(letter);
			_tileQuantity[letter] -= 1;
			
			letterRack.AddLetterToRack(letter);		
		}
				
		_playerRack!.Add(player,letterRack);
	}
	public Rack GetPlayerRack(IPlayer player)
	{
		return _playerRack[player];
	}
	public Dictionary<string, int> GetRemainingTile()
	{
		return _tileQuantity;
	}
	public List<IPlayer> GetPlayerList()
	{
		return _playerList;
	}
	public string GetPlayerStartingTile(IPlayer player)
	{
		return _playerStartingTile[player];
	}
	public IPlayer GetFirstPlayer()
	{
		int i = 0;
		int[] startingTileScore = new int[PlayersCount];
		
		foreach(IPlayer player in _playerList)
		{
			int playerTileScore = new Random().Next(26);
			startingTileScore[i] = playerTileScore;
			
			string playerTile = Enum.GetName(typeof(Letter), playerTileScore)!;
			_playerStartingTile.Add(player, playerTile);
			i++;
		}
		int first = Array.IndexOf(startingTileScore, startingTileScore.Min());
		_currentPlayer = _playerList[first];
		return _playerList[first];
	}
	public IPlayer GetCurrentPlayer()
	{
		return _currentPlayer!;
	}
	public int GetPlayerScore(IPlayer player)
	{  
		// TODO:
		throw new NotImplementedException();
	}
	public bool PlaceLetterToBoard(IPlayer player, int x, int y, string letter)
	{
		Position position = new Position(x, y);
		Rack playerRack = GetPlayerRack(player);
		
		if (playerRack.ContainsLetter(letter))
		{
			if ((_board.GetLetter(position) == " ") || (_board.GetLetter(position) == null))
			{
				_board.PlaceLetter(letter, position);
				playerRack.RemoveLetterFromRack(letter);
				return true;
			}
			else
			{
				return false;
			}
		}		
		else
		{
			return false;
		}		
	}
	public PlayerTurn GetAction()
	{
		throw new NotImplementedException();
	}
	public bool isSubmit()
	{
		throw new NotImplementedException();
	}
	public bool isGameFinish()
	{
		throw new NotImplementedException();
	}
}