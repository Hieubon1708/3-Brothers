using UnityEngine;
using UnityEngine.AI;
using static GameController;

public abstract class Enemy : MonoBehaviour
{
    public EnemyType type;
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public EnemyIndexData enemyIndexData;

    bool isPlayerDetected;
    bool isAttack;

    public virtual void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.angularSpeed = 0;
        navMeshAgent.acceleration = 15;
    }

    public virtual void Start()
    {
        enemyIndexData = new EnemyIndexData(GameController.instance.GetEnemyIndexData(type));
        
        navMeshAgent.speed = enemyIndexData.speed;
        animator.SetFloat("AttackSpeed", enemyIndexData.attackSpeed);
    }

    public void SubtractHealth(int damage)
    {
        if (enemyIndexData.hp <= 0) return;

        enemyIndexData.hp -= damage;

        if (enemyIndexData.hp <= 0)
        {
            navMeshAgent.enabled = false;
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
        if (enemyIndexData.hp <= 0 || LevelController.instance.gameState == GameState.Pause) return;

        Vector3 ePos = transform.position;
        Vector3 pPos = PlayerController.instance.transform.position;

        ePos.y += 1;
        pPos.y += 1;

        Vector3 dir = (pPos - ePos).normalized;

        if (!isPlayerDetected)
        {
            RaycastHit hit;

            Physics.Raycast(ePos, dir, out hit, GameController.instance.enemyVision);
            
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

            animator.SetBool("Run", distance > enemyIndexData.attackRange);

            if (distance <= enemyIndexData.attackRange)
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

        if (distance <= enemyIndexData.attackRange)
        {
            PlayerIndex.instance.SubtractHealth(1);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;

        pos.y = 1f;

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(pos, enemyIndexData.attackRange);
    }
}
