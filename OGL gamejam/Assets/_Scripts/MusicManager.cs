using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip intro, musicLoop;
    // Start is called before the first frame update
    private void Awake() {
        audioSource = GetComponent<AudioSource>();  

    }
    IEnumerator Start()
    {
        PlayClip(intro,false);
        do{
            yield return null;
        }while(audioSource.isPlaying);

        PlayClip(musicLoop, true);
        yield return null;
    }

    public void PlayClip(AudioClip clip, bool loop)
    {
        Debug.Log("Playing "+ clip.name);
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }
}
