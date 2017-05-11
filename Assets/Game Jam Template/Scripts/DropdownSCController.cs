using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class DropdownSCController : MonoBehaviour {

  public Dropdown dropdown1;
  public Dropdown dropdown2;
  public Dropdown dropdown3;
  public Dropdown dropdown4;
  public Text p1;
  public Text p2;
  public Text p3;
  public Text p4;
  List<string> list = new List<string>() {"Act", "Jam", "Noon", "Robert"};


  public void Dropdown_IndexChanged(int index) {
    p1.text = dropdown1.value+1+"";
    p2.text = dropdown2.value+1+"";
    p3.text = dropdown3.value+1+"";
    p4.text = dropdown4.value+1+"";
  }

  void Start() {
    PopulateList();
  }

  void PopulateList() {
    dropdown1.AddOptions(list);
    dropdown2.AddOptions(list);
    dropdown3.AddOptions(list);
    dropdown4.AddOptions(list);

  }
}
