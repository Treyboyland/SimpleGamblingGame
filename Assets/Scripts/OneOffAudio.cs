using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneOffAudio : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    public ClipWithVolume ClipWithVolume
    {
        set
        {
            audioSource.clip = value.Clip;
            audioSource.volume = value.Volume;
        }
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(PlayThenDisable());
        }
    }

    IEnumerator PlayThenDisable()
    {
        audioSource.Play();
        yield return null;

        while (audioSource.isPlaying)
        {
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
