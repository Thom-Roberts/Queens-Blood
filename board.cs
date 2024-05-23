using Godot;

public partial class board : Node3D
{
  [Signal]
  public delegate void SlotSelectedEventHandler(slot slot, int slotIndex);
  [Signal]
  public delegate void CancelSelectionEventHandler();
  public slot[] slots = new slot[15];
  private Label3D[] scoreLabels = new Label3D[6];
  private bool AcceptingInput = false;
  private int selectedSlot = 0;
  public override void _Ready()
  {
    for (int i = 1; i <= 15; i++)
    {
      slots[i - 1] = GetNode<slot>("Slots/Slot" + i.ToString());
    }

    for (int i = 1; i <= 6; i++)
    {
      scoreLabels[i - 1] = GetNode<Label3D>("ScoreLabels/Label" + i.ToString());
      scoreLabels[i - 1].Text = "0";
    }

    var inputManager = GetTree().Root.GetNode<InputManager>("Main/InputManager");
    if(inputManager == null)
    {
      GD.Print("InputManager not found. This is a bug.");
      return;
    }
    // inputManager.Register("ui_select", OnSelect);
    inputManager.Register("select", OnSelect);
    inputManager.Register("cancel", () => EmitSignal(SignalName.CancelSelection));
    inputManager.Register("move_down", () => MoveCursor("down"));
    inputManager.Register("move_up", () => MoveCursor("up"));
    inputManager.Register("move_left", () => MoveCursor("left"));
    inputManager.Register("move_right", () => MoveCursor("right"));
  }

    public void OnSelect()
  {
    if(!AcceptingInput)
      return;

    EmitSignal(SignalName.SlotSelected, slots[selectedSlot], selectedSlot);
  }

  private void MoveCursor(string direction)
  {
    if(!AcceptingInput)
      return;
    
    int newSlot = selectedSlot;
    if(direction == "down")
    {
      if(selectedSlot >= 10)
      {
        return;
      }
      newSlot += 5;
    }
    if(direction == "up")
    {
      if(selectedSlot < 5)
      {
        return;
      }
      newSlot -= 5;
    }
    if(direction == "left")
    {
      if(selectedSlot % 5 == 0)
      {
        return;
      }
      newSlot -= 1;
    }
    if(direction == "right")
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
    if(acceptingInput == true) // Prevent race condition
    {
      SetDeferred("AcceptingInput", true);
    }
    else
    {
      AcceptingInput = acceptingInput;
    }
  }

  public void UpdateLightbulbCount(string[] cardIncreasePositions, int slotIndex)
  {
    foreach(string position in cardIncreasePositions)
    {
      // TODO: Don't update positions that already have cards
      int tilePosition = Utils.ComputeTilePositionForCardImage(position, slotIndex);
      if(tilePosition < 0 || tilePosition >= 15)  
        continue;
      if(slots[tilePosition].occupiedCard != null) // Don't increase lightbulb count if there is already a card there
        continue;
    
      slots[tilePosition].SetLightbulbCount(slots[tilePosition].lightbulbCount + 1);
    }
  }

  public void RecalculateScores()
  {
    // Top row score calculation
    int localScore = 0;
    for(int i = 0; i < 5; i++)
    {
      if(slots[i].occupiedCard == null)
      {
        continue;
      }
      
      localScore += slots[i].occupiedCard.cardData.CardValue;
    }
    scoreLabels[0].Text = localScore.ToString();
    
    // Middle row score calculation
    localScore = 0;
    for(int i = 5; i < 10; i++)
    {
      if(slots[i].occupiedCard == null)
      {
        continue;
      }
      
      localScore += slots[i].occupiedCard.cardData.CardValue;
    }
    scoreLabels[1].Text = localScore.ToString();

    // Bottom row score calculation
    localScore = 0;
    for(int i = 10; i < 15; i++)
    {
      if(slots[i].occupiedCard == null)
      {
        continue;
      }
      
      localScore += slots[i].occupiedCard.cardData.CardValue;
    }
    scoreLabels[2].Text = localScore.ToString();
  }
}
