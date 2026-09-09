using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ArmaController : MonoBehaviour
{
    enum WeaponType
    {
        Pistol,
        Rifle,
        Shotgun,
        Sniper,
        RocketLauncher,
        GrenadeLauncher
    };

    [SerializeField] private WeaponType weaponType = new WeaponType();

    [SerializeField] private float fireRate;
    [SerializeField] private float fireRateDelta;

    private bool EnemyDetected = false;
    private Transform EnemyTransform;

    public GameObject bullet;

    public SpriteRenderer spriteRenderer;

    public Sprite[] sprites = new Sprite[6];
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        switch (weaponType)
        {
            case WeaponType.Pistol:
                fireRate = 0.5f;
                fireRateDelta = 0.5f;
                spriteRenderer.sprite = sprites[0];
                break;
            case WeaponType.Rifle:
                fireRate = 0.1f;
                fireRateDelta = 0.1f;
                spriteRenderer.sprite = sprites[1];
                break;
            case WeaponType.Shotgun:
                fireRate = 0.5f;
                fireRateDelta = 0.5f;
                spriteRenderer.sprite = sprites[2];
                break;
            case WeaponType.Sniper:
                fireRate = 1.0f;
                fireRateDelta = 1.0f;
                spriteRenderer.sprite = sprites[3];
                break;
            case WeaponType.RocketLauncher:
                fireRate = 1.0f;
                fireRateDelta = 1.0f;
                spriteRenderer.sprite = sprites[4];
                break;
            case WeaponType.GrenadeLauncher:
                fireRate = 1.0f;
                fireRateDelta = 1.0f;
                spriteRenderer.sprite = sprites[5];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRateDelta >= 0)
        {
            fireRateDelta -= Time.deltaTime;
        }
        if (EnemyDetected)
        {
            Vector3 dir = EnemyTransform.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if (fireRateDelta <= 0)
            {
                if (weaponType == WeaponType.Shotgun)
                {
                    Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, Random.Range(-10, 10)));
                    Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, Random.Range(-10, 10)));
                    Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, Random.Range(-10, 10)));
                    Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, Random.Range(-10, 10)));
                    Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, Random.Range(-10, 10)));
                }
                else
                {
                    Instantiate(bullet, transform.position, transform.rotation);
                }
                EnemyDetected = false;
                fireRateDelta = fireRate;
            }
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Detected");
            EnemyDetected = true;
            EnemyTransform = collision.gameObject.transform;
        }
    }
}
