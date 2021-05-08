using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class SpaceBoss : MonoBehaviour
{
    
    [SerializeField] GameObject explosion;
    public GameObject projectile;
    [SerializeField] private float speed = 10f;
    private int randomSpot;
    private float waitTime; 
    public float startWaitTime;
    public Transform moveSpot;
    public float minX, maxX, minY, maxY;
    public float health;
    private float timeBetweenShots;
    public float startTimeBetweenShots;
    private Transform player;
    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBetweenShots = startTimeBetweenShots;
        waitTime = startWaitTime;
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.y > 18)
        {
            var target = new Vector2(transform.position.x, transform.position.y - 3f);
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else
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
        
        if (timeBetweenShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
        
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Laser"))
        {
            Destroy(collision.gameObject);
            health--;
        }
        if (health == 0)
        {
            var explode = (GameObject) Instantiate(explosion, collision.transform.position + (Vector3.up *1/2f), collision.transform.rotation);
            Destroy(gameObject);
        }
    }
}
