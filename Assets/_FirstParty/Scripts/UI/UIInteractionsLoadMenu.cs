using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInteractionsLoadMenu : MonoBehaviour
{
  //===========================================================================
  public void OnButtonLoadSLot1()
  {
    //TODO: JFR: 
    Debug.LogWarning("TODO: Add load slot 1 function here");        
  }

  //===========================================================================
  public void OnButtonDeleteSLot1()
  {
    //TODO: JFR: 
    Debug.LogWarning("TODO: Add delete slot 1 function here");    
  }

  //===========================================================================
  public void OnButtonLoadSLot2()
  {
    //TODO: JFR: 
    Debug.LogWarning("TODO: Add load slot 2 function here");
  }

  //===========================================================================
  public void OnButtonDeleteSLot2()
  {
    //TODO: JFR: 
    Debug.LogWarning("TODO: Add delete slot 2 function here");
  }

  //===========================================================================
  public void OnButtonLoadSLot3()
  {
    //TODO: JFR: 
    Debug.LogWarning("TODO: Add load slot 3 function here");
  }

  //===========================================================================
  public void OnButtonDeleteSLot3()
  {
    //TODO: JFR: 
    Debug.LogWarning("TODO: Add delete slot 3 function here");
  }

  //===========================================================================
  public void OnButtonPressedBack()
  {
    SceneManager.LoadScene("MainMenu");
  }
}
