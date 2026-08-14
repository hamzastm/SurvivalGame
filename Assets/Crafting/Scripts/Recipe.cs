using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Items/Recipe")]
public class Recipe : ScriptableObject 
{
    [System.Serializable]

    public struct Ingredient
    {
        public Item item;
        public int quantity;
    }

    public float craftingTime;

    public Item itemToCraft;
    public int quantityToCraft;

    [SerializeField] private List<Ingredient> ingredients;
    public IReadOnlyCollection<Ingredient> Ingredients => ingredients;

   
}
