using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
  public float GameTimer;
  public TextMeshProUGUI ClearTime;

  private void Awake()
  {
    ClearTime = GetComponent<TextMeshProUGUI>();
  }
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    GameTimer += Time.deltaTime;
    ClearTime.text = (Mathf.Round(GameTimer * 10) * 0.1f).ToString();
  }
}
