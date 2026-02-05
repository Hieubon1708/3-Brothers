using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static GameController;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public WeaponType weaponType;
    NavMeshAgent navMeshAgent;

    Animator animator;

    [HideInInspector]
    public Collider[] colliders = new Collider[1];

    public LayerMask enemyLayer;

    bool isAttack;
    bool isDrag;

    public float speed;
    [HideInInspector]
    public float radius;

    PlayerWeapon characterWeapon;

    private void Awake()
    {
        instance = this;

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        characterWeapon = GetComponent<PlayerWeapon>();

        if (weaponType == WeaponType.Knife) transform.AddComponent<KnifeEvent>();
    }

    private void Start()
    {
        int weaponIndex = (int)weaponType;

        characterWeapon.WeaponSelect(weaponIndex, out radius);
    }

    void Update()
    {
        if (PlayerIndex.instance.hp <= 0 || LevelController.instance.gameState == GameState.Pause) return;

        if (Input.GetMouseButtonDown(0))
        {
            isDrag = true;

            animator.SetBool("ImmidiateExit", true);
            if (isAttack) animator.SetBool("Attack", false);
            isAttack = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
        }

        Vector3 pos = transform.position;
        pos.y = 1f;

        int amountEnemy = Physics.OverlapSphereNonAlloc(pos, radius, colliders, enemyLayer);

        Vector3 dir = UIController.instance.uIInGame.uIJoystick.Dir;

        float speedPercent = UIController.instance.uIInGame.uIJoystick.SpeedPercent;
        
        navMeshAgent.Move(dir * speed * speedPercent * PlayerIndex.instance.speed * Time.deltaTime);
        
        if (isAttack) transform.rotation = Quaternion.LookRotation(colliders[0].transform.position - transform.position);
        else if (dir != Vector3.zero) transform.rotation = Quaternion.LookRotation(dir);

        animator.SetBool("Run", dir != Vector3.zero);
        animator.SetFloat("Speed", speedPercent * PlayerIndex.instance.speed);

        if (!isDrag && amountEnemy > 0 && !isAttack)
        {
            animator.SetBool("Attack", true);
            isAttack = true;
        }
        else if (isAttack && amountEnemy == 0)
        {
            animator.SetBool("ImmidiateExit", false);
            animator.SetBool("Attack", false);
            isAttack = false;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        pos.y = 1f;

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(pos, radius);
    }
}
