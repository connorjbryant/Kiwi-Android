using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiOutfit : MonoBehaviour
{
    public PlayerMove move;
    public SpriteRenderer originalKiwiSprite;
    public bool is_Hat_Child_Object;
    private SpriteRenderer currentSprite;
    

    [Header("Anime Sprites")]
    public bool isAnimatedSkin;
    public Animator currentAnimator;
    public Animation currentAnimation;
    public AnimationClip OriginalKiwiAnimation;
    public AnimationClip MamaKiwiAnimation;
    public AnimationClip PapaKiwiAnimation;
    public AnimationClip ProudKiwiAnimation;
    public RuntimeAnimatorController OriginalKiwiController;
    public RuntimeAnimatorController MamaKiwiController;
    public RuntimeAnimatorController DadKiwiController;
    public RuntimeAnimatorController ProudKiwiController;

    // Start is called before the first frame update
    void Start()
    {
        //Stop if you're a witch
        if (move.isWitch) return;
        if (DressingRoom.static_hat_list == null ||
            DressingRoom.static_outfit_list == null) return;

        print("is_Hat_Child_Object: " + is_Hat_Child_Object);
        print("PlayerPrefs.GetInt(isApplyedHat): " + PlayerPrefs.GetInt("isApplyedHat"));

        if (is_Hat_Child_Object && PlayerPrefs.GetInt("isApplyedHat") == 1)
        {
            //HAT!!!
            currentSprite = DressingRoom.static_hat_list[PlayerPrefs.GetInt("appliedSkinID")].gameObject.
                GetComponent<SpriteRenderer>();
            GetComponent<SpriteRenderer>().sprite = currentSprite.sprite;

            print("My hat name is: " + currentSprite.name);

            if (currentSprite.name == "0_No Hat")
                GetComponent<SpriteRenderer>().sprite = null;
        }
        else if (!is_Hat_Child_Object && PlayerPrefs.GetInt("isApplyedHat") == 0)
        {
            //OUTFIT

            //If it is animated
            if (PlayerPrefs.GetInt("appliedSkinID") == 10 ||
                PlayerPrefs.GetInt("appliedSkinID") == 11 ||
                PlayerPrefs.GetInt("appliedSkinID") == 12)
                isAnimatedSkin = true;

            if (isAnimatedSkin)
            {
                print("Setting Animated Skin");
                if (PlayerPrefs.GetInt("appliedSkinID") == 10)
                {
                    currentAnimator.runtimeAnimatorController = MamaKiwiController;
                    currentAnimation.clip = MamaKiwiAnimation;
                }
                else if (PlayerPrefs.GetInt("appliedSkinID") == 11)
                {
                    currentAnimator.runtimeAnimatorController = DadKiwiController;
                    currentAnimation.clip = PapaKiwiAnimation;
                }
                else if (PlayerPrefs.GetInt("appliedSkinID") == 12)
                {
                    currentAnimator.runtimeAnimatorController = ProudKiwiController;
                    currentAnimation.clip = ProudKiwiAnimation;
                }
            }
            else
            {
                currentAnimator.runtimeAnimatorController = OriginalKiwiController; ;
                currentAnimation.clip = OriginalKiwiAnimation;

                currentSprite = DressingRoom.static_outfit_list[PlayerPrefs.GetInt("appliedSkinID")].gameObject.
                GetComponent<SpriteRenderer>();
                GetComponent<SpriteRenderer>().sprite = currentSprite.sprite;

                print("My outfit name is: " + currentSprite.name);

                if (currentSprite.name == "0_No Outfit")
                    GetComponent<SpriteRenderer>().sprite = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
