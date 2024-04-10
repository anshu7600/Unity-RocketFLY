using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationSpeed = 100;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem leftBooster1;
    [SerializeField] ParticleSystem rightBooster;
    [SerializeField] ParticleSystem rightBooster1;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {   
        rb = GetComponent<Rigidbody>();    
        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        ProcessThrust(); 
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey("up"))
        { 
            StartThrust();
        }
        else 
        {
            StopThrusting();
        }
    }

    void StartThrust()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        rb.AddRelativeForce(Vector3.up * mainThrust  * Time.deltaTime); 
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    void StopThrusting()
    {
        mainBooster.Stop();
        audioSource.Stop();        
    }

    void ProcessRotation()
    {
        if (Input.GetKey("left"))
        {
            RotateLeft();
        }        
        else if (Input.GetKey("right"))
        {
            RotateRight();
        }
        else
        {
            rightBooster.Stop();
            leftBooster.Stop();
        }
    }

    void RotateLeft()
    {
        leftBooster.Stop();
        leftBooster1.Stop();
        if (!rightBooster.isPlaying && !rightBooster1.isPlaying)
        {
            rightBooster.Play();  
            rightBooster1.Play(); 
        }
        ApplyRotation(rotationSpeed);        
    }

    void RotateRight()
    {
        rightBooster.Stop();
        rightBooster1.Stop();
        if (!leftBooster.isPlaying && !leftBooster1.isPlaying)
        {
            leftBooster.Play();
            leftBooster1.Play();
        }
        ApplyRotation(-rotationSpeed);
    }

    public void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame  * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so physics system can take over
    }

}
