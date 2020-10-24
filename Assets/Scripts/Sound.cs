using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    [Range(0f, 1f)]
    public float volume;
    [Range(-3f, 3f)]
    public float pitch;
    [Range(0, 256)]
    public int priorety;
    public bool looping;
    public bool playOnAwake;
    public AudioClip clip;
    [HideInInspector]
    public AudioSource audioSource;
}
