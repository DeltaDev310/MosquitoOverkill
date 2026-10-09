using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpgradeManager : MonoBehaviour
{
    public GameObject Bat;
    public GameObject Gun;
    public GameObject Shotgun;
    public GameObject Laser;
    public GameObject Nuke;
    
    private int upgradeStage = 0;
    
    public GameObject UpgradeText;
    
    public IEnumerator UpgradeCoroutine()
    {
        
        yield return new WaitForSeconds(1f);
        UpgradeText.SetActive(false);
    }
    public void UpgradeWeapon(WeaponType weaponType)
    {
        MosquitoManager.Instance.ClearAllMosquitoes();
        switch (weaponType)
        {
            case WeaponType.Gun:
                Gun.SetActive(true);
                Bat.SetActive(false);
                UpgradeText.SetActive(true);
                StartCoroutine(UpgradeCoroutine());
                break;
            case WeaponType.Shotgun:
                Shotgun.SetActive(true);
                Gun.SetActive(false);
                UpgradeText.SetActive(true);
                StartCoroutine(UpgradeCoroutine());
                break;
            case WeaponType.Laser:
                Laser.SetActive(true);
                Shotgun.SetActive(false);
                UpgradeText.SetActive(true);
                StartCoroutine(UpgradeCoroutine());
                break;
            case WeaponType.Nuke:
                Nuke.SetActive(true);
                Laser.SetActive(false);
                UpgradeText.SetActive(true);
                StartCoroutine(UpgradeCoroutine());
                break;
        }
    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Bat.SetActive(true);
        UpgradeText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        int kills = MosquitoManager.Instance.killCount;

        if (upgradeStage == 0 && kills >= 50)
        {
            UpgradeWeapon(WeaponType.Gun);
            upgradeStage = 1;
        }
        else if (upgradeStage == 1 && kills >= 100)
        {
            UpgradeWeapon(WeaponType.Shotgun);
            upgradeStage = 2;
        }
        else if (upgradeStage == 2 && kills >= 300)
        {
            UpgradeWeapon(WeaponType.Laser);
            upgradeStage = 3;
        }
        else if (upgradeStage == 3 && kills >= 400)
        {
            UpgradeWeapon(WeaponType.Nuke);
            upgradeStage = 4;
        }
    }
}
