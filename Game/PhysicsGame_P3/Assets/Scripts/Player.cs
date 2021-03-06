﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public bool hitGoal;
	
	private Rigidbody2D rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.isKinematic = true;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Goal") hitGoal = true;
	}

	private void LateUpdate()
	{
		if (hitGoal) hitGoal = false;

		Time.timeScale = isPaused ? 0 : 1;
	}

	public bool isPaused
	{
		get { return rb.isKinematic; }
		set { rb.isKinematic = value; }
	}
}
