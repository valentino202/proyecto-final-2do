using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioClip AudioClip1;

    public Dictionary<string, AudioClip> MusicData = new();

    public GameObject AudioReproducerPrefad;

    public int PoolSice = 10; 

    public List<GameObject> AudioPool = new();   


    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        for(int i = 0; i < PoolSice; i++)
        {
            Instantiate(AudioReproducerPrefad);
            {
                GameObject obj = Instantiate(AudioReproducerPrefad, transform);
                AudioPool.Add(obj);
            }
        }


    }
    void Start()
    {
        MusicData.Add("ClickButton", AudioClip1);
        
    }

    void Update()
    {
        
    }
    public void PlaySound(string soundName, float volume)
    {
        if (MusicData.TryGetValue(soundName, out AudioClip clip))
        {
            print(clip.name);

            AudioSource audioSource = GetAvalibleSoundReproducer().GetComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = volume;
            AudioReproducerPrefad.SetActive(true);
            audioSource.GetComponent<AudioReproducer>().SetAudio();
        }
        else
        {
            print("no existe");
        }
        
    }
    public GameObject GetAvalibleSoundReproducer()
    {
        foreach(var item in AudioPool)
        {
            if (item.activeSelf == true)
                return item;   
        }  
        return null;

    }
}
