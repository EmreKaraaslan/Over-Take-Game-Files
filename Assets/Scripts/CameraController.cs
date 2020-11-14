using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public KeyCode Touch;

    public GameObject car;
    
    private Vector3 ZoomInPosition, ZoomOutPosition, offset;
   
   
    void Start()
    {
        offset = transform.position - car.transform.position;
       
    }


    void LateUpdate()
    {
        bool held = Input.GetKey(Touch);

        ZoomInPosition = car.transform.position + offset;
        ZoomOutPosition = ZoomInPosition + new Vector3(0, 2.5f, -5f);

        transform.position = ZoomInPosition;
       
        if (held)
        {
            transform.position = Vector3.Lerp(ZoomInPosition, ZoomOutPosition, 0.5f); 
        }
        
    }


  
}

