using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public SoundType type;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    
    public bool isPlaying;
    public bool loop;
    [HideInInspector]
    public AudioSource source;
}
public enum SoundType {
    SFX,
    Music
}
