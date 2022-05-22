using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] Texture2D[] textures;
    [SerializeField] float fps = 30f;

    float fpsCounter;
    int animationStep;
    LineRenderer lineRenderer;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update() {
        fpsCounter += Time.deltaTime;

        if(fpsCounter >= 1f / fps)
        {   
            animationStep++;
            if(animationStep == textures.Length){
                animationStep = 0;
            }

            lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);

            fpsCounter = 0f;
        }
    }
    public void DrawRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
