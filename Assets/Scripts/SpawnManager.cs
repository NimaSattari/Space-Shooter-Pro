using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject EnemyContainer;
    [SerializeField] private GameObject[] PowerUp;
    [SerializeField] private GameObject PowerUpContainer;
    private bool StopSpawning = false;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2.5f);
        while (StopSpawning == false)
        {
            Vector3 posSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(EnemyPrefab, posSpawn, Quaternion.identity);
            newEnemy.transform.parent = EnemyContainer.transform;
            yield return new WaitForSeconds(5f);
        }
    }
    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(2.5f);
        while (StopSpawning == false)
        {
            Vector3 posSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newPowerUp = Instantiate(PowerUp[Random.Range(0, 3)], posSpawn, Quaternion.identity);
            newPowerUp.transform.parent = PowerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(10f, 20f));
        }
    }
    public void OnPlayerDeath()
    {
        StopSpawning = true;
    }
}
