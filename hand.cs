using Godot;
using Godot.Collections;

public partial class hand : Node3D
{
  [Signal]
  public delegate void CardSelectedEventHandler(ThreeDCard card);

  [Export]
  public Array<ThreeDCard> cards = new Array<ThreeDCard>();

  public bool AcceptingInput = true;
  private int ActiveCard = -1; // The raised card

  public override void _Ready()
  {
    var inputManager = GetTree().Root.GetNode<InputManager>("Main/InputManager");
    if(inputManager == null)
    {
      GD.Print("InputManager not found. This is a bug.");
      return;
    }
    // inputManager.Register("ui_select", OnSelect);
    inputManager.Register("select", OnSelect);
    inputManager.Register("move_left", () => HandleDirectionChange("left"));
    inputManager.Register("move_right", () => HandleDirectionChange("right"));
  }

  public void HandleDirectionChange(string direction)
  {
    if(!AcceptingInput)
      return;

    if(direction == "left")
    {
      SetActiveCard(ActiveCard + cards.Count - 1);
    }
    else if(direction == "right")
    {
      SetActiveCard(ActiveCard + cards.Count + 1);
    }
  }

  // TODO: Debug issue where select is triggering after pressing enter on board state
  public void OnSelect()
  {
    if(!AcceptingInput)
      return;

    EmitSignal(SignalName.CardSelected, cards[ActiveCard]);
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

  public void AddCard(ThreeDCard card)
  {
    AddChild(card);
    // Move card to some offset based on its index
    card.Position = new Vector3(cards.Count * 3, 0, 0);
    cards.Add(card);
  }

  public void SetActiveCard(int index)
  {
    int nextIndex = index % cards.Count;

    // Lower the old card, except if we're just starting
    if(ActiveCard != -1)
    {
      cards[ActiveCard].Position = new Vector3(cards[ActiveCard].Position.X, cards[ActiveCard].Position.Y - 1f, cards[ActiveCard].Position.Z + 1f);
    }
    ActiveCard = nextIndex;
    // Raise the new card
    cards[ActiveCard].Position = new Vector3(cards[ActiveCard].Position.X, cards[ActiveCard].Position.Y + 1f, cards[ActiveCard].Position.Z - 1f);
  }
}
