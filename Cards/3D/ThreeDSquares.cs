using Godot;
using System;

public partial class ThreeDSquares : Node3D
{
  [Export]
  public Color affectedColor = new Color(0.765f, 0.725f, 0.388f); 

  private MeshInstance3D[] tiles = new MeshInstance3D[25];

  public override void _Ready()
  {
    for (int i = 1; i <= 25; i++)
    {
      tiles[i] = GetNode<MeshInstance3D>("Square" + i.ToString());
    }
  }

  public void ColorTiles(int[] tilesToColor)
  {
    for (int i = 0; i < tilesToColor.Length; i++)
    {
      tiles[tilesToColor[i]].MaterialOverride = new StandardMaterial3D
      {
        AlbedoColor = affectedColor
      };
    }
  }
}
