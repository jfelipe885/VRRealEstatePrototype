using UnityEngine;

public class BaseMenu : MonoBehaviour
{
  //Constructors (in descending order of complexity)

  //public constants
  //===================================
  public enum MenuPositions
  {
    InFrontUser = 0,
    InFrontInteractable,
  }

  //properties
  //===================================
  public MenuPositions MenuPosition
  {
    get { return _menuPosition; }
    private set { _menuPosition = value; }
  }

  //public methods
  //===========================================================================
  public virtual void SetUpButtons (RM2_InteractableObject interactable) { }

  //protected methods
  //private methods
  //protected fields
  //private fields
  [SerializeField]
  private MenuPositions _menuPosition = MenuPositions.InFrontUser;
}