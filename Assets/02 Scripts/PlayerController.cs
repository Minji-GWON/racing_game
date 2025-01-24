using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float minX = -8.5f; // X축 최소값
    public float maxX = 0.92f; // X축 최대값
    private Rigidbody2D _rb;

    public float fuel = 100f; // 현재 연료값
    public float maxFuel = 100f; // 최대 연료값
    public TextMeshProUGUI fuelText; // UI 텍스트
    public Vector3 initialPosition; // 초기 위치

    public float fuelDecreaseInterval = 2f; // 연료 감소 간격 (초)
    public float fuelDecreaseAmount = 10f; // 감소할 연료량
    
    public float hp = 100f; // 차량 HP
    public float maxHp = 100f; // 최대 HP
    public TextMeshProUGUI hpText; // UI TextMeshPro
    
    public ParticleSystem collisionEffect; // 충돌 시 파티클 효과
    public GameManager gameManager; // 게임 매니저 참조 (게임 오버 처리)

    private bool isDestroyed = false; // 파괴 상태 플래그
    public GameObject playerModel; // 플레이어 차량의 모델 (비활성화용)
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        // Rigidbody2D 설정
        if (_rb != null)
        {
            _rb.gravityScale = 0; // 중력 영향 제거
            _rb.constraints = RigidbodyConstraints2D.FreezePosition; // 위치 고정
        }
        // 초기 상태 설정
        initialPosition = transform.position;
        ResetState();
        
        // 일정 간격마다 연료 감소
        InvokeRepeating(nameof(DecreaseFuelOverTime), fuelDecreaseInterval, fuelDecreaseInterval);
        
    }
    public void ResetState()
    {
        // HP 및 연료, 위치 초기화
        hp = maxHp;
        fuel = maxFuel;
        transform.position = initialPosition;
        
        if (playerModel != null)
        {
            playerModel.SetActive(true);
        }

        // UI 업데이트
        UpdateHpUI();
        UpdateFuelUI();
    }

    private void Update()
    {
        HandleInput(); // 플레이어 입력 처리
        UpdateFuelUI(); // 연료 UI 업데이트
    }
    
    public void TakeDamage(float damage)
    {
        hp = Mathf.Clamp(hp - damage, 0, maxHp); // HP 감소
        UpdateHpUI();

        if (hp <= 0)
        {
            isDestroyed = true; // 파괴 상태 플래그 설정
            
            // 파티클 효과 재생
            if (collisionEffect != null)
            {
                Instantiate(collisionEffect, transform.position, Quaternion.identity);
            }
            
            // 플레이어 모델 비활성화
            if (playerModel != null)
            {
                playerModel.SetActive(false);
            }
            
            // 5초 후에 게임 종료 호출
            Invoke(nameof(DelayedGameOver), 5f);
        }
    }
    private void DelayedGameOver()
    {
        if (gameManager != null)
        {
            gameManager.EndGame();
        }
    }
    private void UpdateHpUI()
    {
        // TextMeshPro에 HP 표시
        hpText.text = $"HP: {Mathf.FloorToInt(hp)}";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemyDamage = collision.gameObject.GetComponent<Enemy>();
            
            if (enemyDamage != null)
            {
                float damage = enemyDamage.damage;
                TakeDamage(damage);
            }

            // 파티클 효과 재생
            if (collisionEffect != null)
            {
                Instantiate(collisionEffect, transform.position, Quaternion.identity);
            }
        }
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