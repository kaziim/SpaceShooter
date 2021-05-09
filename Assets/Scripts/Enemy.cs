using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    private int currentScore = 0;
    
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
        currentScore = Int32.Parse(PlayerPrefs.GetString("currentScore"));
    }

    private void Update()
    {
        var target = new Vector2(transform.position.x, transform.position.y - 20f);
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Laser"))
        {
            var explode = (GameObject) Instantiate(explosion, collision.transform.position + (Vector3.up *1/2f), collision.transform.rotation);
            currentScore += 5;
            PlayerPrefs.SetString("currentScore",currentScore+"");
            SoundManager.PlaySound("enemyHit");
            Destroy(collision.gameObject);
        }
        
        Destroy(gameObject);
    }
    
    
}
