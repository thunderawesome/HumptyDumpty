using UnityEngine;

public class DialogController : MonoBehaviour
{
    #region Public Variables

    public static DialogController Instance;

    #endregion

    #region Private Variables

    [SerializeField]
    private Transform[] dialogSets;

    private int index = 0;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        Instance = this;
        dialogSets = transform.GetComponentsOnlyInChildren<Transform>();
        dialogSets[index].gameObject.SetActive(true);
    }

    #endregion

    #region Public Methods

    public void MoveToNext()
    {
        dialogSets[index].gameObject.SetActive(false);
        if (index < dialogSets.Length - 1)
        {
            index++;
        }
        else
        {
            index = 0;
        }

        dialogSets[index].gameObject.SetActive(true);
    }

    public void DisableDialogSet()
    {
        gameObject.SetActive(false);
    }

    #endregion
}
