using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
  // 모바일 종료 카운터
  int ClickCount = 0;


  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      GameQuit();
    }
  }

  public void DoubleReset()
  {
    ClickCount = 0;
  }

  public void GameQuit()
  {
    ClickCount++;
    if (!IsInvoking("ClickReset"))
    {
      Invoke("DoubleReset", 1.0f);
    }
    else if (ClickCount == 2)
    {
      CancelInvoke("DoubleReset");
      Application.Quit();
    }
  }
}
