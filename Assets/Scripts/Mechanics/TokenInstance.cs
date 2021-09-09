
using UnityEngine;


namespace Platformer.Mechanics
{
    /// <summary>
    /// This class contains the data required for implementing token collection mechanics.
    /// It does not perform animation of the token, this is handled in a batch by the 
    /// TokenController in the scene.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class TokenInstance : Interactable
    {
        [Tooltip("The audio when the object is picked up")]
        public AudioClip tokenCollectAudio;
        [Tooltip("If true, animation will start at a random position in the sequence.")]
        public bool randomAnimationStartTime = false;
        [Tooltip("List of frames that make up the animation.")]
        public Sprite[] idleAnimation, collectedAnimation;
        [Tooltip("Frames per second at which tokens are animated.")]
        public float frameRate = 12;

        internal Sprite[] sprites = new Sprite[0];
        internal SpriteRenderer _renderer;
        internal int frame = 0;

        float nextFrameTime = 0;

        void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            if (randomAnimationStartTime)
                frame = Random.Range(0, sprites.Length);
            sprites = idleAnimation;
        }
        void Update()
        {
            if (Time.time - nextFrameTime > (1f / frameRate))
            {
                _renderer.sprite = sprites[frame];
                if (hasAlreadyInteractedOneTime && frame == sprites.Length - 1)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    frame = (frame + 1) % sprites.Length;
                }
                nextFrameTime += 1f / frameRate;
            }
        }
        protected override void OnPlayerInteract(PlayerController player)
        {
            //disable the gameObject and remove it from the controller update list.
            frame = 0;
            sprites = collectedAnimation;
            AudioSource.PlayClipAtPoint(tokenCollectAudio, transform.position);
        }

    }
}