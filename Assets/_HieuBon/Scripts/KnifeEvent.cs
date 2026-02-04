using UnityEngine;

public class KnifeEvent : MonoBehaviour
{
    public void Attack()
    {
        Vector3 pos = transform.position;
        pos.y = 1f;

        int amountEnemy = Physics.OverlapSphereNonAlloc(pos, PlayerController.instance.radius, PlayerController.instance.colliders, PlayerController.instance.enemyLayer);

        if (amountEnemy > 0)
        {
            GameObject e = PlayerController.instance.colliders[0].gameObject;

            Enemy enemy = LevelController.instance.GetEnemy(e);

            if (enemy != null) enemy.SubtractHealth(10);
        }
    }
}
