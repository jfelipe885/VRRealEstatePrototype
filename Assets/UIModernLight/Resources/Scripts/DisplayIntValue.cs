//------------------------------------------------------------------------------
//            UI Modern Light Beige
// Copyright � 2015 Michael Schmeling. All Rights Reserved.
// http://www.aridocean.com    
//------------------------------------------------------------------------------

using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayIntValue : MonoBehaviour
{
	public void UpdateLabel(float value)
	{
		Text label = GetComponent<Text>();
		if (label != null) {
			label.text = Mathf.RoundToInt(100*value).ToString();
		}
	}
}
