using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Dropper))]
public class Pipette : MonoBehaviour
{
	[SerializeField] private Material formedElements;
	[SerializeField] private Material plasma;
	public BloodClass BloodClass { get; private set; }
	private Dropper _dropper;
	private DropperContent Content
	{
		get => _dropper.content;
		set => _dropper.content = value;
	}

	private GameObject _targetTube;
	private MeshRenderer _contentMesh;
	

	private void Awake()
	{
		GetComponent<XRGrabInteractable>().activated.AddListener(Pickup);
		_contentMesh = transform.GetChild(0).GetComponent<MeshRenderer>();
		_dropper = GetComponent<Dropper>();
	}

	private void Update()
	{
		_contentMesh.gameObject.SetActive(Content != DropperContent.None);
	}

	private void OnTriggerStay(Collider other)
	{
		if (!other.CompareTag("Plasma") && !other.CompareTag("Formed Elements")) return;

		_targetTube = other.gameObject;
	}

	private void Pickup(ActivateEventArgs arg0)
	{
		if (_targetTube is null) return;
		
		_dropper.BloodClass = _targetTube.GetComponentInParent<BloodSample>().BloodClass;
		switch (_targetTube.tag)
		{
			case "Plasma":
				Content = DropperContent.Plasma;
				_contentMesh.material = plasma;
				break;
			case "Formed Elements":
				Content = DropperContent.FormedElements;
				_contentMesh.material = formedElements;
				break;
			default:
				throw new ArgumentException();
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		_targetTube = null;
	}
}