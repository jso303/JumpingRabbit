using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Chaser : MonoBehaviour
{
  Rigidbody2D rbody;
  SpriteRenderer sprite;
  public float DeathWormSpeed;
  public GameObject GameOverPanel;
  private float Move;
  private float MoveTimer;
  private float RageCount;
  private float StagePoint;
  void Start()
  {
    rbody = GetComponent<Rigidbody2D>();
    sprite = GetComponent<SpriteRenderer>();
    rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    GameOverPanel.SetActive(false);
    RageCount = 0;
    StagePoint = 1;
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    MoveTimer += 0.01f;
    MoveRule();
    RageDown();
    transform.position += Vector3.up * DeathWormSpeed * 0.01f * Move;
  }

  void MoveRule()
  {
    if (MoveTimer >= 1)
    {
      Move = 0.1f;
      MoveTimer = 0;
    }
    else if (MoveTimer >= 0.5)
    {
      Move = 1;
    }
  }


  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      Time.timeScale = 0;
      GameOverPanel.SetActive(true);
    }
    if (collision.gameObject.tag == "Attack")
    {
      Destroy(collision.gameObject);
      DeathWormSpeed -= 2 * StagePoint;
      RageCount += 1;
      if (RageCount >= 5)
      {
        DeathWormSpeed = 5 * StagePoint;
      }
      sprite.color = new Color(1, 0.2f * (5 - RageCount), 0.2f * (5 - RageCount));
      Invoke("SpeedReset", 4.0f);
    }
    if (collision.gameObject.tag == "Stage")
    {
      StagePoint += 0.2f;
      Destroy(collision.gameObject);
      DeathWormSpeed = 2 * StagePoint;
    }
  }

  void SpeedReset()
  {
    DeathWormSpeed = 2 * StagePoint;
    sprite.color = new Color(1, 0.2f * (5 - RageCount), 0.2f * (5 - RageCount));
  }

  void RageDown()
  {
    if (RageCount > 0.5f)
    {
      RageCount -= 0.01f;
    }
  }
}


