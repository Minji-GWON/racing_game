using System.Collections;
using UnityEngine;

public class SpecialEnemy : Enemy
{
    public float detectionRange = 5f; // 플레이어 감지 거리
    private Transform playerTransform; // 플레이어의 Transform
    private bool hasMoved = false; // X축으로 한 번만 이동하도록 제어하는 플래그

    private void Start()
    {
        // 태그로 플레이어 찾기
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    private void Update()
    {
        base.Update(); // 기본 Enemy 동작 호출

        // 플레이어가 감지 범위 안에 있고, 아직 이동하지 않았다면
        if (playerTransform && Vector2.Distance(transform.position, playerTransform.position) <= detectionRange && !hasMoved)
        {
            FollowPlayerX();
            hasMoved = true; // 이동 플래그 설정
        }
    }

    private void FollowPlayerX()
    {
        // 플레이어의 X 좌표와 적의 X 좌표 비교
        float direction = playerTransform.position.x > transform.position.x ? 1 : -1; // 이동 방향 결정
        transform.Translate(Vector3.right * (direction * 0.25f)); // X축으로 1만큼 이동
    }
}