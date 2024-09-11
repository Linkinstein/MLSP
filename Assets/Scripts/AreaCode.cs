using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCode : MonoBehaviour
{
    public bool canAlert;
    public GameObject[] Neighbours;
    public Transform[] NeighbourDef;
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
                collidedObject.GetComponent<EnemyAI>().ac = this;
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

            if (units.Contains(collidedObject))
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

    public void Alert(GameObject AO)
    {
        bool isNeighbour = false;
        int i = 0;
        foreach (GameObject neiheyhey in Neighbours)
        {
            if (neiheyhey == AO)
            {
                isNeighbour = true;
                break;
            }
            else i++;
        }

        if (isNeighbour)
        {
            foreach (GameObject dude in units)
            {
                dude.GetComponent<EnemyAI>().Defend(NeighbourDef[i]);
            }
        }
    }

    public void Alarm()
    {
        if (canAlert)
        {
            foreach (GameObject neiheyhey in Neighbours)
            {
                neiheyhey.GetComponent<AreaCode>().Alert(this.gameObject);
            }
        }
    }
}
