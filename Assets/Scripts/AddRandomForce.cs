using UnityEngine;

public class AddRandomForce : MonoBehaviour
{
    #region Public Variables

    public float MIN_FORCE = 100.0f;
    public float MAX_FORCE = 300.0f;

    #endregion

    protected void Start()
    {
        bool makeXNegative = Random.Range(0, 1) == 1;
        bool makeYNegative = Random.Range(0, 1) == 1;
        bool makeZNegative = Random.Range(0, 1) == 1;

        var xForce = Random.Range(MIN_FORCE, MAX_FORCE);

        if (makeXNegative == true)
        {
            xForce *= -1;
        }

        var yForce = Random.Range(MIN_FORCE, MAX_FORCE);

        if (makeYNegative == true)
        {
            yForce *= -1;
        }

        var zForce = Random.Range(MIN_FORCE, MAX_FORCE);

        if (makeZNegative == true)
        {
            zForce *= -1;
        }

        var m_force = new Vector3(xForce, yForce, zForce);

        var rigidbody2D = GetComponent<Rigidbody2D>();
        if (rigidbody2D != null)
        {
            GetComponent<Rigidbody2D>().AddForce(m_force, ForceMode2D.Impulse);
        }

        var rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            GetComponent<Rigidbody>().AddForce(m_force, ForceMode.Acceleration);
        }
    }


}
