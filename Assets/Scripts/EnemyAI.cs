using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public Transform[] waypoints; // los puntos de patrulla del enemigo
    public float speed = 2f; // velocidad de movimiento del enemigo
    public float accuracy = 1f; // precisión de los puntos de patrulla
    public float detectRadius = 5f; // radio de detección del jugador
    public float shootRange = 3f; // distancia de disparo del enemigo
    public GameObject bulletPrefab; // prefab de la bala del enemigo
    public Transform firePoint; // punto de origen de la bala

    private int currentWaypoint = 0; // índice del punto de patrulla actual
    private bool isDetectingPlayer = false; // si el enemigo ha detectado al jugador
    private Transform player; // el transform del jugador

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // encontrar el transform del jugador por etiqueta
    }

    void Update()
    {
        Pursuit();
    }

    private void Pursuit()
    {
        if (isDetectingPlayer)
        {
            // si el enemigo ha detectado al jugador, persigue y dispara
            Vector3 direction = player.position - transform.position;
            float distance = direction.magnitude;

            if (distance > shootRange)
            {
                // si el jugador está fuera del alcance de disparo, perseguirlo
                transform.Translate(speed * Time.deltaTime * direction.normalized, Space.World);
                transform.LookAt(player);
            }
            else
            {
                // si el jugador está dentro del alcance de disparo, disparar
                Shoot();
            }
        }
        else
        {
            // si el enemigo no ha detectado al jugador, patrullar
            Vector3 direction = waypoints[currentWaypoint].position - transform.position;
            transform.Translate(speed * Time.deltaTime * direction.normalized, Space.World);
            transform.LookAt(waypoints[currentWaypoint]);

            if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < accuracy)
            {
                // si el enemigo llega al punto de patrulla, avanzar al siguiente
                currentWaypoint++;
                if (currentWaypoint >= waypoints.Length)
                {
                    // si llega al final, volver al principio
                    currentWaypoint = 0;
                }
            }

            // comprobar si el jugador está dentro del radio de detección
            if (Vector3.Distance(transform.position, player.position) < detectRadius)
            {
                isDetectingPlayer = true;
            }
        }
    }
    private void Shoot()
    {
        // crear una bala y dispararla
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.transform.forward = transform.forward;
    }
}

