using System.Collections;
using System.Collections.Generic;
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
