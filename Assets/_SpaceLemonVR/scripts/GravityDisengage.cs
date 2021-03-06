﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityDisengage : MonoBehaviour
{
    [SerializeField] private  GameObject driveBase;
    private Vector3 gravitySet;

	[SerializeField] private AudioSource gravLeverSound;
	[SerializeField] private AudioSource gravHumSound;

	// Use this for initialization
	void Start ()
	{

	    gravitySet = Physics.gravity;

	}


    // Update is called once per frame
    void Update()
    {

        if (Physics.gravity.y > 0)
        {
            Physics.gravity = Vector3.zero;
        }

    }

	private IEnumerator WaitingToHum()
	{
		gravLeverSound.Play ();
		yield return new WaitForSeconds (2);
		gravHumSound.Play ();
	}

    void OnCollisionExit(Collision collisionInfo)
    {
       

        if (driveBase == collisionInfo.gameObject)
        {
            Physics.gravity = new Vector3(0, 50, 0); ;
			StartCoroutine (WaitingToHum ());
        }


    }

    private void OnCollisionEnter(Collision other)
    {
        if (driveBase == other.gameObject)
        {
            Physics.gravity = gravitySet;
			gravHumSound.Stop ();
        }
    }
}
