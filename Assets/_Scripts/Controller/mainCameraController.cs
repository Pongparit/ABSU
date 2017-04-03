using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCameraController : MonoBehaviour {
	
	public Transform focusTarget ;

	private Transform cameraStand; 

	private bool isdefault = true ; 

	public float camSpeed ; 


	public Transform topTrans;
	private float inverseMoveTime;

	private Camera mainCamera ; 

	private bool lookUp = true; 
	// Use this for initialization
	void Start () {

		inverseMoveTime = 1 / camSpeed; 

		mainCamera = GetComponent<Camera> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (isdefault) {
			transform.LookAt (focusTarget);	
		} else {
			transform.LookAt (cameraStand);	
		}
			
	}

	IEnumerator moveCameraTo (Transform trans){


		float stepSpeed = inverseMoveTime * Time.deltaTime;
		float sqrRemainingDistance = (mainCamera.transform.position - trans.position ).sqrMagnitude;
		while (sqrRemainingDistance > float.Epsilon) {
			Vector3 newPos = Vector3.MoveTowards (mainCamera.transform.position, trans.position, stepSpeed);
			mainCamera.transform.position  =  newPos;
			//Calculate remaining distance after moving 
			sqrRemainingDistance = (mainCamera.transform.position - trans.position).sqrMagnitude; 
			yield return null;
		}
	}


//	IEnumerator moveToTop(){
//
//	}
//
//	IEnumerator moveToSide(){
//
//	}
//
//
//	IEnumerator Rotate(){
//
//	}


	
}
