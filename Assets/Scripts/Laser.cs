using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float Speed;
    private bool isEnemyLaser = false;

    void Update()
    {
        if(isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }
    void MoveUp()
    {
        transform.Translate(Vector3.up * Time.deltaTime * Speed);
        if (transform.position.y > 7f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }
    void MoveDown()
    {
        transform.Translate(Vector3.down * Time.deltaTime * Speed);
        if (transform.position.y < -7f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }
    public void AssignEnemyLaser()
    {
        isEnemyLaser = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.tag == "Player" || collision.tag == "player1" || collision.tag == "player2") && isEnemyLaser == true)
        {
            Player player = collision.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
        }
    }
}
