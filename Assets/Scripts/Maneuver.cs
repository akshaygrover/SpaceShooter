using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maneuver : MonoBehaviour {
	private Rigidbody rb;
	public Boundary boundary;
	public Vector2 startWait;
	public Vector2 maneuverWait;
	public Vector2 maneuverTime;
	
	private float targetManeuver;
	public float dodge;
	public float smoothing;
	public float tilt;
	private float currentSpeed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		StartCoroutine(Evade());
		currentSpeed = rb.velocity.z;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator Evade(){
		yield return new WaitForSeconds (Random.Range(startWait.x,startWait.y));
		while(true){
			targetManeuver = Random.Range(1,dodge) * -Mathf.Sign(transform.position.x);
			yield return new WaitForSeconds(Random.Range(maneuverTime.x,maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds(Random.Range(maneuverWait.x,maneuverWait.y));
		}

	}
	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		float newManeuver = Mathf.MoveTowards(rb.velocity.x,targetManeuver,Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver,0.0f,currentSpeed);
		rb.position = new Vector3(
			Mathf.Clamp(rb.position.x,boundary.xMin,boundary.xMax),
			0.0f,
			Mathf.Clamp(rb.position.z,boundary.zMin,boundary.zMax)

		);
		rb.rotation = Quaternion.Euler(0.0f,0.0f,rb.velocity.x * -tilt);

	}
}
