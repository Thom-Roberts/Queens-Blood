using Godot;
using System;
using System.Collections.Generic;

public partial class InputManager : Node
{
  private Dictionary<string, List<Action>> inputDictionary = new Dictionary<string, List<Action>>();

  public void Register(string inputName, Action method)
  {
    if (!inputDictionary.ContainsKey(inputName))
    {
      inputDictionary[inputName] = new List<Action>();
    }
    
    inputDictionary[inputName].Add(method);
  }

  public override void _Process(double delta)
  {
    base._Process(delta);

    foreach (string inputName in inputDictionary.Keys)
    {
      // GD.Print("keys: " + inputName);
      if (Input.IsActionJustPressed(inputName))
      {
        foreach (Action method in inputDictionary[inputName])
        {
          method();
        }
      }
    }
  }
}
