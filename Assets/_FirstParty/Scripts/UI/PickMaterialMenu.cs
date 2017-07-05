using UnityEngine;
using UnityEngine.UI;

public class PickMaterialMenu : BaseMenu
{
  //Constructors (in descending order of complexity)
  //public constants
  //properties
  //public methods
  //===========================================================================

  public override void SetUpButtons (RM2_InteractableObject interactable)
  {
    base.SetUpButtons(interactable);

    if (interactable == null)
    {
      Debug.LogError("PickMaterialMenu.SetUpButtons(), interactable == null");
      return;
    }

    ChangeMaterialEditAction changeMaterialEditAction = interactable.GetComponent<ChangeMaterialEditAction>();
    if (changeMaterialEditAction == null)
    {
      Debug.LogError("PickMaterialMenu.SetUpButtons(), changeMaterialEditAction == null");
      return;
    }

    if (_originalMaterialImage == null)
    {
      Debug.LogError("SetUpButtons(), _originalColorButton == null");
      return;
    }
    _originalMaterialImage.material = changeMaterialEditAction.OriginalMaterial._material;
    _originalMaterialImage.material.mainTextureScale = new Vector2(changeMaterialEditAction._buttonTilingX, changeMaterialEditAction._buttonTilingY);
    Text originalMaterialText = _originalMaterialImage.GetComponentInChildren<Text>();
    if (originalMaterialText != null)
    {
      originalMaterialText.text = changeMaterialEditAction.OriginalMaterial._name;
    }

    if (changeMaterialEditAction.MaterialChoicesCopies.Length > _materialButtons.Length)
    {
      Debug.LogWarning("changeColorEditAction._colorChoices.Length > _colorButtons.Length");
    }

    for (int i = 0; i < _materialButtons.Length; i++)
    {
      if (i >= changeMaterialEditAction.MaterialChoicesCopies.Length)
      {
        _materialButtons[i].gameObject.SetActive(false);
        continue;
      }
      _materialButtons[i].image.material = changeMaterialEditAction.MaterialChoicesCopies[i]._material;
      _materialButtons[i].image.material.mainTextureScale = new Vector2(changeMaterialEditAction._buttonTilingX, changeMaterialEditAction._buttonTilingY);

      Text materialText = _materialButtons[i].GetComponentInChildren<Text>();
      if (materialText != null)
      {
        materialText.text = changeMaterialEditAction.MaterialChoicesCopies[i]._name;
      }
    }
  }

  //protected methods
  //private methods
  //protected fields
  //private fields
  [SerializeField]
  private Image _originalMaterialImage = null;

  [SerializeField]
  private Button[] _materialButtons = null;
}