using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    Animator animator;

    public int hp = 100;
    public float speed;
    public float attackRange;

    bool isPlayerDetected;
    bool isAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        navMeshAgent.angularSpeed = 0;
        navMeshAgent.acceleration = 15;
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
        else
        {
            animator.SetTrigger("Damage");
        }
    }

    private void Update()
    {
        if (hp <= 0 || PlayerIndex.instance.hp <= 0) return;

        Vector3 ePos = transform.position;
        Vector3 pPos = PlayerController.instance.transform.position;

        ePos.y += 1;
        pPos.y += 1;

        Vector3 dir = (pPos - ePos).normalized;

        if (!isPlayerDetected)
        {
            RaycastHit hit;

            Physics.Raycast(ePos, dir, out hit, LevelController.instance.enemyVision);

            if (hit.collider != null && hit.collider.name.Contains("Player"))
            {
                float dot = Vector3.Dot(transform.forward, dir);

                if (dot > 0)
                {
                    isPlayerDetected = true;
                    animator.SetBool("Run", true);
                }
            }
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10);

            if (isAttack) return;

            float distance = Vector3.Distance(ePos, pPos);

            animator.SetBool("Run", distance > attackRange);

            if (distance <= attackRange)
            {
                isAttack = true;

                navMeshAgent.isStopped = true;
                animator.SetTrigger("Attack");
            }
            else
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(pPos);
            }
        }
    }

    public void EndAttack()
    {
        isAttack = false;
    }

    public void Damage()
    {
        Vector3 ePos = transform.position;
        Vector3 pPos = PlayerController.instance.transform.position;

        float distance = Vector3.Distance(ePos, pPos);
        
        if (distance <= attackRange)
        {
            PlayerIndex.instance.SubtractHealth(110);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        pos.y = 1f;

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
