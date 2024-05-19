using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsToPick : MonoBehaviour
{

    [Header("Configuration")]
    [SerializeField] private InventoryItem inventoryItem;
    [SerializeField] private int amount;
    [SerializeField] private Animator playerAnimation;
   // [SerializeField] private Sfx getItemSound;

    [Header("Propiedades")]
    [SerializeField] private bool isOnTheGround;

    //  private bool pickingUp;
    private bool isInside;

    private const string PICKUP = "PickingUp";
    private const string GETITEM = "GetItem";

    public string ID;

    private void Start()
    {
        ID = inventoryItem.ID;
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && isInside && isOnTheGround)
        {
            StartCoroutine(IEPickUp());
        }
        if (Input.GetButtonDown("Interact") && isInside && !isOnTheGround)
        {
            StartCoroutine(IEGetItemStand());
        }

    }

    private void GetItem()
    {
       Inventory.Instance.AddItem(inventoryItem, amount);

        Destroy(gameObject);

        //******

    }

    private IEnumerator IEPickUp()
    {
        //  pickingUp = true;
        playerAnimation.SetBool(GETITEM, false);
        playerAnimation.SetBool(PICKUP, true);
        isInside = false;
        //getItemSound.GetItemSound();


        yield return new WaitForSeconds(.3f);

        //   pickingUp = false;
        playerAnimation.SetBool(PICKUP, false);
        GetItem();


    }

    private IEnumerator IEGetItemStand()
    {
        playerAnimation.SetBool(GETITEM, true);
        playerAnimation.SetBool(PICKUP, false);
        isInside = false;
        //getItemSound.GetItemSound();


        yield return new WaitForSeconds(.3f);

        //   pickingUp = false;
        playerAnimation.SetBool(GETITEM, false);
        GetItem();
    }


    //***********
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = false;
        }
    }


}
