using Godot;
using Godot.Collections;

public partial class main : Node
{
  public Array<ThreeDCard> deckCards = new Array<ThreeDCard>();
  private hand playerHand;
  private board gameBoard;

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
    // for(int i = 0; i < 15; i++)
    // {
    //   ThreeDCard card = (ThreeDCard)cardScene.NativeInstance();
    //   card.SetCardValue(i);
    //   deckCards.Add(card);
    //   AddChild(card);
    //   card.Position = new Vector3(0, 0, 0);
    // }
  }

  private void HandleCardSelection(ThreeDCard card)
  {
    GD.Print("Card selected: " + card.cardData.CardName);
  }

  private void HandleSlotSelection()
  {
    GD.Print("Slot selected");
  }
}
