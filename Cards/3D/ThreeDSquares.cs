using Godot;
using System;

public partial class ThreeDSquares : Node3D
{
  [Export]
  public Color affectedColor = new Color(0.765f, 0.725f, 0.388f); 
  [Export]
  private StandardMaterial3D centerMaterial;

  private MeshInstance3D[] tiles = new MeshInstance3D[25];

  public override void _Ready()
  {
    for (int i = 1; i <= 25; i++)
    {
      tiles[i - 1] = GetNode<MeshInstance3D>("Square" + i.ToString());
    }
    tiles[12].MaterialOverride = centerMaterial;
  }

  public void ColorTiles(string[] tilesToColor)
  {
    for (int i = 0; i < tilesToColor.Length; i++)
    {
      tiles[ComputeTilePosition(tilesToColor[i])].MaterialOverride = new StandardMaterial3D
      {
        AlbedoColor = affectedColor
      };
    }
  }

  private int ComputeTilePosition(string position)
  {
    string[] coordinates = position.Split(',');
    int x = int.Parse(coordinates[0]);
    int y = int.Parse(coordinates[1]);
    GD.Print(x, y, -(y * 5) + x + 12);
    // 12 is the center tile
    return -(y * 5) + x + 12;
  }
}
