namespace Player
{
  
  public class BoatSeatsInfo
  {
    public BoatSeatsInfo(int countInRow, float deltaX, float deltaZ)
    {
      CountInRow = countInRow;
      DeltaX = deltaX;
      DeltaZ = deltaZ;
    }

    public int CountInRow { get; }
    public float DeltaX { get; }
    public float DeltaZ { get; }
  }
}