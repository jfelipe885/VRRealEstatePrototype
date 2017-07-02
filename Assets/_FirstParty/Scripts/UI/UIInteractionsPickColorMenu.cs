using UnityEngine;
using UnityEngine.UI;

public class UIInteractionsPickColorMenu : MonoBehaviour
{
  //Constructors (in descending order of complexity)
  //public constants
  //properties
  //public methods
  //===========================================================================
  public void OnColorButtonClicked (Image image)
  {
    if (image == null)
    {
      Debug.LogError("OnColorButtonClicked () image == null ");
      return;
    }

    RM2_InteractableObject lockedInteractable = MenuManager.Instance.LockedInteractableObject.GetComponent<RM2_InteractableObject>();
    if (lockedInteractable == null)
    {
      return;
    }

    lockedInteractable.ChangeColor(image.color);
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