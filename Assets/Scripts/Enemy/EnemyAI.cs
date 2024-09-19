using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    LevelManager lm;

    [Header("Physics")]
    public float speed = 1f;
    public float normalSpeed = 1f;
    public float chaseSpeed = 3f;

    [Header("Pathfinding")]
    public Vector2 targetVector;
    public Vector2 PlayerLastSeen;
    public float pathUpdateSeconds = 0.5f;
    public float nextWaypointDistance = 0.1f;
    public int waypointIndex = 0;

    [Header("Patrol")]
    public float patrolPause = 5f;
    public float chaseTimer = 10f;
    public Transform[] patrolPoints;
    public int patrolPointIndex = 0;

    [Header("Defense")]
    public float defTimer = 20f;
    private Coroutine StopDef;
    public GameObject searchMarker;

    [Header("Custom Behavior")]
    public bool moveEnabled = true;
    public bool directionLookEnabled = true;
    public AreaCode ac;

    [Header("Chase")]
    public bool isChasing = false;
    public int chaseMinRange = 1;
    public int chaseMaxRange = 3;
    private Coroutine StopChase;
    public GameObject alertMarker;


    private Path path;
    private Seeker seeker;
    private Rigidbody2D rb;

    public void Start()
    {
        lm = LevelManager.instance;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        if (patrolPoints != null) targetVector = patrolPoints[patrolPointIndex].position;
        else
        {
            patrolPoints = new Transform[1];
            patrolPoints[0] = transform;
        }

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        if (moveEnabled)
        {
            PathFollow();
        }
    }

    private void UpdatePath()
    {
        if (moveEnabled && seeker.IsDone())
        {
            seeker.StartPath(rb.position, targetVector, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        // Reached end of path
        if (waypointIndex >= path.vectorPath.Count)
        {
            return;
        }

        #region Arrive at Patrol Point
        if (Vector2.Distance(rb.position, patrolPoints[patrolPointIndex].position) < nextWaypointDistance && !isChasing)
        {
            StartCoroutine(PatrolPointArrived());
            return;
        }
        #endregion

        Vector2 direction = ((Vector2)path.vectorPath[waypointIndex] - rb.position).normalized;

        #region Move

        if (isChasing)
        {
            float chaseRange = Vector2.Distance(rb.position, targetVector);
            if (chaseRange < chaseMaxRange) moveEnabled = false;
            else moveEnabled = true;
        }
        else moveEnabled = true;

        if (moveEnabled)
        {
            // Direction Calculation
            Vector2 force = direction * speed;

            // Movement
            float distance = Vector2.Distance(rb.position, path.vectorPath[waypointIndex]);
            rb.velocity = new Vector2(force.x, rb.velocity.y);

            // Next Waypoint
            if (distance < nextWaypointDistance)
            {
                waypointIndex++;
            }

        }

        #endregion

        #region Turn
        if (directionLookEnabled)
        {
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        #endregion
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            waypointIndex = 0;
        }
    }

    IEnumerator PatrolPointArrived()
    { 
        //transform.localScale = new Vector3(transform.localScale.x*x, transform.localScale.y, transform.localScale.z);
        moveEnabled = false;
        yield return new WaitForSeconds(patrolPause);
        NextPatrolPoint();
        moveEnabled = true;
    }

    void NextPatrolPoint()
    {
        if (patrolPointIndex >= patrolPoints.Length-1) patrolPointIndex = 0;
        else patrolPointIndex++;
        targetVector = patrolPoints[patrolPointIndex].transform.position;
    }

    public void PlayerFound(Collider2D playerCollider)
    {
        if (!isChasing) lm.alerted++;
        searchMarker.SetActive(false);
        targetVector = playerCollider.transform.position;
        moveEnabled = true;
        speed = chaseSpeed;
        isChasing = true;
        if (StopChase!=null) StopCoroutine(StopChase);
        alertMarker.SetActive(true);
        ac.Alarm();
    }

    public void UpdatePlayerPosition(Collider2D playerCollider)
    {
        targetVector = playerCollider.transform.position;
        isChasing = true;
    }

    public void PlayerLost()
    {
        StopChase = StartCoroutine(BackToPatrol());
    }

    private IEnumerator BackToPatrol()
    {
        yield return new WaitForSeconds(chaseTimer);
        targetVector = patrolPoints[patrolPointIndex].position;
        moveEnabled = true;
        speed = normalSpeed;
        isChasing = false;
        alertMarker.SetActive(false);
    }

    public void Defend(Transform defTarget)
    {
        searchMarker.SetActive(true);
        float rand = Random.Range(-3f, 3f);
        Vector2 newTargetVector = new Vector2(defTarget.position.x + rand, defTarget.position.x);
        isChasing = true;
        speed = chaseSpeed;
        targetVector = newTargetVector;
        StopChase = StartCoroutine(BackFromDefense());
    }

    private IEnumerator BackFromDefense()
    {
        yield return new WaitForSeconds(defTimer);
        targetVector = patrolPoints[patrolPointIndex].position;
        moveEnabled = true;
        speed = normalSpeed;
        isChasing = false;
        searchMarker.SetActive(false);
    }
}
