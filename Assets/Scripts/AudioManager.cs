using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

/*
 * This is only for sounds that are not spacific to some gameObject i.e. the player,
 * enemy, or some interactable item. It is meant to be used for theme songs and other
 * things along those lines.
 * 
 * Another example of where to use this is for the button enter sound.
 */
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        // make sure there is only ever one AudioManager in all scenes
		#region Singlton
		if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
		#endregion

		// make the sound not get cut off when a new scene is loaded
		DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.priority = s.priorety;
            s.audioSource.loop = s.looping;
            s.audioSource.playOnAwake = s.playOnAwake;
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    private Sound GetSound(String name)
    {
        return Array.Find(sounds, sound => sound.name == name);
    }

    private IEnumerator FadeOutCoroutine(string name, float transitionTime)
    {
        Sound s = GetSound(name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + "could not be found. Chech spelling" +
                "and to see if that sound is in the array.");
            yield break;
        }

        for(float i = transitionTime; i > 0; i--)
        {
            s.audioSource.volume -= (1 / transitionTime);
            yield return new WaitForSeconds(0.01f);
        }
        
    }

    private IEnumerator FadeInCoroutine(string name, float transitionTime)
    {
        Sound s = GetSound(name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + "could not be found. Chech spelling" +
                "and to see if that sound is in the array.");
            yield break;
        }

        for (float i = transitionTime; i > 0; i--)
        {
            s.audioSource.volume += (1 / transitionTime);
            yield return new WaitForSeconds(0.01f);
        }

    }

    public void FadeOut(string name, float transitionTime)
    {
        StartCoroutine(FadeOutCoroutine(name, transitionTime));
    }

    public void FadeIn(string name, float transitionTime)
    {
        StartCoroutine(FadeInCoroutine(name, transitionTime));
    }

    public void Play(string name)
    {
        Sound s = GetSound(name);
        if(s == null)
        {
            Debug.LogWarning("Sound " + name + "could not be found. Chech spelling" +
                "and to see if that sound is in the array.");
            return;
        }
        s.audioSource.Play();
    }
}
