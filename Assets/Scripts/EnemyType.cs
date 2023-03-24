using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "EnemyClass/Enemy", order = 1)]
public class EnemyType : ScriptableObject
{
    public Utility.Enemy EnemyProp;

    public GameObject SpawnEnemy()
    {
        EnemyProp.model.GetComponent<EnemyMovement>().timeFactor = EnemyProp.timeFactor;
        EnemyProp.model.GetComponent<EnemyMovement>().no_OfHits = EnemyProp.noOfhits;
        EnemyProp.model.name = "New" + EnemyProp.enemyNo;
        return EnemyProp.model;
    }
}
