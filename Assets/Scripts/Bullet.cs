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
            GooseAttack goose = other.GetComponent<GooseAttack>();
            if(goose != null)
            {
                GameObject newObj = Instantiate(goose.deathRattle);
                newObj.transform.position = goose.transform.position;
                newObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                Destroy(newObj, 1);
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
