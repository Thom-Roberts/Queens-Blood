using Godot;
using System;

public partial class board : Node3D
{
  [Signal]
  public delegate void SlotSelectedEventHandler(slot slot, int slotIndex);
  [Signal]
  public delegate void CancelSelectionEventHandler();
  public slot[] slots = new slot[15];
  private bool AcceptingInput = false;
  private int selectedSlot = 0;
  public override void _Ready()
  {
    for (int i = 1; i <= 15; i++)
    {
      slots[i - 1] = GetNode<slot>("Slots/Slot" + i.ToString());
    }

    var inputManager = GetTree().Root.GetNode<InputManager>("Main/InputManager");
    if(inputManager == null)
    {
      GD.Print("InputManager not found. This is a bug.");
      return;
    }
    inputManager.Register("ui_select", OnSelect);
    inputManager.Register("select", OnSelect);
    inputManager.Register("cancel", () => EmitSignal(SignalName.CancelSelection));
  }

    public void OnSelect()
  {
    if(!AcceptingInput)
      return;

    EmitSignal(SignalName.SlotSelected, slots[selectedSlot], selectedSlot);
  }

  public override void _Input(InputEvent @event)
  {
    if(!AcceptingInput)
      return;

    MoveCursor(@event);
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
    GD.Print("Setting accepting input to: " + acceptingInput);
    AcceptingInput = acceptingInput;
  }

  public void UpdateLightbulbCount(string[] cardIncreasePositions, int slotIndex)
  {
    foreach(string position in cardIncreasePositions)
    {
      // TODO: Don't update positions that already have cards
      int tilePosition = Utils.ComputeTilePositionForCardImage(position, slotIndex);
      if(tilePosition < 0 || tilePosition >= 15)
      {
        continue;
      }
      slots[tilePosition].SetLightbulbCount(slots[tilePosition].lightbulbCount + 1);
    }
  }
}
