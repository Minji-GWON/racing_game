using System.Collections;
using UnityEngine;

public class SpecialEnemy : Enemy
{
    public float detectionRange = 5f; // 플레이어 감지 거리
    public float moveDuration = 0.5f; // 이동 시간
    private Transform playerTransform; // 플레이어의 Transform
    private bool isMoving = false; // 이동 중 여부 확인 플래그
    private Vector3 targetPosition; // 이동 목표 위치

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

        // 플레이어가 감지 범위 안에 있고 이동 중이 아니면
        if (playerTransform && Vector2.Distance(transform.position, playerTransform.position) <= detectionRange && !isMoving)
        {
            // 이동 목표 위치 설정
            float direction = playerTransform.position.x > transform.position.x ? 1f : -1f;
            targetPosition = new Vector3(transform.position.x + direction, transform.position.y, transform.position.z);

            StartCoroutine(MoveToTarget());
        }
    }
    private IEnumerator MoveToTarget()
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            // MoveTowards로 직선 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveDuration * Time.deltaTime);
            yield return null;
        }

        // 정확히 목표 위치에 도달
        transform.position = targetPosition;

        isMoving = false;
    }
}