using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] walls;

    private const float maxSpawnY = 15f;
    private const float minSpawnY = -15f;
    private const float maxSpawnX = 15f;
    private const float minSpawnX = -15f;

    [SerializeField]
    private GameObject[] bats;

    [SerializeField]
    private GameObject[] spiders;

    [SerializeField]
    private GameObject[] slimes;

    [SerializeField]
    private GameObject[] OneEyes;

    [SerializeField]
    private GameObject[] bosseEnemies;

    private float countTime;
    private int level;
    private float reduceSpawnTime;

    private Vector3[] DiagonalPosition = {new Vector3(maxSpawnX, maxSpawnY, 0f), 
                                                new Vector3(maxSpawnX, minSpawnY, 0f), 
                                                new Vector3(minSpawnX, maxSpawnY, 0f), 
                                                new Vector3(minSpawnX, minSpawnY, 0f)};
    private Vector3[] HoriVertiPosition = {new Vector3(maxSpawnX, 0f, 0f),
                                                new Vector3(minSpawnX, 0f, 0f),
                                                new Vector3(0f, maxSpawnY, 0f),
                                                new Vector3(0f, minSpawnY, 0f),};
    private Quaternion SpawnRotation;

    private void Start() {
        countTime = 0;
        level = 0;
        reduceSpawnTime = 0;
        SpawnRotation = Quaternion.identity;
    }

    public void StartSpawning() {
        SpawnBats();
        StartCoroutine("EnemySpawn");
    }

    IEnumerator EnemySpawn() {
        while (GameManager.Instance.GetPlaying()) {
            yield return new WaitForSeconds(1f);
            countTime += 1;
            if (countTime % 180 == 0 && level < 3)  {
                level++;
                reduceSpawnTime = level * 2;
            }
            if (countTime % (11 - reduceSpawnTime) == 0) SpawnBats();
            if (countTime % (17 - reduceSpawnTime) == 0) SpawnSpiders();
            if (countTime % (23 - reduceSpawnTime) == 0) SpawnSlimes();
            if (countTime % (37 - reduceSpawnTime) == 0) {
                SpawnOneEye();
                SpawnBoss();
            }
        }
    }

    private void SpawnBats() {
        foreach(Vector3 pos in DiagonalPosition) {
            GameObject bat = Instantiate(bats[level], pos, SpawnRotation);
        }
    }

    private void SpawnSpiders() {
        foreach(Vector3 pos in HoriVertiPosition) {
            Instantiate(spiders[level], pos, SpawnRotation);
        }
    }

    private void SpawnSlimes() {
        foreach(Vector3 pos in HoriVertiPosition) {
            Instantiate(slimes[level], pos, SpawnRotation);
            Instantiate(slimes[level], pos, SpawnRotation);
            Instantiate(slimes[level], pos, SpawnRotation);
            Instantiate(slimes[level], pos, SpawnRotation);
            Instantiate(slimes[level], pos, SpawnRotation);
        }
    }

    private void SpawnOneEye() {
        int random_index = Random.Range(0, 4);
        Instantiate(OneEyes[level], DiagonalPosition[random_index], SpawnRotation);
    }

    private void SpawnBoss() {
        
    }
}
