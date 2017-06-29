using UnityEngine;

public class UIInteractionAddFurnitureMenu : MonoBehaviour
{
  //===========================================================================
  public void OnButtonClose ()
  {
    MenuManager.Instance.EnterMenuMode(MenuManager.MenuMode.InGameMenu);
    MenuManager.Instance.HandleHideMenu();
  }
}