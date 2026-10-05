using UnityEngine;
public enum WeaponType
{
    Melee,
    Gun,
    Shotgun,
    Laser,
    Nuke
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "MosquitoOverkill/Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public WeaponType weaponType;

    [Header("Combat")]
    public float damage = 1f;
    public float range = 5f;
    public float cooldown = 0.5f;

    [Header("Projectile")]
    public GameObject projectile;
    public int projectileCount = 1;
    public float spread = 0f;

    [Header("Special")]
    public bool piercing;
}