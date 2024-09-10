using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class AreaCode : MonoBehaviour
{
    public GameObject pawn;
    public PatrolMasterNode[] patrols;
    public int patrolIndex;
    public GameObject spawnPoint;
    public List<GameObject> units;
    public bool respawn;
    public float respawnTimer;
    public float respawnInterval; 
    public int unitHousing = 2;

    private float currentRespawnTimer;
    private float currentIntervalTimer;

    private void Update()
    {
        if (patrolIndex >= patrols.Length) patrolIndex = 0;

        if (respawn)
        {
            if (units.Count == 0 && currentRespawnTimer <= 0)
            {
                currentRespawnTimer = respawnTimer;
            }

            if (currentRespawnTimer > 0)
            {
                currentRespawnTimer -= Time.deltaTime;

                if (currentRespawnTimer <= 0)
                {
                    currentRespawnTimer = 0;

                    currentIntervalTimer = 0;
                }
            }

            if (currentIntervalTimer >= 0)
            {
                currentIntervalTimer += Time.deltaTime; 

                if (currentIntervalTimer >= respawnInterval)
                {
                    currentIntervalTimer = 0; 

                    if (units.Count < unitHousing)
                    {
                        Spawn(pawn);
                        patrolIndex++;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameObject collidedObject = collision.gameObject;

            if (!units.Contains(collidedObject))
            {
                AddUnit(collidedObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player") LevelManager.instance.PlayerArea = this.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameObject collidedObject = collision.gameObject;

            if (!units.Contains(collidedObject))
            {
                RemoveUnit(collidedObject);
            }
        }
    }

    private void AddUnit(GameObject unit)
    {
        units.Add(unit);
    }

    private void RemoveUnit(GameObject unit)
    {
        units.Remove(unit);
    }

    private void Spawn(GameObject spawnee)
    {
        if (spawnPoint != null)
        {
            GameObject spwanee = Instantiate(spawnee, spawnPoint.transform.position, Quaternion.identity);
            spwanee.GetComponent<EnemyAI>().patrolPoints = patrols[patrolIndex].PatrolPath;
        }
    }
}
