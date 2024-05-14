    
    using UnityEngine;
    [System.Serializable, CreateAssetMenu(fileName = "Clothes", menuName = "Clothes")]
    public class Clothes : ScriptableObject
    {
        public int id;
        public int price;
        public Sprite icon;
        public Sprite weareable;
        public string clothName;
    }
