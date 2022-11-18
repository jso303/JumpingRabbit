using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.UI;
using TMPro;

public class Clear : MonoBehaviour
{
  Animator anim;

  public GameObject ClearPanel;

  void Start()
  {
    anim = GetComponent<Animator>();
    ClearPanel.SetActive(false);
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
