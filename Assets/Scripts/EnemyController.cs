using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody rb;
    private bool pursuit;
    private float distanceToPursuit = 10f, distanceToLose = 15f;
    private int damageAmount;

    private Vector3 targetPoint;

    private void Update()
    {
        Pursuit();
    }

    
    private void Pursuit()
    {
        targetPoint = PlayerController.Instance.transform.position;
        targetPoint.y = transform.position.y;

        if(!pursuit)
        {
            if(Vector3.Distance(transform.position, targetPoint) < distanceToPursuit)
            {
                pursuit = true;
            }
        }
        else
        {

            transform.LookAt(targetPoint);
            rb.velocity = transform.forward * moveSpeed;

            if(Vector3.Distance(transform.position, targetPoint) < distanceToLose)
            {
                pursuit = false;
            }
        }

    }
}
