using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour {

    public float backgroundSize;
    Transform cameraTransform;
    Transform[] layers;
    float viewZone = 5.5f;
    int leftIndex;
    int rightIndex;

	// Use this for initialization
	void Start () {
        cameraTransform = Camera.main.transform;
        layers = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;
	}


    private void Update()
    {
        if(cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
        {
            ScrollLeft();
        }
        else if(cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
        {
            ScrollRight();
        }
    }
    // Update is called once per frame
    void ScrollLeft()
    {
        int lastRight = rightIndex;
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex = layers.Length - 1;
    }

    void ScrollRight()
    {
        int lastLeft = leftIndex;
        layers[lastLeft].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
            leftIndex = 0;
    }
}
