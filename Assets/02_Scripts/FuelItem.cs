using UnityEngine;

public class FuelItem : MonoBehaviour
{
    public float fuelAmount = 20f; // 플레이어가 획득 시 증가할 연료량
    public float moveSpeed = 3f; // 아이템이 아래로 이동하는 속도
    public float destroyY = -5f; // 아이템이 사라지는 Y축 위치

    private void Update()
    {
        // 아래로 이동
        transform.Translate(Vector3.down * (moveSpeed * Time.deltaTime));

        // 특정 Y축 위치에 도달하면 아이템 제거
        if (transform.position.y <= destroyY)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.IncreaseFuel(fuelAmount);
                Destroy(gameObject); // 연료 아이템 제거
            }
        }
    }
}