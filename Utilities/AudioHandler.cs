using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private List<AudioClip> HitSounds;
    [SerializeField] private List<AudioClip> TriggeredSounds;
    [SerializeField] private List<AudioClip> VictorySounds;
    [SerializeField] private List<AudioClip> DeathSounds;
    [SerializeField] private AudioSource hitAudioSource;
    [SerializeField] private AudioSource victoryAudioSource;
    [SerializeField] private AudioSource chargeAudioSource;
    [SerializeField] private AudioSource deathAudioSource;
    [SerializeField] private AudioSource batonHitAudioSource;
    [SerializeField] private AudioSource explosionAudioSource;
    AudioClip randomClip;
    System.Random random;

    void Awake()
    {
        random = new System.Random();
        AdjustVolumeByPlayerPrefs();
    }
    public void PlayDamageClip()
    {
        if (!hitAudioSource.isPlaying && !chargeAudioSource.isPlaying)
        {
            randomClip = HitSounds[random.Next(HitSounds.Count)];
            //Debug.Log("Random hit clip is:" + randomClip.name);
            hitAudioSource.clip = randomClip;
            //Debug.Log("Playing hit clip");
            hitAudioSource.Play();
        }
    }
    public void PlayVictoryClip()
    {
        if (!victoryAudioSource.isPlaying)
        {
            randomClip = VictorySounds[random.Next(VictorySounds.Count)];
            //Debug.Log("Random victory clip is:" + randomClip.name);
            victoryAudioSource.clip = randomClip;
            victoryAudioSource.Play();
        }
    }
    public void PlayTriggeredClip()
    {
        if (!chargeAudioSource.isPlaying)
        {
            randomClip = TriggeredSounds[random.Next(TriggeredSounds.Count)];
            //Debug.Log("Random triggered clip is:" + randomClip.name);
            chargeAudioSource.clip = randomClip;
            //Debug.Log("Playing triggered clip");
            chargeAudioSource.Play();
        }
    }
    public void PlayDeathClip()
    {
        if (!deathAudioSource.isPlaying)
        {
            randomClip = DeathSounds[random.Next(DeathSounds.Count)];
            //Debug.Log("Random death clip is:" + randomClip.name);
            deathAudioSource.clip = randomClip;
            //Debug.Log("Playing death clip");
            explosionAudioSource.Play();
            deathAudioSource.Play();

        }
    }
    public void PlayBatonClip()
    {
        if (!batonHitAudioSource.isPlaying)
        {
            batonHitAudioSource.Play();
        }
    }

    void AdjustVolumeByPlayerPrefs()
    {
        if (hitAudioSource != null)
        {
            hitAudioSource.volume = PlayerPrefs.GetFloat("VoiceVolume");
        }
        if (victoryAudioSource != null)
        {
            victoryAudioSource.volume = PlayerPrefs.GetFloat("VoiceVolume");
        }
        if (chargeAudioSource != null)
        {
            chargeAudioSource.volume = PlayerPrefs.GetFloat("VoiceVolume");
        }
        if (deathAudioSource != null)
        {
            deathAudioSource.volume = PlayerPrefs.GetFloat("VoiceVolume");
        }
        if (batonHitAudioSource != null)
        {
            batonHitAudioSource.volume = PlayerPrefs.GetFloat("SFXVolume");
        }
    }
}
