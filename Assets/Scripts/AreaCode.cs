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
    public bool respawning = false;
    public float respawnTimer;
    public float respawnInterval; 
    public int unitHousing = 2;

    private void Update()
    {
        if (respawn)
        {
            if (units.Count == 0 && !respawning)
            {
                StartCoroutine(RespawnSequence());
            }
        }
    }

    IEnumerator RespawnSequence()
    {
        respawning = true;
        yield return new WaitForSeconds(respawnTimer);
        while (units.Count < unitHousing-1)
        {
            yield return new WaitForSeconds(respawnInterval);
            if (patrolIndex >= patrols.Length) patrolIndex = 0;
            Spawn(pawn);
            patrolIndex++;
        }
        respawning = false;
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
