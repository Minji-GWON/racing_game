using UnityEngine;

public class FuelSpawner : MonoBehaviour
{
    public GameObject fuelPrefab; // 연료 아이템 프리팹
    public float spawnInterval = 10f; // 연료 생성 간격
    public Vector2 spawnRangeX = new Vector2(-0.8f, 0.92f); // X축 스폰 범위
    public float spawnY = 5f; // Y축 스폰 위치

    private void Start()
    {
        // 일정 간격마다 연료 아이템 스폰
        InvokeRepeating(nameof(SpawnFuel), spawnInterval, spawnInterval);
    }

    private void SpawnFuel()
    {
        float spawnX = Random.Range(spawnRangeX.x, spawnRangeX.y); // 랜덤 X 위치
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0); // 스폰 위치 계산
        Instantiate(fuelPrefab, spawnPosition, Quaternion.identity); // 연료 아이템 생성
    }
}