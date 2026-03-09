using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastShoot : MonoBehaviour
{
    public InputActionReference action;
    public bool projectileShoot = true;
    public GameObject prefab;
    public Transform spawnPosition;
    public float shootSpeed = 20;
    public float bulletLifetime = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShoot(InputValue value)
    {
        if (value.isPressed)
        {
            //raycast to shoot here
            //store any info about the thing we hit
            RaycastHit hit;
            //create our ray from the camera, in the camera forward direction
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out hit) && !projectileShoot)
            {
                //if I hit something with a collider
                if (hit.collider != null)
                {
                    //tell me the name of the thing that i hit
                    Debug.Log(hit.collider.gameObject.name);
                    //if i hit an enemy
                    if (hit.collider.gameObject.GetComponent<EnemyHealth>() != null)
                    {
                        //make them take damage
                        hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
                    }
                }
            }
            else
            {
                Vector3 dir = hit.point;
                if (hit.collider == null)
                {
                    //we dont hit anything with our raycast, so we need to know which direction to shoot in
                    //even if we dont hit anything we still want to shoot in the direciton were looking
                    dir = Camera.main.transform.position + Camera.main.transform.forward * shootSpeed;
                }
                GameObject bullet = Instantiate(prefab, spawnPosition.position, Quaternion.identity);
                Vector3 velocity = dir - spawnPosition.position;
                velocity.Normalize();
                bullet.GetComponent<Rigidbody>().linearVelocity = velocity * shootSpeed;
            }
        }
    }
}
