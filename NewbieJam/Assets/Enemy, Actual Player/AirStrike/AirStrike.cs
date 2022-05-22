using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirStrike : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform[] path;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform fireEndPoint;
    [SerializeField] int scoreReward = 10;
    [SerializeField] float damageAmount = 5;

    LineController laser;
    AudioHandler audioHandler;
    PlayerAI player;
    ScoreKeeper scoreKeeper;
    int waypointIndex = 0;

    private void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        audioHandler = FindObjectOfType<AudioHandler>();
        laser = FindObjectOfType<LineController>();
        player = FindObjectOfType<PlayerAI>();
    }
    private void OnEnable() {
        waypointIndex = 0;
        transform.position = new Vector2(11.5f, 3);
    }
    private void Update() {
        MovePlane();
    }

    void MovePlane()
    {
        transform.position = Vector2.MoveTowards(transform.position, path[waypointIndex].position, speed * Time.deltaTime);

        if(transform.position == path[waypointIndex].position)
        {
            waypointIndex++;
        }
        if(waypointIndex == path.Length)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "playerAI"){
            scoreKeeper.Score(scoreReward);
            player.TakeDamage(damageAmount);
        }
    }
}
