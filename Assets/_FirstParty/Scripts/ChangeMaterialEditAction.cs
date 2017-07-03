using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialEditAction : BaseEditAction
{
  //===================================
  public Material[] MaterialChoicesCopies
  {
    get { return _materialChoicesCopies; }
    set { _materialChoicesCopies = value; }
  }

  //===================================
  public Material OriginalMaterial
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

    OriginalMaterial = new Material(interactableObject.OriginalMaterialCopy);

    _materialChoicesCopies = new Material[_materialChoices.Length];

    for (int i = 0; i < _materialChoices.Length; i++)
    {
      _materialChoicesCopies[i] = new Material(_materialChoices[i]);
    }    
  }

  [SerializeField]
  private Material[] _materialChoices = null;

  private Material[] _materialChoicesCopies = null;

  [SerializeField]
  public float _buttonTilingX = 1.0f;

  [SerializeField]
  public float _buttonTilingY = 1.0f;

  private Material _originalMaterial = null;


}
