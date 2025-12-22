using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float strength = 1;
    [SerializeField] private float frictionForce = 1;
    [SerializeField] private float cooltime = 1;

    private Rigidbody2D rigidbody;
    private LineRenderer lineRenderer;
    private Vector2 stopOffset = new Vector2(0.3f, 0.3f);
    private Vector2 dir;
    private bool isDrag = false;
    private bool isMoving = false;
    private float currentSpeed;

    public ParticleSystem particleP;
    public HealthSystem HealthSystem;
    public event Action OnBump;
    public event Action OnStop;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        HealthSystem = GetComponent<HealthSystem>();
    }

    private void Update()
    {
        if(rigidbody.linearVelocity != Vector2.zero)
        {
            currentSpeed -= (frictionForce*currentSpeed/2/ 250) * (4-(Mathf.InverseLerp(0, -dir.magnitude, rigidbody.linearVelocity.magnitude)*3));
            rigidbody.linearVelocity = -(dir.normalized) * currentSpeed;
            if(Mathf.Abs(rigidbody.linearVelocity.x) < stopOffset.x && Mathf.Abs(rigidbody.linearVelocity.y) < stopOffset.y)
            {
                rigidbody.linearVelocity = Vector2.zero;
                StartCoroutine(Cooltime());
                OnStop?.Invoke();
            }
        }
        if(isDrag)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, mousePosition);
        }
    }

    public void GetSpeed()
    {
        strength += 0.5f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isMoving) return;
        isDrag = true;
        lineRenderer.enabled = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(isDrag)
        {
            isDrag = false;
            isMoving = true;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dir = mousePosition - transform.position;
            float distance = Mathf.Clamp(dir.magnitude, 0, 5);
            currentSpeed = strength * distance;
            rigidbody.linearVelocity = -(dir.normalized) * currentSpeed;
            lineRenderer.SetPosition(1, transform.position);
            lineRenderer.enabled = false;

        }
    }

    private IEnumerator Cooltime()
    {
        yield return new WaitForSeconds(cooltime);
        isMoving = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            ParticleSystem particle = Instantiate(particleP);
            particle.gameObject.transform.position = collision.GetContact(0).point;
            float atan = Mathf.Atan2(collision.GetContact(0).normal.x, collision.GetContact(0).normal.y);
            float angle =  (collision.GetContact(0).normal.x != 0 ? -atan : atan) * Mathf.Rad2Deg;
            particle.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,0, angle));
            particle.Play();
            dir = Vector2.Reflect(dir, collision.GetContact(0).normal);
            rigidbody.linearVelocity = -(dir.normalized) * currentSpeed;
            OnBump?.Invoke();
        }
    }
}
