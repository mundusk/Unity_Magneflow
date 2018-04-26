using UnityEngine;

public class ObstacleController : MonoBehaviour
{
	bool cleared;
	public float speed;

	public bool Cleared { get {return cleared;}}
	public float Speed {set{speed = value;}}

	void Awake()
	{
		cleared = false;
	}

	void Start ()
	{
		
	}
	
	void Update ()
	{
		Vector2 targetPos = this.transform.position;
		targetPos.y -= 10;

		this.transform.position = Vector2.MoveTowards(this.transform.position, targetPos, speed);
	}

	void OnTriggerEnter2D()
	{
		cleared = true;
	}

	void OnBecameInvisible()
	{
		if(gameObject != null)
			Destroy(gameObject);
	}
}
