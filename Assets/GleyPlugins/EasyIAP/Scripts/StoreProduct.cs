#if GleyIAPiOS || GleyIAPGooglePlay || GleyIAPAmazon || GleyIAPMacOS
#define GleyIAPEnabled
#endif

#if GleyIAPEnabled
using UnityEngine.Purchasing;
#endif

public enum ProductType
{
    Consumable = 0,
    NonConsumable = 1,
    Subscription = 2
}

public enum IAPOperationStatus
{
    Success,
    Fail
}

[System.Serializable]
public class StoreProduct
{
    public string productName;
    public ProductType productType;
    public string idGooglePlay;
    public string idAmazon;
    public string idIOS;
    public string idMac;
    public int value;
    public string localizedPriceString = "-";
    public int price;
    public string isoCurrencyCode;
    internal string localizedDescription;
    internal string localizedTitle;
    internal bool active;
    internal SubscriptionInfo subscriptionInfo;

    public StoreProduct(string productName, ProductType productType, int value, string idGooglePlay, string idIOS, string idAmazon, string idMac)
    {
        this.productName = productName;
        this.productType = productType;
        this.value = value;
        this.idGooglePlay = idGooglePlay;
        this.idIOS = idIOS;
        this.idAmazon = idAmazon;
        this.idMac = idMac;
    }


    public StoreProduct()
    {
        productName = "";
        idGooglePlay = "";
        idIOS = "";
        idAmazon = "";
        idMac = "";
        productType = ProductType.Consumable;
    }

#if GleyIAPEnabled
    internal UnityEngine.Purchasing.ProductType GetProductType()
    {
        return (UnityEngine.Purchasing.ProductType)(int)productType;
    }
#endif

    internal string GetStoreID()
    {
#if GleyIAPMacOS
        return idMac;
#elif GleyIAPiOS
        return idIOS;
#elif GleyIAPGooglePlay
        return idGooglePlay;
#elif GleyIAPAmazon
        return idAmazon;
#else
        return "";
#endif
    }
}

#if !GleyIAPEnabled
internal class SubscriptionInfo
{
}
#endif