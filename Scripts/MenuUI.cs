using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BTNType
{
  Main,
  Continue,
  Retry,
  Quit
}

public class MenuUI : MonoBehaviour
{
  public GameObject MenuPanel;

  private void Start()
  {
    MenuPanel.SetActive(false);
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      // Debug.Log("메뉴");
      Time.timeScale = 0;
      MenuPanel.SetActive(true);
    }
  }
}
