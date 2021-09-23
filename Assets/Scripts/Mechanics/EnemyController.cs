﻿using Platformer.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path.
    /// </summary>
    [RequireComponent(typeof(AnimationController), typeof(Collider2D))]
    public class EnemyController : Interactable,ICharacter
    {
        public PatrolPath path;
        public AudioClip ouch;

        internal PatrolPath.Mover mover;
        internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        SpriteRenderer spriteRenderer;

        public Bounds Bounds => _collider.bounds;

    #region MonoBehavior Methods
        void Awake()
        {
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        void Update()
        {
            if (path != null)
            {
                if (mover == null) mover = path.CreateMover(control.maxSpeed * 0.5f);
                control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
            }
            if (transform.position.y < -100)
                Destroy(gameObject);
        }
        #endregion
        #region Player Interaction
        protected override void OnPlayerInteract(PlayerController player)
        {
            PlayerCollision(player);
        }
        void PlayerCollision(PlayerController player)
        {
            var willHurtEnemy = player.Bounds.center.y >= Bounds.max.y;

            if (willHurtEnemy)
            {
                Die();
            }
            else
            {
                player.Die();
            }
        }
        #endregion
        public void Die()
        {
            _collider.enabled = false;
            control.enabled = false;
            if (_audio && ouch)
                _audio.PlayOneShot(ouch);
        }
    }
}