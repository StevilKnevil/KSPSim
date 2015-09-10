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
      print(">>> here <<<");
    }

    ApplicationLauncherButton alButton;

    public void Update()
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

    }

  }
}
