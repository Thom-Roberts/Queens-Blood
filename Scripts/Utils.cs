using Godot;
using System;

public static class Utils
{
  public static Tuple<int, int> SplitPositionToCoordinates(string position)
  {
    string[] coordinates = position.Split(',');
    int x = int.Parse(coordinates[0]);
    int y = int.Parse(coordinates[1]);
    return Tuple.Create(x, y);
  }

  // 12 is default for the card images, but we can change it
  // for example, to the currently active tile when placing cards
  public static int ComputeTilePositionForCardImage(string position, int startingIndex = 12)
  {
    Tuple<int, int> pos = SplitPositionToCoordinates(position);
    int x = pos.Item1;
    int y = pos.Item2;
    return -(y * 5) + x + startingIndex;
  }
}
