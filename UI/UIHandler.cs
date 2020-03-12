using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.Networking;

public class UIHandler : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject GameplayUI;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject VolumeSettingsPanel;
    [SerializeField] AudioSource mainMusic;
    [SerializeField] AudioMixer MusicMixer;
    [SerializeField] AudioMixer SFXMixer;
    [SerializeField] AudioMixer VoiceMixer;
    [SerializeField] GameHandler gameHandler;
    [SerializeField] Text ScoreText;
    [SerializeField] Text EndGameScoreText;
    public static bool gameIsPaused = false;
    void Start()
    {
        mainMusic = mainMusic.GetComponent<AudioSource>();
        VolumeSettingsPanel.SetActive(false);
        ScoreText = ScoreText.GetComponent<Text>();
        EndGameScoreText = EndGameScoreText.GetComponent<Text>();
        //AdjustMusicVolume(-20);
    }
    void Update()
    {
        CheckForPause();
    }
    void PauseGameRoutine()
    {
        if (!gameHandler.isGameOver && !gameIsPaused)
        {
            gameHandler.PauseGame();
            mainMusic.Pause();
            GameplayUI.SetActive(false);
            PausePanel.SetActive(true);
            gameIsPaused = true;
        }
    }
    public void ResumeGameRoutine()
    {
        if (!gameHandler.isGameOver && gameIsPaused)
        {
            mainMusic.UnPause();
            gameHandler.ResumeGame();
            GameplayUI.SetActive(true);
            PausePanel.SetActive(false);
            VolumeSettingsPanel.SetActive(false);
            gameIsPaused = false;
        }
    }
    public void SubmitScore()
    {
        Debug.Log("Submitting Score");
        //StartCoroutine(PostScores());
        StartCoroutine(_SubmitScore());

        //string URL = "http://localhost/priceofiron/insertscore.php";
        //WWWForm form = new WWWForm();
        //form.AddField("Score", gameHandler.score.ToString());
        //var w = UnityWebRequest.Post(URL, form);
        //_SubmitScore();
    }
    public IEnumerator PostScores()
    {     
        string post_url = "http://localhost/priceofiron/insertscore.php";

        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("score", gameHandler.score.ToString()));
        //WWWForm form = new WWWForm();        
        //form.AddField("Score", gameHandler.score);

        //WWW hs_post = new WWW(post_url, form);
        //yield return hs_post;
        UnityWebRequest hs_post = UnityWebRequest.Post(post_url, form);
        yield return hs_post.SendWebRequest(); // Wait until the download is done

        if (hs_post.error != null)
        {
            print("There was an error posting the high score: " + hs_post.error);
        }
        else
        {
            print("Highscore successfuly submitted!");
        }
    }
    public IEnumerator _SubmitScore()
    {
        string URL = "http://skysforge.name.tr/insertscore.php";
        //string URL = "http://localhost/priceofiron/insertscore.php";
        WWWForm form = new WWWForm();
        form.AddField("score", gameHandler.score.ToString());
        using (var w = UnityWebRequest.Post(URL, form)) 
        {
            yield return w.SendWebRequest();
            if (w.isNetworkError || w.isHttpError)
            {
                Debug.Log(w.error);
            }
            else
            {
                Debug.Log("Finished Uploading Score");
            }
        }
    }
    public void ReplayGame()
    {
        StartLaunchRoutine();
        gameHandler.ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        mainMusic.Stop();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void IncreaseScore(int Score)
    {
        ScoreText.text = "Score: " + Score.ToString();
    }
    public void StartLaunchRoutine()
    {
        GameplayUI.SetActive(true);
        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);
        VolumeSettingsPanel.SetActive(false);
        //GameplayUI.transform.gameObject.SetActive(false);
        //GameOverPanel.transform.gameObject.SetActive(false);
        //PausePanel.transform.gameObject.SetActive(false);
    }
    public void StartGameOverRoutine()
    {
        mainMusic.Stop();
        GameplayUI.SetActive(false);
        GameOverPanel.SetActive(true);
        EndGameScoreText.text += gameHandler.score.ToString(); 
    }
    void CheckForPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                ResumeGameRoutine();
            }
            else
            {
                PauseGameRoutine();
            }
        }
    }
    public void OpenVolumeSettings()
    {
        PausePanel.SetActive(false);
        VolumeSettingsPanel.SetActive(true);
    }
    public void CloseVolumeSettings()
    {
        PausePanel.SetActive(true);
        VolumeSettingsPanel.SetActive(false);
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
}
