using UnityEngine;
using UnityEngine.UI;

public class UIInteractionsPickMaterialMenu : MonoBehaviour
{
  //Constructors (in descending order of complexity)
  //public constants
  //properties
  //public methods

  //===========================================================================
  public void OnMaterialButtonClicked (Image image)
  {
    if (image == null)
    {
      Debug.LogError("OnMaterialButtonClicked () image == null ");
      return;
    }

    RM2_InteractableObject lockedInteractable = MenuManager.Instance.LockedInteractableObject.GetComponent<RM2_InteractableObject>();
    if (lockedInteractable == null)
    {
      return;
    }

    lockedInteractable.ChangeMaterial(image.material);
  }

  //===========================================================================
  public void OnCloseButtonClicked ()
  {
    MenuManager.Instance.PopMenu();
    MenuManager.Instance.ShowTopMenu();
  }

  //protected methods
  //private methods
  //protected fields
  //private fields
}