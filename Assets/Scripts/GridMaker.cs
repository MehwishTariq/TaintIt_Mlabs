using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour
{
    public GameObject maskSquare;
    public SpriteRenderer Border;
    Sprite image;
    int completeImage;
    public int rows, column;

    private void Awake()
    {
        GameManager.instance.LevelComplete = false;
        GameManager.instance.LevelFail = false;
    }

    public void CheckImage()
    {
        completeImage = 0;
        image = GetComponent<SpriteRenderer>().sprite;
        foreach(Transform x in transform)
        {
            if (x.GetComponent<SpriteMask>().enabled)
            {
                completeImage++;
            }
            if(!x.GetComponent<SpriteMask>().enabled && !x.GetComponent<BoxCollider>().enabled)
            {
                x.GetComponent<BoxCollider>().enabled = true;
            }
        }
        if(completeImage == transform.childCount)
        {
            GameManager.instance.LevelComplete = true;
            GameManager.instance.LevelFail = false;
        }
        else if(completeImage == 1 && GameManager.instance.EnemyReached)
        {
            GameManager.instance.LevelComplete = false;
            GameManager.instance.LevelFail = true;
        }

        GameManager.instance.UpdateProgres(completeImage, transform.childCount);
    }

    private void Update()
    {
        if (!GameManager.instance.LevelComplete && !GameManager.instance.LevelFail)
            CheckImage();
       
    }

    [ContextMenu("GetBounds")]
    public void GetBounds()
    {
        Debug.Log("BoundsLocal:" + Border.localBounds);
        Debug.Log("BoundsMax:" + Border.localBounds.max);
        Debug.Log("BoundsMin:" + Border.localBounds.min);
        GameObject mask = Instantiate(maskSquare, Border.transform);
        mask.transform.localPosition = Border.localBounds.min;
    }


    [ContextMenu ("Create Grid")]
    public void CreateGrid()
    {
        image = GetComponent<SpriteRenderer>().sprite;
        for(int i = 0; i < column ; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject  mask = Instantiate(maskSquare,transform);
                //mask.transform.localPosition = new Vector3(-1.29f + (0.39f * i), 0.853f - (0.38f * j), 0);
                mask.transform.localPosition = new Vector3(-1.2f + mask.transform.localScale.x * mask.GetComponent<BoxCollider>().size.x * i, 0.64f - mask.transform.localScale.y * mask.GetComponent<BoxCollider>().size.y * j,0);
                mask.transform.localRotation = Quaternion.identity;

            }
        }
    }
}
