using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioClip AudioClip1;

    public Dictionary<string, AudioClip> MusicData = new();

    public GameObject AudioReproducerPrefab;

    public int PoolSize = 10;

    public List<GameObject> AudioPool = new();


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        // Crear pool correctamente
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject obj = Instantiate(AudioReproducerPrefab, transform);
            obj.SetActive(false);
            AudioPool.Add(obj);
        }
    }

    void Start()
    {
        MusicData.Add("ClickButton", AudioClip1);
    }

    public void PlaySound(string soundName, float volume)
    {
        if (MusicData.TryGetValue(soundName, out AudioClip clip))
        {
            GameObject obj = GetAvalibleSoundReproducer();
            if (obj == null)
            {
                print("NO HAY OBJETOS EN EL POOL");
                return;
            }

            AudioSource audioSource = obj.GetComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = volume;

            obj.SetActive(true); 

            audioSource.GetComponent<AudioReproducer>().SetAudio();
        }
        else
        {
            print("no existe");
        }
    }

    public GameObject GetAvalibleSoundReproducer()
    {
        foreach (var item in AudioPool)
        {
            if (!item.activeSelf)  
                return item;
        }

        return null;
    }
}
