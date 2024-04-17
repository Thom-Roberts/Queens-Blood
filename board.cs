using Godot;
using System;

public partial class board : Node3D
{
  public slot[] slots = new slot[15];

  private int currentSlot = 0;
  // Temporary for proof of concept
  private int selectedSlot = 0;
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
      slots[currentSlot].SetLightbulbCount((slots[currentSlot].lightbulbCount + 1) % 4);
      currentSlot = (currentSlot + 1) % 15;
    }
    else if(@event.IsActionPressed("select"))
    {
      GD.Print("Slot " + currentSlot + " has " + slots[currentSlot].lightbulbCount + " lightbulbs");
      slots[currentSlot].SetLightbulbCount((slots[currentSlot].lightbulbCount + 1) % 4);
      currentSlot = (currentSlot + 1) % 15;
    }

    MoveCursor(@event);
  }

  private void MoveCursor(InputEvent @event)
  {
    int newSlot = selectedSlot;
    if(@event.IsActionPressed("move_down"))
    {
      GD.Print("Down pressed");
      if(selectedSlot >= 10)
      {
        return;
      }
      newSlot += 5;
      GD.Print("Selected slot: " + newSlot);
    }
    if(@event.IsActionPressed("move_up"))
    {
      GD.Print("Up pressed");
      if(selectedSlot < 5)
      {
        return;
      }
      newSlot -= 5;
      GD.Print("Selected slot: " + newSlot);
    }
    if(@event.IsActionPressed("move_left"))
    {
      
      GD.Print("Left pressed");
      if(selectedSlot % 5 == 0)
      {
        return;
      }
      newSlot -= 1;
      GD.Print("Selected slot: " + newSlot);
    }
    if(@event.IsActionPressed("move_right"))
    {
      
      GD.Print("Right pressed");
      if(selectedSlot % 5 == 4)
      {
        return;
      }
      newSlot += 1;
      GD.Print("Selected slot: " + newSlot);
    }
    // We are updating
    if(newSlot != selectedSlot)
    {
      slots[selectedSlot].SetColor(false);
      slots[newSlot].SetColor(true);
      selectedSlot = newSlot;
    }
  }
}
