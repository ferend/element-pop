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
        public bool _isMoving;

        private MaterialPropertyBlock mpb;
        private static readonly int propAmount = Shader.PropertyToID("_DisAmount");
        private static readonly int propColorRenderer = Shader.PropertyToID("_Color");
        private static readonly int propDisColorRenderer = Shader.PropertyToID("_DisLineColor");

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

        public MaterialPropertyBlock Mpb
        {
            get
            {
                if (mpb == null)
                {
                    mpb = new MaterialPropertyBlock();
                }
                return mpb;
            }
        }
        
        public void SetColor(Constants.BubbleColors color)
        {
            _color = color;
            _spriteRenderer.color = Constants.ColorCodes[color];
            Mpb.SetColor(propColorRenderer, Constants.ColorCodes[color]);
            Mpb.SetColor(propDisColorRenderer, Constants.ColorCodes[color]);
            _spriteRenderer.SetPropertyBlock(Mpb);
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
            _collider.enabled = false;
            float duration = 0.7f; // duration of the lerp
            float t = 0f; // current time
            while (t < duration)
            {
                t += Time.deltaTime;
                float normalizedTime = t / duration;
                // lerp from 0 to 1 using the normalized time
                float lerpedValue = Mathf.Lerp(0f, 1f, normalizedTime);
                Mpb.SetFloat  ("_DisAmount", lerpedValue);
                _spriteRenderer.SetPropertyBlock(Mpb);
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
                    OnBubbleCollision?.Invoke(this, coll.gameObject.GetComponent<Bubble>().GetGridPosition());
                    OnBubbleMatch?.Invoke(this);
                }
    
                if (coll.gameObject.layer == LayerMask.NameToLayer("Plane"))
                {
                    Destroy(gameObject);
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
