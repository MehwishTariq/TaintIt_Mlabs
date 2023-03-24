using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    Transform wallImage, TriggerArea;
    public Transform spine;
    public bool dead = false;
    public float timeFactor = 4f;
    public int no_OfHits = 1;
    int hit = 0;
    GameObject areaPoint;
    bool wipe = false;
    bool moveEnemy = false;
   
    Tween move;

    public void DecreaseHealth()
    {
        no_OfHits--;
        if (no_OfHits <= 0)
        {
            GameManager.instance.numberOfKilled++;
            dead = true;
        }
    }


    private void Start()
    {
        wallImage = GameObject.FindWithTag("WallImage").transform;
        TriggerArea = GameObject.Find("Trigger").transform;
        CreateMovePoint();
    }

    void CreateMovePoint()
    {
        float add = Random.Range(1f, -1f);
        areaPoint = Instantiate(TriggerArea.gameObject);
        areaPoint.transform.position = new Vector3(TriggerArea.position.x + add, TriggerArea.position.y, TriggerArea.position.z);
        areaPoint.transform.rotation = Quaternion.identity;
        
    }

    void MoveEnemy()
    {
        moveEnemy = true;
        GetComponentInChildren<Animator>().Play("Run");
        spine.LookAt(areaPoint.transform);
        move = transform.DOMove(areaPoint.transform.position, timeFactor).OnComplete(() =>
        {
            GameManager.instance.EnemyReached = true;
        });
        
    }
  
    IEnumerator WipePaint()
    {
        wipe = true;
        while (true)
        {
            if (GameManager.instance.LevelComplete) yield break;
            if (GameManager.instance.LevelFail) yield break;
            wallImage.GetChild(Random.Range(0, wallImage.childCount - 1)).GetComponent<SpriteMask>().enabled = false;
            wallImage.GetChild(Random.Range(0, wallImage.childCount - 1)).GetComponent<BoxCollider>().enabled = true;            
            yield return new WaitForSeconds(0.8f);
            
        }
    }


    private void OnDestroy()
    {
        Destroy(areaPoint);
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameStarted && !moveEnemy)
            MoveEnemy();

        if (GameManager.instance.gameStarted)
        {
            if (Vector3.Distance(areaPoint.transform.position, transform.position) < 0.1f && !dead)
            {
                if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Wipe") && !wipe)
                {
                    spine.LookAt(-wallImage.forward);


                    StartCoroutine(WipePaint());
                }
                else
                    GetComponentInChildren<Animator>().Play("Wipe");

            }

            else if (!dead && (GameManager.instance.LevelComplete || GameManager.instance.LevelFail))
            {
                //play crying anim
                GetComponent<Animator>().Play("Idle");
                move.Kill();

            }
            if (dead && hit == 0)
            {
                GameManager.instance.EnemyReached = false;

                StopAllCoroutines();
                GetComponentInChildren<Animator>().Play("Splashed");
                //to instantiate when one wave is done
                if (GameManager.instance.numberOfKilled == GameManager.instance.numberOfEnemies)
                {
                    StartCoroutine(NewWave());
                    //GameObject.Find("WaveSystem").GetComponent<WaveSystem>().CreateInstance();
                }
                Destroy(gameObject, 2f);
                hit = 1;

            }
        }
    }

    IEnumerator NewWave()
    {
        yield return new WaitForSeconds(GameManager.instance.WaveDelay);
            GameManager.instance.InvokeNewWave(); 
    }
}
