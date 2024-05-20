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
}
