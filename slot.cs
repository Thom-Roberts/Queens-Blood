using Godot;
using System;

public partial class slot : Node3D
{
  public int lightbulbCount = 1;
  public int currentModifier = 0;
  private lightbulb[] lightbulbs = new lightbulb[3];

  public override void _Ready()
  {
    lightbulbs[0] = GetNode<lightbulb>("Lightbulb");
    lightbulbs[1] = GetNode<lightbulb>("Lightbulb2");
    lightbulbs[2] = GetNode<lightbulb>("Lightbulb3");
    SetLightbulbCount(lightbulbCount);
  }

  public void SetLightbulbCount(int count)
  {
    lightbulbCount = count;

    // Set visibility
    for(int i = 0; i < lightbulbs.Length; i++)
    {
      if(i < count)
        lightbulbs[i].Visible = true;
      else
        lightbulbs[i].Visible = false;
    }

    // Move
    if(count == 1)
      lightbulbs[0].Position = new Vector3(0, 1, 0);
    else if(count == 2)
    {
      lightbulbs[0].Position = new Vector3(-1, 1, 0);
      lightbulbs[1].Position = new Vector3(1, 1, 0);
    }
    else if(count == 3)
    {
      lightbulbs[0].Position = new Vector3(-1, 1, -1);
      lightbulbs[1].Position = new Vector3(1, 1, -1);
      lightbulbs[2].Position = new Vector3(0, 1, 1);
    }
  }

  public void SetModifier(int modifier)
  {
    currentModifier = modifier;
  }
}
