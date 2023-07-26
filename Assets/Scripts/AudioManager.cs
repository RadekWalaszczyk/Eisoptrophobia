using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [SerializeField] List<Sound> Sounds = new List<Sound>();
    Dictionary<string, Sound> SoundsDict = new Dictionary<string, Sound>();

    public static AudioManager inst;
    void Awake()
    {
        if (inst != null)
            Destroy(gameObject);
        else
            inst = this;

        DontDestroyOnLoad(gameObject);        

        foreach (var Sound in Sounds)
        {
            Sound.source = gameObject.AddComponent<AudioSource>();
            Sound.source.clip = Sound.clip;

            Sound.source.volume = Sound.volume;
            Sound.source.pitch = Sound.pitch;

            SoundsDict.Add(Sound.soundName, Sound);
        }
    }

    public void PlaySoundByName(string name, float pitchDiffrence = 0f)
    {
        if (SoundsDict.ContainsKey(name))
        {
            float randPitch = SoundsDict[name].pitch + Random.Range(-pitchDiffrence, pitchDiffrence);
            SoundsDict[name].source.pitch = randPitch;
            SoundsDict[name].source.PlayOneShot(SoundsDict[name].clip);
            SoundsDict[name].source.pitch = SoundsDict[name].pitch;
        }
    }
}

[Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}
