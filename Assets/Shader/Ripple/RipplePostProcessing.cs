using UnityEngine;
using UnityEngine.Serialization;

namespace AldhaDev.VFX
{
    public class RipplePostProcessing : MonoSingleton<RipplePostProcessing>
    {
        [FormerlySerializedAs("RippleMaterial")] public Material rippleMaterial;
        [FormerlySerializedAs("MaxAmount")] public float maxAmount = 50f;
        [FormerlySerializedAs("Friction")] [Range(0,1)] public float friction = .9f;
        private float _amount = 0f;
        private static readonly int Amount = Shader.PropertyToID("_Amount");
        private static readonly int CenterX = Shader.PropertyToID("_CenterX");
        private static readonly int CenterY = Shader.PropertyToID("_CenterY");

        void Update()
        {
            this.rippleMaterial.SetFloat(Amount, this._amount);
            this._amount *= this.friction;
        }

        void OnRenderImage(RenderTexture src, RenderTexture dst)
        {
            Graphics.Blit(src, dst, this.rippleMaterial);
        }
    
        #region Utils

        public void CallRipple(Vector2 pos)
        {
            this._amount = this.maxAmount;
            this.rippleMaterial.SetFloat(CenterX, pos.x);
            this.rippleMaterial.SetFloat(CenterY, pos.y);
        }

        public void CallRipple(Vector2 pos, float maxAmount)
        {
            this._amount = maxAmount;
            this.rippleMaterial.SetFloat(CenterX, pos.x);
            this.rippleMaterial.SetFloat(CenterY, pos.y);
        }

        #endregion
    }
}

