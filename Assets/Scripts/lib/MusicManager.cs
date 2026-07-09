using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [System.Serializable]
    public struct Track
    {
        public string name;
        public AudioClip clip;
    }

    [SerializeField] private Track[] tracks;
    [SerializeField] private AudioSource audioSource;

    private Dictionary<string, AudioClip> lookup;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        // build name then clip map once
        lookup = new Dictionary<string, AudioClip>();
        foreach (var t in tracks)
        {
            lookup[t.name] = t.clip;
        }
    }

    public void PlayMusic(string name, bool loop = true)
    {
        if (!lookup.TryGetValue(name, out AudioClip clip))
        {
            Debug.LogWarning($"Track '{name}' not found");
            return;
        }

        // don't restart if this track is already playing
        if (audioSource.clip == clip && audioSource.isPlaying) return;

        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
    }
}
