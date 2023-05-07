using UnityEngine;

namespace Services
{
    public interface IInputService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
    }

    public class InputService : IInputService
    {
        private const string VERTICAL = "Vertical";
        private const string HORIZONTAL = "Horizontal";
        private const string FIRE = "Fire1";

        public Vector2 Axis => new Vector2(Input.GetAxis(HORIZONTAL), Input.GetAxis(VERTICAL));

        public bool IsAttackButtonUp() => Input.GetButtonUp(FIRE);
    }
}