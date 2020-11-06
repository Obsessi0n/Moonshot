using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Gate gate;
    public Sprite activatedSprite;
    public Sprite deactivatedSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.name == "Player" || collision.name == "PlayerHologram")
        {
            GetComponent<SpriteRenderer>().sprite = activatedSprite;
            gate.OpenGate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       
        if (collision.name == "Player" || collision.name == "PlayerHologram")
        {
            gate.CloseGate();
            GetComponent<SpriteRenderer>().sprite = deactivatedSprite;
        }
    }

}
