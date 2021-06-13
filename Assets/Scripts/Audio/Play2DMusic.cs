using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play2DMusic : MonoBehaviour
{
    private AudioSource musicSource = null;
    [SerializeField]
    private float delayBeforeStart = 2f;

    // Start is called before the first frame update
    void Start()
    {
        musicSource = FindObjectOfType<Music2DSource>().GetComponent<AudioSource>();
    }

	public void PlayMusic(AudioClip clip)
	{
        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.PlayDelayed(delayBeforeStart);
	}
}
