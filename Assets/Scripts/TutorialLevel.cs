using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevel : MonoBehaviour
{
    public Transform[] newPos;

    private void Start()
    {
        if (PlayerPrefs.GetInt(Utility.LevelNo, 1) == 2)
        {
            Transform x = GameManager.instance.levelManager.WallImage.transform;
            int i = 0;
            while (i < x.childCount)
            {
                if (Random.value < 0.5f)
                {
                    x.GetChild(i).GetComponent<SpriteMask>().enabled = true;
                    x.GetChild(i).GetComponent<BoxCollider>().enabled = false;
                }
                i++;
            }
            GameManager.instance.waveSystem.newPos = newPos;
            GameManager.instance.waveSystem.CreateInstance();
        }
    }

}
