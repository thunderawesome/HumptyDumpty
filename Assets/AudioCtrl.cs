using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SFX{
    UP
    ,DOWN

}

public class AudioCtrl : MonoBehaviour {
    //AUDIO===========================
    private int pooledAudioCount = 30;
    private int availableAudioObjs;

    public AudioClip[] ALL_AUDIO;
    private List<GameObject> AudioObjs;
    public GameObject AudioObj;

    public static AudioCtrl Instance;

    private static readonly Quaternion QUATERNION_IDENTITY = new Quaternion(0, 0, 0, 1);

    void Awake()
    {
        Instance = this;

        //AUDIO===================================
        availableAudioObjs = (pooledAudioCount - 1);
        AudioObjs = new List<GameObject>();
        for (int i = 0; i < pooledAudioCount; i++)
        {
            GameObject obj = (GameObject)Instantiate(AudioObj, gameObject.transform.position, QUATERNION_IDENTITY);
            AudioObjs.Add(obj);
        }
        for (int j = 0; j < AudioObjs.Count; j++)
        {
            AudioObjs[j].transform.parent = gameObject.transform;
            AudioObjs[j].SetActive(false);
        }
    }
	
    public IEnumerator PlayAudio(SFX sfx)
    {
        //DECLARE
        GameObject audObj = getActiveAudioObj();
        audObj.SetActive(true);
        AudioSource ao = audObj.GetComponent<AudioSource>();
        ao.clip = getAudioClip(sfx);
        ao.Play();

        bool isPlaying = true;

        while(isPlaying)
        {
            if (!ao.isPlaying)
                isPlaying = false;
            
            yield return new WaitForSeconds(0.5F);
        }

        yield return new WaitForSeconds(0.5F);

        audObj.SetActive(false);
        addAvailableAudioObjs();

        yield return null;
    }

    public GameObject getActiveAudioObj()
    {
        availableAudioObjs -= 1;
        for (int i = 0; i < AudioObjs.Count; i++)
        {
            if (!AudioObjs[i].activeSelf)
            {
                AudioObjs[i].SetActive(true);
                return AudioObjs[i];
            }
            else if (i == (AudioObjs.Count - 1))
            {
                GameObject obj = (GameObject)Instantiate(AudioObj, gameObject.transform.position, QUATERNION_IDENTITY);
                AudioObjs.Add(obj);
                pooledAudioCount += 1;
                AudioObjs[(pooledAudioCount - 1)].transform.parent = gameObject.transform;
                AudioObjs[(pooledAudioCount - 1)].SetActive(false);
            }
        }
        return new GameObject();
    }

    public void addAvailableAudioObjs()
    {
        if (availableAudioObjs != (pooledAudioCount - 1))
            availableAudioObjs += 1;
    }
    private AudioClip getAudioClip(SFX snd)
    {
        AudioClip audioReturn = null;

        switch (snd)
        {
            case SFX.UP: audioReturn = ALL_AUDIO[0]; break;
            case SFX.DOWN: audioReturn = ALL_AUDIO[1]; break;
            default: audioReturn = ALL_AUDIO[0]; break;
        }
        return audioReturn;
    }
}
