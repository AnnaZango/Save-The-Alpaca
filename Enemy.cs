using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy settings")]
    [SerializeField] float health = 200;
    [SerializeField] float speedShooting = 10f;
    [SerializeField] int pointsPerKilling = 43;
    
    [Header("Shooting settings")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject enemyProjectilePrefab;    

    [Header("Particles explosion")]
    [SerializeField] GameObject bones;
    [SerializeField] GameObject skull;
    [SerializeField] float durationExplosion = 1f;

    [Header("Sound")]
    [SerializeField] AudioClip clipKill;
    [SerializeField] [Range(0,1)] float volumeKill = 2f;
    [SerializeField] AudioClip clipShoot;
    [SerializeField] [Range(0,1)] float volumeShoot= 2f;    

    void Start()
    {        
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    void Update()
    {
        CountDownAndShot();
    }

    private void CountDownAndShot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {      
        AudioSource.PlayClipAtPoint(clipShoot, Camera.main.transform.position, volumeShoot);
        GameObject enemyFire = Instantiate(enemyProjectilePrefab, transform.position, Quaternion.identity) as GameObject; 
        enemyFire.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speedShooting);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);                                   
        damageDealer.Hit(); 
    }

    private void ProcessHit(DamageDealer damageDealer)
    {  
        health = health - damageDealer.GetDamageInflicted();

        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(clipKill, Camera.main.transform.position, volumeKill);
            Die();            
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject skullPrefab = Instantiate(skull, transform.position, Quaternion.identity) as GameObject;
        Destroy(skullPrefab, 0.15f);
        GameObject explosionParticles = Instantiate(bones, transform.position, transform.rotation) as GameObject;       
        Destroy(explosionParticles, durationExplosion);
        FindObjectOfType<GameSession>().AddToScore(pointsPerKilling);
    }    

}
