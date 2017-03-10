using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.SerializableAttribute] 
public class Boundary{
	public float xMin,xMax,zMin,zMax;
}

public class PlayerController : MonoBehaviour {
	private Rigidbody rb;
	public float speed;
	public Boundary boundary;
	public float tilt;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	

	 /// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
	if (Input.GetKeyDown("space") && Time.time >nextFire){
		nextFire = Time.time + fireRate;
		Instantiate(shot,shotSpawn.position,shotSpawn.rotation);
		GetComponent<AudioSource>().Play();
	}
	}

	 /// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal,0.0f,moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3(
			Mathf.Clamp(rb.position.x,boundary.xMin,boundary.xMax),
			0.0f,
			Mathf.Clamp(rb.position.z,boundary.zMin,boundary.zMax)
		);
		rb.rotation = Quaternion.Euler(0.0f,0.0f,rb.position.x * -tilt);
	}
}
