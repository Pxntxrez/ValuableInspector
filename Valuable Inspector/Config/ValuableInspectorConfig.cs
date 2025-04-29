using BepInEx.Configuration;
using UnityEngine;

public static class ValuableInspectorConfig
{
    public static ConfigEntry<bool> RemoveCloneSuffix;
    public static ConfigEntry<bool> RemoveValuablePrefix;

    public static ConfigEntry<bool> ShowName;
    public static ConfigEntry<bool> ShowValue;
    public static ConfigEntry<bool> ShowWeight;
    public static ConfigEntry<bool> ShowStunInfo;

    public static ConfigEntry<int> BoxWidth;
    public static ConfigEntry<int> BoxHeight;
    public static ConfigEntry<int> OffsetX;
    public static ConfigEntry<int> OffsetY;
    public static ConfigEntry<int> FontSize;
    public static ConfigEntry<FontStyle> FontStyleSetting;

    public static ConfigEntry<int> TextColorR;
    public static ConfigEntry<int> TextColorG;
    public static ConfigEntry<int> TextColorB;

    public static ConfigEntry<int> BackgroundColorR;
    public static ConfigEntry<int> BackgroundColorG;
    public static ConfigEntry<int> BackgroundColorB;

    public static ConfigEntry<int> ShadowColorR;
    public static ConfigEntry<int> ShadowColorG;
    public static ConfigEntry<int> ShadowColorB;

    public static ConfigEntry<Vector2> ShadowOffset;
    public static ConfigEntry<bool> AutoResize;

    public static void Init(ConfigFile config)
    {
        RemoveCloneSuffix = config.Bind("Debug", "RemoveCloneSuffix", true, "Remove (Clone) from names");
        RemoveValuablePrefix = config.Bind("Debug", "RemoveValuablePrefix", true, "Remove Valuable prefix");

        ShowName = config.Bind("Information", "ShowName", true, "Show Name line");
        ShowValue = config.Bind("Information", "ShowValue", true, "Show Value line");
        ShowWeight = config.Bind("Information", "ShowWeight", true, "Show Weight line");
        ShowStunInfo = config.Bind("Information", "ShowStunInfo", true, "Show Stuns on hit line");

        BoxWidth = config.Bind("UI Settings", "BoxWidth", 500,
            new ConfigDescription("Box Width", new AcceptableValueRange<int>(1, 800)));

        BoxHeight = config.Bind("UI Settings", "BoxHeight", 225,
            new ConfigDescription("Box Height", new AcceptableValueRange<int>(1, 600)));

        OffsetX = config.Bind("UI Settings", "OffsetX", 1,
            new ConfigDescription("Offset X", new AcceptableValueRange<int>(-250, 2500)));

        OffsetY = config.Bind("UI Settings", "OffsetY", 66,
            new ConfigDescription("Offset Y", new AcceptableValueRange<int>(-100, 1500)));

        FontSize = config.Bind("UI Settings", "FontSize", 28,
            new ConfigDescription("Font Size", new AcceptableValueRange<int>(1, 60)));

        FontStyleSetting = config.Bind("UI Settings", "FontStyle", FontStyle.Bold, "Font Style");
        ShadowOffset = config.Bind("UI Settings", "ShadowOffset", new Vector2(2f, 2f), "Shadow offset");
        AutoResize = config.Bind("UI Settings", "AutoResize", true, "Automatically resize box to fit text");

        TextColorR = config.Bind("Color Settings", "TextColorR", 255,
            new ConfigDescription("Text Red", new AcceptableValueRange<int>(0, 255)));

        TextColorG = config.Bind("Color Settings", "TextColorG", 255,
            new ConfigDescription("Text Green", new AcceptableValueRange<int>(0, 255)));

        TextColorB = config.Bind("Color Settings", "TextColorB", 255,
            new ConfigDescription("Text Blue", new AcceptableValueRange<int>(0, 255)));

        BackgroundColorR = config.Bind("Color Settings", "BackgroundColorR", 0,
            new ConfigDescription("Background Red", new AcceptableValueRange<int>(0, 255)));

        BackgroundColorG = config.Bind("Color Settings", "BackgroundColorG", 0,
            new ConfigDescription("Background Green", new AcceptableValueRange<int>(0, 255)));

        BackgroundColorB = config.Bind("Color Settings", "BackgroundColorB", 0,
            new ConfigDescription("Background Blue", new AcceptableValueRange<int>(0, 255)));

        ShadowColorR = config.Bind("Color Settings", "ShadowColorR", 0,
            new ConfigDescription("Shadow Red", new AcceptableValueRange<int>(0, 255)));

        ShadowColorG = config.Bind("Color Settings", "ShadowColorG", 0,
            new ConfigDescription("Shadow Green", new AcceptableValueRange<int>(0, 255)));

        ShadowColorB = config.Bind("Color Settings", "ShadowColorB", 0,
            new ConfigDescription("Shadow Blue", new AcceptableValueRange<int>(0, 255)));
    }

    public static Color TextColor =>
        new Color(TextColorR.Value / 255f, TextColorG.Value / 255f, TextColorB.Value / 255f);

    public static Color BackgroundColor =>
        new Color(BackgroundColorR.Value / 255f, BackgroundColorG.Value / 255f, BackgroundColorB.Value / 255f);

    public static Color ShadowColor =>
        new Color(ShadowColorR.Value / 255f, ShadowColorG.Value / 255f, ShadowColorB.Value / 255f);
}
