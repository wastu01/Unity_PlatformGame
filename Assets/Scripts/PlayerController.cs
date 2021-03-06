﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    private Animator anim;
    public Collider2D coll;

    public float speed;

    public float jumpforce;

    public LayerMask Ground;
    public int cherry;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }
    void FixedUpdate()
    {

        Moventment();
        SwitchAnimate();
    }
    void Moventment()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");

        //角色移動

        rb.velocity = new Vector2(horizontalmove * speed * Time.deltaTime, rb.velocity.y);
        anim.SetFloat("running", Mathf.Abs(facedirection));

        if (facedirection != 0)

        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

        //角色跳躍
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers("Ground"))

        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
            anim.SetBool("jumping", true);
        }


    }

    void SwitchAnimate()
    {
        anim.SetBool("idle", false);
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);

            }


        }
        else if (coll.IsTouchingLayers(Ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);

        }

    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Collection")
        {
            cherry += 1;
            Debug.Log(cherry);

            Destroy(other.gameObject);

        }
    }


}
