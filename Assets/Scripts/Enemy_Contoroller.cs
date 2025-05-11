using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Controller : MonoBehaviour
{
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("弾のプレハブ")] public GameObject bulletPrefab;
    [Header("弾の発射位置")] public Transform firePoint;
    [Header("射撃間隔")] public float fireRate = 2f;
    [Header("押しつぶし判定の速度しきい値")] public float crushThreshold = 2f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;
    private bool rightTleftF = false;
    private float nextFireTime = 0f;
    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance < 5f) // 視認範囲
            {
                rb.velocity = Vector2.zero; 
                

                if (Time.time > nextFireTime)
                {
                    StartCoroutine(Shoot());
                    nextFireTime = Time.time + fireRate;
                }
            }
            else
            {
                
                Move();
            }
        }
    }

    void Move()
    {
        int xVector = rightTleftF ? 1 : -1;
        transform.localScale = new Vector3(rightTleftF ? -2 : 2, 2, 2);
        rb.velocity = new Vector2(xVector * speed, -gravity);
    }

    private IEnumerator Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            animator.SetBool("IsAttack", true); 
            yield return new WaitForSeconds(1);
            animator.SetBool("IsAttack", false); 
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null && player != null)
            {
                Vector2 direction = (player.position - firePoint.position).normalized;
                bulletRb.velocity = direction * 3f;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mace")
        {
            // 衝突した地点が敵の上かどうかを確認
            if (collision.contacts[0].point.y > transform.position.y)
            {
                // 一定以上の落下速度がある場合のみ押しつぶし
                if (collision.relativeVelocity.y < -crushThreshold)
                {
                    Debug.Log("Maceが敵を上から押しつぶした！");
                    Destroy(gameObject);
                }
            }

        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TurnPoint"))
        {
            rightTleftF = !rightTleftF;
        }
    }

}

