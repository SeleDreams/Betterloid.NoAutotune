using Betterloid;
using System.Windows;
using System;
using HarmonyLib;
using System.Reflection;
using Yamaha.VOCALOID.VMM;


#if VOCALOID6
using Yamaha.VOCALOID;
using Yamaha.VOCALOID.MusicalEditor;
using Yamaha.VOCALOID.VSM;
using VOCALOID = Yamaha.VOCALOID;
#endif
using System.Collections.Generic;

namespace NoAutotune
{
    public class NoAutotune : IPlugin
    {

        public void Startup()
        {
            var harmony = new Harmony("com.seledreams.no-autotune-patch"); 
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
[HarmonyPatch(typeof(WIVSMMidiPart))]
[HarmonyPatch("GetDefaultAiNoteExpression")]
public class PatchDefault
{
    static void Postfix(ref VSMAiNoteExpression __result)
    {
        //MessageBoxDeliverer.GeneralWarning("The method has been patched successfully!");
        __result.PitchTransitionStart = 0; // Leading Transition
        __result.PitchFine = 0.5f;// Fine Tune
        __result.PitchTransitionEnd = 0; // Following Transition

        __result.PitchDriftStart = 0; // Leading Drift
        __result.PitchScalingOrigin = 0f; // Overall Drift
        __result.PitchDriftEnd = 0; // Following Drift

        __result.AmplitudeEnd = 0.5f; // Following Expression 
        __result.AmplitudeWhole = 0.5f; // Middle Expression
        __result.AmplitudeStart = 0.5f; // Leading Expression
        
        // Controls the overall auto tune strength, there's no need to change it because the rest are already set to good values.
        //__result.PitchScalingCenter = 0.5f;
        
    } 
}