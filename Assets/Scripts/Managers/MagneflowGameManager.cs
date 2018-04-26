using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MagneflowGameManager : MonoBehaviour
{
	GameObject player;
	GameObject obstacleManagerObject;
	GameObject leftPolarity, rightPolarity;
	GameObject leftArrow, rightArrow;
	GameObject uiCanvasObject;
	SpriteRenderer leftArrowSpriteRenderer, rightArrowSpriteRenderer;
	PlayerController playerController;
	ObstacleManager obstacleManager;
	UIController uiTitle;
	Score uiScore;
	PolarityController leftPolarityController, rightPolarityController;
	MobileInput mobileInputController;
	IEnumerator beginCoroutine, restartCoroutine;
	bool isLeftPositive, isRightPositive;
	bool gameHasBegun, restartingGame;
	int score, highScore;
	int startingPolarity;

	public GameObject deathParticleEffect;

	void Awake()
	{
		gameHasBegun = false;
		restartingGame = false;
		score = 0;
		highScore = 0;

		if(PlayerPrefs.HasKey("HighScore"))
			highScore = PlayerPrefs.GetInt("HighScore");
		else
			PlayerPrefs.SetInt("HighScore", highScore);
	}

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		obstacleManagerObject = GameObject.FindGameObjectWithTag("ObstacleManager");
		leftPolarity = GameObject.FindGameObjectWithTag("LeftPole");
		rightPolarity = GameObject.FindGameObjectWithTag("RightPole");
		leftArrow = GameObject.FindGameObjectWithTag("LeftArrow");
		rightArrow = GameObject.FindGameObjectWithTag("RightArrow");
		uiCanvasObject = GameObject.FindGameObjectWithTag("UI");
		leftArrowSpriteRenderer = leftArrow.GetComponent<SpriteRenderer>();
		rightArrowSpriteRenderer = rightArrow.GetComponent<SpriteRenderer>();
		playerController = player.GetComponent<PlayerController>();
		obstacleManager = obstacleManagerObject.GetComponent<ObstacleManager>();
		leftPolarityController = leftPolarity.GetComponent<PolarityController>();
		rightPolarityController = rightPolarity.GetComponent<PolarityController>();
		mobileInputController =	uiCanvasObject.GetComponent<MobileInput>();
		uiTitle = uiCanvasObject.GetComponent<UIController>();
		uiScore = uiCanvasObject.GetComponent<Score>();

		int startingPolarity = Random.Range(0,2); //Generate 0 or 1 to decide to start positive or negative
		leftPolarityController.SetStartingPolarity = startingPolarity;
		rightPolarityController.SetStartingPolarity = startingPolarity;

		restartCoroutine = RestartGame();
		beginCoroutine = BeginGame();

		PassHighScoreToTitle();
	}
	
	void Update ()
	{
		if(gameHasBegun)
		{
			if(playerController.IsAlive)
			{
				#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER

				if(Input.GetButtonDown("Right"))
					rightPolarityController.SwitchPolarity();
				if(Input.GetButtonDown("Left"))
					leftPolarityController.SwitchPolarity();
				
				#elif UNITY_ANDROID

				if(mobileInputController.RightButtonClicked)
				{
					rightPolarityController.SwitchPolarity();
					mobileInputController.RightButtonClicked = false;
				}
				if(mobileInputController.LeftButtonClicked)
				{
					leftPolarityController.SwitchPolarity();
					mobileInputController.LeftButtonClicked = false;
				}

				#endif
				
				isRightPositive = rightPolarityController.GetPolarity;
				isLeftPositive = leftPolarityController.GetPolarity;

				playerController.IsRightPositive = isRightPositive;
				playerController.IsLeftPositive = isLeftPositive;

				if(isRightPositive)
					rightArrowSpriteRenderer.flipX = false;
				else
					rightArrowSpriteRenderer.flipX = true;

				if(isLeftPositive)
					leftArrowSpriteRenderer.flipX = true;
				else
					leftArrowSpriteRenderer.flipX = false;
				
				UpdateCurrentScoreUI();
			}
			else
			{
				if(!restartingGame)
				{
					PlayerDied();
					StartCoroutine(restartCoroutine);
				}
			}
		}
		else
		{
			leftArrow.SetActive(false);
			rightArrow.SetActive(false);
			
			if(Input.anyKey)
			{
				StartCoroutine(beginCoroutine);
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape)) 
    		Application.Quit(); 
	}

	void PassHighScoreToTitle()
	{
		uiTitle.HighScore = highScore;
	}

	void UpdateCurrentScoreUI()
	{
		uiScore.CurrentScore = obstacleManager.ObstaclesCleared;
	}

	void PlayerDied()
	{
		Instantiate(deathParticleEffect, player.transform.position, player.transform.rotation);

		score = obstacleManager.ObstaclesCleared;

		if(score > highScore)
		{
			highScore = score;
			PlayerPrefs.SetInt("HighScore", highScore);
			Debug.Log(PlayerPrefs.GetInt("HighScore"));
		}

		restartingGame = true;
	}

	IEnumerator BeginGame()
	{
		yield return new WaitForSeconds(0.3f);
		this.gameHasBegun = true;
		playerController.GameHasBegun = true;
		mobileInputController.GameHasBegun = true;
		obstacleManager.GameHasBegun = true;
		leftPolarityController.OnTitleScreen = false;
		rightPolarityController.OnTitleScreen = false;
		uiTitle.GameHasBegun = true;
		uiScore.GameHasBegun = true;
		leftArrow.SetActive(true);
		rightArrow.SetActive(true);
		obstacleManager.SpawnObstacle();
	}

	IEnumerator RestartGame()
	{
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
