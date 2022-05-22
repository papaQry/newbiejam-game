using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float scrollSpeed;
    [SerializeField] float maxScrollSpeed = 10f;

    [SerializeField] float accelerationTime = 60;
    PlayerAI player;
    Timer timer;
    float minSpeed;
    float time;

    private void Awake() {
        player = FindObjectOfType<PlayerAI>();
        timer = FindObjectOfType<Timer>();
    }

    private void Start() {
        minSpeed = scrollSpeed;
        time = 0;
    }

    void Update()    
    {
        transform.Translate(-1 * scrollSpeed * Time.deltaTime, 0, 0);

        if (cam.transform.position.x >= transform.position.x + 18f)
        {
            transform.position = new Vector2(cam.transform.position.x + 18f, transform.position.y);
        }

        scrollSpeed = Mathf.SmoothStep(minSpeed, maxScrollSpeed, time / accelerationTime);
        time += Time.deltaTime;

        if(player.isDead == true)
        {
            maxScrollSpeed = 5f;
            scrollSpeed = 5f;
            accelerationTime = Mathf.Epsilon;
        }

        if(timer.timerRanOut)
        {
            maxScrollSpeed = 2f;
            scrollSpeed = 2f;
            accelerationTime = Mathf.Epsilon;
        }
    }
}
