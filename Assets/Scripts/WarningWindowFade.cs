using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class WarningWindowFade : MonoBehaviour
{
	[SerializeField] private float delayBeforeFade = 1;
	private const float FadeTime = 1;
	private CanvasGroup _canvasGroup;
	private Coroutine _fadeCoroutine;

	private void Awake()
	{
		_canvasGroup = GetComponent<CanvasGroup>();
	}
	
	private void OnEnable()
	{
		if (_fadeCoroutine is not null)
		{
			StopCoroutine(_fadeCoroutine);
		}

		_fadeCoroutine = StartCoroutine(Fade());
	}

	private IEnumerator Fade()
	{
		_canvasGroup.alpha = 1;

		yield return new WaitForSeconds(delayBeforeFade);
		while (_canvasGroup.alpha > 0)
		{
			_canvasGroup.alpha -= 1 / FadeTime * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		_fadeCoroutine = null;
		gameObject.SetActive(false);
	}
}
