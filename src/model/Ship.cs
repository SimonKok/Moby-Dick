using UnityEngine;
using System.Collections;
using mobydick;

namespace mobydick.model {
    public class Ship : MonoBehaviour {
        public GameObject harpoon_prefab;
        public int fishes = 0;
        public int health = 100;
        public int harpoons = 30;
        public byte currentLane = 2;
        public bool[] xMovement = { false, false };
        private Engine engine;
        private sbyte tilting = 0;

        // Use this for initialization
        void Start() {
            engine = GameObject.Find("Engine").GetComponent<Engine>();
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyDown(KeyCode.P))
                Engine.playing = !Engine.playing;

            //if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("enter"))

            if (Engine.playing) {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                    if (currentLane < 4)
                        currentLane++;
                }

                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                    if (currentLane > 0)
                        currentLane--;
                }

                if (Input.GetKeyDown(KeyCode.Space)) {
                    if (harpoons > 0) {
                        harpoons--;
                        GameObject harpoon = Instantiate(harpoon_prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                        harpoon.transform.Translate(new Vector3(transform.position.x + 3, 1, transform.position.z - .9f));
                        harpoon.GetComponent<Harpoon>().lane = currentLane;
                        engine.harpoons.Add(harpoon);
                    }
                }

                xMovement[0] = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
                xMovement[1] = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

                if (xMovement[0])
                    transform.Translate(-.075f * Engine.speed, 0, 0);

                if (xMovement[1])
                    transform.Translate(.05f * Engine.speed, 0, 0);

                float destination = Constants.LANE_START + currentLane * Constants.LANE_WIDTH;
                if (transform.position.z > destination) {
                    tilting = -1;
                    transform.Translate(0, 0, -.25f);
                } else if (transform.position.z < destination) {
                    transform.Translate(0, 0, .25f);
                    tilting = 1;
                } else if (transform.position.z == destination) {
                    transform.GetChild(0).Rotate(new Vector3(0, 0, -transform.GetChild(0).rotation.z));
                    tilting = 0;
                }

                switch (tilting) {
                    case -1:
                        if (transform.GetChild(0).rotation.z > -30) {
                            transform.GetChild(0).Rotate(new Vector3(0, 0, -3));
                        }
                        break;
                    case 0:
                        if (transform.GetChild(0).rotation.z < 0)
                            transform.GetChild(0).Rotate(new Vector3(0, 0, 3));
                        if (transform.GetChild(0).rotation.z > 0)
                            transform.GetChild(0).Rotate(new Vector3(0, 0, -3));
                        break;
                    case 1:
                        if (transform.GetChild(0).rotation.z < 30) {
                            transform.GetChild(0).Rotate(new Vector3(0, 0, 3));
                        }
                        break;
                }

                foreach (GameObject obj in engine.fishes.ToArray()) {
                    if (obj.transform.position.x >= 60 || obj.transform.position.x <= -60) {
                        engine.fishes.Remove(obj);
                        Destroy(obj);
                    }
                    if (obj.GetComponent<Fish>().lane == currentLane) {
                        if (obj.transform.position.x <= transform.position.x + 3f && obj.transform.position.x >= transform.position.x - 3f) {
                            engine.fishes.Remove(obj);
                            Destroy(obj);
                            engine.audio.PlayOneShot(engine.fish_catch_clip);
                            fishes++;
                            Engine.speed += .1f;

                        }
                    }

                    foreach (GameObject harpoon in engine.harpoons.ToArray()) {
                        if (obj.transform.position.x >= 60) {
                            engine.harpoons.Remove(obj);
                            Destroy(obj);
                        }
                        if (harpoon.GetComponent<Harpoon>().lane == obj.GetComponent<Fish>().lane) {
                            if (obj.transform.position.x <= harpoon.transform.position.x + 2 && obj.transform.position.x >= harpoon.transform.position.x - 2) {
                                engine.fishes.Remove(obj);
                                Destroy(obj);
                                fishes++;
                                engine.audio.PlayOneShot(engine.fish_catch_clip);
                                Engine.speed += .1f;
                                engine.harpoons.Remove(harpoon);
                                Destroy(harpoon);
                            }
                        }
                    }
                }

                foreach (GameObject obj in engine.powerups.ToArray()) {
                    if (obj.transform.position.x >= 60 || obj.transform.position.x <= -60) {
                        engine.powerups.Remove(obj);
                        Destroy(obj);
                    }
                    if (obj.GetComponent<Powerup>().lane == currentLane) {
                        if (obj.transform.position.x <= transform.position.x + 3f && obj.transform.position.x >= transform.position.x - 3f) {
                            switch (obj.GetComponent<Powerup>().type) {
                                case 0:

                                    break;
                                case 1:
                                    harpoons += 3;
                                    break;
                                case 2:
                                    if (health < 100)
                                        health += 10;
                                    break;
                            }
                            engine.powerups.Remove(obj);
                            Destroy(obj);
                        }
                    }

                    foreach (GameObject harpoon in engine.harpoons.ToArray()) {
                        if (harpoon.GetComponent<Harpoon>().lane == obj.GetComponent<Powerup>().lane) {
                            if (obj.transform.position.x <= harpoon.transform.position.x + 2 && obj.transform.position.x >= harpoon.transform.position.x - 2) {
                                engine.powerups.Remove(obj);
                                Destroy(obj);
                                switch (obj.GetComponent<Powerup>().type) {
                                    case 0:

                                        break;
                                    case 1:
                                        harpoons += 3;
                                        break;
                                    case 2:
                                        if (health < 100)
                                            health += 10;
                                        break;
                                }
                                engine.harpoons.Remove(harpoon);
                                Destroy(harpoon);
                            }
                        }
                    }
                }

                foreach (GameObject obj in engine.rocks.ToArray()) {
                    if (obj.transform.position.x >= 60 || obj.transform.position.x <= -60) {
                        engine.rocks.Remove(obj);
                        Destroy(obj);
                    }
                    Rock rock = obj.GetComponent<Rock>();
                    if (rock.sinking) {
                        if (rock.sunk) {
                            engine.rocks.Remove(obj);
                            Destroy(obj);
                        }
                    } else {
                        if (rock.lane == currentLane) {
                            if (obj.transform.position.x <= transform.position.x + 3 && obj.transform.position.x >= transform.position.x - 3) {
                                rock.sinking = true;
                                health -= 5;
                                if(Engine.speed > 2)
                                    Engine.speed -= .1f;
                                rock.audio.Play();
                            }
                        }
                    }

                    foreach (GameObject harpoon in engine.harpoons.ToArray()) {
                        if (harpoon.GetComponent<Harpoon>().lane == rock.lane) {
                            if (obj.transform.position.x <= harpoon.transform.position.x + 2 && obj.transform.position.x >= harpoon.transform.position.x - 2) {
                                engine.harpoons.Remove(harpoon);
                                Destroy(harpoon);
                            }
                        }
                    }
                }
            }
        }
    }
}