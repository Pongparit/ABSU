using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	//Default var
	private int id ;
	float money ;
	string playerName ;
	private Inventory inventory ;
	private Rigidbody rb;
	private int currentFieldId ;
	private Animator anim;
	private AnimatorStateInfo currentBaseState;
	string stateGet ;
	bool isRo;

	private bool isPastStartPoint ;

	public Camera playerCamera;


	void Start (){
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator>();


//		playerCamera = GetComponent<Camera> ();
	}

  void FixedUpdate () {
		if (stateGet == "walk"){
			Debug.Log("kuy");
		 anim.SetBool("isMove", true);
	 }
	 if (stateGet == "idle") {
		 anim.SetBool("isMove", false);
	 }

	 if (isRo == true) {
		 transform.Rotate(0, 90, 0);
		 isRo = false;
	 }


	}

	//Status var

	public string stateAnimate {
		get {
			return this.stateGet;
		}
		set {
			this.stateGet = value;
		}
	}

	public bool isRotate {
		get{
			return this.isRo;
		}

		set{
			this.isRo = value;
		}
	}

	public Rigidbody rigidBody{
		get{
			return this.rb;
		}

		set{
			this.rb = value;
		}
	}

	public int ID{
		get{
			return id;
		}
		set{
			this.id = value;
		}
	}

	public float Money {

		get {
			return this.money;
		}
		set {
			this.money = value;
		}
	}

	public string PlayerName {
		get{
			return this.playerName;
		}
		set{
			this.playerName = value;
		}
	}

	public int FieldId {
		get{
			return this.currentFieldId;
		}
		set{
			this.currentFieldId = value;
		}
	}

	public bool isPastStart {
		get{return this.isPastStartPoint ; }
		set{this.isPastStartPoint = value ;}
	}


}
