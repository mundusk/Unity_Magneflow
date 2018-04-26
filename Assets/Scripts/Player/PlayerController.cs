using UnityEngine;

public class PlayerController : MonoBehaviour
{
	GameObject leftStopPoint, rightStopPoint, centerStopPoint;
	SpriteRenderer spriteRenderer;
	Sprite allPositiveSprite, allNegativeSprite, positiveNegativeSprite, negativePositiveSprite;
	bool isLeftPositive, isRightPositive;
	bool gameHasBegun;
	bool isAlive;

	public float movementSpeed;

	public bool IsLeftPositive { set{isLeftPositive = value;}}
	public bool IsRightPositive { set{isRightPositive = value;}}
	public bool GameHasBegun { set{ gameHasBegun = value; }}
	public bool IsAlive {get{return isAlive;}}

	void Awake()
	{
		gameHasBegun = false;
		isAlive = true;
	}

	void Start ()
	{
		leftStopPoint = GameObject.FindGameObjectWithTag("LeftStopPoint");
		rightStopPoint = GameObject.FindGameObjectWithTag("RightStopPoint");
		centerStopPoint = GameObject.FindGameObjectWithTag("CenterStopPoint");
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		allPositiveSprite = Resources.Load<Sprite>("Player/player_positive");
		allNegativeSprite = Resources.Load<Sprite>("Player/player_negative");
		positiveNegativeSprite = Resources.Load<Sprite>("Player/player_positive_negative");
		negativePositiveSprite = Resources.Load<Sprite>("Player/player_negative_positive");
	}
	
	void Update ()
	{
		if(gameHasBegun)
		{
			Vector2 targetPos = this.transform.position;

			//TODO: Refactor to remove a lot of the repetition such as always assigning targetPos speed
			if((isLeftPositive && isRightPositive) || (!isLeftPositive && !isRightPositive))
			{
				if(targetPos.x < centerStopPoint.transform.position.x &&
					targetPos.x > (centerStopPoint.transform.position.x -0.1f))
				{
					targetPos.x += 0.2f;
				}
				else if(targetPos.x > centerStopPoint.transform.position.x &&
					targetPos.x < (centerStopPoint.transform.position.x + 0.1f))
				{
					targetPos.x -= 0.2f;
				}
				else
					targetPos.x = centerStopPoint.transform.position.x;
	
				this.transform.position = Vector2.MoveTowards(this.transform.position, targetPos, movementSpeed);
			}
			else if(isLeftPositive && !isRightPositive)
			{
				if(targetPos.x > leftStopPoint.transform.position.x &&
					targetPos.x < (leftStopPoint.transform.position.x + 0.1f))
				{
					targetPos.x -= 0.2f;
				}
				else
					targetPos.x = leftStopPoint.transform.position.x;
				
				this.transform.position = Vector2.MoveTowards(this.transform.position, targetPos, movementSpeed);
			}
			else if(!isLeftPositive && isRightPositive)
			{
				if(targetPos.x < rightStopPoint.transform.position.x &
					targetPos.x > (rightStopPoint.transform.position.x - 0.1f))
				{
					targetPos.x += 0.2f;
				}
				else
					targetPos.x = rightStopPoint.transform.position.x;

				this.transform.position = Vector2.MoveTowards(this.transform.position, targetPos, movementSpeed);
			}

			UpdatePlayerSprite();
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Obstacle")
			isAlive = false;
		
		gameObject.SetActive(false);
	}

	void UpdatePlayerSprite()
	{
		if(isLeftPositive && isRightPositive)
			spriteRenderer.sprite = allPositiveSprite;
		else if(!isLeftPositive && !isRightPositive)
			spriteRenderer.sprite = allNegativeSprite;
		else if(isLeftPositive && !isRightPositive)
			spriteRenderer.sprite = positiveNegativeSprite;
		else if(!isLeftPositive && isRightPositive)
			spriteRenderer.sprite = negativePositiveSprite;
	}
}
