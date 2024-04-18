using Godot;
using System;

public partial class Squares : Control
{
  [Export]
  public Color affectedColor = new Color(0.765f, 0.725f, 0.388f); 

  private ColorRect[] tiles = new ColorRect[25];

  public override void _Ready()
  {
    for (int i = 0; i < 25; i++)
    {
      tiles[i] = GetNode<ColorRect>("Tile" + i.ToString());
    }
  }

  public void ColorTiles(int[] tilesToColor)
  {
    for (int i = 0; i < tilesToColor.Length; i++)
    {
      tiles[tilesToColor[i]].Color = affectedColor;
    }
  }
}
