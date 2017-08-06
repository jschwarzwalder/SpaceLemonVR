﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireEnd : MonoBehaviour 
{
	private Rigidbody thisRB;
	private SwitchboardLights switchBoard;
	private AudioSource aSource;

	public int ID;

	[SerializeField] private WireSocket startingSocket;
	[SerializeField] private float audioVol;
	[SerializeField] private List<AudioClip> pluginSounds = new List<AudioClip>();
	[SerializeField] private List<AudioClip> plugoutSounds = new List<AudioClip>();

	[HideInInspector] public WireSocket currentSocket;

	void Start()
	{
		thisRB = GetComponent<Rigidbody> ();
		switchBoard = FindObjectOfType<SwitchboardLights> ();
		aSource = GetComponent<AudioSource> ();
		AttachToSocket (startingSocket);
	}

	void OnTriggerStay(Collider hit)
	{
		if (hit.GetComponent<WireSocket> () && null == currentSocket && thisRB.useGravity) 
			AttachToSocket (hit.GetComponent<WireSocket>());
	}

	void OnTriggerExit(Collider hit)
	{
		if (hit.GetComponent<WireSocket> () && null != currentSocket) 
			DetachFromSocket ();
	}

	private void AttachToSocket(WireSocket socket)
	{
		//don't plug into full slot
		if (null != socket.currentWire)
			return;

		thisRB.isKinematic = true;
		currentSocket = socket;
		currentSocket.currentWire = this;
		transform.position = socket.transform.position;
		switchBoard.UpdateSwitchboardLights ();
		//plugin sound
		//AudioSource.PlayClipAtPoint(pluginSounds[Random.Range(0,pluginSounds.Count)], transform.position, audioVol);
		aSource.PlayOneShot (pluginSounds [Random.Range (0, pluginSounds.Count)], audioVol);
	}

	public void DetachFromSocket()
	{
		if (null != currentSocket) 
		{
			currentSocket.currentWire = null;
			switchBoard.UpdateSwitchboardLights ();
			currentSocket = null;
			//plugout sound
			//AudioSource.PlayClipAtPoint(plugoutSounds[Random.Range(0,plugoutSounds.Count)], transform.position, audioVol);
			aSource.PlayOneShot (plugoutSounds [Random.Range (0, plugoutSounds.Count)], audioVol);
		}
	}
}