using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ClipWithVolume 
{
    [Tooltip("Audio Clip to play")]
    public AudioClip Clip;
    [Tooltip("Volume of clip to play")]
    public float Volume;
}
