using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "GameDataHolder/Data", order = 1)]

public class GameDataHolder : ScriptableObject
{
    public Utility.Levels[] Levels;

    public float waveDelay()
    {
        return Levels[PlayerPrefs.GetInt(Utility.LevelNo, 1) - 1].waveDelay;
    }

    public int GetEnemiesCount()
    {
        return Levels[PlayerPrefs.GetInt(Utility.LevelNo, 1) - 1].enemyTypes.Length;
    }

    public GameObject SpawnEnvironment(int levelNo)
    {
        return Levels[levelNo].Environment;
    }

    public Sprite SetWallImage()
    {
        return Levels[PlayerPrefs.GetInt(Utility.LevelNo,1) - 1].WallImage;
    }
    public Color SetWallImageColor()
    {
        return Levels[PlayerPrefs.GetInt(Utility.LevelNo, 1) - 1].WallImageColor;
    }

    public Color SetOutlineColor()
    {
        return Levels[PlayerPrefs.GetInt(Utility.LevelNo, 1) - 1].OutlineColor;
    }

    public GameObject SpawnLevelEnemies(int enemyNo)
    {
        return Levels[PlayerPrefs.GetInt(Utility.LevelNo, 1) - 1].enemyTypes[enemyNo].SpawnEnemy();

        //List<GameObject> enemies = new List<GameObject>();
        //enemies.Clear();
        //foreach(EnemyType x in Levels[PlayerPrefs.GetInt(Utility.LevelNo,1) - 1].enemyTypes)
        //{
        //    enemies.Add(x.SpawnEnemy());
        //}
        //return enemies;
    }
}
