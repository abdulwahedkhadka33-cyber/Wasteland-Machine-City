using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class TutorialSceneValidator
{
    private const string ScenePath = "Assets/Scenes/Tutorial_01_AwakeningCorridor.unity";
    private const string BackgroundV7Path = "Assets/Art/Generated/Backgrounds/V7";
    private const string BackgroundV8Path = "Assets/Art/Generated/Backgrounds/V8";
    private const string BackgroundV15Path = "Assets/Art/Generated/Backgrounds/V15";
    private const string EnvironmentV4Path = "Assets/Art/Generated/Environment/V4";
    private const string EnvironmentV7Path = "Assets/Art/Generated/Environment/V7";
    private const string EnvironmentV8Path = "Assets/Art/Generated/Environment/V8";
    private const string EnvironmentV9Path = "Assets/Art/Generated/Environment/V9";
    private const string EnvironmentV10Path = "Assets/Art/Generated/Environment/V10";
    private const string EnvironmentV11Path = "Assets/Art/Generated/Environment/V11";
    private const string EnvironmentV12Path = "Assets/Art/Generated/Environment/V12";
    private const string EnvironmentV19Path = "Assets/Art/Generated/Environment/V19";
    private const string EnvironmentV20Path = "Assets/Art/Generated/Environment/V20";
    private const string EffectsV1Path = "Assets/Art/Generated/Effects/V1";
    private const string EffectsV2Path = "Assets/Art/Generated/Effects/V2";
    private const string EffectsV3Path = "Assets/Art/Generated/Effects/V3";
    private const string EffectsV4Path = "Assets/Art/Generated/Effects/V4";
    private const string EffectsV5Path = "Assets/Art/Generated/Effects/V5";
    private const string EffectsV7Path = "Assets/Art/Generated/Effects/V7";
    private const string EnemyV2Path = "Assets/Art/Generated/Enemies/V2";
    private const string EnemyV3Path = "Assets/Art/Generated/Enemies/V3";
    private const string EnemyV4Path = "Assets/Art/Generated/Enemies/V4";
    private const string EnvironmentV5Path = "Assets/Art/Generated/Environment/V5";
    private const string ProvidedEnvironmentV2Path = "Assets/Art/Provided/Environment/V2";
    private const string ProvidedEnvironmentV3Path = "Assets/Art/Provided/Environment/V3";
    private const string ProvidedEnvironmentV4Path = "Assets/Art/Provided/Environment/V4";
    private const float BackgroundCoverageMargin = 2f;

    private static readonly string[] BackgroundV7ChunkNames =
    {
        "BackgroundPanorama_RustRepairStation_v7_01",
        "BackgroundPanorama_RustRepairStation_v7_02",
        "BackgroundPanorama_RustRepairStation_v7_03",
        "BackgroundPanorama_RustRepairStation_v7_04",
        "BackgroundPanorama_RustRepairStation_v7_05",
    };

    private static readonly string[] BackgroundV8ChunkNames =
    {
        "BackgroundPanorama_RustRepairStation_v8_01",
        "BackgroundPanorama_RustRepairStation_v8_02",
        "BackgroundPanorama_RustRepairStation_v8_03",
        "BackgroundPanorama_RustRepairStation_v8_04",
        "BackgroundPanorama_RustRepairStation_v8_05",
    };

    private static readonly string[] BackgroundV15ChunkNames =
    {
        "BackgroundPanorama_RustRepairStation_v15_01",
        "BackgroundPanorama_RustRepairStation_v15_02",
        "BackgroundPanorama_RustRepairStation_v15_03",
        "BackgroundPanorama_RustRepairStation_v15_04",
        "BackgroundPanorama_RustRepairStation_v15_05",
    };

    private static readonly string[] EnvironmentV7SpriteFiles =
    {
        "envv7_main_floor_module.png",
        "envv7_platform_edge.png",
        "envv7_broken_platform.png",
        "envv7_maintenance_bench.png",
        "envv7_repair_arm.png",
        "envv7_pipe_cluster_top.png",
        "envv7_hanging_chain.png",
        "envv7_cable_bundle.png",
        "envv7_gear_wall.png",
        "envv7_warning_lamp.png",
        "envv7_indicator_lamp_amber.png",
        "envv7_hanging_lamp.png",
        "envv7_crate.png",
        "envv7_compressor_shell.png",
        "envv7_charging_station.png",
        "envv7_boss_door.png",
        "envv7_exit_door.png",
        "envv7_service_panel.png",
        "envv7_robot_debris.png",
        "envv7_mechanical_arm.png",
        "envv7_sign_arrow.png",
        "envv7_scrap_pile.png",
        "envv7_transition_truss.png",
        "envv7_far_tower_cluster.png",
        "envv7_fan_blade_cluster.png",
        "envv7_oil_pool.png",
        "envv7_terminal_console.png",
        "envv7_boss_hall_pipe.png",
        "envv7_trap_machine_frame.png",
        "envv7_bridge_truss.png",
    };

    private static readonly string[] EnvironmentV8SpriteFiles =
    {
        "envv8_road_dark_steel_module.png",
        "envv8_road_amber_top_lip.png",
        "envv8_road_front_shadow.png",
        "envv8_road_under_truss_subtle.png",
        "envv8_platform_end_cap_left.png",
        "envv8_platform_end_cap_right.png",
        "envv8_broken_platform_marks.png",
        "envv8_oil_pool_readable.png",
        "envv8_top_pipe_soft.png",
        "envv8_service_panel_dim.png",
        "envv8_far_gear_soft.png",
        "envv8_hanging_lamp_soft.png",
    };

    private static readonly string[] EnvironmentV9SpriteFiles =
    {
        "envv9_wall_support_bracket.png",
        "envv9_service_panel_lit.png",
        "envv9_valve_pipe_cluster.png",
        "envv9_hanging_cable_bundle.png",
        "envv9_hanging_lamp_detailed.png",
        "envv9_ventilation_fan.png",
        "envv9_far_mechanical_arm.png",
        "envv9_old_gauge_cluster.png",
        "envv9_warning_lamp_shell.png",
        "envv9_far_crane_hook.png",
        "envv9_door_frame_pipe.png",
    };

    private static readonly string[] EnvironmentV10SpriteFiles =
    {
        "envv10_metal_seam_strip.png",
        "envv10_rivet_strip.png",
        "envv10_scratch_plate_overlay.png",
        "envv10_oil_stain_overlay.png",
        "envv10_cracked_metal_overlay.png",
        "envv10_amber_edge_scuff.png",
        "envv10_raised_step_shell.png",
        "envv10_low_obstacle_shell.png",
        "envv10_floating_platform_underbeam.png",
        "envv10_broken_corner_cap.png",
        "envv10_side_cap_short.png",
        "envv10_shallow_front_shadow.png",
    };

    private static readonly string[] EnvironmentV11SpriteFiles =
    {
        "envv11_hanging_lamp.png",
        "envv11_street_lamp.png",
        "envv11_chain_hook.png",
        "envv11_far_crate_stack.png",
        "envv11_far_conveyor.png",
        "envv11_service_panel.png",
        "envv11_broken_cable_bundle.png",
        "envv11_repair_wall_plate.png",
    };

    private static readonly string[] EnvironmentV12SpriteFiles =
    {
        "spawn_repair_bed_refined.png",
    };

    private static readonly string[] EnvironmentV19SpriteFiles =
    {
        "component_terminal_screen_overlay.png",
        "component_control_lock_plate_overlay.png",
        "component_status_core_overlay.png",
    };

    private static readonly string[] EnvironmentV20SpriteFiles =
    {
        "boss_entry_lock_refined_overlay.png",
        "boss_exit_gate_refined_overlay.png",
    };

    private static readonly string[] ProvidedEnvironmentV2SpriteFiles =
    {
        "provided_v2_chain_long.png",
        "provided_v2_storage_crate.png",
        "provided_v2_pipe_horizontal.png",
        "provided_v2_truss_support.png",
        "provided_v2_pipe_vertical.png",
        "provided_v2_hanging_lamp.png",
    };

    private static readonly string[] ProvidedEnvironmentV3SpriteFiles =
    {
        "provided_v3_double_door.png",
        "provided_v3_chain_fence.png",
        "provided_v3_electric_box.png",
        "provided_v3_vent_grate.png",
        "provided_v3_catwalk_grate.png",
        "provided_v3_broken_window.png",
        "provided_v3_red_beacon.png",
        "provided_v3_vent_fan.png",
        "provided_v3_oil_barrel.png",
        "provided_v3_tire.png",
        "provided_v3_scrap_heap.png",
        "provided_v3_broken_robot_arm.png",
        "provided_v3_generator.png",
        "provided_v3_burning_barrel.png",
        "provided_v3_torn_flag.png",
        "provided_v3_toolbox.png",
        "provided_v3_tool_pile.png",
        "provided_v3_robot_wreck.png",
    };

    private static readonly string[] ProvidedEnvironmentV4SpriteFiles =
    {
        "large_machine_frame_v4.png",
        "large_crane_hook_v4.png",
        "large_wall_fan_v4.png",
        "large_overhead_crane_rail_v4.png",
        "large_gear_wall_v4.png",
        "large_boiler_tank_v4.png",
        "large_conveyor_back_v4.png",
    };

    private static readonly string[] EffectsV2SpriteFiles =
    {
        "fxv2_electric_floor_01.png",
        "fxv2_electric_floor_02.png",
        "fxv2_electric_floor_03.png",
        "fxv2_steam_puff.png",
        "fxv2_spark_shower.png",
        "fxv2_lamp_halo_amber.png",
        "fxv2_lamp_halo_red.png",
        "fxv2_dust_veil.png",
        "fxv2_boss_smash_dust.png",
        "fxv2_oil_drip.png",
        "fxv2_scan_beam.png",
    };

    private static readonly string[] EffectsV3SpriteFiles =
    {
        "fxv3_electric_arc_small.png",
        "fxv3_lamp_halo_soft.png",
        "fxv3_thin_steam.png",
        "fxv3_dust_motes.png",
        "fxv3_oil_haze.png",
        "fxv3_fan_shadow.png",
        "fxv3_green_charge_pulse.png",
    };

    private static readonly string[] EffectsV4SpriteFiles =
    {
        "fxv4_thin_steam_wisp.png",
        "fxv4_dust_veil.png",
        "fxv4_lamp_halo_amber.png",
        "fxv4_electric_arc_frame.png",
        "fxv4_fan_shadow.png",
        "fxv4_oil_haze.png",
        "fxv4_warning_blink_red.png",
    };

    private static readonly string[] EffectsV5SpriteFiles =
    {
        "fxv5_falling_dust_curtain.png",
        "fxv5_fine_dust_motes.png",
        "fxv5_lamp_halo_amber.png",
        "fxv5_electric_spark_frame.png",
        "fxv5_fall_fog_plume.png",
        "fxv5_long_falling_dust_veil.png",
    };

    private static readonly string[] EffectsV7SpriteFiles =
    {
        "attack_combo_01_arc.png",
        "attack_combo_02_arc.png",
        "attack_combo_03_arc.png",
        "attack_air_slash_arc.png",
        "attack_hit_spark_heavy.png",
        "attack_charge_flash.png",
    };

    private static readonly string[] EnemyV3SpriteFiles =
    {
        "bossv3_guardian_refined_overlay.png",
    };

    private static readonly string[] EnemyV4SpriteFiles =
    {
        "bossv4_guardian_overload_overlay.png",
    };

    private static readonly (string Name, Vector2 Position, Vector2 Size)[] PlatformExpectations =
    {
        ("Floor_AwakeningBench", new Vector2(9f, -3.1f), new Vector2(18f, 1.2f)),
        ("Floor_MovementTutorial_A", new Vector2(30.6f, -3.15f), new Vector2(25.2f, 1.1f)),
        ("Movement_LowObstacle", new Vector2(22.8f, -2.19f), new Vector2(1.35f, 0.82f)),
        ("Jump_FirstGapPlatform", new Vector2(46.7f, -2.05f), new Vector2(2.4f, 0.5f)),
        ("Jump_MidPlatform", new Vector2(51.0f, -1.22f), new Vector2(3.0f, 0.58f)),
        ("Jump_OptionalRewardPlatform", new Vector2(52.2f, 0.95f), new Vector2(2.8f, 0.42f)),
        ("Jump_RightPlatform", new Vector2(56.6f, -2.18f), new Vector2(3.2f, 0.58f)),
        ("Floor_FirstEnemyArena", new Vector2(74.1f, -3.05f), new Vector2(23.8f, 1.1f)),
        ("Floor_TrapEntry", new Vector2(89.5f, -3.05f), new Vector2(7.0f, 1.1f)),
        ("Floor_TrapExit", new Vector2(109.8f, -3.05f), new Vector2(10.4f, 1.1f)),
        ("Floor_ChargingSavePoint", new Vector2(120f, -3.05f), new Vector2(12f, 1.1f)),
        ("Floor_BossRepairHall", new Vector2(143f, -3.05f), new Vector2(34f, 1.1f)),
        ("Floor_MechCityGate", new Vector2(168f, -3.05f), new Vector2(16f, 1.1f)),
    };

    [MenuItem("Tools/Wasteland Mech City/Validate Tutorial Scene")]
    public static void ValidateTutorialScene()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            EditorApplication.isPlaying = false;
            EditorApplication.delayCall += ValidateTutorialScene;
            Debug.Log("Exiting Play Mode before validating the tutorial scene.");
            return;
        }

        if (!System.IO.File.Exists(ScenePath))
        {
            throw new Exception($"Missing tutorial scene: {ScenePath}");
        }

        EditorSceneManager.OpenScene(ScenePath);

        RequireCoreObjects();
        ForbidLegacyRootsAndSprites();
        RequireGeneratedAssets();
        RequireNoVisibleWhitePixel();
        RequireBackgroundChunks();
        RequireRustRepairStationLayout();
        RequireV8PlatformVisuals();
        RequireCleanDecorV8();
        RequireDecorativePropsV9();
        RequireLayeredDecorV10();
        RequireLayeredDecorV11();
        RequireSingleOpeningLamp();
        RequireProvidedLayeredDecorV2();
        RequireProvidedLayeredDecorV3();
        RequireDynamicDecorV13();
        RequireLargeDecorV14();
        RequireMapReadableDecorV15();
        RequireBackgroundAtmosphereV12();
        RequireSystemPolishReadability();
        RequireDynamicPolishV16();
        RequireGameplayObjects();
        RequirePlayerAndAnimationObjects();
        RequireSpawnIntroObjects();
        RequireJumpRouteBackgroundPolish();
        RequireRespawnCinematicObjects();
        RequireDynamicAtmosphere();

        ChipData repairChip = AssetDatabase.LoadAssetAtPath<ChipData>("Assets/Data/RepairChip.asset");
        if (repairChip == null || repairChip.chipId != "repair_chip" || !repairChip.healOnEnemyKill)
        {
            throw new Exception("RepairChip.asset is missing or incorrectly configured.");
        }

        bool sceneInBuild = EditorBuildSettings.scenes.Any(scene => scene.path == ScenePath && scene.enabled);
        if (!sceneInBuild)
        {
            throw new Exception($"{ScenePath} is not enabled in Build Settings.");
        }

        Debug.Log("Tutorial scene validation passed.");
    }

    private static void RequireCoreObjects()
    {
        RequireObject("Player_SmallAmnesiacRobot");
        RequireObject("Main Camera");
        RequireObject("Visual_PlayerRobot_Parts");
        RequireObject("BG_PanoramaChunks_V15");
        RequireObject("BG_CleanDecor_V8");
        RequireObject("BG_DecorativeProps_V9");
        RequireObject("BG_FarDecor_V10");
        RequireObject("BG_MidDecor_V10");
        RequireObject("BG_NearDecor_V10");
        RequireObject("BG_FarDecor_V11");
        RequireObject("BG_MidDecor_V11");
        RequireObject("BG_NearDecor_V11");
        RequireObject("BG_ProvidedDecor_V2");
        RequireObject("BG_ProvidedDecor_V3");
        RequireObject("BG_DynamicDecor_V13");
        RequireObject("BG_LargeDecor_V14");
        RequireObject("BG_MapReadableDecor_V15");
        RequireObject("BG_BackgroundAtmosphere_V12");
        RequireObject("BG_SystemPolish_Readability");
        RequireObject("BG_SystemPolish_RouteFocus");
        RequireObject("BG_SystemPolish_Landmarks");
        RequireObject("BG_SystemPolish_HazardAndInteract");
        RequireObject("BG_SystemPolish_AtmosphereControl");
        RequireObject("BG_DynamicPolish_V16");
        RequireObject("BG_DynamicPolish_V16_Far");
        RequireObject("BG_DynamicPolish_V16_Mid");
        RequireObject("BG_DynamicPolish_V16_NearSafe");
        RequireObject("BG_DynamicPolish_V16_GameplayReadable");
        RequireObject("BG_AnimatedLayers_V2");
        RequireObject("05_Environmental_Details");
        RequireObject("SpawnIntro_AwakeningPolish");
        RequireObject("RespawnPolish_Runtime");
        RequireObject("OnGUI_TutorialHUD");
        RequireComponent<LevelObjectiveUI>("OnGUI_TutorialHUD");
        RequireHudIndustrialTerminal();
        RequireComponent<CameraFollow2D>("Main Camera");
        RequireRuntimeBossApis();
    }

    private static void RequireHudIndustrialTerminal()
    {
        RequirePrivateFloat<LevelObjectiveUI>("Margin", 18f, 0.01f);
        RequirePrivateFloat<LevelObjectiveUI>("TopMargin", 16f, 0.01f);
        RequirePrivateFloat<LevelObjectiveUI>("ObjectivePanelMaxWidth", 304f, 0.01f);
        RequirePrivateFloat<LevelObjectiveUI>("HealthPanelWidth", 158f, 0.01f);
        RequirePrivateFloat<LevelObjectiveUI>("TopPanelHeight", 34f, 0.01f);
        RequirePrivateFloat<LevelObjectiveUI>("BossPanelMaxWidth", 436f, 0.01f);
        RequirePrivateFloat<LevelObjectiveUI>("PromptPanelMaxWidth", 330f, 0.01f);
        RequirePrivateFloat<LevelObjectiveUI>("HintPanelMaxWidth", 560f, 0.01f);

        RequirePrivateMethod<LevelObjectiveUI>("DrawIndustrialPanel");
        RequirePrivateMethod<LevelObjectiveUI>("DrawCornerBolts");
        RequirePrivateMethod<LevelObjectiveUI>("DrawScanNoise");
        RequirePrivateMethod<LevelObjectiveUI>("DrawSegmentBarFill");
        RequirePrivateMethod<LevelObjectiveUI>("DrawKeyPrompt");
        RequirePrivateMethod<LevelObjectiveUI>("PixelRect");
    }

    private static void RequirePrivateFloat<T>(string fieldName, float expected, float tolerance)
    {
        FieldInfo field = typeof(T).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
        if (field == null || field.FieldType != typeof(float))
        {
            throw new Exception($"{typeof(T).Name} must define private const/static float {fieldName}.");
        }

        float actual = (float)field.GetValue(null);
        if (Mathf.Abs(actual - expected) > tolerance)
        {
            throw new Exception($"{typeof(T).Name}.{fieldName} expected {expected}, found {actual}.");
        }
    }

    private static void RequirePrivateMethod<T>(string methodName)
    {
        if (typeof(T).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static) == null)
        {
            throw new Exception($"{typeof(T).Name} must define private helper {methodName}.");
        }
    }

    private static void RequireRuntimeBossApis()
    {
        if (typeof(LevelObjectiveUI).GetMethod("ShowBossHealth", new[] { typeof(string), typeof(Health) }) == null ||
            typeof(LevelObjectiveUI).GetMethod("HideBossHealth", Type.EmptyTypes) == null)
        {
            throw new Exception("LevelObjectiveUI must expose ShowBossHealth(string, Health) and HideBossHealth().");
        }

        if (typeof(CameraFollow2D).GetMethod("SetTemporaryBounds", new[] { typeof(Vector2), typeof(Vector2) }) == null ||
            typeof(CameraFollow2D).GetMethod("ClearTemporaryBounds", Type.EmptyTypes) == null)
        {
            throw new Exception("CameraFollow2D must expose temporary bounds APIs for the boss encounter.");
        }

        if (typeof(PlayerController2D).GetMethod("SetInputLocked", new[] { typeof(bool) }) == null)
        {
            throw new Exception("PlayerController2D must expose SetInputLocked(bool) for spawn intro control.");
        }

        if (typeof(PlayerRobotVisualAnimator2D).GetMethod("SetSpawnIntroPose", new[] { typeof(float), typeof(float) }) == null)
        {
            throw new Exception("PlayerRobotVisualAnimator2D must expose SetSpawnIntroPose(float, float) for spawn intro posing.");
        }

        if (typeof(PlayerRespawnCinematic2D).GetMethod("PlayRespawn", new[] { typeof(Vector2) }) == null)
        {
            throw new Exception("PlayerRespawnCinematic2D must expose PlayRespawn(Vector2) for checkpoint respawn polish.");
        }

        if (typeof(RepairStationBoss2D).GetMethod("BeginEncounter", new[] { typeof(Transform) }) == null)
        {
            throw new Exception("RepairStationBoss2D must expose BeginEncounter(Transform).");
        }
    }

    private static void ForbidLegacyRootsAndSprites()
    {
        string[] forbiddenObjects =
        {
            "BG_PanoramaChunks_V6",
            "BG_PanoramaChunks_V7",
            "BG_PanoramaChunks_V8",
            "BG_AnimatedLayers",
            "BG_OrganizedDecor_V2",
            "BG_Parallax_CleanDepthFrames",
            "BG_ArtDirectedDecor_V7",
            "BG_Parallax_FarArt_V7",
            "Visual_RobotReference_Blockout",
            "ChipTerminal_RepairChip",
            "Pickup_RepairChip",
            "Door_Manual_Valve",
            "Door_Chip_Tutorial",
            "Door_Final_EnergyDistrict",
            "Enemy_ExitSentinel",
            "ExitGoal_EnergyDistrict",
            "Awakening_V7_MechanicalArm_Left",
            "Awakening_V7_MechanicalArm_Right",
            "Jump_V9_FarMechanicalArm",
            "Boss_V9_FarMechanicalArm",
            "FarDecor_V10_MechArm_AwakeningSilhouette",
            "FarDecor_V10_MechArm_BossSilhouette",
            "Jump_V9_CraneHook_Sway",
            "V14_NearCraneHook_Platform",
        };

        foreach (string objectName in forbiddenObjects)
        {
            ForbidObject(objectName);
        }

        string[] forbiddenPrefixes =
        {
            "BackgroundPanel_",
            "BackgroundPanorama_TutorialCorridor_v",
            "BackgroundPanorama_RustRepairStation_v7",
            "BG_VisibleDecor_Provided",
            "Decor_Provided_",
            "Near_Upper_Pipe_",
            "BG_Backwall_Panel_",
            "Far_Silhouette_Tower_",
            "CleanDepth_",
            "WarningBeacon_Row",
            "BG_WarningBeacon_Row",
            "BG_Gauge_Row",
            "BG_OilDrip_",
        };

        foreach (string prefix in forbiddenPrefixes)
        {
            ForbidSceneObjectPrefix(prefix);
            ForbidSceneSpritePrefix(prefix);
        }

        ForbidSceneObjectNameContains("_V17_");
        ForbidSceneObjectNameContains("_Visual_V18Road");
        ForbidSceneObjectNameContains("_V18_");

        if (AssetDatabase.IsValidFolder("Assets/Art/Generated/Environment/V18") ||
            AssetDatabase.FindAssets("envv18", new[] { "Assets/Art/Generated/Environment" }).Length > 0)
        {
            throw new Exception("V18 road assets are forbidden for the current small-refine road pass.");
        }

        ForbidSceneSpritePathPrefix(EnvironmentV5Path);
        ForbidSceneSpritePathPrefix(BackgroundV7Path);
        ForbidSceneSpritePathPrefix(BackgroundV8Path);
    }

    private static void RequireGeneratedAssets()
    {
        foreach (string chunkName in BackgroundV15ChunkNames)
        {
            RequireGeneratedSpriteImportSettings($"{BackgroundV15Path}/{chunkName}.png", 96, 4096, FilterMode.Bilinear, "V15 background chunk");
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>($"{BackgroundV15Path}/{chunkName}.png");
            if (texture.width != 4096 || texture.height != 2048)
            {
                throw new Exception($"{chunkName} must be 4096x2048, found {texture.width}x{texture.height}.");
            }
        }

        foreach (string fileName in EnvironmentV7SpriteFiles)
        {
            RequireGeneratedSpriteImportSettings($"{EnvironmentV7Path}/{fileName}", 256, 4096, FilterMode.Bilinear, "Environment/V7 sprite");
        }

        foreach (string fileName in EnvironmentV8SpriteFiles)
        {
            RequireGeneratedSpriteImportSettings($"{EnvironmentV8Path}/{fileName}", 256, 4096, FilterMode.Bilinear, "Environment/V8 sprite");
        }

        foreach (string fileName in EnvironmentV9SpriteFiles)
        {
            RequireGeneratedSpriteImportSettings($"{EnvironmentV9Path}/{fileName}", 256, 4096, FilterMode.Bilinear, "Environment/V9 sprite");
        }

        foreach (string fileName in EnvironmentV10SpriteFiles)
        {
            RequireGeneratedSpriteImportSettings($"{EnvironmentV10Path}/{fileName}", 256, 4096, FilterMode.Bilinear, "Environment/V10 sprite");
        }

        foreach (string fileName in EnvironmentV11SpriteFiles)
        {
            RequireGeneratedSpriteImportSettings($"{EnvironmentV11Path}/{fileName}", 256, 4096, FilterMode.Bilinear, "Environment/V11 sprite");
        }

        foreach (string fileName in EnvironmentV12SpriteFiles)
        {
            RequireGeneratedSpriteImportSettings($"{EnvironmentV12Path}/{fileName}", 256, 4096, FilterMode.Bilinear, "Environment/V12 sprite");
        }

        foreach (string fileName in EnvironmentV19SpriteFiles)
        {
            RequireGeneratedSpriteImportSettings($"{EnvironmentV19Path}/{fileName}", 256, 4096, FilterMode.Bilinear, "Environment/V19 component overlay");
        }

        foreach (string fileName in EnvironmentV20SpriteFiles)
        {
            RequireGeneratedSpriteImportSettings($"{EnvironmentV20Path}/{fileName}", 256, 4096, FilterMode.Bilinear, "Environment/V20 boss door overlay");
        }

        foreach (string fileName in ProvidedEnvironmentV2SpriteFiles)
        {
            RequireGeneratedSpriteImportSettings($"{ProvidedEnvironmentV2Path}/{fileName}", 256, 4096, FilterMode.Bilinear, "Provided Environment/V2 sprite");
        }

        foreach (string fileName in ProvidedEnvironmentV3SpriteFiles)
        {
            RequireGeneratedSpriteImportSettings($"{ProvidedEnvironmentV3Path}/{fileName}", 256, 4096, FilterMode.Bilinear, "Provided Environment/V3 sprite");
        }

        foreach (string fileName in ProvidedEnvironmentV4SpriteFiles)
        {
            RequireGeneratedSpriteImportSettings($"{ProvidedEnvironmentV4Path}/{fileName}", 256, 4096, FilterMode.Bilinear, "Provided Environment/V4 sprite");
        }

        foreach (string fileName in EffectsV2SpriteFiles)
        {
            RequireGeneratedSpriteImportSettings($"{EffectsV2Path}/{fileName}", 256, 4096, FilterMode.Bilinear, "Effects/V2 sprite");
        }

        foreach (string fileName in EffectsV3SpriteFiles)
        {
            RequireGeneratedSpriteImportSettings($"{EffectsV3Path}/{fileName}", 256, 4096, FilterMode.Bilinear, "Effects/V3 sprite");
        }

        foreach (string fileName in EffectsV4SpriteFiles)
        {
            RequireGeneratedSpriteImportSettings($"{EffectsV4Path}/{fileName}", 256, 4096, FilterMode.Bilinear, "Effects/V4 sprite");
        }

        foreach (string fileName in EffectsV5SpriteFiles)
        {
            RequireGeneratedSpriteImportSettings($"{EffectsV5Path}/{fileName}", 256, 4096, FilterMode.Bilinear, "Effects/V5 sprite");
        }

        foreach (string fileName in EffectsV7SpriteFiles)
        {
            RequireGeneratedSpriteImportSettings($"{EffectsV7Path}/{fileName}", 256, 4096, FilterMode.Bilinear, "Effects/V7 sprite");
        }

        foreach (string fileName in EnemyV3SpriteFiles)
        {
            RequireGeneratedSpriteImportSettings($"{EnemyV3Path}/{fileName}", 256, 4096, FilterMode.Bilinear, "Enemy/V3 boss overlay");
        }

        foreach (string fileName in EnemyV4SpriteFiles)
        {
            RequireGeneratedSpriteImportSettings($"{EnemyV4Path}/{fileName}", 256, 4096, FilterMode.Bilinear, "Enemy/V4 boss overload overlay");
        }
    }

    private static void RequireBackgroundChunks()
    {
        Bounds combinedBounds = new Bounds(Vector3.zero, Vector3.zero);
        bool hasBounds = false;

        foreach (string chunkName in BackgroundV15ChunkNames)
        {
            GameObject chunk = RequireObject(chunkName);
            SpriteRenderer renderer = RequireSpriteRenderer(chunkName);
            string expectedPath = $"{BackgroundV15Path}/{chunkName}.png";
            string spritePath = AssetDatabase.GetAssetPath(renderer.sprite);
            if (spritePath != expectedPath)
            {
                throw new Exception($"{chunkName} references {spritePath}, expected {expectedPath}.");
            }

            Vector3 scale = chunk.transform.localScale;
            if (Mathf.Abs(scale.x) > 1.02f || Mathf.Abs(scale.y) > 1.02f)
            {
                throw new Exception($"{chunkName} must stay authored at scene scale. Current scale: {scale}.");
            }

            if (!hasBounds)
            {
                combinedBounds = renderer.bounds;
                hasBounds = true;
            }
            else
            {
                combinedBounds.Encapsulate(renderer.bounds);
            }
        }

        RequireCameraBackgroundCoverage(combinedBounds);
    }

    private static void RequireRustRepairStationLayout()
    {
        string[] sectionNames =
        {
            "Section_01_AwakeningBench",
            "Section_02_MovementTutorial",
            "Section_03_BrokenPlatformJump",
            "Section_04_FirstEnemy",
            "Section_05_TrapMachineHall",
            "Section_06_ChargingSavePoint",
            "Section_07_BossRepairHall",
            "Section_08_MechCityGate",
        };

        foreach (string sectionName in sectionNames)
        {
            RequireObject(sectionName);
        }

        foreach ((string name, Vector2 position, Vector2 size) in PlatformExpectations)
        {
            GameObject platform = RequireObject(name);
            BoxCollider2D collider = platform.GetComponent<BoxCollider2D>();
            if (collider == null)
            {
                throw new Exception($"{name} must be a platform collider.");
            }

            RequireNear(platform.transform.position, position, 0.12f, $"{name}.position");
            RequireNear(collider.size, size, 0.08f, $"{name}.size");

            if (platform.GetComponent<SpriteRenderer>() != null)
            {
                throw new Exception($"{name} must be hidden collision only, with no SpriteRenderer.");
            }
        }
    }

    private static void RequireV8PlatformVisuals()
    {
        foreach (var platform in PlatformExpectations)
        {
            string name = platform.Name;
            RequireObject(name + "_Visual_V8Road");
        }

        RequireSceneObjectNameContains("_V8_RoadModule_", 11);
        RequireSceneObjectNameContains("_V8_AmberWalkableLip_", 14);
        RequireSceneObjectNameContains("_V8_FrontShadow", 8);
        RequireSceneObjectNameContains("_V8_UnderTruss_Subtle", 8);
        RequireSceneObjectNameContains("_V8_EndCap_Left", 3);
        RequireSceneObjectNameContains("_V8_EndCap_Right", 2);
        RequireSceneObjectNameContains("_V10_AmberEdgeScuff", PlatformExpectations.Length);
        RequireSceneObjectNameContains("_V10_MetalSeamOverlay", 8);
        RequireSceneObjectNameContains("_V10_CrackOverlay", 3);
        RequireSceneObjectNameContains("_V10_LowObstacleShell", 1);
        RequireSceneObjectNameContains("_V10_ThinSupportShadow", 4);
        RequireSpritePathPrefix("Floor_AwakeningBench_V8_RoadModule_00", EnvironmentV8Path);
        RequireSpritePathPrefix("Floor_AwakeningBench_V8_AmberWalkableLip_00", EnvironmentV8Path);
        RequireSpritePathPrefix("Movement_LowObstacle_V10_LowObstacleShell", EnvironmentV10Path);
        ForbidObject("Movement_Step");
        ForbidObject("Movement_Step_Visual_V8Road");
        ForbidObject("Movement_Step_V8_BlockStepDeck");
        ForbidObject("Movement_Step_V10_BlockStepShadow");
        ForbidObject("Movement_Step_V10_RaisedStepShell");
        ForbidObject("Movement_Step_V10_RaisedStepUnderbeam");
        ForbidObject("Floor_MovementTutorial_B");
        ForbidObject("Floor_MovementTutorial_B_Visual_V8Road");
        ForbidObject("Floor_MovementTutorial_B_V8_BlockStepDeck");
        ForbidObject("Floor_MovementTutorial_B_V8_AmberWalkableLip_00");
        ForbidObject("Floor_MovementTutorial_B_V10_BlockStepShadow");
        RequireSpritePathPrefix("Jump_FirstGapPlatform_V8_ThinJumpDeck", EnvironmentV8Path);
        RequireSpritePathPrefix("Jump_FirstGapPlatform_V10_ThinSupportShadow", EnvironmentV10Path);
        RequireSpritePathPrefix("Jump_MidPlatform_V8_ThinFloatingDeck", EnvironmentV8Path);
        RequireSpritePathPrefix("Jump_OptionalRewardPlatform_V8_ThinFloatingDeck", EnvironmentV8Path);
        RequireSpritePathPrefix("Jump_RightPlatform_V8_ThinJumpDeck", EnvironmentV8Path);
        RequireSpritePathPrefix("Jump_MidPlatform_V10_ThinSupportShadow", EnvironmentV10Path);
        RequireSpritePathPrefix("Jump_OptionalRewardPlatform_V10_ThinSupportShadow", EnvironmentV10Path);
        RequireSpritePathPrefix("Jump_RightPlatform_V10_ThinSupportShadow", EnvironmentV10Path);
        ForbidObject("Jump_LeftPlatform");
        ForbidObject("Jump_LeftPlatform_Visual_V8Road");
        ForbidObject("Jump_LeftPlatform_V8_ThinJumpDeck");
        ForbidObject("Jump_LeftPlatform_V8_AmberWalkableLip_00");
        ForbidObject("Jump_LeftPlatform_V10_ThinSupportShadow");
        ForbidObject("Jump_MovingPlatform_01");
        ForbidObject("Jump_MovingPlatform_01_V8_MovingDeck");
        ForbidObject("Jump_MovingPlatform_01_V8_AmberWalkableLip_00");
        ForbidObject("Jump_MovingPlatform_01_V10_MovingDeckShadow");
        ForbidObject("Jump_MovingPlatform_01_V10_MovingUnderbeam");
        ForbidObject("Jump_MovingPlatform_01_PointA");
        ForbidObject("Jump_MovingPlatform_01_PointB");

        string[] compactPlatforms =
        {
            "Movement_LowObstacle",
            "Jump_FirstGapPlatform",
            "Jump_MidPlatform",
            "Jump_OptionalRewardPlatform",
            "Jump_RightPlatform",
            "Trap_InsulatedStep_A",
            "Trap_InsulatedStep_B",
        };

        foreach (string compactPlatform in compactPlatforms)
        {
            ForbidObject(compactPlatform + "_V8_FrontShadow");
            ForbidObject(compactPlatform + "_V8_UnderTruss_Subtle");
            ForbidObject(compactPlatform + "_V8_EndCap_Left");
            ForbidObject(compactPlatform + "_V8_EndCap_Right");
            ForbidObject(compactPlatform + "_V8_RoadModule_00");
            ForbidObject(compactPlatform + "_V10_BrokenJumpUnderbeam");
            ForbidObject(compactPlatform + "_V10_FloatingUnderbeam");
            ForbidObject(compactPlatform + "_V10_MovingUnderbeam");
            ForbidObject(compactPlatform + "_V10_ShortBridgeUnderbeam");
        }

        string[] jumpPlatforms =
        {
            "Jump_FirstGapPlatform",
            "Jump_MidPlatform",
            "Jump_OptionalRewardPlatform",
            "Jump_RightPlatform",
        };

        foreach (string jumpPlatform in jumpPlatforms)
        {
            ForbidObject(jumpPlatform + "_V8_RoadModule_00");
        }

        string[] sharedEdgeCaps =
        {
            "Floor_AwakeningBench_V8_EndCap_Right",
            "Floor_MovementTutorial_A_V8_EndCap_Left",
            "Floor_MovementTutorial_A_V8_EndCap_Right",
            "Floor_FirstEnemyArena_V8_EndCap_Right",
            "Floor_TrapEntry_V8_EndCap_Left",
            "Floor_TrapExit_V8_EndCap_Right",
            "Floor_ChargingSavePoint_V8_EndCap_Left",
            "Floor_ChargingSavePoint_V8_EndCap_Right",
            "Floor_BossRepairHall_V8_EndCap_Left",
            "Floor_BossRepairHall_V8_EndCap_Right",
            "Floor_MechCityGate_V8_EndCap_Left",
        };

        foreach (string capName in sharedEdgeCaps)
        {
            ForbidObject(capName);
        }
    }

    private static void RequireCleanDecorV8()
    {
        string[] requiredV8Objects =
        {
            "Awakening_V8_TopPipe_Soft",
            "Awakening_V8_ServicePanel_Dim",
            "Tutorial_V8_TopPipe_Soft",
            "Jump_V8_FarGear",
            "Trap_V8_TopPipe_Soft",
            "Boss_V8_HallPipe_Soft",
            "FarArt_V15_GearSilhouette_01",
        };

        foreach (string objectName in requiredV8Objects)
        {
            RequireSpritePathPrefix(objectName, EnvironmentV8Path);
        }

        RequireSceneObjectNameContains("_V8_", 45);
        RequireSceneObjectNameContains("WarningLamp", 4);
        RequireSceneObjectNameContains("HangingChain", 4);
        RequireSceneObjectNameContains("Pipe", 6);

        GameObject cleanRoot = RequireObject("BG_CleanDecor_V8");
        int lowForegroundRiskCount = 0;
        foreach (SpriteRenderer renderer in cleanRoot.GetComponentsInChildren<SpriteRenderer>(true))
        {
            if (renderer.GetComponentInParent<Collider2D>() != null)
            {
                throw new Exception($"{renderer.gameObject.name} is background decor and must not have a Collider2D.");
            }

            float worldY = renderer.transform.position.y;
            if (worldY > -2.4f && worldY < 0.8f && renderer.sortingOrder > -35)
            {
                lowForegroundRiskCount++;
            }
        }

        if (lowForegroundRiskCount > 0)
        {
            throw new Exception("BG_CleanDecor_V8 has bright/foreground decor in the player-height corridor.");
        }
    }

    private static void RequireDecorativePropsV9()
    {
        string[] requiredV9Objects =
        {
            "Awakening_V9_WallSupportBracket",
            "Tutorial_V9_ValvePipe_Back",
            "Trap_V9_BackFrameValvePipe",
            "Charge_V9_GreenChargePulse",
            "Boss_V9_VentFan_Rotating",
            "Exit_V9_DoorFramePipe",
        };

        foreach (string objectName in requiredV9Objects)
        {
            SpriteRenderer renderer = RequireSpriteRenderer(objectName);
            string spritePath = AssetDatabase.GetAssetPath(renderer.sprite);
            if (!spritePath.StartsWith(EnvironmentV9Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EffectsV3Path, StringComparison.Ordinal))
            {
                throw new Exception($"{objectName} should use V9/V3 background art. Current sprite: {spritePath}");
            }
        }

        RequireSceneObjectNameContains("_V9_", 24);
        RequireSceneObjectNameContains("ElectricArc", 2);
        RequireMinimum<SwayingDecor2D>(2);
        RequireMinimum<ElectricArcFlicker2D>(3);

        GameObject decorRoot = RequireObject("BG_DecorativeProps_V9");
        int visibleProps = 0;
        int roadCorridorRisks = 0;
        foreach (Transform child in decorRoot.GetComponentsInChildren<Transform>(true))
        {
            if (child.GetComponent<Collider2D>() != null)
            {
                throw new Exception($"{child.gameObject.name} is background decor and must not have a Collider2D.");
            }
        }

        foreach (SpriteRenderer renderer in decorRoot.GetComponentsInChildren<SpriteRenderer>(true))
        {
            string spritePath = AssetDatabase.GetAssetPath(renderer.sprite);
            if (!spritePath.StartsWith(EnvironmentV9Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EffectsV3Path, StringComparison.Ordinal))
            {
                throw new Exception($"{renderer.gameObject.name} under BG_DecorativeProps_V9 references unexpected sprite {spritePath}.");
            }

            visibleProps++;
            float worldY = renderer.transform.position.y;
            float alpha = renderer.color.a;
            if (worldY > -3.3f && worldY < -1.4f && (renderer.sortingOrder > -18 || alpha > 0.42f))
            {
                roadCorridorRisks++;
            }

            if (renderer.gameObject.name.Contains("AmberWalkable") || renderer.gameObject.name.Contains("RoadModule"))
            {
                throw new Exception($"{renderer.gameObject.name} uses road-language naming in background decor.");
            }
        }

        if (visibleProps < 24)
        {
            throw new Exception($"Expected at least 24 V9/V3 decorative renderers, found {visibleProps}.");
        }

        if (roadCorridorRisks > 0)
        {
            throw new Exception("BG_DecorativeProps_V9 has bright or foreground-sorted decor in the walkable road corridor.");
        }
    }

    private static void RequireLayeredDecorV10()
    {
        string[] requiredLayerRoots =
        {
            "BG_FarDecor_V10",
            "BG_MidDecor_V10",
            "BG_NearDecor_V10",
        };

        foreach (string rootName in requiredLayerRoots)
        {
            GameObject root = RequireObject(rootName);
            if (root.GetComponent<ParallaxLayer2D>() == null)
            {
                throw new Exception($"{rootName} must have ParallaxLayer2D for near/far separation.");
            }

            foreach (Transform child in root.GetComponentsInChildren<Transform>(true))
            {
                if (child.GetComponent<Collider2D>() != null)
                {
                    throw new Exception($"{child.gameObject.name} is layered background decor and must not have a Collider2D.");
                }
            }
        }

        RequireSpritePathPrefix("FarDecor_V10_SlowFan_00", EnvironmentV9Path);
        RequireSpritePathPrefix("FarDecor_V10_WarningBlink_00", EffectsV4Path);
        RequireSpritePathPrefix("MidDecor_V10_Arc_TrapBackwall", EffectsV4Path);
        RequireSpritePathPrefix("MidDecor_V10_Steam_BossPipeA", EffectsV4Path);
        RequireSpritePathPrefix("NearDecor_V10_DustVeil_Long", EffectsV4Path);

        RequireSceneObjectNameContains("FarDecor_V10_", 10);
        RequireSceneObjectNameContains("MidDecor_V10_", 9);
        RequireSceneObjectNameContains("NearDecor_V10_", 8);

        GameObject nearRoot = RequireObject("BG_NearDecor_V10");
        RequireSerializedVector2(nearRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", Vector2.zero, "BG_NearDecor_V10.parallaxFactor");
        RequireNoForegroundMotion(nearRoot);
        int nearRoadCorridorRisks = 0;
        foreach (SpriteRenderer renderer in nearRoot.GetComponentsInChildren<SpriteRenderer>(true))
        {
            float worldY = renderer.transform.position.y;
            if (worldY > -3.3f && worldY < -1.4f && renderer.sortingOrder > -18 && renderer.color.a > 0.18f)
            {
                nearRoadCorridorRisks++;
            }
        }

        if (nearRoadCorridorRisks > 0)
        {
            throw new Exception("BG_NearDecor_V10 has foreground decor inside the walkable road corridor.");
        }
    }

    private static void RequireLayeredDecorV11()
    {
        string[] requiredLayerRoots =
        {
            "BG_FarDecor_V11",
            "BG_MidDecor_V11",
            "BG_NearDecor_V11",
        };

        foreach (string rootName in requiredLayerRoots)
        {
            GameObject root = RequireObject(rootName);
            if (root.GetComponent<ParallaxLayer2D>() == null)
            {
                throw new Exception($"{rootName} must have ParallaxLayer2D for near/far separation.");
            }

            foreach (Transform child in root.GetComponentsInChildren<Transform>(true))
            {
                if (child.GetComponent<Collider2D>() != null)
                {
                    throw new Exception($"{child.gameObject.name} is V11 layered background decor and must not have a Collider2D.");
                }
            }
        }

        RequireSpritePathPrefix("FarDecor_V11_CrateStack_Awakening", EnvironmentV11Path);
        RequireSpritePathPrefix("FarDecor_V11_Conveyor_Trap", EnvironmentV11Path);
        RequireSpritePathPrefix("MidDecor_V11_Lamp_Trap", EnvironmentV11Path);
        RequireSpritePathPrefix("MidDecor_V11_ElectricSpark_TrapCable", EffectsV5Path);
        RequireSpritePathPrefix("NearDecor_V11_StreetLamp_TrapEntry", EnvironmentV11Path);
        RequireSpritePathPrefix("NearDecor_V11_LongFallingDust", EffectsV5Path);

        RequireSceneObjectNameContains("FarDecor_V11_", 10);
        RequireSceneObjectNameContains("MidDecor_V11_", 16);
        RequireSceneObjectNameContains("NearDecor_V11_", 12);
        RequireSceneObjectNameContains("NearDecor_V11_TopChain", 6);
        RequireSceneObjectNameContains("StreetLamp", 3);
        RequireSceneObjectNameContains("Conveyor", 5);

        GameObject nearRoot = RequireObject("BG_NearDecor_V11");
        RequireSerializedVector2(nearRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", Vector2.zero, "BG_NearDecor_V11.parallaxFactor");
        RequireNoForegroundMotion(nearRoot);
        RequireNear(RequireObject("NearDecor_V11_StreetLamp_TrapEntry").transform.position, new Vector2(84.6f, -1.48f), 0.05f, "trap street lamp road position");
        RequireNear(RequireObject("NearDecor_V11_StreetLamp_Charge").transform.position, new Vector2(109.7f, -1.48f), 0.05f, "charge street lamp road position");
        RequireNear(RequireObject("NearDecor_V11_StreetLamp_Exit").transform.position, new Vector2(164.3f, -1.48f), 0.05f, "exit street lamp road position");
        foreach (SpriteRenderer renderer in nearRoot.GetComponentsInChildren<SpriteRenderer>(true))
        {
            float worldY = renderer.transform.position.y;
            if (renderer.gameObject.name.IndexOf("StreetLamp", StringComparison.Ordinal) >= 0)
            {
                continue;
            }

            if (worldY > -3.3f && worldY < -1.4f && renderer.sortingOrder > -18 && renderer.color.a > 0.18f)
            {
                throw new Exception($"{renderer.gameObject.name} is V11 near decor inside the walkable road corridor.");
            }
        }
    }

    private static void RequireSingleOpeningLamp()
    {
        RequireSpritePathPrefix("MidDecor_V11_Lamp_Awakening", EnvironmentV11Path);
        RequireSpritePathPrefix("MidDecor_V11_Lamp_Awakening_Halo", EffectsV5Path);
        ForbidObject("Awakening_V8_HangingLamp");
        ForbidObject("Awakening_V9_LampHalo");
        ForbidObject("V13_MidLamp_AwakeningBench");
        ForbidObject("V13_MidLamp_AwakeningBench_Halo");
    }

    private static void RequireBackgroundAtmosphereV12()
    {
        GameObject root = RequireObject("BG_BackgroundAtmosphere_V12");
        GameObject farRoot = RequireObject("BG_FarAtmosphere_V12");
        GameObject midRoot = RequireObject("BG_MidAtmosphere_V12");

        RequireSerializedVector2(farRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", new Vector2(0.028f, 0.006f), "BG_FarAtmosphere_V12.parallaxFactor");
        RequireSerializedVector2(midRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", new Vector2(0.075f, 0.015f), "BG_MidAtmosphere_V12.parallaxFactor");

        foreach (Transform child in root.GetComponentsInChildren<Transform>(true))
        {
            if (child.GetComponent<Collider2D>() != null)
            {
                throw new Exception($"{child.gameObject.name} is atmosphere decor and must not have a Collider2D.");
            }
        }

        RequireSpritePathPrefix("V12_FarLightning_TrapWall", EffectsV5Path);
        RequireSpritePathPrefix("V12_MidLightning_ChargeCable_Left", EffectsV5Path);
        RequireSpritePathPrefix("V12_FlickerLight_ChargeSide", EffectsV5Path);
        RequireSpritePathPrefix("V12_MidDust_Trap", EffectsV5Path);
        RequireSceneObjectNameContains("V12_FarLightning", 3);
        RequireSceneObjectNameContains("V12_MidLightning", 3);
        RequireSceneObjectNameContains("V12_FlickerLight", 3);
        RequireSceneObjectNameContains("V12_MidDust", 3);
        RequireSceneObjectNameContains("V12_FarDust", 3);
    }

    private static void RequireProvidedLayeredDecorV2()
    {
        GameObject root = RequireObject("BG_ProvidedDecor_V2");
        GameObject farRoot = RequireObject("BG_ProvidedFarDecor_V2");
        GameObject midRoot = RequireObject("BG_ProvidedMidDecor_V2");
        GameObject nearRoot = RequireObject("BG_ProvidedNearDecor_V2");

        RequireSerializedVector2(farRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", new Vector2(0.03f, 0.006f), "BG_ProvidedFarDecor_V2.parallaxFactor");
        RequireSerializedVector2(midRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", new Vector2(0.07f, 0.014f), "BG_ProvidedMidDecor_V2.parallaxFactor");
        RequireSerializedVector2(nearRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", Vector2.zero, "BG_ProvidedNearDecor_V2.parallaxFactor");
        RequireNoForegroundMotion(nearRoot);

        RequireSpritePathPrefix("ProvidedV2_FarCrate_Awakening", ProvidedEnvironmentV2Path);
        RequireSpritePathPrefix("ProvidedV2_MidPipe_Trap", ProvidedEnvironmentV2Path);
        RequireSpritePathPrefix("ProvidedV2_MidHangingLamp_BossHall", ProvidedEnvironmentV2Path);
        RequireSpritePathPrefix("ProvidedV2_NearTopChain_Boss", ProvidedEnvironmentV2Path);
        RequireSceneObjectNameContains("ProvidedV2_", 13);
        RequireSceneObjectNameContains("ProvidedV2_MidPipe", 2);
        RequireSceneObjectNameContains("ProvidedV2_NearTopChain", 2);

        foreach (Transform child in root.GetComponentsInChildren<Transform>(true))
        {
            if (child.GetComponent<Collider2D>() != null)
            {
                throw new Exception($"{child.gameObject.name} is provided background decor and must not have a Collider2D.");
            }
        }

        foreach (SpriteRenderer renderer in root.GetComponentsInChildren<SpriteRenderer>(true))
        {
            string spritePath = AssetDatabase.GetAssetPath(renderer.sprite);
            if (!spritePath.StartsWith(ProvidedEnvironmentV2Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EffectsV4Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EffectsV5Path, StringComparison.Ordinal))
            {
                throw new Exception($"{renderer.gameObject.name} under BG_ProvidedDecor_V2 references unexpected sprite {spritePath}.");
            }

            float worldY = renderer.transform.position.y;
            if (worldY > -3.3f && worldY < -1.4f && renderer.sortingOrder > -18 && renderer.color.a > 0.18f)
            {
                throw new Exception($"{renderer.gameObject.name} is provided decor inside the walkable road corridor.");
            }
        }
    }

    private static void RequireProvidedLayeredDecorV3()
    {
        GameObject root = RequireObject("BG_ProvidedDecor_V3");
        GameObject farRoot = RequireObject("BG_ProvidedFarDecor_V3");
        GameObject midRoot = RequireObject("BG_ProvidedMidDecor_V3");
        GameObject nearRoot = RequireObject("BG_ProvidedNearDecor_V3");

        RequireSerializedVector2(farRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", new Vector2(0.025f, 0.006f), "BG_ProvidedFarDecor_V3.parallaxFactor");
        RequireSerializedVector2(midRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", new Vector2(0.075f, 0.015f), "BG_ProvidedMidDecor_V3.parallaxFactor");
        RequireSerializedVector2(nearRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", Vector2.zero, "BG_ProvidedNearDecor_V3.parallaxFactor");

        RequireSpritePathPrefix("ProvidedV3_FarFence_Tutorial", ProvidedEnvironmentV3Path);
        RequireSpritePathPrefix("ProvidedV3_MidElectricBox_Tutorial", ProvidedEnvironmentV3Path);
        RequireSpritePathPrefix("ProvidedV3_MidFan_TrapRotating", ProvidedEnvironmentV3Path);
        RequireSpritePathPrefix("ProvidedV3_MidBurningBarrel_Exit", ProvidedEnvironmentV3Path);
        RequireSpritePathPrefix("ProvidedV3_NearTornFlag_HighBoss", ProvidedEnvironmentV3Path);
        RequireSceneObjectNameContains("ProvidedV3_", 18);
        RequireSceneObjectNameContains("ProvidedV3_Far", 6);
        RequireSceneObjectNameContains("ProvidedV3_Mid", 10);
        RequireSceneObjectNameContains("ProvidedV3_Near", 2);
        RequireSceneObjectNameContains("SeamBreakerV8_", 24);

        foreach (Transform child in root.GetComponentsInChildren<Transform>(true))
        {
            if (child.GetComponent<Collider2D>() != null)
            {
                throw new Exception($"{child.gameObject.name} is provided V3 background decor and must not have a Collider2D.");
            }
        }

        foreach (SpriteRenderer renderer in root.GetComponentsInChildren<SpriteRenderer>(true))
        {
            string spritePath = AssetDatabase.GetAssetPath(renderer.sprite);
            if (!spritePath.StartsWith(ProvidedEnvironmentV3Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EffectsV3Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EffectsV4Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EffectsV5Path, StringComparison.Ordinal))
            {
                throw new Exception($"{renderer.gameObject.name} under BG_ProvidedDecor_V3 references unexpected sprite {spritePath}.");
            }

            float worldY = renderer.transform.position.y;
            if (worldY > -3.3f && worldY < -1.4f && renderer.sortingOrder > -18 && renderer.color.a > 0.18f)
            {
                throw new Exception($"{renderer.gameObject.name} is provided V3 decor inside the walkable road corridor.");
            }
        }
    }

    private static void RequireDynamicDecorV13()
    {
        GameObject root = RequireObject("BG_DynamicDecor_V13");
        GameObject farRoot = RequireObject("BG_DynamicFarDecor_V13");
        GameObject midRoot = RequireObject("BG_DynamicMidDecor_V13");
        GameObject nearRoot = RequireObject("BG_DynamicNearDecor_V13");

        RequireSerializedVector2(farRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", new Vector2(0.032f, 0.007f), "BG_DynamicFarDecor_V13.parallaxFactor");
        RequireSerializedVector2(midRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", new Vector2(0.078f, 0.016f), "BG_DynamicMidDecor_V13.parallaxFactor");
        RequireSerializedVector2(nearRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", Vector2.zero, "BG_DynamicNearDecor_V13.parallaxFactor");

        RequireSpritePathPrefix("V13_FarRotator_TrapGear", EnvironmentV9Path);
        RequireSpritePathPrefix("V13_MidLightning_TrapWall", EffectsV5Path);
        RequireSpritePathPrefix("V13_MidConveyorPulse_Trap", EffectsV5Path);
        RequireSpritePathPrefix("V13_MidRotator_BossWallFan", ProvidedEnvironmentV3Path);
        RequireSpritePathPrefix("V13_NearSwayFlag_BossHigh", ProvidedEnvironmentV3Path);

        RequireSceneObjectNameContains("V13_Far", 15);
        RequireSceneObjectNameContains("V13_Mid", 22);
        RequireSceneObjectNameContains("V13_Near", 6);
        RequireSceneObjectNameContains("V13_MidLightning", 8);
        RequireSceneObjectNameContains("V13_FarRotator", 5);
        RequireSceneObjectNameContains("V13_FarDust", 5);
        RequireSceneObjectNameContains("V13_MidConveyorPulse", 3);
        RequireSceneObjectNameContains("V13_NearSwayChain", 5);
        ForbidSceneObjectPrefix("V13_MidLamp_AwakeningBench");
        ForbidSceneObjectPrefix("V13_NearStreetLamp");

        foreach (Transform child in root.GetComponentsInChildren<Transform>(true))
        {
            if (child.GetComponent<Collider2D>() != null)
            {
                throw new Exception($"{child.gameObject.name} is V13 dynamic background decor and must not have a Collider2D.");
            }
        }

        foreach (SpriteRenderer renderer in root.GetComponentsInChildren<SpriteRenderer>(true))
        {
            string spritePath = AssetDatabase.GetAssetPath(renderer.sprite);
            if (!spritePath.StartsWith(EnvironmentV9Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EnvironmentV11Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(ProvidedEnvironmentV3Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EffectsV3Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EffectsV4Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EffectsV5Path, StringComparison.Ordinal))
            {
                throw new Exception($"{renderer.gameObject.name} under BG_DynamicDecor_V13 references unexpected sprite {spritePath}.");
            }

            if (renderer.gameObject.name.Contains("AmberWalkable") ||
                renderer.gameObject.name.Contains("RoadModule") ||
                renderer.gameObject.name.Contains("Road_Amber"))
            {
                throw new Exception($"{renderer.gameObject.name} uses walkable-road visual language inside V13 background decor.");
            }

            float worldY = renderer.transform.position.y;
            if (worldY > -3.3f && worldY < -1.4f && renderer.color.a > 0.12f)
            {
                throw new Exception($"{renderer.gameObject.name} is V13 decor inside the walkable road corridor.");
            }
        }
    }

    private static void RequireLargeDecorV14()
    {
        GameObject root = RequireObject("BG_LargeDecor_V14");
        GameObject farRoot = RequireObject("BG_LargeFarDecor_V14");
        GameObject midRoot = RequireObject("BG_LargeMidDecor_V14");
        GameObject nearRoot = RequireObject("BG_LargeNearDecor_V14");

        RequireSerializedVector2(farRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", new Vector2(0.024f, 0.005f), "BG_LargeFarDecor_V14.parallaxFactor");
        RequireSerializedVector2(midRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", new Vector2(0.07f, 0.014f), "BG_LargeMidDecor_V14.parallaxFactor");
        RequireSerializedVector2(nearRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", Vector2.zero, "BG_LargeNearDecor_V14.parallaxFactor");

        RequireSpritePathPrefix("V14_FarLargeConveyor_Tutorial", ProvidedEnvironmentV4Path);
        RequireSpritePathPrefix("V14_FarLargeConveyor_AwakeningBack", ProvidedEnvironmentV4Path);
        RequireSpritePathPrefix("V14_FarLargeBoiler_EnemyBack", ProvidedEnvironmentV4Path);
        RequireSpritePathPrefix("V14_MidLargeWallFan_Awakening", ProvidedEnvironmentV4Path);
        RequireSpritePathPrefix("V14_MidLargeWallFan_Enemy", ProvidedEnvironmentV4Path);
        RequireSpritePathPrefix("V14_MidLargeBoiler_Trap", ProvidedEnvironmentV4Path);
        RequireSpritePathPrefix("V14_MidLargeMachineFrame_Charge", ProvidedEnvironmentV4Path);
        RequireSpritePathPrefix("V14_NearOverheadCraneRail_Awakening", ProvidedEnvironmentV4Path);
        RequireSpritePathPrefix("V14_NearCraneHook_Awakening", ProvidedEnvironmentV4Path);
        RequireSpritePathPrefix("V14_NearOverheadCraneRail_Boss", ProvidedEnvironmentV4Path);
        RequireSpritePathPrefix("V14_NearCraneHook_Boss", ProvidedEnvironmentV4Path);
        RequireSpritePathPrefix("V14_MidRotator_AwakeningWallFanBlade", EnvironmentV9Path);
        RequireSpritePathPrefix("V14_MidRotator_EnemyWallFanBlade", EnvironmentV9Path);
        RequireSpritePathPrefix("V14_MidLightning_TutorialMachineFeed", EffectsV5Path);
        RequireSpritePathPrefix("V14_MidSteam_TrapBoiler_A", EffectsV4Path);

        RequireSceneObjectNameContains("V14_FarLarge", 7);
        RequireSceneObjectNameContains("V14_MidLarge", 7);
        RequireSceneObjectNameContains("V14_Near", 8);
        RequireSceneObjectNameContains("V14_MidLightning", 3);
        RequireSceneObjectNameContains("V14_MidRotator", 4);
        RequireSceneObjectNameContains("V14_MidSteam", 3);
        RequireSceneObjectNameContains("V14_NearCraneHook", 2);
        RequireSceneObjectNameContains("V14_FarDust", 3);

        foreach (Transform child in root.GetComponentsInChildren<Transform>(true))
        {
            if (child.GetComponent<Collider2D>() != null)
            {
                throw new Exception($"{child.gameObject.name} is V14 large background decor and must not have a Collider2D.");
            }
        }

        foreach (SpriteRenderer renderer in root.GetComponentsInChildren<SpriteRenderer>(true))
        {
            string spritePath = AssetDatabase.GetAssetPath(renderer.sprite);
            if (spritePath.EndsWith("/large_power_cable_bridge_v4.png", StringComparison.Ordinal))
            {
                throw new Exception($"{renderer.gameObject.name} still references the removed first-photo V4 decor.");
            }

            if (!spritePath.StartsWith(ProvidedEnvironmentV4Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EnvironmentV9Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EffectsV4Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EffectsV5Path, StringComparison.Ordinal))
            {
                throw new Exception($"{renderer.gameObject.name} under BG_LargeDecor_V14 references unexpected sprite {spritePath}.");
            }

            if (renderer.gameObject.name.Contains("AmberWalkable") ||
                renderer.gameObject.name.Contains("RoadModule") ||
                renderer.gameObject.name.Contains("Road_Amber"))
            {
                throw new Exception($"{renderer.gameObject.name} uses walkable-road visual language inside V14 large background decor.");
            }

            float worldY = renderer.transform.position.y;
            if (renderer.transform.IsChildOf(nearRoot.transform) &&
                worldY > -3.3f &&
                worldY < -1.4f &&
                renderer.color.a > 0.12f)
            {
                throw new Exception($"{renderer.gameObject.name} is V14 near decor inside the walkable road corridor.");
            }
        }
    }

    private static void RequireMapReadableDecorV15()
    {
        GameObject root = RequireObject("BG_MapReadableDecor_V15");
        GameObject farRoot = RequireObject("BG_MapReadableFarDecor_V15");
        GameObject midRoot = RequireObject("BG_MapReadableMidDecor_V15");
        GameObject nearRoot = RequireObject("BG_MapReadableNearDecor_V15");

        RequireSerializedVector2(farRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", new Vector2(0.018f, 0.004f), "BG_MapReadableFarDecor_V15.parallaxFactor");
        RequireSerializedVector2(midRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", new Vector2(0.06f, 0.012f), "BG_MapReadableMidDecor_V15.parallaxFactor");
        RequireSerializedVector2(nearRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", Vector2.zero, "BG_MapReadableNearDecor_V15.parallaxFactor");

        RequireSpritePathPrefix("V15_SeamTruss_01", EnvironmentV7Path);
        RequireSpritePathPrefix("V15_FarMachineWall_Awakening", ProvidedEnvironmentV4Path);
        RequireSpritePathPrefix("V15_FarConveyor_TrapVista", ProvidedEnvironmentV4Path);
        RequireSpritePathPrefix("V15_FarGearWall_BossVista", ProvidedEnvironmentV4Path);
        RequireSpritePathPrefix("V15_MidRotator_TutorialWallFan", EnvironmentV9Path);
        RequireSpritePathPrefix("V15_MidLightning_TrapCable", EffectsV5Path);
        RequireSpritePathPrefix("V15_MidSteam_TrapMachine", EffectsV4Path);
        RequireSpritePathPrefix("V15_NearOverheadRail_Boss", ProvidedEnvironmentV4Path);
        RequireSpritePathPrefix("V15_NearSwayHook_BossHigh", ProvidedEnvironmentV4Path);

        RequireSceneObjectNameContains("V15_SeamTruss", 4);
        RequireSceneObjectNameContains("V15_SeamDustVeil", 4);
        RequireSceneObjectNameContains("V15_Far", 8);
        RequireSceneObjectNameContains("V15_MidRotator", 3);
        RequireSceneObjectNameContains("V15_MidLightning", 4);
        RequireSceneObjectNameContains("V15_MidSteam", 3);
        RequireSceneObjectNameContains("V15_MidConveyorPulse", 3);
        RequireSceneObjectNameContains("V15_NearOverheadRail", 3);
        RequireSceneObjectNameContains("V15_NearSwayHook", 3);

        foreach (Transform child in root.GetComponentsInChildren<Transform>(true))
        {
            if (child.GetComponent<Collider2D>() != null)
            {
                throw new Exception($"{child.gameObject.name} is V15 map-readable decor and must not have a Collider2D.");
            }
        }

        foreach (SpriteRenderer renderer in root.GetComponentsInChildren<SpriteRenderer>(true))
        {
            string spritePath = AssetDatabase.GetAssetPath(renderer.sprite);
            if (!spritePath.StartsWith(EnvironmentV7Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EnvironmentV9Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(ProvidedEnvironmentV4Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EffectsV4Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EffectsV5Path, StringComparison.Ordinal))
            {
                throw new Exception($"{renderer.gameObject.name} under BG_MapReadableDecor_V15 references unexpected sprite {spritePath}.");
            }

            if (renderer.gameObject.name.Contains("AmberWalkable") ||
                renderer.gameObject.name.Contains("RoadModule") ||
                renderer.gameObject.name.Contains("Road_Amber"))
            {
                throw new Exception($"{renderer.gameObject.name} uses walkable-road visual language inside V15 background decor.");
            }

            float worldY = renderer.transform.position.y;
            if (worldY > -3.3f && worldY < -1.4f && renderer.color.a > 0.12f)
            {
                throw new Exception($"{renderer.gameObject.name} is V15 decor inside the walkable road corridor.");
            }
        }
    }

    private static void RequireSystemPolishReadability()
    {
        GameObject root = RequireObject("BG_SystemPolish_Readability");
        GameObject routeRoot = RequireObject("BG_SystemPolish_RouteFocus");
        GameObject landmarkRoot = RequireObject("BG_SystemPolish_Landmarks");
        GameObject hazardRoot = RequireObject("BG_SystemPolish_HazardAndInteract");
        GameObject atmosphereRoot = RequireObject("BG_SystemPolish_AtmosphereControl");

        RequireSerializedVector2(routeRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", Vector2.zero, "BG_SystemPolish_RouteFocus.parallaxFactor");
        RequireSerializedVector2(landmarkRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", new Vector2(0.02f, 0.004f), "BG_SystemPolish_Landmarks.parallaxFactor");
        RequireSerializedVector2(hazardRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", Vector2.zero, "BG_SystemPolish_HazardAndInteract.parallaxFactor");
        RequireSerializedVector2(atmosphereRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", new Vector2(0.04f, 0.01f), "BG_SystemPolish_AtmosphereControl.parallaxFactor");

        RequireSceneObjectNameContains("SystemPolish_RouteFocus_", 8);
        RequireSceneObjectNameContains("SystemPolish_Landmark_", 7);
        RequireSceneObjectNameContains("SystemPolish_Hazard_", 4);
        RequireSceneObjectNameContains("SystemPolish_Interact_", 3);
        RequireSceneObjectNameContains("SystemPolish_Atmosphere_", 6);
        RequireSpritePathPrefix("SystemPolish_RouteFocus_Boss", EffectsV4Path);
        RequireSpritePathPrefix("SystemPolish_Landmark_ChargeStationGreen", EffectsV5Path);
        RequireSpritePathPrefix("SystemPolish_Hazard_ElectricFloorA", EffectsV5Path);
        RequireSpritePathPrefix("SystemPolish_Interact_PowerSwitchCyan", EffectsV5Path);
        RequireSpritePathPrefix("SystemPolish_Atmosphere_BossSteamLeft", EffectsV4Path);
        RequireSpritePathPrefix("SystemPolish_Atmosphere_BossCeilingArc", EffectsV5Path);

        foreach (Transform child in root.GetComponentsInChildren<Transform>(true))
        {
            if (child.GetComponent<Collider2D>() != null)
            {
                throw new Exception($"{child.gameObject.name} is system polish decor and must not have a Collider2D.");
            }

            SpriteRenderer renderer = child.GetComponent<SpriteRenderer>();
            if (renderer != null && renderer.sortingOrder > 0)
            {
                throw new Exception($"{child.gameObject.name} must stay behind gameplay readability layers.");
            }
        }
    }

    private static void RequireDynamicPolishV16()
    {
        GameObject root = RequireObject("BG_DynamicPolish_V16");
        GameObject farRoot = RequireObject("BG_DynamicPolish_V16_Far");
        GameObject midRoot = RequireObject("BG_DynamicPolish_V16_Mid");
        GameObject nearRoot = RequireObject("BG_DynamicPolish_V16_NearSafe");
        GameObject readableRoot = RequireObject("BG_DynamicPolish_V16_GameplayReadable");

        RequireSerializedVector2(farRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", new Vector2(0.026f, 0.006f), "BG_DynamicPolish_V16_Far.parallaxFactor");
        RequireSerializedVector2(midRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", new Vector2(0.062f, 0.012f), "BG_DynamicPolish_V16_Mid.parallaxFactor");
        RequireSerializedVector2(nearRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", Vector2.zero, "BG_DynamicPolish_V16_NearSafe.parallaxFactor");
        RequireSerializedVector2(readableRoot.GetComponent<ParallaxLayer2D>(), "parallaxFactor", Vector2.zero, "BG_DynamicPolish_V16_GameplayReadable.parallaxFactor");

        RequireSceneObjectNameContains("V16_FarDust_", 5);
        RequireSceneObjectNameContains("V16_FarConveyorPulse_", 3);
        RequireSceneObjectNameContains("V16_FarRotator_", 2);
        RequireSceneObjectNameContains("V16_MidPulse_", 5);
        RequireSceneObjectNameContains("V16_MidElectric_", 5);
        RequireSceneObjectNameContains("V16_MidSteam_", 4);
        RequireSceneObjectNameContains("V16_MidScan_", 2);
        RequireSceneObjectNameContains("V16_NearSwayChain_", 4);
        RequireSceneObjectNameContains("V16_Readable", 4);

        RequireSpritePathPrefix("V16_FarDust_AwakeningWarm", EffectsV5Path);
        RequireSpritePathPrefix("V16_FarConveyorPulse_Move", EffectsV5Path);
        RequireSpritePathPrefix("V16_FarRotator_BossGearSlow", EnvironmentV9Path);
        RequireSpritePathPrefix("V16_MidPulse_ChargeStationCyan", EffectsV5Path);
        RequireSpritePathPrefix("V16_MidElectric_TrapBlueArcA", EffectsV5Path);
        RequireSpritePathPrefix("V16_MidScan_ChargeStation", EffectsV2Path);
        RequireSpritePathPrefix("V16_NearSwayChain_BossHigh", EnvironmentV11Path);
        RequireSpritePathPrefix("V16_NearSwayFlag_BossHigh", ProvidedEnvironmentV3Path);
        RequireSpritePathPrefix("V16_ReadableOilMist_TrapBelow", EffectsV5Path);

        foreach (Transform child in root.GetComponentsInChildren<Transform>(true))
        {
            if (child.GetComponent<Collider2D>() != null)
            {
                throw new Exception($"{child.gameObject.name} is V16 dynamic polish decor and must not have a Collider2D.");
            }
        }

        foreach (SpriteRenderer renderer in root.GetComponentsInChildren<SpriteRenderer>(true))
        {
            string spritePath = AssetDatabase.GetAssetPath(renderer.sprite);
            if (!spritePath.StartsWith(EnvironmentV9Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EnvironmentV11Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(ProvidedEnvironmentV3Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EffectsV2Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EffectsV4Path, StringComparison.Ordinal) &&
                !spritePath.StartsWith(EffectsV5Path, StringComparison.Ordinal))
            {
                throw new Exception($"{renderer.gameObject.name} under BG_DynamicPolish_V16 references unexpected sprite {spritePath}.");
            }

            if (renderer.gameObject.name.Contains("AmberWalkable") ||
                renderer.gameObject.name.Contains("RoadModule") ||
                renderer.gameObject.name.Contains("Road_Amber"))
            {
                throw new Exception($"{renderer.gameObject.name} uses walkable-road visual language inside V16 dynamic polish.");
            }

            bool isReadableLowEffect = renderer.transform.IsChildOf(readableRoot.transform);
            float worldY = renderer.transform.position.y;
            if (!isReadableLowEffect && worldY > -3.3f && worldY < -1.35f && renderer.color.a > 0.12f)
            {
                throw new Exception($"{renderer.gameObject.name} is V16 decor inside the walkable road corridor.");
            }

            if (renderer.transform.IsChildOf(nearRoot.transform) && worldY < 2.65f && renderer.color.a > 0.12f)
            {
                throw new Exception($"{renderer.gameObject.name} is V16 near-safe decor below the high overhead band.");
            }

            if (isReadableLowEffect && renderer.sortingOrder > 0)
            {
                throw new Exception($"{renderer.gameObject.name} must stay behind gameplay while in V16 readability polish.");
            }
        }
    }

    private static void RequireGameplayObjects()
    {
        RequireComponent<DestructibleCrate2D>("DestructibleCrate_AttackTutorial");
        RequireSpritePathPrefix("DestructibleCrate_AttackTutorial_Visual", EnvironmentV7Path);
        GameObject attackCrate = RequireObject("DestructibleCrate_AttackTutorial");
        RequireNear(attackCrate.transform.position, new Vector2(28.2f, -1.025f), 0.05f, "crate position");
        BoxCollider2D attackCrateCollider = attackCrate.GetComponent<BoxCollider2D>();
        if (attackCrateCollider == null || attackCrateCollider.isTrigger)
        {
            throw new Exception("DestructibleCrate_AttackTutorial must keep a solid BoxCollider2D.");
        }

        RequireNear(attackCrateCollider.size, new Vector2(1.2f, 3.15f), 0.02f, "attack tutorial crate size");

        RequireComponent<EnemyPatrol2D>("Enemy_FirstRepairPatrol");
        RequireComponent<Health>("Enemy_FirstRepairPatrol");
        RequireComponent<EnemyVisualAnimator2D>("Enemy_FirstRepairPatrol");
        RequireSpritePathPrefix("Enemy_FirstRepairPatrol_Visual", EnemyV2Path);
        RequireNear(RequireObject("Enemy_FirstRepairPatrol").transform.position, new Vector2(76f, -2.18f), 0.2f, "first enemy position");

        RequireComponent<EnemyPatrol2D>("Enemy_TrapGuardRepairPatrol");
        RequireComponent<Health>("Enemy_TrapGuardRepairPatrol");
        RequireComponent<EnemyVisualAnimator2D>("Enemy_TrapGuardRepairPatrol");
        RequireSpritePathPrefix("Enemy_TrapGuardRepairPatrol_Visual", EnemyV2Path);
        RequireSerializedFloat<EnemyPatrol2D>("Enemy_TrapGuardRepairPatrol", "moveSpeed", 1.25f);
        RequireNear(RequireObject("Enemy_TrapGuardRepairPatrol").transform.position, new Vector2(89.1f, -2.18f), 0.2f, "trap guard enemy position");

        ForbidObject("Trap_ElectricFloor_01");
        ForbidObject("Trap_InsulatedStep");
        RequireTimedElectricFloor("Trap_ElectricFloor_A", new Vector2(95.7f, -2.08f), new Vector2(5.8f, 0.56f), 0f);
        RequireTimedElectricFloor("Trap_ElectricFloor_B", new Vector2(102.05f, -2.08f), new Vector2(5.8f, 0.56f), 1.1f);
        RequirePowerSwitch("PowerSwitch_TrapBreaker", new Vector2(92.55f, -1.78f), new[] { "Trap_ElectricFloor_A", "Trap_ElectricFloor_B" });

        RequireInsulatedStep("Trap_InsulatedStep_A", new Vector2(98.85f, -2.08f), new Vector2(1.1f, 0.32f));
        RequireInsulatedStep("Trap_InsulatedStep_B", new Vector2(105.0f, -2.08f), new Vector2(1.1f, 0.32f));

        RequireComponent<HazardRespawn2D>("FallDeathZone_Global");
        BoxCollider2D fallZoneCollider = RequireObject("FallDeathZone_Global").GetComponent<BoxCollider2D>();
        if (fallZoneCollider == null || !fallZoneCollider.isTrigger)
        {
            throw new Exception("FallDeathZone_Global must be a trigger collider.");
        }
        RequireNear(RequireObject("FallDeathZone_Global").transform.position, new Vector2(88f, -6.1f), 0.1f, "global fall death zone position");
        RequireNear(fallZoneCollider.size, new Vector2(188f, 1.0f), 0.1f, "global fall death zone size");

        RequireCompressorTrap("Trap_Compressor_01", new Vector2(106.2f, -1.82f), 0f);
        RequireCompressorTrap("Trap_Compressor_02", new Vector2(111.0f, -1.82f), 1.35f);

        RequireComponent<ChargingStation2D>("ChargingStation_Temporary");
        RequireSpritePathPrefix("ChargingStation_Temporary_Visual", EnvironmentV7Path);
        RequireComponentPolishRoot("ChargingStation_Temporary_PolishRefined");
        RequireSpritePathPrefix("ChargingStation_Temporary_PolishRefined_StatusCore", EnvironmentV19Path);
        RequireSpritePathPrefix("ChargingStation_Temporary_PolishRefined_SafeHalo", EffectsV5Path);
        RequireSpritePathPrefix("ChargingStation_Temporary_PolishRefined_ChargeScan", EffectsV2Path);
        RequireSerializedObjectReference<ChargingStation2D>("ChargingStation_Temporary", "respawnPoint");
        RequireSerializedObjectReference<ChargingStation2D>("ChargingStation_Temporary", "chargeLight");
        RequireSerializedObjectReference<ChargingStation2D>("ChargingStation_Temporary", "statusCore");
        RequireSerializedObjectReference<ChargingStation2D>("ChargingStation_Temporary", "safeGlow");
        RequireNear(RequireObject("ChargingStation_Temporary").transform.position, new Vector2(120f, -1.78f), 0.2f, "charging station position");

        RequireComponent<SupplyCrate2D>("SupplyCrate_OptionalJumpCache");
        RequireSpritePathPrefix("SupplyCrate_OptionalJumpCache_Visual", EnvironmentV7Path);
        RequireComponentPolishRoot("SupplyCrate_OptionalJumpCache_PolishRefined");
        RequireSpritePathPrefix("SupplyCrate_OptionalJumpCache_PolishRefined_StatusCore", EnvironmentV19Path);
        RequireSpritePathPrefix("SupplyCrate_OptionalJumpCache_PolishRefined_SafeHalo", EffectsV5Path);
        RequireSerializedObjectReference<SupplyCrate2D>("SupplyCrate_OptionalJumpCache", "visual");
        RequireSerializedObjectReference<SupplyCrate2D>("SupplyCrate_OptionalJumpCache", "statusCore");
        RequireSerializedObjectReference<SupplyCrate2D>("SupplyCrate_OptionalJumpCache", "softGlow");
        RequireNear(RequireObject("SupplyCrate_OptionalJumpCache").transform.position, new Vector2(52.2f, 1.68f), 0.2f, "optional jump cache position");

        RequireComponent<LoreTerminal>("LoreTerminal_RebootLog");
        RequireSpritePathPrefix("LoreTerminal_RebootLog_TerminalVisual", EnvironmentV7Path);
        RequireSpritePathPrefix("LoreTerminal_RebootLog_ScreenGlow", EffectsV2Path);
        RequireComponentPolishRoot("LoreTerminal_RebootLog_PolishRefined");
        RequireSpritePathPrefix("LoreTerminal_RebootLog_PolishRefined_TerminalScreenOverlay", EnvironmentV19Path);
        RequireSpritePathPrefix("LoreTerminal_RebootLog_PolishRefined_ReadScan", EffectsV2Path);
        RequireSpritePathPrefix("LoreTerminal_RebootLog_PolishRefined_ReadFlash", EffectsV5Path);
        RequireSerializedArrayMinimum<LoreTerminal>("LoreTerminal_RebootLog", "idleGlowRenderers", 1);
        RequireSerializedArrayMinimum<LoreTerminal>("LoreTerminal_RebootLog", "readPulseRenderers", 1);

        RequireComponent<RepairStationBoss2D>("Boss_RepairStationGuardian");
        RequireComponent<Health>("Boss_RepairStationGuardian");
        RequireSerializedInt<Health>("Boss_RepairStationGuardian", "maxHealth", 14);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_BodyVisual", EnemyV2Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_EyeLight_Red", EnemyV2Path);
        GameObject bossV3Assembly = RequireObject("Boss_RepairStationGuardian_V3RefinedAssembly");
        if (bossV3Assembly.GetComponentsInChildren<Collider2D>(true).Length > 0)
        {
            throw new Exception("Boss V3 refined assembly must not add Collider2D components.");
        }
        GameObject bossV4Assembly = RequireObject("Boss_RepairStationGuardian_V4OverloadAssembly");
        if (bossV4Assembly.GetComponentsInChildren<Collider2D>(true).Length > 0)
        {
            throw new Exception("Boss V4 overload assembly must not add Collider2D components.");
        }

        RequireSpritePathPrefix("Boss_RepairStationGuardian_V3RefinedOverlay", EnemyV3Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_V4OverloadOverlay", EnemyV4Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_CoreLight_Red", EffectsV2Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_CrackGlow_Overload", EnvironmentV10Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_DeathCoreFlash", EffectsV2Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_DeathDustVeil", EffectsV5Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_DeathSparkBurst", EffectsV2Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_DeathFragment_A", EnvironmentV10Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_DeathFragment_C", EnvironmentV7Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_SweepArmVisual", EnvironmentV7Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_SmashWarning_Dust", EffectsV2Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_SweepTrail_ElectricDrag", EffectsV5Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_SmashDustRing", EffectsV2Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_ShockwaveVisual", EffectsV5Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_ShockwaveSparkTrail", EffectsV2Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_ArcBurstWarning", EffectsV5Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_ArcBurstVisual", EffectsV2Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_CoreBeamWarningScan", EffectsV2Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_CoreBeamVisual", EffectsV2Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_CeilingSparkWarning_A", EffectsV2Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_CeilingSparkVisual_A", EffectsV5Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_FinalPulseWarning", EffectsV5Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_FinalPulseLeftVisual", EffectsV2Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_FinalPulseRightVisual", EffectsV2Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_HitSpark", EffectsV2Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_PhaseSteamBoost", EffectsV2Path);
        RequireSpritePathPrefix("Boss_RepairStationGuardian_DeathSmoke", EffectsV2Path);
        RequireNear(RequireObject("Boss_RepairStationGuardian").transform.position, new Vector2(143.5f, -1.75f), 0.2f, "boss position");
        RequireComponent<BossDamageHitbox2D>("Boss_RepairStationGuardian_SweepHitbox");
        RequireComponent<BossDamageHitbox2D>("Boss_RepairStationGuardian_SmashHitbox");
        RequireComponent<BossDamageHitbox2D>("Boss_RepairStationGuardian_ShockwaveHitbox");
        RequireComponent<BossDamageHitbox2D>("Boss_RepairStationGuardian_ArcBurstHitbox");
        RequireComponent<BossDamageHitbox2D>("Boss_RepairStationGuardian_CoreBeamHitbox");
        RequireComponent<BossDamageHitbox2D>("Boss_RepairStationGuardian_CeilingSparkHitbox_A");
        RequireComponent<BossDamageHitbox2D>("Boss_RepairStationGuardian_CeilingSparkHitbox_B");
        RequireComponent<BossDamageHitbox2D>("Boss_RepairStationGuardian_CeilingSparkHitbox_C");
        RequireComponent<BossDamageHitbox2D>("Boss_RepairStationGuardian_FinalPulseHitbox_Left");
        RequireComponent<BossDamageHitbox2D>("Boss_RepairStationGuardian_FinalPulseHitbox_Right");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "bodyRenderer");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "refinedOverlay");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "overloadOverlay");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "coreLight");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "crackGlow");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "shockwaveVisual");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "sweepTrailVisual");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "smashDustRingVisual");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "shockwaveSparkTrail");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "arcBurstWarning");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "arcBurstVisual");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "coreBeamWarning");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "coreBeamVisual");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "finalPulseWarning");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "finalPulseLeftVisual");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "finalPulseRightVisual");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "hitSparkVisual");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "phaseSteamBoost");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "deathCoreFlash");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "deathDustVeil");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "deathSparkBurst");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "shockwaveHitbox");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "arcBurstHitbox");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "coreBeamHitbox");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "finalPulseLeftHitbox");
        RequireSerializedObjectReference<RepairStationBoss2D>("Boss_RepairStationGuardian", "finalPulseRightHitbox");
        RequireSerializedArrayMinimum<RepairStationBoss2D>("Boss_RepairStationGuardian", "ceilingSparkWarnings", 3);
        RequireSerializedArrayMinimum<RepairStationBoss2D>("Boss_RepairStationGuardian", "ceilingSparkVisuals", 3);
        RequireSerializedArrayMinimum<RepairStationBoss2D>("Boss_RepairStationGuardian", "ceilingSparkHitboxes", 3);
        RequireSerializedArrayMinimum<RepairStationBoss2D>("Boss_RepairStationGuardian", "deathFragments", 5);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "windupSeconds", 0.82f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "introSeconds", 0.58f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "actionSeconds", 0.76f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "recoverSeconds", 0.55f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "sweepRecoverSeconds", 0.55f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "smashRecoverSeconds", 0.75f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "shockwaveRecoverSeconds", 0.85f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "shockwaveWindupSeconds", 0.72f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "shockwaveSeconds", 0.62f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "arcBurstWindupSeconds", 0.9f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "arcBurstSeconds", 0.58f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "arcBurstRecoverSeconds", 0.78f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "coreBeamWindupSeconds", 0.75f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "coreBeamSeconds", 0.52f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "coreBeamRecoverSeconds", 0.86f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "ceilingSparkWindupSeconds", 0.82f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "ceilingSparkSeconds", 0.68f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "ceilingSparkRecoverSeconds", 0.88f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "finalCorePulseWindupSeconds", 0.9f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "finalCorePulseSeconds", 0.72f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "finalCorePulseRecoverSeconds", 0.95f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "comboWindupSeconds", 0.72f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "sweepShockComboSeconds", 1.48f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "smashArcComboSeconds", 1.56f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "finalComboRecoverSeconds", 0.95f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "phaseTwoHealthRatio", 0.5f);
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "deathShowSeconds", 1.8f);
        RequireSerializedInt<RepairStationBoss2D>("Boss_RepairStationGuardian", "lowHealthSummonThreshold", 7);
        RequireSerializedInt<RepairStationBoss2D>("Boss_RepairStationGuardian", "finalPhaseHealthThreshold", 4);
        RepairStationBoss2D bossComponent = RequireObject("Boss_RepairStationGuardian").GetComponent<RepairStationBoss2D>();
        RequireSerializedVector2(bossComponent, "sweepHitboxOffset", new Vector2(-2.15f, -0.28f), "Boss_RepairStationGuardian.sweepHitboxOffset");
        RequireSerializedVector2(bossComponent, "smashHitboxOffset", new Vector2(-1.05f, -1.25f), "Boss_RepairStationGuardian.smashHitboxOffset");
        RequireSerializedVector2(bossComponent, "shockwaveHitboxStartOffset", new Vector2(-2.15f, -1.25f), "Boss_RepairStationGuardian.shockwaveHitboxStartOffset");
        RequireSerializedVector2(bossComponent, "shockwaveHitboxEndOffset", new Vector2(-4.3f, -1.25f), "Boss_RepairStationGuardian.shockwaveHitboxEndOffset");
        RequireSerializedVector2(bossComponent, "arcBurstLocalXRange", new Vector2(-7.2f, 7.2f), "Boss_RepairStationGuardian.arcBurstLocalXRange");
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "arcBurstGroundY", -1.36f);
        RequireSerializedVector2(bossComponent, "coreBeamHitboxOffset", new Vector2(-3.35f, -0.68f), "Boss_RepairStationGuardian.coreBeamHitboxOffset");
        RequireSerializedVector2(bossComponent, "coreBeamVisualOffset", new Vector2(-3.35f, -0.62f), "Boss_RepairStationGuardian.coreBeamVisualOffset");
        RequireSerializedVector2(bossComponent, "ceilingSparkLocalXRange", new Vector2(-6.4f, 6.4f), "Boss_RepairStationGuardian.ceilingSparkLocalXRange");
        RequireSerializedFloat<RepairStationBoss2D>("Boss_RepairStationGuardian", "ceilingSparkGroundY", -0.95f);
        RequireSerializedVector2(bossComponent, "finalPulseLeftHitboxOffset", new Vector2(-3.1f, -1.18f), "Boss_RepairStationGuardian.finalPulseLeftHitboxOffset");
        RequireSerializedVector2(bossComponent, "finalPulseRightHitboxOffset", new Vector2(3.1f, -1.18f), "Boss_RepairStationGuardian.finalPulseRightHitboxOffset");

        RequireComponent<BossEncounterController2D>("BossEncounter_StartTrigger");
        RequireNear(RequireObject("BossEncounter_StartTrigger").transform.position, new Vector2(129.35f, -1.35f), 0.1f, "boss encounter trigger position");
        BoxCollider2D bossTrigger = RequireObject("BossEncounter_StartTrigger").GetComponent<BoxCollider2D>();
        if (bossTrigger == null || !bossTrigger.isTrigger)
        {
            throw new Exception("BossEncounter_StartTrigger must be a trigger collider.");
        }

        RequireNear(bossTrigger.size, new Vector2(3.0f, 3.2f), 0.05f, "boss encounter trigger size");
        BossEncounterController2D encounter = RequireObject("BossEncounter_StartTrigger").GetComponent<BossEncounterController2D>();
        RequireSerializedObjectReference<BossEncounterController2D>("BossEncounter_StartTrigger", "boss");
        RequireSerializedObjectReference<BossEncounterController2D>("BossEncounter_StartTrigger", "bossHealth");
        RequireSerializedObjectReference<BossEncounterController2D>("BossEncounter_StartTrigger", "entryLockRoot");
        RequireSerializedObjectReference<BossEncounterController2D>("BossEncounter_StartTrigger", "entryLockCollider");
        RequireSerializedObjectReference<BossEncounterController2D>("BossEncounter_StartTrigger", "entryLockVisual");
        RequireSerializedObjectReference<BossEncounterController2D>("BossEncounter_StartTrigger", "entryLockLight");
        RequireSerializedObjectReference<BossEncounterController2D>("BossEncounter_StartTrigger", "entryLockEngagedHalo");
        RequireSerializedObjectReference<BossEncounterController2D>("BossEncounter_StartTrigger", "entryLockUnlockedGlow");
        RequireSerializedObjectReference<BossEncounterController2D>("BossEncounter_StartTrigger", "entryLockUnlockSpark");
        RequireSerializedObjectReference<BossEncounterController2D>("BossEncounter_StartTrigger", "entryLockRefinedOverlay");
        RequireSerializedObjectReference<BossEncounterController2D>("BossEncounter_StartTrigger", "entryLockPressureGlow");
        RequireSerializedObjectReference<BossEncounterController2D>("BossEncounter_StartTrigger", "entryLockSideArcLeft");
        RequireSerializedObjectReference<BossEncounterController2D>("BossEncounter_StartTrigger", "entryLockSideArcRight");
        RequireSerializedObjectReference<BossEncounterController2D>("BossEncounter_StartTrigger", "entryLockBottomSteam");
        RequireSerializedObjectReference<BossEncounterController2D>("BossEncounter_StartTrigger", "entryLockUnlockScan");
        RequireSerializedVector2(encounter, "cameraMinBounds", new Vector2(124f, -5.4f), "BossEncounter_StartTrigger.cameraMinBounds");
        RequireSerializedVector2(encounter, "cameraMaxBounds", new Vector2(162.2f, 6.8f), "BossEncounter_StartTrigger.cameraMaxBounds");

        GameObject entryLock = RequireObject("BossArena_EntryLock");
        RequireNear(entryLock.transform.position, new Vector2(127.0f, -0.95f), 0.1f, "boss arena entry lock position");
        BoxCollider2D lockCollider = entryLock.GetComponent<BoxCollider2D>();
        if (lockCollider == null || lockCollider.isTrigger)
        {
            throw new Exception("BossArena_EntryLock must have a solid BoxCollider2D.");
        }

        RequireNear(lockCollider.size, new Vector2(1.05f, 4.15f), 0.05f, "boss arena entry lock size");
        RequireSpritePathPrefix("BossDoor_RepairHall_Visual", EnvironmentV7Path);
        RequireSpritePathPrefix("BossDoor_RepairHall_V20_BackdropOverlay", EnvironmentV20Path);
        RequireSpritePathPrefix("BossArena_EntryLock_Slab", EnvironmentV7Path);
        RequireSpritePathPrefix("BossArena_EntryLock_V20_RefinedOverlay", EnvironmentV20Path);
        RequireSpritePathPrefix("BossArena_EntryLock_RedLamp", EnvironmentV7Path);
        RequireSpritePathPrefix("BossArena_EntryLock_RedHalo", EffectsV2Path);
        RequireComponentPolishRoot("BossArena_EntryLock_FXPolish");
        RequireSpritePathPrefix("BossArena_EntryLock_FXPolish_UnlockGlow", EffectsV5Path);
        RequireSpritePathPrefix("BossArena_EntryLock_FXPolish_UnlockSpark", EffectsV5Path);
        RequireSpritePathPrefix("BossArena_EntryLock_FXPolish_CenterScan", EffectsV2Path);
        RequireComponentPolishRoot("BossArena_EntryLock_V20FX");
        RequireSpritePathPrefix("BossArena_EntryLock_V20FX_PressureRedGlow", EffectsV2Path);
        RequireSpritePathPrefix("BossArena_EntryLock_V20FX_LeftArc", EffectsV5Path);
        RequireSpritePathPrefix("BossArena_EntryLock_V20FX_RightArc", EffectsV5Path);
        RequireSpritePathPrefix("BossArena_EntryLock_V20FX_BottomSteam", EffectsV2Path);
        RequireSpritePathPrefix("BossArena_EntryLock_V20FX_UnlockScan", EffectsV2Path);

        RequireComponent<DoorLock>("Door_BossExit_MechCity");
        RequireSpritePathPrefix("Door_BossExit_MechCity_SlabVisual", EnvironmentV7Path);
        RequireSpritePathPrefix("Door_BossExit_MechCity_AmberLock_Socket", EnvironmentV7Path);
        RequireComponentPolishRoot("Door_BossExit_MechCity_PolishRefined");
        RequireSpritePathPrefix("Door_BossExit_MechCity_V20_RefinedOverlay", EnvironmentV20Path);
        RequireSpritePathPrefix("Door_BossExit_MechCity_PolishRefined_LockPlate", EnvironmentV19Path);
        RequireSpritePathPrefix("Door_BossExit_MechCity_PolishRefined_LockRed", EffectsV2Path);
        RequireSpritePathPrefix("Door_BossExit_MechCity_PolishRefined_OpenScan", EffectsV2Path);
        RequireSpritePathPrefix("Door_BossExit_MechCity_PolishRefined_DoorSteam", EffectsV2Path);
        RequireSpritePathPrefix("Door_BossExit_MechCity_PolishRefined_LeftStatusLamp", EnvironmentV7Path);
        RequireSpritePathPrefix("Door_BossExit_MechCity_PolishRefined_RightStatusLamp", EnvironmentV7Path);
        RequireSpritePathPrefix("Door_BossExit_MechCity_PolishRefined_OpenedCyanGlow", EffectsV5Path);
        RequireSpritePathPrefix("Door_BossExit_MechCity_PolishRefined_UnlockSpark", EffectsV5Path);
        RequireSpritePathPrefix("Door_BossExit_MechCity_V20FX_LockedWarningGlow", EffectsV2Path);
        RequireSpritePathPrefix("Door_BossExit_MechCity_V20FX_OpenSeamCyan", EffectsV2Path);
        RequireSpritePathPrefix("Door_BossExit_MechCity_V20FX_UnlockSteamBurst", EffectsV2Path);
        RequireSpritePathPrefix("Door_BossExit_MechCity_V20FX_TopStatusHalo", EffectsV5Path);
        RequireSerializedObjectReference<DoorLock>("Door_BossExit_MechCity", "lockLight");
        RequireSerializedObjectReference<DoorLock>("Door_BossExit_MechCity", "openScan");
        RequireSerializedObjectReference<DoorLock>("Door_BossExit_MechCity", "steamGlow");
        RequireSerializedObjectReference<DoorLock>("Door_BossExit_MechCity", "openedGlow");
        RequireSerializedObjectReference<DoorLock>("Door_BossExit_MechCity", "unlockSpark");
        RequireSerializedObjectReference<DoorLock>("Door_BossExit_MechCity", "lockedWarningGlow");
        RequireSerializedObjectReference<DoorLock>("Door_BossExit_MechCity", "openSeamGlow");
        RequireSerializedObjectReference<DoorLock>("Door_BossExit_MechCity", "unlockSteamBurst");
        RequireSerializedDoorMode("Door_BossExit_MechCity", DoorUnlockMode.EnemyClear);
        RequireSerializedWatchedEnemy("Door_BossExit_MechCity", "Boss_RepairStationGuardian");
        RequireNear(RequireObject("Door_BossExit_MechCity").transform.position, new Vector2(159.5f, -1.15f), 0.2f, "boss exit door position");

        RequireObject("ExitGoal_MechCityEntrance");
        RequireNear(RequireObject("ExitGoal_MechCityEntrance").transform.position, new Vector2(172f, -1.6f), 0.2f, "exit goal position");
        RequireArrowFXPolish("Wall_Arrow_Right_GeneratedSign", new Vector2(25.85f, -1.1f));
        RequireArrowFXPolish("MechCity_Entrance_SignPlate", new Vector2(164.8f, -0.55f));

        RequireMinimum<TutorialTrigger>(8);
        RequireMinimum<Checkpoint2D>(3);
        RequireMinimum<BossDamageHitbox2D>(8);
        RequireMinimum<BossEncounterController2D>(1);
    }

    private static void RequirePlayerAndAnimationObjects()
    {
        string[] playerParts =
        {
            "Boxbot_BackArm",
            "Boxbot_BackLeg",
            "Boxbot_Body",
            "Boxbot_FrontLeg",
            "Boxbot_FrontArm",
            "Boxbot_CableTail",
            "Boxbot_Eyes",
            "Boxbot_AntennaStem",
            "Boxbot_AntennaLamp",
            "Boxbot_GroundShadow",
        };

        foreach (string partName in playerParts)
        {
            RequireSpriteRenderer(partName);
        }

        RequireComponent<PlayerController2D>("Player_SmallAmnesiacRobot");
        RequireComponent<PlayerRobotVisualAnimator2D>("Player_SmallAmnesiacRobot");
        RequireComponent<SpriteFlicker2D>("Boxbot_Eyes");
        RequireComponent<SpriteFlicker2D>("Boxbot_AntennaLamp");
        RequireSceneObjectNameContains("Boxbot_Rim_", 6);
        RequireSpritePathPrefix("Boxbot_AntennaStem", EnvironmentV7Path);
        RequireSpritePathPrefix("Boxbot_AntennaLamp", EnvironmentV7Path);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "walkSpeed", 4.1f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "runSpeed", 7.2f);
        RequireSerializedObjectReference<PlayerRobotVisualAnimator2D>("Player_SmallAmnesiacRobot", "landingDustVisual");
        RequireSerializedObjectReference<PlayerRobotVisualAnimator2D>("Player_SmallAmnesiacRobot", "landingSparkVisual");
        RequireSerializedObjectReference<PlayerRobotVisualAnimator2D>("Player_SmallAmnesiacRobot", "runDustVisual");
        RequireSerializedObjectReference<PlayerRobotVisualAnimator2D>("Player_SmallAmnesiacRobot", "jumpBurstVisual");
        RequireSerializedObjectReference<PlayerRobotVisualAnimator2D>("Player_SmallAmnesiacRobot", "antennaStem");
        RequireSerializedObjectReference<PlayerRobotVisualAnimator2D>("Player_SmallAmnesiacRobot", "antennaTip");
        RequireSerializedObjectReference<PlayerRobotVisualAnimator2D>("Player_SmallAmnesiacRobot", "attackSlashVisual");
        RequireSerializedObjectReference<PlayerController2D>("Player_SmallAmnesiacRobot", "attackVisualRoot");
        RequireSerializedObjectReference<PlayerController2D>("Player_SmallAmnesiacRobot", "attackVisualAnimator");
        RequireSerializedObjectReference<PlayerController2D>("Player_SmallAmnesiacRobot", "hitSparkVisual");
        RequireSerializedObjectReference<PlayerController2D>("Player_SmallAmnesiacRobot", "heavyHitSparkVisual");
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "combo1Seconds", 0.22f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "combo2Seconds", 0.24f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "combo3Seconds", 0.32f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "airSlashSeconds", 0.28f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "comboInputBufferSeconds", 0.14f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "comboContinueWindow", 0.34f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "attackRange", 1.32f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "attackRangeStartRatio", 0.22f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "attackRangeGrowthPower", 1.18f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "attackDamageStartTime", 0.12f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "attackDamageEndTime", 0.92f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "attackRecoveryRangeRatio", 0.82f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "attackHitboxForwardOffset", -0.02f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "attackHitboxVerticalOffset", 0.02f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "attackHitboxHeight", 0.9f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "airSlashHitboxVerticalOffset", -0.24f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "airSlashHitboxHeight", 1.25f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "combo2RangeBonus", 0.38f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "combo3RangeBonus", 0.68f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "airSlashRangeBonus", 0.43f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "combo2HitboxVerticalBonus", 0.18f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "combo2HitboxHeightBonus", 0.28f);
        RequireSerializedFloat<PlayerController2D>("Player_SmallAmnesiacRobot", "combo3HitboxHeightBonus", 0.34f);
        RequireNear(RequireObject("AttackPoint").transform.localPosition, new Vector2(0.16f, -0.02f), 0.01f, "attack point local position");
        RequireComponent<AttackSlashAnimator2D>("AttackVisualRoot");
        RequireSerializedObjectReference<AttackSlashAnimator2D>("AttackVisualRoot", "combo1Arc");
        RequireSerializedObjectReference<AttackSlashAnimator2D>("AttackVisualRoot", "combo2Arc");
        RequireSerializedObjectReference<AttackSlashAnimator2D>("AttackVisualRoot", "combo3Arc");
        RequireSerializedObjectReference<AttackSlashAnimator2D>("AttackVisualRoot", "airSlashArc");
        RequireSerializedObjectReference<AttackSlashAnimator2D>("AttackVisualRoot", "chargeFlash");
        RequireSerializedFloat<AttackSlashAnimator2D>("AttackVisualRoot", "maxAlpha", 1f);
        RequireSerializedFloat<AttackSlashAnimator2D>("AttackVisualRoot", "scalePulse", 0.2f);
        RequireSpritePathPrefix("AttackCombo1_AmberArc", EffectsV7Path);
        RequireSpritePathPrefix("AttackCombo2_UpperArc", EffectsV7Path);
        RequireSpritePathPrefix("AttackCombo3_HeavyCleave", EffectsV7Path);
        RequireSpritePathPrefix("AttackAirSlash_DownArc", EffectsV7Path);
        RequireSpritePathPrefix("AttackCombo3_ChargeFlash", EffectsV7Path);
        RequireSpritePathPrefix("Player_HitSpark_HeavyBurst", EffectsV7Path);
        RequireComponent<OneShotSpriteBurst2D>("Player_HitSpark_HeavyBurst");
        ForbidObject("AttackSlash_AmberArc");
        RequireMinimum<OneShotSpriteBurst2D>(5);
    }

    private static void RequireSpawnIntroObjects()
    {
        GameObject introRoot = RequireObject("SpawnIntro_AwakeningPolish");
        RequireComponent<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish");
        RequireObject("SpawnIntro_CameraFocus");
        RequireObject("SpawnIntro_NarrativeCameraTarget");
        RequireObject("SpawnIntro_NarrativeScanStart");
        RequireObject("SpawnIntro_NarrativeScanEnd");
        RequireObject("SpawnIntro_NarrativeBackHalo");
        RequireObject("SpawnIntro_NarrativeScanLine");
        RequireObject("SpawnIntro_NarrativeWeakArc");
        RequireObject("SpawnIntro_NarrativeFineDust");
        RequireObject("SpawnIntro_NarrativeStatusLamp");
        RequireObject("SpawnIntro_WarmBenchHalo");
        RequireObject("SpawnIntro_LowFloorGlow");
        RequireObject("SpawnIntro_GroundFog");
        RequireObject("SpawnIntro_TopFineDust");
        RequireObject("SpawnIntro_ServicePanelGlow");
        RequireObject("SpawnIntro_ScanBeam");
        RequireObject("SpawnIntro_ScanHalo");
        RequireObject("SpawnIntro_ArmSpark");
        RequireObject("SpawnIntro_CableSpark");
        RequireObject("SpawnIntro_BedArc_Left");
        RequireObject("SpawnIntro_BedArc_Right");
        RequireObject("SpawnIntro_SteamLeft");
        RequireObject("SpawnIntro_SteamRight");
        RequireObject("SpawnIntro_BenchStatusLamp");
        RequireObject("SpawnIntro_RepairArm");

        RequireSerializedObjectReference<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "playerController");
        RequireSerializedObjectReference<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "playerBody");
        RequireSerializedObjectReference<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "playerVisualAnimator");
        RequireSerializedObjectReference<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "cameraFollow");
        RequireSerializedObjectReference<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "introCameraTarget");
        RequireSerializedObjectReference<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "narrativeCameraTarget");
        RequireSerializedObjectReference<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "narrativeScanStart");
        RequireSerializedObjectReference<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "narrativeScanEnd");
        RequireSerializedObjectReference<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "scanBeamTransform");
        RequireSerializedObjectReference<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "repairArmTransform");
        RequireSerializedObjectReference<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "benchShakeRoot");
        RequireSerializedFloat<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "introSeconds", 10f);
        RequireSerializedBool<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "allowSkip", true);
        RequireSerializedBool<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "narrativeIntroEnabled", true);
        RequireSerializedFloat<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "terminalLogSeconds", 2.8f);
        RequireSerializedFloat<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "environmentScanSeconds", 3.2f);
        RequireSerializedFloat<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "awakeningSeconds", 4f);
        RequireSerializedString<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "narrativeHeader", "A-07 // 离线重启");
        RequireSerializedString<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "narrativeLineB", "记忆核心缺失");
        RequireSerializedFloat<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "benchShakeAmplitude", 0.034f);
        RequireSerializedFloat<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "cameraFocusShakeAmplitude", 0.012f);
        RequireSerializedArrayMinimum<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "warmGlowRenderers", 2);
        RequireSerializedArrayMinimum<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "scanRenderers", 2);
        RequireSerializedArrayMinimum<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "steamRenderers", 3);
        RequireSerializedArrayMinimum<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "sparkRenderers", 2);
        RequireSerializedArrayMinimum<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "electricRenderers", 2);
        RequireSerializedArrayMinimum<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "dustRenderers", 1);
        RequireSerializedArrayMinimum<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "serviceLightRenderers", 2);
        RequireSerializedArrayMinimum<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "narrativeScanRenderers", 2);
        RequireSerializedArrayMinimum<PlayerSpawnIntro2D>("SpawnIntro_AwakeningPolish", "narrativeEnvironmentRenderers", 3);

        RequireNear(RequireObject("SpawnIntro_CameraFocus").transform.position, new Vector2(2.35f, -1.85f), 0.1f, "spawn intro camera focus");
        RequireNear(RequireObject("SpawnIntro_NarrativeScanStart").transform.position, new Vector2(10.15f, -0.25f), 0.1f, "spawn intro narrative scan start");
        RequireNear(RequireObject("SpawnIntro_NarrativeScanEnd").transform.position, new Vector2(2.35f, -1.85f), 0.1f, "spawn intro narrative scan end");
        RequireNear(RequireObject("SpawnIntro_RepairArm").transform.position, new Vector2(1.62f, -1.02f), 0.1f, "spawn intro repair arm");
        RequireNear(RequireObject("SpawnIntro_ArmSpark").transform.position, new Vector2(2.22f, -0.98f), 0.1f, "spawn intro arm spark");
        RequireSpritePathPrefix("SpawnIntro_NarrativeBackHalo", EffectsV5Path);
        RequireSpritePathPrefix("SpawnIntro_NarrativeScanLine", EffectsV2Path);
        RequireSpritePathPrefix("SpawnIntro_NarrativeWeakArc", EffectsV5Path);
        RequireSpritePathPrefix("SpawnIntro_NarrativeFineDust", EffectsV5Path);
        RequireSpritePathPrefix("SpawnIntro_NarrativeStatusLamp", EnvironmentV7Path);
        RequireSpritePathPrefix("SpawnIntro_WarmBenchHalo", EffectsV2Path);
        RequireSpritePathPrefix("SpawnIntro_ScanBeam", EffectsV2Path);
        RequireSpritePathPrefix("SpawnIntro_ArmSpark", EffectsV5Path);
        RequireSpritePathPrefix("SpawnIntro_BedArc_Left", EffectsV5Path);
        RequireSpritePathPrefix("SpawnIntro_RepairArm", EnvironmentV7Path);
        RequireAwakeningBenchRefinedAssembly();
        if (introRoot.GetComponentsInChildren<Collider2D>(true).Length > 0)
        {
            throw new Exception("SpawnIntro_AwakeningPolish must not add gameplay collision.");
        }
    }

    private static void RequireAwakeningBenchRefinedAssembly()
    {
        GameObject assembly = RequireObject("AwakeningBench_RefinedAssembly");
        RequireObject("AwakeningBench_RefinedBody");
        RequireSpritePathPrefix("AwakeningBench_RefinedBody", EnvironmentV12Path);
        ForbidObject("Awakening_V7_RepairBench");
        RequireAwakeningBenchTablePolish();
        if (assembly.GetComponentsInChildren<Collider2D>(true).Length > 0)
        {
            throw new Exception("AwakeningBench_RefinedAssembly must not add gameplay collision.");
        }
    }

    private static void RequireAwakeningBenchTablePolish()
    {
        GameObject root = RequireObject("AwakeningBench_TablePolish");
        RequireObject("AwakeningBench_Table_UnderShadow");
        RequireObject("AwakeningBench_Table_UnderGlow");
        RequireObject("AwakeningBench_Table_TopScuffs");
        RequireObject("AwakeningBench_Table_FrontRivets");
        RequireObject("AwakeningBench_Table_FrontRivets_B");
        RequireObject("AwakeningBench_Table_OilStain");
        RequireObject("AwakeningBench_Table_CableUnderRun");
        RequireObject("AwakeningBench_Table_ScanLine");
        RequireObject("AwakeningBench_Table_StatusLamp");
        RequireSpritePathPrefix("AwakeningBench_Table_UnderShadow", EffectsV4Path);
        RequireSpritePathPrefix("AwakeningBench_Table_UnderGlow", EffectsV5Path);
        RequireSpritePathPrefix("AwakeningBench_Table_TopScuffs", EnvironmentV8Path);
        RequireSpritePathPrefix("AwakeningBench_Table_FrontRivets", EnvironmentV10Path);
        RequireSpritePathPrefix("AwakeningBench_Table_CableUnderRun", EnvironmentV7Path);
        RequireSpritePathPrefix("AwakeningBench_Table_ScanLine", EffectsV2Path);
        RequireComponent<SpriteFlicker2D>("AwakeningBench_Table_StatusLamp");
        RequireComponent<LoopingBackgroundMotion2D>("AwakeningBench_Table_ScanLine");

        if (root.GetComponentsInChildren<Collider2D>(true).Length > 0)
        {
            throw new Exception("AwakeningBench_TablePolish must not add gameplay collision.");
        }
    }

    private static void RequireJumpRouteBackgroundPolish()
    {
        GameObject root = RequireObject("JumpRoute_BackgroundPolish_BrokenPlatforms");
        RequireObject("JumpRoute_Background_MachineFrame");
        RequireObject("JumpRoute_Background_RepairPlate");
        RequireObject("JumpRoute_Background_RightBrokenWindow");
        RequireObject("JumpRoute_Background_LeftTruss");
        RequireObject("JumpRoute_Background_LowerRoadWarmBacklight");
        RequireObject("JumpRoute_Background_UpperDeckSoftGlow");
        RequireObject("JumpRoute_Background_RightRoadCoolGlow");
        RequireObject("JumpRoute_Background_LowOilMist");
        RequireObject("JumpRoute_Background_FineDust");
        RequireObject("JumpRoute_Background_SteamUnderLeft");
        RequireObject("JumpRoute_Background_SteamUnderRight");
        RequireObject("JumpRoute_Background_WallElectricTick");
        RequireObject("JumpRoute_Background_UpperScanBeam");
        RequireObject("JumpRoute_Background_HighChain_A");
        RequireObject("JumpRoute_Background_HighChain_B");
        RequireObject("JumpRoute_Background_SmallStatusLamp");

        RequireSpritePathPrefix("JumpRoute_Background_MachineFrame", ProvidedEnvironmentV4Path);
        RequireSpritePathPrefix("JumpRoute_Background_RepairPlate", EnvironmentV11Path);
        RequireSpritePathPrefix("JumpRoute_Background_RightBrokenWindow", ProvidedEnvironmentV3Path);
        RequireSpritePathPrefix("JumpRoute_Background_LeftTruss", ProvidedEnvironmentV2Path);
        RequireSpritePathPrefix("JumpRoute_Background_LowerRoadWarmBacklight", EffectsV5Path);
        RequireSpritePathPrefix("JumpRoute_Background_LowOilMist", EffectsV5Path);
        RequireSpritePathPrefix("JumpRoute_Background_SteamUnderLeft", EffectsV4Path);
        RequireSpritePathPrefix("JumpRoute_Background_WallElectricTick", EffectsV5Path);
        RequireSpritePathPrefix("JumpRoute_Background_UpperScanBeam", EffectsV2Path);
        RequireSpritePathPrefix("JumpRoute_Background_HighChain_A", EnvironmentV11Path);
        RequireSpritePathPrefix("JumpRoute_Background_SmallStatusLamp", EnvironmentV7Path);
        RequireComponent<SwayingDecor2D>("JumpRoute_Background_HighChain_A");
        RequireComponent<SwayingDecor2D>("JumpRoute_Background_HighChain_B");
        RequireComponent<SpriteFlicker2D>("JumpRoute_Background_SmallStatusLamp");

        foreach (Transform child in root.GetComponentsInChildren<Transform>(true))
        {
            if (child.GetComponent<Collider2D>() != null)
            {
                throw new Exception($"{child.gameObject.name} is jump route background polish and must not have a Collider2D.");
            }

            SpriteRenderer renderer = child.GetComponent<SpriteRenderer>();
            if (renderer != null &&
                (renderer.gameObject.name.Contains("AmberWalkable") ||
                 renderer.gameObject.name.Contains("RoadModule") ||
                 renderer.gameObject.name.Contains("Road_Amber")))
            {
                throw new Exception($"{renderer.gameObject.name} must not reuse walkable road language in jump route background polish.");
            }
        }
    }

    private static void RequireRespawnCinematicObjects()
    {
        GameObject runtimeRoot = RequireObject("RespawnPolish_Runtime");
        RequireComponent<PlayerRespawnCinematic2D>("RespawnPolish_Runtime");
        RequireObject("RespawnPolish_CameraFocus");
        RequireObject("RespawnPolish_FailureAnchor");
        RequireObject("RespawnPolish_ReturnAnchor");
        RequireObject("RespawnPolish_FailureFlash");
        RequireObject("RespawnPolish_FailureSpark");
        RequireObject("RespawnPolish_FailureDust");
        RequireObject("RespawnPolish_ReturnHalo");
        RequireObject("RespawnPolish_ReturnScanBeam");
        RequireObject("RespawnPolish_ReturnSteam");
        RequireObject("RespawnPolish_ReturnSpark");
        RequireObject("RespawnPolish_ReturnDust");
        RequireObject("RespawnPolish_ReturnStatusLight");

        RequireSerializedObjectReference<PlayerRespawnCinematic2D>("RespawnPolish_Runtime", "playerController");
        RequireSerializedObjectReference<PlayerRespawnCinematic2D>("RespawnPolish_Runtime", "playerBody");
        RequireSerializedObjectReference<PlayerRespawnCinematic2D>("RespawnPolish_Runtime", "playerHealth");
        RequireSerializedObjectReference<PlayerRespawnCinematic2D>("RespawnPolish_Runtime", "playerVisualAnimator");
        RequireSerializedObjectReference<PlayerRespawnCinematic2D>("RespawnPolish_Runtime", "cameraFollow");
        RequireSerializedObjectReference<PlayerRespawnCinematic2D>("RespawnPolish_Runtime", "cameraFocusTarget");
        RequireSerializedObjectReference<PlayerRespawnCinematic2D>("RespawnPolish_Runtime", "failureAnchor");
        RequireSerializedObjectReference<PlayerRespawnCinematic2D>("RespawnPolish_Runtime", "returnAnchor");
        RequireSerializedObjectReference<PlayerRespawnCinematic2D>("RespawnPolish_Runtime", "respawnScanBeam");
        RequireSerializedFloat<PlayerRespawnCinematic2D>("RespawnPolish_Runtime", "cinematicSeconds", 1.25f);
        RequireSerializedFloat<PlayerRespawnCinematic2D>("RespawnPolish_Runtime", "teleportAt", 0.38f);
        RequireSerializedArrayMinimum<PlayerRespawnCinematic2D>("RespawnPolish_Runtime", "failureRenderers", 3);
        RequireSerializedArrayMinimum<PlayerRespawnCinematic2D>("RespawnPolish_Runtime", "returnRenderers", 6);

        RequireRespawnPointPolish("AwakeningBench", "Checkpoint_AwakeningBench", new Vector2(2.2f, -1.85f));
        RequireRespawnPointPolish("BeforeJump", "Checkpoint_BeforeJump", new Vector2(40.5f, -1.85f));
        RequireRespawnPointPolish("ChargingStation", "Checkpoint_ChargingStation", new Vector2(117f, -1.85f));

        RequireSpritePathPrefix("RespawnPolish_FailureFlash", EffectsV5Path);
        RequireSpritePathPrefix("RespawnPolish_FailureSpark", EffectsV2Path);
        RequireSpritePathPrefix("RespawnPolish_ReturnScanBeam", EffectsV2Path);
        RequireSpritePathPrefix("RespawnPolish_ReturnSpark", EffectsV5Path);
        RequireSpritePathPrefix("RespawnPolish_ReturnStatusLight", EnvironmentV7Path);
        if (runtimeRoot.GetComponentsInChildren<Collider2D>(true).Length > 0)
        {
            throw new Exception("RespawnPolish_Runtime must not add gameplay collision.");
        }
    }

    private static void RequireRespawnPointPolish(string key, string checkpointName, Vector2 expectedPosition)
    {
        string rootName = "RespawnPoint_Polish_" + key;
        GameObject root = RequireObject(rootName);
        RequireObject(rootName + "_Halo");
        RequireObject(rootName + "_Dust");
        RequireObject(rootName + "_StatusLamp");
        RequireObject(rootName + "_ActivationPulse");
        RequireComponentPolishRoot(checkpointName + "_PolishRefined");
        RequireSpritePathPrefix(checkpointName + "_PolishRefined_StatusCore", EnvironmentV19Path);
        RequireSpritePathPrefix(checkpointName + "_PolishRefined_ScanLine", EffectsV2Path);
        RequireComponentPolishRoot(checkpointName + "_FXPolish");
        RequireSpritePathPrefix(checkpointName + "_FXPolish_IdleRing", EffectsV5Path);
        RequireSpritePathPrefix(checkpointName + "_FXPolish_ActivatedRing", EffectsV5Path);
        RequireSpritePathPrefix(checkpointName + "_FXPolish_VerticalScan", EffectsV2Path);
        RequireSpritePathPrefix(checkpointName + "_FXPolish_FineDust", EffectsV5Path);
        RequireComponent<OneShotSpriteBurst2D>(rootName + "_ActivationPulse");
        RequireSerializedObjectReference<Checkpoint2D>(checkpointName, "activationPulse");
        RequireSerializedObjectReference<Checkpoint2D>(checkpointName, "activeLight");
        RequireSerializedObjectReference<Checkpoint2D>(checkpointName, "idleGlow");
        RequireSerializedObjectReference<Checkpoint2D>(checkpointName, "activatedGlow");
        RequireNear(root.transform.position, expectedPosition, 0.1f, rootName + " position");
        RequireSpritePathPrefix(rootName + "_Halo", EffectsV5Path);
        RequireSpritePathPrefix(rootName + "_StatusLamp", EnvironmentV7Path);
        if (root.GetComponentsInChildren<Collider2D>(true).Length > 0)
        {
            throw new Exception(rootName + " must not add gameplay collision.");
        }
    }

    private static void RequireArrowFXPolish(string signName, Vector2 expectedPosition)
    {
        RequireSpritePathPrefix(signName, EnvironmentV7Path);
        RequireNear(RequireObject(signName).transform.position, expectedPosition, 0.08f, signName + " position");
        RequireComponentPolishRoot(signName + "_FXPolish");
        RequireNear(RequireObject(signName + "_FXPolish").transform.position, Vector2.zero, 0.02f, signName + "_FXPolish root position");
        RequireSpritePathPrefix(signName + "_FXPolish_BackHalo", EffectsV5Path);
        RequireSpritePathPrefix(signName + "_FXPolish_DirectionScan", EffectsV2Path);
        RequireSpritePathPrefix(signName + "_FXPolish_StatusLamp", EnvironmentV7Path);
        RequireSpritePathPrefix(signName + "_FXPolish_TinySpark", EffectsV5Path);
    }

    private static void RequireDynamicAtmosphere()
    {
        RequireMinimum<SpriteFlicker2D>(19);
        RequireMinimum<SteamPuff2D>(25);
        RequireMinimum<SimpleRotator2D>(18);
        RequireMinimum<AmbientDrift2D>(16);
        RequireMinimum<ParallaxLayer2D>(4);
        RequireMinimum<LoopingBackgroundMotion2D>(14);
        RequireMinimum<ElectricArcFlicker2D>(17);
        RequireMinimum<SwayingDecor2D>(10);
        RequireMinimum<EnemyVisualAnimator2D>(3);
        RequireSceneObjectNameContains("DustVeil_V2_", 4);
        RequireSceneObjectNameContains("SteamV2_", 12);
        RequireSceneObjectNameContains("SparkV2_", 9);
        RequireSceneObjectNameContains("ScanBeamV2_", 2);
        RequireSceneObjectNameContains("ThinSteam", 3);
        RequireSceneObjectNameContains("DustMotes", 1);
        RequireSceneObjectNameContains("GreenChargePulse", 1);
        RequireSceneObjectNameContains("VentFan", 1);
        RequireSceneObjectNameContains("FarDecor_V10_WarningBlink", 5);
        RequireSceneObjectNameContains("MidDecor_V10_Arc", 3);
        RequireSceneObjectNameContains("NearDecor_V10_TopChain", 5);
        RequireSceneObjectNameContains("MidDecor_V11_Lamp", 4);
        RequireSceneObjectNameContains("ProvidedV2_", 13);
        RequireSceneObjectNameContains("ProvidedV3_", 18);
        RequireSceneObjectNameContains("SeamBreakerV8_", 24);
        RequireSceneObjectNameContains("V12_FarLightning", 3);
        RequireSceneObjectNameContains("V12_MidLightning", 3);
        RequireSceneObjectNameContains("V12_FlickerLight", 3);
        RequireSceneObjectNameContains("V13_MidLightning", 8);
        RequireSceneObjectNameContains("V13_FarDust", 5);
        RequireSceneObjectNameContains("V13_FarRotator", 5);
        RequireSceneObjectNameContains("V13_MidConveyorPulse", 3);
        RequireSceneObjectNameContains("V13_NearSwayChain", 5);
        RequireSceneObjectNameContains("V14_FarLarge", 7);
        RequireSceneObjectNameContains("V14_MidLarge", 7);
        RequireSceneObjectNameContains("V14_NearCraneHook", 2);
        RequireSceneObjectNameContains("V14_MidLightning", 3);
        RequireSceneObjectNameContains("V14_FarDust", 3);
        RequireSceneObjectNameContains("V15_SeamTruss", 4);
        RequireSceneObjectNameContains("V15_MidLightning", 4);
        RequireSceneObjectNameContains("V15_MidRotator", 3);
        RequireSceneObjectNameContains("V15_NearSwayHook", 3);
        RequireSceneObjectNameContains("V16_FarDust_", 5);
        RequireSceneObjectNameContains("V16_MidElectric_", 5);
        RequireSceneObjectNameContains("V16_MidScan_", 2);
        RequireSceneObjectNameContains("V16_NearSwayChain_", 4);
        RequireSceneObjectNameContains("FallDust", 2);
        RequireSceneObjectNameContains("FallingDust", 1);
        RequireSpritePathPrefix("Trap_V7_BlueArc_Detail_A", EffectsV2Path);
        RequireSpritePathPrefix("Trap_V7_BlueArc_Detail_B", EffectsV2Path);
    }

    private static void RequireGeneratedSpriteImportSettings(string path, int pixelsPerUnit, int maxTextureSize, FilterMode filterMode, string label)
    {
        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        if (texture == null)
        {
            throw new Exception($"Missing {label} texture asset: {path}");
        }

        TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
        if (importer == null ||
            importer.textureType != TextureImporterType.Sprite ||
            importer.spritePixelsPerUnit != pixelsPerUnit ||
            importer.maxTextureSize != maxTextureSize ||
            importer.wrapMode != TextureWrapMode.Clamp ||
            importer.filterMode != filterMode ||
            !importer.alphaIsTransparency)
        {
            throw new Exception($"{path} has incorrect import settings for {label}.");
        }

        TextureImporterPlatformSettings defaultSettings = importer.GetDefaultPlatformTextureSettings();
        TextureImporterPlatformSettings standaloneSettings = importer.GetPlatformTextureSettings("Standalone");
        if (defaultSettings.maxTextureSize != maxTextureSize ||
            defaultSettings.textureCompression != TextureImporterCompression.Uncompressed ||
            !standaloneSettings.overridden ||
            standaloneSettings.maxTextureSize != maxTextureSize ||
            standaloneSettings.textureCompression != TextureImporterCompression.Uncompressed)
        {
            throw new Exception($"{path} must keep {maxTextureSize}px uncompressed import settings for Default and Standalone platforms.");
        }
    }

    private static void RequireGeneratedSpriteImportSettings(string path, int pixelsPerUnit, int importerMaxTextureSize, int platformMaxTextureSize, FilterMode filterMode, string label)
    {
        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        if (texture == null)
        {
            throw new Exception($"Missing {label} texture asset: {path}");
        }

        TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
        if (importer == null ||
            importer.textureType != TextureImporterType.Sprite ||
            importer.spritePixelsPerUnit != pixelsPerUnit ||
            importer.maxTextureSize != importerMaxTextureSize ||
            importer.wrapMode != TextureWrapMode.Clamp ||
            importer.filterMode != filterMode ||
            !importer.alphaIsTransparency)
        {
            throw new Exception($"{path} has incorrect import settings for {label}.");
        }

        TextureImporterPlatformSettings defaultSettings = importer.GetDefaultPlatformTextureSettings();
        TextureImporterPlatformSettings standaloneSettings = importer.GetPlatformTextureSettings("Standalone");
        if (defaultSettings.maxTextureSize != platformMaxTextureSize ||
            defaultSettings.textureCompression != TextureImporterCompression.Uncompressed ||
            !standaloneSettings.overridden ||
            standaloneSettings.maxTextureSize != platformMaxTextureSize ||
            standaloneSettings.textureCompression != TextureImporterCompression.Uncompressed)
        {
            throw new Exception($"{path} must keep {platformMaxTextureSize}px uncompressed import settings for Default and Standalone platforms.");
        }
    }

    private static void RequireExistingSpriteImportSettings(string path, int pixelsPerUnit, string label)
    {
        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        if (texture == null)
        {
            throw new Exception($"Missing {label} texture asset: {path}");
        }

        TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
        if (importer == null ||
            importer.textureType != TextureImporterType.Sprite ||
            importer.spritePixelsPerUnit != pixelsPerUnit ||
            !importer.alphaIsTransparency)
        {
            throw new Exception($"{path} has incorrect import settings for {label}.");
        }
    }

    private static void RequireCameraBackgroundCoverage(Bounds backgroundBounds)
    {
        GameObject cameraObject = RequireObject("Main Camera");
        Camera camera = cameraObject.GetComponent<Camera>();
        CameraFollow2D follow = cameraObject.GetComponent<CameraFollow2D>();
        if (camera == null || follow == null)
        {
            throw new Exception("Main Camera is missing Camera or CameraFollow2D.");
        }

        RequireSerializedFloat<CameraFollow2D>("Main Camera", "horizontalLookahead", 1.15f);
        RequireSerializedFloat<CameraFollow2D>("Main Camera", "maxLookaheadSpeed", 7.2f);
        RequireSerializedFloat<CameraFollow2D>("Main Camera", "lookaheadSmoothTime", 0.24f);

        Vector2 minBounds = GetSerializedVector2(follow, "minBounds");
        Vector2 maxBounds = GetSerializedVector2(follow, "maxBounds");
        float[] aspects = { 16f / 9f, 16f / 10f };
        foreach (float aspect in aspects)
        {
            float halfHeight = camera.orthographicSize;
            float halfWidth = halfHeight * aspect;
            Rect viewport = GetCameraViewportBounds(minBounds, maxBounds, halfWidth, halfHeight);
            if (backgroundBounds.min.x > viewport.xMin - BackgroundCoverageMargin + 0.05f ||
                backgroundBounds.max.x < viewport.xMax + BackgroundCoverageMargin - 0.05f ||
                backgroundBounds.min.y > viewport.yMin - BackgroundCoverageMargin + 0.05f ||
                backgroundBounds.max.y < viewport.yMax + BackgroundCoverageMargin - 0.05f)
            {
                throw new Exception($"V15 background bounds {backgroundBounds} do not cover {aspect:0.###} viewport {viewport} with {BackgroundCoverageMargin} units margin.");
            }
        }
    }

    private static Rect GetCameraViewportBounds(Vector2 minBounds, Vector2 maxBounds, float halfWidth, float halfHeight)
    {
        float minCenterX = minBounds.x + halfWidth;
        float maxCenterX = maxBounds.x - halfWidth;
        float minCenterY = minBounds.y + halfHeight;
        float maxCenterY = maxBounds.y - halfHeight;

        if (minCenterX > maxCenterX)
        {
            float center = (minCenterX + maxCenterX) * 0.5f;
            minCenterX = center;
            maxCenterX = center;
        }

        if (minCenterY > maxCenterY)
        {
            float center = (minCenterY + maxCenterY) * 0.5f;
            minCenterY = center;
            maxCenterY = center;
        }

        return Rect.MinMaxRect(minCenterX - halfWidth, minCenterY - halfHeight, maxCenterX + halfWidth, maxCenterY + halfHeight);
    }

    private static void RequireNoVisibleWhitePixel()
    {
        foreach (SpriteRenderer renderer in UnityEngine.Object.FindObjectsOfType<SpriteRenderer>(true))
        {
            if (renderer.sprite != null && renderer.sprite.name == "white_pixel")
            {
                throw new Exception($"{renderer.gameObject.name} still renders white_pixel; visible art must use generated sprites.");
            }
        }
    }

    private static void RequireTimedElectricFloor(string name, Vector2 position, Vector2 colliderSize, float cycleOffsetSeconds)
    {
        RequireComponent<TimedElectricFloor2D>(name);
        RequireSpritePathPrefix(name + "_BlueCurrentVisual", EffectsV2Path);
        RequireComponentPolishRoot(name + "_PolishRefined");
        RequireSpritePathPrefix(name + "_PolishRefined_WarningRed", EffectsV2Path);
        RequireSpritePathPrefix(name + "_PolishRefined_SafeCyan", EffectsV5Path);
        RequireSpritePathPrefix(name + "_PolishRefined_ArcA", EffectsV2Path);
        RequireSpritePathPrefix(name + "_PolishRefined_ArcB", EffectsV2Path);
        GameObject currentVisual = RequireObject(name + "_BlueCurrentVisual");
        RequireNear(currentVisual.transform.localPosition, new Vector2(0f, 0.16f), 0.03f, $"{name} current visual local position");
        RequireNear(RequireObject(name).transform.position, position, 0.2f, $"{name} position");
        BoxCollider2D electricCollider = RequireObject(name).GetComponent<BoxCollider2D>();
        if (electricCollider == null || !electricCollider.isTrigger)
        {
            throw new Exception($"{name} must be a trigger electric floor.");
        }

        RequireNear(electricCollider.size, colliderSize, 0.08f, $"{name} collider size");
        GameObject conductiveDeck = RequireObject(name + "_ConductiveDeck");
        BoxCollider2D deckCollider = conductiveDeck.GetComponent<BoxCollider2D>();
        if (deckCollider == null || deckCollider.isTrigger)
        {
            throw new Exception($"{name}_ConductiveDeck must be a solid platform underneath the current.");
        }

        RequireNear(conductiveDeck.transform.position, position, 0.05f, $"{name} conductive deck position");
        RequireNear(deckCollider.size, new Vector2(colliderSize.x, 0.32f), 0.08f, $"{name} conductive deck size");
        RequireSpritePathPrefix(name + "_ConductiveDeckVisual", EnvironmentV11Path);
        SpriteRenderer currentRenderer = currentVisual.GetComponent<SpriteRenderer>();
        SpriteRenderer deckRenderer = RequireObject(name + "_ConductiveDeckVisual").GetComponent<SpriteRenderer>();
        if (currentRenderer == null || deckRenderer == null || currentRenderer.sortingOrder <= deckRenderer.sortingOrder)
        {
            throw new Exception($"{name} blue current must render above its conductive deck.");
        }

        RequireSerializedFloat<TimedElectricFloor2D>(name, "cycleOffsetSeconds", cycleOffsetSeconds);
        RequireSerializedObjectReference<TimedElectricFloor2D>(name, "warningLight");
        RequireSerializedObjectReference<TimedElectricFloor2D>(name, "safeLight");
        RequireSerializedArrayMinimum<TimedElectricFloor2D>(name, "accentArcs", 2);
    }

    private static void RequirePowerSwitch(string name, Vector2 position, string[] targetFloorNames)
    {
        RequireComponent<PowerSwitch2D>(name);
        RequireSpritePathPrefix(name + "_Panel", EnvironmentV9Path);
        RequireSpritePathPrefix(name + "_BreakerLight", EffectsV5Path);
        RequireComponentPolishRoot(name + "_PolishRefined");
        RequireSpritePathPrefix(name + "_PolishRefined_ControlPlate", EnvironmentV19Path);
        RequireSpritePathPrefix(name + "_PolishRefined_ReadyAmber", EffectsV5Path);
        RequireSpritePathPrefix(name + "_PolishRefined_ActiveCyan", EffectsV5Path);
        RequireSpritePathPrefix(name + "_PolishRefined_CooldownRed", EffectsV2Path);
        RequireNear(RequireObject(name).transform.position, position, 0.12f, $"{name} position");
        RequireSerializedObjectReference<PowerSwitch2D>(name, "switchLight");
        RequireSerializedObjectReference<PowerSwitch2D>(name, "readyGlow");
        RequireSerializedObjectReference<PowerSwitch2D>(name, "activeGlow");
        RequireSerializedObjectReference<PowerSwitch2D>(name, "cooldownGlow");
        RequireSerializedFloat<PowerSwitch2D>(name, "safetySeconds", 5.2f);
        RequireSerializedFloat<PowerSwitch2D>(name, "cooldownSeconds", 7.0f);

        GameObject switchObject = RequireObject(name);
        PowerSwitch2D powerSwitch = switchObject.GetComponent<PowerSwitch2D>();
        SerializedObject serializedObject = new SerializedObject(powerSwitch);
        SerializedProperty targets = serializedObject.FindProperty("targetFloors");
        if (targets == null || !targets.isArray || targets.arraySize != targetFloorNames.Length)
        {
            throw new Exception($"{name}.targetFloors must reference {targetFloorNames.Length} electric floors.");
        }

        foreach (string targetFloorName in targetFloorNames)
        {
            TimedElectricFloor2D expectedFloor = RequireObject(targetFloorName).GetComponent<TimedElectricFloor2D>();
            bool found = false;
            for (int i = 0; i < targets.arraySize; i++)
            {
                if (targets.GetArrayElementAtIndex(i).objectReferenceValue == expectedFloor)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                throw new Exception($"{name}.targetFloors is missing {targetFloorName}.");
            }
        }

        BoxCollider2D trigger = switchObject.GetComponent<BoxCollider2D>();
        if (trigger == null || !trigger.isTrigger)
        {
            throw new Exception($"{name} must use a trigger collider for interaction.");
        }
    }

    private static void RequireInsulatedStep(string name, Vector2 position, Vector2 size)
    {
        GameObject insulatedStep = RequireObject(name);
        BoxCollider2D insulatedStepCollider = insulatedStep.GetComponent<BoxCollider2D>();
        if (insulatedStepCollider == null || insulatedStepCollider.isTrigger)
        {
            throw new Exception($"{name} must be a solid helper platform.");
        }

        RequireNear(insulatedStep.transform.position, position, 0.1f, $"{name} position");
        RequireNear(insulatedStepCollider.size, size, 0.08f, $"{name} size");
        RequireSpritePathPrefix(name + "_V10_InsulatedPadShadow", EnvironmentV10Path);
        RequireSpritePathPrefix(name + "_V11_InsulatedPad", EnvironmentV11Path);
        ForbidObject(name + "_V10_ShortBridgeUnderbeam");
        ForbidObject(name + "_V11_InsulatedPlate");
    }

    private static void RequireCompressorTrap(string name, Vector2 position, float cycleOffsetSeconds)
    {
        RequireComponent<CompressorTrap2D>(name);
        RequireSpritePathPrefix(name + "_MovingPlate", EnvironmentV7Path);
        RequireSpritePathPrefix(name + "_WarningLamp_Red", EnvironmentV7Path);
        RequireNear(RequireObject(name).transform.position, position, 0.2f, $"{name} position");
        RequireSerializedFloat<CompressorTrap2D>(name, "cycleOffsetSeconds", cycleOffsetSeconds);
    }

    private static GameObject RequireObject(string name)
    {
        GameObject target = FindSceneObject(name);
        if (target == null)
        {
            throw new Exception($"Missing required scene object: {name}");
        }

        return target;
    }

    private static GameObject RequireComponentPolishRoot(string name)
    {
        GameObject root = RequireObject(name);
        if (root.GetComponentsInChildren<Collider2D>(true).Length > 0)
        {
            throw new Exception($"{name} must be visual-only and must not add gameplay collision.");
        }

        return root;
    }

    private static void ForbidObject(string name)
    {
        if (FindSceneObject(name) != null)
        {
            throw new Exception($"Forbidden legacy scene object still exists: {name}");
        }
    }

    private static void ForbidSceneObjectPrefix(string prefix)
    {
        GameObject match = AllSceneObjects().FirstOrDefault(obj => obj.name.StartsWith(prefix, StringComparison.Ordinal));
        if (match != null)
        {
            throw new Exception($"Forbidden legacy scene object still exists: {match.name}");
        }
    }

    private static void ForbidSceneObjectNameContains(string fragment)
    {
        GameObject match = AllSceneObjects().FirstOrDefault(obj => obj.name.IndexOf(fragment, StringComparison.Ordinal) >= 0);
        if (match != null)
        {
            throw new Exception($"Forbidden legacy scene object still exists: {match.name}");
        }
    }

    private static void ForbidSceneSpritePrefix(string prefix)
    {
        foreach (SpriteRenderer renderer in UnityEngine.Object.FindObjectsOfType<SpriteRenderer>(true))
        {
            if (renderer.sprite != null && renderer.sprite.name.StartsWith(prefix, StringComparison.Ordinal))
            {
                throw new Exception($"Forbidden legacy sprite is still referenced by {renderer.gameObject.name}: {renderer.sprite.name}");
            }
        }
    }

    private static void ForbidSceneSpritePathPrefix(string prefix)
    {
        foreach (SpriteRenderer renderer in UnityEngine.Object.FindObjectsOfType<SpriteRenderer>(true))
        {
            string spritePath = renderer.sprite != null ? AssetDatabase.GetAssetPath(renderer.sprite) : string.Empty;
            if (spritePath.StartsWith(prefix, StringComparison.Ordinal))
            {
                throw new Exception($"{renderer.gameObject.name} still references old sprite path {spritePath}.");
            }
        }
    }

    private static void RequireComponent<T>(string objectName) where T : Component
    {
        GameObject target = RequireObject(objectName);
        if (target.GetComponent<T>() == null)
        {
            throw new Exception($"{objectName} is missing component {typeof(T).Name}.");
        }
    }

    private static SpriteRenderer RequireSpriteRenderer(string objectName)
    {
        GameObject target = RequireObject(objectName);
        SpriteRenderer renderer = target.GetComponent<SpriteRenderer>();
        if (renderer == null || renderer.sprite == null)
        {
            throw new Exception($"{objectName} is missing a SpriteRenderer sprite.");
        }

        return renderer;
    }

    private static void RequireSpritePathPrefix(string objectName, string expectedPrefix)
    {
        SpriteRenderer renderer = RequireSpriteRenderer(objectName);
        string spritePath = AssetDatabase.GetAssetPath(renderer.sprite);
        if (!spritePath.StartsWith(expectedPrefix, StringComparison.Ordinal))
        {
            throw new Exception($"{objectName} should use a sprite under {expectedPrefix}. Current sprite: {spritePath}");
        }
    }

    private static void RequireSceneObjectNameContains(string fragment, int minimum)
    {
        int count = AllSceneObjects().Count(obj => obj.name.IndexOf(fragment, StringComparison.Ordinal) >= 0);
        if (count < minimum)
        {
            throw new Exception($"Expected at least {minimum} scene objects containing '{fragment}', found {count}.");
        }
    }

    private static void RequireMinimum<T>(int minimum) where T : Component
    {
        int count = UnityEngine.Object.FindObjectsOfType<T>(true).Length;
        if (count < minimum)
        {
            throw new Exception($"Expected at least {minimum} components of type {typeof(T).Name}, found {count}.");
        }
    }

    private static void RequireNear(Vector3 actual, Vector2 expected, float tolerance, string label)
    {
        RequireNear(new Vector2(actual.x, actual.y), expected, tolerance, label);
    }

    private static void RequireNear(Vector2 actual, Vector2 expected, float tolerance, string label)
    {
        if (Vector2.Distance(actual, expected) > tolerance)
        {
            throw new Exception($"{label} expected near {expected}, found {actual}.");
        }
    }

    private static Vector2 GetSerializedVector2(UnityEngine.Object target, string propertyName)
    {
        SerializedObject serializedObject = new SerializedObject(target);
        SerializedProperty property = serializedObject.FindProperty(propertyName);
        if (property == null)
        {
            throw new Exception($"{target.name} is missing serialized Vector2 field {propertyName}.");
        }

        return property.vector2Value;
    }

    private static void RequireSerializedVector2(Component component, string propertyName, Vector2 expectedValue, string label)
    {
        if (component == null)
        {
            throw new Exception($"{label} is missing its required component.");
        }

        SerializedObject serializedObject = new SerializedObject(component);
        SerializedProperty property = serializedObject.FindProperty(propertyName);
        if (property == null || property.propertyType != SerializedPropertyType.Vector2)
        {
            throw new Exception($"{label} is missing serialized Vector2 field {propertyName}.");
        }

        RequireNear(property.vector2Value, expectedValue, 0.001f, label);
    }

    private static void RequireNoForegroundMotion(GameObject root)
    {
        if (root.GetComponentInChildren<SwayingDecor2D>(true) != null ||
            root.GetComponentInChildren<AmbientDrift2D>(true) != null ||
            root.GetComponentInChildren<LoopingBackgroundMotion2D>(true) != null ||
            root.GetComponentInChildren<SteamPuff2D>(true) != null)
        {
            throw new Exception($"{root.name} must stay visually fixed; use flicker only for foreground lamps/decor.");
        }
    }

    private static void RequireSerializedObjectReference<T>(string objectName, string propertyName) where T : Component
    {
        GameObject target = RequireObject(objectName);
        T component = target.GetComponent<T>();
        if (component == null)
        {
            throw new Exception($"{objectName} is missing component {typeof(T).Name}.");
        }

        SerializedObject serializedObject = new SerializedObject(component);
        SerializedProperty property = serializedObject.FindProperty(propertyName);
        if (property == null || property.objectReferenceValue == null)
        {
            throw new Exception($"{objectName}.{propertyName} must reference a generated scene object.");
        }
    }

    private static void RequireSerializedArrayMinimum<T>(string objectName, string propertyName, int minimumSize) where T : Component
    {
        GameObject target = RequireObject(objectName);
        T component = target.GetComponent<T>();
        if (component == null)
        {
            throw new Exception($"{objectName} is missing component {typeof(T).Name}.");
        }

        SerializedObject serializedObject = new SerializedObject(component);
        SerializedProperty property = serializedObject.FindProperty(propertyName);
        if (property == null || !property.isArray || property.arraySize < minimumSize)
        {
            throw new Exception($"{objectName}.{propertyName} must include at least {minimumSize} references.");
        }

        for (int i = 0; i < property.arraySize; i++)
        {
            if (property.GetArrayElementAtIndex(i).objectReferenceValue == null)
            {
                throw new Exception($"{objectName}.{propertyName}[{i}] must not be null.");
            }
        }
    }

    private static void RequireSerializedDoorMode(string objectName, DoorUnlockMode expectedMode)
    {
        GameObject target = RequireObject(objectName);
        DoorLock door = target.GetComponent<DoorLock>();
        if (door == null)
        {
            throw new Exception($"{objectName} is missing DoorLock.");
        }

        SerializedObject serializedObject = new SerializedObject(door);
        SerializedProperty property = serializedObject.FindProperty("unlockMode");
        if (property == null || property.enumValueIndex != (int)expectedMode)
        {
            throw new Exception($"{objectName}.unlockMode must be {expectedMode}.");
        }
    }

    private static void RequireSerializedWatchedEnemy(string doorName, string watchedEnemyName)
    {
        GameObject doorObject = RequireObject(doorName);
        DoorLock door = doorObject.GetComponent<DoorLock>();
        GameObject enemyObject = RequireObject(watchedEnemyName);
        Health enemyHealth = enemyObject.GetComponent<Health>();
        if (door == null || enemyHealth == null)
        {
            throw new Exception($"{doorName} must watch {watchedEnemyName}'s Health component.");
        }

        SerializedObject serializedObject = new SerializedObject(door);
        SerializedProperty property = serializedObject.FindProperty("watchedEnemies");
        if (property == null || !property.isArray || property.arraySize == 0)
        {
            throw new Exception($"{doorName}.watchedEnemies must include {watchedEnemyName}.");
        }

        for (int i = 0; i < property.arraySize; i++)
        {
            if (property.GetArrayElementAtIndex(i).objectReferenceValue == enemyHealth)
            {
                return;
            }
        }

        throw new Exception($"{doorName}.watchedEnemies does not include {watchedEnemyName}.");
    }

    private static void RequireSerializedFloat<T>(string objectName, string propertyName, float expectedValue) where T : Component
    {
        GameObject target = RequireObject(objectName);
        T component = target.GetComponent<T>();
        if (component == null)
        {
            throw new Exception($"{objectName} is missing component {typeof(T).Name}.");
        }

        SerializedObject serializedObject = new SerializedObject(component);
        SerializedProperty property = serializedObject.FindProperty(propertyName);
        if (property == null || Mathf.Abs(property.floatValue - expectedValue) > 0.001f)
        {
            throw new Exception($"{objectName}.{propertyName} must be {expectedValue:0.###}.");
        }
    }

    private static void RequireSerializedInt<T>(string objectName, string propertyName, int expectedValue) where T : Component
    {
        GameObject target = RequireObject(objectName);
        T component = target.GetComponent<T>();
        if (component == null)
        {
            throw new Exception($"{objectName} is missing component {typeof(T).Name}.");
        }

        SerializedObject serializedObject = new SerializedObject(component);
        SerializedProperty property = serializedObject.FindProperty(propertyName);
        if (property == null || property.intValue != expectedValue)
        {
            throw new Exception($"{objectName}.{propertyName} must be {expectedValue}.");
        }
    }

    private static void RequireSerializedBool<T>(string objectName, string propertyName, bool expectedValue) where T : Component
    {
        GameObject target = RequireObject(objectName);
        T component = target.GetComponent<T>();
        if (component == null)
        {
            throw new Exception($"{objectName} is missing component {typeof(T).Name}.");
        }

        SerializedObject serializedObject = new SerializedObject(component);
        SerializedProperty property = serializedObject.FindProperty(propertyName);
        if (property == null || property.boolValue != expectedValue)
        {
            throw new Exception($"{objectName}.{propertyName} must be {expectedValue}.");
        }
    }

    private static void RequireSerializedString<T>(string objectName, string propertyName, string expectedValue) where T : Component
    {
        GameObject target = RequireObject(objectName);
        T component = target.GetComponent<T>();
        if (component == null)
        {
            throw new Exception($"{objectName} is missing component {typeof(T).Name}.");
        }

        SerializedObject serializedObject = new SerializedObject(component);
        SerializedProperty property = serializedObject.FindProperty(propertyName);
        if (property == null || property.stringValue != expectedValue)
        {
            throw new Exception($"{objectName}.{propertyName} must be {expectedValue}.");
        }
    }

    private static GameObject FindSceneObject(string name)
    {
        return AllSceneObjects().FirstOrDefault(obj => obj.name == name);
    }

    private static GameObject[] AllSceneObjects()
    {
        return Resources.FindObjectsOfTypeAll<GameObject>()
            .Where(obj => obj.scene.IsValid() && !EditorUtility.IsPersistent(obj))
            .ToArray();
    }
}
