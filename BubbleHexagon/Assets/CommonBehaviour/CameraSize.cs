using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour
{
    public int target_width;
    public int target_height;
    public float target_size;

    float target_ratio;

    Resolution resolution;
    public float current_ratio;
    //public float aspect_ratio;

    Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        target_size = mainCamera.orthographicSize;
        target_ratio = (float)target_height / target_width;

        resolution = Screen.currentResolution;
        current_ratio = (float)resolution.height / resolution.width;
        //aspect_ratio = 1f / camera.aspect;
    }

    void Start()
    {
#if UNITY_EDITOR
#else
        CalculateCameraSize();
#endif

    }

    public void CalculateCameraSize()
    {
        if(current_ratio > target_ratio)
        {
            mainCamera.orthographicSize = target_size * current_ratio / target_ratio;
        }

        //camera.orthographicSize = target_size * aspect_ratio / target_ratio;
        
    }


}
