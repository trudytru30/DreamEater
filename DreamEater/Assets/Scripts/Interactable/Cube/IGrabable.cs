using System.Collections.Generic;
using UnityEngine;

public interface IGrabable
{
    // public void Grab(Transform grabber, float holdDistance);
    public void Grab(Transform grabber);
    public void Release();
    // Method to update position while held
    void UpdateHoldPosition(Vector3 targetPosition, Quaternion targetRotation);
}