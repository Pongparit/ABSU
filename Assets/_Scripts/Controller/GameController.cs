using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

	/**
	 *
	 * BEWARE
	 *ID start from 1
	 *Index Start from 0
	 *
	 *Camera Changing not working right now. fuck me !
	**/

	//This var send from GameManager**
	Text countOfPlayer ;
	// GameObject dropdown = GameObject.Find ("Dropdown");
	string chosenValue;
	int playerCount ;

	List<Player> players = new List<Player>();
	List<Field> field  = new List<Field>();
	public RandomNum[] rans = new RandomNum[12] ;

	public Camera mainCamera;


	private int  currentTurn ;
	private int currentPlayer = 0;
	public int turn ; //number of round for this game.
	public int startMoney ;

	public float moveTime ; // in sec
	private float inverseMoveTime;

	bool isGameOver = false;
	bool isGameMoving = false;

	int boardLength ;



	// Use this for initialization
	void Start () {
		countOfPlayer = GameObject.Find("PlayerCount").GetComponent<Text>();
		chosenValue = countOfPlayer.text;
		playerCount = int.Parse(chosenValue);
		StartCoroutine(setUp());
	}




	// Update is called once per frame
	void Update () {
		//Check Game State Section




		//demo moving
		if (Input.GetKeyDown (KeyCode.Space) && !isGameMoving) {
			StartCoroutine( playerTurn ());
		}

		Debug.Log ("I am curious");


	}

	void FixedUpdate(){

	}


	IEnumerator playerTurn(){
		isGameMoving = true;

		switchCamera (currentPlayer, true);
		//Move player to Center of the cell
		yield  return StartCoroutine(aTob(players[currentPlayer], field [players[currentPlayer].FieldId-1].transform.position));
		//Demo replace Dice  not live yet
		int diceNum = Random.Range(1,6);
		Debug.Log ("Dice num :" + diceNum);
		// Move Player
		yield return Move(currentPlayer, diceNum);



		// All Action are finished
		// Move player back to his own Pos
		yield  return StartCoroutine(aTob(players[currentPlayer], field [players[currentPlayer].FieldId-1].trans[currentPlayer].position));
		//Change back to Main Cam
		switchCamera (currentPlayer, false);

		currentPlayer = (currentPlayer + 1) %4 ;
		currentTurn++;
		isGameMoving = false;

	}

	void movePlayer(int currentPlayer, int diceNum){
		//move to target position
		StartCoroutine(Move(currentPlayer,diceNum));
		//Check event
	}

	protected IEnumerator Move(int currentPlayer, int diceNum){
		//Field Index not Field Id (if Id = index +1)
		int currentField = players[currentPlayer].FieldId;
		Debug.Log("Current Player"+ players[currentPlayer].PlayerName+"\n"+
			"Before Move :\n" +
			"Current Field Id : "+currentField);

		yield return new WaitForSeconds (0.3f);
		//Need to disable all player while doing this action ** Not implemented

		players [currentPlayer].stateAnimate = "walk";
		//Make player move 1 slot per times
		for (int i = 0; i < diceNum; i++) {

			//sound and animation implement here when move

			yield return StartCoroutine(aTob(players[currentPlayer], field [currentField].transform.position));
			currentField = (currentField + 1 ) % boardLength;

			if (currentField == 1 || currentField == 7 || currentField == 13 || currentField == 19 ) {
				players [currentPlayer].isRotate = true ;
			}

			// Check current that player stand Field here
			// yield return checkField(currentField-1);

			//wait for 0.3 sec then move to nextPos
			yield return new WaitForSeconds (0.3f);
		}
		players [currentPlayer].FieldId = currentField;
		isGameMoving = false;
		players [currentPlayer].stateAnimate = "idle";
		Debug.Log ("Current Player"+ players[currentPlayer].PlayerName+"\n"+
			"After Move :\n" +
			" Current Field Id : "+currentField);
	}

	IEnumerator aTob (Player player,Vector3 nextPos){

		float stepSpeed = inverseMoveTime * Time.deltaTime;
		float sqrRemainingDistance = (player.transform.position - nextPos ).sqrMagnitude;
		while (sqrRemainingDistance > float.Epsilon) {
			Vector3 newPos = Vector3.MoveTowards (player.transform.position, nextPos, stepSpeed);
			player.transform.position  =  newPos;
			//Calculate remaining distance after moving
			sqrRemainingDistance = (player.transform.position - nextPos).sqrMagnitude;
			yield return null;
		}



	}

	// IEnumerator checkField(int currentField){
	// 	// Field field = Fields[currentField];


	// }

	//Set up the Game .
	IEnumerator setUp(){
		players.Clear();
		field.Clear ();
		inverseMoveTime = 1f / moveTime;

		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Fields")) {
			field.Add (g.GetComponent<Field> ());
			yield return null;
		}
		Debug.Log (field.Count);
		field.Sort ((a, b) => a.ID.CompareTo (b.ID));

		boardLength = field.Count;



		for (int i = 0; i < playerCount; i++) {
			GameObject player = (GameObject) Instantiate (Resources.Load("Prefabs/Player/Player_model")) ;
			player.tag = "Players";
			player.name = "_Player " + (i+1); // Game ObjName

			players.Add (player.GetComponent<Player>());
			players [i].ID = (i+1);
			players [i].Money = startMoney;
			players [i].FieldId = 1;
			players [i].playerCamera.enabled = false;

			//Move All player to Start Location
			players [i].transform.position = field [0].trans [i].position;
			Debug.Log ("ee");
			yield return null ;
		}

		// Set who start game // Couruteen needed
//		currentPlayer = Random.Range (0, playerCount);//Range 0 - (playerC -1)
//		int tempPlayer = currentPlayer;
//		for (int tempCount = playerCount; tempCount < players.Count; tempCount--) {
//
//		}

	}
	/*Fucntion that switch Camera
	 *
	 * currentPlayer = index of current player
	 * mode
	 *
	 *
	 * */
	private void switchCamera (int currentPlayer,bool mode){
		Debug.Log ("Changing Camera to Player"+ (currentPlayer+1)+"Mode"+mode);
		mainCamera.enabled = !mode;
		players [currentPlayer].playerCamera.enabled = mode;

	}
}
