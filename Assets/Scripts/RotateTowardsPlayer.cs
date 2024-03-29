using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
	private Transform _playerPos;
	private void Awake()
	{
		_playerPos = Camera.main.transform;
	}
	private void Update()
	{
		transform.forward = -(_playerPos.transform.position - transform.position);
	}
}
