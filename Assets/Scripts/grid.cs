using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour
{
    private Vector3 offset;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        Vector3 targetPosition = GetMouseWorldPos() + offset;
        rb.MovePosition(targetPosition); // Rigidbodyを使って移動（物理エンジンの影響を受ける）
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Vector3.Distance(Camera.main.transform.position, transform.position);
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}

