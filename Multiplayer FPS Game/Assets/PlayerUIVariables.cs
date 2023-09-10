using UnityEngine;

public class PlayerUIVariables : MonoBehaviour
{

    [SerializeField]
    RectTransform thrusterFuelFill;

    private PlayerController controller;

    public void SetController(PlayerController controller)
    {
        this.controller = controller;
    }
    void SetFuelAmount(float _amount)
    {
        thrusterFuelFill.localScale = new Vector3 (1f, _amount, 1f);
    }

    void Update()
    {
        SetFuelAmount(controller.GetThrusterFuelAmount());
    }


}
