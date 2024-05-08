using Godot;
using Godot.Collections;

public partial class hand : Node3D
{
  [Export]
  public Array<ThreeDCard> cards = new Array<ThreeDCard>();

  private bool AcceptingInput = true;
  private int ActiveCard = 0; // The raised card

  public override void _Input(InputEvent @event)
  {
    if(!AcceptingInput)
      return;

    if(@event.IsActionPressed("move_left"))
    {
      SetActiveCard(ActiveCard + cards.Count - 1);
    }
    else if(@event.IsActionPressed("move_right"))
    {
      SetActiveCard(ActiveCard + cards.Count + 1);
    }
    else if(@event.IsActionPressed("select"))
    {
      // Trigger select on card, board now handles the input
      AcceptingInput = false;
    }
  }

  public void SetAcceptingInput(bool acceptingInput)
  {
    AcceptingInput = acceptingInput;
  }

  public void AddCard(ThreeDCard card)
  {
    AddChild(card);
    // Move card to some offset based on its index
    card.Position = new Vector3(cards.Count * 3, 0, 0);
    cards.Add(card);
  }

  private void SetActiveCard(int index)
  {
    int nextIndex = index % cards.Count;

    // Lower the old card
    cards[ActiveCard].Position = new Vector3(cards[ActiveCard].Position.X, cards[ActiveCard].Position.Y, cards[ActiveCard].Position.Z - 1f);
    ActiveCard = nextIndex;
    // Raise the new card
    cards[ActiveCard].Position = new Vector3(cards[ActiveCard].Position.X, cards[ActiveCard].Position.Y, cards[ActiveCard].Position.Z + 1f);
  }
}
