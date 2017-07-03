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
  public GameObject TouchedInteractableObject
  {
    get { return _touchedInteractableObject; }
    private set { _touchedInteractableObject = value; }
  }

  //===================================
  public EditModeMenu EditMenu
  {
    get { return _editMenu; }
    private set { _editMenu = value; }
  }

  //===================================
  public GameObject LockedInteractableObject
  {
    get { return _lockedInteractableObject; }
    set { _lockedInteractableObject = value; }
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
        _intearctPointer.interactWithObjects = false;
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
        InFronUserTransform(topMenu.transform);
        break;

      case BaseMenu.MenuPositions.InFrontInteractable:
        InFrontTouchedInteractableTransform(topMenu.transform);
        break;

      case BaseMenu.MenuPositions.InFrontLockInteractable:
        InFrontLockedInteractableTransform(topMenu.transform);
        break;

      case BaseMenu.MenuPositions.AlongPointerVector:
        AlongPointerVectorTransform(topMenu.transform);
        break;

      case BaseMenu.MenuPositions.UseCurrentTransform:
        topMenu.transform.position = _currentTransform.position;
        topMenu.transform.rotation = _currentTransform.rotation;
        break;
    }


    RM2_InteractableObject interactable = (TouchedInteractableObject == null) ? 
      ((LockedInteractableObject == null) ? null : LockedInteractableObject.GetComponent<RM2_InteractableObject>())
      : TouchedInteractableObject.GetComponent<RM2_InteractableObject>();
    topMenu.SetUpButtons(interactable);    
    topMenu.gameObject.SetActive(true);
  }

  //===========================================================================
  public void HideTopMenu ()
  {
    if (_menuStack.Count == 0)
    {
      return;
    }

    BaseMenu topMenu = _menuStack.Peek();
    topMenu.gameObject.SetActive(false);
    if (topMenu.PopWhenHidden == true)
    {
      _menuStack.Pop();
    }
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
      _currentTransform = _menuStack.Peek().transform;
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
    if (_touchedInteractableObject == null)
    {
      PushMenu(_editMenuNothingTouched);
    }

    ShowTopMenu();
  }

  //===========================================================================
  public void InFronUserTransform (Transform inFrontUserTransform)
  {
    inFrontUserTransform.position = _camera.transform.position + (_camera.transform.forward * _distanceInFrontUser);
    inFrontUserTransform.position += (-inFrontUserTransform.up * _yOffsetInGameMenu);
    inFrontUserTransform.LookAt(inFrontUserTransform.transform.position + (_camera.transform.forward * _distanceInFrontUser));
  }

  //===========================================================================
  public void InFrontTouchedInteractableTransform (Transform menuTransform)
  {
    InFrontInteractable(menuTransform, _touchedInteractableObject);    
  }

  //===========================================================================
  public void InFrontLockedInteractableTransform (Transform menuTransform)
  {
    InFrontInteractable(menuTransform, LockedInteractableObject);
  }

  //===========================================================================
  public void AlongPointerVectorTransform (Transform menuTransform)
  {
    if (_intearctPointer == null)
    {
      Debug.LogWarning("AlongPointerVectorTransform() , _intearctPointer == null");
      return;
    }

    RaycastHit rayCastHit = _intearctPointer.pointerRenderer.GetDestinationHit();
    Vector3 vectorForMenuDisplay = (rayCastHit.point - _intearctPointer.transform.position);
    float distanceToInteractable = (vectorForMenuDisplay * _distanceMenuToInteractableFactor).magnitude;
    distanceToInteractable = Mathf.Clamp(distanceToInteractable, _minDistanceToInteractable, _maxDistanceToInteractable);
    menuTransform.position = _intearctPointer.transform.position + vectorForMenuDisplay.normalized * distanceToInteractable;
    menuTransform.LookAt(menuTransform.transform.position + vectorForMenuDisplay.normalized * distanceToInteractable);
  }

  //===========================================================================
  private void InFrontInteractable (Transform menuTransform, GameObject interactable)
  {
    //let's set up the menu along the vector between the camera and the object
    Vector3 vectorForMenuDisplay = vectorForMenuDisplay = (interactable.transform.position - _camera.transform.position);    
    float distanceToInteractable = (vectorForMenuDisplay * _distanceMenuToInteractableFactor).magnitude;
    distanceToInteractable =  Mathf.Clamp(distanceToInteractable, _minDistanceToInteractable, _maxDistanceToInteractable);    
    menuTransform.position = _camera.transform.position + vectorForMenuDisplay.normalized * distanceToInteractable;
    menuTransform.LookAt(interactable.transform.position);
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

    if (_touchedInteractableObject == null)
    {
      return;
    }
    RM2_InteractableObject interactable = _touchedInteractableObject.GetComponent<RM2_InteractableObject>();
    if (interactable != null)
    {
      interactable.StopTouching(null);
      interactable.ForceHightLight = false;
      interactable.ToggleHighlight(false);
    }
    _touchedInteractableObject = null;
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

  //===========================================================================
  public bool CurrentMenusLockInteractable ()
  {
    BaseMenu[] menuArray = _menuStack.ToArray();
    foreach (BaseMenu m in menuArray)
    {
      EditModeMenu editModeMenu = m.GetComponent<EditModeMenu>();
      if (editModeMenu != null)
      {
        return true;
      }
    }
    return false;
  }

  //protected methods
  //private methods
  //===========================================================================
  private void OnTouchEventHandler (object sender, ObjectInteractEventArgs e)
  {
    _touchedInteractableObject = e.target;
    return;
  }

  //===========================================================================
  private void OnUnTouchEventHandler (object sender, ObjectInteractEventArgs e)
  {
    _touchedInteractableObject = null;
    return;
  }

  //protected fields

  //private fields
  [SerializeField]
  private BaseMenu _inGameMenu = null;

  [SerializeField]
  private EditModeMenu _editMenu = null;

  [SerializeField]
  private BaseMenu _editMenuNothingTouched = null;

  [SerializeField]
  private BaseMenu _addFurnitureMenu = null;

  [SerializeField]
  private GameObject _camera = null;

  [SerializeField]
  private VRTK_InteractTouch _interactTouch = null;

  [SerializeField]
  private float _distanceInFrontUser = 0.75f; //Meters

  [SerializeField]
  private float _yOffsetInGameMenu = 0.1f;

  [SerializeField]
  private float _distanceMenuToInteractableFactor = 0.25f;

  [SerializeField]
  private float _maxDistanceToInteractable = 0.75f; //Meters
  [SerializeField]
  private float _minDistanceToInteractable = 0.1f; //Meters

  [SerializeField]
  private Text _currentModeText = null;

  [SerializeField]
  private VRTK_Pointer _intearctPointer = null;
  
  //Object that our pointer is currently touching.
  private GameObject _touchedInteractableObject = null;

  //Object that we have locked on that we are currently editing.
  private GameObject _lockedInteractableObject = null;

  private MenuMode _currentMenuMode = MenuMode.InGameMenu;

  private string[] _modeText = new string[(int)MenuMode.NumMenuModes];

  private Stack<BaseMenu> _menuStack = new Stack<BaseMenu>(10);

  private Transform _currentTransform = null;
}