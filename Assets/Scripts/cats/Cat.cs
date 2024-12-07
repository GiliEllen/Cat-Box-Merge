using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public string color;
    public Sprite LoafSprite;
    public Sprite sitSprite;
    public Sprite StandSprite;
    public bool isClickable = false;
    public CatStatus catStatus = CatStatus.Loafing;
    public BoxAreaManager boxAreaManager;

    void Awake() {
        ChangeCatStatus(CatStatus.Loafing);
    }
    

    public void setClickableTrue() 
    {
        isClickable = true;
        ChangeCatStatus(CatStatus.Standing);
    }

    public void setClickableFalse() 
    {
        isClickable = false;
        ChangeCatStatus(CatStatus.Sitting);
    }

    void OnMouseDown()
    {
        if (!isClickable) {
            return;
        } else {
            //TODO:
            //move cat
            Debug.Log("cat is OK to move");
            boxAreaManager.AddCat(this);
            setClickableFalse();
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
                break;

            case CatStatus.Standing:
                spriteRenderer.sprite = StandSprite;
                break;

            case CatStatus.Loafing:
                spriteRenderer.sprite = LoafSprite;
                spriteRenderer.color = new Color(174f / 255f, 174f / 255f, 174f / 255f, 255f / 255f);
                break;

            default:
                spriteRenderer.sprite = sitSprite;
                break;
            }
         }
    }
}
