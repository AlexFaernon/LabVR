using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class Dropper : MonoBehaviour
{
	private DropperContent _content;
	private DropperContent _target;
	[FormerlySerializedAs("blood")]
	[SerializeField] private Material formedElements;
	[SerializeField] private Material plasma;
	private MeshRenderer _contentMesh;
	

	private void Awake()
	{
		GetComponent<XRGrabInteractable>().activated.AddListener(Pickup);
		_contentMesh = transform.GetChild(0).GetComponent<MeshRenderer>();
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Plasma"))
		{
			_target = DropperContent.Plasma;
		}

		if (other.CompareTag("Formed Elements"))
		{
			_target = DropperContent.FormedElements;
		}
	}

	private void Pickup(ActivateEventArgs arg0)
	{
		_content = _target;
		_contentMesh.gameObject.SetActive(_content != DropperContent.None);
		switch (_content)
		{
			case DropperContent.Plasma:
				_contentMesh.material = plasma;
				break;
			case DropperContent.FormedElements:
				_contentMesh.material = formedElements;
				break;
			case DropperContent.None:
			default:
				break;
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		_target = DropperContent.None;
	}
}