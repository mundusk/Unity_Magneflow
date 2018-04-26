using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	Text[] uiText;
	bool gameHasBegun;
	bool highScoreTextSet;
	int highScore;

	public bool GameHasBegun { set { gameHasBegun = value;}}
	public int HighScore {set {highScore = value;}}

	void Awake()
	{
		gameHasBegun = false;
		highScoreTextSet = false;
	}

	void Start ()
	{
		uiText = GetComponentsInChildren<Text>();

		SetInstructionText();
	}
	
	void Update ()
	{
		if(!highScoreTextSet)
			SetHighScoreText();

		if(gameHasBegun)
		{
			for(int i=0; i < uiText.Length; i++)
			{
				if(uiText[i].tag != "Score")
					uiText[i].enabled = false;
			}
		}	
	}

	void SetInstructionText()
	{
		for(int i=0; i < uiText.Length; i++)
		{
			if(uiText[i].name == "Instruction")
			{
				#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER

				uiText[i].text = "Press any key to start";

				#elif UNITY_ANDROID

				uiText[i].text = "Tap to start";

				#endif
			}
		}
	}

	void SetHighScoreText()
	{
		for(int i=0; i < uiText.Length; i++)
		{
			if(uiText[i].name == "HighScore")
			{
				uiText[i].text += "\n";
				uiText[i].text += highScore.ToString();
			}
		}

		highScoreTextSet = true;	
	}
}
