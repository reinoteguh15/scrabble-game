namespace ScrabbleGame.Classes;

public struct Position
{
	public int X {get; private set;}
	public int Y {get; private set;}
	
	public Position(int xCoord, int yCoord)
	{
		X = xCoord;
		Y = yCoord;
	}
	
	public int GetCoordinateX()
	{
		return X;
	}
	public int GetCoordinateY()
	{
		return Y;
	}
}
