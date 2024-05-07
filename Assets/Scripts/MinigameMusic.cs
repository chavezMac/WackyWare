using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameMusic : MonoBehaviour
{
    private AudioSource music;
    public float fadeDuration = 2f;

    private void Start()
    {
        music = GetComponent<AudioSource>();
    }

    public void FadeOutMusic()
    {
        StartCoroutine(FadeOutMusicRoutine());
    }

    private IEnumerator FadeOutMusicRoutine()
    {
        float startVolume = music.volume;

        // Calculate the rate at which to decrease the volume per frame
        float fadeSpeed = startVolume / fadeDuration;
        while (music.volume > 0)
        {
            music.volume -= fadeSpeed * Time.deltaTime;
            yield return null;
        }

        // Ensure the volume is set to zero when the fade-out is complete
        music.volume = 0;

        // Stop the audio playback after the fade-out is complete
        music.Stop();
    }
    
    public void FadeOutMusicFailure()
    {
        StartCoroutine(FadeOutMusicRoutineFailure());
    }

    private IEnumerator FadeOutMusicRoutineFailure()
    {
        float startVolume = music.pitch;

        // Calculate the rate at which to decrease the volume per frame
        float fadeSpeed = startVolume / fadeDuration;
        while (music.pitch > 0)
        {
            music.pitch -= fadeSpeed * Time.deltaTime;
            yield return null;
        }

        // Ensure the volume is set to zero when the fade-out is complete
        music.pitch = 0;

        // Stop the audio playback after the fade-out is complete
        music.Stop();
    }
}
