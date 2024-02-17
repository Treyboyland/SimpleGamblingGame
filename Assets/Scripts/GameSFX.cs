using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSFX : MonoBehaviour
{
    [SerializeField]
    AudioPool pool;

    [SerializeField]
    ClipWithVolume buttonSound;

    [SerializeField]
    ClipWithVolume playButtonSound;

    public void PlayPlayButtonSound()
    {
        PlaySound(playButtonSound);
    }

    public void PlayButtonSound()
    {
        PlaySound(buttonSound);
    }

    void PlaySound(ClipWithVolume clip)
    {
        var sound = pool.GetObject();
        sound.ClipWithVolume = clip;
        sound.gameObject.SetActive(true);
    }
}
