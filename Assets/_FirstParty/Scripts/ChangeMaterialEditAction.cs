using System;
using UnityEngine;

public class ChangeMaterialEditAction : BaseEditAction
{
  [Serializable]
  public class MaterialChoice
  {
    public Material _material = null;
    public String _name;
  }

  //===================================
  public MaterialChoice[] MaterialChoicesCopies
  {
    get { return _materialChoicesCopies; }
    set { _materialChoicesCopies = value; }
  }

  //===================================
  public MaterialChoice OriginalMaterial
  {
    get { return _originalMaterial; }
    set { _originalMaterial = value; }
  }

  //===========================================================================
  public void Start ()
  {
    RM2_InteractableObject interactableObject = GetComponent<RM2_InteractableObject>();
    if (interactableObject == null)
    {
      Debug.LogError("ChangeMaterialEditAction.Start (), interactableObject == null");
    }

    OriginalMaterial = new MaterialChoice();
    OriginalMaterial._material = new Material(interactableObject.OriginalMaterialCopy);
    OriginalMaterial._name = "Original";

    _materialChoicesCopies = new MaterialChoice[_materialChoices.Length];

    for (int i = 0; i < _materialChoices.Length; i++)
    {
      _materialChoicesCopies[i] = new MaterialChoice();
      _materialChoicesCopies[i]._material = new Material(_materialChoices[i]._material);
      _materialChoicesCopies[i]._name = _materialChoices[i]._name;
    }
  }

  [SerializeField]
  private MaterialChoice[] _materialChoices = null;

  [SerializeField]
  public float _buttonTilingX = 1.0f;

  [SerializeField]
  public float _buttonTilingY = 1.0f;

  private MaterialChoice[] _materialChoicesCopies = null;
  private MaterialChoice _originalMaterial = null;
}