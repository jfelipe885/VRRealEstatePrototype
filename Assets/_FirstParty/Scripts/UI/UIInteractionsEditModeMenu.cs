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
  }

  //===========================================================================
  public void OnChangeColor ()
  {
    if (MenuManager.Instance.EditMenu == null)
    {
      return;
    }

    MenuManager.Instance.EditMenu.ShowPickColor();
  }

  //===========================================================================
  public void OnButtonSwap ()
  {
  }

  //===========================================================================
  public void OnMaterialButtonClicked ()
  {
    if (MenuManager.Instance.EditMenu == null)
    {
      return;
    }

    MenuManager.Instance.EditMenu.ShowPickMaterial();
  }

  //protected methods

  //private methods

  //protected fields

  //private fields
}