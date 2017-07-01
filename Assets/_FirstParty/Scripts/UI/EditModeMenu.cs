using UnityEngine;

public class EditModeMenu : BaseMenu
{
  //Constructors (in descending order of complexity)

  //public constants

  //properties

  //public methods
  //===========================================================================
  public void ShowPickColor ()
  {
    MenuManager.Instance.PushMenu(_pickColorMenu);
    MenuManager.Instance.ShowTopMenu();
  }

  //===========================================================================
  public override void SetUpButtons (RM2_InteractableObject interactable)
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

  //protected methods

  //private methods

  //protected fields

  //private fields
  [SerializeField]
  private BaseMenu _pickColorMenu = null;

  [SerializeField]
  private GameObject _pickMaterialMenu = null;

  [SerializeField]
  private EditModeButton[] _editModeButtons = null;
}