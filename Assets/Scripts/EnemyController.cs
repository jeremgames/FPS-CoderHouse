using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    
    private bool pursuit;
    public float distanceToPursuit = 10f, distanceToLose = 15f, distanceToStop;
    private Vector3 targetPoint, startPoint;
    public float keepPursuitTime = 5f;
    private float pursuitCounter;

    public GameObject bullet;
    public Transform firePoint;
    public float fireRate, waitBetweenShots = 2f, timeToShoot = 1f;
    private float fireCount, shotWaitCounter, shootTimeCounter;

    private void Start()
    {
        startPoint = transform.position;
        shootTimeCounter = timeToShoot;
        shotWaitCounter = waitBetweenShots;
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
                shootTimeCounter = timeToShoot;
                shotWaitCounter = waitBetweenShots;
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

            if(shotWaitCounter > 0)
            {
                shotWaitCounter-= Time.deltaTime;
                if(shotWaitCounter <=0)
                {
                    shootTimeCounter = timeToShoot;
                }
            }
            else
            {
                shootTimeCounter -= Time.deltaTime;

                if (shootTimeCounter > 0)
                {
                    fireCount -= Time.deltaTime;

                    if (fireCount <= 0)
                    {
                        fireCount = fireRate;
                        Instantiate(bullet, firePoint.position, firePoint.rotation);
                    }
                    agent.destination = transform.position;
                }
                else
                {
                    shotWaitCounter = waitBetweenShots;
                }
            }
        }
    }
}

