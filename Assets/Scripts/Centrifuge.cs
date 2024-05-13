using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Centrifuge : MonoBehaviour
{
	[SerializeField] private Transform rotate;
	private float _rotationSpeed;
	[SerializeField] private Transform cap;
	[SerializeField] private TMP_Text label;
	[SerializeField] private Image progressBar;
	[SerializeField] private float rotationTime;
	private TubeSlot[] _centrifugeSlots;
	private bool _isSeparating;

	private void Awake()
	{
		StartCoroutine(SwitchCap(true));
		_centrifugeSlots = GetComponentsInChildren<TubeSlot>();
	}

	private void Update()
	{
		label.text = _isSeparating ? "Сепарация..." : "Готов";
		if (_isSeparating)
		{
			progressBar.fillAmount += Time.deltaTime / rotationTime;
			_rotationSpeed += 1000 * Time.deltaTime;
		}
		else
		{
			progressBar.fillAmount -= Time.deltaTime * 2;
			_rotationSpeed -= 1000 * Time.deltaTime;
		}

		_rotationSpeed = Mathf.Clamp(_rotationSpeed, 0, 1000);
		rotate.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
	}

	private void OnCollisionEnter(Collision other)
	{
		if (!other.gameObject.CompareTag("BloodTube") || _isSeparating) return;
		
		Debug.Log("Sample in centrifuge found");
		foreach (var centrifugeSlot in _centrifugeSlots)
		{
			if (centrifugeSlot.BloodSample is not null) continue;
			
			centrifugeSlot.BloodSample = other.gameObject.GetComponent<BloodSample>();
			break;
		}
	}

	public void StartSeparating()
	{
		if (_isSeparating) return;
		
		StartCoroutine(SeparateSamples());
	}

	private IEnumerator SeparateSamples()
	{
		_isSeparating = true;
		StartCoroutine(SwitchCap(false));

		yield return new WaitForSecondsRealtime(rotationTime);
		StartCoroutine(SwitchCap(true));
		
		foreach (var centrifugeSlot in _centrifugeSlots)
		{
			if (centrifugeSlot.BloodSample is not null)
			{
				centrifugeSlot.BloodSample.isSeparated = true;
			}
		}
		
		Debug.Log("Samples separated");
		_isSeparating = false;
	}

	private IEnumerator SwitchCap(bool isOpen)
	{
		var targetAngle = new Vector3(isOpen ? 270 : 359, 0 , 0);
		var t = 0.0f;
		while (Vector3.Distance(cap.localEulerAngles, targetAngle) > 0.1f)
		{
			cap.localEulerAngles = Vector3.Lerp(cap.localEulerAngles, targetAngle, t);
			yield return new WaitForEndOfFrame();
			t += Time.deltaTime * 0.5f;
		}
		cap.localEulerAngles = targetAngle;
	}
}