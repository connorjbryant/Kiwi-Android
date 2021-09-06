using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    private string removeAds = "com.teamapex.kiwi.remove_ads";
    private string coin100 = "com.teamapex.kiwi.coin_100";
    private string coin500 = "com.teamapex.kiwi.coin_500";
    private string coin1000 = "com.teamapex.kiwi.coin_1000";
    

    public void OnPurchaseComplete(Product product) 
    {
        if (product.definition.id == removeAds)
        {
            Debug.Log("All ads removed!");
        }
        if (product.definition.id == coin100)
        {  
            Debug.Log("You've gained 100 coins!");
        }
        if (product.definition.id == coin500)
        {
            Debug.Log("You've gained 500 coins!");
        }
        if (product.definition.id == coin1000)
        {
            Debug.Log("You've gained 1000 coins!");
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(product.definition.id + " failed because" + failureReason);
    }



     
}
