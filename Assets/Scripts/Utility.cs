using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility 
{
    public static string LevelNo = "LevelNo";

    [System.Serializable]
    public struct Enemy
    {
        public int enemyNo;
        public GameObject model;
        public float timeFactor;
        [Range(2,8)]
        public int noOfhits;
    }

    [System.Serializable]
    public struct Levels
    {
        public GameObject Environment;
        public Sprite WallImage;
        public Color WallImageColor;
        public Color OutlineColor;
        public EnemyType[] enemyTypes;
        public float waveDelay;
    }
}
