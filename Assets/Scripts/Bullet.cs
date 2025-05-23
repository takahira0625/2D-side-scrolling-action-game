using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f; // 弾の速度
    public float lifetime = 5f; // 弾の寿命
    private Rigidbody2D rb;
    private bool isReflected = false; // 反射されたかどうかを判定するフラグ

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime); // 一定時間後に弾を削除
    }

    void OnMouseDown()
    {
        ReflectBullet(); // クリックで弾を反射
    }

    void ReflectBullet()
    {
        if (rb != null)
        {
            rb.velocity = -rb.velocity; // 速度の向きを反転
            isReflected = true; // 反射フラグを有効にする
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") && isReflected)
        {
            Destroy(collision.gameObject); // 反射された弾が敵に当たった場合のみ敵を破壊
            Destroy(gameObject); // 弾も消す
        }
    }
}
