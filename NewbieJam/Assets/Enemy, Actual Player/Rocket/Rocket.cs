using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float shootSpeed;
    [SerializeField] Transform targetPlayer;
    [SerializeField] int scoreReward = 10;
    [SerializeField] float damageAmount = 5;

    EnemyController enemyController;
    AudioHandler audioHandler;
    PlayerAI player;
    ScoreKeeper scoreKeeper;


    private void Awake() {
        enemyController = FindObjectOfType<EnemyController>();
        audioHandler = FindObjectOfType<AudioHandler>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        player = FindObjectOfType<PlayerAI>();
    }


    void Update()
    {
        transform.Translate((-Vector3.right) * shootSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("playerAI"))
        {
            audioHandler.PlayRocketHitClip();
            enemyController.GetUlitPoints();
            scoreKeeper.Score(scoreReward);
            player.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
