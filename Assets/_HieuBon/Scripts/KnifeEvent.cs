using DG.Tweening;
using UnityEngine;

public class KnifeEvent : MonoBehaviour
{
    public void Attack()
    {
        if (!PlayerController.instance.isAttack) return;

        Transform e = PlayerController.instance.enemyTarget.transform;

        Enemy enemy = LevelController.instance.GetEnemy(e);

        if (enemy != null)
        {
            if (enemy.enemyIndexData.hp <= 0) return;

            int damage = 70;

            enemy.enemyIndexData.hp -= damage;

            if (enemy.enemyIndexData.hp <= 0)
            {
                enemy.navMeshAgent.enabled = false;
                enemy.animator.SetTrigger("Die");

                DOVirtual.DelayedCall(0.15f, delegate
                {
                    PlayerController.instance.enemyTarget.enabled = false;
                });
            }
            else enemy.animator.SetTrigger("Damage");
        }
    }
}
