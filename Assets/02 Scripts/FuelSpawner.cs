using UnityEngine;

public class FuelSpawner : MonoBehaviour
{
    public GameObject fuelPrefab; // 연료 아이템 프리팹
    public float spawnInterval = 10f; // 연료 생성 간격
    public float spawnY = 5f; // Y축 스폰 위치

    private float[] spawnPositionsX = { -0.8f, 0.92f }; // 고정된 X축 위치 배열

    private void Start()
    {
        // 일정 간격마다 연료 아이템 스폰
        InvokeRepeating(nameof(SpawnFuel), spawnInterval, spawnInterval);
    }

    private void SpawnFuel()
    {
        // X축 위치를 고정된 값 중 하나로 랜덤 선택
        float spawnX = spawnPositionsX[Random.Range(0, spawnPositionsX.Length)];
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0); // 스폰 위치 계산
        Instantiate(fuelPrefab, spawnPosition, Quaternion.identity); // 연료 아이템 생성
    }
}