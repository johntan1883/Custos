using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : Interactable
{
    //public bool IsHoldingKey;
    //public bool BoyIsHoldingKey =false;

    //private GameObject dog;
    //private GameObject boy;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    dog = GameObject.FindWithTag("Player");
    //    boy = GameObject.FindWithTag("BlindBoy");
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    CheckHoldingKey();
    //    BoyCheckHoldingKey();
    //}

    //public void HoldKey()
    //{
    //    IsHoldingKey = !IsHoldingKey;
    //}
    //public void BoyInteract()
    //{
    //    BoyIsHoldingKey = !BoyIsHoldingKey;
    //}

    ////FOR DOG
    //private void CheckHoldingKey()
    //{
    //    if (IsHoldingKey)
    //    {
    //        HoldKeyLogic();
    //        Debug.Log("Holding key!");
    //    }
    //    else if (!IsHoldingKey)
    //    {
    //        ReleaseKey();
    //        Debug.Log("Is not holding key!");
    //    }
    //}
    //private void HoldKeyLogic()
    //{
    //    // Make the key a child of the dog
    //    transform.SetParent(dog.transform);

    //    // Set the local position relative to the dog's position
    //    transform.localPosition = new Vector3(1.39f, 0.8f, 0f);

    //    // Disable boy's movement (assuming Rigidbody2D is used)
    //    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //}
    //private void ReleaseKey()
    //{
    //    transform.parent = null;
    //}

    ////FOR BOY
    //private void BoyCheckHoldingKey()
    //{
    //    if (BoyIsHoldingKey)
    //    {
    //        BoyReleaseKey();
    //        Debug.Log("Boy dropped the key");
    //    }
    //    else if (!BoyIsHoldingKey)
    //    {
    //        //BoyHoldKeyLogic();
    //        Debug.Log("Boy is now holding the key");
    //    }
    //}

    ////private void BoyHoldKeyLogic()
    ////{
    ////    //Finding the HoldingPosition object
    ////    Transform holdingPosition = boy.transform.Find("HoldingPosition");

    ////    if (holdingPosition != null)
    ////    {
    ////        gameObject.transform.parent = holdingPosition;
    ////        gameObject.transform.position = holdingPosition.position;
    ////        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    ////    }
    ////}

    //private void BoyReleaseKey()
    //{
    //    gameObject.transform.parent=null;
    //    gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    //}

    public override void Interact()
    {
        base.Interact();// call base method if needed
        PickUpKey();
    }

    private void PickUpKey()
    {

        Debug.Log("Key picked up!");
    }
}
