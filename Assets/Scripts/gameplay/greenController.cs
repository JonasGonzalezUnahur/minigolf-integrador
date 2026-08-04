using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class greenController : MonoBehaviour
{
    [Header("Components")]
    public SpriteRenderer sprite;
    public GameObject arrow;
    
    [Header("Slide")]
    public bool isSlide = false;
    public float slideAngle;

    public void Awake()
    {
        if (isSlide)
        {
            sprite.color = new Color(sprite.color.r - 0.3f, sprite.color.g - 0.3f, sprite.color.b - 0.3f);
            arrow.SetActive(true);
            arrow.transform.Rotate(0, 0, slideAngle);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.GetComponent<playerBall>();
        player.groundOk = true;
        float slideAnglex = Mathf.Cos(slideAngle * Mathf.PI / 180);
        float slideAngley = Mathf.Sin(slideAngle * Mathf.PI / 180);
        if (collision.gameObject.CompareTag("Player") && isSlide)
        {
            player.rb2d.AddForce(new Vector2(slideAnglex, slideAngley) * 2);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<playerBall>();
        player.groundOk = false;
    }
}
