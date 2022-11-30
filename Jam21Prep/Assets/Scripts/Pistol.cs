using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public Transform player;
    new Rigidbody2D rigidbody;
    private Vector2 mousePos;
    new public Camera camera;
    new SpriteRenderer renderer;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        if (rigidbody.rotation >= 90 || rigidbody.rotation <= -90)
        {
            renderer.flipY = true;
        }
        else
        {
            renderer.flipY = false;
        }
    }

    private void FixedUpdate()
    {
        //rigidbody.MovePosition(new Vector2(player.position.x, player.position.y));
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        rigidbody.rotation = angle;
    }
}
