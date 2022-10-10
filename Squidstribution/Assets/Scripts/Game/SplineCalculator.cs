using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineCalculator : MonoBehaviour
{
    enum Direction { FORWARDS, BACKWARDS};
    //choses to go forwards of backwards based on this state
    Direction direction = Direction.FORWARDS;

    [SerializeField] bool reversing;

    enum InterpType { LINEAR, QUADRATIC };
    //pick how the object will move along spline
    [SerializeField] InterpType interpolation = InterpType.LINEAR;

    //object you want to travel along spline (a building as a silly example)
    [SerializeField] GameObject splineObject;
    //unpack the prefab and add transforms for the spline points to the object as children and in this list
    [SerializeField] List<Transform> splinePoints;
    //add points in here to make the spline curve in a quadratic interpolation rather than linear
    //[SerializeField] List<Transform> curvePoints;
    //changes how fast the object will travel
    [SerializeField] float travelSpeed = 5f;

    Transform currentPoint;
    Transform nextPoint;

    float interpolateAmount;
    float splineLength;
    int currentIndex = 0;
    int nextIndex;

    private void Start()
    {
        splineLength = splinePoints.Count - 1;
        currentPoint = splinePoints[0];
        nextPoint = splinePoints[1];
    }

    private void Update()
    {
        if(direction == Direction.FORWARDS)
        {
            nextIndex = 1;
        }
        if(direction == Direction.BACKWARDS)
        {
            nextIndex = -1;
        }

        interpolateAmount = (interpolateAmount + Time.deltaTime * travelSpeed);

            if (interpolation == InterpType.LINEAR)
                NextPointLinear(currentPoint, nextPoint);
            if (interpolation == InterpType.QUADRATIC)
                return;
        
    }

    void NextPointLinear(Transform A, Transform B)
    {
        splineObject.transform.position = Vector3.Lerp(A.position, B.position, interpolateAmount);
        if (IsNextPointLinear(B))
        {
            currentIndex += nextIndex;
            currentPoint = nextPoint;

            if (reversing && (currentIndex == splineLength || currentIndex == 0))
            {
                if (direction == Direction.FORWARDS)
                {
                    direction = Direction.BACKWARDS;
                    nextIndex = -1;
                }
                else if(direction == Direction.BACKWARDS)
                {
                    direction = Direction.FORWARDS;
                    nextIndex = 1;
                }
                nextPoint = splinePoints[currentIndex + nextIndex];
            }

            if (currentIndex + nextIndex <= splineLength && 0 < (currentIndex + nextIndex))
                nextPoint = splinePoints[currentIndex + nextIndex];

            interpolateAmount = 0;
        }
    }

    bool IsNextPointLinear(Transform B)
    {
        if (currentIndex >= splineLength || currentIndex < 0)
        {
            return false;
        }
        return splineObject.transform.position == B.position;
    }
}
