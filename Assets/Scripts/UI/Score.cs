using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	Text scoreText;

	int currentScore;
	bool gameHasBegun;

	public bool GameHasBegun { set { gameHasBegun = value;}}
	public int CurrentScore {set {currentScore = value;}}

	void Awake()
	{
		gameHasBegun = false;
		currentScore = 0;
	}

	void Start ()
	{
		scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();	
	}

	void Update ()
	{
		if(gameHasBegun)
		{
			scoreText.text = currentScore.ToString();
		}	
	}
}
