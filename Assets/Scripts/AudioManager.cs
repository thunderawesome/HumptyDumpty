using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Public Variables

    public static AudioManager Instance;

    public AudioClip[] cameraSFX;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    #region Public Methods

    public void PlaySoundFX(AudioSource source, AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    #endregion
}
