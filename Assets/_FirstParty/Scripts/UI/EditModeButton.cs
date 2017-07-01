using UnityEngine;

public class EditModeButton : MonoBehaviour
{
  //===================================
  public RM2_InteractableObject.EditModeActions EditModeAction
  {
    get { return _editModeAction; }
    private set { _editModeAction = value; }
  }

  [SerializeField]
  private RM2_InteractableObject.EditModeActions _editModeAction = RM2_InteractableObject.EditModeActions.Remove;
}