using Godot;
using Godot.Collections;

public partial class main : Node
{
  [Export]
  public Array<CardData> deckCards = new Array<CardData>();
  private hand playerHand;
  private board gameBoard;
  private ThreeDCard selectedCard;
  private PackedScene cardScene;
  public override void _Ready()
  {
    // Called every time the node is added to the scene.
    // Initialization here
    // Load the deck
    playerHand = GetNode<hand>("Hand");
    gameBoard = GetNode<board>("Board");
    playerHand.CardSelected += HandleCardSelection;
    gameBoard.SlotSelected += HandleSlotSelection;
    gameBoard.CancelSelection += HandleCancelSelection;

    // Setup for later card draws and initialization
    cardScene = GD.Load<PackedScene>("res://Cards/3D/3d_card.tscn");
    
    // Start the game
    LoadDeck();
  }

  private void LoadDeck()
  {
    // Draw 5 cards randomly from the deck
    for(int i = 0; i < 5; i++)
    {
      var randomIndex = GD.RandRange(0, deckCards.Count);
      var cardData = deckCards[randomIndex];
      var card = cardScene.Instantiate<ThreeDCard>();
      card.cardData = cardData; 
      playerHand.AddCard(card);
      deckCards.RemoveAt(randomIndex);
    }

    // var securityOfficerCard = cardScene.Instantiate<ThreeDCard>();
    // securityOfficerCard.cardData = GD.Load<CardData>(CardList.cardLookup[CardType.Security_Officer]);
    
    // playerHand.AddCard(securityOfficerCard);

    // var jUnitSweeperCard = cardScene.Instantiate<ThreeDCard>();
    // jUnitSweeperCard.cardData = GD.Load<CardData>(CardList.cardLookup[CardType.J_Unit_Sweeper]);
    // playerHand.AddCard(jUnitSweeperCard);
  
    playerHand.SetAcceptingInput(true);
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
    if(selectedCard.cardData.CardCost > slot.lightbulbCount || slot.occupiedCard != null)
    {
      GD.Print("Unable to play card in this slot. Not enough lightbulbs.");
      return;
    }

    GD.Print(slot.lightbulbCount + " < " + selectedCard.cardData.CardCost);
    
    GD.Print("Attempting to place card");
    selectedCard.GlobalPosition = slot.GetCardPosition();
    slot.SetOccupiedCard(selectedCard);
    gameBoard.UpdateLightbulbCount(selectedCard.cardData.CostIncreaseTilesAsString, slotIndex);
    gameBoard.RecalculateScores();
    playerHand.RemoveCard(selectedCard);

    DrawNewCard();

    // TOOD: Move to the CPU's turn
    playerHand.SetAcceptingInput(true);
    gameBoard.SetAcceptingInput(false);
    GD.Print("Card placed");
    selectedCard = null;
  }

  private void HandleCancelSelection()
  {
    playerHand.SetAcceptingInput(true);
    gameBoard.SetAcceptingInput(false);
  }

  private void DrawNewCard()
  {

  }
}
