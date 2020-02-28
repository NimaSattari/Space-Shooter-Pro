using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float RotateSpeed = 10f;
    [SerializeField] private GameObject EXPrefab;
    private SpawnManager spawnManager;

    private void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    void Update()
    {
        transform.Rotate(Vector3.forward * RotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Laser")
        {
            Instantiate(EXPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            spawnManager.StartSpawning();
            Destroy(this.gameObject,0.25f);
        }
    }
}
