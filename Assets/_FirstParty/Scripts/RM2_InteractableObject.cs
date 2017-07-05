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
  public Material OriginalMaterialCopy
  {
    get { return _originalMaterialCopy; }
    private set { _originalMaterialCopy = value; }
  }

  //===================================
  public Color OriginalColor
  {
    get { return _originalColor; }
    private set { _originalColor = value; }
  }

  //public methods

  //===========================================================================
  override protected void Awake ()
  {
    base.Awake();
    Renderer renderer = GetComponent<Renderer>();
    if (renderer != null)
    {
      //this actually makes a copy of the material
      _originalMaterialCopy = new Material(renderer.material);
      _originalColor = renderer.material.color;
    }
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
    //TODO: JFR: this seems pretty hacky. we might find a better way to do this later. or at least get rid of the MenuManager here
    if (MenuManager.Instance.LockedInteractableObject != null)
    {
      if (MenuManager.Instance.LockedInteractableObject != this.gameObject)
      {
        return;
      }
    }

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

  private Color _originalColor = new Color();
  private Material _originalMaterialCopy = null;
}