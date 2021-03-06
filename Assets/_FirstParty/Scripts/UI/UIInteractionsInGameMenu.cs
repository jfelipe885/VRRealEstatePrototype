﻿using AC.TimeOfDaySystemFree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIInteractionsInGameMenu : MonoBehaviour
{
  //Constructors(in descending order of complexity)

  //public constants

  //properties

  //public methods
  //===========================================================================
  public void OnButtonMainMenu ()
  {
    SceneManager.LoadScene("MainMenu");
  }

  //===========================================================================
  public void OnSliderTimeOfDayChange (float value)
  {
    if (_timeOfDaySlider == null)
    {
      return;
    }
    if (_timeOfDayManager == null)
    {
      return;
    }
    _timeOfDayManager.timeline = _timeOfDaySlider.value;
  }

  //===========================================================================
  public void OnButtonEdit ()
  {
    MenuManager.Instance.EnterMenuMode(MenuManager.MenuMode.EditMode);
  }

  //===========================================================================
  public void OnButtonAddFurniture ()
  {
    //MenuManager.Instance.HideInGameMenu();
    MenuManager.Instance.EnterMenuMode(MenuManager.MenuMode.AddFurniture);
    MenuManager.Instance.ShowTopMenu();
  }

  //protected methods
  //private methods
  //protected fields

  //private fields
  [SerializeField]
  private TimeOfDayManager _timeOfDayManager = null;

  [SerializeField]
  private Slider _timeOfDaySlider = null;
}