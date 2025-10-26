using UnityEditor;
using UnityEngine;
using System;

public class GerstnerWavesShaderGUI : ShaderGUI
{
    // These bools remember the foldout state
    private bool showGlobals = true;
    private bool showFoam = true; // <-- ADDED THIS
    private bool showWave1 = true;
    private bool showWave2 = true;
    private bool showWave3 = true;
    private bool showWave4 = true;

    // Declare properties
    MaterialProperty gravity, water, waterDepth;
    MaterialProperty outlineColor, outlineThickness;
    MaterialProperty normalMap, normalTime, normalScale;
    MaterialProperty dir1, depth1, phase1, scaleTime1, amp1;
    MaterialProperty dir2, depth2, phase2, scaleTime2, amp2;
    MaterialProperty dir3, depth3, phase3, scaleTime3, amp3;
    MaterialProperty dir4, depth4, phase4, scaleTime4, amp4;

    // --- ADDED FOAM PROPERTIES ---
    MaterialProperty foamHeight, foamBlendRange, foamTiling, foamSpeed, foamTexture, foamSharpness, foamColor;


    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        // --- Globals ---
        gravity = FindProperty("_Gravity", properties);
        water = FindProperty("_Water", properties);
        waterDepth = FindProperty("_WaterDepth", properties);
        outlineColor = FindProperty("_OutlineColor", properties);
        outlineThickness = FindProperty("_OutlineThickness", properties);
        normalMap = FindProperty("_NormalMap", properties);
        normalTime = FindProperty("_NormalTime", properties);
        normalScale = FindProperty("_NormalScale", properties);

        // --- Foam --- (Added this section)
        foamHeight = FindProperty("_FoamHeight", properties);
        foamBlendRange = FindProperty("_FoamBlendRange", properties);
        foamTiling = FindProperty("_FoamTiling", properties);
        foamSpeed = FindProperty("_FoamSpeed", properties);
        foamTexture = FindProperty("_FoamTexture", properties);
        foamSharpness = FindProperty("_FoamSharpness", properties);
        foamColor = FindProperty("_FoamColor", properties);

        // --- Wave 1 ---
        dir1 = FindProperty("_Direction_1", properties);
        depth1 = FindProperty("_Depth_1", properties);
        phase1 = FindProperty("_Phase_1", properties);
        scaleTime1 = FindProperty("_ScaleTime_1", properties);
        amp1 = FindProperty("_Amplitude_1", properties);

        // --- Wave 2 ---
        dir2 = FindProperty("_Direction_2", properties);
        depth2 = FindProperty("_Depth_2", properties);
        phase2 = FindProperty("_Phase_2", properties);
        scaleTime2 = FindProperty("_ScaleTime_2", properties);
        amp2 = FindProperty("_Amplitude_2", properties);

        // --- Wave 3 ---
        dir3 = FindProperty("_Direction_3", properties);
        depth3 = FindProperty("_Depth_3", properties);
        phase3 = FindProperty("_Phase_3", properties);
        scaleTime3 = FindProperty("_ScaleTime_3", properties);
        amp3 = FindProperty("_Amplitude_3", properties);

        // --- Wave 4 (This assumes the 4th wave has un-numbered references) ---
        dir4 = FindProperty("_Direction", properties);
        depth4 = FindProperty("_Depth", properties);
        phase4 = FindProperty("_Phase", properties);
        scaleTime4 = FindProperty("_ScaleTime", properties);
        amp4 = FindProperty("_Amplitude", properties);

        // --- Draw the Inspector GUI ---

        showGlobals = EditorGUILayout.BeginFoldoutHeaderGroup(showGlobals, "Global Settings");
        if (showGlobals)
        {
            materialEditor.ShaderProperty(gravity, "Gravity");
            materialEditor.ShaderProperty(water, "Water");
            materialEditor.ShaderProperty(waterDepth, "Water Depth");

            EditorGUILayout.Space(); // Adds a small gap

            materialEditor.ShaderProperty(outlineColor, "Outline Color");
            materialEditor.ShaderProperty(outlineThickness, "Outline Thickness");

            EditorGUILayout.Space();

            materialEditor.ShaderProperty(normalMap, "Normal Map");
            materialEditor.ShaderProperty(normalTime, "Normal Time");
            materialEditor.ShaderProperty(normalScale, "Normal Scale");

            EditorGUILayout.Space();
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        // --- Draw Foam Settings --- (Added this section)
        showFoam = EditorGUILayout.BeginFoldoutHeaderGroup(showFoam, "Foam Settings");
        if (showFoam)
        {
            materialEditor.ShaderProperty(foamColor, "Foam Color");
            materialEditor.ShaderProperty(foamTexture, "Foam Texture");
            materialEditor.ShaderProperty(foamHeight, "Foam Height");
            materialEditor.ShaderProperty(foamBlendRange, "Foam Blend Range");
            materialEditor.ShaderProperty(foamTiling, "Foam Tiling");
            materialEditor.ShaderProperty(foamSpeed, "Foam Speed");
            materialEditor.ShaderProperty(foamSharpness, "Foam Sharpness");
            EditorGUILayout.Space();
        }
        EditorGUILayout.EndFoldoutHeaderGroup();


        // --- Draw Wave 1 ---
        showWave1 = EditorGUILayout.BeginFoldoutHeaderGroup(showWave1, "Wave 1");
        if (showWave1)
        {
            materialEditor.ShaderProperty(dir1, "Direction");
            materialEditor.ShaderProperty(depth1, "Depth");
            materialEditor.ShaderProperty(phase1, "Phase");
            materialEditor.ShaderProperty(scaleTime1, "Scale Time");
            materialEditor.ShaderProperty(amp1, "Amplitude");
            EditorGUILayout.Space();
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        // --- Draw Wave 2 ---
        showWave2 = EditorGUILayout.BeginFoldoutHeaderGroup(showWave2, "Wave 2");
        if (showWave2)
        {
            materialEditor.ShaderProperty(dir2, "Direction");
            materialEditor.ShaderProperty(depth2, "Depth");
            materialEditor.ShaderProperty(phase2, "Phase");
            materialEditor.ShaderProperty(scaleTime2, "Scale Time");
            materialEditor.ShaderProperty(amp2, "Amplitude");
            EditorGUILayout.Space();
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        // --- Draw Wave 3 ---
        showWave3 = EditorGUILayout.BeginFoldoutHeaderGroup(showWave3, "Wave 3");
        if (showWave3)
        {
            materialEditor.ShaderProperty(dir3, "Direction");
            materialEditor.ShaderProperty(depth3, "Depth");
            materialEditor.ShaderProperty(phase3, "Phase");
            materialEditor.ShaderProperty(scaleTime3, "Scale Time");
            materialEditor.ShaderProperty(amp3, "Amplitude");
            EditorGUILayout.Space();
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        // --- Draw Wave 4 ---
        showWave4 = EditorGUILayout.BeginFoldoutHeaderGroup(showWave4, "Wave 4");
        if (showWave4)
        {
            materialEditor.ShaderProperty(dir4, "Direction");
            materialEditor.ShaderProperty(depth4, "Depth");
            materialEditor.ShaderProperty(phase4, "Phase");
            materialEditor.ShaderProperty(scaleTime4, "Scale Time");
            materialEditor.ShaderProperty(amp4, "Amplitude");
            EditorGUILayout.Space();
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }

    // Helper function to find properties
    private MaterialProperty FindProperty(string name, MaterialProperty[] properties, bool propertyIsMandatory = true)
    {
        MaterialProperty prop = ShaderGUI.FindProperty(name, properties, false);
        if (prop == null && propertyIsMandatory)
            Debug.LogWarning($"Could not find MaterialProperty: '{name}'. Check the Reference name in your Shader Graph.");
        return prop;
    }
}