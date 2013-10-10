using UnityEngine;
using System.Collections;

namespace mobydick.ui {
    public class Home : MonoBehaviour {
        public Texture2D home_title;
        public GUISkin home_play,
                       home_options,
                       home_credits;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        void OnGUI() {
            GUI.DrawTexture(new Rect(750, 15, 470, 255), home_title);
            GUI.skin = home_play;
            GUILayout.BeginArea(new Rect(850, 275, 294, 92));
            if (GUILayout.Button("", GUILayout.Width(294), GUILayout.Height(92))) {
                Application.LoadLevel(1);
            }
            GUI.skin = home_options;
            GUILayout.EndArea();
            GUILayout.BeginArea(new Rect(850, 275 + 135, 294, 92));
            if (GUILayout.Button("", GUILayout.Width(294), GUILayout.Height(92))) {
                //Application.LoadLevel(3);
            }
            GUI.skin = home_credits;
            GUILayout.EndArea();
            GUILayout.BeginArea(new Rect(850, 275 + 135 + 135, 294, 92));
            if (GUILayout.Button("", GUILayout.Width(294), GUILayout.Height(92))) {
                Application.LoadLevel(2);
            }
            GUILayout.EndArea();
        }
    }
}