using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public static void Stop()
    {
        var go = FindObjectOfType<Music>();
        go?.StopMusic();
        Destroy(go);
    }

    public static void Play()
    {
        var go = FindObjectOfType<Music>();
        go?.PlayMusic();
    }

    public enum Kind
    {
        Background, Game
    }
}