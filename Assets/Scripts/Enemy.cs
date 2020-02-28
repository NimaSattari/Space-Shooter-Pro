using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float Speed = 4.0f;
    private Player player;
    private Player player1;
    private Player player2;
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] private GameObject LaserPrefab;
    private float fireRate = 3.0f;
    private float CanFire = -1f;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        /*player1 = GameObject.Find("player1").GetComponent<Player>();
        player2 = GameObject.Find("player2").GetComponent<Player>();*/
        audioSource = GetComponent<AudioSource>();
        if (player == null)
        {
            Debug.Log("EnemyPlayer is NULL");
        }
        /*if (player1 == null)
        {
            Debug.Log("EnemyPlayer1 is NULL");
        }
        if (player2 == null)
        {
            Debug.Log("EnemyPlayer2 is NULL");
        }
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.Log("EnemyAnimator is NULL");
        }*/
    }

    void Update()
    {
        CalculateMovement();
        if(Time.time > CanFire)
        {
            fireRate = Random.Range(2.5f, 7.5f);
            CanFire = Time.time + fireRate;
            GameObject EnemyLaser = Instantiate(LaserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = EnemyLaser.GetComponentsInChildren<Laser>();
            for(int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }
    void CalculateMovement()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
        if (transform.position.y < -5f)
        {
            float RandomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(RandomX, 7, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" || other.tag == "player1" || other.tag == "player2")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            GetComponent<Collider2D>().enabled = false;
            animator.SetTrigger("Die");
            Speed = 0;
            audioSource.Play();
            Destroy(this.gameObject, 3f);
        }
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if(player != null)
            {
                player.AddScore(10);
            }
            GetComponent<Collider2D>().enabled = false;
            animator.SetTrigger("Die");
            Speed = 0;
            audioSource.Play();
            Destroy(this.gameObject, 3f);
        }
    }
}
