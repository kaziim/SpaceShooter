using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    
    [SerializeField] private float speed = 10f;
    private int randomSpot;
    private float waitTime; 
    public float startWaitTime;
    
    public Transform moveSpot;
    public float minX, maxX, minY, maxY;
    
   

    private void Start() 
    {
        waitTime = startWaitTime;

        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, 
                                         moveSpot.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position,moveSpot.position) < 0.2f) 
        {
            if (waitTime <= 0) {
                moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                waitTime = startWaitTime;
            }
            else {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var bumper = (GameObject) Instantiate(explosion, collision.transform.position + (Vector3.up *1/2f), collision.transform.rotation);
        Destroy(gameObject);
    }
    
    
}
