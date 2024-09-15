using TofAr.V0.Color;
using TofAr.V0.Tof;

using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject hologramCamera;

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
        hologramCamera.SetActive(false);

        // var configrations = TofArTofManager.Instance.GetProperty<CameraConfigurationsProperty>();
        // foreach (var conf in configrations.configurations)
        // {
        //     if (conf.lensFacing == 0)
        //     {
        //         var resolutions = TofArColorManager.Instance.GetProperty<AvailableResolutionsProperty>();
        //         foreach (var res in resolutions.resolutions)
        //         {
        //             if ((res.lensFacing == conf.lensFacing) && (((float)res.width / (float)res.height) == ((float)conf.width / (float)conf.height)))
        //             {
        //                 TofArTofManager.Instance.StartStreamWithColor(conf, res, false, false);
        //                 break;
        //             }
        //         }
        //         break;
        //     }
        // }
    }

    void Update()
    {
        if (tofStarted && colorStarted && !hologramCamera.activeInHierarchy)
        {
            hologramCamera.SetActive(true);
        }
    }
}
