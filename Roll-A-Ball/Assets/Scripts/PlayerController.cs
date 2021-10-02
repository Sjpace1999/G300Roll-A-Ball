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

    private float speed;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

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

    }

    void SetLoseText()
    {
            
    }

    public void incrementCount()
    {
        count++;
        SetCountText();
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
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
