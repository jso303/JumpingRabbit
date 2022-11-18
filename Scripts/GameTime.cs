using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{
  public TextMesh PlayTime;
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    ClearTime += Time.deltaTime;
    PlayTime.text = "Clear : " + Mathf.Round(ClearTime);
  }
}
