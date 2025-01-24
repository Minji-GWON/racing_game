
// GameManager.cs
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // 싱글톤 인스턴스

    public GameObject homePanel; // Home Panel
    public GameObject gameUI; // 게임 중 표시될 UI 
    public GameObject replayPanel; // Replay Panel

    public BackgroundTiling[] backgrounds; //스크립트 배열 
    
    public PlayerController playerController; // PlayerController 참조

    private bool _isGameOver = false; // 게임 종료 상태 확인
    public EnemySpawner enemySpawner; // EnemySpawner 참조


    private void Awake()
    {
        // 싱글톤 인스턴스 초기화
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 게임 시작 시 초기화
        ShowHomePanel();
    }

    private void InitializeGame()
    {
        // 플레이어 연료 초기화
        if (playerController != null)
        {
            playerController.ResetFuel();
        }

        Debug.Log("게임 초기화 완료!");
    }

    public void StartGame()
    {
        // Home Panel 비활성화
        homePanel.SetActive(false);

        // 게임 UI 활성화 
        if (gameUI != null)
        {
            gameUI.SetActive(true);
        }

        // Replay Panel 비활성화
        replayPanel.SetActive(false);
        
        // Background 타일링 활성화
        foreach (var background in backgrounds)
        {
            background.SetTilingActive(true);
        }
        
        // 게임 초기화
        InitializeGame();
    }

    private void ShowHomePanel()
    {
        // Home Panel 활성화
        homePanel.SetActive(true);

        // 게임 UI 비활성화 
        if (gameUI != null)
        {
            gameUI.SetActive(false);
        }
        // 게임 상태 초기화
        _isGameOver = false;
    }

    public void ReplayGame()
    {
        // Replay Panel 비활성화
        replayPanel.SetActive(false);

        // 게임 UI 활성화 (선택 사항)
        if (gameUI != null)
        {
            gameUI.SetActive(true);
        }

        // 게임 초기화
        InitializeGame();
    }

    public void EndGame()
    {
        // Replay Panel 활성화
        replayPanel.SetActive(true);

        // 게임 UI 비활성화
        if (gameUI)
        {
            gameUI.SetActive(false);
        }
        
        // 적 스폰 중단
        if (enemySpawner)
        {
            enemySpawner.StopSpawning();
        }
        
        // Background 타일링 비활성화
        foreach (var background in backgrounds)
        {
            background.SetTilingActive(false);
        }

        Debug.Log("게임 종료!");
    }

    public void ExitGame()
    {
        // 게임 종료
        Debug.Log("종료!");
        Application.Quit();
    }
}