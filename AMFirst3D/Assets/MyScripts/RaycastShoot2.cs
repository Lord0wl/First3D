using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastShoot2 : MonoBehaviour
{
    public bool projectileShoot = true;
    public GameObject prefab;
    public Transform spawnPosition;
    public float shootSpeed = 20;
    public float bulletLifetime = 10;

    private bool isFiring;

    void Update()
    {
        if (!isFiring) return;

        //raycast to shoot here
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit) && !projectileShoot)
        {
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);

                EnemyHealth enemy = hit.collider.gameObject.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(1);
                }
            }
        }
        else
        {
            Vector3 dir;

            if (Physics.Raycast(ray, out hit))
            {
                dir = hit.point;
            }
            else
            {
                dir = Camera.main.transform.position + Camera.main.transform.forward * shootSpeed;
            }

            GameObject bullet = Instantiate(prefab, spawnPosition.position, Quaternion.identity);

            Vector3 velocity = dir - spawnPosition.position;
            velocity.Normalize();

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = velocity * shootSpeed;
            }

            Destroy(bullet, bulletLifetime);
        }
    }

    public void OnShoot(InputValue value)
    {
        isFiring = value.isPressed;
    }
}