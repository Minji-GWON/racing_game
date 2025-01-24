using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{   
    public GameObject[] enemyPrefabs; // 적 프리팹 배열
    public int[] spawnWeights; // 각 적의 등장 확률 가중치
    public float spawnIntervalMin = 3f; // 최소 스폰 간격
    public float spawnIntervalMax = 4f; // 최대 스폰 간격
    public float spawnY = 10f; // Y축 스폰 위치
    public float spawnRangeXMin = -0.8f; // X축 최소값
    public float spawnRangeXMax = 0.92f; // X축 최대값

    private int totalWeight; // 전체 가중치 합
    
    private bool isSpawning = true; // 스폰 상태 플래그

    private void Start()
    {
        // 전체 가중치 합 계산
        foreach (int weight in spawnWeights)
        {
            totalWeight += weight;
        }

        StartSpawning();
    }

    private void StartSpawning()
    {
        if (isSpawning)
        {
            // 일정 간격으로 적 스폰 예약
            Invoke(nameof(SpawnEnemy), Random.Range(spawnIntervalMin, spawnIntervalMax));
        }
    }

    private void SpawnEnemy()
    {
        if (!isSpawning) return; // 스폰 중단 시 실행하지 않음

        // 랜덤한 X 위치 계산
        float spawnX = Random.Range(spawnRangeXMin, spawnRangeXMax);

        // 랜덤한 적 프리팹 선택
        GameObject enemyPrefab = ChooseRandomEnemy();

        // 적 생성
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // 다음 스폰 예약
        StartSpawning();
    }
    
    public void StopSpawning()
    {
        isSpawning = false; // 스폰 중단
        CancelInvoke(nameof(SpawnEnemy)); // 예약된 Invoke 중단
    }

    private GameObject ChooseRandomEnemy()
    {
        // 랜덤 값 생성
        int randomValue = Random.Range(0, totalWeight);

        // 가중치 범위에 따라 적 선택
        int cumulativeWeight = 0;
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            cumulativeWeight += spawnWeights[i];
            if (randomValue < cumulativeWeight)
            {
                return enemyPrefabs[i];
            }
        }

        return enemyPrefabs[0]; // 기본값 (안전 장치)
    }
}
