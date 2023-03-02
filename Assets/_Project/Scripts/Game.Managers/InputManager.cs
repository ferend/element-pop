using System;
using Game.Helpers;
using UnityEngine;
using Input = UnityEngine.Input;

namespace Game.Managers
{
    public class InputManager : Manager
    {
        [SerializeField] private BaseInput _inputType;
       
        public BaseInput BaseInput
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
            if (Input.GetMouseButtonDown(0))
                BaseInput.OnDown();
            else if (Input.GetMouseButton(0))
                BaseInput.OnDrag();
            else if (Input.GetMouseButtonUp(0))
                BaseInput.OnUp();
        }
    }
}
