﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WorldUIController : MonoBehaviour
{
	public static WorldUIController Instance;

	public CanvasGroup Toolbar;
	public GameObject CompasArrow;
	public Text ClockText;

	public Vector3 playerHome { get; set; }
	public Player player { get; set; }
	private float cooldown;

	void Awake ()
	{
		Instance = this;
	}

	public void OnClickHelp ()
	{

	}
	public void OnClickCallPolice ()
	{
		player.callPolice ();
		cooldown = -1;
	}
	public void OnClickCamera ()
	{

	}

	void Update ()
	{
		var time = new DateTime ( 2015, 1, 24, 13, 30, 0, 0 );
		time += TimeSpan.FromSeconds ( Time.time );
		ClockText.text = time.ToString ( "t" );

		Toolbar.interactable = player != null && player.iac != null && cooldown > 0;
		cooldown += Time.deltaTime;

	}
}
