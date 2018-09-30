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

    public Image fadeToBlack;
    public TMPro.TextMeshProUGUI gameOverTextMeshPro;

    public string winningText = "You did the right thing";
    public string losingText = "You should be ashamed of yourself";

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    #endregion

    #region Public Methods

    public void ChangeMeterValue(float value)
    {

        // TJS: Blends the happy/sad face animation of the egg depending on the slider value
        animator.SetFloat("Blend", willToLiveMeterValue / MAX_METER_VALUE);

        PlaySFXDependingOnPositiveOrNegativeValue(value);

        if (value == 0) { return; }

        willToLiveMeterValue += value;

        if (willToLiveMeterValue <= MIN_METER_VALUE)
        {
            StartCoroutine(GameOver());
            return;
        }

        if (willToLiveMeterValue >= MAX_METER_VALUE)
        {
            StartCoroutine(NotAnAsshole());
            return;
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

    private IEnumerator NotAnAsshole()
    {
        willToLiveMeterValue = MAX_METER_VALUE;
        willToLiveSlider.value = willToLiveMeterValue;

        DialogController.Instance.DisableAll();

        var fadeTime = 5f;

        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / fadeTime;
            fadeToBlack.color = Color.Lerp(fadeToBlack.color, Color.black, t);

            yield return null;
        }


        //AudioManager.Instance.PlaySoundFX(AudioManager.Instance.GetComponent<AudioSource>(), AudioManager.Instance.eggBreakSFX);
        gameOverTextMeshPro.text = winningText;

        fadeTime = 124f;
        t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / fadeTime;            
            gameOverTextMeshPro.color = Color.Lerp(gameOverTextMeshPro.color, Color.white, t);
        }
    }

    private IEnumerator GameOver()
    {
        willToLiveMeterValue = MIN_METER_VALUE;
        willToLiveSlider.value = willToLiveMeterValue;

        DialogController.Instance.DisableAll();

        var fadeTime = 5f;

        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / fadeTime;
            fadeToBlack.color = Color.Lerp(fadeToBlack.color, Color.black, t);

            yield return null;
        }

        
        AudioManager.Instance.PlaySoundFX(AudioManager.Instance.GetComponent<AudioSource>(), AudioManager.Instance.eggBreakSFX);
        gameOverTextMeshPro.text = losingText;

        fadeTime = 124f;
        t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / fadeTime;           
            gameOverTextMeshPro.color = Color.Lerp(gameOverTextMeshPro.color, Color.white, t);
        }
    }



    #endregion
}
