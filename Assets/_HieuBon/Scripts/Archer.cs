using UnityEngine;

public class Archer : Enemy
{
    public GameObject preArrow;

    int amountArrow = 3;

    Arrow[] arrows;

    public override void Start()
    {
        base.Start();

        arrows = new Arrow[amountArrow];

        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i] = Instantiate(preArrow, LevelController.instance.transform).GetComponent<Arrow>();
        }
    }

    public void StartAttack()
    {

    }
}
