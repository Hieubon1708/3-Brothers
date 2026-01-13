using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    float speed;

    Animator animator;

    Collider[] colliders = new Collider[1];

    public float radius;

    public LayerMask enemyLayer;

    [HideInInspector]
    public bool isLookAtEnemy;

    bool isAttack;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        int amountEnemy = Physics.OverlapSphereNonAlloc(transform.position, radius, colliders, enemyLayer);

        isLookAtEnemy = amountEnemy > 0;

        float speed = isLookAtEnemy ? 2.5f : 5f;

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(x, 0, z);

        characterController.Move(dir * speed * Time.deltaTime);

        if (amountEnemy > 0) transform.rotation = Quaternion.LookRotation(colliders[0].transform.position - transform.position);
        else if (dir != Vector3.zero) transform.rotation = Quaternion.LookRotation(dir);

        animator.SetBool("Run", dir != Vector3.zero);

        if (amountEnemy > 0)
        {
            if (!isAttack) animator.SetBool("Hit", true);

            isAttack = true;
        }
        else
        {
            if (isAttack) animator.SetBool("Hit", false);

            isAttack = false;
        }
    }
}
