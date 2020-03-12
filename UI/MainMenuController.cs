using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject HowToPlayMenu;
    [SerializeField] GameObject SoundMenu;
    [SerializeField] AudioMixer MusicMixer;
    [SerializeField] AudioMixer SFXMixer;
    [SerializeField] AudioMixer VoiceMixer;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ReturnToMainMenu();
        //AdjustMusicVolume(-30);
    }
    public void ReturnToMainMenu()
    {
        HowToPlayMenu.SetActive(false);
        SoundMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void OpenHowToPlayMenu()
    {
        MainMenu.SetActive(false);
        SoundMenu.SetActive(false);
        HowToPlayMenu.SetActive(true);
    }
    public void OpenSettingsMenu()
    {
        HowToPlayMenu.SetActive(false);
        MainMenu.SetActive(false);
        SoundMenu.SetActive(true);
    }
    public void StartGame()
    {
        audioSource.Stop();
        SceneManager.LoadScene(1);
    }
    public void AdjustMusicVolume(float volume)
    {
        MusicMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
    public void AdjustSFXVolume(float volume)
    {
        SFXMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }
    public void AdjustVoiceVolume(float volume)
    {
        VoiceMixer.SetFloat("VoiceVolume", Mathf.Log10(volume) * 20);
    }
    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
