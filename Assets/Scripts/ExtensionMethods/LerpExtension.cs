using System.Collections.Generic;
using UnityEngine;

namespace AldhaDev.ExtensionMethods
{
    public static class LerpExtension
    {
        public static void Lerp(this Transform platePos, List<Transform> lerpPoints, int lerpInitialIndex, float lerpValue)
        {
            platePos.position = Vector3.Lerp(lerpPoints[lerpInitialIndex].position,
                lerpPoints[lerpInitialIndex + 1].position, lerpValue);
        }
        public static void Lerp(this Transform platePos, List<Vector3> lerpPoints, int lerpInitialIndex, float lerpValue)
        {
            platePos.position = Vector3.Lerp(lerpPoints[lerpInitialIndex],
                lerpPoints[lerpInitialIndex + 1], lerpValue);
        }

        public static void Lerp(this Transform platePos, Transform initialPos, Transform endPos, float lerpValue)
        {
            platePos.position = Vector3.Lerp(initialPos.position,
                endPos.position, lerpValue);
        }
        public static void Lerp(this RectTransform platePos, Vector3 initialPos, Vector3 endPos, float lerpValue)
        {
            platePos.localPosition = Vector3.Lerp(initialPos,
                endPos, lerpValue);
        }

        public static void Lerp_X(this Transform objectTransform, float xEndPos)
        {
            Vector3 temp = objectTransform.position;
            objectTransform.position = new Vector3(xEndPos, temp.y, temp.z);
        }
    }
}
