using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Public Variables

    public static GameManager Instance;

    public float willToLiveMeterValue = 100.0f;

    public Slider willToLiveSlider;

    #endregion

    #region Private Variables

    private const float MIN_METER_VALUE = 0.0f;
    private const float MAX_METER_VALUE = 100.0f;

    private Camera m_mainCamera;

    private CameraManager m_camManager;

    public Vector3 finalCamPosition;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        Instance = this;

        m_mainCamera = Camera.main;
        m_camManager = m_mainCamera.GetComponent<CameraManager>();
    }

    #endregion

    #region Public Methods

    public void ChangeMeterValue(float value)
    {
        willToLiveMeterValue += value;

        if (willToLiveMeterValue <= MIN_METER_VALUE)
        {
            //TODO: Game over
            GameOver();
            return;
        }

        if (willToLiveMeterValue >= MAX_METER_VALUE)
        {
            willToLiveMeterValue = MAX_METER_VALUE;
        }

        willToLiveSlider.value = willToLiveMeterValue;

        m_camManager.isMoving = true;
        if (value <= 0)
            StartCoroutine(AudioCtrl.Instance.PlayAudio(SFX.DOWN));
        else
        {
            StartCoroutine(AudioCtrl.Instance.PlayAudio(SFX.UP));
        }

        m_camManager.targetPosition = Vector3.Lerp(m_mainCamera.transform.position, finalCamPosition, (MAX_METER_VALUE - willToLiveMeterValue)/MAX_METER_VALUE);
    }

    private void GameOver()
    {
        willToLiveMeterValue = MIN_METER_VALUE;
        willToLiveSlider.value = willToLiveMeterValue;
    }

    #endregion
}
