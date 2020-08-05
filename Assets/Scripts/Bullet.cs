using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed = 5;
    [SerializeField]
    float lifeTime = 0.5f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<MCController>() == null)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
