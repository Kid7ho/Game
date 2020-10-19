using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	public Transform camera_transform, canvas_transform, compass_transform;
	int CurrentGravity;
	
	void Start()
	{
		CurrentGravity = 5;
	}
	
	void Update()
	{
		if(CurrentGravity != Control.gravity.DirectionNum){
			CurrentGravity = Control.gravity.DirectionNum;
			camera_transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Control.gravity.DirectionRadian + 90);
            canvas_transform.rotation = Quaternion.Euler(0, 0, 0);
            compass_transform.rotation = Quaternion.Euler(0, 0, (-1)*(Mathf.Rad2Deg * Control.gravity.DirectionRadian + 60));
        }
	}
}
