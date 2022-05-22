using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int scoreReward = 10;
    [SerializeField] float damageAmount = 5;

    EnemyController enemyController;
    AudioHandler audioHandler;
    PlayerAI player;
    ScoreKeeper scoreKeeper;

    private void Awake() {
        enemyController = FindObjectOfType<EnemyController>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        player = FindObjectOfType<PlayerAI>();
        audioHandler = FindObjectOfType<AudioHandler>();
    }

    private void Update() {
        transform.Translate((-Vector3.right) * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "playerAI")
        {
            audioHandler.PlayObstacleHitClip();
            enemyController.GetUlitPoints();
            scoreKeeper.Score(scoreReward);
            player.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if( other.gameObject.CompareTag("Wall")) { Destroy(gameObject); }
    }
}
