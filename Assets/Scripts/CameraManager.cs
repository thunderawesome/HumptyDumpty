using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [HideInInspector]
    public Vector3 startingPosition;
    public Vector3 finalCamPosition;
    [HideInInspector]
    public Vector3 targetPosition;
    public AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startingPosition = transform.position;
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }
}
