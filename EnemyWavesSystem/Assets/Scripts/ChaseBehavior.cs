using UnityEngine;
using System.Collections;

/// <summary>
/// Simple chase behavior for enemies.
/// </summary>
public class ChaseBehavior : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float speed;

    private Rigidbody theRigidbody;

    private void Awake()
    {
        theRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 movementDirection = (target.position - transform.position).normalized;
        theRigidbody.AddForce(movementDirection * speed * Time.deltaTime);
    }
}
