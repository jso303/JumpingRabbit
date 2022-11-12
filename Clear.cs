using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
  }

}
