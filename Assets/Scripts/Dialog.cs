using UnityEngine;

public enum Type
{
    Neutral = 0,
    VeryBad = -10,
    Bad = -5,
    Good = 5,
    VeryGood = 10
}

public class Dialog : MonoBehaviour
{
    #region Public Variables

    public Type dialogType = Type.Neutral;

    #endregion

    #region Public Methods

    public void DialogChosen()
    {
        GameManager.Instance.ChangeMeterValue((float)dialogType);
    }

    #endregion

}
