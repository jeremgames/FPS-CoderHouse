using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public Animator anim;

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
            if (agent.remainingDistance < 0.25f)
            {
                anim.SetBool("isMoving", false);
            }
            else
            {
                anim.SetBool("isMoving", true);
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
                anim.SetBool("isMoving", true);
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
                        firePoint.LookAt(PlayerController.Instance.transform.position + new Vector3(0f,1.5f,0f));
                        Vector3 targetDirection = PlayerController.Instance.transform.position - transform.position;
                        float angle = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up);
                        if (Mathf.Abs(angle) < 30f)
                        {
                            Instantiate(bullet, firePoint.position, firePoint.rotation);
                            anim.SetTrigger("fireShot");
                        }
                        else
                        {
                            shotWaitCounter = waitBetweenShots;
                        }
                    }
                    agent.destination = transform.position;
                }
                else
                {
                    shotWaitCounter = waitBetweenShots;
                }
                anim.SetBool("isMoving", false);
            }
        }
    }
}

