using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform startingPoint;
    public float lerpSpeed;
    public int score;
    private Animator animator;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        playerController.gameOver = true;
        StartCoroutine(playIntro());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator playIntro()
    {
        Vector3 startPos = playerController.transform.position;
        Vector3 endPos = startingPoint.position;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;
        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        playerController.GetComponent<Animator>().SetFloat("Speed_Multiplier",0.5f);


        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            playerController.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }
        playerController.GetComponent<Animator>().SetFloat("Speed_Multiplier",
        1.0f);
        playerController.gameOver = false;
    }


public void addScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }
    public void changeAnimatorSpeed(float value)
    {
        animator.speed = value;
    }
}
