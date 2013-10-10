using UnityEngine;
using System.Collections;
using mobydick.model;

namespace mobydick.ui {
    public class HUD : MonoBehaviour {
        private Ship ship;
        public GUISkin hud_main,
                       hud_menu_resume,
                       hud_menu_menu,
                       hud_menu_options,
                       hud_gameover_menu;
        public Texture2D harpoons,
                         fishes,
                         hpbar_frame,
                         hpbar_red,
                         hpbar_blue,
                         gameover,
                         gameover_menu,
                         menu,
                         menu_resume,
                         menu_menu,
                         menu_options;

        // Use this for initialization
        void Start() {
            ship = GameObject.Find("Ship").GetComponent<Ship>();
        }

        // Update is called once per frame
        void Update() {

        }

        void OnGUI() {
            GUI.skin = hud_main;
            GUI.DrawTexture(new Rect(1000, 20, 99, 106), fishes);
            GUI.DrawTexture(new Rect(1120, 20, 99, 106), harpoons);
            GUILayout.BeginArea(new Rect(1000, 80, 99, 40));
            GUILayout.Label(ship.fishes.ToString());
            GUILayout.EndArea();
            GUILayout.BeginArea(new Rect(1120, 80, 99, 40));
            GUILayout.Label(ship.harpoons.ToString());
            GUILayout.EndArea();
            GUI.DrawTexture(new Rect(419, 22, 441, 20), hpbar_red);
            GUI.BeginGroup(new Rect(419, 22, 441f * ((float)ship.health / 100f), 20));
            GUI.DrawTexture(new Rect(0, 0, 441, 20), hpbar_blue);
            GUI.EndGroup();
            GUI.DrawTexture(new Rect(407, 20, 466, 29), hpbar_frame);

            if (ship.health <= 0) {
                Engine.playing = false;
            }

            if (!Engine.playing) {
                if (ship.health <= 0) {
                    GUI.DrawTexture(new Rect(640 - 464 / 2, 360 - 360 / 2, 464, 360), gameover);
                    GUI.skin = hud_gameover_menu;
                    GUILayout.BeginArea(new Rect(495, 369, 289, 93));
                    if (GUILayout.Button("", GUILayout.Width(289), GUILayout.Height(93))) {
                        Application.LoadLevel(0);
                    }
                    GUILayout.EndArea();
                } else {
                    GUI.DrawTexture(new Rect(640 - 500 / 2, 360 - 610 / 2, 500, 610), menu);
                    GUI.skin = hud_menu_resume;
                    GUILayout.BeginArea(new Rect(640 - 500 / 2 + 104, 360 - 610 / 2 + 120, 294, 92));
                    if (GUILayout.Button("", GUILayout.Width(294), GUILayout.Height(92))) {
                        Engine.playing = true;
                    }
                    GUILayout.EndArea();
                    GUI.skin = hud_menu_menu;
                    GUILayout.BeginArea(new Rect(640 - 500 / 2 + 104, 360 - 610 / 2 + 120 + 137, 294, 92));
                    if (GUILayout.Button("", GUILayout.Width(294), GUILayout.Height(92))) {
                        Application.LoadLevel(0);
                    }
                    GUILayout.EndArea();
                    GUI.skin = hud_menu_options;
                    GUILayout.BeginArea(new Rect(640 - 500 / 2 + 104, 360 - 610 / 2 + 120 + 137 * 2, 294, 91));
                    if (GUILayout.Button("", GUILayout.Width(294), GUILayout.Height(91))) {
                        //Application.LoadLevel(3);
                    }
                    GUILayout.EndArea();
                }
            }
        }
    }
}