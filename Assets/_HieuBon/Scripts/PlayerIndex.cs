using UnityEngine;

public class PlayerIndex : MonoBehaviour
{
    public static PlayerIndex instance;

    public int hp;
    public float speed;

    Animator animator;

    private void Awake()
    {
        instance = this;

        animator = GetComponent<Animator>();
    }

    public void SubtractHealth(int damage)
    {
        if (hp <= 0) return;

        hp -= damage;

        if (hp <= 0)
        {
            animator.SetTrigger("Die");
            GetComponent<Collider>().enabled = false;
        }
        /*else
        {
            animator.SetTrigger("Damage");
        }*/
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("Die");
        }
    }
}
