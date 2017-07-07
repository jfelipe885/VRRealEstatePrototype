using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractionsSwapObjectMenu : MonoBehaviour
{
  //Constructors (in descending order of complexity)
  //public constants
  //properties
  //public methods

  //===========================================================================
  public void OnObjectSwapButtonClicked (int swapObjectIndex)
  {
    if (MenuManager.Instance.LockedInteractableObject == null)
    {
      return;
    }
    RM2_InteractableObject lockedInteractable = MenuManager.Instance.LockedInteractableObject.GetComponent<RM2_InteractableObject>();
    if (lockedInteractable == null)
    {
      return;
    }

    SwapObjectEditAction swapAction = lockedInteractable.GetComponent<SwapObjectEditAction>();
    if (swapAction == null)
    {
      return;
    }

    GameObject swapObject = null;
    if (swapObjectIndex == -1)
    {
      swapObject = swapAction.GetOriginalObjectInstance(false);
    }
    else
    {
      swapObject = swapAction.GetObjectChoiceInstance(swapObjectIndex, false);
    }

    lockedInteractable.SwapObject(swapObject);
  }

  //===========================================================================
  public void OnPointerEnter (GameObject enteredObject)
  {
    RM2_InteractableObject interactableObject = enteredObject.GetComponentInChildren<RM2_InteractableObject>();
    if (interactableObject != null)
    {
      _rotatingObject = interactableObject.gameObject;
    }
  }

  //===========================================================================
  public void Update ()
  {
    if (_rotatingObject != null)
    {
      _rotatingObject.transform.Rotate(Vector3.up * Time.deltaTime * _rotatingSpeed);
    }
  }

  //===========================================================================
  public void OnCloseButtonClicked ()
  {
    MenuManager.Instance.PopMenu();
    MenuManager.Instance.ShowTopMenu();
  }

  //protected methods
  //private methods
  //protected fields
  //private fields
  [SerializeField]
  private float _rotatingSpeed = 100.0f;

  private GameObject _rotatingObject = null;
}
