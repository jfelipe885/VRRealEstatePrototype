using VRTK;

public class RM2_InteractableObject : VRTK_InteractableObject
{
  public bool ForceHightLight
  {
    get { return _forceHightlight; }
    set { _forceHightlight = value; }
  }

  //===========================================================================
  public override void ToggleHighlight (bool toggle)
  {
    if (ForceHightLight == true)
    {
      toggle = true;
    }
    base.ToggleHighlight(toggle);
  }

  private bool _forceHightlight = false;
}