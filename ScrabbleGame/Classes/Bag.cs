using ScrabbleGame.Enums;

namespace ScrabbleGame.Classes;

public class Bag
{
	private List<string> _letterBag;
	
	public Bag()
	{
		_letterBag = new();
	}
	
	public void AddLetterToBag(string letter)
	{
		_letterBag.Add(letter);
	}
	
	public void RemoveLetter(string letter)
	{
		_letterBag.Remove(letter);
	}
	
	public void ShuffleBag()
	{
		
	}
	
	public List<string> GetLettersFromBag()
	{
		return _letterBag;
	}
	
	public int GetBagCapacity()
	{
		return _letterBag.Count;
	}
}
