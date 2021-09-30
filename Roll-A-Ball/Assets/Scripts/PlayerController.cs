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
        if(count >= 12)
        {
            winTextObject.SetActive(true);
        }
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
            count++;
            SetCountText();
        }
    }
}
