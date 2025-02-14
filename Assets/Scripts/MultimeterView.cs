using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultimeterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private TextMeshProUGUI DCVoltageText;
    [SerializeField] private TextMeshProUGUI currentText;
    [SerializeField] private TextMeshProUGUI resistanceText;
    [SerializeField] private TextMeshProUGUI ACVoltageText;
    [SerializeField] private Transform arrow;

    private readonly Vector3[] arrowRotations =
    {
        new Vector3(0, 0, 0), // Neutral
        new Vector3(0, -45, 0), // Current
        new Vector3(0, -150, 0), // Resistance
        new Vector3(0, -220, 0), // AC Voltage
        new Vector3(0, -300, 0) // DC Voltage
    };

    public void UpdateDisplay(MultimeterState state, float DCvoltage, float current, float resistance, float ACVoltage)
    {
        string[] stateValues =
        {
            "0", current.ToString("F2"), resistance.ToString("F2"), ACVoltage.ToString("F2"), DCvoltage.ToString("F2")
        };

        displayText.text = stateValues[(int)state];
        DCVoltageText.text = "V: " + (state == MultimeterState.DCVoltage ? DCvoltage.ToString("F2") : "0");
        currentText.text = "A: " + (state == MultimeterState.Current ? current.ToString("F2") : "0");
        resistanceText.text = "Ω: " + (state == MultimeterState.Resistance ? resistance.ToString("F2") : "0");
        ACVoltageText.text = "~: " + (state == MultimeterState.ACVoltage ? ACVoltage.ToString("F2") : "0");
        arrow.rotation = Quaternion.Euler(arrowRotations[(int)state]);
    }
}