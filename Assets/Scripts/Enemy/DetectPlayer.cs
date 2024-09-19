using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    [SerializeField] EnemyAI eAI;
    CircleCollider2D cc;
    private Coroutine ccFade;
    public LayerMask groundLayerMask;

    private void Start()
    {
        cc = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 directionToCollision = (Vector2)collision.transform.position - (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, directionToCollision, directionToCollision.magnitude, groundLayerMask);

        if (hit.collider == null)
        {
            eAI.PlayerFound(collision);
            cc.enabled = true;
            StopCoroutine(ccFade);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Vector2 directionToCollision = (Vector2)collision.transform.position - (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, directionToCollision, directionToCollision.magnitude, groundLayerMask);

        if (hit.collider == null)
        {
            PlayerManager.Instance.seen = true;
            eAI.UpdatePlayerPosition(collision);
            cc.enabled = true;
        }
        else
        {
            PlayerManager.Instance.seen = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerManager.Instance.seen = false;
        eAI.PlayerLost();
        ccFade = StartCoroutine(ccFadeStart());
    }

    IEnumerator ccFadeStart()
    {
        yield return new WaitForSeconds(7f);
        cc.enabled = false;
    }
}
