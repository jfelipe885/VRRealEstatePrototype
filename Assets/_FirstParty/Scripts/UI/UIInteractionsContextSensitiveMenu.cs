using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AC.TimeOfDaySystemFree;
using UnityEngine.UI;

public class UIInteractionsContextSensitiveMenu : MonoBehaviour
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

  }

  //===========================================================================
  public void OnChangeColor ()
  {

  }

  //protected methods
  //private methods
  //protected fields
  //private fields    
}
