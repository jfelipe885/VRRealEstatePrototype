using System;
using UnityEngine;

public class SwapObjectEditAction : BaseEditAction
{
  [Serializable]
  public class ObjectChoice
  {
    public GameObject _object;
    public String _name;
  }

  //===========================================================================
  public GameObject GetOriginalObjectInstance (bool forUI)
  {
    if (_originalObject == null)
    {
      return null;
    }

    GameObject newObject = Instantiate(_originalObject._object);
    RM2_InteractableObject interactable = newObject.GetComponent<RM2_InteractableObject>();
    if (interactable != null && forUI == true)
    {
      interactable.SetUpforUI();
      interactable.name += "(UI)";
    }
    return newObject;
  }

  //===========================================================================
  public String GetObjectChoiceName (int index)
  {
    if (MathHelper.IndexWithin(index, 0, _objectChoices.Length) == false)
    {
      return null;
    }

    return _objectChoices[index]._name;
  }

  //===========================================================================
  public int GetObjectChoicesLength ()
  {
    return _objectChoices.Length;
  }

  //===========================================================================
  public GameObject GetObjectChoiceInstance (int index, bool forUI)
  {
    if (MathHelper.IndexWithin(index, 0, _objectChoices.Length) == false)
    {
      return null;
    }

    GameObject newObject = Instantiate(_objectChoices[index]._object);
    RM2_InteractableObject interactable = newObject.GetComponent<RM2_InteractableObject>();
    if (interactable != null && forUI == true)
    {
      interactable.SetUpforUI();
      interactable.name += "(UI)";
    }
    return newObject;
  }

  [SerializeField]
  private ObjectChoice _originalObject = null;
  [SerializeField]
  private ObjectChoice[] _objectChoices = null;  
}