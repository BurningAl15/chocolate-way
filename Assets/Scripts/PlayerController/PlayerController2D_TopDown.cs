using System.Collections;
using System.Globalization;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

namespace AldhaDev.Player.TopDown
{
    using Managers;
    
    public enum Direction
    {
        NoDirection, Up, Down, Right, Left
    }

    public class PlayerController2D_TopDown : MonoBehaviour
    {
        public float moveSpeed;
        public bool isMoving;
        private Vector2 _input;

        private Coroutine _currentCoroutine;

        [FormerlySerializedAs("anim")] [SerializeField] private Animator _anim;

        [SerializeField] private float collisionRadius;
        [SerializeField] private LayerMask collisionMask;

        private Transform _playerPos;

        [SerializeField] TextMeshPro textSteps;
        [SerializeField] Transform spriteTransform;
        [SerializeField] ParticleSystem particleSystem;
        [SerializeField] Animator keyboardAnimator;

        [SerializeField] Direction direction = Direction.NoDirection;
        [SerializeField] AnimationCurve animCurve;
        private static readonly int ButtonPressed = Animator.StringToHash("ButtonPressed");


        // public event Action OnEncountered;

        public void Init()
        {
            _playerPos = transform;
            TilesetManager.Current.SetWalked(new Vector3Int(Mathf.FloorToInt(_playerPos.position.x), Mathf.FloorToInt(_playerPos.position.y), 0));
            SaveDirection.Current.AddDirection(this.transform.position);
            UpdateSteps();
        }

        public void HandleUpdate()
        {
            Movement();
        }

        public void HandleUpdate(Direction _)
        {
            if (GameManager.Current.gameState == GameState.Playing)
            {
                if (_ != Direction.NoDirection)
                    Movement(_);
                else
                    keyboardAnimator.SetInteger(ButtonPressed, -1);
            }
        }

        private float CheckMobileDirection_Horizontal(Direction _)
        {
            if (_ == Direction.Left)
                return -1;
            else if (_ == Direction.Right)
                return 1;
            else
                return 0;
        }

        private float CheckMobileDirection_Vertical(Direction _)
        {
            if (_ == Direction.Down)
                return -1;
            else if (_ == Direction.Up)
                return 1;
            else
                return 0;
        }

        private void Movement(Direction _)
        {
            if (!isMoving)
            {
                direction = _;
                // input.x = Input.GetAxisRaw("Horizontal");
                // input.y = Input.GetAxisRaw("Vertical");
                _input.x = CheckMobileDirection_Horizontal(_);
                _input.y = CheckMobileDirection_Vertical(_);

                //W
                if (direction == Direction.Up)
                    keyboardAnimator.SetInteger(ButtonPressed, 0);

                //S
                else if (direction == Direction.Down)
                    keyboardAnimator.SetInteger(ButtonPressed, 3);

                //A
                else if (direction == Direction.Left)
                    keyboardAnimator.SetInteger(ButtonPressed, 1);

                //D
                else if (direction == Direction.Right)
                    keyboardAnimator.SetInteger(ButtonPressed, 2);

                //No Diagonal Movement
                if (_input.x != 0) _input.y = 0;

                if (_input != Vector2.zero)
                {
                    // anim.SetFloat("moveX", input.x);
                    // anim.SetFloat("moveY", input.y);

                    var targetPos = transform.position;
                    targetPos.x += _input.x;
                    targetPos.y += _input.y;

                    // Debug.DrawLine(playerPos.position,targetPos,Color.green);

                    if (IsWalkable(targetPos))
                    {
                        SaveDirection.Current.AddDirection(targetPos);
                        _currentCoroutine ??= StartCoroutine(Move(targetPos));
                    }
                    else
                    {
                        SoundManager.Current.PlaySound(SoundType.Player_Error);
                    }
                }
            }
            // anim.SetBool("isMoving", isMoving);
        }

        private void Movement()
        {
            if (isMoving) return;
            _input.x = Input.GetAxisRaw("Horizontal");
            _input.y = Input.GetAxisRaw("Vertical");

            //W
            if (Input.GetKeyDown(KeyCode.W))
                keyboardAnimator.SetInteger(ButtonPressed, 0);
            else if (Input.GetKeyUp(KeyCode.W))
                keyboardAnimator.SetInteger(ButtonPressed, -1);
            //S
            else if (Input.GetKeyDown(KeyCode.S))
                keyboardAnimator.SetInteger(ButtonPressed, 3);
            else if (Input.GetKeyUp(KeyCode.S))
                keyboardAnimator.SetInteger(ButtonPressed, -1);
            //A
            else if (Input.GetKeyDown(KeyCode.A))
                keyboardAnimator.SetInteger(ButtonPressed, 1);
            else if (Input.GetKeyUp(KeyCode.A))
                keyboardAnimator.SetInteger(ButtonPressed, -1);
            //D
            else if (Input.GetKeyDown(KeyCode.D))
                keyboardAnimator.SetInteger(ButtonPressed, 2);
            else if (Input.GetKeyUp(KeyCode.D))
                keyboardAnimator.SetInteger(ButtonPressed, -1);

            //No Diagonal Movement
            if (_input.x != 0) _input.y = 0;

            if (_input != Vector2.zero)
            {
                // anim.SetFloat("moveX", input.x);
                // anim.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += _input.x;
                targetPos.y += _input.y;

                // Debug.DrawLine(playerPos.position,targetPos,Color.green);

                if (IsWalkable(targetPos))
                {
                    SaveDirection.Current.AddDirection(targetPos);
                    if (_currentCoroutine == null)
                    {
                        _currentCoroutine = StartCoroutine(Move(targetPos));
                    }
                }
                else
                {
                    SoundManager.Current.PlaySound(SoundType.Player_Error);
                }
            }
            // anim.SetBool("isMoving", isMoving);
        }

        private IEnumerator Move(Vector3 targetPos)
        {
            isMoving = true;

            while ((targetPos - _playerPos.position).sqrMagnitude > Mathf.Epsilon)
            {
                _playerPos.position = Vector3.MoveTowards(_playerPos.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
            SoundManager.Current.PlaySound(SoundType.Player_Move);

            TilesetManager.Current.SetWalked(new Vector3Int(Mathf.FloorToInt(targetPos.x), Mathf.FloorToInt(targetPos.y), 0));
            UpdateSteps();
            _playerPos.position = targetPos;
            // anim.SetTrigger("Animate");
            yield return StartCoroutine(AnimateWobble());

            particleSystem.Play();
            isMoving = false;
            // yield return CheckForEncounters();
            _currentCoroutine = null;
        }

        private IEnumerator ResetMove(Vector3 targetPos, bool _ = false)
        {
            isMoving = true;

            while ((targetPos - _playerPos.position).sqrMagnitude > Mathf.Epsilon)
            {
                _playerPos.position = Vector3.MoveTowards(_playerPos.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
            SoundManager.Current.PlaySound(SoundType.Player_Move);
            TilesetManager.Current.ResetWalked(new Vector3Int(Mathf.FloorToInt(targetPos.x), Mathf.FloorToInt(targetPos.y), 0), _);

            UpdateSteps();
            _playerPos.position = targetPos;
            // anim.SetTrigger("Animate");
            yield return StartCoroutine(AnimateWobble());
            particleSystem.Play();
            isMoving = false;
            // yield return CheckForEncounters();
            _currentCoroutine = null;
        }

        private IEnumerator AnimateWobble()
        {
            float animationTime = .25f;
            for (float i = 0; i < animationTime; i += Time.deltaTime)
            {
                _playerPos.localScale = Vector2.one * animCurve.Evaluate(i/animationTime);
                yield return null;
            }
            _playerPos.localScale = Vector2.one * animCurve.Evaluate(1);
            yield return null;
        }

        public void ResetMovement()
        {
            _currentCoroutine ??= StartCoroutine(Reset());
        }

        private IEnumerator Reset()
        {
            while (SaveDirection.Current.Size() > 1)
            {
                yield return StartCoroutine(ResetMove(SaveDirection.Current.GetDirection()));
            }
            yield return StartCoroutine(ResetMove(SaveDirection.Current.GetInitialDirection(), true));


            _currentCoroutine = null;
        }

        void UpdateSteps()
        {
            textSteps.text = TilesetManager.Current.GetWalkablePoints().ToString(CultureInfo.InvariantCulture);
            spriteTransform.localScale = Vector2.one * TilesetManager.Current.GetWalkableScale();
        }

        bool IsWalkable(Vector3 targetPos)
        {
            if (Physics2D.OverlapCircle(targetPos, collisionRadius, collisionMask) != null)
            {
                // print("IS NOT WALKABLE");
                return false;
            }

            return true;
        }

        // IEnumerator CheckForEncounters()
        // {
        //     if (Physics2D.OverlapCircle(playerPos.position, collisionRadius, battleMask) != null)
        //     {
        //         if (Random.Range(1, 101) <= 10)
        //         {
        //             // GameStateManager._instance.GameState_Battle();
        //             // anim.SetBool("isMoving", false);
        //             yield return TransitionManager._instance.TransitionEffect_FadeIn(.5f);
        //             if (OnEncountered != null) OnEncountered();
        //         }
        //     }
        // }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = new Color(1, 0, 0);
        //     Gizmos.DrawSphere(transform.position, collisionRadius);
        // }
    }
}

