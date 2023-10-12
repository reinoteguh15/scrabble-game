namespace ScrabbleGame.Classes;

public class Rack
{
	private List<string> _letterRack;
	
	public Rack()
	{
		_letterRack = new();
	}
	public bool ContainsLetter(string letter)
	{
		return _letterRack.Contains(letter);
	}
	public void AddLetterToRack(string letter)
	{
		if (_letterRack.Count < 7)
		{
			_letterRack.Add(letter);
		}
		else
		{
			return;
		}
	}
	public void RemoveLetterFromRack(string letter)
	{
		_letterRack.Remove(letter);
	}
	public int GetRackSize()
	{
		return _letterRack.Count;
	}
	
	public List<string> GetRack()
	{
		return _letterRack;
	}
}
