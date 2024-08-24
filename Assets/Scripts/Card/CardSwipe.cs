using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardBehaviour : MonoBehaviour
{
    public static event System.Action<CardAction> OnCardDestroyed;

    private enum AnimationState
    {
        Idle,
        Converging,
        FlyingAway,
        Revealing
    }

    private const float _animationDuration = 0.4f;

    public float swipeThreshold = 0.7f;
    public Vector3 snapPosition;
    public Vector3 snapRotationAngles;
    
    public TextMeshProUGUI leftActionText;   
    public TextMeshProUGUI rightActionText; 
    public Image cardBackSpriteRenderer;
    public Image cardFrontSpriteRenderer;
    public Image cardImageSpriteRenderer;

    public Sprite characterImg;

    private Card cardData;  // Instance of Card class holding the data

    public void Initialize(Card card, Sprite characterImgret)
    {
        cardData = card;
        characterImg = characterImgret;
        UpdateCardUI();

        transform.localEulerAngles = new Vector3(0, 180, 0);

        snapRotationAngles.y = 0.0f;

        CardDescriptionDisplay.SetDescription(card.CardText, card.CharacterName);
 

    }

    private void UpdateCardUI()
    {
        leftActionText.text = cardData.LeftSwipeText;
        rightActionText.text = cardData.RightSwipeText;
        cardImageSpriteRenderer.sprite = characterImg;

        snapRotationAngles.y = 0.0f;

        // Initialize animation state
        animationStartPosition = transform.position;
        animationStartRotationAngles = transform.eulerAngles;
        animationStartTime = Time.time;
        animationState = AnimationState.Revealing;

        SetTextAlpha(leftActionText, 0.0f);
        SetTextAlpha(rightActionText, 0.0f);
    }



    private Vector3 dragStartPosition;
    private Vector3 dragStartPointerPosition;
    private Vector3 animationStartPosition;
    private Vector3 animationStartRotationAngles;
    private float animationStartTime;
    private AnimationState animationState = AnimationState.Idle;
    
    private bool animationSuspended;

    private void Start()
    {

        animationStartPosition = transform.position;
        animationStartRotationAngles = transform.eulerAngles;
        SetTextAlpha(leftActionText, 0.0f);
        SetTextAlpha(rightActionText, 0.0f);
        ShowVisibleSide();
    }

    private void Update()
    {
        if (animationState != AnimationState.Idle && !animationSuspended)
        {
            float animationProgress = (Time.time - animationStartTime) / _animationDuration;
            float scaledProgress = ScaleProgress(animationProgress);

            if (scaledProgress > 1.0f || animationProgress > 1.0f)
            {
                transform.position = snapPosition;
                transform.eulerAngles = snapRotationAngles;

                if (animationState == AnimationState.Revealing)
                {
                    snapRotationAngles.y -= 0.0f;
                }

                if (animationState == AnimationState.FlyingAway)
                {
                    Destroy(gameObject);
                }
                else
                {
                    animationState = AnimationState.Idle;
                }
            }
            else
            {
                transform.position = Vector3.Lerp(animationStartPosition, snapPosition, scaledProgress);
                transform.eulerAngles = Vector3.Lerp(animationStartRotationAngles, snapRotationAngles, scaledProgress);
                
                ShowVisibleSide();
            }

        }
    }

    public void BeginDrag()
    {
        animationSuspended = true;
        dragStartPosition = transform.position;
        dragStartPointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void Drag()
    {
        Vector3 displacement = Camera.main.ScreenToWorldPoint(Input.mousePosition) - dragStartPointerPosition;
        displacement.z = 0.0f;
        transform.position = dragStartPosition + displacement;

        float alphaCoord = (transform.position.x - snapPosition.x) / (swipeThreshold / 2);
        SetTextAlpha(leftActionText, Mathf.Clamp01(-alphaCoord));
        SetTextAlpha(rightActionText, Mathf.Clamp01(alphaCoord));
    }

    private void EndDrag()
    {
        animationStartPosition = transform.position;
        animationStartRotationAngles = transform.eulerAngles;
        animationStartTime = Time.time;

        if (animationState != AnimationState.FlyingAway)
        {
            if (transform.position.x < snapPosition.x - swipeThreshold)
            {
                Vector3 displacement = animationStartPosition - snapPosition;
                snapPosition += displacement.normalized * 10.0f;
                snapRotationAngles = transform.eulerAngles;
                animationState = AnimationState.FlyingAway;
                OnCardDestroyed?.Invoke(cardData.LeftSwipeAction);

            }
            else if (transform.position.x > snapPosition.x + swipeThreshold)
            {
                Vector3 displacement = animationStartPosition - snapPosition;
                snapPosition += displacement.normalized * 10.0f;
                snapRotationAngles = transform.eulerAngles;
                animationState = AnimationState.FlyingAway;
                OnCardDestroyed?.Invoke(cardData.RightSwipeAction);
            }
            else
            {
                animationState = AnimationState.Converging;
            }
        }
        animationSuspended = false;
    }


    private void SetTextAlpha(TextMeshProUGUI textMesh, float alpha)
    {
        if (textMesh != null)
        {
            Color color = textMesh.color;
            color.a = Mathf.Clamp01(alpha);
            textMesh.color = color;
        }
    }

    private void ShowVisibleSide() {
        bool isFacingCamera= Vector3.Dot(gameObject.transform.forward, Camera.main.transform.forward) > 0.0f;
        cardBackSpriteRenderer.enabled = !isFacingCamera;
        cardFrontSpriteRenderer.enabled = isFacingCamera;
        cardImageSpriteRenderer.enabled = isFacingCamera;
        leftActionText.enabled = isFacingCamera;
        rightActionText.enabled = isFacingCamera;
    }

    private float ScaleProgress(float animationProgress)
    {
        switch (animationState)
        {
            case AnimationState.Converging:
                return 0.15f * Mathf.Pow(animationProgress, 3.0f)
                       - 1.5f * Mathf.Pow(animationProgress, 2.0f)
                       + 2.38f * animationProgress;
            case AnimationState.FlyingAway:
                return 1.5f * Mathf.Pow(animationProgress, 3.0f)
                       + 0.55f * animationProgress;
            default:
                return animationProgress;
        }
    }
}
