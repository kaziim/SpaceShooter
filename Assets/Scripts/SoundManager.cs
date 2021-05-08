using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip laserSound,enemyHitSound;
    private static AudioSource audioSrc;
    
    // Start is called before the first frame update
    void Start()
    {
        laserSound = Resources.Load<AudioClip>("laser-shot");
        enemyHitSound = Resources.Load<AudioClip>("alien-hit");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "fire":
                audioSrc.PlayOneShot(laserSound);
                break; 
            case "enemyHit":
                audioSrc.PlayOneShot(enemyHitSound);
                break;
        }
    }
}
