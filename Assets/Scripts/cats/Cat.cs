using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public string color;
    public Sprite LoafSprite;
    public Sprite sitSprite;
    public Sprite StandSprite;
    public bool isClickable = true;
    public CatStatus catStatus = CatStatus.Loafing;
    public BoxAreaManager boxAreaManager;
    public int catId;
    public Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            Debug.Log("Animator component found!");
        }
        else
        {
            Debug.LogError("Animator component not found on this GameObject!");
        }
        
    }
    

    public void setClickableTrue() 
    {
        isClickable = true;
        ChangeCatStatus(CatStatus.Standing);
        animator.SetBool("isClickable", true);
        animator.SetBool("isSitting", false);
        animator.SetBool("isLoafing", false);
    }

    public void setClickableFalse(CatStatus status) 
    {
        isClickable = false;
        ChangeCatStatus(status);
        animator.SetBool("isClickable", false);
        if (status == CatStatus.Sitting) {
            animator.SetBool("isSitting", true);
            animator.SetBool("isLoafing", false);
        } else if (status ==  CatStatus.Loafing) {
            animator.SetBool("isSitting", false);
            animator.SetBool("isLoafing", true);
        }
    }

    void OnMouseDown()
    {
        if (!isClickable) {
             Debug.Log("cat is BLOCK");
            return;
        } else {
            //TODO:
            //move cat
            Debug.Log("cat is OK to move");
            Debug.Log(this);
            boxAreaManager.AddCat(this);
            setClickableFalse(CatStatus.Sitting);
        }
    }

    public void ChangeCatStatus(CatStatus status) {
        catStatus = status;
        // Debug.Log("cat " + color + " is " + catStatus);
        SetSprite(status);
    }

    public void SetSprite(CatStatus status) {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
         if (spriteRenderer != null){
            switch (catStatus)
            {
            case CatStatus.Sitting:
                spriteRenderer.sprite = sitSprite;
                setColor(spriteRenderer, "white");
                break;

            case CatStatus.Standing:
                spriteRenderer.sprite = StandSprite;
                setColor(spriteRenderer, "white");
                break;

            case CatStatus.Loafing:
                spriteRenderer.sprite = LoafSprite;
                setColor(spriteRenderer, "gray");
                break;

            default:
                spriteRenderer.sprite = sitSprite;
                break;
            }
         }
    }

    public void setColor(SpriteRenderer spriteRenderer, string color) {
        if (color == "gray") {
            spriteRenderer.color = new Color(174f / 255f, 174f / 255f, 174f / 255f, 255f / 255f);
        } else if (color == "white") {
             spriteRenderer.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        }
    }
}
