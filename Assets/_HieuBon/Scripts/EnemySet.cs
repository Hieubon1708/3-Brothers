using Unity.VisualScripting;
using UnityEngine;
using static GameController;

public class EnemySet : MonoBehaviour
{
#if UNITY_EDITOR
    public EnemyType type;

    public bool isReset;

    public GameObject[] weapons;
    public GameObject[] bodies;
    public GameObject[] heads;
    public GameObject[] shields;

    public AnimatorOverrideController[] animatorControllers;

    private void OnValidate()
    {
        if (!isReset || Application.isPlaying) return;

        DeactiveAll();

        Enemy enemy = GetComponent<Enemy>();

        if (type == EnemyType.E4)
        {
            GetComponent<Animator>().runtimeAnimatorController = animatorControllers[1];
            if (enemy == null) enemy = transform.AddComponent<Archer>();
        }
        else
        {
            GetComponent<Animator>().runtimeAnimatorController = animatorControllers[0];
            if (enemy == null) enemy = transform.AddComponent<Infantitry>();
        }

        enemy.type = type;

        switch (type)
        {
            case EnemyType.E1:
                ActiveSet(0, 0, 0);
                break;
            case EnemyType.E2:
                ActiveSet(1, 1, 1);
                break;
            case EnemyType.E3:
                ActiveSet(2, 1, 0);
                break;
            case EnemyType.E4:
                ActiveSet(5, 2, 1, 0);
                break;
            case EnemyType.E5:
                ActiveSet(4, 1, 1);
                break;
        }
    }

    void ActiveSet(int weaponIndex, int headIndex, int bodyIndex, int shieldIndex = -1)
    {
        weapons[weaponIndex].SetActive(true);
        heads[headIndex].SetActive(true);
        bodies[bodyIndex].SetActive(true);
        if (shieldIndex != -1) shields[shieldIndex].SetActive(true);
    }

    void DeactiveAll()
    {
        foreach (var e in weapons)
        {
            e.SetActive(false);
        }
        foreach (var e in heads)
        {
            e.SetActive(false);
        }
        foreach (var e in bodies)
        {
            e.SetActive(false);
        }
        foreach (var e in shields)
        {
            e.SetActive(false);
        }
    }
}
#endif


