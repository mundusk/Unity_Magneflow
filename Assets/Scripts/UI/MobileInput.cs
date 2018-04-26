using UnityEngine;
using UnityEngine.UI;

public class MobileInput : MonoBehaviour
{
	bool leftButtonClicked, rightButtonClicked;
	bool gameHasBegun;

	public Button leftInputButton;
	public Button rightInputButton;

	public bool GameHasBegun { set{ gameHasBegun = value; }}

	public bool LeftButtonClicked
	{
		get { return leftButtonClicked; }
		set { leftButtonClicked = value; }
	}

	public bool RightButtonClicked
	{
		get { return rightButtonClicked; }
		set { rightButtonClicked = value; }
	}

	void Awake()
	{
		gameHasBegun = false;
	}
	
	void Start ()
	{
		leftInputButton.onClick.AddListener(SwitchLeftPolarity);
		rightInputButton.onClick.AddListener(SwitchRightPolarity);	
	}

	void SwitchLeftPolarity()
	{
		if(gameHasBegun)
			leftButtonClicked = true;
	}

	void SwitchRightPolarity()
	{
		if(gameHasBegun)
			rightButtonClicked = true;
	}
}
