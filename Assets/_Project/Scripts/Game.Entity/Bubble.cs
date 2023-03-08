using System;
using System.Collections;
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
        private Material _material;
        public bool _isMoving;

        public static event Action<Bubble,GridCell> OnBubbleCollision;
        public static event Action<Bubble> OnBubbleMatch;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            _material = _spriteRenderer.material;
        }
        

        public Constants.BubbleColors GetBallColor()
        {
            return _color;
        }
        
        public void SetColor(Constants.BubbleColors color)
        {
            _color = color;
            _spriteRenderer.color = Constants.ColorCodes[color];
            _material.SetColor("_Color", Constants.ColorCodes[color]);
            _material.SetColor("_DisLineColor", Constants.ColorCodes[color]);
            Debug.Log(_material.GetFloat("_DisAmount"));

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

        public void PopBubble()
        {
            Destroy(this.gameObject);
        }
        
        private void ShootForce(Vector2 force)
        {
            //_rb.AddForce(new Vector2(force.x, force.y),ForceMode2D.Impulse);
            _rb.velocity = new Vector2(force.x, force.y)*Time.fixedDeltaTime * 40f; 
           
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

        public IEnumerator BubbleExplodeEffect()
        {

            float duration = 1f; // duration of the lerp
            float t = 0f; // current time
            while (t < duration)
            {
                t += Time.deltaTime;
                float normalizedTime = t / duration;
                // lerp from 0 to 1 using the normalized time
                float lerpedValue = Mathf.Lerp(0f, 1f, normalizedTime);
                _material.SetFloat  ("_DisAmount", lerpedValue);
                yield return null; // wait for next frame
            }
            // execute code after lerping is done
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
