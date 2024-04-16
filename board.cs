using Godot;
using System;

public partial class board : Node3D
{
  public slot[] slots = new slot[15];

  private int currentSlot = 0;
  public override void _Ready()
  {
    for (int i = 1; i <= 15; i++)
    {
      slots[i - 1] = GetNode<slot>("Slots/Slot" + i.ToString());
    }
  }

  public override void _Input(InputEvent @event)
  {
    if(@event.IsActionPressed("ui_select"))
    {
      GD.Print("Slot " + currentSlot + " has " + slots[currentSlot].lightbulbCount + " lightbulbs");
      GD.Print("Slots " + currentSlot + " has modifier " + slots[currentSlot].currentModifier);
      slots[currentSlot].SetLightbulbCount((slots[currentSlot].lightbulbCount + 1) % 4);
      currentSlot = (currentSlot + 1) % 15;
    }
    else if(@event.IsActionPressed("select"))
    {
      GD.Print("Slot " + currentSlot + " has " + slots[currentSlot].lightbulbCount + " lightbulbs");
      GD.Print("Slots " + currentSlot + " has modifier " + slots[currentSlot].currentModifier);
      slots[currentSlot].SetLightbulbCount((slots[currentSlot].lightbulbCount + 1) % 4);
      currentSlot = (currentSlot + 1) % 15;
    }
  }
}
