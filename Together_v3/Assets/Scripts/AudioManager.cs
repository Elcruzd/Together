using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.isPlaying = false;
        }
    }

    void Start() 
    {
        Play("background_cricket");
        Play("background_rain");
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s.source.isPlaying == false) {
            // Debug.Log("isplaying is false");
            // Debug.Log(s.name);
            s.source.Play();
            s.isPlaying = true;
        }
        // if(PauseMenu.GameIsPaused) {
        //     s.source.pitch *= .5f;
        // }
    }
    public void Pause(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Pause();
        s.isPlaying = false;
    }
    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
        s.isPlaying = false;
    }

    public void ChangeSFXVolume(SoundType type, float volume) {
        foreach (Sound s in sounds) 
        {
            if (s.type == type) {
                s.volume = volume;
                s.source.volume = volume;
                Debug.Log(s.name + " changed volume: " + s.volume + " : " + volume);
            }    
        }
    }
}
