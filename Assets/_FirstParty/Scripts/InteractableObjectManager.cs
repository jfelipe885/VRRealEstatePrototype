using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class InteractableObjectManager : Singleton<InteractableObjectManager>
{
  //Constructors(in descending order of complexity)
  //===========================================================================
  void Start ()
  {
    _allInteractableObjects = Object.FindObjectsOfType<VRTK_InteractableObject>();
  }

  //public constants
  //properties

  //public methods
  //===========================================================================
  public void EnableInteractables()
  {
    foreach (VRTK_InteractableObject io in _allInteractableObjects)
    {
      io.enabled = true;
    }
  }

  //===========================================================================
  public void DisableInteractables()
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

  VRTK.VRTK_InteractableObject[] _allInteractableObjects;
}
