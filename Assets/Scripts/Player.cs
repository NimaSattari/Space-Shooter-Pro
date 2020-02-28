using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isPlayer1 = false;
    public bool isPlayer2 = false;
    [SerializeField] private float Speed;
    [SerializeField] private float SpeedMulti;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject tripleShotPrefab;
    [SerializeField] private float FireRate = 0.5f;
    [SerializeField] private int Lives = 3;
    private float CanFire = -1f;
    private SpawnManager SpawnManager;
    [SerializeField] private bool isTripleShotActive = false;
    [SerializeField] private float TripleShotPowerDownRoutineTime = 5f;
    [SerializeField] private bool isSpeedUpActive = false;
    [SerializeField] private float SpeedUpPowerDownRoutineTime = 5f;
    [SerializeField] private bool isShieldActive = false;
    [SerializeField] private float ShieldPowerDownRoutineTime = 5f;
    [SerializeField] private GameObject Shield;
    [SerializeField] private int Score;
    private UIManager UIManager;
    [SerializeField] private GameObject REngine, LEngine;
    [SerializeField] AudioClip LaserClip;
    AudioSource audioSource;

    void Start()
    {
        UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        SpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        audioSource = GetComponent<AudioSource>();
        if(SpawnManager == null)
        {
            Debug.LogError("SpawnManager is NULL");
        }
        if (UIManager == null)
        {
            Debug.LogError("UIManager is NULL");
        }
        if (audioSource == null)
        {
            Debug.LogError("AudioSource.Player is NULL");
        }
        else
        {
            audioSource.clip = LaserClip;
        }
    }

    void Update()
    {
        if (isPlayer1)
        {
            CalculateMovment();
            FireLaser();
        }
        if (isPlayer2)
        {
            CalculateMovment2();
            FireLaser2();
        }
    }

    void CalculateMovment()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 4), 0);
        transform.Translate(direction * Speed * Time.deltaTime);

        if (transform.position.x > 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x < -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }
    void CalculateMovment2()
    {
        float horizontalInput = Input.GetAxis("Horizontal2");
        float verticalInput = Input.GetAxis("Vertical2");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 4), 0);
        transform.Translate(direction * Speed * Time.deltaTime);

        if (transform.position.x > 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x < -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > CanFire)
        {
            CanFire = Time.time + FireRate;
            if (isTripleShotActive)
            {
                Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(laserPrefab, transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);
            }
            audioSource.Play();
        }
    }
    void FireLaser2()
    {
        if (Input.GetKey(KeyCode.RightControl) && Time.time > CanFire)
        {
            CanFire = Time.time + FireRate;
            if (isTripleShotActive)
            {
                Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(laserPrefab, transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);
            }
            audioSource.Play();
        }
    }

    public void Damage()
    {
        if (isShieldActive)
        {
            isShieldActive = false;
            Shield.SetActive(false);
            return;
        }
        Lives--;
        if(Lives == 2)
        {
            LEngine.SetActive(true);
        }
        if (Lives == 1)
        {
            REngine.SetActive(true);
        }

        UIManager.UpdateLives(Lives);
        if(Lives <= 0)
        {
            SpawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(TripleShotPowerDownRoutineTime);
        isTripleShotActive = false;
    }
    public void SpeedUpActive()
    {
        isSpeedUpActive = true;
        Speed *= SpeedMulti;
        StartCoroutine(SpeedUpPowerDownRoutine());
    }
    IEnumerator SpeedUpPowerDownRoutine()
    {
        yield return new WaitForSeconds(SpeedUpPowerDownRoutineTime);
        Speed /= SpeedMulti;
        isSpeedUpActive = false;
    }
    public void ShieldActive()
    {
        isShieldActive = true;
        Shield.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutine());
    }
    IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(ShieldPowerDownRoutineTime);
        isShieldActive = false;
        Shield.SetActive(false);
    }
    public void AddScore(int points)
    {
        Score += points;
        UIManager.UpdateScore(Score);
    }
}
