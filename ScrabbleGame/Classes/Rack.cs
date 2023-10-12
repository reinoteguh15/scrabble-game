namespace ScrabbleGame.Classes;

public class Rack
{
	private string[] _letterRack;
	
	public Rack()
	{
		_letterRack = new string[7];
	}
	
	public void AddLetter(string letter, int index)
	{
		_letterRack[index] = letter;
		
	}
	
	public int GetSize()
	{
		return _letterRack.Length;
	}
}
