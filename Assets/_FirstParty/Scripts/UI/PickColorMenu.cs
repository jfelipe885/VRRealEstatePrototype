using UnityEngine;
using UnityEngine.UI;

public class PickColorMenu : BaseMenu
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
      Debug.LogError("SetUpButtons(), interactable == null");
      return;
    }

    ChangeColorEditAction changeColorEditAction = interactable.GetComponent<ChangeColorEditAction>();
    if (changeColorEditAction == null)
    {
      Debug.LogError("SetUpButtons(), changeColorEditAction == null");
      return;
    }

    if (_originalColorImage == null)
    {
      Debug.LogError("SetUpButtons(), _originalColorButton == null");
      return;
    }
    _originalColorImage.color = changeColorEditAction.OriginalColor._color;
    Text originalColorText = _originalColorImage.GetComponentInChildren<Text>();
    if (originalColorText != null)
    {
      originalColorText.text = changeColorEditAction.OriginalColor._name;
    }

    if (changeColorEditAction.ColorChoices.Length > _colorButtons.Length)
    {
      Debug.LogWarning("changeColorEditAction._colorChoices.Length > _colorButtons.Length");
    }

    for (int i = 0; i < _colorButtons.Length; i++)
    {
      if (i >= changeColorEditAction.ColorChoices.Length)
      {
        _colorButtons[i].gameObject.SetActive(false);
        continue;
      }
      _colorButtons[i].image.color = changeColorEditAction.ColorChoices[i]._color;
      Text colorButtonText = _colorButtons[i].GetComponentInChildren<Text>();
      if (colorButtonText != null)
      {
        colorButtonText.text = changeColorEditAction.ColorChoices[i]._name;
      }
    }
  }

  //protected methods
  //private methods
  //protected fields
  //private fields
  [SerializeField]
  private Image _originalColorImage = null;

  [SerializeField]
  private Button[] _colorButtons = null;
}