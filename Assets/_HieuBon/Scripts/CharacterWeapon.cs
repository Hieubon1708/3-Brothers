using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
    public WeaponSet[] weaponSets;

    public AnimatorOverrideController[] animatorOverrideControllers;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void WeaponSelect(int index, out float radius)
    {
        radius = weaponSets[index].radius;

        animator.runtimeAnimatorController = animatorOverrideControllers[index];

        for (int i = 0; i < weaponSets.Length; i++)
        {
            for (int j = 0; j < weaponSets[i].weapon.Length; j++)
            {
                weaponSets[i].weapon[j].SetActive(i == index);
            }
        }
    }
}

[System.Serializable]
public class WeaponSet
{
    public float radius;
    public GameObject[] weapon;
}
