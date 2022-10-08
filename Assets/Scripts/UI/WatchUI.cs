using UnityEngine;
using UnityEngine.UI;
using Unity.VectorGraphics;
using TMPro;

public class WatchUI : MonoBehaviour
{
    public class CountdownColor
    {
        public static Color32 Ok { get => new Color32(51, 153, 0, 255); }
        public static Color32 Hurry { get => new Color32(255, 204, 0, 255); }
        public static Color32 Ending { get => new Color32(204, 51, 0, 255); }
    }

    public class CountdownStates
    {
        public static float Ok { get => 0.7f; }
        public static float Hurry { get => 0.5f; }
        public static float Ending { get => 0.2f; }
    }

    WaveSpawner waveSpawner;
    SVGImage svgWatch;

    public TMP_Text CountdownText;

    private static float threshold = 0.05f;

    void Start()
    {
        waveSpawner = GameObject.FindObjectOfType<WaveSpawner>();
        svgWatch = GameObject.FindObjectOfType<SVGImage>();

        InvokeRepeating("ChangeTimerColor", 0, 1f);
    }

    void Update()
    {
        CountdownText.ForceMeshUpdate();
        CountdownText.text = waveSpawner.Countdown.ToString();
    }

    private void ChangeTimerColor()
    {
        if (waveSpawner.Countdown >= 0)
        {
            float percentage = waveSpawner.Countdown / waveSpawner.timeBetweenWaves;

            if (percentage > CountdownStates.Ok || FastApproximately(percentage, CountdownStates.Ok))
            {
                Debug.Log($"percentage: {percentage} | State: Ok");
                CountdownText.color = CountdownColor.Ok;
                svgWatch.material.color = CountdownColor.Ok;
            }
            else if (percentage > CountdownStates.Hurry || FastApproximately(percentage, CountdownStates.Hurry))
            {
                Debug.Log($"percentage: {percentage} | State: Hurry");
                CountdownText.color = CountdownColor.Hurry;
                svgWatch.material.color = CountdownColor.Hurry;
            }
            else if (percentage > CountdownStates.Ending || FastApproximately(percentage, CountdownStates.Ending))
            {
                Debug.Log($"percentage: {percentage} | State: Ending");
                CountdownText.color = CountdownColor.Ending;
                svgWatch.material.color = CountdownColor.Ending;
            }
        }
    }

    public static bool FastApproximately(float a, float b)
    {
        if (threshold > 0f)
        {
            return Mathf.Abs(a - b) <= threshold;
        }
        else
        {
            return Mathf.Approximately(a, b);
        }
    }

}
