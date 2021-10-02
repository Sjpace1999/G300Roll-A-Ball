using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speedRot = 0;
    public float ExtSpeed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public Rigidbody projectile;
    public Rigidbody enemy;

    private float speed;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private int numEnemies=12;

    private float rotationA;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }

    void OnFire()
    {
        Rigidbody clone;
        clone = Instantiate(projectile, transform.position, Quaternion.Euler(0, transform.rotation.y+35, 0));
        clone.velocity = transform.TransformDirection(Vector3.forward * 10);

        //script from https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        //movementX = movementVector.x;
        //movementY = movementVector.y;

        if (movementVector.x > 0) {
            rotationA = 1*speedRot;
        }
        else if (movementVector.x < 0)
        {
            rotationA = -1*speedRot;
        }
        else
        {
            rotationA = 0;
        }

        if(movementVector.y > 0)
        {
            speed = ExtSpeed;
        }
        else
        {
            speed = 0;
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 50)
        {
            winTextObject.SetActive(true);
        }
    }

    public void incrementCount()
    {
        count++;
        numEnemies--;
        SetCountText();
    }

    public void spawnEnemy(Vector3 position)
    {
        Rigidbody clone1;
        Rigidbody clone2;
        Instantiate(enemy, position, Quaternion.Euler(45, 45, 45));
    }


    void FixedUpdate()
    {
        //Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddRelativeForce(Vector3.forward * speed);

        transform.Rotate(new Vector3(0, rotationA, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            loseTextObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
