using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Text score;
    private int personalBest;
    
    [SerializeField] GameObject explosion;
    
    public GameObject GameOverMenuUI;

    private void Start()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("currentScore",""+0);
        personalBest = PlayerPrefs.GetInt("personalBest", 0);
    }

    void Update()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }
        
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        score.text = PlayerPrefs.GetString("currentScore","0");
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            var explode = (GameObject) Instantiate(explosion, collision.transform.position, collision.transform.rotation);
            health--;
        }

        if (health == 0)
        {
            transform.localScale = new Vector2(0, 0);
            int temp = Int32.Parse(PlayerPrefs.GetString("currentScore"));
            if (temp > personalBest)
            {
                PlayerPrefs.SetInt("personalBest",temp);
            }
            GameOverMenuUI.SetActive(true);
            Time.timeScale = 0f;

        }
        
    }
}
