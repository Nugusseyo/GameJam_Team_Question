using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float strength = 1;
    [SerializeField] private float frictionForce = 1;
    [SerializeField] private float cooltime = 1;
    [SerializeField] private LineRenderer predictLine;
    [SerializeField] private LineRenderer predictLine2;

    private Rigidbody2D rigidbody;
    private LineRenderer lineRenderer;
    private CinemachineImpulseSource impulseSource; 
    private Vector2 stopOffset = new Vector2(0.3f, 0.3f);
    private Vector2 dir;
    private bool isDrag = false;
    private bool isMoving = false;
    private float currentSpeed;

    public ParticleSystem particleP;
    public ParticleSystem particleP2;
    public Upgrade UpgradeC;
    public HealthSystem HealthSystem;
    public Level Level;
    public event Action OnBump;
    public event Action OnStop;
    public Vector2? RandomBounce = null;
    public Vector3 StartPosition;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        HealthSystem = GetComponent<HealthSystem>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        Level = GetComponent<Level>();
        UpgradeC = GetComponent<Upgrade>();
        lineRenderer.enabled = false;
    }

    private void FixedUpdate()
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
            lineRenderer.SetPosition(0, new Vector3(StartPosition.x, StartPosition.y, 0));
            lineRenderer.SetPosition(1, mousePosition);
            Vector2 predictDir = -(mousePosition - StartPosition);
            RaycastHit2D hit2d = Physics2D.Raycast(transform.position, predictDir, 200, LayerMask.GetMask("Wall"));
            RaycastHit2D hit2d2 = Physics2D.Raycast(hit2d.point+hit2d.normal, Vector2.Reflect(predictDir,hit2d.normal), 200, LayerMask.GetMask("Wall"));
            if (hit2d.collider == null) return;
            Vector2 dir2 = new Vector2(Mathf.Lerp(hit2d.point.x, hit2d2.point.x, 0.3f), Mathf.Lerp(hit2d.point.y, hit2d2.point.y, 0.3f));
            predictLine.SetPosition(0, transform.position);
            predictLine.SetPosition(1, hit2d.point);
            predictLine2.transform.position = hit2d.point;
            predictLine2.SetPosition(0, hit2d.point);
            predictLine2.SetPosition(1, dir2);
        }
    }

    public void SetMove(Vector2 vector2, float power)
    {
        dir = vector2;
        rigidbody.linearVelocity = dir * power;
    }

    public Vector2 GetDir()
    {
        return dir;
    }

    public void GetSpeed()
    {
        strength += 0.5f;
    }

    public void DragStart(Vector3 pos)
    {
        if (isMoving) return;
        isDrag = true;
        lineRenderer.enabled = true;
        predictLine.enabled = true;
        predictLine2.enabled = true;
        StartPosition = pos;
    }

    public void DragEnd()
    {
        if(isDrag)
        {
            isDrag = false;
            isMoving = true;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dir = mousePosition - StartPosition;
            float distance = Mathf.Clamp(dir.magnitude, 0, 5);
            currentSpeed = strength * distance;
            rigidbody.linearVelocity = -(dir.normalized) * currentSpeed;
            lineRenderer.enabled = false;
            predictLine.enabled = false;
            predictLine2.enabled = false;
        }
    }
    public void ResetDrag()
    {
        isDrag = false;
        lineRenderer.enabled = false;
        predictLine.enabled = false;
        predictLine2.enabled = false;
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
            ParticleSpawn(collision, particleP2, -1,1);
            ParticleSpawn(collision, particleP, 1, 0.4f);
            impulseSource.GenerateImpulseWithVelocity(collision.GetContact(0).normal/180*currentSpeed);
            OnBump?.Invoke();
            dir = RandomBounce != null ? (Vector2)RandomBounce : Vector2.Reflect(dir, collision.GetContact(0).normal);
            rigidbody.linearVelocity = -(dir.normalized) * currentSpeed;
            RandomBounce = null;
        }
    }

    private void ParticleSpawn(Collision2D other, ParticleSystem par, int rotation, float scale)
    {
        ParticleSystem particle = Instantiate(par);
        particle.gameObject.transform.localScale = new Vector2(scale, scale);
        particle.gameObject.transform.position = other.GetContact(0).point;
        float atan = Mathf.Atan2(other.GetContact(0).normal.x, -other.GetContact(0).normal.y);
        float angle = (other.GetContact(0).normal.y != 0 ? atan : -atan) * Mathf.Rad2Deg;
        particle.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle * rotation));
        particle.Play();
        StartCoroutine(ParticleDestroy(particle.gameObject));
    }

    private IEnumerator ParticleDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(2);
        Destroy(obj);
    }

}
