using Godot;

/**
* Card increase tiles are done with a 5x5 grid, with the center tile being the card itself.
* The tiles are numbered as follows:
*  0  1  2  3  4
*  5  6  7  8  9
* 10 11 12 13 14
* 15 16 17 18 19
* 20 21 22 23 24
*/

/**
* Card cost will use the value 0 for a replacement tile
*/

public enum CardEffect
{
  None,
  IncreaseCost,
  IncreasePower,
  LowerPower,
  AddCardsToHand,
}

[GlobalClass]
public partial class CardData : Resource
{
  [Export]
  public string CardName { get; set; }
  [Export]
  public int CardCost {get; set;}
  [Export]
  public int CardValue { get; set; }
  [Export]
  public int[] CostIncreaseTiles {get; set;}
  [Export]
  public CardEffect Effect {get; set;}
  [Export]
  public int[] SpecialEffectTiles {get; set;}

  // Make sure you provide a parameterless constructor.
  // In C#, a parameterless constructor is different from a
  // constructor with all default values.
  // Without a parameterless constructor, Godot will have problems
  // creating and editing your resource via the inspector.
  public CardData() : this("", 0, 0, null, CardEffect.None, null) {}

  public CardData(string cardName, int cardCost, int cardValue, int[] costIncreaseTiles, CardEffect effect, int[] specialEffectTiles)
  {
    CardName = cardName;
    CardCost = cardCost;
    CardValue = cardValue;
    CostIncreaseTiles = costIncreaseTiles ?? System.Array.Empty<int>();
    Effect = effect;
    SpecialEffectTiles = specialEffectTiles ?? System.Array.Empty<int>();
  }
}
