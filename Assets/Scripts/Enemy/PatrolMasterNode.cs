using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMasterNode : MonoBehaviour
{
    [SerializeField] public Transform[] PatrolPath;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow; 

        if (PatrolPath != null && PatrolPath.Length > 1)
        {
            for (int i = 0; i < PatrolPath.Length - 1; i++)
            {
                Gizmos.DrawLine(PatrolPath[i].position, PatrolPath[i + 1].position);
            }
        }
    }
}
