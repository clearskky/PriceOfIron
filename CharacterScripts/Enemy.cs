using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour, IDamagable
{
    /*
    * Abandon hope all ye who enter here
    */
    [SerializeField] private float AIRefreshRate;
    [SerializeField] private float MovementSpeed;
    [SerializeField] private int MaxHealth;
    [SerializeField] private AudioHandler audioHandler;
    [SerializeField] private ParticleSystem explosion;
    public int DetectionRadius;
    public bool isTriggered;
    public bool isDying;
    private int CurrentHealth;
    GameHandler gameHandler;
    NavMeshAgent Pathfinder;
    Transform Player;
    Animator anim;
    HealthBar healthBar;
    Canvas healthbarCanvas;
    void Awake()
    {
        isDying = false;
        healthbarCanvas = transform.GetChild(1).GetComponent<Canvas>();
        healthBar = transform.GetChild(1).GetComponent<HealthBar>();
        anim = transform.GetChild(0).GetComponent<Animator>(); //First child of the enemy object is the enemy model itself
        gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Pathfinder = GetComponent<NavMeshAgent>();
        Pathfinder.speed = MovementSpeed;
        CurrentHealth = MaxHealth;
    }
    void Update()
    {
        AnimateTheEnemy();
    }
    public void AnimateTheEnemy()
    {
        try // The bulk of the code is in a try-catch block because the code gives out errors if the player is dead, we use that to make bots assume their idle positions when that happens.
        {
            if (Vector3.Distance(Player.position, transform.position) < Pathfinder.stoppingDistance && Player != null) // Enemy needs to face constantly face the player if he is within attacking range so the stabs don't miss.
            {
                anim.SetBool("attacking", true);
                Vector3 targetDirection = (Player.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
            }
            else
            {
                anim.SetBool("attacking", false);
            }
        }
        catch (System.Exception)
        {
            anim.SetBool("attacking", false);
            anim.SetBool("isTriggered", false);
            audioHandler.PlayVictoryClip();
        }
    }
    public void SeekPlayer()
    {
        StartCoroutine(seekPlayer()); //Couldn't find a way to insert a whole code block inside he StartCoroutine() method so I called my own method inside it.
    }
    public IEnumerator seekPlayer()
    {
        if (!isDying)
        {
            isTriggered = true;
            anim.SetBool("isTriggered", true);
            audioHandler.PlayTriggeredClip();
            while (Player != null && isTriggered)
            {
                Vector3 targetPosition = new Vector3(Player.position.x, 0, Player.position.z); // NavMeshAgent doesn't need to concern itself with the Y axis since the AI can only move horizontally.
                Pathfinder.SetDestination(targetPosition);
                yield return new WaitForSeconds(AIRefreshRate); //The parameter is the refresh rate of the Enemy pathfinder, you can set it from the Inspector.
            }
        }
    }
    public void TakeDamage(int Damage)
    {
        CurrentHealth -= Damage;
        float NewHealthPercentage = (float)CurrentHealth / (float)MaxHealth;
        if (CurrentHealth <= 0)
        {
            if (!isDying)
            {
                DisableColliders();
                isDying = true;
                isTriggered = false;
                explosion.Play();
                audioHandler.PlayDeathClip();
                healthbarCanvas.enabled = false;
                anim.enabled = false; //Disabling the animator ragdolls the enemy
                gameHandler.UpdateScore();
                Destroy(this.gameObject, 2);
            }
        }
        else
        {
            audioHandler.PlayDamageClip();
            healthBar.HandleHealthChange(NewHealthPercentage);
        }
    }

    private void DisableColliders()
    {
        BoxCollider boxCollider = transform.GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }
}
