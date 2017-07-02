using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractionsEditModeNothingSelectedMenu : MonoBehaviour
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

  //protected methods

  //private methods

  //protected fields

  //private fields
}
