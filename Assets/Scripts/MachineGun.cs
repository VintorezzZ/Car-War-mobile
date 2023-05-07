using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class MachineGun : MonoBehaviour
{
    public Transform gun;
    public Transform barrel;
    public float gunSpeed;
    public float barrelSpeed;
    private float gunAngle;
    private float barrelAngle;
    public Projectile bullet;
    public Transform barrelL;
    public Transform barrelR;
    public float bulletSpeed;
    private float nextTimeToFire;
    private float fireRate = 5f;
    public AudioClip shotSound;
    public AudioSource audioSource;
    [SerializeField] private ParticleSystem _shootEffect;

    public int currentAmmo = 0;
    private float timer;

    private void Update()
    {        
        //RotateGun();
        //RotateBarrel();

        if (!GameManager.Instance.pause)
        {
            if (Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                if (currentAmmo > 0)
                {
                    Shoot();
                }
            }
        }

        UpdateAmmo();
        GameManager.Instance.UpdateAmmo(currentAmmo);
    }

    void RotateGun()
    {
        gunAngle += Input.GetAxis("Mouse X") * GameManager.Instance.sens * Time.deltaTime;
        gunAngle = Mathf.Clamp(gunAngle,-90,90);
        gun.localRotation = Quaternion.AngleAxis(gunAngle, Vector3.up);
    }

    void RotateBarrel()
    {
        barrelAngle += Input.GetAxis("Mouse Y") * barrelSpeed * -Time.deltaTime;
        barrelAngle = Mathf.Clamp(barrelAngle, -1, 20);
        barrel.localRotation = Quaternion.AngleAxis(barrelAngle, Vector3.right);
    }

    void Shoot()
    {        
        var bulletInstanceL = Instantiate(bullet, barrelL.position, Quaternion.identity);
        bulletInstanceL.Rigidbody.AddForce(barrelL.forward * bulletSpeed);;

        Destroy(bulletInstanceL, 5f);

        currentAmmo--;

        _shootEffect.Play();
        audioSource.PlayOneShot(shotSound);
    }

    void UpdateAmmo()
    {        
        timer += Time.deltaTime;
        if (timer >= 2.5f)
        {
            currentAmmo++;
            timer = 0;
        }

        if (currentAmmo < 0)
        {
            currentAmmo = 0;
        }
    }
}
