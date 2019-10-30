using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour {

    public static AudioSystem sharedInstance;
    public AudioClip option;
    public AudioClip enemyDie;
    public AudioClip playerAttack;
    public AudioClip stageMusic;
    public AudioClip playerDie;
    public AudioClip dieMusic;
    public AudioClip laughTrack;
    AudioSource audio;

    private void Start()
    {
        sharedInstance = GetComponent<AudioSystem>();
        audio = GetComponent<AudioSource>();
    }

    public void PlayOption()
    {
        audio.clip = option;
        audio.Play();
    }

    public void PlayEnemyDie()
    {
        audio.clip = enemyDie;
        audio.Play();
    }

    public void PlayPlayerAttack()
    {
        audio.clip = playerAttack;
        audio.Play();
    }

    public void PlayStageMusic()
    {
        audio.clip = stageMusic;
        audio.Play();
    }

    public void PlayPlayerDie()
    {
        audio.clip = playerDie;
        audio.Play();
    }

    public void PlayDieMusic()
    {
        audio.clip = dieMusic;
        audio.Play();
    }

    public void PlayLaugh()
    {
        audio.clip = laughTrack;
        audio.Play();
    }

    public void StopMusic()
    {
        audio.Stop();
    }
}
