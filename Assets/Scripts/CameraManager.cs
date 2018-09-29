using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public bool isMoving = false;

    float currentTime = 0f;
    float timeToMove = 2f;

    public Vector3 targetPosition;

    void Update()
    {
        if (isMoving == true)
        {
            if (currentTime <= timeToMove)
            {
                currentTime += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, targetPosition, currentTime / timeToMove);
            }
            else
            {
               // transform.position.x = 2.3f;
                currentTime = 0f;
                isMoving = false;
            }
        }
    }


}
