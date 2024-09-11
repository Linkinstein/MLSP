using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainXScale : MonoBehaviour
{
    private Transform parentTransform;

    void Start()
    {
        // Get the parent transform
        parentTransform = transform.parent;

        if (parentTransform == null)
        {
            Debug.LogWarning("No parent transform found. This GameObject will not adjust scale.");
            return;
        }

        // Adjust the scale to counteract the parent's negative scale
        AdjustScale();
    }

    void Update()
    {
        // Continuously adjust the scale in case the parent's scale changes during runtime
        AdjustScale();
    }

    private void AdjustScale()
    {
        if (parentTransform == null) return;

        // Get the parent's scale
        Vector3 parentScale = parentTransform.localScale;

        // Adjust the child's scale based on the parent's scale
        Vector3 adjustedScale = transform.localScale;

        // If the parent scale is negative, invert the child's scale accordingly
        adjustedScale.x = Mathf.Abs(adjustedScale.x) * Mathf.Sign(parentScale.x);

        transform.localScale = adjustedScale;
    }
}

