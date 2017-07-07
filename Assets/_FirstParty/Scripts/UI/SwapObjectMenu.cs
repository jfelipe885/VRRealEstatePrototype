using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapObjectMenu : BaseMenu
{
  //Constructors (in descending order of complexity)
  //public constants
  //properties
  //public methods  
  //===========================================================================
  public override void SetUpButtons (RM2_InteractableObject interactable)
  {
    if (interactable == null)
    {
      Debug.LogError("SetUpButtons() , interactable == null");
      return;
    }

    SwapObjectEditAction swapableAction = interactable.GetComponent<SwapObjectEditAction>();
    if (swapableAction == null)
    {
      return;
    }

    cleanObject(_originalObjectButton);
    GameObject originalGameObject = swapableAction.GetOriginalObjectInstance(true);
    if (originalGameObject != null)
    {
      originalGameObject.transform.SetParent(_originalObjectButton.transform);
      MathHelper.CopyTransform(originalGameObject.transform, _originalObjectTransform);
    }
    
    Text originalColorText = _originalObjectButton.GetComponentInChildren<Text>();
    if (originalColorText != null)
    {
      originalColorText.text = "Original";
    }

    int objectChoicesLength = swapableAction.GetObjectChoicesLength();
    if (objectChoicesLength > _swapObjectButtons.Length)
    {
      Debug.LogWarning("changeColorEditAction._colorChoices.Length > _colorButtons.Length");
    }

    for (int i = 0; i < _swapObjectButtons.Length; i++)
    {
      if (i >= objectChoicesLength)
      {
        _swapObjectButtons[i].gameObject.SetActive(false);
        continue;
      }

      cleanObject(_swapObjectButtons[i].gameObject);
      GameObject buttonGameObject = swapableAction.GetObjectChoiceInstance(i, true);
      if (buttonGameObject == null)
      {
        continue;
      }
      
      buttonGameObject.transform.SetParent(_swapObjectButtons[i].transform);
      if (_ObjectButtonTransforms[i] != null)
      {
        MathHelper.CopyTransform(buttonGameObject.transform, _ObjectButtonTransforms[i]);
      }

      Text swapButtonText = _swapObjectButtons[i].GetComponentInChildren<Text>();
      if (swapButtonText != null)
      {
        swapButtonText.text = swapableAction.GetObjectChoiceName(i);
      }
    }
  }

  //protected methods
  //private methods
  //===========================================================================
  private void cleanObject (GameObject gameObject)
  {
    if (gameObject == null)
    {
      return;
    }

    for (int i = gameObject.transform.childCount-1; i >= 0; i--)
    {
      Transform child = gameObject.transform.GetChild(i);
      if (child == null)
      {
        continue;
      }
      if (child.GetComponent<RM2_InteractableObject>() != null)
      {
        Destroy(child.gameObject);
      }
    }
  }

  //protected fields
  //private fields
  [SerializeField]
  private GameObject _originalObjectButton = null;

  [SerializeField]
  private Transform _originalObjectTransform = null;

  [SerializeField]
  private Button[] _swapObjectButtons = null;

  [SerializeField]
  private Transform[] _ObjectButtonTransforms = null;
}
