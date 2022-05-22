using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [Header("Obstacle")]
    [SerializeField] AudioClip obstacleHitClip;
    [SerializeField] [Range(0f, 1f)]float obstacleHitVolume = 1;

    [Header("Rocket")]
    [SerializeField] AudioClip rocketHitClip;
    [SerializeField] AudioClip rocketSoundClip;
    [SerializeField] [Range(0f, 1f)]float rocketHitVolume = 1;

    [Header("AirStrike")]
    [SerializeField] AudioClip airStrikeClip;
    [SerializeField] [Range(0f, 1f)]float airStrikeVolume = 1;


    static AudioHandler instance;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayObstacleHitClip()
    {
        AudioSource.PlayClipAtPoint(obstacleHitClip, Camera.main.transform.position, obstacleHitVolume);
    }

        public void PlayRocketHitClip()
    {
        AudioSource.PlayClipAtPoint(rocketHitClip, Camera.main.transform.position, rocketHitVolume);
    }

        public void PlayRocketSoundClip()
    {
        AudioSource.PlayClipAtPoint(rocketSoundClip, Camera.main.transform.position, rocketHitVolume);
    }

        public void PlayAirStrikeClip()
    {
        AudioSource.PlayClipAtPoint(airStrikeClip, Camera.main.transform.position, airStrikeVolume);
    }
}
