using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonType : MonoBehaviour
{
  public BTNType currentType;
  public GameObject MenuPanel;

  public void OnBtnClick()
  {
    switch (currentType)
    {
      case BTNType.Main:
        SceneManager.LoadScene(0);
        // Debug.Log("메인");

        Time.timeScale = 1.0f;
        break;
      case BTNType.Continue:
        MenuPanel.SetActive(false);
        Time.timeScale = 1.0f;
        // Debug.Log("계속");
        break;
      case BTNType.Retry:
        SceneManager.LoadScene(2);
        Time.timeScale = 1.0f;
        // Debug.Log("재시작");
        break;
      case BTNType.Quit:
        Application.Quit();
        // Debug.Log("종료");
        break;
    }
  }
}
