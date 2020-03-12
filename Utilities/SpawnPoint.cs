using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject EnemyContainer;
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private Transform Player;
    [SerializeField] private Spawner spawner;
    public bool isValidSpawnPoint;
    public float DistanceBetweenPlayer;
    RaycastHit hit;

    void FixedUpdate()
    {
        //ShowRay();
        //CheckIfValid();
        CheckDistanceBetweenPlayer();
    }
    void ShowRay()
    {
        Debug.DrawRay(transform.position, transform.up * 2f, Color.red);
    }
    public bool CheckIfValid()
    {
        //Debug.Log("CheckIfValid is called");
        if (Physics.Raycast(transform.position, transform.up, out hit, 2f))
        {
            isValidSpawnPoint = false;
            Debug.Log(string.Format("Invalid spawn point, {0} is on top of me", hit.transform.name));
        }
        else
        {
            isValidSpawnPoint = true;
        }
        return isValidSpawnPoint;
    }
    void CheckDistanceBetweenPlayer()
    {
        if (Player != null)
        {
            DistanceBetweenPlayer = Vector3.Distance(transform.position, Player.position);
        }
    }
    public void InstantiateEnemy()
    {
        Debug.Log("Spawning an enemy");
        GameObject enemy = Instantiate(EnemyPrefab, transform.position, transform.rotation);
        enemy.transform.parent = EnemyContainer.transform;
        //spawner.CurrentNumberOfEnemies += 1;
    }
}