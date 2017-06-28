using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class MenuManager : Singleton<MenuManager>
{
  //Constructors(in descending order of complexity)
  //===========================================================================
  public void Awake()
  {
    if (_interactTouch != null)
    {
      _interactTouch.ControllerTouchInteractableObject += OnTouchEventHandler;
      _interactTouch.ControllerUntouchInteractableObject += OnUnTouchEventHandler;
    }
    
    //TODO: JFR: these need to be localized.    
    _modeText[(int)MenuMode.InGameMenu] = "";
    _modeText[(int)MenuMode.ContextSensitive] = "Editing Mode";
  }

  //===========================================================================
  public void Start ()
  {
    EnterMenuMode(MenuMode.InGameMenu);
  }

  public enum MenuMode
  {
    InGameMenu = 0,
    ContextSensitive,
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
  public void EnterMenuMode(MenuMode newMode)
  {
    if (_intearctPointer == null)
    {
      Debug.LogError("EnterMenuMode, _intearctPointer is null");
      return;
    }

    CurrentMenuMode = newMode;
    _currentModeText.text = _modeText[(int)newMode];

    _intearctPointer.interactWithObjects = (newMode == MenuMode.ContextSensitive);
  }

  //===========================================================================
  public void HandleShowMenu()
  {
    switch (CurrentMenuMode)
    {
      case MenuMode.InGameMenu:
        ShowInGameMenu();
        break;

      case MenuMode.ContextSensitive:
        ShowContextSensitiveMenu();
        break;
    }
  }

  //===========================================================================
  public void HandleHideMenu()
  {
    HideInGameMenu();
    HideContextSensitiveMenu();
  }

  //===========================================================================
  public void ShowInGameMenu()
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

    if (_currentModeText != null)
    {
      _currentModeText.text = "";
    }
  }

  //===========================================================================
  public void HideInGameMenu()
  {
    if (_inGameMenu != null)
    {
      _inGameMenu.SetActive(false);
    }
  }

  //===========================================================================
  public void ShowContextSensitiveMenu()
  {
    if (_contextSensitiveMenu == null)
    {
      return;
    }
    if (_currentInteractableObject == null)
    {
      return;
    }

    //let's set up the menu along the vector between the camera and the object
    Vector3 cameraToObjectVector = (_currentInteractableObject.transform.position - _camera.transform.position);
    _contextSensitiveMenu.transform.position = _camera.transform.position + cameraToObjectVector * _distanceToContextSensitiveMenu;
    //_contextSensitiveMenu.transform.LookAt(_camera.transform.position);
    _contextSensitiveMenu.transform.LookAt(_currentInteractableObject.transform.position);
    _contextSensitiveMenu.SetActive(true);

    RM2_InteractableObject interactable = _currentInteractableObject.GetComponent<RM2_InteractableObject>();
    if (interactable != null)
    {
      interactable.ForceHightLight = true;
    }
  }

  //===========================================================================
  public void HideContextSensitiveMenu()
  {
    if (_contextSensitiveMenu == null)
    {
      Debug.LogError("HideContextSensitiveMenu() _contextSensitiveMenu == null");
      return;
    }
    if (_currentInteractableObject == null)
    {
      Debug.LogError("HideContextSensitiveMenu() _currentInteractableObject == null");
      return;
    }

    _contextSensitiveMenu.SetActive(false);    
    RM2_InteractableObject interactable = _currentInteractableObject.GetComponent<RM2_InteractableObject>();
    if (interactable != null)
    {
      interactable.StopTouching(null);
      interactable.ForceHightLight = false;
      interactable.ToggleHighlight(false);
    }
  }

  //protected methods
  //private methods
  //===========================================================================
  private void OnTouchEventHandler(object sender, ObjectInteractEventArgs e)
  {
    _currentInteractableObject = e.target;
    return;
  }

  //===========================================================================
  private void OnUnTouchEventHandler(object sender, ObjectInteractEventArgs e)
  {
    //_currentInteractableObject = null;
    return;
  }

  //protected fields

  //private fields
  [SerializeField]
  private GameObject _inGameMenu = null;

  [SerializeField]
  private GameObject _contextSensitiveMenu = null;

  [SerializeField]
  private GameObject _camera = null;

  [SerializeField]
  private VRTK_InteractTouch _interactTouch = null;

  [SerializeField]
  private float _distanceToInGameMenu = 0.5f; //meters?

  [SerializeField]
  private float _yOffsetInGameMenu = 0.1f;

  [SerializeField]
  private float _distanceToContextSensitiveMenu = 0.25f; //meters?

  [SerializeField]
  private float _yOffsetContextSensitiveMenu = 0.1f;

  [SerializeField]
  private Text _currentModeText = null;

  [SerializeField]
  private VRTK_Pointer _intearctPointer = null;

  private GameObject _currentInteractableObject = null;

  private MenuMode _currentMenuMode = MenuMode.InGameMenu;

  private string[] _modeText = new string[(int)MenuMode.NumMenuModes];
}