using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
  Animator anim;
  void Start()
  {
    anim = GetComponent<Animator>();
  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      anim.SetBool("Clear", true);
      Invoke("MainScenes", 3.0f);
    }
  }

  void MainScenes()
  {
    SceneManager.LoadScene(0);
  }

}
