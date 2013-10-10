using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {
    public Texture2D home_title;
    public Texture2D credits;
    public float ypos = 720;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ypos -= 2;
        if (ypos <= -500)
            ypos = 720;

        if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Escape)) {
            Application.LoadLevel(0);
        }
	}

    void OnGUI() {
        GUI.DrawTexture(new Rect(750, 15, 470, 255), home_title);
        GUI.DrawTexture(new Rect(Screen.width / 2 - 250, ypos, 500, 600), credits);
    }
}
