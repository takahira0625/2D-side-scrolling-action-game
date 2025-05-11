using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public float speed = 5f;   // ���Ɉړ����鑬�x
    public float jumpP = 500f; // �W�����v��
    private bool isGround = true;
    [SerializeField] private float fallThreshold = -10f;
    // �I�u�W�F�N�g�E�R���|�[�l���g�Q��
    private Rigidbody2D rigidbody2D; // Rigidbody2D�R���|�[�l���g�ւ̎Q��

    // �ړ��֘A�ϐ�
    private float xSpeed; // X�����ړ����x
    [SerializeField] private Animator animator;
    private bool isMoving = false;
    private bool isJumping = false;
    private bool isFalling = false;
    

    // Start is called before the first frame update
    void Start()
    {
        // ���W�b�h�{�f�B2D���R���|�[�l���g����擾���ĕϐ��ɓ����
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
        // �W�����v�����邽�߂̃R�[�h�i�����X�y�[�X�L�[��������āA������ɑ��x���Ȃ����Ɂj
        /*if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            // ���W�b�h�{�f�B�ɗ͂�������i������ɃW�����v�͂�������j
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
      
        // �ړ����x�x�N�g�������ݒl����擾
        Vector2 velocity = rigidbody2D.velocity;
        // X�����̑��x����͂��猈��
        velocity.x = xSpeed;

        // �v�Z�����ړ����x�x�N�g����Rigidbody2D�ɔ��f
        rigidbody2D.velocity = velocity;
        isFalling = rigidbody2D.velocity.y < -0.5f;
    }



    private void MoveUpdate()
    {
        // X�����ړ�����
        if (Input.GetKey(KeyCode.D))
        {// �E�����̈ړ�����
         // X�����ړ����x���v���X�ɐݒ�
            xSpeed = 6.0f;
            isMoving = true;
            transform.localScale = new Vector3(1, 1, 1); // ���̃X�P�[���ɖ߂�
        }
        else if (Input.GetKey(KeyCode.A))
        {// �������̈ړ�����
         // X�����ړ����x���}�C�i�X�ɐݒ�
            xSpeed = -6.0f;
            isMoving = true;
            transform.localScale = new Vector3(-1, 1, 1); // X���𔽓]
        }
        else
        {// ���͂Ȃ�
         // X�����̈ړ����~
            xSpeed = 0.0f;
            isMoving = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGround = true;
            Debug.Log("�n��");
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