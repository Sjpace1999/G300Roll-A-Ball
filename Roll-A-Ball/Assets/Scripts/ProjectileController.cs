using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController playerController;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            playerController.incrementCount();
            playerController.spawnEnemy(transform.position);
            Destroy(gameObject);
            
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
