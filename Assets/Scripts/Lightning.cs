using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField]
    float strikeRadius;
    [SerializeField]
    string[] layers;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, strikeRadius);
    }
    public void Strike()
    {
        Destroy(gameObject);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, strikeRadius, LayerMask.GetMask(layers));
        foreach(Collider hit in hitColliders)
        {
            Destroy(hit.gameObject);
        }
    }
}
