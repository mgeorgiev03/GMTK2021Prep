using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    new BoxCollider2D collider;
    new Rigidbody2D rigidbody;
    private bool flag = true;
    public int HP = 6;

    private Vector2 move;
    [SerializeField] public float moveSpeed = 6;


    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        if (move.x != 0 || move.y != 0)
        {
            if (flag)
                StartCoroutine(Walking());
        }
        

        animator.SetFloat("Horizontal", move.x);
        animator.SetFloat("Vertical", move.y);
        animator.SetFloat("Speed", Mathf.Abs(move.sqrMagnitude));

        if (HP == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + move * moveSpeed * Time.fixedDeltaTime);
    }

    public IEnumerator Walking()
    {
        FindObjectOfType<AudioManager>().Play("Walking");
        flag = false;
        yield return new WaitForSeconds(0.4f);
        flag = true;
    }

    public void TakeDamage()
    {
        HP--;
    }
}
