using UnityEngine;

public class LightMotion : MonoBehaviour
{
    [SerializeField] Vector2 mCycleDurationXZ = new Vector2(20f, 20f);
    [SerializeField] AnimationCurve mMovementPathX;
    [SerializeField] AnimationCurve mMovementPathZ;
    [SerializeField] Vector2 mMovementMagnitudeXZ = new Vector2(1, 1);
    [SerializeField] Vector2 mMovementTimeOffsetXZ = new Vector2();

    private Vector3 _mInitialPosition;

    private void Awake()
    {
        _mInitialPosition = transform.position;
    }

    void Update()
    {
        UpdateMotion();
    }

    private void UpdateMotion()
    {
        float timeX = Time.time % mCycleDurationXZ.x;
        timeX /= mCycleDurationXZ.x;

        float timeZ = Time.time % mCycleDurationXZ.y;
        timeZ /= mCycleDurationXZ.y;

        float newX = mMovementPathX.Evaluate(timeX + mMovementTimeOffsetXZ.x) * mMovementMagnitudeXZ.x;
        float newZ = mMovementPathZ.Evaluate(timeZ + mMovementTimeOffsetXZ.y) * mMovementMagnitudeXZ.y;

        transform.position = _mInitialPosition + new Vector3(newX, 0, newZ);
    }
}