using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody theRigidbody;

    private void Awake()
    {
        theRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 movementDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            movementDirection = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movementDirection = Vector3.right;
        }

        theRigidbody.AddForce(movementDirection * speed * Time.deltaTime);
    }
}
