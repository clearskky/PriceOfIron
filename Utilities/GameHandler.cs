using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private GameObject EnemyHolder;
    [SerializeField] private UIHandler uiHandler;
    public Text ScoreText;
    public int score;
    public bool isGameOver;
    void Start()
    {
        uiHandler = GameObject.FindGameObjectWithTag("UIHandler").GetComponent<UIHandler>();
        uiHandler.StartLaunchRoutine();
        score = 0;
    }
    void Update()
    {
        CheckDistance();
        CheckIfGameOver();
    }
    public void UpdateScore()
    {
        score += 100;
        uiHandler.IncreaseScore(score);
    }
    private void CheckIfGameOver()
    {
        if (!isGameOver)
        {
            if (Player == null)
            {
                isGameOver = true;
                uiHandler.StartGameOverRoutine();
            }
        }
    }
    private void CheckDistance()
    {
        foreach (Transform Enemy in EnemyHolder.transform)
        {
            Enemy enemy = Enemy.GetComponent<Enemy>();
            Animator enemyAnimator = Enemy.GetChild(0).GetComponent<Animator>();
            if (!enemy.isDying && !enemy.isTriggered && Player != null)
            {
                float distance = Vector3.Distance(Player.position, Enemy.position);
                if (distance < enemy.DetectionRadius)
                {
                    enemy.SeekPlayer();
                }
            }
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
