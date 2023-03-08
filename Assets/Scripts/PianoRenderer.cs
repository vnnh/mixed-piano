using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoRenderer : MonoBehaviour
{
	[SerializeField]
	private GameObject whiteKey;
	[SerializeField]
	private GameObject blackKey;

	[SerializeField]
	private GameObject keyContainer;

	public List<GameObject> pianoKeys = new List<GameObject>();

	public void Render()
	{
		if (pianoKeys.Count != 0) { return; }

		float whiteKeyWidth = 0.022f;
		float whiteKeyLength = 0.16f;
		float whiteKeyHeight = whiteKey.transform.localScale.z;

		float blackKeyWidth = 0.0135f;
		float blackKeyLength = 0.09f;
		float blackKeyHeight = blackKey.transform.localScale.z;

		//on a full piano, there are 52 white keys, and it starts on A and ends on C
		for (int i = 0; i < 52; i++)
		{
			int positionIndex = i - 23; //middle C is the 23rd white key
			int noteLetter = i % 7;

			Vector3 relativePosition = new Vector3(positionIndex * (whiteKeyWidth + 0.001f), 0, 0);

			GameObject whiteObject = Instantiate(whiteKey, Vector3.zero, Quaternion.identity, keyContainer.transform);
			whiteObject.transform.localPosition = relativePosition;
			whiteObject.transform.localScale = new Vector3(whiteKeyWidth, whiteKeyLength, whiteKeyHeight);

			pianoKeys.Add(whiteObject);

			if (i == 51) { continue; }
			if (noteLetter == 1 || noteLetter == 4) { continue; } //B and E do not have black keys after them

			GameObject blackObject = Instantiate(blackKey, Vector3.zero, Quaternion.identity, keyContainer.transform);
			blackObject.transform.localPosition = relativePosition + new Vector3(whiteKeyWidth / 2, -whiteKeyLength / 2 + blackKeyLength / 2, blackKeyHeight / 2);
			blackObject.transform.localScale = new Vector3(blackKeyWidth, blackKeyLength, blackKeyHeight);

			pianoKeys.Add(blackObject);
		}
	}

	private void Start()
	{
		Render();
	}
}
