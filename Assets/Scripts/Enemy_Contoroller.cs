using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Controller : MonoBehaviour
{
    [Header("�ړ����x")] public float speed;
    [Header("�d��")] public float gravity;
    [Header("�e�̃v���n�u")] public GameObject bulletPrefab;
    [Header("�e�̔��ˈʒu")] public Transform firePoint;
    [Header("�ˌ��Ԋu")] public float fireRate = 2f;
    [Header("�����Ԃ�����̑��x�������l")] public float crushThreshold = 2f;

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

            if (distance < 5f) // ���F�͈�
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
            // �Փ˂����n�_���G�̏ォ�ǂ������m�F
            if (collision.contacts[0].point.y > transform.position.y)
            {
                // ���ȏ�̗������x������ꍇ�̂݉����Ԃ�
                if (collision.relativeVelocity.y < -crushThreshold)
                {
                    Debug.Log("Mace���G���ォ�牟���Ԃ����I");
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

