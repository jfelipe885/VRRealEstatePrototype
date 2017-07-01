using UnityEngine;
using VRTK;

public class RM2_InteractableObject : VRTK_InteractableObject
{
  //Constructors (in descending order of complexity)

  //public constants
  //===================================
  public enum EditModeActions
  {
    Remove = 0,
    Swap,
    Color,
    OnOff,
    Material,
  };

  //properties
  //===================================
  public bool ForceHightLight
  {
    get { return _forceHightlight; }
    set { _forceHightlight = value; }
  }

  //public methods
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
  public void ChangeColor (Color color)
  {
    Renderer renderer = GetComponent<Renderer>();
    if (renderer == null)
    {
      return;
    }

    Material[] materials = renderer.materials;

    foreach (Material m in materials)
    {
      m.color = color;
    }
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

  //protected methods
  //private methods
  //protected fields

  //private fields
  private bool _forceHightlight = false;

  [SerializeField]
  private EditModeActions[] _editModeActions = null;
}