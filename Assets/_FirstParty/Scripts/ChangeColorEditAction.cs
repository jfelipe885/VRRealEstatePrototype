using System;
using UnityEngine;

public class ChangeColorEditAction : BaseEditAction
{
  [Serializable]
  public class ColorChoice
  {
    public Color _color;
    public String _name;
  }

  //===================================
  public ColorChoice[] ColorChoices
  {
    get { return _colorChoices; }
    set { _colorChoices = value; }
  }

  //===================================
  public ColorChoice OriginalColor
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

    OriginalColor._color = interactableObject.OriginalColor;
    //JFR: TODO: localize this string
    OriginalColor._name = "Original";
  }

  [SerializeField]
  private ColorChoice[] _colorChoices = null;

  private ColorChoice _originalColor = new ColorChoice();
}