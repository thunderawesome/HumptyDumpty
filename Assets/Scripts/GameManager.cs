using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Public Variables

    public static GameManager Instance;

    public float willToLiveMeterValue = 100.0f;

    public Slider willToLiveSlider;

    public Animator animator;

    #endregion

    #region Private Variables

    private const float MIN_METER_VALUE = 0.0f;
    private const float MAX_METER_VALUE = 100.0f;

    private Camera m_mainCamera;

    private CameraManager m_camManager;


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

        // TJS: Blends the happy/sad face animation of the egg depending on the slider value
        animator.SetFloat("Blend", willToLiveMeterValue / MAX_METER_VALUE);

        PlaySFXDependingOnPositiveOrNegativeValue(value);

        if (willToLiveMeterValue <= MIN_METER_VALUE)
        {
            StartCoroutine(GameOver());
            return;
        }

        if (willToLiveMeterValue >= MAX_METER_VALUE)
        {
            willToLiveMeterValue = MAX_METER_VALUE;
        }

        willToLiveSlider.value = willToLiveMeterValue;
        MoveTheCameraBasedOnTheMeterValue();
    }

    private void MoveTheCameraBasedOnTheMeterValue()
    {
        m_camManager.targetPosition = Vector3.Lerp(m_camManager.startingPosition, m_camManager.finalCamPosition, (MAX_METER_VALUE - willToLiveMeterValue) / MAX_METER_VALUE);
        m_camManager.StartCoroutine(m_camManager.MoveToPosition(m_mainCamera.transform, m_camManager.targetPosition, 2));
    }

    private void PlaySFXDependingOnPositiveOrNegativeValue(float value)
    {
        // TJS: if the value is greater than zero then it means it is a positive thing (UP sfx)
        var sfxIndex = value > 0 ? 0 : 1;
        AudioManager.Instance.PlaySoundFX(m_camManager.audioSource, AudioManager.Instance.cameraSFX[sfxIndex]);
    }

    private IEnumerator GameOver()
    {
        willToLiveMeterValue = MIN_METER_VALUE;
        willToLiveSlider.value = willToLiveMeterValue;

        yield return null; //TODO: Fade to black
    }

    #endregion
}
