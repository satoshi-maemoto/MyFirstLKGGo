using LookingGlass;
using TofAr.V0.Color;
using TofAr.V0.Face;
using TofAr.V0.Tof;
using UnityEngine;

public class FocusAdjusterFace : MonoBehaviour
{
    public HologramCamera hologramCamera;
    public float adjust = 0f;

    private bool tofStarted = false;
    private bool colorStarted = false;

    void Start()
    {
        TofArTofManager.OnStreamStarted += (s, dt, ct, pt) =>
        {
            tofStarted = true;
        };
        TofArColorManager.OnStreamStarted += (s, t) =>
        {
            colorStarted = true;
        };
        TofArFaceManager.OnFaceEstimated += (r) =>
        {
            var faceZ = r.results[0].pose.position.z;
            //Debug.Log($"Z: {faceZ}");
            hologramCamera.CameraProperties.FocalPlane = faceZ + adjust;

        };
    }

    void Update()
    {
        if (tofStarted && colorStarted && !TofArFaceManager.Instance.IsStreamActive)
        {
            TofArFaceManager.Instance.StartStream();
        }
    }
}
