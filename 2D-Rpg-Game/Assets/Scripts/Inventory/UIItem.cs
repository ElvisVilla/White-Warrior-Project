﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour {

    public Item item;
    private Image spriteImage;

	void Start ()
    {
        spriteImage = GetComponent<Image>();
        UpdateItem(null);
	}
	
	public void UpdateItem(Item item)
    {
        this.item = item;
        if(this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = item.Icon;
        }
        else
        {
            spriteImage.color = Color.clear;
        }
    }
}