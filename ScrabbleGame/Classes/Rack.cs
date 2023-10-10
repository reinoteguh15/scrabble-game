namespace ScrabbleGame.Classes;

public class Rack
{
	private string[] _letterRack;
	
	public Rack()
	{
		_letterRack = new string[7];
	}
	
	public void AddLetterToRack(string letter, int index)
	{
		_letterRack[index] = letter;
		
	}
	
	public int GetRackSize()
	{
		return _letterRack.Length;
	}
	
	public string[] GetRack()
	{
		return _letterRack;
	}
}
