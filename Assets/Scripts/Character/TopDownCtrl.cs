using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCtrl : MonoBehaviour
{
    Rigidbody2D myRigidbody = null;
    Animator myAnimator = null;
    SpriteRenderer mySpriteRenderer = null;
    SpriteRenderer myChildSpriteRenderer = null;

    public float speed = 1f;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myChildSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Idle
        if (horizontal == 0f && vertical == 0f)
		{
            mySpriteRenderer.flipX = false;
            myChildSpriteRenderer.flipX = false;
            myAnimator.Play("Idle");
		}
        else if (vertical <= -1f)
		{
            mySpriteRenderer.flipX = false;
            myChildSpriteRenderer.flipX = false;
            myAnimator.Play("Down");
        }
        else if (vertical >= 1f)
		{
            mySpriteRenderer.flipX = false;
            myChildSpriteRenderer.flipX = false;
            myAnimator.Play("Up");
        }
        else if (horizontal <= -1f)
		{
            myAnimator.Play("Horizontal");
            mySpriteRenderer.flipX = true;
            myChildSpriteRenderer.flipX = true;
        }
        else if (horizontal >= 1f)
		{
            myAnimator.Play("Horizontal");
            mySpriteRenderer.flipX = false;
            myChildSpriteRenderer.flipX = false;
        }

        myRigidbody.velocity = new Vector2(horizontal, vertical).normalized * speed;
    }
}
