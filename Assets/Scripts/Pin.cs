using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public bool _done;
    public AudioSource AudioSource;



    private void Start()
    {
        AudioSource = GetComponentInParent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.collider.CompareTag("Ball") || collision.collider.CompareTag("Pin")) && !_done)
        {
            // بررسی زاویه پین نسبت به حالت عمودی
            float tiltAngle = Vector3.Angle(transform.up, Vector3.up);

            if (tiltAngle > 30f) // اگر زاویه بیش از 30 درجه باشد، پین افتاده است
            {
                var ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
                GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>().Falen_pin();
                AudioSource.Play();
                GameObject.FindGameObjectWithTag("Poing").GetComponent<TextMeshProUGUI>().text =
                    $" {ball.Point} PINS KNOCKED OUT ";

                _done = true; // جلوگیری از حساب شدن دوباره این پین

                Destroy(this.gameObject, 1.5f);
            }
        }
    }


}
