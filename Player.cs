using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header ("Player configuration")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f; 
    [SerializeField] int health = 500;
    [SerializeField] int maxHealth = 300;
    [SerializeField] GameObject killedPrefab;
    [SerializeField] GameObject zZzPrefab;
    [SerializeField] int totalSpitAmount = 100;
    [SerializeField] int maxSpitAmount = 100;
    
    [Header("Spit configuration")]
    [SerializeField] GameObject spitPrefab=default; 
    [SerializeField] float spitSpeed = 10f;
    [SerializeField] float firingPeriod = 0.1f;

    [Header("Audio")]
    [SerializeField] AudioClip playerKill;
    [SerializeField] [Range(0,1)] float volumeKill = 1f;
    
    Coroutine firingCoroutine;     

    float xMin;
    float xMax;
    float yMin;
    float yMax;
        
    void Start()
    {
        SetUpMovementBoundaries();        
    }

    void Update()
    {
        Move();
        Fire();        
    }

    private void Fire()
    {        
        if (Input.GetButtonDown("Fire1"))
        {
            if (totalSpitAmount > 0)
            {
                firingCoroutine = StartCoroutine(ContinuousFire());
            }            
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator ContinuousFire()
    {
        while (totalSpitAmount>0) 
        {
            GameObject spit = Instantiate(spitPrefab, new Vector2(transform.position.x, transform.position.y + padding), Quaternion.identity) as GameObject; 
            spit.GetComponent<Rigidbody2D>().velocity = new Vector2(0, spitSpeed);
            yield return new WaitForSeconds(firingPeriod);
            totalSpitAmount--;
        }      
    }

    private void SetUpMovementBoundaries()
    {
        Camera gameCamera = Camera.main;
        
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        
        var newXposition = Mathf.Clamp((transform.position.x + deltaX), xMin, xMax);        
        var newYposition = Mathf.Clamp((transform.position.y + deltaY), yMin, yMax);

        transform.position = new Vector2(newXposition, newYposition);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {        
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer) 
        {
            Debug.LogError("No DamageDealer component attached to the GameObject you collided with!");
            return; 
        } 

        ProcessHit(damageDealer);
        damageDealer.Hit();              
    }

    private void ProcessHit(DamageDealer damageDealer)
    {       
        health = health - damageDealer.GetDamageInflicted();
        totalSpitAmount = totalSpitAmount + damageDealer.GetHidration();

        if (totalSpitAmount > maxSpitAmount)
        {
            totalSpitAmount = maxSpitAmount;
        }
        if (health <= 0)
        {
            PlayerDies();            
        }
        if (health >= maxHealth)
        {
            health = maxHealth;            
        }        
    }

    private void PlayerDies()
    {        
        AudioSource.PlayClipAtPoint(playerKill, Camera.main.transform.position, volumeKill);
        Destroy(gameObject);
        GameObject explosionParticles = Instantiate(zZzPrefab, transform.position, transform.rotation) as GameObject;
        Destroy(explosionParticles, 2);
        Instantiate(killedPrefab, transform.position, Quaternion.identity);        
        FindObjectOfType<LevelSettings>().LoadGameOver();
    }

    public int GetHealth()
    {
        if (health < 0)
        {
            health = 0;
        }
        return health;
    }

    public int GetHidration()
    {
        return totalSpitAmount;
    }
    
}
