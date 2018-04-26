using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarityController : MonoBehaviour
{
	SpriteRenderer spriteRenderer;
	
	public Sprite positivePolaritySprite;
	public Sprite negativePolaritySprite;

	bool isPositive;
	bool onTitleScreen;
	bool startPositive;

	public bool GetPolarity { get {return isPositive;} }
	public bool OnTitleScreen { set {onTitleScreen = value;}}

	public int SetStartingPolarity
	{
		set
		{
			if(value == 0)
				isPositive = false;
			else
				isPositive = true;
		}
	}

	void Awake()
	{
		onTitleScreen = true;
	}
	void Start ()
	{
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		spriteRenderer.sprite = positivePolaritySprite;
	}
	
	void Update ()
	{
		if(onTitleScreen)
		{
			if(this.gameObject.tag == "LeftPole")
				spriteRenderer.sprite = positivePolaritySprite;
			if(this.gameObject.tag == "RightPole")
				spriteRenderer.sprite = negativePolaritySprite;
		}
		else
		{
			if(isPositive)
				spriteRenderer.sprite = positivePolaritySprite;
			else
				spriteRenderer.sprite = negativePolaritySprite;
		}
	}

	public void SwitchPolarity()
	{
		isPositive = !isPositive;
	}
}
