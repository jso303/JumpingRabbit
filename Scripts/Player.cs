using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private Rigidbody2D rbody;

  // 점프 파워 계수
  private float JumpPower = 2;
  // 최대 점프 게이지
  private float JumpTimeLimit = 4.0f;
  // 점프 게이지 (최소 1.0f)
  private float JumpTime = 1.0f;

  // 버그 수정용 타이머
  private float BugFix = 2.0f;
  private float BugTime = 0;


  // 연속 점프 게이지
  public float ComboTime;

  private SpriteRenderer rend;
  public bool Flip;
  Animator anim;
  AudioSource audioSource;

  // 모바일 키 변수
  bool Jump_Down;
  bool Jump_Up;
  bool Left_Down;
  bool Right_Down;
  bool Left_Up;
  bool Right_Up;
  bool isClick;

  // 모바일 종료 카운터
  int ClickCount = 0;


  void Start()
  {
    rbody = GetComponent<Rigidbody2D>();
    rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    rend = GetComponentInChildren<SpriteRenderer>();
    anim = GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();
    Screen.SetResolution(Screen.width, (Screen.width * 20) / 9, true);
  }


  void Update()
  {
    Jump();
    Jump_Up = false;
    Jump_Down = false;

    if (Input.GetKeyDown(KeyCode.Escape))
    {
      ClickCount++;
      if (!IsInvoking("DoubleClick"))
      {
        Invoke("DoubleClick", 1.0f);
      }
      else if (ClickCount == 2)
      {
        CancelInvoke("DoubleClick");
        Application.Quit();
      }
    }
  }

  void DoubleClick()
  {
    ClickCount = 0;
  }

  void FixedUpdate()
  {
    BugTime += 0.01f;
    BugFixed();
    JumpGauge();
    RayDetect();
    groundFlip();
    Move();
  }

  public void ButtonDown(string type)
  {
    switch (type)
    {
      case "J":
        Jump_Down = true;
        isClick = true;
        break;
      case "L":
        Left_Down = true;
        break;
      case "R":
        Right_Down = true;
        break;
    }
  }
  public void ButtonUp(string type)
  {
    switch (type)
    {
      case "J":
        Jump_Up = true;
        isClick = false;
        break;
      case "L":
        Left_Down = false;
        break;
      case "R":
        Right_Down = false;
        break;
    }
  }

  void BugFixed()
  {
    if (BugTime > BugFix)
    {
      transform.position += Vector3.left * 0.1f;
      transform.position += Vector3.up * 0.1f;
      BugTime = 0;
    }
  }

  // 점프 입력
  void Jump()
  {
    // 점프 키를 떼면 점프 실행
    if (Jump_Up && !anim.GetBool("isJump"))
    {
      // 연속 점프 판정
      if (ComboTime > 0.0f)
      {
        JumpPower += 1;
      }
      // 점프 파워 계수 초기화
      else JumpPower = 2;

      anim.SetBool("isDown", false);
      anim.SetBool("readyToJump", false);
      // 점프 계산식 = JumpPower(점프 계수) * JumpTime(점프 게이지(시간비례))
      rbody.AddForce(Vector3.up * JumpPower * JumpTime, ForceMode2D.Impulse);
      audioSource.Play();
      anim.SetBool("isJump", true);
      // 최소 점프 수치
      JumpTime = 0.5f;
      // 연속 점프 게이지 초기화
      ComboTime = 1.0f;
    }
  }

  void groundFlip()
  {
    // 지상에 있을 때
    if (!anim.GetBool("isJump"))
    {
      ComboTime -= 0.02f;
      anim.SetBool("isHit", false);
      BugTime = 0;
      if (Left_Down)
      {
        rend.flipX = true;
        Flip = true;
      }
      if (Right_Down)
      {
        rend.flipX = false;
        Flip = false;
      }
    }
  }

  // 공중에 있을 때
  void Move()
  {
    if (anim.GetBool("isJump") && !anim.GetBool("isHit"))
    {
      if (Jump_Down)
      {
        JumpPower = 2;
      }
      if (Flip == true && Left_Down)
      {
        rbody.AddForce(Vector3.left * 4.0f, ForceMode2D.Force);
      }
      else if (Flip == false && Right_Down)
      {
        rbody.AddForce(Vector3.right * 4.0f, ForceMode2D.Force);
      }
    }
  }

  // 점프 게이지 모으기
  void JumpGauge()
  {
    // 점프 키를 입력할 때 점프 중이 아니라면 (바닥에 있다면)
    if (isClick && !anim.GetBool("isJump"))
    {
      // 점프 게이지 증가 (최대치 존재)
      if (JumpTimeLimit > JumpTime)
        JumpTime += 0.1f;
    }

    if (JumpTime > 1.1f)
    {
      anim.SetBool("readyToJump", true);
    }
  }

  // 벽에 부딧혔을때
  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Block" && anim.GetBool("isJump"))
    {
      anim.SetBool("isHit", true);
      JumpPower = 2;
      if (Flip == true)
      {
        rbody.AddForce(Vector3.right, ForceMode2D.Impulse);
      }
      else if (Flip == false)
      {
        rbody.AddForce(Vector3.left, ForceMode2D.Impulse);
      }
    }
  }


  // Raycast로 바닥 탐지
  void RayDetect()
  {
    if (rbody.velocity.y < 0)
    {
      anim.SetBool("isDown", true);
      ComboTime -= 0.01f;

      // Ray 그리기
      Debug.DrawRay(rbody.position, Vector3.down, new Color(0, 1, 0));
      // 아래로 Ray 쏘기, Layer가 "Platform"인 경우만 탐지
      RaycastHit2D rayHit = Physics2D.Raycast(rbody.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

      // 충돌된 경우
      if (rayHit.collider != null)
      {
        // 적중된 개체가 레이길이의 0.5보다 작은 경우 
        if (rayHit.distance < 0.5f)
        {
          anim.SetBool("isJump", false);
        }
      }
    }
  }
}
