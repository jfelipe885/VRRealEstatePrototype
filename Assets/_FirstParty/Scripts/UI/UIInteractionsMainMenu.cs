using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIInteractionsMainMenu : MonoBehaviour
{
  //===========================================================================
  public void OnButtonPressedEnterFloorPlan ()
  {
    Debug.LogWarning("Options Button Pressed");
    SceneManager.LoadScene("FloorPlan");
  }

  //===========================================================================
  public void OnButtonPressedLoadSetUp ()
  {
    SceneManager.LoadScene("LoadMenu");
  }

  //===========================================================================
  public void OnButtonPressedOptions()
  {
    SceneManager.LoadScene("OptionsMenu");
  }
}
