using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioClip menuMusic;
    public AudioClip levelMusic;
    public AudioClip levelvictori;
    public AudioClip leveldead;
    private AudioSource audioSource;

    private void Awake()
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

        audioSource = GetComponent<AudioSource>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "menu")
        {
            PlayMusic(menuMusic);
        }
        else if (scene.name == "nivel 1" || scene.name == "perdiste" || scene.name == "ganaste")
        {
            PlayMusic(levelMusic);
            PlayMusic(levelvictori);
            PlayMusic(leveldead);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip)
            return; 

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
