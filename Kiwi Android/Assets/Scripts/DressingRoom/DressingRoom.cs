using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DressingRoom : MonoBehaviour
{
    public static DressingRoom instance;
    public LeveLoader levelLoader;

    //For Kiwi Wearing it in-game
    public static bool isApplyedHat;
    public static int appliedSkinID;

    public bool isHat; //If putting on a hat or a outfit
    public static CosmeticButton currentCosmeticButton;
    public static Image currentSkinImage;
    public SpriteRenderer noneImage;
    public string currentSkinName;
    public static bool equipedHat;

    public GameObject[] hat_list;
    public static GameObject[] static_hat_list;
    public Transform Hat_ButtonListContent_transform; //Parent of Hat Button Prefabs
    public GameObject Hat_Button_Prefab;

    public GameObject[] outfit_list;
    public static GameObject[] static_outfit_list;
    public Transform Outfit_ButtonListContent_transform; //Parent of Outfit Button Prefabs
    public GameObject Outfit_Button_Prefab;

    public Image kiwiSprite; //The kiwi image on the left of screen
    public GameObject skins_name_obj; //The text above the skin selection UI - Drag it in
    public GameObject skins_Hat_obj; //The Outfit Image attached to Kiwi- Drag it in
    public GameObject skins_Outfit_obj; //The Outfit Image attached to Kiwi- Drag it in
    public GameObject Owned_Button; //Drag the button
    public GameObject Buy_Button; //Drag the button
    public GameObject Equip_Button; //Drag the button

    public GameObject LockedSkin;
    public GameObject LockedButton;
    public AdManager ad;

    [Header("AudioSource")]
    public AudioSource audioSource;
    public AudioClip equipSound;
    public AudioClip clickSound;
    public AudioClip purchaseSound;
    public AudioClip nonEnoughCoinsSound;

    //This is to save bought items
    static List<string> hatNames = new List<string>();
    static List<string> outfitNames = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LeveLoader>();
        audioSource = GetComponent<AudioSource>();
        ad = FindObjectOfType<AdManager>();

        static_hat_list = hat_list;
        static_outfit_list = outfit_list;

        //------------------------------------------

        if (SceneManager.GetActiveScene().name != "DressingRoom") return;

        for (int i = 0; i < hat_list.Length; i++)
        {
            GameObject hat_button = Instantiate(Hat_Button_Prefab);
            hat_button.transform.SetParent(Hat_ButtonListContent_transform);
            hat_list[i].GetComponent<Cosmetic>().cosmetic_Hat_ID = i;
            hat_button.GetComponent<CosmeticButton>().cosmeticSkin = hat_list[i].GetComponent<Cosmetic>();
        }
        for (int i = 0; i < outfit_list.Length; i++)
        {
            GameObject outfit_button = Instantiate(Outfit_Button_Prefab);
            outfit_button.transform.SetParent(Outfit_ButtonListContent_transform);
            outfit_list[i].GetComponent<Cosmetic>().cosmetic_Outfit_ID = i;
            outfit_button.GetComponent<CosmeticButton>().cosmeticSkin = outfit_list[i].GetComponent<Cosmetic>();
        }

        currentSkinImage = GetComponent<Image>();
        if (PlayerPrefs.GetInt("isApplyedHat") == 1)
            isApplyedHat = true;
        else
            isApplyedHat = false;


        if (isApplyedHat)
        {
            currentSkinImage.sprite = hat_list[PlayerPrefs.GetInt("appliedSkinID")].gameObject.
                GetComponent<SpriteRenderer>().sprite;
            currentSkinName = hat_list[PlayerPrefs.GetInt("appliedSkinID")].gameObject.
                GetComponent<Cosmetic>().cosmeticName;
            kiwiSprite.enabled = true;
        }
        else
        {
            currentSkinImage.sprite = outfit_list[PlayerPrefs.GetInt("appliedSkinID")].gameObject.
                GetComponent<SpriteRenderer>().sprite;
            currentSkinName = outfit_list[PlayerPrefs.GetInt("appliedSkinID")].gameObject.
                GetComponent<Cosmetic>().cosmeticName;
            kiwiSprite.enabled = true;
        }
            

        noneImage.sprite = GetComponent<SpriteRenderer>().sprite;
        isHat = isApplyedHat;
        updateSkinSprite();


        //Reset all Hats and Skins:
        /*
        foreach (GameObject hat in hat_list)
        {
            if (hat.GetComponent<Cosmetic>().isOwned == false)
                PlayerPrefs.SetInt("Own_Hat_ID_" + hat.GetComponent<Cosmetic>().cosmetic_Hat_ID, 0);
        }
        foreach (GameObject outfit in outfit_list)
        {
            if (outfit.GetComponent<Cosmetic>().isOwned == false)
                PlayerPrefs.SetInt("Own_Outfit_ID_" + outfit.GetComponent<Cosmetic>().cosmetic_Outfit_ID, 0);
        }
        */


        //Save Data for Hat List
        foreach (GameObject hat in hat_list)
        {
            //If you don't own it, check the playerpref save data. 
            if (hat.GetComponent<Cosmetic>().isOwned == false)
                if (PlayerPrefs.GetInt("Own_Hat_ID_" + hat.GetComponent<Cosmetic>().cosmetic_Hat_ID) == 1)
                    hat.GetComponent<Cosmetic>().isOwned = true;
        }

        //Save Data for Skin List
        foreach (GameObject outfit in outfit_list)
        {
            if (outfit.GetComponent<Cosmetic>().isOwned == false)
                if (PlayerPrefs.GetInt("Own_Outfit_ID_" + outfit.GetComponent<Cosmetic>().cosmetic_Outfit_ID) == 1)
                    outfit.GetComponent<Cosmetic>().isOwned = true;

            //If you beat the game, papa and mama kiwi is unlocked
            if (outfit.GetComponent<Cosmetic>().cosmetic_Outfit_ID == 10 ||
                outfit.GetComponent<Cosmetic>().cosmetic_Outfit_ID == 11)
            {
                if (PlayerPrefs.GetInt("numberOfUnlockedStages") >= 4)
                    outfit.GetComponent<Cosmetic>().isOwned = true;
                else
                    outfit.GetComponent<Cosmetic>().isOwned = false;
            }
            
        }

        if (PlayerPrefs.GetInt("WatchedTouristSkinAd") == 1)
        {
            AdManager.instance.ad = false;
        }
    }

    void Update()
    {
        if (SceneManager.GetSceneByName("DressingRoom")
            != SceneManager.GetActiveScene())
            return;

        if (AdManager.instance.ad == false || PlayerPrefs.GetInt("DisabledAds") == 1)
        {
            AdManager.instance.ad = false;
            skins_Outfit_obj.SetActive(true);
            LockedSkin.SetActive(false);
            kiwiSprite.enabled = true;
            PlayerPrefs.SetInt("WatchedTouristSkinAd", 1);
        }

        if (LockedButton == null)
            return;

        if (AdManager.instance.ad == true && currentSkinName == "Tourist")
        {
            LockedButton.SetActive(false);
        }
        else
        {
            LockedButton.SetActive(true);
        }
    }

    public void updateSkinSprite()
    {
        skins_name_obj.GetComponent<TextMeshProUGUI>().text = currentSkinName;
        if (isHat)
        {
            skins_Hat_obj.GetComponent<Image>().sprite = currentSkinImage.sprite;
            skins_Outfit_obj.GetComponent<Image>().sprite = noneImage.sprite;
        }
        else
        {
            skins_Outfit_obj.GetComponent<Image>().sprite = currentSkinImage.sprite;
            skins_Hat_obj.GetComponent<Image>().sprite = noneImage.sprite;
        }

        if (currentSkinName == "Tourist")
        {
            //Need a condition so that after you watched the video, turn it back on.
            skins_Outfit_obj.SetActive(false);
            kiwiSprite.enabled = false;
            LockedSkin.SetActive(true);
            LockedButton.SetActive(true);
            Debug.Log("Tourist selected");
        }
        else
        {
            kiwiSprite.enabled = true;
            skins_Outfit_obj.SetActive(true);
            LockedSkin.SetActive(false);
            LockedButton.SetActive(true);
        }
    }

    public void backButton()
    {
        levelLoader.LoadNextLevel("StartMenu");
    }

    public void ApplyChanges()
    {
        int temp;
        if (isApplyedHat) temp = 1; else temp = 0;
        PlayerPrefs.SetInt("isApplyedHat", temp);
        PlayerPrefs.SetInt("appliedSkinID", appliedSkinID);
        //updateSkinSprite();

        audioSource.clip = equipSound;
        audioSource.Play();
    }

    public void buySkin()
    {
        if (currentCosmeticButton.cosmeticSkin.isOwned) return;
        if (currentCosmeticButton.cosmeticSkin.cosmetic_Outfit_ID == 10 ||
            currentCosmeticButton.cosmeticSkin.cosmetic_Outfit_ID == 11) 
            return;

        //If you don't have money, return;
        if (PlayerPrefs.GetInt("numCoins") < currentCosmeticButton.cosmeticSkin.cost)
        {
            audioSource.clip = nonEnoughCoinsSound;
            audioSource.Play();
            return;
        }

        //If you have money, decrease the money
        audioSource.clip = purchaseSound;
        audioSource.Play();

        PlayerPrefs.SetInt("numCoins", PlayerPrefs.GetInt("numCoins") - currentCosmeticButton.cosmeticSkin.cost);

        currentCosmeticButton.cosmeticSkin.isOwned = true;
        if (currentCosmeticButton.cosmeticSkin.isHat)
            PlayerPrefs.SetInt("Own_Hat_ID_" + currentCosmeticButton.cosmeticSkin.cosmetic_Hat_ID, 1);
        else
            PlayerPrefs.SetInt("Own_Outfit_ID_" + currentCosmeticButton.cosmeticSkin.cosmetic_Outfit_ID, 1);

        currentCosmeticButton.gameObject.transform.Find("X").gameObject.SetActive(false);
        currentCosmeticButton.gameObject.transform.Find("SkinCost").gameObject.SetActive(false);
        equipedHat = isHat;

        currentCosmeticButton.click();
    }
}
