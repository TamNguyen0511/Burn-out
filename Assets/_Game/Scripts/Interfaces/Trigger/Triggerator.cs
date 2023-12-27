using System;
using UnityEngine;

namespace _Game.Scripts.Interfaces.Trigger
{
    public class Triggerator : MonoBehaviour
    {
        public float speed;
        private Rigidbody2D rb2d;

        void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            rb2d.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);

            // Try out this delta time method??
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<ITriggerable>() == null) return;
            other.GetComponent<ITriggerable>().TriggerAction(this);
        }
    }
}