using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float minX = -8.5f; // X축 최소값
    public float maxX = 0.92f; // X축 최대값

    private float screenMiddle; // 화면 중앙값

    void Start()
    {
        // 화면 중앙값 미리 계산
        screenMiddle = Screen.width * 0.5f;
    }

    void Update()
    {
        // 화면 입력 처리
        if (Input.GetMouseButton(0)) // 마우스 왼쪽 버튼 또는 터치 입력
        {
            Vector3 touchPosition = Input.mousePosition; // 입력된 화면 위치

            if (touchPosition.x > screenMiddle)
            {
                // 오른쪽 입력: Player를 오른쪽으로 이동
                Move(Vector3.right);
            }
            else if (touchPosition.x < screenMiddle)
            {
                // 왼쪽 입력: Player를 왼쪽으로 이동
                Move(Vector3.left);
            }
        }
    }

    void Move(Vector3 direction)
    {
        // Player 이동 처리
        transform.Translate(direction * (moveSpeed * Time.deltaTime));

        // X축 이동 범위 제한
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}