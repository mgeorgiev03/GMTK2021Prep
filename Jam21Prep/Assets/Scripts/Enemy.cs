using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;
    public Transform[] moveSpots;
    private int randomSpot;
    new private BoxCollider2D collider;
    public LayerMask PlayerLayerMask;
    public LayerMask BulletLayerMask;
    private bool HasSeenPlayer = false;
    public float attackCooldown = 4f;
    GameObject plr;
    Vector2 target;
    public float attackRange = 0.5f;
    public float HP = 2f;



    // Start is called before the first frame update
    void Start()
    {
        plr = GameObject.Find("Player");
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var scale = transform.localScale;
        if (transform.position.x - plr.transform.position.x < 0)
        {
            scale.x = Mathf.Abs(scale.x) * -1;

        }
        else if (transform.position.x - plr.transform.position.x > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        if (HP == 0)
            Destroy(this.gameObject);

        if (!HasSeenPlayer)
        {
            Patrol();
            Vision();
        }
        target = plr.transform.position;
        if (HasSeenPlayer)
            Attack();
    }

    void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= startWaitTime;
            }
        }
    }

    void Vision()
    {
        RaycastHit2D raycast = Physics2D.Raycast(collider.bounds.center, Vector2.left, collider.bounds.extents.y + 30f,
            PlayerLayerMask);

        if (raycast.collider != null)
            HasSeenPlayer = true;
    }

    void Attack()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        Collider2D hit = Physics2D.OverlapCircle(collider.bounds.center, attackRange, PlayerLayerMask);

        if (hit != null)
        {

            StartCoroutine(StartDealingDamage(hit));
        }
    }

    private IEnumerator StartDealingDamage(Collider2D hit)
    {
        if (attackCooldown <= 0f)
        {
            hit.GetComponent<PlayerMovement>().TakeDamage();
            attackCooldown = 1f;
        }
        else
        {
            attackCooldown -= Time.fixedDeltaTime;
        }
        yield return new WaitForSeconds(1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == BulletLayerMask)
        {
            TakeDamage();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == BulletLayerMask)
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        HP--;
    }
}
