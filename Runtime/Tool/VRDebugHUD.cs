using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Profiling;

public class VRDebugHUD : MonoBehaviour
{
    public Text hudText;              // ÍÏÈë Text ×é¼þ
    private float updateInterval = 0.5f;

    private float timeLeft;
    private float accum;
    private int frames;

    void Start()
    {
        timeLeft = updateInterval;
    }

    void Update()
    {
        float deltaTime = Time.unscaledDeltaTime;
        timeLeft -= deltaTime;
        accum += deltaTime;
        frames++;

        if (timeLeft <= 0f)
        {
            float avgDeltaTime = accum / frames;
            float fps = 1f / avgDeltaTime;
            float frameMs = avgDeltaTime * 1000f;

            long monoUsed = Profiler.GetMonoUsedSizeLong() / (1024 * 1024);   // MB
            long totalUsed = Profiler.GetTotalAllocatedMemoryLong() / (1024 * 1024); // MB
            long gfxUsed = Profiler.GetAllocatedMemoryForGraphicsDriver() / (1024 * 1024); // MB

            hudText.text = string.Format(
                "FPS: {0:F1} ({1:F1} ms)\n" +
                "Mono: {2} MB\n" +
                "Total Mem: {3} MB\n" +
                "GPU Mem: {4} MB",
                fps, frameMs, monoUsed, totalUsed, gfxUsed
            );

            // Reset
            timeLeft = updateInterval;
            accum = 0f;
            frames = 0;
        }
    }
}
