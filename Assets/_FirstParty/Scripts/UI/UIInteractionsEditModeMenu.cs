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
    GameObject lockedInteractable = MenuManager.Instance.LockedInteractableObject;
    if (lockedInteractable == null)
    {
      return;
    }

    Destroy(lockedInteractable);
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
  public void OnOnOFFButtonClicked ()
  {
    GameObject lockedInteractable = MenuManager.Instance.LockedInteractableObject;
    if (lockedInteractable == null)
    {
      return;
    }

    Light light = lockedInteractable.GetComponentInChildren<Light>();
    if (light == null)
    {
      return;
    }

    light.enabled = !light.enabled;
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