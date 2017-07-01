using System.Collections.Generic;
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
    PushMenu(_inGameMenu);
  }

  //public constants
  //===================================
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

  //===================================
  public GameObject CurrentInteractable
  {
    get { return _currentInteractableObject; }
    private set { _currentInteractableObject = value; }
  }

  //===================================
  public EditModeMenu EditMenu
  {
    get { return _editMenu; }
    private set { _editMenu = value; }
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

    switch (newMode)
    {
      case MenuMode.EditMode:
        _intearctPointer.interactWithObjects = true;
        PushMenu(_editMenu);
        break;

      case MenuMode.InGameMenu:
        PushMenu(_inGameMenu);
        break;

      case MenuMode.AddFurniture:
        PushMenu(_addFurnitureMenu);
        break;
    }
  }

  //===========================================================================
  public void ShowTopMenu ()
  {
    if (_menuStack.Count == 0)
    {
      return;
    }

    BaseMenu topMenu = _menuStack.Peek();
    switch (topMenu.MenuPosition)
    {
      case BaseMenu.MenuPositions.InFrontUser:
        InGameMenuTransform(topMenu.transform);
        break;

      case BaseMenu.MenuPositions.InFrontInteractable:
        InteractableMenuTransform(topMenu.transform);
        break;
    }

    if (CurrentInteractable != null)
    {
      RM2_InteractableObject interactable = CurrentInteractable.GetComponent<RM2_InteractableObject>();
      topMenu.SetUpButtons(interactable);
      //TODO: JFR: we might need a better way to handle this
      interactable.ForceHightLight = true;
    }
    else
    {
      topMenu.SetUpButtons(null);
    }
    topMenu.gameObject.SetActive(true);
  }

  //===========================================================================
  public void HideTopMenu ()
  {
    if (_menuStack.Count == 0)
    {
      return;
    }
    _menuStack.Peek().gameObject.SetActive(false);
  }

  //===========================================================================
  public void PushMenu (BaseMenu menu)
  {
    if (menu == null)
    {
      Debug.LogError("ShowMenu() menu == null");
      return;
    }

    if (_menuStack.Count > 0)
    {
      _menuStack.Peek().gameObject.SetActive(false);
    }

    _menuStack.Push(menu);
  }

  //===========================================================================
  public void PopMenu ()
  {
    if (_menuStack.Count == 0)
    {
      return;
    }
    _menuStack.Pop().gameObject.SetActive(false);
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
    ShowTopMenu();
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

    ShowTopMenu();
  }

  //===========================================================================
  public void InGameMenuTransform (Transform inGameMenutransform)
  {
    inGameMenutransform.position = _camera.transform.position + (_camera.transform.forward * _distanceInFrontUser);
    inGameMenutransform.position += (-inGameMenutransform.up * _yOffsetInGameMenu);
    inGameMenutransform.LookAt(_inGameMenu.transform.position + (_camera.transform.forward * _distanceInFrontUser));
  }

  //===========================================================================
  public void InteractableMenuTransform (Transform menuTransform)
  {
    //let's set up the menu along the vector between the camera and the object
    Vector3 cameraToObjectVector = (_currentInteractableObject.transform.position - _camera.transform.position);
    menuTransform.position = _camera.transform.position + cameraToObjectVector * _distanceMenuToInteractable;
    menuTransform.LookAt(_currentInteractableObject.transform.position);
  }

  //===========================================================================
  public void HideEditModeMenu ()
  {
    if (_editMenu == null)
    {
      Debug.LogError("HideEditModeMenu() _editMenu == null");
      return;
    }

    if (_menuStack.Peek() != _editMenu)
    {
      Debug.LogWarning("HideEditModeMenu (), _menuStack.Peek() != _editMenu");
    }

    HideTopMenu();

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

    ShowTopMenu();
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
  private BaseMenu _inGameMenu = null;

  [SerializeField]
  private EditModeMenu _editMenu = null;

  [SerializeField]
  private BaseMenu _addFurnitureMenu = null;

  [SerializeField]
  private GameObject _camera = null;

  [SerializeField]
  private VRTK_InteractTouch _interactTouch = null;

  [SerializeField]
  private float _distanceInFrontUser = 0.75f; //meters?

  [SerializeField]
  private float _yOffsetInGameMenu = 0.1f;

  [SerializeField]
  private float _distanceMenuToInteractable = 0.25f; //meters?

  [SerializeField]
  private Text _currentModeText = null;

  [SerializeField]
  private VRTK_Pointer _intearctPointer = null;

  private GameObject _currentInteractableObject = null;

  private MenuMode _currentMenuMode = MenuMode.InGameMenu;

  private string[] _modeText = new string[(int)MenuMode.NumMenuModes];

  private Stack<BaseMenu> _menuStack = new Stack<BaseMenu>(10);
}