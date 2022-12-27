using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]

public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager;

    private Camera cam;

    private Vector3 mousePos;

    private TrailRenderer trail;

    private BoxCollider col;

    private bool swiping = false;

    public AudioSource sliceSound;

    void Awake() // метод инициализирует приватные переменные, похож на метод start
    {
        cam = Camera.main;
        trail = GetComponent<TrailRenderer>();
        col = GetComponent<BoxCollider>();

        trail.enabled = false;
        col.enabled = false;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
       if(gameManager.isGameActive)
        {
            if(Input.GetMouseButtonDown(0))
            {
                swiping = true;
                UpdateComponents();
            }
            else if(Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }

            if(swiping)
            {
                UpdateMousePosition();
            }
        }
    }

    void UpdateMousePosition()
    {
        Vector3 newPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        //Преобразует позицию мыши на экране в мировую позицию. 10f потому что  камера имеет положение -10f по Z
        mousePos = newPos;
        transform.position = mousePos;
    }

    void UpdateComponents()
    {
        trail.enabled = swiping;
        col.enabled = swiping;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Target>())
        {
            //Destroy the target
            collision.gameObject.GetComponent<Target>().DestroyTarget();
            sliceSound.Play();
        }
    }
}
