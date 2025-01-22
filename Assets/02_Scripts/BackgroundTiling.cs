using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTiling : MonoBehaviour
{
    public float speed = 3f; // 이동 속도
    public float resetPositionY = 10f; // 초기 위치 (위로 이동)
    public float endPositionY = -10f; // 반복 위치 (아래로 이동)

    private Vector3 startPosition; // 초기 위치 저장

    private void Start()
    {
        // 현재 위치를 초기 위치로 설정
        startPosition = transform.position;
    }

    private void Update()
    {
        // 아래로 이동
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // 특정 위치에 도달하면 초기 위치로 이동
        if (transform.position.y <= endPositionY)
        {
            transform.position = new Vector3(transform.position.x, resetPositionY, transform.position.z);
        }
    }
}
