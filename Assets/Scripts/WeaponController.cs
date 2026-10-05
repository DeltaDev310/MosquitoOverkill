using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponData currentWeapon;
    public Transform firePoint;

    private float nextAttackTime;

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

    void ShootShotgun()
    {
        Debug.Log("SHOTGUN");
    }

    void ShootLaser()
    {
        Debug.Log("LASER");
    }

    void UseNuke()
    {
        Debug.Log("NUKE");
    }
}