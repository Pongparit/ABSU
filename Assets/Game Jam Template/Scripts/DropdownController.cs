using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour {

  public Dropdown dropdown;
  public Text selectCount;
  List<string> number = new List<string>() {"Please Select Number of players", "1", "2", "3", "4"};

  public void Dropdown_IndexChanged(int index) {
    selectCount.text = number[index];
  }

  void Start() {
    PopulateList();
    selectCount.enabled = false;
  }

  void PopulateList() {
    dropdown.AddOptions(number);

  }
}
