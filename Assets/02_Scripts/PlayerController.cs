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
        UpdateFuelUI(); // 초기 연료값 표시
        
    }

    private void Update()
    {
        HandleInput(); // 플레이어 입력 처리
        UpdateFuelUI(); // 연료 UI 업데이트
    }

    private void HandleInput()
    {
        if (fuel <= 0) 
        {
            GameManager.Instance.EndGame(); // 연료가 없으면 입력 무시
        }
        
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
            // 연료가 0이 되면 게임 종료 호출
            GameManager.Instance.EndGame();
        }
    }
    
    public void ResetFuel()
    {
        // 연료값 초기화
        fuel = maxFuel;
        UpdateFuelUI();
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