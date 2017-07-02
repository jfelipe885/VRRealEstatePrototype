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

    if (_originalMaterialImage == null)
    {
      Debug.LogError("SetUpButtons(), _originalColorButton == null");
      return;
    }

    if (interactable == null)
    {
      Debug.LogError("SetUpButtons(), interactable == null");
      return;
    }

    _originalMaterialImage.material = interactable.OriginalMaterial;
  }
  //protected methods
  //private methods
  //protected fields
  //private fields
  [SerializeField]
  private Image _originalMaterialImage = null;
}