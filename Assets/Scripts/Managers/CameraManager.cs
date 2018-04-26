using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour 
{
	//Screen aspect ratios based on width/height
	const float ASPECT_RATIO_9_18 = 0.5f;
	const float ASPECT_RATIO_9_16 = 0.57f;
	const float ASPECT_RATIO_3_5 = 0.6f;
	const float ASPECT_RATIO_5_8 = 0.625f;
	const float ASPECT_RATIO_2_3 = 0.67f;
	const float ASPECT_RATIO_3_4 = 0.75f;
	//Camera orthagraphic size based on aspect ratio
	const float CAMERA_RATIO_9_18 = 5.6f;
	const float CAMERA_RATIO_9_16 = 5f;
	const float CAMERA_RATIO_3_5 = 4.65f;
	const float CAMERA_RATIO_5_8 = 4.5f;
	const float CAMERA_RATIO_2_3 = 4.2f;
	const float CAMERA_RATIO_3_4 = 3.75f;

	void Start ()
	{
		AdjustCameraSize();
	}
	
	void AdjustCameraSize()
	{
		if(Camera.main.aspect <= ASPECT_RATIO_9_18)
		{
			Camera.main.orthographicSize = CAMERA_RATIO_9_18;
			Debug.Log("Camera updated to 18:9");
		}
		else if(Camera.main.aspect <= ASPECT_RATIO_9_16)
		{
			Camera.main.orthographicSize = CAMERA_RATIO_9_16;
			Debug.Log("Camera updated to 16:9");
		}
		else if(Camera.main.aspect <= ASPECT_RATIO_3_5)
		{
			Camera.main.orthographicSize = CAMERA_RATIO_3_5;
			Debug.Log("Camera updated to 5:3");
		}
		else if(Camera.main.aspect <= ASPECT_RATIO_5_8)
		{
			Camera.main.orthographicSize = CAMERA_RATIO_5_8;
			Debug.Log("Camera updated to 5:8");
		}
		else if(Camera.main.aspect <= ASPECT_RATIO_2_3)
		{
			Camera.main.orthographicSize = CAMERA_RATIO_2_3;
			Debug.Log("Camera updated to 3:2");
		}
		else if(Camera.main.aspect <= ASPECT_RATIO_3_4)
		{
			Camera.main.orthographicSize = CAMERA_RATIO_3_4;
			Debug.Log("Camera updated to 4:3");
		}
		else
			Debug.Log("NO UPDATES!");
	}
}
