using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Moku;

public class ParallaxImage : MonoBehaviour {

    //public 
    public float speedX = 0;
    public float speedY = 0;
    public int spawnCount = 2;
    public float repositionBuffer = .5f;

    public ImageType imageType;

    //private
    private const int roundFactor = 1000;

    private Vector3 startPos;
    private float imageWidth;
    private float minLeftX;
    private float maxRightX;
    private Transform[] controlledTransforms;
    private SpriteRenderer sr;


    private Movement vertical;
    private Movement horizontal;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        startPos = transform.position;
    }

    public void MoveY(float moveBy) {
        moveBy *= speedY * vertical.speedMultiplier;
        if (vertical.direction == Direction.Negative) moveBy *= -1;

        for (int i = 0; i < controlledTransforms.Length; i++) {
            Vector3 newPos = controlledTransforms[i].position;
            newPos.y += moveBy;
            controlledTransforms[i].position = newPos;
        }
    }

    public void SetY(float y) {
        y *= speedY * vertical.speedMultiplier;
        if (vertical.direction == Direction.Negative) y *= -1;

        for (int i = 0; i < controlledTransforms.Length; i++) {
            Vector3 newPos = controlledTransforms[i].position;
            newPos.y = y;
            controlledTransforms[i].position = newPos;
        }
    }

    public void MoveX(float moveBy, bool doSpeedCalc = true) {
        if (doSpeedCalc) moveBy *= speedX * horizontal.speedMultiplier;
        if (horizontal.direction == Direction.Negative) moveBy *= -1;

        moveBy = Mathf.Round(moveBy * roundFactor) / roundFactor;
        for (int i = 0; i < controlledTransforms.Length; i++) {
            Vector3 newPos = controlledTransforms[i].position;
            newPos.x += moveBy;
            newPos.x = Mathf.Round(newPos.x * roundFactor) / roundFactor;
            controlledTransforms[i].position = newPos;
        }
       
    }

   
    public void CleanUpImage()
    {
        if (controlledTransforms != null)
        {
            for (int i = 1; i < controlledTransforms.Length; i++)
            {
                Destroy(controlledTransforms[i].gameObject);
            }
        }
    }

    public void InitImage(Movement horizontal, Movement vertical) {
        this.horizontal = horizontal;
        this.vertical = vertical;

        transform.position = startPos;

        PrepareVariables();

        CreateImageInstances();

    }

    private void PrepareVariables() {
        imageWidth = sr.bounds.size.x;
     
        if (horizontal.type == MoveType.FollowTransform) {
            minLeftX = transform.position.x - imageWidth * (spawnCount + 1) - repositionBuffer;
            maxRightX = transform.position.x + imageWidth * (spawnCount + 1) + repositionBuffer;

        } else {
            if (horizontal.direction == Direction.Negative) {
                minLeftX = transform.position.x - imageWidth - repositionBuffer;
                maxRightX = float.PositiveInfinity;
            } else if (horizontal.direction == Direction.Positive) {
                maxRightX = transform.position.x + imageWidth + repositionBuffer;
                minLeftX = float.NegativeInfinity;
            } else if (horizontal.direction == Direction.Fix) {
                minLeftX = float.NegativeInfinity;
                maxRightX = float.PositiveInfinity;
            }
        }
    }

    private void CreateImageInstances() {
        int arraySize = spawnCount;
        if (horizontal.type == MoveType.FollowTransform) arraySize *= 2;
        arraySize += 1;

        controlledTransforms = new Transform[arraySize];
        controlledTransforms[0] = transform;

        float changeBy;
        for (int i = 1; i <= spawnCount; i++)
        {
            if (horizontal.direction == Direction.Positive) {
                changeBy = -imageWidth * i;
            } else {
                changeBy = imageWidth * i;
            }
                 
        }
    }
}

public enum ImageType {
    Seamless
}


