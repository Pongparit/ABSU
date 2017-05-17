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
  public Image p1img;
  public Image p2img;
  public Image p3img;
  public Image p4img;
  public Sprite player1cha;
  public Sprite player2cha;
  public Sprite player3cha;
  public Sprite player4cha;

  List<string> list = new List<string>() {"player1", "player2", "player3", "player4"};

  void Start() {
		countOfPlayer = GameObject.Find("PlayerCount").GetComponent<Text>();
  }

  void Update() {
    countOfPlayer = GameObject.Find("PlayerCount").GetComponent<Text>();

    setImg(p1img, dropdown1.value);
    setImg(p2img, dropdown2.value);
    setImg(p3img, dropdown3.value);
    setImg(p4img, dropdown4.value);


    if(countOfPlayer.text =="4") {

      dropdown4.gameObject.SetActive(true);
      dropdown3.gameObject.SetActive(true);
      dropdown2.gameObject.SetActive(true);
      dropdown1.gameObject.SetActive(true);
      pt4.gameObject.SetActive(true);
      pt3.gameObject.SetActive(true);
      pt2.gameObject.SetActive(true);
      pt1.gameObject.SetActive(true);
      p1img.gameObject.SetActive(true);
      p2img.gameObject.SetActive(true);
      p3img.gameObject.SetActive(true);
      p4img.gameObject.SetActive(true);
		}

		else if(countOfPlayer.text =="3") {

      dropdown4.gameObject.SetActive(false);
      dropdown3.gameObject.SetActive(true);
      dropdown2.gameObject.SetActive(true);
      dropdown1.gameObject.SetActive(true);
      pt4.gameObject.SetActive(false);
      pt3.gameObject.SetActive(true);
      pt2.gameObject.SetActive(true);
      pt1.gameObject.SetActive(true);
      p1img.gameObject.SetActive(true);
      p2img.gameObject.SetActive(true);
      p3img.gameObject.SetActive(true);
      p4img.gameObject.SetActive(false);
		}
		else if (countOfPlayer.text == "2" || countOfPlayer.text == "") {
      dropdown4.gameObject.SetActive(false);
      dropdown3.gameObject.SetActive(false);
      dropdown2.gameObject.SetActive(true);
      dropdown1.gameObject.SetActive(true);
      pt4.gameObject.SetActive(false);
      pt3.gameObject.SetActive(false);
      pt2.gameObject.SetActive(true);
      pt1.gameObject.SetActive(true);
      p1img.gameObject.SetActive(true);
      p2img.gameObject.SetActive(true);
      p3img.gameObject.SetActive(false);
      p4img.gameObject.SetActive(false);
		}

  }

  void setImg(Image img, int value) {
    if(value == 0) {
      img.sprite = player1cha;
    }
    else if(value == 1) {
      img.sprite = player2cha;
    }
    else if(value == 2) {
      img.sprite = player3cha;
    }
    else if(value == 3) {
      img.sprite = player4cha;
    }
  }
}
