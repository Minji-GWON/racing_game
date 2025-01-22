using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float minX = -8.5f; // X축 최소값
    public float maxX = 0.92f; // X축 최대값

    public float fuel = 100f; // 현재 연료값
    public float maxFuel = 100f; // 최대 연료값
    public TextMeshProUGUI fuelText; // UI 텍스트

    public float fuelDecreaseInterval = 2f; // 연료 감소 간격 (초)
    public float fuelDecreaseAmount = 10f; // 감소할 연료량

    private void Start()
    {
        // 일정 간격마다 연료 감소
        InvokeRepeating(nameof(DecreaseFuelOverTime), fuelDecreaseInterval, fuelDecreaseInterval);
    }

    private void Update()
    {
        HandleInput(); // 플레이어 입력 처리
        UpdateFuelUI(); // 연료 UI 업데이트
    }

    private void HandleInput()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 touchPosition = Input.mousePosition;
            float screenMiddle = Screen.width * 0.5f;

            if (touchPosition.x > screenMiddle)
            {
                Move(Vector3.right);
            }
            else if (touchPosition.x < screenMiddle)
            {
                Move(Vector3.left);
            }
        }
    }

    private void Move(Vector3 direction)
    {
        transform.Translate(direction * (moveSpeed * Time.deltaTime));

        // X축 이동 범위 제한
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    private void DecreaseFuelOverTime()
    {
        // 연료 감소
        fuel = Mathf.Clamp(fuel - fuelDecreaseAmount, 0, maxFuel);
        if (fuel <= 0)
        {
            Debug.Log("연료가 모두 소진되었습니다!");
            // 추가 동작(게임 오버 등)을 구현할 수 있습니다.
        }
    }

    public void IncreaseFuel(float amount)
    {
        fuel = Mathf.Clamp(fuel + amount, 0, maxFuel);
    }

    private void UpdateFuelUI()
    {
        // 연료값을 정수로 변환하여 UI에 업데이트
        fuelText.text = $"Fuel: {Mathf.FloorToInt(fuel)}";
    }
}
