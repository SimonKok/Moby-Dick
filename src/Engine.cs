using UnityEngine;
using System.Collections.Generic;

namespace mobydick {
    public class Engine : MonoBehaviour {
        public static bool playing = true;
        public static float speed = 2f;
        public GameObject blowfish_prefab;
        public GameObject boxfish_prefab;
        public GameObject flyfish_prefab;
        public GameObject jellyfish_prefab;
        public GameObject kingfish_prefab;
        public GameObject rock_prefab;
        public GameObject powerup_speed_prefab,
                          powerup_health_prefab,
                          powerup_harpoon_prefab;
        public List<GameObject> fishes,
                                clouds,
                                rocks,
                                harpoons,
                                bgrocks1, 
                                bgrocks2,
                                powerups;
        private System.Random rand;
        public List<Animation> animations;
        /*public AudioClip catch_fish_clip,
                         hit_object_clip,
                         powerup_clip,
                         theme_clip;
        public AudioSource catch_fish,
                           hit_object,
                           powerup_,
                           theme;*/
        public AudioSource fish_catch = new AudioSource();
        public AudioClip fish_catch_clip;

        // Use this for initialization
        void Start() {
            if(fishes.Count != 0)
                foreach (GameObject fish in fishes) {
                    Destroy(fish);
                }
            if (rocks.Count != 0)
                foreach (GameObject rock in rocks) {
                    Destroy(rock);
                }
            if (harpoons.Count != 0)
                foreach (GameObject harpoon in harpoons) {
                    Destroy(harpoon);
                }
            if (powerups.Count != 0)
                foreach (GameObject powerup in powerups) {
                    Destroy(powerup);
                }
            playing = true;
            speed = 2f;
            fishes = new List<GameObject>();
            rocks = new List<GameObject>();
            harpoons = new List<GameObject>();
            powerups = new List<GameObject>();
            animations = new List<Animation>();
            rand = new System.Random();
        }

        // Update is called once per frame
        void Update() {
            if (playing) {
                if (rand.Next(0, (int) (100 - speed * 5)) == 1) {
                    byte type = (byte)rand.Next(0, 4);
                    GameObject fish = null;
                    switch (type) {
                        case 0:
                            fish = Instantiate(blowfish_prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            break;
                        case 1:
                            fish = Instantiate(boxfish_prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            break;
                        case 2:
                            fish = Instantiate(kingfish_prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            break;
                        case 3:
                            fish = Instantiate(blowfish_prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            break;
                        case 4:
                            fish = Instantiate(kingfish_prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            break;
                    }
                    fishes.Add(fish);
                }

                if (rand.Next(0, (int)(100 - speed * 5)) == 1) {
                    GameObject rock = Instantiate(rock_prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    rocks.Add(rock);
                }

                if (rand.Next(0, 360) == 1) {
                    GameObject powerup = null;
                    switch (rand.Next(0, 3)) {
                        case 0:
                            //powerup = Instantiate(powerup_speed_prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            break;
                        case 1:
                            powerup = Instantiate(powerup_harpoon_prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            break;
                        case 2:
                            powerup = Instantiate(powerup_health_prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            break;
                    }
                    if(powerup != null)
                        powerups.Add(powerup);
                }

                foreach (GameObject cloud in clouds) {
                    cloud.transform.Translate(-.05f * speed, 0, 0);
                    if (cloud.transform.position.x <= -20) {
                        cloud.transform.Translate(70, 0, 0);
                    }
                }

                foreach (GameObject rocks in bgrocks1) {
                    rocks.transform.Translate(-.05f * speed, 0, 0);
                    if (rocks.transform.position.x <= -65) {
                        rocks.transform.Translate(150, 0, 0);
                    }
                }
                foreach (GameObject rocks in bgrocks2) {
                    rocks.transform.Translate(-.025f * speed, 0, 0);
                    if (rocks.transform.position.x <= -65) {
                        rocks.transform.Translate(150, 0, 0);
                    }
                }
            }
        }
    }
}