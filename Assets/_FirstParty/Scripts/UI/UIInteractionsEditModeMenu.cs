using UnityEngine;

public class UIInteractionsEditModeMenu : MonoBehaviour
{
  //Constructors(in descending order of complexity)
  //public constants
  //properties
  //public methods
  //===========================================================================
  public void OnButtonClose ()
  {
    MenuManager.Instance.EnterMenuMode(MenuManager.MenuMode.InGameMenu);
    MenuManager.Instance.HandleHideMenu();
  }

  //===========================================================================
  public void OnButtonRemove ()
  {
    GameObject currentInteractable = MenuManager.Instance.CurrentInteractable;
    if (currentInteractable == null)
    {
      return;
    }

    Destroy(currentInteractable);
    MenuManager.Instance.HandleHideMenu();
  }

  //===========================================================================
  public void OnChangeColor ()
  {

  }

  //===========================================================================
  public void OnButtonSwap ()
  {

  }

  //protected methods
  //private methods
  //protected fields
  //private fields
}