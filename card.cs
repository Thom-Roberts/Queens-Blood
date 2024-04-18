using Godot;

public partial class card : Node3D
{
  [Export]
  private CardData cardData {get; set;}

  [Export]
  public Label3D CardValueLabel { get; set; }

  public override void _Ready()
  {
    // Do something with cardData
    CardValueLabel.Text = cardData.CardValue.ToString();
  }
}
