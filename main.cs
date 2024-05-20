using Godot;
using Godot.Collections;

public partial class main : Node
{
  public Array<ThreeDCard> deckCards = new Array<ThreeDCard>();
  private hand playerHand;
  private board gameBoard;
  private ThreeDCard selectedCard;

  public override void _Ready()
  {
    // Called every time the node is added to the scene.
    // Initialization here
    // Load the deck
    playerHand = GetNode<hand>("Hand");
    gameBoard = GetNode<board>("Board");
    LoadDeck();
    playerHand.CardSelected += HandleCardSelection;
    gameBoard.SlotSelected += HandleSlotSelection;
    gameBoard.CancelSelection += HandleCancelSelection;
  }

  private void LoadDeck()
  {
    // Load the deck
    PackedScene cardScene = GD.Load<PackedScene>("res://Cards/3D/3d_card.tscn");
    var securityOfficerCard = cardScene.Instantiate<ThreeDCard>();
    securityOfficerCard.cardData = GD.Load<CardData>(CardList.cardLookup[CardType.Security_Officer]);
    
    playerHand.AddCard(securityOfficerCard);

    var jUnitSweeperCard = cardScene.Instantiate<ThreeDCard>();
    jUnitSweeperCard.cardData = GD.Load<CardData>(CardList.cardLookup[CardType.J_Unit_Sweeper]);
    playerHand.AddCard(jUnitSweeperCard);

    playerHand.SetActiveCard(0);
  }

  private void HandleCardSelection(ThreeDCard card)
  {
    GD.Print("Card selected: " + card.cardData.CardName);
    selectedCard = card;
    playerHand.SetAcceptingInput(false);
    gameBoard.SetAcceptingInput(true);
  }

  private void HandleSlotSelection(slot slot, int slotIndex)
  {
    GD.Print("Slot selected");
    if(slot.lightbulbCount <= selectedCard.cardData.CardCost)
    {
      GD.Print("Attempting to place card");
      selectedCard.GlobalPosition = slot.GetCardPosition();
      gameBoard.UpdateLightbulbCount(selectedCard.cardData.CostIncreaseTilesAsString, slotIndex);
    }
    else
    {
      GD.Print("Unable to play card in this slot. Not enough lightbulbs.");
    }
  }

  private void HandleCancelSelection()
  {
    playerHand.SetAcceptingInput(true);
    gameBoard.SetAcceptingInput(false);
  }
}
