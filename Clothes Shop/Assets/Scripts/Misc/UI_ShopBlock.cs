using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShopBlock : MonoBehaviour
{
   [SerializeField] private Image _clotheImage;
   [SerializeField] private TextMeshProUGUI _clotheName;
   [SerializeField] private TextMeshProUGUI _clothePrice;
   [SerializeField] private Button _button;
   private int _clotheId;
   public event Action<int> OnButtonPressed;
   public void UpdateBlock(Sprite newImage,string newName, int newPrice, int id)
   {
      _clotheImage.sprite = newImage;
      _clotheName.text = newName;
      _clothePrice.text = newPrice.ToString();
      _clotheId = id;
   }

   private void Awake()
   {
      _button.onClick.AddListener((() =>
      {
         OnButtonPressed?.Invoke(_clotheId);
      }));
   }
}
