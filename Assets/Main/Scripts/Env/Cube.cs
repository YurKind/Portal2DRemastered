﻿using System.Linq;
using UnityEngine;

public class Cube : GrabbableAndThrowableObject
{
	public AudioClip collisionSound;
	
	private AudioSource audioSource;
	private GameObject environment;
	private Collider2D[] environmentColliders;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		environment = GameObject.Find("Environment");
		environmentColliders = environment.GetComponentsInChildren<Collider2D>();
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (environmentColliders.Any(col => col == other.collider))
		{
			if (!audioSource.isPlaying)
			{
				audioSource.PlayOneShot(collisionSound);
			}
		}
	}
}
