using UnityEngine;

public class AudioManagerSingleton : MonoBehaviour
{
    public static AudioManagerSingleton audioManager;
    public AudioSource effect;
    public AudioSource music;
    void Awake()
    {
        if (audioManager == null) { audioManager = this; DontDestroyOnLoad(gameObject); }
        else {
            Destroy(gameObject);
        }
    }

    public void PlayOneShot(AudioClip clip)
    {
        effect.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (music != null && clip != null)
        {
            if (music.clip != clip)
            {
                music.clip = clip;
                music.loop = loop;
                music.Play();
            }
        }
    }
}
