using VRTK;
using UnityEngine;

public class RM2_InteractableObject : VRTK_InteractableObject
{
  //Edit mode actions.
  public enum EditModeActions
  {
    Remove = 0,
    Swap, 
    Color,
    OnOff,
    Material,
  }; 

  //===================================
  public bool ForceHightLight
  {
    get { return _forceHightlight; }
    set { _forceHightlight = value; }
  }

  //===========================================================================
  public bool HasEditModeAction (EditModeActions action)
  {
    foreach (EditModeActions a in _editModeActions)
    {
      if (a == action)
      {
        return true;
      }
    }
    return false;
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

  [SerializeField]
  private EditModeActions[] _editModeActions;
}