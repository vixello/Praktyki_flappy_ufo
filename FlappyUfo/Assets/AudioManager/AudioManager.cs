using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource m_Source;
    [SerializeField] AudioSource sfx_Source;

    public AudioClip backgroundMusic;
    public AudioClip deathSound;
    public AudioClip jumpSound;

    private void Start()
    {
        m_Source.clip = backgroundMusic;
        m_Source.loop = true;
        m_Source.Play();
    }

    public void PlaySoundEffects(AudioClip clip)
    {
        if(sfx_Source.isPlaying)
        {
            StopPlaying();
        }
        sfx_Source.PlayOneShot(clip);
    }

    private IEnumerator StopPlaying()
    {
        yield return new WaitForSeconds(0.1f);
        sfx_Source.Stop();
    }
}
