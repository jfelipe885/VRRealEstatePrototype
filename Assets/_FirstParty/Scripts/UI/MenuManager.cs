using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class MenuManager : Singleton<MenuManager>
{
  //Constructors(in descending order of complexity)
  //===========================================================================
  public void Awake ()
  {
    if (_interactTouch != null)
    {
      _interactTouch.ControllerTouchInteractableObject += OnTouchEventHandler;
      _interactTouch.ControllerUntouchInteractableObject += OnUnTouchEventHandler;
    }

    //TODO: JFR: these need to be localized.
    _modeText[(int)MenuMode.InGameMenu] = "";
    _modeText[(int)MenuMode.EditMode] = "Editing Mode";
    _modeText[(int)MenuMode.AddFurniture] = "Add Furniture Mode";
  }

  //===========================================================================
  public void Start ()
  {
    EnterMenuMode(MenuMode.InGameMenu);
  }

  public enum MenuMode
  {
    InGameMenu = 0,
    EditMode,
    AddFurniture,
    NumMenuModes,
  }

  //properties
  //===================================
  public MenuMode CurrentMenuMode
  {
    get { return _currentMenuMode; }
    private set { _currentMenuMode = value; }
  }

  //public methods
  //===========================================================================
  public void EnterMenuMode (MenuMode newMode)
  {
    if (_intearctPointer == null)
    {
      Debug.LogError("EnterMenuMode, _intearctPointer is null");
      return;
    }

    CurrentMenuMode = newMode;
    _currentModeText.text = _modeText[(int)newMode];

    _intearctPointer.interactWithObjects = (newMode == MenuMode.EditMode);
  }

  //===========================================================================
  public void HandleShowMenu ()
  {
    switch (CurrentMenuMode)
    {
      case MenuMode.InGameMenu:
        ShowInGameMenu();
        break;

      case MenuMode.EditMode:
        ShowEditModeMenu();
        break;

      case MenuMode.AddFurniture:
        ShowAddFurnitureMenu();
        break;
    }
  }

  //===========================================================================
  public void HandleHideMenu ()
  {
    HideInGameMenu();
    HideEditModeMenu();
    HideAddFurnitureMenu();
  }

  //===========================================================================
  public void ShowInGameMenu ()
  {
    if (_camera == null)
    {
      Debug.LogError("ShowInGameMenu () _camera is null");
      return;
    }

    if (_inGameMenu == null)
    {
      Debug.LogError("ShowInGameMenu () _inGameMenu is null");
      return;
    }

    //get the camera forward vector
    _inGameMenu.transform.position = _camera.transform.position + (_camera.transform.forward * _distanceToInGameMenu);
    _inGameMenu.transform.position += (-_inGameMenu.transform.up * _yOffsetInGameMenu);
    _inGameMenu.transform.LookAt(_inGameMenu.transform.position + (_camera.transform.forward * _distanceToInGameMenu));
    _inGameMenu.SetActive(true);
  }

  //===========================================================================
  public void HideInGameMenu ()
  {
    if (_inGameMenu != null)
    {
      _inGameMenu.SetActive(false);
    }
  }

  //===========================================================================
  public void ShowEditModeMenu ()
  {
    if (_editMenu == null)
    {
      Debug.LogError("ShowEditModeMenu () _editMenu == null");
      return;
    }
    if (_currentInteractableObject == null)
    {
      return;
    }

    //let's set up the menu along the vector between the camera and the object
    Vector3 cameraToObjectVector = (_currentInteractableObject.transform.position - _camera.transform.position);
    _editMenu.transform.position = _camera.transform.position + cameraToObjectVector * _distanceToEditModeMenu;    
    _editMenu.transform.LookAt(_currentInteractableObject.transform.position);
    _editMenu.gameObject.SetActive(true);

    RM2_InteractableObject interactable = _currentInteractableObject.GetComponent<RM2_InteractableObject>();
    if (interactable != null)
    {
      interactable.ForceHightLight = true;
      _editMenu.SetUpButtons(interactable);
    }
  }

  //===========================================================================
  public void HideEditModeMenu ()
  {
    if (_editMenu == null)
    {
      Debug.LogError("HideEditModeMenu() _editMenu == null");
      return;
    }
    _editMenu.gameObject.SetActive(false);

    if (_currentInteractableObject == null)
    {
      return;
    }
    RM2_InteractableObject interactable = _currentInteractableObject.GetComponent<RM2_InteractableObject>();
    if (interactable != null)
    {
      interactable.StopTouching(null);
      interactable.ForceHightLight = false;
      interactable.ToggleHighlight(false);
    }
  }

  //===========================================================================
  public void ShowAddFurnitureMenu ()
  {
    if (_camera == null)
    {
      Debug.LogError("ShowAddFurnitureMenu () _camera is null");
      return;
    }

    if (_addFurnitureMenu == null)
    {
      Debug.LogError("ShowAddFurnitureMenu () _addFurnitureMenu is null");
      return;
    }

    //get the camera forward vector
    _addFurnitureMenu.transform.position = _camera.transform.position + (_camera.transform.forward * _distanceToInGameMenu);
    _addFurnitureMenu.transform.position += (-_addFurnitureMenu.transform.up * _yOffsetInGameMenu);
    _addFurnitureMenu.transform.LookAt(_addFurnitureMenu.transform.position + (_camera.transform.forward * _distanceToInGameMenu));
    _addFurnitureMenu.SetActive(true);
  }

  //===========================================================================
  public void HideAddFurnitureMenu ()
  {
    if (_addFurnitureMenu != null)
    {
      _addFurnitureMenu.SetActive(false);
    }
  }

  //protected methods
  //private methods
  //===========================================================================
  private void OnTouchEventHandler (object sender, ObjectInteractEventArgs e)
  {
    _currentInteractableObject = e.target;
    return;
  }

  //===========================================================================
  private void OnUnTouchEventHandler (object sender, ObjectInteractEventArgs e)
  {
    //_currentInteractableObject = null;
    return;
  }

  //protected fields

  //private fields
  [SerializeField]
  private GameObject _inGameMenu = null;

  [SerializeField]
  private EditModeMenu _editMenu = null;

  [SerializeField]
  private GameObject _addFurnitureMenu = null;

  [SerializeField]
  private GameObject _camera = null;

  [SerializeField]
  private VRTK_InteractTouch _interactTouch = null;

  [SerializeField]
  private float _distanceToInGameMenu = 0.5f; //meters?

  [SerializeField]
  private float _yOffsetInGameMenu = 0.1f;

  [SerializeField]
  private float _distanceToEditModeMenu = 0.25f; //meters?

  [SerializeField]
  private float _yOffsetEditModeMenu = 0.1f;

  [SerializeField]
  private Text _currentModeText = null;

  [SerializeField]
  private VRTK_Pointer _intearctPointer = null;

  private GameObject _currentInteractableObject = null;

  private MenuMode _currentMenuMode = MenuMode.InGameMenu;

  private string[] _modeText = new string[(int)MenuMode.NumMenuModes];
}