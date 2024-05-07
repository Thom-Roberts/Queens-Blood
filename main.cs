using Godot;
using Godot.Collections;
using System;

public partial class main : Node
{
  public Array<ThreeDCard> deckCards = new Array<ThreeDCard>();
  private hand playerHand;

  public override void _Ready()
  {
    // Called every time the node is added to the scene.
    // Initialization here
    // Load the deck
    playerHand = GetNode<hand>("Hand");
    LoadDeck();
  }

  private void LoadDeck()
  {
    // Load the deck
    PackedScene cardScene = GD.Load<PackedScene>("res://Cards/3D/3d_card.tscn");
    var securityOfficerCard = cardScene.Instantiate<ThreeDCard>();
    securityOfficerCard.cardData = GD.Load<CardData>(CardList.cardLookup[CardType.Security_Officer]);
    
    playerHand.AddCard(securityOfficerCard);
    // for(int i = 0; i < 15; i++)
    // {
    //   ThreeDCard card = (ThreeDCard)cardScene.NativeInstance();
    //   card.SetCardValue(i);
    //   deckCards.Add(card);
    //   AddChild(card);
    //   card.Position = new Vector3(0, 0, 0);
    // }
  }
}
