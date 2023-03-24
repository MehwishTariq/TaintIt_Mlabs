using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] Levels;
    [Header("Count of enemies appearing at a time")]
    public int[] enemiesCount;
    
    public int currentLevel;
    public Transform LevelPlaceHolder;
    public SpriteRenderer WallImage;
    public SpriteRenderer Square;

    private void Awake()
    {
        
        currentLevel = PlayerPrefs.GetInt(Utility.LevelNo, 1);
        SpawnLevel();
        WallImage.sprite = GameManager.instance.dataHolder.SetWallImage();
        WallImage.color = GameManager.instance.dataHolder.SetWallImageColor();
        Square.material.SetColor("_SolidOutline",GameManager.instance.dataHolder.SetOutlineColor());
        // Levels[currentLevel - 1].SetActive(true);
    }

    public void SpawnLevel()
    {
        Instantiate(GameManager.instance.dataHolder.SpawnEnvironment(currentLevel - 1), LevelPlaceHolder).SetActive(true);
    }

    private void Start()
    {
        GameManager.instance.onGameComplete += SetNextLevel;
        
    }
    public void SetNextLevel()
    {
        if (currentLevel == 5)
            currentLevel = 1;
        else
            currentLevel++;

        PlayerPrefs.SetInt(Utility.LevelNo, currentLevel);
        
    }

    private void OnDisable()
    {
        GameManager.instance.onGameComplete -= SetNextLevel;
    }
}
