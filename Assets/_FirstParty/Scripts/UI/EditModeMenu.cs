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
  public void ShowSwapObject ()
  {
    MenuManager.Instance.PushMenu(_swapObjectMenu);
    MenuManager.Instance.ShowTopMenu();
  }

  //===========================================================================
  public override void SetUpButtons (RM2_InteractableObject interactable)
  {
    if (interactable == null)
    {
      return;
    }

    SetUpButton<ChangeColorEditAction>(interactable, _colorButton);
    SetUpButton<ChangeMaterialEditAction>(interactable, _materialButton);
    SetUpButton<SwapObjectEditAction>(interactable, _swapButton);
    SetUpButton<RemoveObjectEditAction>(interactable, _removeButton);
    SetUpButton<OnOffEditAction>(interactable, _onOffButton);
  }

  //protected methods

  //private methods
  //===========================================================================
  private void SetUpButton<T> (RM2_InteractableObject interactable, Button button)
  {
    button.gameObject.SetActive(false);
    T component = interactable.GetComponent<T>();
    if (component == null)
    {
      return;
    }
    button.gameObject.SetActive(true);
  }

  //protected fields

  //private fields
  [SerializeField]
  private BaseMenu _pickColorMenu = null;

  [SerializeField]
  private BaseMenu _pickMaterialMenu = null;

  [SerializeField]
  private BaseMenu _swapObjectMenu = null;

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