using System;
using UnityEngine;

public class ItemDetection : MonoBehaviour
{
    [SerializeField] private float _lenght;
    [SerializeField] private LayerMask _layerMask;

    public Item SearchItem()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _lenght, _layerMask);

        if (colliders == null)
            throw new Exception("Item not");

        for (int i = 0; i < colliders.Length; i++)
        {
            var collider = colliders[i].gameObject;

            if (collider.GetComponent<Item>() == null)
                continue;

            Debug.Log("Work");

            Destroy(collider, 0.1f);
            return collider.GetComponent<Item>();
        }

        return null;
    }
}
