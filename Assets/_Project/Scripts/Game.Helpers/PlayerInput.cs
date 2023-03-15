using System;
using Game.Entity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Helpers
{
    public class PlayerInput : BaseInput
    {
        private float _timer;
        public bool _canShoot; 
        private Transform _bulletsRoot;
        
        [SerializeField] private  Transform bulletTransform;
        [SerializeField] private Bubble ballPrefabs;
        [SerializeField] private Transform _pivot;
        
        private Vector2 mousePosition;

        public Bubble Bullet { get; set; }

        protected float triggerInterval = 0;

        private void Awake()
        {
            _bulletsRoot = _pivot.GetChild(0);
            _canShoot = true;
            LoadBullets(GenerateBallAsBullet());
        }

        public override void OnDrag()
        {
            _timer += Time.deltaTime;
            if (_timer >= triggerInterval)
            {
                _timer = 0;
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Rotation(mousePosition);
            }
        }

        public override void OnUp()
        {
            _timer = 0f;
            Shooting();
        }
        
        public void Shooting()
        {
            if (_canShoot )
            {
                Vector3 force = mousePosition.normalized * 20;
                Bullet.Shooted(_bulletsRoot, force);
                LoadBullets(GenerateBallAsBullet());
            }
        }
        
        public void Rotation(Vector3 position)
        {
            Vector3 nvec = new Vector3(position.x, position.y, transform.position.z);
            Vector3 direction = nvec - transform.position;
            if (Vector3.Angle(Vector3.up, direction) < 83)
            {
                transform.up = direction;
                mousePosition = direction;
            }
        }
        
        private void LoadBullets(Bubble newBullet)
        {
            LoadDoneBullets(newBullet);
        }
        
        private void LoadDoneBullets(Bubble first)
        {
            Bullet = first;
            Transform transform1;
            (transform1 = first.transform).SetParent(bulletTransform);
            transform1.localPosition = Vector3.zero;
        }
        
        private Bubble GenerateBallAsBullet()
        {
            var ball = InstantiateBubble(RandomBallColor(0,8));
            ball.tag = "Bullet";
            ball.SetNewLayer("Bullet");
            ball.FixPosition();
            return ball;
        }
        private Constants.BubbleColors RandomBallColor(int num, int num2)
        {
            return (Constants.BubbleColors) Random.Range(num, num2);
        }

        private Bubble InstantiateBubble(Constants.BubbleColors colors)
        {
            Bubble go = Instantiate(ballPrefabs);
            go.SetColor(colors);
            //go.gameObject.tag = "Ball";
            return go;
        }
        
    }
}
