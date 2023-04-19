
using SurvivalWizard.Base;
using System.Collections.Generic;
using UnityEngine;

public static class FindNearestTarget
{
    public static Transform GetNearestTarget(Transform startedPoint, List<Entity> targets)
    {
        Transform nearestTarget = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = startedPoint.position;

        foreach (var target in targets)
        {
            float distance = Vector3.Distance(target.transform.position, currentPosition);
            if (distance < minDistance)
            {
                nearestTarget = target.transform;
                minDistance = distance;
            }
        }

        return nearestTarget;
    }

    public static List<Entity> GetVisibleTargets(Transform position, List<Entity> targets, int raycastLayerMask = ~0)
    {
        List<Entity> visibleTargets = new List<Entity>();
        foreach (var target in targets)
        {
            if(target == null)
            {
                continue;
            }
            RaycastHit hit;
            if (Physics.Raycast(position.position, target.transform.position - position.position, out hit, Mathf.Infinity, raycastLayerMask))
            {
                
                if (hit.collider.gameObject == target.gameObject)
                {
                    visibleTargets.Add(target);
                }
            }
        }
        return visibleTargets;
    }
}
