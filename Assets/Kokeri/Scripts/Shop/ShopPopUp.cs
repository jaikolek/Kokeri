using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopUp : BasePopUp
{
    [SerializeField] private Character shopState;

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

    public Action<Character> OnSwitchState;

    private void Awake()
    {
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

    new private void Start()
    {
        base.Start();

        chikoBtn.onClick.AddListener(OnClickChiko);
        kettiBtn.onClick.AddListener(OnClickKetti);
        beriBtn.onClick.AddListener(OnClickBeri);

        OnSwitchState += HandleShopChange;

        SwitchState(Character.CHIKO);
    }

    private void HandleShopChange(Character _shopState)
    {
        switch (_shopState)
        {
            case Character.CHIKO:
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

            case Character.KETTI:
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

            case Character.BERI:
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

    public void SwitchState(Character _state)
    {
        shopState = _state;

        OnSwitchState?.Invoke(shopState);
    }

    public void OnClickChiko()
    {
        SwitchState(Character.CHIKO);
    }

    public void OnClickKetti()
    {
        SwitchState(Character.KETTI);
    }

    public void OnClickBeri()
    {
        SwitchState(Character.BERI);
    }
}

[Serializable]
public struct ShopItem
{
    public string name;
    public Sprite productImage;
    public int price;

}
