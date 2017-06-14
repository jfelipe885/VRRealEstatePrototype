//------------------------------------------------------------------------------
//            UI Modern Light Beige
// Copyright © 2015 Michael Schmeling. All Rights Reserved.
// http://www.aridocean.com    
//------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI; 
using System.Collections;

//=================================================================
// This script must be attached to a Canvas object in your scene.
//=================================================================

// ElementRow represents a row of ui elements.
// Each row has a panel at the top which is used to elems.Display a short
// message whenever the user interacts with an UI element in this row.
public class ElementRow
{
	private UIText displayText;
	private UIElement elems;
	
	public ElementRow(UIElement parentPanel, string name)
	{
      UIElement row = new UIPanel(parentPanel, UIPanel.Type.Empty, elemName: name);
		row.AddVerticalLayoutGroup(spacing: 5);  

		// A panel for the message text		
      UIElement displayRow = new UIPanel(row, UIPanel.Type.Thin, UIPanel.Border.Sunken, "Header for "+name);
		displayRow.AddHorizontalLayoutGroup(spacing: 10, alignment: TextAnchor.MiddleCenter);   
		displayRow.AddLayoutElement(preferredWidth: 900, preferredHeight: 40);
   
   	UIText headerText = new UIText(displayRow, fontSize: 16);
		headerText.SetAlignment(TextAnchor.MiddleCenter);
		headerText.SetText(name+":");
			
		displayText = new UIText(displayRow, fontSize: 16);
		displayText.SetAlignment(TextAnchor.MiddleCenter);

		// A panel for the UI elements in this row
		// An empty UIPanel is used to hold the layout group for its children. The panel itself is not visible to the user ('Empty').		
      elems = new UIPanel(row, UIPanel.Type.Empty, elemName: "Elements: "+name);
		elems.AddHorizontalLayoutGroup(alignment: TextAnchor.MiddleLeft, spacing: 10, padding: new RectOffset(0,0,2,10));  // left, right, top, bottom
	}
	
	public void Display(string text)
	{
		displayText.SetText(text);
	}

	public static implicit operator UIElement(ElementRow r)
	{
		return r.elems;
	}	
}

public class DemoScene : MonoBehaviour
{
	void Start()
	{
		// Get the Canvas the script is attached to
		UIElement canvas = new UIElement(gameObject);
		canvas.AddVerticalLayoutGroup(expandWidth: true, expandHeight: true);

		// Add a separate tooltip canvas, which is placed in sort order above all other elements
		// The tooltip canvas is assigned to a static variable in UIElement for easier access
		UIElement.SetTooltipCanvas(new UITooltipCanvas());

		// Create a main panel which will include all UI elements
      UIElement mainPanel = new UIPanel(canvas, UIPanel.Type.Thick, UIPanel.Border.RaisedEtched, "Main Panel");
		mainPanel.AddVerticalLayoutGroup(expandWidth: true, spacing: 5, padding: new RectOffset(20,20,20,20));  // left, right, top, bottom

		CreateTitlePanel(mainPanel);
		
		// create a panel for all content below the header panel
      UIElement contentPanel = new UIPanel(mainPanel, UIPanel.Type.Thick, UIPanel.Border.Raised, elemName: "Content Panel");
		contentPanel.AddVerticalLayoutGroup(padding: new RectOffset(15,10,15,15));   // left, right, top, bottom
		contentPanel.AddLayoutElement(preferredHeight: 1000);

		// The content should be scrollable, so we create a scrollview
		// UIScrollView automatically creates a scroll parent panel which holds the scroll view and the two scroll bars
		// Note that the horizontal scrollbar is parented to the content panel in order to place it correctly
		UIScrollView scrollView = new UIScrollView(contentPanel);

		// Now create the actual scrollable	content panel, which holds all scrollable elements	
      UIElement scrollContent = new UIPanel(scrollView, UIPanel.Type.Empty, elemName: "ScrollContent");
		scrollContent.AddVerticalLayoutGroup(spacing: 0, padding: new RectOffset(0,20,0,20)); 
		scrollContent.AddContentSizeFitter();
		scrollView.AddScrollContent(scrollContent);

		// Create rows with the different types of ui elements we want to show
		CreateToggleButtons(scrollContent);
		CreateButtons(scrollContent);
		CreateSliders(scrollContent);
		CreateKnobs(scrollContent);
		CreateInputFields(scrollContent);

		// This is required to place the intial scroll position at the upper left corner
		// Otherwise the scroll view is positioned in the middle
		// Probably related to Unity 4.6.0 Bug 643604 
		scrollView.SetScrollPosition();
	}
	
	void CreateTitlePanel(UIElement parentPanel)
	{
		// Create a header panel at the top with the title text
      UIElement titlePanel = new UIPanel(parentPanel, UIPanel.Type.Thick, UIPanel.Border.SunkenEtched, elemName: "Title Panel");
		titlePanel.AddVerticalLayoutGroup(expandWidth: true, padding: new RectOffset(20,20,20,0));  // left, right, top, bottom
		titlePanel.AddLayoutElement(minHeight: 75);

		new UIText(titlePanel, "Modern Light/Beige GUI Skin and Widgets", fontSize: 28);		
		new UIText(titlePanel, "for uGUI (4.6+)", fontSize: 20);		
	}

	void CreateToggleButtons(UIElement parentPanel)
	{
		ElementRow elems = new ElementRow(parentPanel, "Toggle Buttons"); 

		// A regular (square) toggle button
		// An event handler is attached to the button which elems.Display a short text in the elems.Display panel at the top of the row
		// Similar handler are attached to all UI elements
      UIToggle squareToggle = new UIToggle(elems, UIToggle.Type.Square, UIToggle.CheckStyle.Green, "Square Toggle");
      squareToggle.SetText("Toggle"); 
      squareToggle.AddTooltip("This is a Square Toogle");
		squareToggle.AddLayoutElement(preferredWidth: 90);
		squareToggle.SetState(isOn: true); 
		squareToggle.OnValueChanged((v) => {
			elems.Display( (v ? "Square Toggle On" : "Square Toggle Off") );
		});

		// A regular (square) toggle button with red and green states
      UIToggle squareToggle2 = new UIToggle(elems, UIToggle.Type.Square, UIToggle.CheckStyle.RedGreen, "Square Toggle 2");
      squareToggle2.SetText("Toggle"); 
      squareToggle2.AddTooltip("This is a Square Toogle");
		squareToggle2.AddLayoutElement(preferredWidth: 90);
		squareToggle2.SetState(isOn: false); 
		squareToggle2.OnValueChanged((v) => {
			elems.Display( (v ? "Square Toggle 2 On" : "Square Toggle 2 Off") );
		});

		// A regular (square) toggle button with checkmark
      UIToggle squareToggle3 = new UIToggle(elems, UIToggle.Type.Square, UIToggle.CheckStyle.Checkmark, "Square Toggle 3");
      squareToggle3.SetText("Toggle"); 
      squareToggle3.AddTooltip("This is a Square Toogle");
		squareToggle3.AddLayoutElement(preferredWidth: 90);
		squareToggle3.SetState(isOn: true); 
		squareToggle3.OnValueChanged((v) => {
			elems.Display( (v ? "Square Toggle 3 On" : "Square Toggle 3 Off") );
		});

      UIToggle roundToggle = new UIToggle(elems, UIToggle.Type.Round, UIToggle.CheckStyle.Green, "Round Toggle");
		roundToggle.AddLayoutElement(preferredWidth: 90);
      roundToggle.SetText("Toggle");
      roundToggle.AddTooltip("This is a Round Toogle");
		roundToggle.SetState(isOn: true); 
		roundToggle.OnValueChanged((v) => {
			elems.Display( (v ? "Round Toggle On" : "Round Toggle Off") );
		});

		// A round toggle button with red and green states
      UIToggle roundToggle2 = new UIToggle(elems, UIToggle.Type.Round, UIToggle.CheckStyle.RedGreen, "Round Toggle 2");
		roundToggle2.AddLayoutElement(preferredWidth: 90);
      roundToggle2.SetText("Toggle");
      roundToggle2.AddTooltip("This is a Round Toogle");
		roundToggle2.SetState(isOn: false); 
		roundToggle2.OnValueChanged((v) => {
			elems.Display( (v ? "Round Toggle 2 On" : "Round Toggle 2 Off") );
		});

		// A round toggle button with checkmark
      UIToggle roundToggle3 = new UIToggle(elems, UIToggle.Type.Round, UIToggle.CheckStyle.Checkmark, "Round Toggle 3");
		roundToggle3.AddLayoutElement(preferredWidth: 90);
      roundToggle3.SetText("Toggle");
      roundToggle3.AddTooltip("This is a Round Toogle");
		roundToggle3.SetState(isOn: true); 
		roundToggle3.OnValueChanged((v) => {
			elems.Display( (v ? "Round Toggle 3 On" : "Round Toggle 3 Off") );
		});
	}

	void CreateButtons(UIElement parentPanel)
	{
		ElementRow elems = new ElementRow(parentPanel, "Buttons"); 

		UIButton roundButton = new UIButton(elems, UIButton.Type.Round, "Round Button");
		roundButton.AddLayoutElement(preferredWidth: 140, minHeight: 50);
      roundButton.AddTooltip("This is a Round Button");
		roundButton.OnValueChanged(() => {
			elems.Display("Round Button clicked");
		});

		UIButton rectButton = new UIButton(elems, UIButton.Type.Rectangle, "Rectangular Button");
		rectButton.SetText("Rect Button");
		rectButton.AddLayoutElement(minWidth: 130, minHeight: 50);
      rectButton.AddTooltip("This is a Rectangle Button");
      //rectButton.SetEnabled(false);
		rectButton.OnValueChanged(() => {
			elems.Display("Rectangular Button clicked");
		});

		UIButton roundRectButton = new UIButton(elems, UIButton.Type.RoundRectangle, "Round Rectangular Button");
		roundRectButton.SetText("Rnd Rect Button");
		roundRectButton.AddLayoutElement(minWidth: 150, minHeight: 50);
      roundRectButton.AddTooltip("This is a Round Rectangle Button");
		roundRectButton.OnValueChanged(() => {
			elems.Display("Round Rectangular Button clicked");
		});
	}

	void CreateInputFields(UIElement parentPanel)
	{
		ElementRow elems = new ElementRow(parentPanel, "InputFields"); 

		// This is a standard input field
		UITextInputField input = new UITextInputField(elems, "Type here...");
		input.AddLayoutElement(minWidth: 100, preferredWidth: 300, minHeight: 100);
      input.AddTooltip("This is a Text Input Field");
		input.onEndEdit((s) => { 
			elems.Display("Input submitted: "+s);
		});
	}

	void CreateRadioButtons(UIElement parentPanel)
	{
		ElementRow elems = new ElementRow(parentPanel, "RadioButtons"); 

		// A group panel is used as a toggle group		
      UIPanel radioGroup1 = new UIPanel(elems, UIPanel.Type.Thin, UIPanel.Border.Sunken, "Radio Group 1");
      // The radio buttons will be arranged vertically
		radioGroup1.AddVerticalLayoutGroup(spacing: 5, padding: new RectOffset(20,0,15,0));   // left, right, top, bottom
		radioGroup1.AddLayoutElement(preferredWidth: 180, preferredHeight: 100);

      UIToggle radio11 = new UIToggle(radioGroup1, UIToggle.Type.Square, UIToggle.CheckStyle.Green, "Radio Button 1");
		radio11.AddLayoutElement(preferredWidth: 190);
      radio11.AddTooltip("This is a Radio Button");
		// Sets the intial state of this toggle button to 'on'
		radio11.SetState(isOn: true); 
		
		// When a toggle button is attached to the group panel it is turns into a radio group
		radioGroup1.AddToggle(radio11);
		radio11.OnValueChanged((v) => {
			elems.Display( (v ? "Radio Button 1 On" : "Radio Button 1 Off") );
		});

      UIToggle radio12 = new UIToggle(radioGroup1, UIToggle.Type.Square, UIToggle.CheckStyle.Green, "Radio Button 2");
		radio12.AddLayoutElement(preferredWidth: 190); 
      radio12.AddTooltip("This is a Radio Button");
		// add the second toggle button to the radio group
		radioGroup1.AddToggle(radio12);
		radio12.OnValueChanged((v) => {
			elems.Display( (v ? "Radio Button 2 On" : "Radio Button 2 Off") );
		});


		// all other radio groups are created in the same manner a above
      UIPanel radioGroup2 = new UIPanel(elems, UIPanel.Type.Thin, UIPanel.Border.RaisedEtched, "Radio Group 2");
		radioGroup2.AddVerticalLayoutGroup(spacing: 5, padding: new RectOffset(20,0,15,0));   // left, right, top, bottom
		radioGroup2.AddLayoutElement(preferredWidth: 180, preferredHeight: 100);

      UIToggle radio21 = new UIToggle(radioGroup2, UIToggle.Type.Round, UIToggle.CheckStyle.RedGreen, "Radio Button 3");
		radio21.AddLayoutElement(preferredWidth: 190);
      radio21.AddTooltip("This is a Radio Button");
		radio21.SetState(isOn: true);
		radioGroup2.AddToggle(radio21);
		radio21.OnValueChanged((v) => {
			elems.Display( (v ? "Radio Button 3 On" : "Radio Button 3 Off") );
		});

      UIToggle radio22 = new UIToggle(radioGroup2, UIToggle.Type.Round, UIToggle.CheckStyle.RedGreen, "Radio Button 4");
		radio22.AddLayoutElement(preferredWidth: 190);
      radio22.AddTooltip("This is a Radio Button");
		radioGroup2.AddToggle(radio22);
		radio22.OnValueChanged((v) => {
			elems.Display( (v ? "Radio Button 4 On" : "Radio Button 4 Off") );
		});
         
         
		// all other radio groups are created in the same manner a above
      UIPanel radioGroup3 = new UIPanel(elems, UIPanel.Type.Thin, UIPanel.Border.SunkenEtched, "Radio Group 3");
		radioGroup3.AddVerticalLayoutGroup(spacing: 5, padding: new RectOffset(20,0,15,0));   // left, right, top, bottom
		radioGroup3.AddLayoutElement(preferredWidth: 180, preferredHeight: 100);

      UIToggle radio31 = new UIToggle(radioGroup3, UIToggle.Type.Round, UIToggle.CheckStyle.Checkmark, "Radio Button 5");
		radio31.AddLayoutElement(preferredWidth: 190);
      radio31.AddTooltip("This is a Radio Button");
		radio31.SetState(isOn: true);
		radioGroup3.AddToggle(radio31);
		radio31.OnValueChanged((v) => {
			elems.Display( (v ? "Radio Button 5 On" : "Radio Button 5 Off") );
		});

      UIToggle radio32 = new UIToggle(radioGroup3, UIToggle.Type.Round, UIToggle.CheckStyle.Checkmark, "Radio Button 6");
		radio32.AddLayoutElement(preferredWidth: 190);
      radio32.AddTooltip("This is a Radio Button");
		radioGroup3.AddToggle(radio32);
		radio32.OnValueChanged((v) => {
			elems.Display( (v ? "Radio Button 6 On" : "Radio Button 6 Off") );
		});
	}

	void CreateSliders(UIElement parentPanel)
	{
		ElementRow elems = new ElementRow(parentPanel, "Sliders"); 
		// This row shows the various slider variants
		// When the user moves a slider, a corresponding text with the slider value is shown 
		// in the elems.Display area on top of the row
		UISlider slider1 = new UISlider(elems, UISlider.Type.Horizontal, UISlider.Style.Plain); 
      slider1.AddTooltip("This is a Plain Horizontal Slider");
		slider1.SetValue(0.6f);
		slider1.OnValueChanged((v) => { 
			elems.Display("Horizontal Slider changed: "+Mathf.RoundToInt(v*100)+"%");
		});  
		
		UISlider slider2 = new UISlider(elems, UISlider.Type.Vertical, UISlider.Style.Plain);
      slider2.AddTooltip("This is a Plain Vertical Slider");
		slider2.SetValue(0.7f);
		slider2.OnValueChanged((v) => { 
			elems.Display("Vertical Slider changed: "+Mathf.RoundToInt(v*100)+"%");
		});  
		
		UISlider slider3 = new UISlider(elems, UISlider.Type.Horizontal, UISlider.Style.Labelled);
      slider3.AddTooltip("This is a Labelled Horizontal Slider");
		slider3.SetValue(0.6f);
		slider3.OnValueChanged((v) => { 
			elems.Display("Horizontal Labeled Slider changed: "+Mathf.RoundToInt(v*100)+"%");
		});  

		UISlider slider4 = new UISlider(elems, UISlider.Type.Vertical, UISlider.Style.Labelled);
      slider4.AddTooltip("This is a Labelled Vertical Slider");
		slider4.SetValue(0.4f);
		slider4.OnValueChanged((v) => { 
			elems.Display("Vertical Labelled Slider changed: "+Mathf.RoundToInt(v*100)+"%");
		});  

		UISlider slider5 = new UISlider(elems, UISlider.Type.Vertical, UISlider.Style.LabelTab);
      slider5.AddTooltip("This is a Labelled Vertical Slider with Tab");
		slider5.SetValue(0.7f);
		slider5.OnValueChanged((v) => { 
			elems.Display("Vertical Tab Label Slider changed: "+Mathf.RoundToInt(v*100)+"%");
		});  

		UISlider slider6 = new UISlider(elems, UISlider.Type.Horizontal, UISlider.Style.LabelTab);
      slider6.AddTooltip("This is a Labelled Horizontal Slider with Tab");
		slider6.SetValue(0.4f);
		slider6.OnValueChanged((v) => { 
			elems.Display("Horizontal Tab Label Slider changed: "+Mathf.RoundToInt(v*100)+"%");
		});  
	}

	void CreateKnobs(UIElement parentPanel)
	{
		ElementRow elems = new ElementRow(parentPanel, "Knobs"); 

      UIRotatingKnob knob1 = new UIRotatingKnob(elems);
		knob1.AddLayoutElement(preferredWidth: 200, preferredHeight: 200);
      knob1.AddTooltip("This is a Rotating Knob");
		knob1.OnValueChanged((v) => {
			elems.Display("Knob changed: "+Mathf.Round(v*10)/10+"%");
		});
	}
}