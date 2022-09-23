using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopUp : BasePopUp
{
    [SerializeField] private ShopState shopState;

    [Header("Chiko")]
    [SerializeField] private List<ShopItem> chikoItems;
    [SerializeField] private Button chikoBtn;
    [SerializeField] private Sprite chikoClicked;
    [SerializeField] private Sprite chikoUnclicked;
    private List<GameObject> chikoProducts = new List<GameObject>();

    [Header("Ketti")]
    [SerializeField] private List<ShopItem> kettiItems;
    [SerializeField] private Button kettiBtn;
    [SerializeField] private Sprite kettiClicked;
    [SerializeField] private Sprite kettiUnclicked;
    private List<GameObject> kettiProducts = new List<GameObject>();

    [Header("Beri")]
    [SerializeField] private List<ShopItem> beriItems;
    [SerializeField] private Button beriBtn;
    [SerializeField] private Sprite beriClicked;
    [SerializeField] private Sprite beriUnclicked;
    private List<GameObject> beriProducts = new List<GameObject>();

    [Header("Product")]
    [SerializeField] private GameObject shopProductContainer;
    [SerializeField] private GameObject shopProductPrefab;

    new private void Start()
    {
        base.Start();

        chikoBtn.onClick.AddListener(OnClickChiko);
        kettiBtn.onClick.AddListener(OnClickKetti);
        beriBtn.onClick.AddListener(OnClickBeri);

        // create shop products
        foreach (ShopItem item in chikoItems)
        {
            GameObject shopProduct = Instantiate(shopProductPrefab, shopProductContainer.transform);
            shopProduct.GetComponent<ShopProduct>().SetShopItem(item);
            chikoProducts.Add(shopProduct);
        }

        foreach (ShopItem item in kettiItems)
        {
            GameObject shopProduct = Instantiate(shopProductPrefab, shopProductContainer.transform);
            shopProduct.GetComponent<ShopProduct>().SetShopItem(item);
            kettiProducts.Add(shopProduct);
        }

        foreach (ShopItem item in beriItems)
        {
            GameObject shopProduct = Instantiate(shopProductPrefab, shopProductContainer.transform);
            shopProduct.GetComponent<ShopProduct>().SetShopItem(item);
            beriProducts.Add(shopProduct);
        }
    }

    private void Update()
    {
        HandleShopChange();
    }

    private void HandleShopChange()
    {
        switch (shopState)
        {
            case ShopState.CHIKO:
                chikoBtn.image.sprite = chikoClicked;
                kettiBtn.image.sprite = kettiUnclicked;
                beriBtn.image.sprite = beriUnclicked;

                foreach (GameObject product in chikoProducts)
                {
                    product.SetActive(true);
                }

                foreach (GameObject product in kettiProducts)
                {
                    product.SetActive(false);
                }

                foreach (GameObject product in beriProducts)
                {
                    product.SetActive(false);
                }
                break;

            case ShopState.KETTI:
                chikoBtn.image.sprite = chikoUnclicked;
                kettiBtn.image.sprite = kettiClicked;
                beriBtn.image.sprite = beriUnclicked;

                foreach (GameObject product in chikoProducts)
                {
                    product.SetActive(false);
                }

                foreach (GameObject product in kettiProducts)
                {
                    product.SetActive(true);
                }

                foreach (GameObject product in beriProducts)
                {
                    product.SetActive(false);
                }
                break;

            case ShopState.BERI:
                chikoBtn.image.sprite = chikoUnclicked;
                kettiBtn.image.sprite = kettiUnclicked;
                beriBtn.image.sprite = beriClicked;

                foreach (GameObject product in chikoProducts)
                {
                    product.SetActive(false);
                }

                foreach (GameObject product in kettiProducts)
                {
                    product.SetActive(false);
                }

                foreach (GameObject product in beriProducts)
                {
                    product.SetActive(true);
                }
                break;
        }
    }

    public void SwitchState(ShopState _state)
    {
        shopState = _state;
    }

    public void OnClickChiko()
    {
        SwitchState(ShopState.CHIKO);
    }

    public void OnClickKetti()
    {
        SwitchState(ShopState.KETTI);
    }

    public void OnClickBeri()
    {
        SwitchState(ShopState.BERI);
    }
}

[Serializable]
public struct ShopItem
{
    public string name;
    public Sprite productImage;
    public int price;

}

[Serializable]
public struct Star {
    public Sprite starSprite;
    
}
