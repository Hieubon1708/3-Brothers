using UnityEngine;

public class Arrow : MonoBehaviour
{
    float force;

    public void Shot(Vector3 startPosition, Vector3 dir, float force)
    {
        this.force = force;

        transform.position = startPosition;
        transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(-90f, 0, 0);

        gameObject.SetActive(true);
    }

    private void Update()
    {
        transform.Translate(-transform.forward * force * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
