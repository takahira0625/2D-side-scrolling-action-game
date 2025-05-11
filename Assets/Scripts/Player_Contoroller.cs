using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public float speed = 5f;   // 横に移動する速度
    public float jumpP = 500f; // ジャンプ力
    private bool isGround = true;
    [SerializeField] private float fallThreshold = -10f;
    // オブジェクト・コンポーネント参照
    private Rigidbody2D rigidbody2D; // Rigidbody2Dコンポーネントへの参照

    // 移動関連変数
    private float xSpeed; // X方向移動速度
    [SerializeField] private Animator animator;
    private bool isMoving = false;
    private bool isJumping = false;
    private bool isFalling = false;
    

    // Start is called before the first frame update
    void Start()
    {
        // リジッドボディ2Dをコンポーネントから取得して変数に入れる
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            SceneManager.LoadScene("GameoverScene");
        }
        MoveUpdate();
        // ジャンプをするためのコード（もしスペースキーが押されて、上方向に速度がない時に）
        /*if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            // リジッドボディに力を加える（上方向にジャンプ力をかける）
            rigidbody2D.AddForce(transform.up * jumpP);
            isGround = false;
            isJumping = true;
        }*/
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsFalling", isFalling);
    }

    private void FixedUpdate()
    {
      
        // 移動速度ベクトルを現在値から取得
        Vector2 velocity = rigidbody2D.velocity;
        // X方向の速度を入力から決定
        velocity.x = xSpeed;

        // 計算した移動速度ベクトルをRigidbody2Dに反映
        rigidbody2D.velocity = velocity;
        isFalling = rigidbody2D.velocity.y < -0.5f;
    }



    private void MoveUpdate()
    {
        // X方向移動入力
        if (Input.GetKey(KeyCode.D))
        {// 右方向の移動入力
         // X方向移動速度をプラスに設定
            xSpeed = 6.0f;
            isMoving = true;
            transform.localScale = new Vector3(1, 1, 1); // 元のスケールに戻す
        }
        else if (Input.GetKey(KeyCode.A))
        {// 左方向の移動入力
         // X方向移動速度をマイナスに設定
            xSpeed = -6.0f;
            isMoving = true;
            transform.localScale = new Vector3(-1, 1, 1); // X軸を反転
        }
        else
        {// 入力なし
         // X方向の移動を停止
            xSpeed = 0.0f;
            isMoving = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGround = true;
            Debug.Log("地面");
            isJumping = false;
        }

        if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            SceneManager.LoadScene("GameoverScene");
        }

        if (collision.gameObject.tag == "goal")
        {
            SceneManager.LoadScene("ClearScene");
        }
    }

}