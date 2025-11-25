using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioReproducer : MonoBehaviour
{
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource> ();
    }

    void Start()
    {
        
    }

    public void SetAudio()
    {
        Invoke("DesactiveOdj", source.clip.length);
    }
    public void DesactiveOdj()
    {
        gameObject.SetActive(false);
    }
}
