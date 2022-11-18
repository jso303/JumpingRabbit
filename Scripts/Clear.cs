using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.UI;

public class Clear : MonoBehaviour
{
  Animator anim;
  public float GameTimer;
  TextMesh ClearTime;
  public GameObject ClearPanel;
  void Start()
  {
    anim = GetComponent<Animator>();
    ClearPanel.SetActive(false);
  }

  void Update()
  {
    GameTimer += Time.deltaTime;
    ClearTime.text = "Clear : " + Mathf.Round(GameTimer);
  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      Time.timeScale = 0;
      ClearPanel.SetActive(true);
    }
  }



}
