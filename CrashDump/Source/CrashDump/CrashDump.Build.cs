// zompi 2014-2017 all rights reserverd

using System;
using System.IO;
using UnrealBuildTool;

public class CrashDump : ModuleRules
{
	public CrashDump(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = ModuleRules.PCHUsageMode.UseExplicitOrSharedPCHs;
		
		PublicDependencyModuleNames.AddRange(
			new string[] {
					"Core",
					"CoreUObject",
					"Engine"
            });

        if (Target.Platform == UnrealTargetPlatform.Android && 
            Target.Configuration != UnrealTargetConfiguration.Shipping &&
            Target.Configuration != UnrealTargetConfiguration.Test)
        {
            PrivateDefinitions.Add("WITH_CRASH_DUMP=1");
            PrivateDependencyModuleNames.AddRange(new string[] { "Launch" });

            string PluginPath = Utils.MakePathRelativeTo(ModuleDirectory, Target.RelativeEnginePath);
            AdditionalPropertiesForReceipt.Add("AndroidPlugin", Path.Combine(PluginPath, "CrashDump_UPL_Android.xml"));


            string ThirdPartyPath = Path.Combine(ModuleDirectory, "..", "ThirdParty");
            PublicIncludePaths.Add(Path.Combine(ThirdPartyPath, "GoogleBreakpad", "include"));
            PublicAdditionalLibraries.Add(Path.Combine(ThirdPartyPath, "GoogleBreakpad", "libbreakpad_client.a"));
        }
        else
        {
            PrivateDefinitions.Add("WITH_CRASH_DUMP=0");
        }

    }
}
