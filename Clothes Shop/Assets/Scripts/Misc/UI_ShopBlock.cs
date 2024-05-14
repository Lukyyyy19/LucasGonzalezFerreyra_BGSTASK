using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShopBlock : MonoBehaviour
{
   private Clothes _clothe;
   [SerializeField] private Image _clotheImage;
   [SerializeField] private TextMeshProUGUI _clotheName;
   [SerializeField] private TextMeshProUGUI _clothePrice;
   [SerializeField] private Button _button;
   [SerializeField] private Button _sellButton;
   [SerializeField] private GameObject _sold;
   private bool _isSold;
   public bool IsSold => _isSold;
   private int _clotheId;
   public event Action<int> OnBuyButtonPressed;
   
   
   public event Action<Clothes> OnSellButtonPressed;
   public void UpdateBlock(Clothes clothe)
   {
      _clothe = clothe;
      _clotheImage.sprite = _clothe.icon;
      _clotheName.text = _clothe.name;
      _clothePrice.text = _clothe.price.ToString();
      _clotheId = _clothe.id;
   }

   private void Awake()
   {
      _button.onClick.AddListener((() =>
      {
         OnBuyButtonPressed?.Invoke(_clotheId);
      }));
      _sellButton.onClick.AddListener((() =>
      {
         OnSellButtonPressed?.Invoke(_clothe);
         UnSold();
      }));
   }

   public void Sold()
   {
      _sold.SetActive(true);
      _sellButton.gameObject.SetActive(true);
      _isSold = true;
   }

   private void UnSold()
   {
      _sold.SetActive(false);
      _sellButton.gameObject.SetActive(false);
      _isSold = false;
   }
}
