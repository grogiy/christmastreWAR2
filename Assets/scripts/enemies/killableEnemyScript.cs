using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;

    public Transform pointA;
    public Transform pointB;

    private Transform currentPoint;

    public float speed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        currentPoint = pointB;
        anim.SetBool("isRunning", true);
    }

    void Update()
    {
        // Двигаемся к текущей точке
        if (currentPoint == pointB)
        {
            rb.linearVelocity = new Vector3(-speed, rb.linearVelocity.y, rb.linearVelocity.z);
        }
        else
        {
            rb.linearVelocity = new Vector3(speed, rb.linearVelocity.y, rb.linearVelocity.z);
        }

        // Проверяем, достигли ли точки B
        if (Vector3.Distance(transform.position, pointB.position) < 0.5f 
            && currentPoint == pointB)
        {
            currentPoint = pointA;
        }

        // Проверяем, достигли ли точки A
        if (Vector3.Distance(transform.position, pointA.position) < 0.5f 
            && currentPoint == pointA)
        {
            currentPoint = pointB;
        }
    }
}