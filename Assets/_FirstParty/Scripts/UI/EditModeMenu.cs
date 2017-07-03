using UnityEngine;
using UnityEngine.UI;

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
  public void ShowPickMaterial ()
  {
    MenuManager.Instance.PushMenu(_pickMaterialMenu);
    MenuManager.Instance.ShowTopMenu();
  }

  //===========================================================================
  public override void SetUpButtons (RM2_InteractableObject interactable)
  {
    if (interactable == null)
    {
      return;
    }

    ChangeColorEditAction changeColorEditAction = interactable.GetComponent<ChangeColorEditAction>();
    if (_colorButton != null)
    {
      _colorButton.gameObject.SetActive(false);
      if(changeColorEditAction != null)
      {
        _colorButton.gameObject.SetActive(true);
      }
    }

    ChangeMaterialEditAction changeMaterialEditAction = interactable.GetComponent<ChangeMaterialEditAction>();
    if (_materialButton != null)
    {
      _materialButton.gameObject.SetActive(false);
      if (changeMaterialEditAction != null)
      {
        _materialButton.gameObject.SetActive(true);
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
  private BaseMenu _pickMaterialMenu = null;

  //JFR: TODO: need to implement these
  [SerializeField]
  private Button _removeButton = null;

  [SerializeField]
  private Button _swapButton = null;

  [SerializeField]
  private Button _colorButton = null;

  [SerializeField]
  private Button _onOffButton = null;

  [SerializeField]
  private Button _materialButton = null;
}