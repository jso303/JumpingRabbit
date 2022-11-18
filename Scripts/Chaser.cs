using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Chaser : MonoBehaviour
{
  private Rigidbody2D rbody;
  public float DeathWormSpeed;
  public GameObject GameOverPanel;
  void Start()
  {
    rbody = GetComponent<Rigidbody2D>();
    rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    GameOverPanel.SetActive(false);
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    transform.position += Vector3.up * DeathWormSpeed;
  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      Time.timeScale = 0;
      GameOverPanel.SetActive(true);
    }
  }


}
