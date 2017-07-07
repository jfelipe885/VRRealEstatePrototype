using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//===========================================================================
public class MathHelper
{
  static public bool IndexWithin (int index , int min, int max)
  {
    if (index < min || index >= max)
    {
      return false;
    }
    return true;
  }

  //===========================================================================
  static public void CopyTransform (Transform lhs, Transform rhs)
  {
    lhs.localPosition = rhs.localPosition;
    lhs.localRotation = rhs.localRotation;
    lhs.localScale = rhs.localScale;
  }
}
