using LookingGlass;

using TofAr.V0.Color;
using TofAr.V0.Hand;
using TofAr.V0.Tof;

using UnityEngine;

public class FocusAdjusterHand : MonoBehaviour
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
        TofArHandManager.OnFrameArrived += (r) =>
        {
            var handData = TofArHandManager.Instance.HandData.Data;
            var leftZ = ((handData.handStatus == HandStatus.LeftHand) || (handData.handStatus == HandStatus.BothHands))
                        ? handData.featurePointsLeft[(int)HandPointIndex.HandCenter].z : 0;
            var rightZ = ((handData.handStatus == HandStatus.RightHand) || (handData.handStatus == HandStatus.BothHands))
                        ? handData.featurePointsRight[(int)HandPointIndex.HandCenter].z : 0;

            //Debug.Log($"Z: {faceZ}");
            if (leftZ > 0 || rightZ > 0)
            {
                hologramCamera.CameraProperties.FocalPlane = Mathf.Max(leftZ, rightZ) + adjust;
            }
        };
    }

    void Update()
    {
        if (tofStarted && colorStarted && !TofArHandManager.Instance.IsStreamActive)
        {
            TofArHandManager.Instance.StartStream();
        }
    }
}
