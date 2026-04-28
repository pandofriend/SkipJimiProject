using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private Animator animator;
    private Move move;
    private Jump jump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     animator = GetComponent<Animator>();   
     move = GetComponent<Move>();
     jump = GetComponent<Jump>();
    }

    // Update is called once per frame
    void Update()
    { 
      if(!jump) return;
      if(!move) return;
      if(!animator) return;

    //Check moveHorizontal in PLayground Move
      if(move.moveHorizontal == 1.0f || move.moveHorizontal == -1.0f )
      {
      animator.SetBool("isWalking", true);
      } else if (move.moveHorizontal == 0.0f)
        {
        animator.SetBool("isWalking", false);
        }
     
       //Check jumping in Playground Jump
       animator.SetBool("isJumping", !jump.canJump);

    }
}
