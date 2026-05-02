using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private Animator animator;
    private Move move;
    private Jump jump;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     sr = GetComponent<SpriteRenderer>();
     rb = GetComponent<Rigidbody2D>();
     animator = GetComponent<Animator>();   
     move = GetComponent<Move>();
     jump = GetComponent<Jump>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!jump) return;
        if (!move) return;
        if (!animator) return;

        //Check moveHorizontal in PLayground Move
        if (move.moveHorizontal > 0.0f || move.moveHorizontal < -0.0f)
        {
            animator.SetBool("isWalking", true);
        }
        else if (move.moveHorizontal == 0.0f)
        {
            animator.SetBool("isWalking", false);
        }
         if (move.moveHorizontal > 0.01f)
        {
            sr.flipX = false;
        }
         else if (move.moveHorizontal < -0.01f)
        {
            sr.flipX = true;
        }
        if (!jump.canJump)
        {
            if (rb.linearVelocityY > 0.1f)
            {
                animator.SetBool("isFalling", false);
                animator.SetBool("isRising", true);
            }
            else if (rb.linearVelocityY < 0.1f)
            {
                animator.SetBool("isFalling", true);
                animator.SetBool("isRising", false);
            }
        } else
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isRising", false);
        }
            //Check jumping in Playground Jump
            animator.SetBool("isJumping", !jump.canJump);

        
    }
    }
