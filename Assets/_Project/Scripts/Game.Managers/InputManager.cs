using Game.Helpers;
using UnityEngine;

namespace Game.Managers
{
    public class InputManager : Manager
    {
        [SerializeField] private BaseInput _inputType;
       
        public BaseInput Input
        {
            get => _inputType;
            set => _inputType = value;
        }
        
        public override void Setup()
        {
        }
        
        public override void Dispose()
        {
            base.Dispose();

        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);
            _inputType.KeyboardControls();
        }
    }
}
