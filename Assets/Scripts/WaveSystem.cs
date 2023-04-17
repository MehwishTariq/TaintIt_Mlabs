using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    
    public Transform[] newPos;
    public GameObject EnemyPrefab;
 
    //int numberOfEnemiesAtTime;

    private void OnDisable()
    {
        GameManager.instance.onGameComplete -= RemoveAllEnemies;
        GameManager.instance.onGameFail -= RemoveAllEnemies;
        GameManager.instance.onNewWaveCreation -= CreateInstance;
    }
    private void OnEnable()
    {
        GameManager.instance.onGameComplete += RemoveAllEnemies;
        GameManager.instance.onGameFail += RemoveAllEnemies;
        GameManager.instance.onNewWaveCreation += CreateInstance;
    }
    private void Start()
    {
        //numberOfEnemiesAtTime = GameManager.instance.numberOfEnemies;
        if(PlayerPrefs.GetInt(Utility.LevelNo,1) != 2)
            CreateInstance();
    }

    public void CreateInstance()
    {
        GameManager.instance.numberOfKilled = 0;
        int posNo = 0;
        for (int i = 0; i < GameManager.instance.numberOfEnemies; i++)
        {
            if (posNo >= newPos.Length)
                posNo = 0;
            Instantiate(GameManager.instance.dataHolder.SpawnLevelEnemies(i), newPos[posNo].position, Quaternion.identity, gameObject.transform);
            posNo++;
        }
    }

    public void RemoveAllEnemies()
    {
        foreach (Transform x in transform)
            x.gameObject.SetActive(false);
    }
}
