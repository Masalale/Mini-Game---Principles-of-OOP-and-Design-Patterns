using UnityEngine;

// Polymorphism: touching a potion collects it, touching an enemy ends the game
public class Potion : MonoBehaviour, ITouchable
{
    public void OnPlayerTouched()
    {
        GameManager.Instance.CollectPotion();
        Destroy(gameObject);
    }
}
