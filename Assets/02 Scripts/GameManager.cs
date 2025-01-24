
// GameManager.cs
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // 싱글톤 인스턴스

    public GameObject homePanel; // Home Panel
    public GameObject gameUI; // 게임 중 표시될 UI 
    public GameObject replayPanel; // Replay Panel

    public BackgroundTiling[] backgrounds; //스크립트 배열 
    
    public PlayerController playerController; // PlayerController 참조

    private bool _isGameOver = false; // 게임 종료 상태 확인
    public Spawner spawner; // Spawner 참조


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
        // 플레이어 상태 초기화
        if (playerController != null)
        {
            playerController.ResetState();
        }
        
        if (spawner != null)
        {
            spawner.InitializeSpawner();
        }
        
        // 게임 UI 활성화 
        if (gameUI != null)
        {
            gameUI.SetActive(true);
        }
        // Background 타일링 활성화
        foreach (var background in backgrounds)
        {
            background.SetTilingActive(true);
        }
    }

    public void StartGame()
    {
        // Home Panel 비활성화
        homePanel.SetActive(false);

        // Replay Panel 비활성화
        replayPanel.SetActive(false);
        
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

        InitializeGame();
    }

    public void EndGame()
    {
        // 게임 UI 비활성화
        if (gameUI)
        {
            gameUI.SetActive(false);
        }
        
        // 적 스폰 중단
        if (spawner)
        {
            spawner.StopSpawning();
        }
        
        // Background 타일링 비활성화
        foreach (var background in backgrounds)
        {
            background.SetTilingActive(false);
        }
        // Replay Panel 활성화
        replayPanel.SetActive(true);
    }

    public void ExitGame()
    {
        // 게임 종료
        Application.Quit();
    }
}