using Godot;
using System;
using System.Collections.Generic;

public enum CardType
{
  Security_Officer,
  J_Unit_Sweeper,
  Queen_Bee,
  Levrikon,
  Elphadunk,
  Grasslands_Wolf,
  Mu,
  Magic_Pot
}

public static class CardList {
  public static Godot.Collections.Dictionary<CardType, string> cardLookup = new Godot.Collections.Dictionary<CardType, string>
  {
    {CardType.Security_Officer, "res://Cards/Data/Security_Officer.res"},
    {CardType.J_Unit_Sweeper, "res://Cards/Data/J_Unit_Sweeper.res"},
    {CardType.Queen_Bee, "res://Cards/Data/Queen_Bee.res"},
    {CardType.Levrikon, "res://Cards/Data/Levrikon.res"},
    {CardType.Elphadunk, "res://Cards/Data/Elphadunk.res"},
    {CardType.Grasslands_Wolf, "res://Cards/Data/Grasslands_Wolf.res"},
    {CardType.Mu, "res://Cards/Data/Mu.res"},
    {CardType.Magic_Pot, "res://Cards/Data/Magic_Pot.res"}
  };
}

