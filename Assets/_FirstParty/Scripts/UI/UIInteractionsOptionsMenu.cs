using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInteractionsOptionsMenu : MonoBehaviour
{
  //===========================================================================
  public void OnButtonPressedBack ()
  {
    SceneManager.LoadScene("MainMenu");
  }
}
