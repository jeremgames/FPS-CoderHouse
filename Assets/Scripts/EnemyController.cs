using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    public Animator anim;

    private Transform playerTransform;
    private float timeSinceLastShot;
    private bool canShoot = true;

    void Start()
    {
        playerTransform = PlayerController.Instance.transform;
        timeSinceLastShot = fireRate;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < agent.stoppingDistance)
        {
            // El enemigo está cerca del jugador, así que deja de moverse
            agent.isStopped = true;
            anim.SetBool("isMoving", false);

            // Si es posible, dispara al jugador
            if (canShoot)
            {
                Shoot();
            }
        }
        else
        {
            // El enemigo está lejos del jugador, así que sigue moviéndose
            agent.isStopped = false;
            anim.SetBool("isMoving", true);

            // Actualiza el destino del enemigo para seguir al jugador
            agent.SetDestination(playerTransform.position);
        }
    }

    void Shoot()
    {
        // Apunta el firePoint hacia el jugador
        firePoint.LookAt(playerTransform.position + new Vector3(0f, 1.5f, 0f));

        // Crea una bala y la dispara
        Instantiate(bullet, firePoint.position, firePoint.rotation);

        // Inicia la animación de disparo
        anim.SetTrigger("fireShot");

        // Desactiva la capacidad de disparar temporalmente para evitar que el enemigo dispare demasiado rápido
        canShoot = false;
        timeSinceLastShot = 0;
    }

    void FixedUpdate()
    {
        // Actualiza el tiempo transcurrido desde el último disparo
        timeSinceLastShot += Time.deltaTime;

        // Si ha pasado suficiente tiempo desde el último disparo, habilita la capacidad de disparar nuevamente
        if (timeSinceLastShot > fireRate)
        {
            canShoot = true;
        }
    }
}

