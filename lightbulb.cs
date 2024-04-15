using Godot;
using System;

public partial class lightbulb : MeshInstance3D
{ 
  [Export]
  public BaseMaterial3D RedMaterial {get; set;}
  [Export]
  public BaseMaterial3D GreenMaterial {get; set;}
  public override void _Ready() 
  {
    GD.Print("Hello, World!");

    this.Mesh.SurfaceSetMaterial(0, RedMaterial);
  }

  public void SetMaterial(bool isPlayer)
  {
    if(isPlayer)
      this.Mesh.SurfaceSetMaterial(0, GreenMaterial);
    else
      this.Mesh.SurfaceSetMaterial(0, RedMaterial);
  }
}
