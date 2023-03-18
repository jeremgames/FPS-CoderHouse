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
    [SerializeField] float timeToForget;
    [SerializeField] float newtimeToForget;
    [SerializeField] private Transform playerTransforms;
    [SerializeField] private float laserDistance;
    RaycastHit hit;
    bool pursuit = false;
    void Start()
    {
        playerTransform = PlayerController.Instance.transform;
        timeSinceLastShot = fireRate;
    }

    void Update()
    {
        Vector3 vectorToChar = playerTransforms.position - transform.position;
        Vector3 vectorToChar2 = vectorToChar.normalized;


        if (Physics.Raycast(transform.position, vectorToChar2, out hit, laserDistance))
        {
            pursuit = true;
            Debug.Log("detectado");
            Move();
        }
        else if (pursuit == true)
        {
            Debug.Log("en un rato me olvido");
            Move();
            timeToForget -= Time.deltaTime;

            if (timeToForget <= 0)
            {
                pursuit = false;
                timeToForget = newtimeToForget;

            }
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

    public void Move()
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

