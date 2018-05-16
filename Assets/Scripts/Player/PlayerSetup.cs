using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {
    // All of the components we want to be disabled for the local player
    [SerializeField]
    Behaviour[] ComponentsToDisable;
    GameObject SceneCamera;

    void Start () {

        if (!isLocalPlayer) {
            foreach (Behaviour Component in ComponentsToDisable) {
                if (Component) {
                    Component.enabled = false;

                    if (Component.GetComponent<Camera>()) {
                        Component.GetComponent<Camera>().enabled = false;
                    }
                }
            }
        }
        else {
            // Lock the player's cursor
            Cursor.lockState = CursorLockMode.Locked;

            SceneCamera = GameObject.FindGameObjectWithTag("Scene Camera");

            // Disable the scene camera since we're not using it
            SceneCamera.SetActive(false);
        }
    }

    void OnDisable() {
        // Enable the scene camera when disconnected
        SceneCamera.SetActive(true);
    }
}
