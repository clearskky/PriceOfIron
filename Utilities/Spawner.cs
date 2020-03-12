using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int MaximumNumberOfEnemies;
    [SerializeField] private Transform EnemyContainer;
    SpawnPoint ClosestValidSpawnPoint;
    public int CurrentNumberOfEnemies;

    void FixedUpdate()
    {
        CheckNumberOfEnemiesAlive();
    }
    public void CheckNumberOfEnemiesAlive()
    {
        CurrentNumberOfEnemies = EnemyContainer.childCount;
        if (CurrentNumberOfEnemies < MaximumNumberOfEnemies)
        {
            Debug.Log(string.Format("There are only {0} enemies, {1} more needs to be spawned", CurrentNumberOfEnemies, (MaximumNumberOfEnemies - CurrentNumberOfEnemies)));
            DetermineSpawnPoint();
        }
    }
    public void UpdateCurrentEnemyCount(int incrementAmount)
    {
        CurrentNumberOfEnemies += incrementAmount;
    }
    void DetermineSpawnPoint()
    {
        float ShortestDistance = 1000; //The value of this variable should be greater than half the distance between two furthest spawn points on the map
        foreach (Transform sp in transform)
        {
            SpawnPoint spawnPoint = sp.GetComponent<SpawnPoint>(); 
            if (spawnPoint.DistanceBetweenPlayer < ShortestDistance && spawnPoint.CheckIfValid())
            {
                ShortestDistance = spawnPoint.DistanceBetweenPlayer; //We update this value in future iterations of the loop for determining the closest SpawnPoint
                ClosestValidSpawnPoint = spawnPoint;
            }
        }
        ClosestValidSpawnPoint.InstantiateEnemy();
    }
}
