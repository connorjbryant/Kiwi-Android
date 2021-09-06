using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ShareButton : MonoBehaviour
{
	private string shareMessage;
	public void ClickShareButton()
	{
		shareMessage = "Wow! You are on a roll today! Your highscore is " + PlayerPrefs.GetInt("HighScore").ToString() + "!";

		StartCoroutine(TakeScreenshotAndShare());
	}

	private IEnumerator TakeScreenshotAndShare()
	{
		yield return new WaitForEndOfFrame();

		Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		ss.Apply();

		string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
		File.WriteAllBytes(filePath, ss.EncodeToPNG());

		// To avoid memory leaks
		Destroy(ss);

		new NativeShare().AddFile(filePath).SetSubject("Kiwi").SetText("Invite your").Share();

	}
}
