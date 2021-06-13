using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play2DSFX : MonoBehaviour
{
    AudioSource sfxSource = null;
    // Start is called before the first frame update
    void Start()
    {
        sfxSource = FindObjectOfType<SFX2DSource>().GetComponent<AudioSource>();
    }

    public void playSFX(AudioClip clip)
    {
        sfxSource.Stop();
        sfxSource.clip = clip;
        sfxSource.Play();
    }
}
