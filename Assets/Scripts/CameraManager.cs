using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public bool isMoving = false;

    public float currentTime = 0f;
    public float timeToMove = 2f;

    public Vector3 targetPosition;

    public AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isMoving == true)
        {
            if (currentTime <= timeToMove)
            {
                currentTime += Time.deltaTime;
                // The step size is equal to speed times frame time.
                float step = (timeToMove * Time.deltaTime)/12f;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
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
