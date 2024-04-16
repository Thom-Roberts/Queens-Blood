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
    this.Mesh.SurfaceSetMaterial(0, RedMaterial);
  }

  public void SetMaterial(bool isPlayer)
  {
    GD.Print("Setting material");
    if(isPlayer)
      this.MaterialOverride = GreenMaterial;
      // this.Mesh.SurfaceSetMaterial(0, GreenMaterial);
    else
      this.MaterialOverride = RedMaterial;
      // this.Mesh.SurfaceSetMaterial(0, RedMaterial);
  }
}
