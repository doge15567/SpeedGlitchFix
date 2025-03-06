using System.Reflection;
using MelonLoader;

[assembly: AssemblyDescription(SpeedGlitchFix.Main.Description)]
[assembly: AssemblyCopyright("Developed by " + SpeedGlitchFix.Main.Author)]
[assembly: AssemblyTrademark(SpeedGlitchFix.Main.Company)]
[assembly: MelonInfo(typeof(SpeedGlitchFix.Main), SpeedGlitchFix.Main.Name, SpeedGlitchFix.Main.Version, SpeedGlitchFix.Main.Author, SpeedGlitchFix.Main.DownloadLink)]
[assembly: MelonColor(255, 138, 138, 138)]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame("Stress Level Zero", "BONELAB")]