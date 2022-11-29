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
  AudioSource audioSource;



  void Start()
  {
    rbody = GetComponent<Rigidbody2D>();
    sprite = GetComponent<SpriteRenderer>();
    rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    audioSource = GetComponent<AudioSource>();
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
      sprite.color = new Color(1, 0.2f * (5 - RageCount), 0.2f * (5 - RageCount));
    }
    else if (MoveTimer >= 0.5)
    {
      Move = 1;
    }
  }
  void TimeStop()
  {
    Time.timeScale = 0;
  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      Invoke("TimeStop", 7);
      Handheld.Vibrate();
      GameOverPanel.SetActive(true);
    }
    if (collision.gameObject.tag == "Attack")
    {
      Destroy(collision.gameObject);
      DeathWormSpeed -= 2 * StagePoint;
      RageCount += 1;
      audioSource.Play();
      if (RageCount >= 3)
      {
        DeathWormSpeed = 6 * StagePoint;
        Handheld.Vibrate();
      }
      sprite.color = new Color(1, 0.2f * (5 - RageCount), 0.2f * (5 - RageCount));
      Invoke("SpeedReset", 4.0f);
    }
    if (collision.gameObject.tag == "Stage")
    {
      StagePoint += 0.2f;
      Destroy(collision.gameObject);
      DeathWormSpeed = 2 * StagePoint;
      Handheld.Vibrate();
      audioSource.Play();
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
      RageCount -= 0.002f;
    }
  }
}


