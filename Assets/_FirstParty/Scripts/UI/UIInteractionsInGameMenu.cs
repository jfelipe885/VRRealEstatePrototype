using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AC.TimeOfDaySystemFree;
using UnityEngine.UI;

public class UIInteractionsInGameMenu : MonoBehaviour
{
  [SerializeField]
  private TimeOfDayManager _timeOfDayManager = null;

  [SerializeField]
  private Slider _timeOfDaySlider = null;

  //===========================================================================
  public void OnButtonMainMenu ()
  {
    SceneManager.LoadScene("MainMenu");
  }

  //===========================================================================
  public void OnTimeOfDaySliderChange (float value)
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
}
