using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComabtTutorial : Death
{
    public GameObject SpawnUnit;
    public Transform[] Location;
    public GameObject SpawnPoint;

    public override void Die()
    {
        GameObject spawnee = Instantiate(SpawnUnit, SpawnPoint.transform.position, Quaternion.identity);
        spawnee.GetComponent<EnemyAI>().patrolPoints = Location;
        base.Die();
    }
}
