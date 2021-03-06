using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

public class PlayerController2D_TopDown : MonoBehaviour
{
    public float moveSpeed;
    public bool isMoving;
    private Vector2 input;

    private Coroutine currentCoroutine = null;

    [SerializeField] private Animator anim;

    [SerializeField] private float collisionRadius;
    [SerializeField] private LayerMask collisionMask;

    // [SerializeField] private LayerMask battleMask;

    private Transform playerPos;

    [SerializeField] TextMeshPro textSteps;
    [SerializeField] Transform spriteTransform;
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] Animator keyboardAnimator;


    // public event Action OnEncountered;

    public void Init()
    {
        playerPos = transform;
        TilesetManager._instance.SetWalked(new Vector3Int(Mathf.FloorToInt(playerPos.position.x), Mathf.FloorToInt(playerPos.position.y), 0));
        SaveDirection._instance.AddDirection(this.transform.position);
        UpdateSteps();
    }

    public void HandleUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //W
            if (Input.GetKeyDown(KeyCode.W))
                keyboardAnimator.SetInteger("ButtonPressed", 0);
            else if (Input.GetKeyUp(KeyCode.W))
                keyboardAnimator.SetInteger("ButtonPressed", -1);
            //S
            else if (Input.GetKeyDown(KeyCode.S))
                keyboardAnimator.SetInteger("ButtonPressed", 3);
            else if (Input.GetKeyUp(KeyCode.S))
                keyboardAnimator.SetInteger("ButtonPressed", -1);
            //A
            else if (Input.GetKeyDown(KeyCode.A))
                keyboardAnimator.SetInteger("ButtonPressed", 1);
            else if (Input.GetKeyUp(KeyCode.A))
                keyboardAnimator.SetInteger("ButtonPressed", -1);
            //D
            else if (Input.GetKeyDown(KeyCode.D))
                keyboardAnimator.SetInteger("ButtonPressed", 2);
            else if (Input.GetKeyUp(KeyCode.D))
                keyboardAnimator.SetInteger("ButtonPressed", -1);

            //No Diagonal Movement
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                // anim.SetFloat("moveX", input.x);
                // anim.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                // Debug.DrawLine(playerPos.position,targetPos,Color.green);

                if (isWalkable(targetPos))
                {
                    SaveDirection._instance.AddDirection(targetPos);
                    if (currentCoroutine == null)
                    {
                        currentCoroutine = StartCoroutine(Move(targetPos));
                    }
                }
                else
                {
                    SoundManager._instance.PlaySound(SoundType.Player_Error);
                }
            }
        }
        // anim.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - playerPos.position).sqrMagnitude > Mathf.Epsilon)
        {
            playerPos.position = Vector3.MoveTowards(playerPos.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        SoundManager._instance.PlaySound(SoundType.Player_Move);

        TilesetManager._instance.SetWalked(new Vector3Int(Mathf.FloorToInt(targetPos.x), Mathf.FloorToInt(targetPos.y), 0));
        UpdateSteps();
        playerPos.position = targetPos;
        anim.SetTrigger("Animate");
        particleSystem.Play();
        isMoving = false;
        // yield return CheckForEncounters();
        currentCoroutine = null;
    }

    IEnumerator ResetMove(Vector3 targetPos, bool _ = false)
    {
        isMoving = true;

        while ((targetPos - playerPos.position).sqrMagnitude > Mathf.Epsilon)
        {
            playerPos.position = Vector3.MoveTowards(playerPos.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        SoundManager._instance.PlaySound(SoundType.Player_Move);
        TilesetManager._instance.ResetWalked(new Vector3Int(Mathf.FloorToInt(targetPos.x), Mathf.FloorToInt(targetPos.y), 0), _);

        UpdateSteps();
        playerPos.position = targetPos;
        anim.SetTrigger("Animate");
        particleSystem.Play();
        isMoving = false;
        // yield return CheckForEncounters();
        currentCoroutine = null;
    }

    public void ResetMovement()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        while (SaveDirection._instance.Size() > 1)
        {
            yield return StartCoroutine(ResetMove(SaveDirection._instance.GetDirection()));
        }
        yield return StartCoroutine(ResetMove(SaveDirection._instance.GetInitialDirection(), true));


        currentCoroutine = null;
    }

    void UpdateSteps()
    {
        textSteps.text = TilesetManager._instance.GetWalkablePoints().ToString();
        spriteTransform.localScale = Vector2.one * TilesetManager._instance.GetWalkableScale();
    }

    bool isWalkable(Vector3 targetPos)
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
