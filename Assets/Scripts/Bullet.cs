using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f; // �e�̑��x
    public float lifetime = 5f; // �e�̎���
    private Rigidbody2D rb;
    private bool isReflected = false; // ���˂��ꂽ���ǂ����𔻒肷��t���O

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime); // ��莞�Ԍ�ɒe���폜
    }

    void OnMouseDown()
    {
        ReflectBullet(); // �N���b�N�Œe�𔽎�
    }

    void ReflectBullet()
    {
        if (rb != null)
        {
            rb.velocity = -rb.velocity; // ���x�̌����𔽓]
            isReflected = true; // ���˃t���O��L���ɂ���
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") && isReflected)
        {
            Destroy(collision.gameObject); // ���˂��ꂽ�e���G�ɓ��������ꍇ�̂ݓG��j��
            Destroy(gameObject); // �e������
        }
    }
}
