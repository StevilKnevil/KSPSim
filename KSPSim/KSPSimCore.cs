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

      // could try:
      //SpaceTracking.GoToAndFocusVessel(v);

      // or
      //FlightDriver.StartWithNewLaunch(string fullFilePath, string missionFlagURL, string launchSiteName, VesselCrewManifest manifest);

      // or even: and we can store the previous state so that we can revert easily
      //FlightDriver.StartAndFocusVessel(Game stateToLoad, int vesselToFocusIdx);
      // See also all the other stuff in there, looks prime for it! Maybe even hack CanRevert via reflection for hardcore modes?




#if false
      // need a game object with a transform called "SimulationLaunchSite_Spawn"

      GameObject go = new GameObject("go");

      PSystemSetup.SpaceCenterFacility newFacility = new PSystemSetup.SpaceCenterFacility();
      newFacility.name = "SimulationLaunchSite";
      newFacility.facilityName = "SimulationLaunchSite";
      newFacility.facilityPQS = ((CelestialBody)obj.getSetting("CelestialBody")).pqsController;
      newFacility.facilityTransformName = "SimulationLaunchSiteXForm";
      newFacility.pqsName = ((CelestialBody)obj.getSetting("CelestialBody")).pqsController.name;
      PSystemSetup.SpaceCenterFacility.SpawnPoint spawnPoint = new PSystemSetup.SpaceCenterFacility.SpawnPoint();
      spawnPoint.name = "SimulationLaunchSite";
      spawnPoint.spawnTransformURL = "SimulationLaunchSite_Spawn";
      newFacility.spawnPoints = new PSystemSetup.SpaceCenterFacility.SpawnPoint[1];
      newFacility.spawnPoints[0] = spawnPoint;

      // Find and hack the array of facilities via reflection
      foreach (FieldInfo fi in PSystemSetup.Instance.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
			{
				if (fi.FieldType.Name == "SpaceCenterFacility[]")
        {
          // Make a copy of the existing facilities array and append another entry at the end.
          PSystemSetup.SpaceCenterFacility[] facilities = (PSystemSetup.SpaceCenterFacility[])fi.GetValue(PSystemSetup.Instance);
          facilities.AddUnique(newFacility);
          /*
          PSystemSetup.SpaceCenterFacility[] newFacilities = new PSystemSetup.SpaceCenterFacility[facilities.Length + 1];
          for (int i = 0; i < facilities.Length; ++i)
          {
            newFacilities[i] = facilities[i];
          }
          newFacilities[newFacilities.Length - 1] = newFacility;
          fi.SetValue(PSystemSetup.Instance, newFacilities);
          facilities = newFacilities;
          */
        }
      }
#endif
  

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
