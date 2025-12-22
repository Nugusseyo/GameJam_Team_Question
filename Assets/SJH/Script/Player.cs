using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float strength = 1;
    [SerializeField] private float frictionForce = 1;

    public event Action OnStop;

    private Rigidbody2D rigidbody;
    private Vector2 stopOffset = new Vector2(0.05f, 0.05f);
    private bool isDrag = false;

    private Vector2 dir;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(rigidbody.linearVelocity != Vector2.zero)
        {
            rigidbody.linearVelocity -= (frictionForce * rigidbody.linearVelocity / 2) / 250;
            if(Mathf.Abs(rigidbody.linearVelocity.x) < stopOffset.x && Mathf.Abs(rigidbody.linearVelocity.y) < stopOffset.y)
            {
                rigidbody.linearVelocity = Vector2.zero;
                OnStop?.Invoke();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDrag = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(isDrag)
        {
            isDrag = false;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dir = mousePosition - transform.position;
            float distance = Mathf.Clamp(dir.magnitude, 0, 5);
            rigidbody.linearVelocity = -(dir.normalized) * strength * distance;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            dir = Vector2.Reflect(dir, collision.GetContact(0).normal);
            rigidbody.linearVelocity = -(dir.normalized) * strength;
        }
    }
}
