using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public Rigidbody rb;
    public float startSpeed = 40f;

    private Transform _arrow;

    private bool _ballMoving;

    private Vector3 _ballStartPosition;

    private List<GameObject> _pins = new();

    private readonly Dictionary<GameObject, Vector3> _pinsDefaultPositions = new();
    private readonly Dictionary<GameObject, Quaternion> _pinsDefaultRotations = new();

    public int Point;

    public int _currentAttempt = 0;
    private const int MaxAttempts = 5;


    [SerializeField] private Animator cameraAnim;
    private TextMeshProUGUI feedBack;

    public AudioSource AudioSource;

    private void Start()
    {
        _currentAttempt++;
        _arrow = GameObject.FindGameObjectWithTag("Arrow").transform;

        rb = GetComponent<Rigidbody>();

        _ballStartPosition = transform.position;

        _pins = GameObject.FindGameObjectsWithTag("Pin").ToList();

        foreach (var pin in _pins)
        {
            _pinsDefaultPositions[pin] = pin.transform.position;
            _pinsDefaultRotations[pin] = pin.transform.rotation;
        }

        feedBack = GameObject.FindGameObjectWithTag("FeedBack").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (_ballMoving)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        cameraAnim.SetTrigger("Go");
        cameraAnim.SetFloat("CameraSpeed", _arrow.transform.localScale.z);

        _ballMoving = true;
        _arrow.gameObject.SetActive(false);
        rb.isKinematic = false;

        Vector3 forceVector = _arrow.forward * (startSpeed * _arrow.transform.localScale.z);
        Vector3 forcePosition = transform.position + (transform.right * 0.5f);

        rb.AddForceAtPosition(forceVector, forcePosition, ForceMode.Impulse);
        
        AudioSource.Play();

        yield return new WaitForSecondsRealtime(5);

        _ballMoving = false;

        _currentAttempt++;

        // بررسی وضعیت بازی
        if (Point>= 10)
        {
            Victory();
        }
        else if (_currentAttempt == MaxAttempts)
        {
            Defeat();
        }
        else
        {
            ResetPositions(); 
        }
    }

    private void ResetPositions()
    {
        // ریست توپ
        rb.isKinematic = true;
        transform.position = _ballStartPosition;
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;



        cameraAnim.SetTrigger("GoBack");


        // فعال‌سازی دوباره فلش
        _arrow.gameObject.SetActive(true);
    }

    private void Victory()
    {
        feedBack.text = "Victory! You knocked down 10 pins!";
        feedBack.GetComponent<Animator>().SetTrigger("Show");

        StartCoroutine(RestartGameAfterDelay());
        // در اینجا می‌توانید کد اضافی برای پیروزی اضافه کنید.
    }


    public void Check_Level()
    {
        if(SceneManager.GetActiveScene().name == "Level 1")
        {
            SceneManager.LoadScene("Level 2");
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }

    private void Defeat()
    {
        feedBack.text = "Defeat! You didn't knock down 10 pins!";
        feedBack.GetComponent<Animator>().SetTrigger("Show");
       
        StartCoroutine(RestartGameAfterDelay());
        // در اینجا می‌توانید کد اضافی برای شکست اضافه کنید.
    }

    private IEnumerator RestartGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(5);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



    public void Falen_pin()
    {
        Point += 1;

        if (Point >= 10)
        {
            Victory();
        }
        else if (_currentAttempt == MaxAttempts)
        {
            Defeat();
        }
    }
}
