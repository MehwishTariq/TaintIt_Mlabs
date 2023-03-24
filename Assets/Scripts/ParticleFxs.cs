using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFxs : MonoBehaviour
{
    public static ParticleFxs instance;

    [SerializeField]
    public List<ParticleFx> fx;

    public List<GameObject> spawnedFx;

    public Transform fxParent;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnAllFx();
    }

    public void SpawnAllFx()
    {
        foreach (ParticleFx p_Sx in fx) 
        {
            for (int i = 0; i < p_Sx.NumberToPool; i++)
            {
                GameObject particle = Instantiate(p_Sx.fx, fxParent);
                particle.SetActive(false);
                particle.name = p_Sx.fxName.ToString();
                spawnedFx.Add(particle);
            }
        }
    }

    public void PlayFx(ParticleName name, Vector3 localPos, Vector3 localRot, Vector3 localScale, bool isworld = false)
    {
        foreach (GameObject x in spawnedFx)
        {
            if (x.name == name.ToString() && !x.activeInHierarchy)
            {
                if (isworld)
                {
                    x.transform.position = localPos;
                    x.transform.eulerAngles = localRot;
                    x.transform.localScale = localScale;
                }
                else
                {
                    x.transform.localPosition = localPos;
                    x.transform.localEulerAngles = localRot;
                    x.transform.localScale = localScale;
                }
                x.SetActive(true);
                x.GetComponent<ParticleSystem>().Play(true);
                StartCoroutine(DisableFx(x));
                break;
            }

        }
    }

    IEnumerator DisableFx(GameObject x)
    {
        yield return new WaitUntil(() => {
            return !x.GetComponent<ParticleSystem>().isPlaying; 
        });
        x.SetActive(false);
    }

    public void StopFx(ParticleName name)
    {
        foreach (ParticleFx x in fx)
        {
            if (x.fxName == name)
            {
                if (!x.fx.GetComponent<ParticleSystem>().isPlaying)
                    x.fx.GetComponent<ParticleSystem>().Stop();
            }
        }
    }
}


[System.Serializable]
public class ParticleFx
{
    [SerializeField]
    public ParticleName fxName;
    public GameObject fx;
    public int NumberToPool;
}

public enum ParticleName
{
    PaintSplash,
}
