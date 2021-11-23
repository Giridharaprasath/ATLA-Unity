using UnityEngine;

public class WaterBending : MonoBehaviour
{
    public GameObject iceCube;

    private void Awake()
    {
        InputManager.Controls.WaterBending.FreezeWater.performed += ctx => PerformedFreezeWater();
        InputManager.Controls.WaterBending.IceCube.performed += ctx => PerformedIceCube();
    }

    private void PerformedFreezeWater()
    {
        Debug.Log("PERFORMED FREEZE WATER");

    }

    private void PerformedIceCube()
    {
        Debug.Log("PERFORMED ICE CUBE");
    }
}
