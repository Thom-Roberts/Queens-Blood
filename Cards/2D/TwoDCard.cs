using Godot;
using System;

public partial class TwoDCard : Control
{
  [Export]
  private CardData cardData {get; set;}
  private TextureRect[] pawns = new TextureRect[3];
  private Label pointLabel;

  public override void _Ready()
  {
    pawns[0] = GetNode<TextureRect>("Pawns/Pawn1");
    pawns[1] = GetNode<TextureRect>("Pawns/Pawn2");
    pawns[2] = GetNode<TextureRect>("Pawns/Pawn3");

    pointLabel = GetNode<Label>("PointLabel");
  }

  private void SetInitialCardProperties()
  {
    pointLabel.Text = cardData.CardValue.ToString();
    for (int i = 0; i < pawns.Length; i++)
    {
      pawns[i].Visible = i < cardData.CardCost;
    }
  }
}
