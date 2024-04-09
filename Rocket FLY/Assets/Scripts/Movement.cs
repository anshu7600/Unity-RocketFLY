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
    [SerializeField] ParticleSystem rightBooster;

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
        else 
        {
            mainBooster.Stop();
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey("left"))
        {
            leftBooster.Stop();
            if (!rightBooster.isPlaying)
            {
                rightBooster.Play();   
            }
            ApplyRotation(rotationSpeed);
            
        }        
        else if (Input.GetKey("right"))
        {
                        rightBooster.Stop();
            if (!leftBooster.isPlaying)
            {
                leftBooster.Play();
            }
            ApplyRotation(-rotationSpeed);
        }
    }

    public void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame  * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so physics system can take over
    }

}
