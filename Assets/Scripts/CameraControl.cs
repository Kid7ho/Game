using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	public Transform camera_transform, compass_transform, minimap_transform;
    public Transform player_transform;

    int CurrentGravity, PreGravity;
	
	void Start()
	{
        PreGravity = 5;
        CurrentGravity = 5;
	}
	
	void Update()
	{
        camera_transform.position = new Vector3(player_transform.position.x, player_transform.position.y, -10);//카메라가 플레이어를 따라가도록

        if (CurrentGravity != Control.gravity.DirectionNum)
        {
            PreGravity = CurrentGravity;
            CurrentGravity = Control.gravity.DirectionNum;

            //시간 멈춤
            Time.timeScale = 0f;
        }
        if (CurrentGravity != PreGravity)
        {
            Debug.Log("Func Recalled");
            camera_transform.rotation = Quaternion.Slerp(camera_transform.rotation, Quaternion.Euler(0, 0, Mathf.Rad2Deg * Control.gravity.DirectionRadian + 90), 0.1f);
            compass_transform.rotation = Quaternion.Slerp(compass_transform.rotation, Quaternion.Euler(0, 0, (-1) * (Mathf.Rad2Deg * Control.gravity.DirectionRadian + 60)), 0.1f);
            minimap_transform.rotation = Quaternion.Slerp(compass_transform.rotation, Quaternion.Euler(0, 0, (-1) * (Mathf.Rad2Deg * Control.gravity.DirectionRadian + 120)), 0.1f);

            if (Mathf.Abs((camera_transform.rotation.eulerAngles.z + 360) % 360 - (Mathf.Rad2Deg * Control.gravity.DirectionRadian + 90 + 360) % 360) <= 5f)
            {
                Debug.Log("Done");
                camera_transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Control.gravity.DirectionRadian + 90);
                compass_transform.rotation = Quaternion.Euler(0, 0, (-1) * (Mathf.Rad2Deg * Control.gravity.DirectionRadian + 60));
                minimap_transform.rotation = Quaternion.Euler(0, 0, (-1) * (Mathf.Rad2Deg * Control.gravity.DirectionRadian + 120));

                PreGravity = CurrentGravity;
                Time.timeScale = 1f;
            }
        }
    }
}
