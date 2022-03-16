using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    [HideInInspector]
    public Scene currentScene;

    public AudioMixerGroup SFXMixer;
    public AudioMixerGroup MusicMixer;
    public AudioMixer AudioMixer;
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
            if (s.type == SoundType.SFX) {
                s.source.outputAudioMixerGroup = SFXMixer;
            } 
            else {
                s.source.outputAudioMixerGroup = MusicMixer;
            }
        }
    }

    void Start() 
    {
        currentScene = SceneManager.GetActiveScene ();
        if (currentScene.name == "Menu") {
            Play("title_screen");
        }
        else {
            Pause("title_screen");
            Play("background_cricket");
            Play("background_rain");
        }
        
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        if(s.source.isPlaying == false) {
            // Debug.Log("isplaying is false");
            // Debug.Log(s.name);
            s.source.Play();
            s.isPlaying = true;
            Debug.Log(name + " Playing");
        }
        // if(PauseMenu.GameIsPaused) {
        //     s.source.pitch *= .5f;
        // }
    }
    public void Pause(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Pause();
        s.isPlaying = false;
        Debug.Log(name + " Paused");
    }
    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Stop();
        s.isPlaying = false;
    }

    public void ChangeVolume(SoundType type, float volume) {
        if(type == SoundType.SFX) {
            AudioMixer.SetFloat("SFXVolume", volume);
        }
        else {
            AudioMixer.SetFloat("MusicVolume", volume);
        }
    }

    public float GetVolume(SoundType type) {
        float toReturn = 1;
        if(type == SoundType.SFX) {
            AudioMixer.GetFloat("SFXVolume", out toReturn);
        }
        else {
            AudioMixer.GetFloat("MusicVolume", out toReturn);
        }
        return toReturn;
    }

    
}
