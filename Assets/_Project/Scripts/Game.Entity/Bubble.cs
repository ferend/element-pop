using System;
using UnityEngine;

namespace Game.Entity
{
    public class Bubble : MonoBehaviour
    {
        private Constants.BubbleColors _color;
        private GridCell _gridPosition;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public Constants.BubbleColors GetBallColor()
        {
            return _color;
        }
        
        public void SetColor(Constants.BubbleColors color)
        {
            _color = color;
            _spriteRenderer.color = Constants.ColorCodes[color];
        }
        
        public GridCell GetGridPosition()
        {
            return _gridPosition;
        }
    
        public void SetGridPosition(GridCell grid)
        {
            _gridPosition = grid;
        }
        
        // public void FixPosition()
        // {
        //     _rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        //     _rb.useGravity = true;
        //     _rb.velocity = Vector3.zero;
        // }

    }
}
