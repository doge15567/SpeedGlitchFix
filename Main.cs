
using MelonLoader;
using Il2CppSLZ.Props;
using Il2CppSLZ.Rig;
using UnityEngine;
using System;
using Il2CppInterop.Runtime.Injection;

using BoneLib;
using BoneLib.BoneMenu;
using HarmonyLib;
using Il2CppSLZ.Marrow;


namespace SpeedGlitchFix
{
    internal partial class Main : MelonMod
    {
        internal const string Name = "SpeedGlitchFix";
        internal const string Description = "Clamps crouch speed mult on rig to prevent buggy super speed when the loco ball gets stuck";
        internal const string Author = "doge15567";
        internal const string Company = "";
        internal const string Version = "0.0.1";
        internal const string DownloadLink = "Link";
        internal static MelonLogger.Instance MelonLog;

        public override void OnInitializeMelon()
        {
            MelonLog = LoggerInstance;

            ClassInjector.RegisterTypeInIl2Cpp<CrouchSpeedClamper>();

            if (!ClassInjector.IsTypeRegisteredInIl2Cpp(typeof(CrouchSpeedClamper)))
            { MelonLog.Error("Failed to Register Type!!!!!!!!"); }
            else { MelonLog.Msg("Registerd Class!!!"); }



            MelonLog.Msg("This game has Chat Voice, brought to you by Mad Studio.");
#if DEBUG
            BoneMenu.Setup();
#endif
        }


        
    }
#if DEBUG
    internal static class BoneMenu
    {
        private static Page _mainCategory;
        public static void Setup()
        {
            _mainCategory = Page.Root.CreatePage("Speed Glitch Fix", Color.gray);  //MenuManager.CreateCategory("Air Control", "#FF0000");
            _mainCategory.CreateFunction("Set Grav Zero", Color.white, () => Physics.gravity = new Vector3(0f, 0f, 0f));
            _mainCategory.CreateFunction("Set Grav Earth", Color.white, () => Physics.gravity = new Vector3(0f, -9.81f, 0f));
        }
    }
#endif

    [HarmonyPatch(typeof(RemapRig))]
    public static class RemapRigStartPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(RemapRig.OnAwake))]
        public static void Postfix(RemapRig __instance)
        {
            var x = __instance.gameObject.AddComponent<CrouchSpeedClamper>();
            x.remapRig = __instance;
        }
    }

    [RegisterTypeInIl2Cpp]
    public class CrouchSpeedClamper : MonoBehaviour // MonoBehavior because this is an aircontrol reskin and I also want it to work in fusion and effort BLAAAHAHAHBBH
    {
        public Il2CppSLZ.Marrow.RemapRig remapRig;

        public CrouchSpeedClamper(IntPtr intPtr) // I feel like this is more of a hold-over from older versions as my random ideas mod never has this on monobehaviors and they work just fine.
            : base(intPtr)
        {
        }

        private void Start()
        {
#if DEBUG
            Main.MelonLog.Msg($"Hooked into RemapRig on {transform.root.name}");
#endif
            FixedUpdate();
        }

        private void FixedUpdate()
        {
            if (!remapRig) return;
            remapRig._crouchSpeedLimit = Mathf.Clamp01(remapRig._crouchSpeedLimit);
        }
    }
}
