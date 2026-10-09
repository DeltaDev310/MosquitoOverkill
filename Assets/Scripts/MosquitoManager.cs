using UnityEngine;
using TMPro;

public class MosquitoManager : MonoBehaviour
{
    public static MosquitoManager Instance;

    public int maxMosquitoes = 100;
    public int currentMosquitoes = 0;
    
    public int killCount = 0;
    
    public TMP_Text mosquitoCounterText;
    private void Awake()
    {
        Instance = this;
        mosquitoCounterText.text = "Mosquitoes: " + killCount;
    }

    public bool CanSpawn()
    {
        return currentMosquitoes < maxMosquitoes;
    }

    public void RegisterSpawn()
    {
        currentMosquitoes++;
    }

    public void RegisterDeath()
    {
        currentMosquitoes--;
        killCount++;
        mosquitoCounterText.text = "Mosquitoes: " + killCount;
    }
    public void ClearAllMosquitoes()
    {
        GameObject[] mosquitoes =
            GameObject.FindGameObjectsWithTag("Mosquito");

        GameObject survivor = null;

        if (mosquitoes.Length > 0)
        {
            survivor = mosquitoes[0];
        }

        foreach (GameObject mosquito in mosquitoes)
        {
            if (mosquito != survivor)
            {
                Destroy(mosquito);
            }
        }

        currentMosquitoes = survivor != null ? 1 : 0;
    }
}