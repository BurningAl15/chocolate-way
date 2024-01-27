using UnityEngine;

namespace AldhaDev.ScriptableObjects
{
    [CreateAssetMenu(menuName = "UI/ContainerLimits", fileName = "New Container Limits Asset", order = 0)]
    public class ContainerLimitsAsset : ScriptableObject
    {
        public Vector3 InitialPoint => initialPoint;

        public Vector3 MidPoint => midPoint;

        public Vector3 EndPoint => endPoint;

        public AnimationCurve AnimationCurve => animationCurve;

        [SerializeField] Vector3 initialPoint, midPoint, endPoint;
        [SerializeField] AnimationCurve animationCurve;
    }
}