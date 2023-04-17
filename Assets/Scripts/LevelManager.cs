using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    int currentLevel;
    public Transform LevelPlaceHolder;
    public SpriteRenderer WallImage { get; set; }
    public SpriteRenderer Square;

    private void Awake()
    {
        
        currentLevel = PlayerPrefs.GetInt(Utility.LevelNo, 1);
        SpawnLevel();
        
        Square.material.SetColor("_SolidOutline",GameManager.instance.dataHolder.SetOutlineColor());
    }

    void SpawnLevel()
    {
        Instantiate(GameManager.instance.dataHolder.SpawnEnvironment(currentLevel - 1), LevelPlaceHolder).SetActive(true);
        WallImage = GameManager.instance.dataHolder.SpawnWallImage(currentLevel - 1, LevelPlaceHolder);
        WallImage.sprite = GameManager.instance.dataHolder.SetWallImage();
        WallImage.color = GameManager.instance.dataHolder.SetWallImageColor();
    }

    private void OnEnable()
    {
        GameManager.instance.onGameComplete += SetNextLevel;
        
    }
    void SetNextLevel()
    {
        if (currentLevel == GameManager.instance.dataHolder.Levels.Length)
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
