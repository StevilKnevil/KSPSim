using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSPSim
{
  [KSPAddon(KSPAddon.Startup.EditorAny, false)]
  public class KSPSimCore : MonoBehaviour
  {
    public void toggle()
    {
      // TODO: When leaving flight mode - revert back to normal launch site.
      // TODO: We also need to revert the launch site name if the launch fails (e.g. nothing in the editor)
      print(">>>>" + EditorLogic.fetch.launchSiteName);

      // Configure the simulation

      //EditorLogic.fetch.launchSiteName = "SimulationLaunchSite";
      EditorLogic.fetch.launchVessel();
    }

    ApplicationLauncherButton alButton;

    public void Start()
    {
      if (ApplicationLauncher.Ready && alButton == null)
      {
        Texture2D buttonTexture = null; //GameDatabase.Instance.GetTexture("KSPSim/Icons/KSPSim", false);
        alButton = ApplicationLauncher.Instance.AddModApplication(
          toggle, toggle,
          null, null,
          null, null,
          ApplicationLauncher.AppScenes.ALWAYS,
          buttonTexture);
      }

      // TODO: Remove/hide the app button when leaving the editor (OnEnable/Disable?)

    }

  }
}
