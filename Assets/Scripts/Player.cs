using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    //Configs
    [SerializeField] float moveSpeed = 10f, padding = 0.5f,laserSpeed = 10f;
    [SerializeField] GameObject laserPrefab;
    private float xMin, xMax,yMin,yMax;
    private Coroutine fireCoroutine;
    [SerializeField] GameObject explosion;
    
    // Start is called before the first frame update
    void Start()
    {
        SetUpFrameBoundries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(RapidFire());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }

    }

    IEnumerator RapidFire()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position,Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = Vector2.up * laserSpeed;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Move()
    {
        
        //Move the ship
        var changeX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newxPos = Mathf.Clamp(transform.position.x,xMin,xMax) + changeX;
        var changeY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newyPos = Mathf.Clamp(transform.position.y,yMin,yMax) + changeY;
        transform.position = new Vector2(newxPos, newyPos);

        //Tilt the ship
        if (changeX != 0)
        {
            var tilt = transform.localScale.x * 199/200;
            if(transform.localScale.x >= 0.75)
                transform.localScale = new Vector2(tilt,transform.localScale.y);
        }
        else
        {
            var tilt = transform.localScale.x * 200/199;
            if(transform.localScale.x <= 1)
                transform.localScale = new Vector2(tilt,transform.localScale.y);
        }
    }
    
    private void SetUpFrameBoundries()
    {
        Camera myGameCamera = Camera.main;
        xMin = myGameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x + padding;
        xMax = myGameCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x - padding;
        yMin = myGameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).y + padding;
        yMax = myGameCamera.ViewportToWorldPoint(new Vector3(0,1,0)).y - padding;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            var explode = (GameObject) Instantiate(explosion, collision.transform.position, collision.transform.rotation);
        }
        
    }
}
