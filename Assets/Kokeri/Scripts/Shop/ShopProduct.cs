using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopProduct : MonoBehaviour
{
    [SerializeField] private Image productImage;
    [SerializeField] private TextMeshProUGUI priceTxt;

    public void SetShopItem(ShopItem item)
    {
        productImage.sprite = item.productImage;
        priceTxt.text = item.price.ToString();
    }
}
