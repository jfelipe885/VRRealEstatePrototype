using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditModeMenu : MonoBehaviour
{
  //===========================================================================
  public void SetUpButtons (RM2_InteractableObject interactable)
  {
    if (interactable == null)
    {
      return;
    }

    foreach (EditModeButton button in _editModeButtons)
    {
      button.gameObject.SetActive(false);
      if (interactable.HasEditModeAction(button.EditModeAction) == true)
      {
        button.gameObject.SetActive(true);
      }
    }
  }

  [SerializeField]
  private EditModeButton[] _editModeButtons;
}
