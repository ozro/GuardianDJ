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
            GooseAttack goose = hit.GetComponent<GooseAttack>();
            if (!goose.immuneToLightning)
            {
                Destroy(hit.gameObject);
                GameObject newObj = Instantiate(hit.GetComponent<GooseAttack>().deathRattle);
                newObj.transform.position = hit.transform.position;
                newObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            }
        }
    }
}
