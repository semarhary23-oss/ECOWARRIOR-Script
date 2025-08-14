using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayWalk()
    {
        anim.SetBool("isWalking", true);
    }

    public void StopWalk()
    {
        anim.SetBool("isWalking", false);
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public void Die()
    {
        anim.SetBool("isDead", true);
    }
}
