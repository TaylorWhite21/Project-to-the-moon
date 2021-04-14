using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //todo Fix left and right booster audio

    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationalThrust = 100f;
    [SerializeField] AudioClip MainThrustersAudio;
    [SerializeField] AudioClip LeftThrustersAudio;
    [SerializeField] AudioClip RightThrustersAudio;



    [SerializeField] ParticleSystem MainThrusterEngine;
    [SerializeField] ParticleSystem LeftThrusterEngine;
    [SerializeField] ParticleSystem RightThrusterEngine;

    Rigidbody myRigidbody;
    AudioSource rocketAudio;
    AudioSource leftRocketAudio;
    AudioSource rightRocketAudio;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        rocketAudio = GetComponent<AudioSource>();
        leftRocketAudio = GetComponent<AudioSource>();
        rightRocketAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
       if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }

        else
        {
            StopThrust();
        }

    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }

        else
        {
            StopRotation();
        }
    }


    void StartThrust()
    {
        myRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!rocketAudio.isPlaying)
        {
            rocketAudio.PlayOneShot(MainThrustersAudio);
        }
        if (!MainThrusterEngine.isPlaying)
        {
            MainThrusterEngine.Play();
        }
    }

    void StopThrust()
    {
        rocketAudio.Stop();
        MainThrusterEngine.Stop();
    }



    void RotateLeft()
    {
        ApplyRotation(rotationalThrust);
        if (!RightThrusterEngine.isPlaying)
        {
            RightThrusterEngine.Play();
        }
        //if (!rightRocketAudio.isPlaying)
        //{
        //rightRocketAudio.PlayOneShot(RightThrustersAudio);
        //}
    }

    void RotateRight()
    {
        ApplyRotation(-rotationalThrust);
        if (!LeftThrusterEngine.isPlaying)
        {
            LeftThrusterEngine.Play();
        }
        //if (!leftRocketAudio.isPlaying)
        //{
            //leftRocketAudio.PlayOneShot(LeftThrustersAudio);
        //}
    }

    void StopRotation()
    {
        RightThrusterEngine.Stop();
        LeftThrusterEngine.Stop();
        //leftRocketAudio.Stop();
        //rightRocketAudio.Stop();

    }

    void ApplyRotation(float rotationThisFrame)
    {
        myRigidbody.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        myRigidbody.freezeRotation = false; //unfreezing rotation so physics system can take over
    }

}
