using Godot;
using System;

public partial class board : Node3D
{
  [Signal]
  public delegate void SlotSelectedEventHandler(slot slot);
  public slot[] slots = new slot[15];
  private bool AcceptingInput = false;
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
    if(!AcceptingInput)
      return;

    MoveCursor(@event);

    if(@event.IsActionPressed("ui_select"))
    {
      EmitSignal(SignalName.SlotSelected, slots[selectedSlot]);
    }
    else if(@event.IsActionPressed("select"))
    {
      EmitSignal(SignalName.SlotSelected, slots[selectedSlot]);
    }

  }

  private void MoveCursor(InputEvent @event)
  {
    int newSlot = selectedSlot;
    if(@event.IsActionPressed("move_down"))
    {
      if(selectedSlot >= 10)
      {
        return;
      }
      newSlot += 5;
    }
    if(@event.IsActionPressed("move_up"))
    {
      if(selectedSlot < 5)
      {
        return;
      }
      newSlot -= 5;
    }
    if(@event.IsActionPressed("move_left"))
    {
      if(selectedSlot % 5 == 0)
      {
        return;
      }
      newSlot -= 1;
    }
    if(@event.IsActionPressed("move_right"))
    {
      if(selectedSlot % 5 == 4)
      {
        return;
      }
      newSlot += 1;
    }
    // We are updating
    if(newSlot != selectedSlot)
    {
      GD.Print("Selected slot: " + newSlot);
      slots[selectedSlot].SetColor(false);
      slots[newSlot].SetColor(true);
      selectedSlot = newSlot;
    }
  }
  public void SetAcceptingInput(bool acceptingInput)
  {
    AcceptingInput = acceptingInput;
  }
}
