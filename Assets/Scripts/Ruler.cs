using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Ruler : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    Color color;
    [SerializeField]
    float maxDist = 100;
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawRay(transform.position, Vector3.right*maxDist);
        for(int i = 0; i < maxDist; i+= 5)
        {
            Handles.Label(transform.position + Vector3.right*i, i.ToString());
            Gizmos.DrawRay(transform.position + Vector3.right * i, Vector3.up*0.5f);
        }
    }
#endif
}
