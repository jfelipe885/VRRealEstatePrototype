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

  //===================================
  public Material OriginalMaterial
  {
    get { return _originalMaterial; }
    private set { _originalMaterial = value; }
  }

  //===================================
  public Color OriginalColor
  {
    get { return _originalColor; }
    private set { _originalColor = value; }
  }

  //public methods

  //===========================================================================
  public void Start ()
  {
    Renderer renderer = GetComponent<Renderer>();
    if (renderer != null)
    {
      _originalMaterial = new Material(renderer.material);
      _originalColor = renderer.material.color;
    }
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
  public void ChangeMaterial (Material material)
  {
    Renderer renderer = GetComponent<Renderer>();
    if (renderer == null)
    {
      return;
    }

    renderer.material = material;
    renderer.material.mainTextureScale = new Vector2(1.0f, 1.0f);
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

  [SerializeField]
  private EditModeActions[] _editModeActions = null;

  private bool _forceHightlight = false;

  private Color _originalColor = new Color();

  private Material _originalMaterial = null;
}