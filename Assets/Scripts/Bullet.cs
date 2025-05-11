using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f; // ’e‚Ì‘¬“x
    public float lifetime = 5f; // ’e‚Ìõ–½
    private Rigidbody2D rb;
    private bool isReflected = false; // ”½Ë‚³‚ê‚½‚©‚Ç‚¤‚©‚ğ”»’è‚·‚éƒtƒ‰ƒO

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime); // ˆê’èŠÔŒã‚É’e‚ğíœ
    }

    void OnMouseDown()
    {
        ReflectBullet(); // ƒNƒŠƒbƒN‚Å’e‚ğ”½Ë
    }

    void ReflectBullet()
    {
        if (rb != null)
        {
            rb.velocity = -rb.velocity; // ‘¬“x‚ÌŒü‚«‚ğ”½“]
            isReflected = true; // ”½Ëƒtƒ‰ƒO‚ğ—LŒø‚É‚·‚é
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") && isReflected)
        {
            Destroy(collision.gameObject); // ”½Ë‚³‚ê‚½’e‚ª“G‚É“–‚½‚Á‚½ê‡‚Ì‚İ“G‚ğ”j‰ó
            Destroy(gameObject); // ’e‚àÁ‚·
        }
    }
}
