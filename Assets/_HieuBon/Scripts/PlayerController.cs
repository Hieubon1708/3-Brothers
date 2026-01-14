using UnityEngine;
using static GameController;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    public WeaponType weaponType;

    Animator animator;

    Collider[] colliders = new Collider[1];

    public LayerMask enemyLayer;

    bool isAttack;
    bool isDrag;

    float speed;
    float radius;

    CharacterIndex characterIndex;
    CharacterWeapon characterWeapon;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        characterIndex = GetComponent<CharacterIndex>();
        characterWeapon = GetComponent<CharacterWeapon>();
    }

    private void Start()
    {
        int weaponIndex = (int)weaponType;

        animator.SetInteger("WeaponIndex", weaponIndex);

        characterWeapon.WeaponSelect(weaponIndex, out radius);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrag = true;

            if (isAttack) animator.SetBool("Attack", false);

            isAttack = false;

            speed = 5f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
        }

        int amountEnemy = Physics.OverlapSphereNonAlloc(transform.position, radius, colliders, enemyLayer);

        Vector3 dir = UIController.instance.uIInGame.uIJoystick.Dir;

        float speedPercent = UIController.instance.uIInGame.uIJoystick.SpeedPercent;

        characterController.Move(dir * speed * speedPercent * characterIndex.speed * Time.deltaTime);

        if (isAttack) transform.rotation = Quaternion.LookRotation(colliders[0].transform.position - transform.position);
        else if (dir != Vector3.zero) transform.rotation = Quaternion.LookRotation(dir);

        animator.SetBool("Run", dir != Vector3.zero);
        animator.SetFloat("Speed", speedPercent * characterIndex.speed);

        if (!isDrag && amountEnemy > 0)
        {
            speed = 2.5f;

            if (!isAttack) animator.SetBool("Attack", true);

            isAttack = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
