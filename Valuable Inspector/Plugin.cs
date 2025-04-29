using BepInEx;
using HarmonyLib;
using UnityEngine;
using System.Text;
using System.Globalization;

[BepInPlugin("Pxntxrez.ValuableInspector", "Valuable Inspector", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    private Harmony harmony;
    private ValuableObject lastValuable;
    private string infoText = string.Empty;

    private void Awake()
    {
        harmony = new Harmony("Pxntxrez.ValuableInspector");
        harmony.PatchAll();

        ValuableInspectorConfig.Init(Config);
        Logger.LogInfo("Valuable Inspector initialized!");
    }

    private void Update()
    {
        ValuableObject current = null;

        foreach (var vo in ValuableRegistry.AllValuables)
        {
            if (vo == null || vo.physGrabObject == null)
                continue;

            var pg = vo.physGrabObject;
            bool isBroken = pg.dead;
            bool held = !isBroken && pg.playerGrabbing != null && pg.playerGrabbing.Count > 0
                        && ((GameManager.instance.gameMode == 0) ? pg.grabbed : pg.grabbedLocal);

            if (held)
            {
                current = vo;
                break;
            }
        }

        if (lastValuable != null && lastValuable.physGrabObject.dead)
            current = null;

        if (current != lastValuable)
        {
            if (current == null)
                ClearInfo();
            else
                BuildInfo(current);

            lastValuable = current;
        }
    }

    private void BuildInfo(ValuableObject vo)
    {
        string name = vo.gameObject.name;

        if (ValuableInspectorConfig.RemoveCloneSuffix.Value && name.EndsWith("(Clone)"))
            name = name.Substring(0, name.Length - 7);

        if (ValuableInspectorConfig.RemoveValuablePrefix.Value && name.StartsWith("Valuable "))
            name = name.Substring(9);

        int minV = Mathf.RoundToInt(vo.valuePreset.valueMin);
        int maxV = Mathf.RoundToInt(vo.valuePreset.valueMax);
        float w = vo.physAttributePreset.mass;

        StringBuilder sb = new();

        if (ValuableInspectorConfig.ShowName.Value)
            sb.AppendLine($"Name: {name}");

        if (ValuableInspectorConfig.ShowValue.Value)
            sb.AppendLine($"Value: {minV}-{maxV}");

        if (ValuableInspectorConfig.ShowWeight.Value)
            sb.AppendLine($"Weight: {FormatWeight(w)}");

        if (ValuableInspectorConfig.ShowStunInfo.Value)
        {
            string stun = GetStunInfo(w);
            if (!string.IsNullOrEmpty(stun))
                sb.AppendLine(stun);
        }

        infoText = sb.ToString().TrimEnd();
    }

    private string GetStunInfo(float weight)
    {
        if (weight < 0.5f)
            return null;

        if (weight < 2f)
        {
            return "Stuns on hit:\n- SHADOW CHILD\n- SPEWER\n- RUGRAT\n- ANIMAL\n- UPSCREAM\n- CHEF\n- HIDDEN";
        }
        else
        {
            return "Stuns on hit:\n- All Enemies except:\n  DUCK, PEAPER";
        }
    }

    private string FormatWeight(float weight)
    {
        if (Mathf.Approximately(weight % 1f, 0f))
        {
            return ((int)weight).ToString();
        }
        else
        {
            return weight.ToString("0.##", CultureInfo.InvariantCulture);
        }
    }

    private void ClearInfo()
    {
        infoText = string.Empty;
        lastValuable = null;
    }

    private void OnGUI()
    {
        if (string.IsNullOrEmpty(infoText))
            return;

        GUIStyle style = new GUIStyle(GUI.skin.label)
        {
            fontSize = ValuableInspectorConfig.FontSize.Value,
            fontStyle = ValuableInspectorConfig.FontStyleSetting.Value,
            wordWrap = true,
            normal = { textColor = ValuableInspectorConfig.TextColor }
        };

        Vector2 textSize = style.CalcSize(new GUIContent(infoText));

        float boxW = ValuableInspectorConfig.AutoResize.Value ? textSize.x + 40f : ValuableInspectorConfig.BoxWidth.Value;
        float boxH = ValuableInspectorConfig.AutoResize.Value ? textSize.y + 40f : ValuableInspectorConfig.BoxHeight.Value;

        Rect box = new Rect(
            Mathf.FloorToInt(Screen.width - boxW - ValuableInspectorConfig.OffsetX.Value),
            Mathf.FloorToInt(Screen.height - boxH - ValuableInspectorConfig.OffsetY.Value),
            boxW, boxH);

        GUI.color = ValuableInspectorConfig.BackgroundColor;
        GUI.Box(box, GUIContent.none);

        GUI.color = ValuableInspectorConfig.ShadowColor;
        Rect shadowRect = new Rect(
            box.x + 20f + ValuableInspectorConfig.ShadowOffset.Value.x,
            box.y + 20f + ValuableInspectorConfig.ShadowOffset.Value.y,
            boxW - 40f,
            boxH - 40f);
        GUI.Label(shadowRect, infoText, style);

        GUI.color = ValuableInspectorConfig.TextColor;
        Rect textRect = new Rect(box.x + 20f, box.y + 20f, boxW - 40f, boxH - 40f);
        GUI.Label(textRect, infoText, style);
    }
}
