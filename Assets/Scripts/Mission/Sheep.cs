﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Sheep : MonoBehaviour {
	// Variable initial
	public GameObject Panel;
	public AudioClip ClickAudio;
	[Header("Tutorials Settings")]
	public GameObject PanelTutorials;
	public GameObject closeTutorials;
	public Sprite[] spriteTutorials;
	public AudioClip[] VoiceTutorials;
	[Header("Sheep Settings")]
	public GameObject SheepPanel;
	public Sprite[] spriteSheeps;
	[Header("Ready Settings")]
	public GameObject PanelReady;
	public Sprite[] spriteReady;
	public AudioClip[] VoiceReady;
	[Header("Win Settings")]
	public GameObject PanelWin;

	public GameObject PanelLose, Group1, Group2;
	public AudioClip AudioCorrect, AudioWorng, LoseVoice;
	public GameObject SheepCorrect;
	public Sprite SheepHint;

	private bool Reseted = false;

	// On start scene will call OnLoadTutorials for display tutorials
	private void Start() => StartCoroutine(OnLoadTutorials());

	// Back to scene map
	public void backToMap() {
		AudioSource.PlayClipAtPoint(ClickAudio, new Vector3(0f, 0f, -10f));
		SceneManager.LoadSceneAsync(Singleton.Scene.Map.ToString());
	}

	// When clear mission this method will do
	public void ClearMission(int Mission) {
		// Set this mission status to complate
		PlayerPrefs.SetString("MissionLevel" + Mission.ToString(), "complate");
		// Unlock next level
		PlayerPrefs.SetString("MissionLevel" + (Mission + 1).ToString(), "play");
		// Load map scene
		backToMap();
	}

	// For hide tutorials and display ready text
	public void closeTutorial() {
		AudioSource.PlayClipAtPoint(ClickAudio, new Vector3(0f, 0f, -10f));
		PanelTutorials.SetActive(false);
		StartCoroutine(SheepImage());
	}

	// Win text display
	public void Win() {
		AudioSource.PlayClipAtPoint(ClickAudio, new Vector3(0f, 0f, -10f));
		Panel.SetActive(true);
		PanelWin.SetActive(true);
	}

	// Method for delay to display tutorials in array spriteTutorials[]
	IEnumerator OnLoadTutorials() {
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < spriteTutorials.Length; i++) {
			PanelTutorials.GetComponent<Image>().sprite = spriteTutorials[i];
			AudioSource.PlayClipAtPoint(VoiceTutorials[i], new Vector3(0f, 0f, -10f));
			yield return new WaitForSeconds(VoiceTutorials[i].length + 1.5f);
		}
		closeTutorials.SetActive(true);
	}
	public void SheepClick(int i) {
		if (!Reseted) {
			switch (i) {
				case 1:
					AudioSource.PlayClipAtPoint(AudioWorng, new Vector3(0f, 0f, -10f));
					Reseted = true;
					StartCoroutine(ShowLose());
					break;
				case 2:
					AudioSource.PlayClipAtPoint(AudioCorrect, new Vector3(0f, 0f, -10f));
					gameObject.SendMessage("Win");
					break;
				case 3:
					AudioSource.PlayClipAtPoint(AudioWorng, new Vector3(0f, 0f, -10f));
					Reseted = true;
					StartCoroutine(ShowLose());
					break;
			}
		} else {
			switch (i) {
				case 1:
					AudioSource.PlayClipAtPoint(AudioWorng, new Vector3(0f, 0f, -10f));
					SheepCorrect.GetComponent<Image>().sprite = SheepHint;
					break;
				case 2:
					AudioSource.PlayClipAtPoint(AudioWorng, new Vector3(0f, 0f, -10f));
					SheepCorrect.GetComponent<Image>().sprite = SheepHint;
					break;
				case 3:
					AudioSource.PlayClipAtPoint(AudioCorrect, new Vector3(0f, 0f, -10f));
					gameObject.SendMessage("Win");
					break;
			}
		}
	}
	// Method for delay to display ready text in array spriteReady[]
	IEnumerator OnLoadReady() {
		PanelReady.SetActive(true);
		for (int i = 0; i < spriteReady.Length; i++) {
			PanelReady.GetComponent<Image>().sprite = spriteReady[i];
			AudioSource.PlayClipAtPoint(VoiceReady[i], new Vector3(0f, 0f, -10f));
			yield return new WaitForSeconds(VoiceReady[i].length + 1f);
		}
		PanelReady.SetActive(false);
		Panel.SetActive(false);
	}
	IEnumerator SheepImage() {
		SheepPanel.SetActive(true);
		SheepPanel.GetComponent<Image>().sprite = spriteSheeps[0];
		yield return new WaitForSeconds(1f);
		SheepPanel.GetComponent<Image>().sprite = spriteSheeps[1];
		yield return new WaitForSeconds(1f);
		SheepPanel.GetComponent<Image>().sprite = spriteSheeps[2];
		yield return new WaitForSeconds(1f);
		SheepPanel.GetComponent<Image>().sprite = spriteSheeps[3];
		yield return new WaitForSeconds(1f);
		SheepPanel.SetActive(false);
		StartCoroutine(OnLoadReady());
	}
	IEnumerator ShowLose() {
		Panel.SetActive(true);
		PanelLose.SetActive(true);
		AudioSource.PlayClipAtPoint(LoseVoice, new Vector3(0f, 0f, -10f));
		yield return new WaitForSeconds(LoseVoice.length + 0.25f);
		Group1.SetActive(false);
		Group2.SetActive(true);
		Panel.SetActive(false);
		PanelLose.SetActive(false);
	}
}