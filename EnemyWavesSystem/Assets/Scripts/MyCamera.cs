using UnityEngine;
using System.Collections;

public class MyCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float smooth;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * smooth);
    }
}
