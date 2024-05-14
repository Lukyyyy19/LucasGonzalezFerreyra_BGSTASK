using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[System.Serializable, CreateAssetMenu(fileName = "CharacterDialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public string owner;
    public string[] lines;
    public float lineSpeed;
    public UnityEvent Consecuence;
}