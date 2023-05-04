
using System.Linq;
using UnityEngine;

public static class GetCollider
{
    public static Collider GetNearestCollider(Transform startedPoint, Collider[] targets)
    {
        Collider nearestCollider = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = startedPoint.position;

        foreach (var target in targets)
        {
            float distance = Vector3.Distance(target.transform.position, currentPosition);
            if (distance < minDistance)
            {
                nearestCollider = target;
                minDistance = distance;
            }
        }
        return nearestCollider;
    }

    public static Collider GetRandomCollider(Collider[] targets)
    {
        if(targets.Length == 0)
        {
            return null;
        }
        int numberTarget = Random.Range(0, targets.Count());
        return targets[numberTarget];
    }
}
