using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
  Rigidbody2D rbody;
  void Start()
  {
    rbody = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      rbody.isKinematic = false;
    }
  }
}
