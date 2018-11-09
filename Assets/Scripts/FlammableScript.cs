using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammableScript : MonoBehaviour {

	// private Rigidbody rb;
	private ParticleSystem ps;
	public bool lit = false;

	// Use this for initialization
	void Start () {
		// rb = GetComponent<Rigidbody>();
		ps = GetComponent<ParticleSystem>();
		if (lit) {
			ps.Play();
		}
	}

	// Update is called once per frame
	void Update () {

	}

	private void OnTriggerEnter(Collider col) {
		Debug.Log(col.gameObject.tag);
		Debug.Log(col.gameObject.GetComponent<FlammableScript>());
		if (col.gameObject.tag == "Fire" && col.gameObject.GetComponent<FlammableScript>().lit) {
			Ignite();
		}
	}

	private void Ignite() {
		lit = true;
		ps.Play();
	}

	private void Douse() {
		lit = false;
		ps.Stop();
	}

}
