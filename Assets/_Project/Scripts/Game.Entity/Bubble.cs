using System;
using UnityEngine;

namespace Game.Entity
{
    public class Bubble : MonoBehaviour
    {
        private Constants.BubbleColors _color;
        private Rigidbody2D _rb;
        private Collider2D _collider;
        private GridCell _gridPosition;
        private SpriteRenderer _spriteRenderer;
        public bool _isMoving;

        public static event Action<Bubble,GridCell> OnBubbleCollision;
        public static event Action<Bubble> OnBubbleMatch;
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
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
        
        public void Shooted(Transform bulletRoot, Vector2 force)
        {
            UnFixPosition();
            transform.SetParent(bulletRoot);
            ShootForce(force);
        }
        
        private void ShootForce(Vector2 force)
        {
            _rb.AddForce(new Vector2(force.x, force.y),ForceMode2D.Impulse);
            //_rigidBody.velocity = new Vector2(force.x, force.y)*Time.fixedDeltaTime*100f; 
           
            //this.myforce = force;
            _isMoving = true;
        }
        
        public void SetNewLayer(string newLayer)
        {
            gameObject.layer = LayerMask.NameToLayer(newLayer);
            _collider.enabled=true;
        }
        
        public void FixPosition()
        {
            _isMoving = false;
            _rb.bodyType = RigidbodyType2D.Static;
        }
        public void UnFixPosition()
        {
            _isMoving = true;
            _rb.bodyType = RigidbodyType2D.Dynamic;
        }

        public void BubbleExplodeEffect()
        {
            Destroy(this.gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            BubbleCollisionHandler(collision);
        }


        private void BubbleCollisionHandler(Collision2D coll)
        {
            if (gameObject.CompareTag("Bullet"))
            {
                
                if (coll.gameObject.CompareTag("Ball"))
                {
                    FixPosition();
                    gameObject.tag = "Ball";
                    //gameObject.layer = LayerMask.NameToLayer("Ball");
                    OnBubbleCollision?.Invoke(this, coll.gameObject.GetComponent<Bubble>().GetGridPosition());
                    OnBubbleMatch?.Invoke(this);
                    //BallManager.Instance.OnShootComplete();
                }
    
                if (coll.gameObject.layer == LayerMask.NameToLayer("Plane"))
                {
                    Destroy(gameObject);
                    //BallManager.Instance.OnShootComplete();

                }
            }
            
            if (coll.gameObject.CompareTag("Ball"))
            {
                if (coll.transform != null)
                {
                    float originalPos = coll.transform.position.z;
                }
            }
        }
        

    }
}
