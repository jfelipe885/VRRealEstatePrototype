//------------------------------------------------------------------------------
//            UI Modern Light Beige
// Copyright © 2015 Michael Schmeling. All Rights Reserved.
// http://www.aridocean.com    
//------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIToggle : UIElement
{ 
	public enum Type {Square, Round}; 
	public enum CheckStyle {Green, RedGreen, Checkmark}; 

	public UIToggle(UIElement parent, Type type, CheckStyle style=CheckStyle.Green, string elemName="") : base(parent)
	{ 	
		string prefabName = "Toggle"+type+style;
		LoadPrefab(parent, prefabName, elemName); 
		SetText(elemName);
		SetState(isOn: false);
	}
	
	public void SetText(string text, int fontSize=14)
	{
		Text t = GetObject().GetComponentInChildren<Text>();
		t.text = text;
		t.fontSize = fontSize;
	}     
	     
	public void SetState(bool isOn)
	{
		GetObject().GetComponent<Toggle>().isOn = isOn;
	}

	public void OnValueChanged(UnityAction<bool> callback)
	{
		GetObject().GetComponent<Toggle>().onValueChanged.AddListener(callback);
	}
	
}
