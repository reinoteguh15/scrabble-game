namespace ScrabbleGame.Controller;

using ScrabbleGame.Classes;
using ScrabbleGame.Enums;
using ScrabbleGame.Interface;

class GameController
{
	public int MaxPlayer {get; private set;}
	public bool GameFinished {get; private set;}
	private List<string> _lettersBag;
	private Board _board;
	private IPlayer? _currentPlayer;
	private List<IPlayer> _playerList;
	private Dictionary<IPlayer, PlayerData> _playerInfo;
	private Dictionary<string, int> _tileQuantity;
	private Dictionary<string, int> _tileScore;
	private Dictionary<Position, Bonus> _multiplier;
	
	public GameController(int maxPlayers)
	{
		// Assign maximum number of players
		MaxPlayer = maxPlayers;
		
		// Create a new list string for letters bag
		_lettersBag = new();
		
		// Create a new board with size of 15 x 15
		_board = new(15);
		
		// Add player to game
		_playerList = new();
		
		// Assign new player info
		_playerInfo = new();
		
		for (int i = 1; i <= MaxPlayer; i++)
		{
			Player player = new();
			PlayerData playerData = new();
			player.SetPlayerID(i);
			_playerList.Add(player);
			_playerInfo.Add(player, playerData);
		}
		
		_tileQuantity = new();
		_tileScore = new();
		_multiplier = new();
	}
	
	public int GetBoardSize()
	{
		return _board.GetSize();
	}
	public string GetBoardLetter(Position position)
	{
		return _board.GetLetter(position);
	}
	public void InitializeBoard()
	{
		_board.SetUp();
	}
	public void InitializeBag()
	{
		foreach (var letter in Enum.GetNames(typeof(Letter)))
		{
			LetterQuantity letterQuantity = (LetterQuantity) Enum.Parse(typeof(LetterQuantity), letter);
			LetterScore letterScore = (LetterScore) Enum.Parse(typeof(LetterScore), letter);
			
			for (int i = 0; i < (int)letterQuantity; i++)
			{
				_lettersBag.Add(letter);
			}
			
			_tileQuantity.Add(letter, (int)letterQuantity);
			_tileScore.Add(letter, (int)letterScore);
		}
	}
	public void InitializeRack(IPlayer player)
	{
		Random rnd = new();
		
		// Rack only contains 7 letters, so i < 7
		for (int i = 0; i < 7; i++)
		{
			int randomIndex = rnd.Next(1,_lettersBag.Count);
			string letter = _lettersBag[randomIndex];
						
			_lettersBag.RemoveAt(randomIndex);
			_playerInfo[player].AddLetter(letter);
			_tileQuantity[letter] -= 1;
		}
				
	}
	public Dictionary<string, int> GetRemainingTile()
	{
		return _tileQuantity;
	}
	public List<IPlayer> GetPlayerList()
	{
		return _playerList;
	}
	public string? GetInitialTile(IPlayer player)
	{
		return _playerInfo[player].GetStartingTile();
	}
	public IPlayer GetInitialPlayer()
	{
		int i = 0;
		int[] initialTileScore = new int[MaxPlayer];
		
		foreach(IPlayer player in _playerList)
		{		
			string? playerTile = GetInitialTile(player);
			
			Letter letterScore = (Letter) Enum.Parse(typeof(LetterScore), playerTile);
			initialTileScore[i] = (int)letterScore;
			i++;
		}
		int initialPlayer = Array.IndexOf(initialTileScore, initialTileScore.Min());
		
		_currentPlayer = _playerList[initialPlayer];
		return _playerList[initialPlayer];
	}
	public IPlayer GetCurrentPlayer()
	{
		return _currentPlayer;
	}
	public IPlayer GetNextPlayer()
	{
		int currentIndex = _playerList.FindIndex(currentPlayer => currentPlayer.Equals(_currentPlayer));
		if (currentIndex == _playerList.Count - 1)
		{
			_currentPlayer = _playerList[0];
			return _currentPlayer;
		}
		else
		{
			_currentPlayer = _playerList[currentIndex + 1];
			return _currentPlayer;
		}
	}
	public List<string> GetPlayerRack(IPlayer player)
	{
		return _playerInfo[player].GetRack();
	}
	
	public bool PlaceLetter(IPlayer player, int x, int y, string letter)
	{
		Position position = new Position(x, y);
		List<string> playerRack = GetPlayerRack(player);
		
		if (playerRack.Contains(letter))
		{
			if ((_board.GetLetter(position) == "") || (_board.GetLetter(position) == null))
			{
				_board.PlaceLetter(letter, position);
				playerRack.Remove(letter);
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
	public bool IsValidMove()
	{
		throw new NotImplementedException();
	}
	public bool IsValidWord()
	{
		throw new NotImplementedException();
	}
	public int EvaluateWords(List<Position> positions, string word)
	{
		
		throw new NotImplementedException();
	}
	public int GetPlayerScore(IPlayer player)
	{  
		// TODO:
		throw new NotImplementedException();
	}
	public bool IsGameFinish()
	{
		// throw new NotImplementedException();
		return false;
	}
}