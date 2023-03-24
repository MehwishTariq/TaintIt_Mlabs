using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painthit : MonoBehaviour
{

    public Transform reticle, spine;
    RaycastHit rayhit;
    public GameObject paintDecal,Gun, PaintBall;
    CinemachineImpulseSource impSource;
    Camera cam;
    Ray ray;
    [SerializeField]
    float offset;


    void Update()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        reticle.position = new Vector2(Mathf.Clamp(Input.mousePosition.x,0, Screen.width), Mathf.Clamp(Input.mousePosition.y, 0, Screen.height));

    }

    public void PaintBallHit(RaycastHit hit, Ray ray)
    {
        GameObject ball = Instantiate(PaintBall, ray.origin, Quaternion.identity);
        ball.transform.DOMove(hit.point, 0.1f).OnComplete(() => {
            ParticleFxs.instance.PlayFx(ParticleName.PaintSplash, hit.point, Vector3.zero, new Vector3(0.5f,0.5f,0.5f), true);
            Destroy(ball);
        });
    }

    IEnumerator ShootPaint()
    {
        while (true)
        {
           
            if (GameManager.instance.LevelFail && !GameManager.instance.LevelComplete)
            {
                GameManager.instance.GameFailed();
                reticle.gameObject.SetActive(false);
                Gun.SetActive(false);
                transform.DORotate(new Vector3(0,-165.95f,0),0.2f);
                GetComponentInChildren<Animator>().Play("Crying");
                yield break;
            }

            if (GameManager.instance.LevelComplete && !GameManager.instance.LevelFail)
            {
                GameManager.instance.GameComplete();
                reticle.gameObject.SetActive(false);
                Gun.SetActive(false);
                GetComponentInChildren<Animator>().Play("Dancing");
                yield break;
            }
            else
            {
                GameManager.instance.onGameStart -= Shoot;
                //spine.LookAt(reticle.position);
                
                if (Physics.Raycast(ray, out rayhit, 200f))
                {
                    transform.LookAt(new Vector3(rayhit.point.x , transform.localPosition.y, rayhit.point.z + offset));
                    Debug.DrawRay(ray.origin, ray.direction, Color.red);

                    if (rayhit.collider.CompareTag("Mask"))
                    {
                        UIManager.instance.scoreNo+=5;
                        UIManager.instance.SetScore();
                        GetComponentInChildren<Animator>().Play("Shoot");
                        AudioManager.instance.Play("PaintSplash");
                        impSource.GenerateImpulse();
                        GameObject paintSplatter = Instantiate(paintDecal, rayhit.point, Quaternion.identity);
                       
                        yield return new WaitForSeconds(0.1f);
                        rayhit.collider.GetComponent<SpriteMask>().enabled = true;
                        rayhit.collider.GetComponent<BoxCollider>().enabled = false;

                        paintSplatter.GetComponent<SpriteRenderer>().DOFade(0, 1f).OnComplete
                        (
                            () =>
                            {
                                Destroy(paintSplatter);
                            }
                        );

                    }

                    if (rayhit.collider.CompareTag("Enemy") && !rayhit.collider.GetComponent<EnemyMovement>().dead)
                    {
                        UIManager.instance.scoreNo+=5;
                        UIManager.instance.SetScore();
                        GetComponentInChildren<Animator>().Play("Shoot");
                        AudioManager.instance.Play("HitEnemy");
                        impSource.GenerateImpulse();
                        rayhit.collider.GetComponent<EnemyMovement>().DecreaseHealth();
                        PaintBallHit(rayhit, ray);


                    }
                    GetComponentInChildren<Animator>().Play("ShootPose");
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

        

    }

    public void Shoot()
    {
        
        //transform.LookAt(impSource.transform.forward);
        reticle.gameObject.SetActive(true);
        StartCoroutine(ShootPaint());
    }

    void Start()
    {
        cam = Camera.main;
        impSource = reticle.GetComponent<CinemachineImpulseSource>();
        GameManager.instance.onGameStart += Shoot;
       
    }
}
