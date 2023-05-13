using CodeBase.infrastructure.Service;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
    }

    public class InputService : IInputService
    {
        private const string VERTICAL = "Vertical";
        private const string HORIZONTAL = "Horizontal";
        private const string FIRE = "Fire1";

        public Vector2 Axis => new Vector2(UnityEngine.Input.GetAxis(HORIZONTAL), UnityEngine.Input.GetAxis(VERTICAL));

        public bool IsAttackButtonUp() => UnityEngine.Input.GetButtonUp(FIRE);
    }
}