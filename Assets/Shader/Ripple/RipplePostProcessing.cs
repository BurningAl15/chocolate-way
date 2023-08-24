using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RipplePostProcessing : MonoBehaviour
{
    public static RipplePostProcessing _instance;

    [FormerlySerializedAs("RippleMaterial")] public Material rippleMaterial;
    [FormerlySerializedAs("MaxAmount")] public float maxAmount = 50f;
    [FormerlySerializedAs("Friction")] [Range(0,1)] public float friction = .9f;
    private float amount = 0f;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);
    }
    
    void Update()
    {
        this.rippleMaterial.SetFloat("_Amount", this.amount);
        this.amount *= this.friction;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, this.rippleMaterial);
    }
    
    #region Utils

    public void CallRipple(Vector2 pos)
    {
        this.amount = this.maxAmount;
        this.rippleMaterial.SetFloat("_CenterX", pos.x);
        this.rippleMaterial.SetFloat("_CenterY", pos.y);
    }

    public void CallRipple(Vector2 pos, float _maxAmount)
    {
        this.amount = _maxAmount;
        this.rippleMaterial.SetFloat("_CenterX", pos.x);
        this.rippleMaterial.SetFloat("_CenterY", pos.y);
    }

    #endregion
}
