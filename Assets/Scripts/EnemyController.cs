using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class EnemyController : MonoBehaviour
{
    private bool pursuit;
    public float distanceToPursuit = 10f, distanceToLose = 15f, distanceToStop;
    
    public NavMeshAgent agent;
    private Vector3 targetPoint, startPoint;

    public float keepPursuitTime = 5f;
    private float pursuitCounter;

    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCount;

    private void Start()
    {
        startPoint = transform.position;
    }

    private void Update()
    {
        Pursuit();
    }


    private void Pursuit()
    {
        targetPoint = PlayerController.Instance.transform.position;
        targetPoint.y = transform.position.y;

        if (!pursuit)
        {
            if (Vector3.Distance(transform.position, targetPoint) < distanceToPursuit)
            {
                pursuit = true;
                fireCount = 1f;
            }
            if (pursuitCounter > 0)
            {
                pursuitCounter -= Time.deltaTime;
                if (pursuitCounter <= 0)
                {
                    agent.destination = startPoint;
                }
            }
        }
        else
        {
            agent.destination = targetPoint;

            if (Vector3.Distance(transform.position, targetPoint) > distanceToLose)
            {
                pursuit = false;
                pursuitCounter = keepPursuitTime;
            }
            
            fireCount -= Time.deltaTime;    

            if (fireCount <= 0)
            {
                fireCount = fireRate;
                Instantiate(bullet, firePoint.position, firePoint.rotation);
            }
        }
    }
}

