using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 10f; // 적의 공격력
    [SerializeField] private float moveSpeed = 3f;
    public float pushForce = 1f; // 적이 옆으로 밀리는 힘
    public ParticleSystem destructionEffect; // 파괴 시 파티클 효과 

    public void Update()
    {
        MoveDown();
    }
    private void MoveDown()
    {
        transform.Translate(Vector3.down * (moveSpeed * Time.deltaTime));

        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 적 자동차가 충돌 시 옆으로 밀리도록 힘을 추가
            Vector2 pushDirection = collision.contacts[0].normal; // 충돌 방향
            Vector2 push = new Vector2(pushDirection.x * pushForce, 0); // X축으로만 힘 적용
            GetComponent<Rigidbody2D>().AddForce(push, ForceMode2D.Impulse);
            
            // 파괴 효과 재생
            if (destructionEffect != null)
            {
                Instantiate(destructionEffect, transform.position, Quaternion.identity);
            }

            // 적 자동차 파괴
            Destroy(gameObject);
        }
    }
    
}
