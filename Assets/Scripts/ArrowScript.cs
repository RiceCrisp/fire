using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

	private Rigidbody rb;
	private FlammableScript flammable;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		flammable = GetComponentInChildren(typeof(FlammableScript)) as FlammableScript;
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Player"));
		// transform.rotation = Quaternion.LookRotation(rb.velocity);
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (rb && !rb.isKinematic) {
			transform.rotation = Quaternion.LookRotation(rb.velocity);
		}
	}

	public void Fire(float arrowSpeed) {
		rb.isKinematic = false;
		rb.velocity = transform.forward * arrowSpeed;
		transform.rotation = Quaternion.LookRotation(rb.velocity);
		flammable.enabled = true;
	}

	private void OnCollisionEnter(Collision col) {
		rb.isKinematic = true;
		transform.parent = col.transform;
		Destroy(rb);
	}

}
