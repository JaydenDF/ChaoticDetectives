using System;
using System.Collections.Generic;
using UnityEngine;

namespace CursorSearcher{
public class ConeCollidersHandler : MonoBehaviour
{
    [System.Serializable]
    private struct ConeCollider
    {
        public Collider2D cone;
        public Direction direction;
    }

    [SerializeField] private ConeCollider[] _coneColliders;

    private void Start()
    {
        SetCollidersActive(false);
    }

    public Collider2D[] CollidersInDirection(Direction direction)
    {
        SetCollidersActive(true);

        Collider2D[] colliders = new Collider2D[0];
        foreach (ConeCollider coneCollider in _coneColliders)
        {
            if (coneCollider.direction == direction)
            {
                coneCollider.cone.OverlapCollider(new ContactFilter2D(), colliders);
                return colliders;
            }
        }

        SetCollidersActive(false);
        return null;
    }

    public Dictionary<Direction, Collider2D[]> GetCollidersInAllDirections()
    {
        SetCollidersActive(true);
        Dictionary<Direction, Collider2D[]> colliders = new Dictionary<Direction, Collider2D[]>();
        foreach (ConeCollider coneCollider in _coneColliders)
        {
            List<Collider2D> collidersInDirection = new List<Collider2D>();
            Physics2D.OverlapCollider(coneCollider.cone, new ContactFilter2D(), collidersInDirection);
            colliders.Add(coneCollider.direction, collidersInDirection.ToArray());
        }
        SetCollidersActive(false);
        return colliders;
    }

    public Dictionary<Direction, Collider2D[]> GetNearestColliderPerDirection()
    {
        Dictionary<Direction, Collider2D[]> colliders = GetCollidersInAllDirections();
        Dictionary<Direction, Collider2D[]> nearestColliders = new Dictionary<Direction, Collider2D[]>();

        foreach (KeyValuePair<Direction, Collider2D[]> directionColliders in colliders)
        {
            Collider2D nearestCollider = null;
            float nearestDistance = float.MaxValue;
            foreach (Collider2D collider in directionColliders.Value)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestCollider = collider;
                }
            }
            nearestColliders.Add(directionColliders.Key, new Collider2D[] { nearestCollider });
        }

        return nearestColliders;
    }

    public Dictionary<Direction, Collider2D> GetNearestColliderPerDirectionOfComponent<T>(GameObject[] gameObjectsToIgnore = null) where T : Component
    {
        Dictionary<Direction, Collider2D[]> colliders = GetCollidersInAllDirections();
        Dictionary<Direction, Collider2D> nearestColliders = new Dictionary<Direction, Collider2D>();

        foreach (KeyValuePair<Direction, Collider2D[]> directionColliders in colliders)
        {
            Collider2D nearestCollider = null;
            float nearestDistance = float.MaxValue;
            foreach (Collider2D collider in directionColliders.Value)
            {
                bool same = false;

                if (gameObjectsToIgnore != null && gameObjectsToIgnore.Length > 0)
                {
                    foreach (GameObject gobject in gameObjectsToIgnore)
                    {
                        if (collider.gameObject == gobject)
                        {
                            same = true;
                        }
                    }
                }
                if (same) continue;

                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance && collider.GetComponent<T>() != null)
                {
                    nearestDistance = distance;
                    nearestCollider = collider;
                }
            }
            nearestColliders.Add(directionColliders.Key, nearestCollider);
        }

        return nearestColliders;
    }

    private void SetCollidersActive(bool active)
    {
        foreach (ConeCollider coneCollider in _coneColliders)
        {
            coneCollider.cone.gameObject.SetActive(active);
        }
    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}}