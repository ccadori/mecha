using UnityEngine;
using System.Collections;

public class GearPropellant : Gear {

	public float force;
	public ParticleSystem particle; 

	internal virtual void FixedUpdate(){
		if (active) {
			body.AddForce (transform.up * force * Time.deltaTime, ForceMode2D.Force);
		}
	}

	public override void SetInGame ()
	{
		base.SetInGame ();
		particle.gameObject.SetActive (true);
	}

	internal override void Active (bool state)
	{
		if (state) {
			particle.enableEmission = true;
		} else {
			particle.enableEmission = false;
		}
		base.Active (state);
	}
}
