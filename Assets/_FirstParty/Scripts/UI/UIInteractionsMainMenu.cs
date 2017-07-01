using UnityEngine;
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
  public void OnButtonPressedOptions ()
  {
    SceneManager.LoadScene("OptionsMenu");
  }
}