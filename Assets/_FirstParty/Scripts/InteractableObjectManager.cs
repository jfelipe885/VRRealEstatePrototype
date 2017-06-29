using UnityEngine;
using VRTK;

public class InteractableObjectManager : Singleton<InteractableObjectManager>
{
  //Constructors(in descending order of complexity)
  //===========================================================================
  private void Start ()
  {
    _allInteractableObjects = Object.FindObjectsOfType<VRTK_InteractableObject>();
  }

  //public constants
  //properties

  //public methods
  //===========================================================================
  public void EnableInteractables ()
  {
    foreach (VRTK_InteractableObject io in _allInteractableObjects)
    {
      io.enabled = true;
    }
  }

  //===========================================================================
  public void DisableInteractables ()
  {
    foreach (VRTK_InteractableObject io in _allInteractableObjects)
    {
      io.enabled = false;
    }
  }

  //protected methods
  //private methods
  //protected fields
  //private fields

  private VRTK.VRTK_InteractableObject[] _allInteractableObjects;
}