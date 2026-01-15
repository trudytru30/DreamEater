using UnityEngine;

public class LightCookieMotion : MonoBehaviour
{
    [SerializeField] Material mLightCookieMaterial;

    [Header("Texture 1")]
    [SerializeField] Vector2 mCycleDuration1UV = new Vector2(20f, 20f);
    [SerializeField] AnimationCurve mMovementPath1U;
    [SerializeField] AnimationCurve mMovementPath1V;
    [SerializeField] Vector2 mMovementMagnitude1UV = new Vector2(0.1f, 0.1f);
    [SerializeField] Vector2 mMovementTimeOffset1UV = new Vector2();
    [SerializeField] Vector2 mTex1TilingUV = new Vector2(1f, 1f);
    [SerializeField] Vector2 mTex1OffsetUV = new Vector2();

    [Header("Texture 2")]
    [SerializeField] Vector2 mCycleDuration2UV = new Vector2(20f, 20f);
    [SerializeField] AnimationCurve mMovementPath2U;
    [SerializeField] AnimationCurve mMovementPath2V;
    [SerializeField] Vector2 mMovementMagnitude2UV = new Vector2(0.1f, 0.1f);
    [SerializeField] Vector2 mMovementTimeOffset2UV = new Vector2();
    [SerializeField] Vector2 mTex2TilingUV = new Vector2(2f, 2f);
    [SerializeField] Vector2 mTex2OffsetUV = new Vector2();

    private float _mTime1U;
    private float _mTime1V;

    private float _mTime2U;
    private float _mTime2V;

    void Update()
    {
        _mTime1U = Time.time % mCycleDuration1UV.x;
        _mTime1U /= mCycleDuration1UV.x;

        _mTime1V = Time.time % mCycleDuration1UV.y;
        _mTime1V /= mCycleDuration1UV.y;

        _mTime2U = Time.time % mCycleDuration2UV.x;
        _mTime2U /= mCycleDuration2UV.x;

        _mTime2V = Time.time % mCycleDuration2UV.y;
        _mTime2V /= mCycleDuration2UV.y;

        UpdateMaterial(); 
    }

    private void UpdateMaterial()
    {
        float newU1 = mMovementPath1U.Evaluate(_mTime1U + mMovementTimeOffset1UV.x) * mMovementMagnitude1UV.x;
        float newV1 = mMovementPath1V.Evaluate(_mTime1V + mMovementTimeOffset1UV.y) * mMovementMagnitude1UV.y;

        var newUV1 = new Vector4(mTex1TilingUV.x, mTex1TilingUV.y, newU1 + mTex1OffsetUV.x, newV1 + mTex1OffsetUV.y);

        mLightCookieMaterial.SetVector("_Tex1_ST", newUV1);

        float newU2 = mMovementPath2U.Evaluate(_mTime2U + mMovementTimeOffset2UV.x) * mMovementMagnitude2UV.x;
        float newV2 = mMovementPath2V.Evaluate(_mTime2V + mMovementTimeOffset2UV.y) * mMovementMagnitude2UV.y;

        var newUV2 = new Vector4(mTex2TilingUV.x, mTex2TilingUV.y, newU2 + mTex2OffsetUV.x, newV2 + mTex2OffsetUV.y);

        mLightCookieMaterial.SetVector("_Tex2_ST", newUV2);
    }

    private void OnValidate()
    {
        UpdateMaterial();
    }
}