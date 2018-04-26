using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
	bool gameHasBegun;
	int obstaclesCleared;
	int obstacleLevel;

	GameObject[] obstaclesLevelOne, obstaclesLevelTwo;
	List<GameObject> obstacleList;
	GameObject tempObstacle;
	ObstacleController tempObstacleController;

	public bool GameHasBegun { set{gameHasBegun = value;}}
	public int ObstaclesCleared { get{return obstaclesCleared;}}

	void Awake()
	{
		gameHasBegun = false;
		obstaclesCleared = 0;
		obstacleLevel = 1;
		obstaclesLevelOne = Resources.LoadAll<GameObject>("Obstacles/LevelOne");
		obstaclesLevelTwo = Resources.LoadAll<GameObject>("Obstacles/LevelTwo");
		obstacleList = new List<GameObject>();
	}
	void Start ()
	{
		foreach(GameObject go in obstaclesLevelOne)
			obstacleList.Add(go);
	}
	
	void Update ()
	{
		if(gameHasBegun) 
		{
			if(tempObstacleController != null)
			{
				if(tempObstacleController.Cleared)
				{
					obstaclesCleared++;
					SpawnObstacle();

					if(obstaclesCleared == 10)
						ChangeToLevelTwo();
					
					if(obstaclesCleared == 20)
						ChangeToLevelThree();
				}
			}
		}
	}

	public void SpawnObstacle()
	{
		//TODO: Find a nicer way to handle checking the collision
		tempObstacle = Instantiate(obstacleList[Random.Range(0, obstacleList.Count)], this.transform.position,
			Quaternion.identity);
		tempObstacleController = tempObstacle.GetComponent<ObstacleController>();
		SetObstacleSpeed();
	}

	void ChangeToLevelTwo()
	{
		for(int i = 0; i < obstaclesLevelTwo.Length; i++)
			obstacleList.Add(obstaclesLevelTwo[i]);
		
		obstacleLevel = 2;
	}

	void ChangeToLevelThree()
	{
		obstacleLevel = 3;
	}

	void SetObstacleSpeed()
	{
		if(obstacleLevel == 1)
			tempObstacleController.Speed = 0.05f;
		
		if(obstacleLevel == 2)
			tempObstacleController.Speed = 0.07f;
		
		if(obstacleLevel == 3)
			tempObstacleController.Speed = 0.09f;
	}
}
