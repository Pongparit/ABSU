using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SelectCharacterOptions : MonoBehaviour {

  public Dropdown dropdown1;
	public Dropdown dropdown2;
	public Dropdown dropdown3;
	public Dropdown dropdown4;
	public Text pt1 ;
	public Text pt2 ;
	public Text pt3 ;
	public Text pt4 ;
	public Text countOfPlayer;

  void Start() {
		countOfPlayer = GameObject.Find("PlayerCount").GetComponent<Text>();
  }

  void Update() {
    countOfPlayer = GameObject.Find("PlayerCount").GetComponent<Text>();

    if(countOfPlayer.text =="4") {

			dropdown4.enabled = true;
			pt4.enabled = true;
      pt3.enabled = true;
			pt2.enabled = true;
      pt1.enabled = true;
      dropdown3.enabled = true;
      dropdown2.enabled = true;
      dropdown1.enabled = true;
		}

		else if(countOfPlayer.text =="3") {

			dropdown4.enabled = false;
			pt4.enabled = false;
      pt3.enabled = true;
			pt2.enabled = true;
      pt1.enabled = true;
      dropdown3.enabled = true;
      dropdown2.enabled = true;
      dropdown1.enabled = true;
      dropdown4.gameObject.SetActive(false);
      pt4.gameObject.SetActive(false);
		}
		else if (countOfPlayer.text == "2" || countOfPlayer.text == "") {
			dropdown4.enabled = false;
			dropdown3.enabled = false;
      dropdown2.enabled = true;
      dropdown1.enabled = true;
			pt4.enabled = false;
			pt3.enabled = false;
      pt2.enabled = true;
      pt1.enabled = true;
      dropdown4.gameObject.SetActive(false);
      dropdown3.gameObject.SetActive(false);
      pt4.gameObject.SetActive(false);
      pt3.gameObject.SetActive(false);
		}
		else {
			dropdown4.enabled = false;
			dropdown3.enabled = false;
			dropdown2.enabled = false;
      dropdown1.enabled = true;
			pt4.enabled = false;
			pt3.enabled = false;
			pt2.enabled = false;
      pt1.enabled = true;
      dropdown4.gameObject.SetActive(false);
      dropdown3.gameObject.SetActive(false);
      dropdown2.gameObject.SetActive(false);
      pt4.gameObject.SetActive(false);
      pt3.gameObject.SetActive(false);
      pt2.gameObject.SetActive(false);
		}
  }
}
