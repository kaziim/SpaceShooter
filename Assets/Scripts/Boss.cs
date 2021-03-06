using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private float timeBetweenShots;
    public float startTimeBetweenShots;

    public float health;

    public GameObject projectile;
    [SerializeField] GameObject explosion;
    
    private Transform player;
    
    public Text score;
    private int currentScore = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBetweenShots = startTimeBetweenShots;
        currentScore = Int32.Parse(PlayerPrefs.GetString("currentScore"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position,player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, 
                speed * Time.deltaTime);
        }else if (Vector2.Distance(transform.position,player.position) < stoppingDistance &&
                  Vector2.Distance(transform.position,player.position) > stoppingDistance)
        {
            transform.position = this.transform.position;
        }else if (Vector2.Distance(transform.position,player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, 
                -speed * Time.deltaTime);
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
            currentScore += 10;
            PlayerPrefs.SetString("currentScore",currentScore+"");
            SoundManager.PlaySound("enemyHit");
        }
        if (health == 0)
        {
            currentScore += 15;
            PlayerPrefs.SetString("currentScore",currentScore+"");
            var explode = (GameObject) Instantiate(explosion, collision.transform.position + (Vector3.up *1/2f), collision.transform.rotation);
            Destroy(gameObject);
        }
        
    }
}
