using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BounceOffScreenEdges : MonoBehaviour {

    //private Vector2 m_force;

    public const float MIN_FORCE = 100.0f;
    public const float MAX_FORCE = 300.0f;

    private void Start()
    {
        bool makeMinNegative = Random.Range(0, 1) == 1;
        bool makeMaxNegative = Random.Range(0, 1) == 1;

        var minForce = Random.Range(MIN_FORCE, MAX_FORCE);

        if (makeMinNegative == true)
        {
            minForce *= -1;
        }

        var maxForce = Random.Range(MIN_FORCE, MAX_FORCE);

        if (makeMaxNegative == true)
        {
            maxForce *= -1;
        }

        var m_force = new Vector2(minForce, maxForce);

        GetComponent<Rigidbody2D>().AddForce(m_force, ForceMode2D.Impulse);
    }
}
