using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CosmeticButton : MonoBehaviour
{
    private DressingRoom dressingRoom;
    public Cosmetic cosmeticSkin;
    public GameObject WingOnlySprite;
    private GameObject kiwiSprite;

    // Start is called before the first frame update
    void Start()
    {
        dressingRoom = GameObject.Find("DressingRoomScript").GetComponent<DressingRoom>();
        kiwiSprite = GameObject.Find("DressingRoomCanvas").transform.Find("Kiwi").gameObject;

        //Scaling the button
        GetComponent<RectTransform>().localScale = new Vector3(1, 1.25f, 1);

        //Updating the Button itself
        GetComponent<Image>().sprite = cosmeticSkin.gameObject.GetComponent<SpriteRenderer>().sprite;

        if (!cosmeticSkin.isOwned)
        {
            //Don't own the skin
            if (cosmeticSkin.cosmetic_Outfit_ID == 10 || cosmeticSkin.cosmetic_Outfit_ID == 11)
                transform.Find("SkinCost").GetComponent<TextMeshProUGUI>().text = "Beat the Game";
            else
                transform.Find("SkinCost").GetComponent<TextMeshProUGUI>().text = cosmeticSkin.cost.ToString() + " Coins";
            transform.Find("X").gameObject.SetActive(true);
        }
        else
        {
            //Own the skin
            transform.Find("SkinCost").GetComponent<TextMeshProUGUI>().text = "";
        }

        //Remove Wing sprite for animated Kiwis
        if (cosmeticSkin.cosmetic_Outfit_ID == 10 ||
            cosmeticSkin.cosmetic_Outfit_ID == 11 ||
            cosmeticSkin.cosmetic_Outfit_ID == 12)
        {
            WingOnlySprite.SetActive(false);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            transform.Find("CurrentEquipStar").gameObject.SetActive(false);
        }
    }

    public void click()
    {
        dressingRoom.audioSource.clip = dressingRoom.clickSound;
        dressingRoom.audioSource.Play();

        dressingRoom.isHat = cosmeticSkin.isHat;
        dressingRoom.currentSkinName = cosmeticSkin.cosmeticName;
        DressingRoom.currentSkinImage.sprite = 
            cosmeticSkin.gameObject.GetComponent<SpriteRenderer>().sprite;
        DressingRoom.currentCosmeticButton = this;

        dressingRoom.updateSkinSprite();
        transform.Find("CurrentEquipStar").gameObject.SetActive(true);

        if (!cosmeticSkin.isOwned)
        {
            dressingRoom.Equip_Button.SetActive(false);

            if (cosmeticSkin.cosmetic_Outfit_ID == 10 || cosmeticSkin.cosmetic_Outfit_ID == 11)
                dressingRoom.Buy_Button.SetActive(false);
            else
                dressingRoom.Buy_Button.SetActive(true);
        }
        else
        {
            dressingRoom.Equip_Button.SetActive(true);
            dressingRoom.Buy_Button.SetActive(false);

            DressingRoom.isApplyedHat = cosmeticSkin.isHat;
            if (cosmeticSkin.isHat)
            {
                DressingRoom.appliedSkinID = cosmeticSkin.cosmetic_Hat_ID;
                //kiwiSprite.GetComponent<Image>().enabled = true;
            }
            else
            {
                DressingRoom.appliedSkinID = cosmeticSkin.cosmetic_Outfit_ID;
                //kiwiSprite.GetComponent<Image>().enabled = false;
            }
        }
    }
}
