using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EasyMode : MonoBehaviour
{
  void Start()
  {
  }

  // Update is called once per frame
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      SceneManager.LoadScene(1);
    }
  }
}
