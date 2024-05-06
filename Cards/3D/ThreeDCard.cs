using Godot;
using System;

public partial class ThreeDCard : Node3D
{
  [Export]
  private CardData cardData {get; set;}

  private ThreeDSquares squares;
  private Label3D cardNameLabel;
  private Sprite3D[] pawns = new Sprite3D[3];
  private Label3D pointLabel;
  private Sprite3D cardArt;

  public override void _Ready() {
    squares = GetNode<ThreeDSquares>("3DSquares");
    cardNameLabel = GetNode<Label3D>("CardNameLabel");
    
    pawns[0] = GetNode<Sprite3D>("Pawns/Pawn1");
    pawns[1] = GetNode<Sprite3D>("Pawns/Pawn2");
    pawns[2] = GetNode<Sprite3D>("Pawns/Pawn3");

    pointLabel = GetNode<Label3D>("PointLabel");
    cardArt = GetNode<Sprite3D>("CardArt");
  }

  private void SetInitialCardProperties()
  {
    if(cardData == null)
    {
      GD.PrintErr("CardData is null");
      return;
    }

    squares.ColorTiles(cardData.CostIncreaseTiles);
    cardNameLabel.Text = cardData.CardName;
    SetAmountOfPawns(cardData.CardCost);
    pointLabel.Text = cardData.CardValue.ToString();
    cardArt.Texture = cardData.CardArt;
  }

  private void SetAmountOfPawns(int pawnCount)
  {
    for (int i = 0; i < pawns.Length; i++)
    {
      pawns[i].Visible = i < pawnCount;
    }
  }
}
