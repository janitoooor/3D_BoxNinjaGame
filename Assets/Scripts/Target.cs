using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private GameObject goodOne;

    public ParticleSystem exploisionParticle;

    private float minSpeed = 10.5f;
    private float maxSpeed = 14;
    private float maxTorque = 10;
    private float xRange = 4;
    private float spawnYpos = 0;

    private int randomPoint = 51;
    public int pointValue;
    private int lives = -1;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        MoveTarget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), spawnYpos);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private void MoveTarget()
    {
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);//придаем вращение с помощью силы и Rb
        transform.position = RandomSpawnPos();
    }
    //private void OnMouseDown()
    //{
        //if(gameManager.isGameActive && gameManager.isPauseActive == false)
       // {
           // Destroy(gameObject);
           // Instantiate(exploisionParticle, transform.position, exploisionParticle.transform.rotation);
           // gameManager.UpdateScore(pointValue);
           // if (gameObject.CompareTag("GoodOne"))
           // {
               //int randomValue = Random.Range(pointValue, randomPoint);
               // gameManager.UpdateScore(randomValue);
               // Destroy(gameObject);
               // Instantiate(exploisionParticle, transform.position, exploisionParticle.transform.rotation);
            //}
        //}
    //}

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if(!gameObject.CompareTag("Bad"))
        {
            gameManager.UpdateLives(lives);
        }
    }

    public void DestroyTarget()
    {
        if(gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(exploisionParticle, transform.position, exploisionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
            if (gameObject.CompareTag("GoodOne"))
            {
                int randomValue = Random.Range(pointValue, randomPoint);
                gameManager.UpdateScore(randomValue);
                Destroy(gameObject);
                Instantiate(exploisionParticle, transform.position, exploisionParticle.transform.rotation);
            }

            if (gameObject.CompareTag("Bad"))
            {
                gameManager.UpdateLives(lives);
                Destroy(gameObject);
                Instantiate(exploisionParticle, transform.position, exploisionParticle.transform.rotation);
            }
        }
    }
}