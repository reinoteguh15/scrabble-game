namespace ScrabbleGame.Controller;

using ScrabbleGame.Classes;
using ScrabbleGame.Enums;
using ScrabbleGame.Interface;

class GameController
{
	public int MaxPlayer {get; private set;}
	private bool _firstMove;
	public bool GameFinished {get; private set;}
	private List<char> _lettersBag;
	private List<string> _wordDictionary;
	private Board _board;
	private IPlayer? _currentPlayer;
	private IPlayer _winner;
	private List<IPlayer> _playerList;
	private Dictionary<IPlayer, PlayerData> _playerInfo;
	private Dictionary<char, int> _tileQuantity;
	private Dictionary<char, int> _tileScore;
	private Dictionary<Position, Bonus> _multiplier;
	
	public GameController(int maxPlayers)
	{
		// Assign maximum number of players
		MaxPlayer = maxPlayers;
		
		_firstMove = true;
		
		// Create a new list string for letters bag
		_lettersBag = new();
		
		// Create a new list string for word dictionary
		_wordDictionary = new();
		
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
	public char GetBoardLetter(Position position)
	{
		return _board.GetLetter(position);
	}
	public Bonus GetBonus(Position position)
	{
		return _board.GetPositionBonus(position);
	}
	public void InitializeBoard()
	{
		_board.SetUp();
	}
	public void InitializeBag()
	{
		foreach (var letter in Enum.GetNames(typeof(Letter)))
		{
			char letterChar;
			if (letter == "Blank")
			{
				letterChar = '?';
			}
			else
			{
				letterChar = char.Parse(letter);
			}
			
			LetterQuantity letterQuantity = (LetterQuantity) Enum.Parse(typeof(LetterQuantity), letter);
			LetterScore letterScore = (LetterScore) Enum.Parse(typeof(LetterScore), letter);
			
			for (int i = 0; i < (int)letterQuantity; i++)
			{
				_lettersBag.Add(letterChar);
			}
			
			_tileQuantity.Add(letterChar, (int)letterQuantity);
			_tileScore.Add(letterChar, (int)letterScore);
		}
	}
	public void InitializeRack(IPlayer player)
	{
		Random rnd = new();
		
		// Rack only contains 7 letters, so i < 7
		for (int i = 0; i < 7; i++)
		{
			int randomIndex = rnd.Next(1,_lettersBag.Count);
			char letter = _lettersBag[randomIndex];
						
			_lettersBag.RemoveAt(randomIndex);
			_playerInfo[player].AddLetter(letter);
			_tileQuantity[letter] -= 1;
		}
				
	}
	public Dictionary<char, int> GetRemainingTile()
	{
		return _tileQuantity;
	}
	public List<IPlayer> GetPlayerList()
	{
		return _playerList;
	}
	public char GetInitialTile(IPlayer player)
	{
		return _playerInfo[player].GetStartingTile();
	}
	public IPlayer GetInitialPlayer()
	{
		int i = 0;
		int[] initialTileScore = new int[MaxPlayer];
		
		foreach(IPlayer player in _playerList)
		{		
			char playerTile = GetInitialTile(player);
			string tileString;
			
			if (playerTile == '?')
			{
				tileString = "Blank";
			}
			else
			{
				tileString = playerTile.ToString();
			}
			
			Letter letterScore = (Letter) Enum.Parse(typeof(Letter), tileString);
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
		_firstMove = false;
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
	public List<char> GetPlayerRack(IPlayer player)
	{
		return _playerInfo[player].GetRack();
	}
	
	public bool PlaceLetter(IPlayer player, int x, int y, char letter)
	{
		Position position = new Position(x, y);
		List<char> playerRack = GetPlayerRack(player);
		
		if (!playerRack.Contains(letter) || !(_board.GetLetter(position) == '\0')) return false;
	
		_board.PlaceLetter(letter, position);
		playerRack.Remove(letter);
		return true;
	}
	public void ReturnLetterToRack (IPlayer player, List<Position> positions)
	{
		foreach (Position pos in positions)
		{
			char tile = _board.GetLetter(pos);
			_playerInfo[player].AddLetter(tile);
			_board.RemoveLetter(pos);
		}
	}
		
	public bool IsValidMove(List<Position> positions)
	{
		int fixedPosition;
		bool verticalNotChanged = true;
		bool horizontalNotChanged = true;
		bool isConsecutive = true;
		List<int> movementIndex = new();
		List<int> tempIndex = new();
		
		if (_firstMove && !positions.Contains(new Position(8, 8))) return false;
		if (positions.Count > 1)
		{
			Position reference = positions[0];
			foreach (Position pos in positions)
			{
				if (reference.X != pos.X) horizontalNotChanged = false;
				if (reference.Y != pos.Y) verticalNotChanged = false;
			}
			if (horizontalNotChanged)
			{
				fixedPosition = positions[0].X;
				foreach(Position pos in positions)
				{
					movementIndex.Add(pos.Y);
					tempIndex.Add(pos.Y);
				}
				movementIndex.Sort();
				
				for (int i = movementIndex[0]; i < GetBoardSize(); i++)
				{
					if (GetBoardLetter(new Position(fixedPosition, i)) != '\0')
					{
						tempIndex.Add(i);
					}
				}
			}
			else if (verticalNotChanged)
			{
				fixedPosition = positions[0].Y;
				foreach (Position pos in positions)
				{
					movementIndex.Add(pos.X);
					tempIndex.Add(pos.X);
				}
				movementIndex.Sort();
				
				for (int i = movementIndex[0]; i < GetBoardSize(); i++)
				{
					if (GetBoardLetter(new Position(i, fixedPosition)) != '\0')
					{
						tempIndex.Add(i);
					}
				}
			}
		}
		
		if (movementIndex.Count > 0)
		{
			tempIndex.Sort();
			for (int i = 1; i < tempIndex.Count; i++)
			{
				if (tempIndex[i] != tempIndex[i-1] + 1)
				{
					isConsecutive = false;
					break;
				}
			}
		}
		
		return (verticalNotChanged || horizontalNotChanged) && isConsecutive;
	}	
	public async Task ReadFile(string filePath)
	{
		using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			using (StreamReader sr = new StreamReader(fs))
			{
				while(!sr.EndOfStream)
				{
					var line = await sr.ReadLineAsync();
					if (line != null) _wordDictionary.Add(line);
				}
			}
	}
	public bool IsValidWord(string word)
	{
		string filePath = @"./wordslist.txt";
		Task readFile = ReadFile(filePath);
		readFile.Wait();
		
		return _wordDictionary.Contains(word);
	}
	public int EvaluateWords(IPlayer player, List<Position> positions, string word)
	{
		
		if (IsValidWord(word) && IsValidMove(positions))
		{
			int score = 0;
			foreach(char letter in word)
			{
				score += (int)(LetterScore) Enum.Parse(typeof(LetterScore), letter.ToString());
			}
			return score;
		}
		else {return 0;}
		
	}
	public int GetPlayerScore(IPlayer player)
	{  
		return _playerInfo[player].GetScore();
	}
	public PlayerStatus GetPlayerStatus(IPlayer player)
	{
		return _playerInfo[player].GetStatus();
	}
	public void SetPlayerStatus (IPlayer player, PlayerStatus status)
	{
		_playerInfo[player].SetStatus(status);
	}
	public bool IsPlayerTurnFinish(IPlayer player)
	{
		PlayerStatus status = GetPlayerStatus(player);
		if (status != PlayerStatus.SkipTurn || status != PlayerStatus.EndTurn) return false;
		else return true;
	}
	public IPlayer GetWinner()
	{
		return _winner;
	}
	public bool IsGameFinish()
	{
		// throw new NotImplementedException();
		return false;
	}
}