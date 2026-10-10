using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
    public GameObject Bat;
    public GameObject Gun;
    public GameObject Shotgun;
    public GameObject Laser;
    public GameObject Nuke;
    
    public GameObject WhiteNukeLight;
    public GameObject RedAlarmLight;
    public AudioSource AlarmAudio;
    public AudioSource KazakhstanAudio;
    public AudioSource NukeAudio;
    
    private int upgradeStage = 0;
    private bool nukeActivated = false;
    
    public GameObject UpgradeText;

    public IEnumerator NukeCoroutine()
    {
        if (nukeActivated)
            yield break;

        nukeActivated = true;
        Time.timeScale = 0f;

        AlarmAudio.Play();
        KazakhstanAudio.Play();

        for (int i = 0; i < 30; i++)
        {
            RedAlarmLight.SetActive(true);
            yield return new WaitForSecondsRealtime(0.25f);

            RedAlarmLight.SetActive(false);
            yield return new WaitForSecondsRealtime(0.25f);
        }

        AlarmAudio.Stop();
        KazakhstanAudio.Stop();

        RedAlarmLight.SetActive(false);
        WhiteNukeLight.SetActive(true);

        NukeAudio.Play();

        yield return new WaitForSecondsRealtime(NukeAudio.clip.length);

        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    
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
        else if (upgradeStage == 2 && kills >= 200)
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
