using UnityEngine;
using UnityEngine.EventSystems;

namespace AldhaDev.Player.TopDown
{
    public class MobileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] PlayerController2D_TopDown player;
        [SerializeField] Direction direction;

        public void OnPointerDown(PointerEventData _)
        {
            player.HandleUpdate(direction);
        }

        public void OnPointerUp(PointerEventData _)
        {
            player.HandleUpdate(Direction.NoDirection);
        }
    }
}

