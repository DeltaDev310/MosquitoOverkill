using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WeaponController : MonoBehaviour
{
    public WeaponData currentWeapon;
    public Transform firePoint;
    public GameObject muzzleFlash;
    public GameObject laser;

    private LineRenderer laserLine;
    private float nextAttackTime;
    
    public UpgradeManager upgradeManager;

    void Start()
    {
        laserLine = laser.GetComponent<LineRenderer>();
        laser.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + currentWeapon.cooldown;
        }
    }
    
    

    void Attack()
    {
        switch (currentWeapon.weaponType)
        {
            case WeaponType.Gun:
                Shoot();
                break;

            case WeaponType.Melee:
                MeleeAttack();
                break;

            case WeaponType.Shotgun:
                ShootShotgun();
                break;

            case WeaponType.Laser:
                ShootLaser();
                break;

            case WeaponType.Nuke:
                UseNuke();
                break;
        }
    }

    void Shoot()
    {
        Instantiate(
            currentWeapon.projectile,
            firePoint.position,
            firePoint.rotation
        );
    }

    void MeleeAttack()
    {
        Debug.Log("BAT ATTACK");
    }

    void ShootLaser()
    {
        laser.SetActive(true);

        Vector2 origin = firePoint.position;
        Vector2 direction = firePoint.up;

        RaycastHit2D[] hits = Physics2D.RaycastAll(
            origin,
            direction,
            currentWeapon.range
        );

        Vector2 endPoint = origin + direction * currentWeapon.range;

        foreach (RaycastHit2D hit in hits)
        {

            if (hit.collider.CompareTag("Mosquito"))
            {
                Debug.Log("LASER HIT MOSQUITO");

                Mosquito mosquito = hit.collider.GetComponent<Mosquito>();

                if (mosquito != null)
                {
                    mosquito.Die();
                }
            }
        }

        laserLine.SetPosition(0, origin);
        laserLine.SetPosition(1, endPoint);

        Invoke(nameof(HideLaser), 0.05f);
    }

    void UseNuke()
    {
        enabled = false;
        upgradeManager.StartCoroutine(upgradeManager.NukeCoroutine());
    }

    void ShootShotgun()
    {
        muzzleFlash.SetActive(true);
        Invoke(nameof(HideMuzzleFlash), 0.05f);

        Vector2 origin = firePoint.position;
        Vector2 direction = firePoint.up;

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            origin,
            currentWeapon.range
        );

        foreach (Collider2D hit in hits)
        {
            if (!hit.CompareTag("Mosquito"))
                continue;

            Vector2 toTarget =
                (hit.transform.position - transform.position).normalized;

            float angle = Vector2.Angle(direction, toTarget);

            if (angle <= currentWeapon.spread / 2f)
            {
                Mosquito mosquito = hit.GetComponent<Mosquito>();

                if (mosquito != null)
                {
                    mosquito.Die();
                }
            }
        }
    }

    void HideMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }

    void HideLaser()
    {
        laser.SetActive(false);
    }
}