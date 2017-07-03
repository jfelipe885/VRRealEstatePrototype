using UnityEngine;

public class ChangeColorEditAction : BaseEditAction
{
  //===================================
  public Color[] ColorChoices
  {
    get { return _colorChoices; }
    set { _colorChoices = value; }
  }

  //===================================
  public Color OriginalColor
  {
    get { return _originalColor; }
    set { _originalColor = value; }
  }

  //===========================================================================
  public void Start ()
  {
    RM2_InteractableObject interactableObject = GetComponent<RM2_InteractableObject>();
    if (interactableObject == null)
    {
      Debug.LogError("ChangeColorEditAction.Start (), interactableObject == null");
    }

    OriginalColor = interactableObject.OriginalColor;
  }

  [SerializeField]
  private Color[] _colorChoices = null;

  private Color _originalColor = new Color();
}