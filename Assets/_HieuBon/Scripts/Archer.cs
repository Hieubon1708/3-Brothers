using UnityEngine;

public class Archer : Enemy
{
    public GameObject preArrow;

    int amountArrow = 3;
    int indexArrow;

    Arrow[] arrows;

    Transform hand;

    public override void Awake()
    {
        base.Awake();

        hand = animator.GetBoneTransform(HumanBodyBones.LeftHand);
    }

    public override void Start()
    {
        base.Start();

        arrows = new Arrow[amountArrow];

        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i] = Instantiate(preArrow, LevelController.instance.transform).GetComponent<Arrow>();
            arrows[i].gameObject.SetActive(false);
        }
    }

    public void StartAttack()
    {
        Vector3 dir = (PlayerController.instance.transform.position - transform.position).normalized;
        
        arrows[indexArrow].Shot(hand.position, dir, 15f);
        indexArrow++;
        Debug.DrawRay(hand.position, dir * 10, Color.red, 100);

        if(indexArrow == amountArrow) indexArrow = 0;
    }
}
