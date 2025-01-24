using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // 적 프리팹 배열
    public int[] spawnWeights; // 각 적의 등장 확률 가중치
    public GameObject fuelPrefab; // 연료 아이템 프리팹

    public float enemySpawnIntervalMin = 3f; // 최소 적 스폰 간격
    public float enemySpawnIntervalMax = 4f; // 최대 적 스폰 간격
    public float fuelSpawnInterval = 10f; // 연료 생성 간격

    public float spawnY = 10f; // Y축 스폰 위치
    public float spawnRangeXMin = -1.09f; // X축 최소값
    public float spawnRangeXMax = 1.09f; // X축 최대값
    public float spawnRadius = 1.0f; // 겹침 방지 거리

    public LayerMask spawnCheckLayer; // 충돌 검사 레이어

    private int totalWeight; // 전체 가중치 합
    private bool isSpawning = true; // 스폰 상태 플래그

    public void InitializeSpawner()
    {
        // 가중치 합 계산
        totalWeight = 0;
        foreach (int weight in spawnWeights)
        {
            totalWeight += weight;
        }

        isSpawning = true; // 스폰 활성화
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnFuel());
    }
    
    public IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            float spawnX = Random.Range(spawnRangeXMin, spawnRangeXMax);
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

            if (IsPositionValid(spawnPosition))
            {
                GameObject enemyPrefab = ChooseRandomEnemy();
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
            yield return new WaitForSeconds(Random.Range(enemySpawnIntervalMin, enemySpawnIntervalMax));
        }
    }

    public IEnumerator SpawnFuel()
    {
        while (isSpawning)
        {
            float spawnX = Random.Range(spawnRangeXMin, spawnRangeXMax);
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

            if (IsPositionValid(spawnPosition))
            {
                Instantiate(fuelPrefab, spawnPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(fuelSpawnInterval);
        }
    }

    private GameObject ChooseRandomEnemy()
    {
        int randomValue = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            cumulativeWeight += spawnWeights[i];
            if (randomValue < cumulativeWeight)
            {
                return enemyPrefabs[i];
            }
        }

        return enemyPrefabs[0];
    }

    private bool IsPositionValid(Vector3 position)
    {
        return !Physics2D.OverlapCircle(position, spawnRadius, spawnCheckLayer);
    }
    
    public void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines();
    }
}

