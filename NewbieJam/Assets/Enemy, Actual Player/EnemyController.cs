using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Camera mainCam;
    [SerializeField] Transform rocketSpawnPoint;
    [SerializeField] Transform obstacleSpawnPoint;
    [SerializeField] Transform airStrikeSpawnPoint;
    [SerializeField] GameObject obstacle;
    [SerializeField] GameObject rocket;
    [SerializeField] GameObject plane;
    Timer timer;
    AudioHandler audioHandler;

#region Abilities Section
[Header("Abilities")]
    [Header("Ability1")]
    [SerializeField] Image abilityImage1;
    [SerializeField] float cooldown1;
    bool onCD1 = false;

    [Header("Ability2")]
    [SerializeField] Image abilityImage2;
    [SerializeField] float cooldown2;
    int rocketShootAmount;
    bool onCD2 = false;

    [Header("Ability3")]
    [SerializeField] Image abilityImage3;
    [SerializeField] Image[] ultiBalls;
    public int ultiPoints;
    #endregion

    Vector3 mouseWorldPosition;

    private void Awake() {
        ultiPoints = 0;
        timer = FindObjectOfType<Timer>();
        audioHandler = FindObjectOfType<AudioHandler>();
    }

    void Update()
    {
        mouseWorldPosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        EnemySkills();
    }

    void EnemySkills()
    {
        SpawnObstacle();
        SpawnRocket();
        CallAirStrike();
    }

    void SpawnObstacle()
    {
        if (Input.GetButtonDown("Fire1") && !onCD1 && !timer.timerRanOut)
        {
            onCD1 = true;
            abilityImage1.fillAmount = 1;
            Instantiate(obstacle, obstacleSpawnPoint.transform.position, Quaternion.identity);
        }

        if(onCD1)
        {
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

            if(abilityImage1.fillAmount == 0)
            {
                onCD1 = false;
            }
        }
    }

    void SpawnRocket()
    {
        rocketSpawnPoint.transform.position = new Vector3( 7.5f , Mathf.Clamp(mouseWorldPosition.y, -3f, 2.5f) , 0 );
        if (Input.GetButtonDown("Fire2") && !onCD2 && !timer.timerRanOut)
        {
            rocketShootAmount++;
            audioHandler.PlayRocketSoundClip();
            Instantiate(rocket, rocketSpawnPoint.transform.position, Quaternion.identity);
            if(rocketShootAmount >= 5)
            {
                onCD2 = true;
                abilityImage2.fillAmount = 1;
            }
        }

        if(onCD2)
        {
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;

            if(abilityImage2.fillAmount == 0)
            {
                onCD2 = false;
                rocketShootAmount = 0;
            }
        }
    }

    void CallAirStrike()
    {
        if(ultiPoints >= 5)
        {
            abilityImage3.gameObject.SetActive(true);
            if(Input.GetKeyDown(KeyCode.R) && !timer.timerRanOut)
            {
                plane.SetActive(true);
                audioHandler.PlayAirStrikeClip();
                ultiPoints = 0;
            }
        }
        else
        {
            abilityImage3.gameObject.SetActive(false);
        }

        for (int i = 0; i < ultiBalls.Length; i++)
        {
            if(i < ultiPoints)
            {
                ultiBalls[i].enabled = true;
            }
            else
            {
                ultiBalls[i].enabled = false;
            }

            if(ultiPoints >= 5)
            {
                ultiBalls[i].enabled = false;
            }
        }
    }

    public void GetUlitPoints ()
    {
        ultiPoints++;
    }
}
