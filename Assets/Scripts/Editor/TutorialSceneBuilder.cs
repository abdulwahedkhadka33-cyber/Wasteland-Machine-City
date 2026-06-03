using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public static class TutorialSceneBuilder
{
    private const string ScenePath = "Assets/Scenes/Tutorial_01_AwakeningCorridor.unity";
    private const string GeneratedArtPath = "Assets/Art/Generated";
    private const string GeneratedBackgroundPath = "Assets/Art/Generated/Backgrounds";
    private const string GeneratedBackgroundV2Path = "Assets/Art/Generated/Backgrounds/V2";
    private const string GeneratedBackgroundV3Path = "Assets/Art/Generated/Backgrounds/V3";
    private const string GeneratedBackgroundV4Path = "Assets/Art/Generated/Backgrounds/V4";
    private const string GeneratedBackgroundV5Path = "Assets/Art/Generated/Backgrounds/V5";
    private const string GeneratedBackgroundV6Path = "Assets/Art/Generated/Backgrounds/V6";
    private const string GeneratedBackgroundV7Path = "Assets/Art/Generated/Backgrounds/V7";
    private const string GeneratedBackgroundV8Path = "Assets/Art/Generated/Backgrounds/V8";
    private const string GeneratedBackgroundV15Path = "Assets/Art/Generated/Backgrounds/V15";
    private const string GeneratedEnvironmentPath = "Assets/Art/Generated/Environment";
    private const string GeneratedEnvironmentV1Path = "Assets/Art/Generated/Environment/V1";
    private const string GeneratedEnvironmentV2Path = "Assets/Art/Generated/Environment/V2";
    private const string GeneratedEnvironmentV3Path = "Assets/Art/Generated/Environment/V3";
    private const string GeneratedEnvironmentV4Path = "Assets/Art/Generated/Environment/V4";
    private const string GeneratedEnvironmentV5Path = "Assets/Art/Generated/Environment/V5";
    private const string GeneratedEnvironmentV7Path = "Assets/Art/Generated/Environment/V7";
    private const string GeneratedEnvironmentV8Path = "Assets/Art/Generated/Environment/V8";
    private const string GeneratedEnvironmentV9Path = "Assets/Art/Generated/Environment/V9";
    private const string GeneratedEnvironmentV10Path = "Assets/Art/Generated/Environment/V10";
    private const string GeneratedEnvironmentV11Path = "Assets/Art/Generated/Environment/V11";
    private const string GeneratedEnvironmentV12Path = "Assets/Art/Generated/Environment/V12";
    private const string GeneratedEnvironmentV19Path = "Assets/Art/Generated/Environment/V19";
    private const string GeneratedEnvironmentV20Path = "Assets/Art/Generated/Environment/V20";
    private const string GeneratedEffectsPath = "Assets/Art/Generated/Effects";
    private const string GeneratedEffectsV1Path = "Assets/Art/Generated/Effects/V1";
    private const string GeneratedEffectsV2Path = "Assets/Art/Generated/Effects/V2";
    private const string GeneratedEffectsV3Path = "Assets/Art/Generated/Effects/V3";
    private const string GeneratedEffectsV4Path = "Assets/Art/Generated/Effects/V4";
    private const string GeneratedEffectsV5Path = "Assets/Art/Generated/Effects/V5";
    private const string GeneratedEffectsV7Path = "Assets/Art/Generated/Effects/V7";
    private const string GeneratedEnemiesPath = "Assets/Art/Generated/Enemies";
    private const string GeneratedEnemiesV1Path = "Assets/Art/Generated/Enemies/V1";
    private const string GeneratedEnemiesV2Path = "Assets/Art/Generated/Enemies/V2";
    private const string GeneratedEnemiesV3Path = "Assets/Art/Generated/Enemies/V3";
    private const string GeneratedEnemiesV4Path = "Assets/Art/Generated/Enemies/V4";
    private const string ProvidedEnvironmentPath = "Assets/Art/Provided/Environment";
    private const string ProvidedEnvironmentV1Path = "Assets/Art/Provided/Environment/V1";
    private const string ProvidedEnvironmentV2Path = "Assets/Art/Provided/Environment/V2";
    private const string ProvidedEnvironmentV3Path = "Assets/Art/Provided/Environment/V3";
    private const string ProvidedEnvironmentV4Path = "Assets/Art/Provided/Environment/V4";
    private const int BackgroundV6PixelsPerUnit = 96;
    private const int BackgroundV7PixelsPerUnit = 96;
    private const int BackgroundV8PixelsPerUnit = 96;
    private const int BackgroundV15PixelsPerUnit = 96;
    private const int EnvironmentPixelsPerUnit = 128;
    private const int ProvidedEnvironmentV2PixelsPerUnit = 256;
    private const int ProvidedEnvironmentV3PixelsPerUnit = 256;
    private const int ProvidedEnvironmentV4PixelsPerUnit = 256;
    private const int EnvironmentV2PixelsPerUnit = 256;
    private const int EnvironmentV3PixelsPerUnit = 96;
    private const int EnvironmentV4PixelsPerUnit = 128;
    private const int EnvironmentV5PixelsPerUnit = 256;
    private const int EnvironmentV7PixelsPerUnit = 256;
    private const int EnvironmentV8PixelsPerUnit = 256;
    private const int EnvironmentV9PixelsPerUnit = 256;
    private const int EnvironmentV10PixelsPerUnit = 256;
    private const int EnvironmentV11PixelsPerUnit = 256;
    private const int EnvironmentV12PixelsPerUnit = 256;
    private const int EnvironmentV19PixelsPerUnit = 256;
    private const int EnvironmentV20PixelsPerUnit = 256;
    private const int EffectsV1PixelsPerUnit = 256;
    private const int EffectsV2PixelsPerUnit = 256;
    private const int EffectsV3PixelsPerUnit = 256;
    private const int EffectsV4PixelsPerUnit = 256;
    private const int EffectsV5PixelsPerUnit = 256;
    private const int EffectsV7PixelsPerUnit = 256;
    private const int EnemyV1PixelsPerUnit = 256;
    private const int EnemyV2PixelsPerUnit = 256;
    private const int EnemyV3PixelsPerUnit = 256;
    private const int EnemyV4PixelsPerUnit = 256;
    private const float BackgroundV6MinX = -12f;
    private const float BackgroundV6CenterY = 0.7f;
    private const float BackgroundV6ChunkWorldWidth = 42.666667f;
    private const float BackgroundV6OverlapWorld = 6f;
    private const float BackgroundV7MinX = -12f;
    private const float BackgroundV7CenterY = 0.7f;
    private const float BackgroundV7ChunkWorldWidth = 42.666667f;
    private const float BackgroundV7OverlapWorld = 5f;
    private const float BackgroundV8MinX = -12f;
    private const float BackgroundV8CenterY = 0.7f;
    private const float BackgroundV8ChunkWorldWidth = 42.666667f;
    private const float BackgroundV8OverlapWorld = 5f;
    private const float BackgroundV15MinX = -12f;
    private const float BackgroundV15CenterY = 0.7f;
    private const float BackgroundV15ChunkWorldWidth = 42.666667f;
    private const float BackgroundV15OverlapWorld = 5f;
    private const string GeneratedRobotPath = "Assets/Art/Generated/Characters/PlayerRobot";
    private const string GeneratedRobotV2Path = "Assets/Art/Generated/Characters/PlayerRobot/V2";
    private const string GeneratedRobotV3Path = "Assets/Art/Generated/Characters/PlayerRobot/V3";
    private const string RobotReferencePath = "Assets/Art/Reference/RobotReference.png";
    private static Sprite whiteSprite;

    private enum PlatformVisualType
    {
        MainFloor,
        BlockStep,
        LowObstacle,
        ThinFloatingDeck,
        ThinJumpDeck,
    }

    private static readonly string[] RobotPartFiles =
    {
        "boxbot_full_reference_v3.png",
        "boxbot_back_arm.png",
        "boxbot_back_leg.png",
        "boxbot_body.png",
        "boxbot_cable_tail.png",
        "boxbot_eyes.png",
        "boxbot_front_arm.png",
        "boxbot_front_leg.png",
    };

    private static readonly string[] BackgroundV6Files =
    {
        "BackgroundPanorama_TutorialCorridor_v6_01.png",
        "BackgroundPanorama_TutorialCorridor_v6_02.png",
        "BackgroundPanorama_TutorialCorridor_v6_03.png",
        "BackgroundPanorama_TutorialCorridor_v6_04.png",
        "BackgroundPanorama_TutorialCorridor_v6_05.png",
    };

    private static readonly string[] BackgroundV7Files =
    {
        "BackgroundPanorama_RustRepairStation_v7_01.png",
        "BackgroundPanorama_RustRepairStation_v7_02.png",
        "BackgroundPanorama_RustRepairStation_v7_03.png",
        "BackgroundPanorama_RustRepairStation_v7_04.png",
        "BackgroundPanorama_RustRepairStation_v7_05.png",
    };

    private static readonly string[] BackgroundV8Files =
    {
        "BackgroundPanorama_RustRepairStation_v8_01.png",
        "BackgroundPanorama_RustRepairStation_v8_02.png",
        "BackgroundPanorama_RustRepairStation_v8_03.png",
        "BackgroundPanorama_RustRepairStation_v8_04.png",
        "BackgroundPanorama_RustRepairStation_v8_05.png",
    };

    private static readonly string[] BackgroundV15Files =
    {
        "BackgroundPanorama_RustRepairStation_v15_01.png",
        "BackgroundPanorama_RustRepairStation_v15_02.png",
        "BackgroundPanorama_RustRepairStation_v15_03.png",
        "BackgroundPanorama_RustRepairStation_v15_04.png",
        "BackgroundPanorama_RustRepairStation_v15_05.png",
    };

    private static readonly string[] EnvironmentSpriteFiles =
    {
        "env_platform_panel.png",
        "env_platform_rust_edge.png",
        "env_platform_underbeam.png",
        "env_platform_grate.png",
        "env_rivet.png",
        "env_crack.png",
        "env_oil_stain.png",
        "env_pipe_straight.png",
        "env_pipe_flange.png",
        "env_pipe_clamp.png",
        "env_valve_wheel.png",
        "env_cable_bundle.png",
        "env_steam_vent.png",
        "env_spark.png",
        "env_dust_puff.png",
    };

    private static readonly string[] ProvidedEnvironmentSpriteFiles =
    {
        "decor_amber_tube_light.png",
        "decor_valve_pipe.png",
        "decor_hanging_chain.png",
        "decor_pipe_straight.png",
        "decor_pipe_elbow.png",
        "decor_lamp_post.png",
        "decor_hanging_lantern.png",
        "decor_hook_chain.png",
        "decor_storage_crate.png",
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

    private static readonly string[] EnvironmentV2SpriteFiles =
    {
        "envv2_pipe_straight_hd.png",
        "envv2_tube_light_hd.png",
        "envv2_valve_pipe_hd.png",
        "envv2_pipe_elbow_hd.png",
        "envv2_hanging_chain_hd.png",
        "envv2_hanging_lantern_hd.png",
        "envv2_storage_crate_hd.png",
        "envv2_cable_bundle_hd.png",
    };

    private static readonly string[] EnvironmentV3SpriteFiles =
    {
        "envv3_soft_glow_round_amber.png",
        "envv3_tube_glow_amber.png",
        "envv3_indicator_pill_amber.png",
        "envv3_status_lamp_round.png",
        "envv3_electric_arc_01.png",
        "envv3_electric_arc_02.png",
        "envv3_electric_arc_03.png",
        "envv3_dust_wisp_wide.png",
        "envv3_scan_beam_amber.png",
        "envv3_backwall_panel_clean.png",
        "envv3_pulley_wheel.png",
        "envv3_repair_chip_pickup.png",
    };

    private static readonly string[] EnvironmentV4SpriteFiles =
    {
        "warning_lamp_red.png",
        "status_lamp_red_wall.png",
        "repair_bed_rusty.png",
        "repair_mechanical_arm.png",
        "charging_station_green.png",
        "electric_floor_panel_blue.png",
        "compressor_plate_hazard.png",
        "boss_gate_repair_hall.png",
        "mech_city_exit_gate.png",
        "robot_corpse_small.png",
    };

    private static readonly string[] EnemyV1SpriteFiles =
    {
        "enemy_patrol_repair_bot.png",
        "boss_repair_station_guardian.png",
    };

    private static readonly string[] EnvironmentV5SpriteFiles =
    {
        "envv5_broken_floor_panel.png",
        "envv5_platform_edge_rusted.png",
        "envv5_railing_segment.png",
        "envv5_pipe_support_bracket.png",
        "envv5_hanging_chain_detailed.png",
        "envv5_cable_socket.png",
        "envv5_oil_pool_surface.png",
        "envv5_scrap_pile.png",
        "envv5_crate_detailed.png",
        "envv5_mechanical_door_slab.png",
        "envv5_terminal_console.png",
        "envv5_boss_sweep_arm.png",
        "envv5_warning_sign.png",
        "envv5_gear_cluster.png",
        "envv5_hanging_cable_bundle.png",
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

    private static readonly string[] EffectsV1SpriteFiles =
    {
        "fx_attack_slash_01.png",
        "fx_attack_slash_02.png",
        "fx_attack_slash_03.png",
        "fx_hit_sparks.png",
        "fx_run_dust.png",
        "fx_jump_sparks.png",
        "fx_land_dust.png",
        "fx_electric_zap.png",
        "fx_smash_warning_ring.png",
        "fx_smoke_puff.png",
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

    private static readonly string[] EnemyV2SpriteFiles =
    {
        "enemyv2_patrol_repair_bot.png",
        "enemyv2_patrol_repair_bot_eye.png",
        "enemyv2_repair_bot_wreck.png",
        "bossv2_guardian_body.png",
        "bossv2_guardian_eye.png",
    };

    private static readonly string[] EnemyV3SpriteFiles =
    {
        "bossv3_guardian_refined_overlay.png",
    };

    private static readonly string[] EnemyV4SpriteFiles =
    {
        "bossv4_guardian_overload_overlay.png",
    };

    [MenuItem("Tools/Wasteland Mech City/Build Tutorial Scene")]
    public static void BuildTutorialScene()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            EditorApplication.isPlaying = false;
            EditorApplication.delayCall += BuildTutorialScene;
            Debug.Log("Exiting Play Mode before rebuilding the tutorial scene.");
            return;
        }

        EnsureProjectFolders();
        AssetDatabase.Refresh();
        ConfigureBuiltIn2DRenderer();
        ConfigureImportedSprite(RobotReferencePath, 100);
        foreach (string backgroundPath in GetBackgroundV15Paths())
        {
            ConfigureImportedSprite(backgroundPath, BackgroundV15PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string environmentPath in GetEnvironmentSpritePaths())
        {
            ConfigureImportedSprite(environmentPath, EnvironmentPixelsPerUnit, 2048);
        }

        foreach (string providedEnvironmentPath in GetProvidedEnvironmentSpritePaths())
        {
            ConfigureImportedSprite(providedEnvironmentPath, EnvironmentPixelsPerUnit, 2048);
        }

        foreach (string providedEnvironmentV2Path in GetProvidedEnvironmentV2SpritePaths())
        {
            ConfigureImportedSprite(providedEnvironmentV2Path, ProvidedEnvironmentV2PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string providedEnvironmentV3Path in GetProvidedEnvironmentV3SpritePaths())
        {
            ConfigureImportedSprite(providedEnvironmentV3Path, ProvidedEnvironmentV3PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string providedEnvironmentV4Path in GetProvidedEnvironmentV4SpritePaths())
        {
            ConfigureImportedSprite(providedEnvironmentV4Path, ProvidedEnvironmentV4PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string environmentV2Path in GetEnvironmentV2SpritePaths())
        {
            ConfigureImportedSprite(environmentV2Path, EnvironmentV2PixelsPerUnit, 4096, FilterMode.Point);
        }

        foreach (string environmentV3Path in GetEnvironmentV3SpritePaths())
        {
            ConfigureImportedSprite(environmentV3Path, EnvironmentV3PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string environmentV4Path in GetEnvironmentV4SpritePaths())
        {
            ConfigureImportedSprite(environmentV4Path, EnvironmentV4PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string environmentV5Path in GetEnvironmentV5SpritePaths())
        {
            ConfigureImportedSprite(environmentV5Path, EnvironmentV5PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string environmentV7Path in GetEnvironmentV7SpritePaths())
        {
            ConfigureImportedSprite(environmentV7Path, EnvironmentV7PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string environmentV8Path in GetEnvironmentV8SpritePaths())
        {
            ConfigureImportedSprite(environmentV8Path, EnvironmentV8PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string environmentV9Path in GetEnvironmentV9SpritePaths())
        {
            ConfigureImportedSprite(environmentV9Path, EnvironmentV9PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string environmentV10Path in GetEnvironmentV10SpritePaths())
        {
            ConfigureImportedSprite(environmentV10Path, EnvironmentV10PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string environmentV11Path in GetEnvironmentV11SpritePaths())
        {
            ConfigureImportedSprite(environmentV11Path, EnvironmentV11PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string environmentV12Path in GetEnvironmentV12SpritePaths())
        {
            ConfigureImportedSprite(environmentV12Path, EnvironmentV12PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string environmentV19Path in GetEnvironmentV19SpritePaths())
        {
            ConfigureImportedSprite(environmentV19Path, EnvironmentV19PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string environmentV20Path in GetEnvironmentV20SpritePaths())
        {
            ConfigureImportedSprite(environmentV20Path, EnvironmentV20PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string effectV1Path in GetEffectsV1SpritePaths())
        {
            ConfigureImportedSprite(effectV1Path, EffectsV1PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string effectV2Path in GetEffectsV2SpritePaths())
        {
            ConfigureImportedSprite(effectV2Path, EffectsV2PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string effectV3Path in GetEffectsV3SpritePaths())
        {
            ConfigureImportedSprite(effectV3Path, EffectsV3PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string effectV4Path in GetEffectsV4SpritePaths())
        {
            ConfigureImportedSprite(effectV4Path, EffectsV4PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string effectV5Path in GetEffectsV5SpritePaths())
        {
            ConfigureImportedSprite(effectV5Path, EffectsV5PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string effectV7Path in GetEffectsV7SpritePaths())
        {
            ConfigureImportedSprite(effectV7Path, EffectsV7PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string enemyV2Path in GetEnemyV2SpritePaths())
        {
            ConfigureImportedSprite(enemyV2Path, EnemyV2PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string enemyV3Path in GetEnemyV3SpritePaths())
        {
            ConfigureImportedSprite(enemyV3Path, EnemyV3PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string enemyV4Path in GetEnemyV4SpritePaths())
        {
            ConfigureImportedSprite(enemyV4Path, EnemyV4PixelsPerUnit, 4096, FilterMode.Bilinear);
        }

        foreach (string robotPartPath in GetRobotPartPaths())
        {
            ConfigureImportedSprite(robotPartPath, 512);
        }

        whiteSprite = GetOrCreateSolidSprite("white_pixel", Color.white);

        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        scene.name = "Tutorial_01_AwakeningCorridor";

        GameObject roots = new GameObject("Tutorial_01_AwakeningCorridor");
        GameObject backgroundRoot = NewChild(roots.transform, "00_Background");
        GameObject geometryRoot = NewChild(roots.transform, "01_Level_Geometry");
        GameObject gameplayRoot = NewChild(roots.transform, "02_Gameplay");
        GameObject tutorialRoot = NewChild(roots.transform, "03_Tutorial_Triggers");
        GameObject uiRoot = NewChild(roots.transform, "04_UI");
        GameObject detailRoot = NewChild(roots.transform, "05_Environmental_Details");

        GameObject player = CreatePlayer(gameplayRoot.transform);
        CreateCamera(player.transform);
        CreateLightingMood();
        CreateBackground(backgroundRoot.transform);
        CreateGeometry(geometryRoot.transform);
        GameObject awakeningBenchAssembly = CreateEnvironmentalDetails(detailRoot.transform);
        CreateSpawnIntro(detailRoot.transform, player, awakeningBenchAssembly != null ? awakeningBenchAssembly.transform : null);
        CreateTutorialTriggers(tutorialRoot.transform);

        ChipData repairChip = CreateRepairChipAsset();
        Health sentinelHealth = CreateGameplayObjects(gameplayRoot.transform, repairChip);
        CreateRespawnPolish(detailRoot.transform, player);
        CreateUi(uiRoot.transform);
        CreateFinalDoorWatcher(sentinelHealth);

        Selection.activeObject = player;
        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene, ScenePath);
        EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(ScenePath, true) };
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Built playable tutorial scene at {ScenePath}");
    }

    private static void EnsureProjectFolders()
    {
        EnsureFolder("Assets/Art");
        EnsureFolder("Assets/Art/Reference");
        EnsureFolder(GeneratedArtPath);
        EnsureFolder(GeneratedBackgroundPath);
        EnsureFolder(GeneratedBackgroundV8Path);
        EnsureFolder(GeneratedBackgroundV15Path);
        EnsureFolder(GeneratedEnvironmentPath);
        EnsureFolder(GeneratedEnvironmentV1Path);
        EnsureFolder(GeneratedEnvironmentV2Path);
        EnsureFolder(GeneratedEnvironmentV3Path);
        EnsureFolder(GeneratedEnvironmentV4Path);
        EnsureFolder(GeneratedEnvironmentV5Path);
        EnsureFolder(GeneratedEnvironmentV7Path);
        EnsureFolder(GeneratedEnvironmentV8Path);
        EnsureFolder(GeneratedEnvironmentV9Path);
        EnsureFolder(GeneratedEnvironmentV10Path);
        EnsureFolder(GeneratedEnvironmentV11Path);
        EnsureFolder(GeneratedEnvironmentV12Path);
        EnsureFolder(GeneratedEnvironmentV19Path);
        EnsureFolder(GeneratedEnvironmentV20Path);
        EnsureFolder(GeneratedEffectsPath);
        EnsureFolder(GeneratedEffectsV1Path);
        EnsureFolder(GeneratedEffectsV2Path);
        EnsureFolder(GeneratedEffectsV3Path);
        EnsureFolder(GeneratedEffectsV4Path);
        EnsureFolder(GeneratedEffectsV5Path);
        EnsureFolder(GeneratedEffectsV7Path);
        EnsureFolder(GeneratedEnemiesPath);
        EnsureFolder(GeneratedEnemiesV2Path);
        EnsureFolder(GeneratedEnemiesV3Path);
        EnsureFolder(GeneratedEnemiesV4Path);
        EnsureFolder("Assets/Art/Provided");
        EnsureFolder("Assets/Art/Provided/Environment");
        EnsureFolder(ProvidedEnvironmentPath);
        EnsureFolder(ProvidedEnvironmentV1Path);
        EnsureFolder(ProvidedEnvironmentV2Path);
        EnsureFolder(ProvidedEnvironmentV3Path);
        EnsureFolder(ProvidedEnvironmentV4Path);
        EnsureFolder("Assets/Art/Generated/Characters");
        EnsureFolder(GeneratedRobotPath);
        EnsureFolder(GeneratedRobotV3Path);
        EnsureFolder("Assets/Data");
        EnsureFolder("Assets/Scenes");
        EnsureFolder("Assets/Settings");
        EnsureFolder("Assets/Docs");
    }

    private static void ConfigureBuiltIn2DRenderer()
    {
        GraphicsSettings.renderPipelineAsset = null;
        QualitySettings.renderPipeline = null;
    }

    private static GameObject CreatePlayer(Transform parent)
    {
        GameObject player = new GameObject("Player_SmallAmnesiacRobot");
        player.transform.SetParent(parent);
        player.transform.position = new Vector3(2f, -1.85f, 0f);
        player.layer = 2;

        Rigidbody2D body = player.AddComponent<Rigidbody2D>();
        body.gravityScale = 3.4f;
        body.freezeRotation = true;
        body.interpolation = RigidbodyInterpolation2D.Interpolate;

        BoxCollider2D collider = player.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(0.8f, 1.35f);
        collider.offset = new Vector2(0f, -0.03f);

        Health health = player.AddComponent<Health>();
        health.DisableOnDeath = false;
        player.AddComponent<HitFlash2D>();
        player.AddComponent<PlayerChipInventory>();
        PlayerController2D controller = player.AddComponent<PlayerController2D>();
        SetFloat(controller, "walkSpeed", 4.1f);
        SetFloat(controller, "runSpeed", 7.2f);
        SetFloat(controller, "groundAcceleration", 44f);
        SetFloat(controller, "groundDeceleration", 58f);
        SetFloat(controller, "airAcceleration", 30f);

        GameObject visual = NewChild(player.transform, "Visual_PlayerRobot_Parts");
        visual.transform.localPosition = Vector3.zero;
        GameObject backArm = AddRobotSpritePart(visual.transform, "Boxbot_BackArm", "boxbot_back_arm.png", new Vector2(-0.29f, -0.22f), 0.33f, false, 20);
        GameObject backLeg = AddRobotSpritePart(visual.transform, "Boxbot_BackLeg", "boxbot_back_leg.png", new Vector2(-0.13f, -0.56f), 0.31f, false, 21);
        GameObject boxBody = AddRobotSpritePart(visual.transform, "Boxbot_Body", "boxbot_body.png", new Vector2(0f, -0.04f), 0.67f, false, 22);
        GameObject frontLeg = AddRobotSpritePart(visual.transform, "Boxbot_FrontLeg", "boxbot_front_leg.png", new Vector2(0.16f, -0.56f), 0.32f, false, 23);
        GameObject frontArm = AddRobotSpritePart(visual.transform, "Boxbot_FrontArm", "boxbot_front_arm.png", new Vector2(0.32f, -0.22f), 0.34f, false, 24);
        GameObject cableTail = AddRobotSpritePart(visual.transform, "Boxbot_CableTail", "boxbot_cable_tail.png", new Vector2(-0.36f, -0.05f), 0.28f, true, 19);
        GameObject eyeLight = AddRobotSpritePart(visual.transform, "Boxbot_Eyes", "boxbot_eyes.png", new Vector2(0.11f, 0.08f), 0.22f, true, 29);
        TintSpriteRenderer(eyeLight, new Color(1f, 0.8f, 0.38f, 1f));
        eyeLight.AddComponent<SpriteFlicker2D>();
        AddRobotRimPart(backArm, "Boxbot_Rim_BackArm", 1.08f, 0.2f);
        AddRobotRimPart(backLeg, "Boxbot_Rim_BackLeg", 1.08f, 0.2f);
        AddRobotRimPart(boxBody, "Boxbot_Rim_Body", 1.07f, 0.24f);
        AddRobotRimPart(frontLeg, "Boxbot_Rim_FrontLeg", 1.08f, 0.22f);
        AddRobotRimPart(frontArm, "Boxbot_Rim_FrontArm", 1.08f, 0.22f);
        AddRobotRimPart(cableTail, "Boxbot_Rim_CableTail", 1.1f, 0.18f);
        GameObject antennaStem = AddEnvironmentV7SpriteChild(visual.transform, "Boxbot_AntennaStem", "envv7_cable_bundle.png", new Vector2(0.02f, 0.48f), new Vector2(0.035f, 0.32f), new Color(0.32f, 0.25f, 0.16f, 0.96f), 30);
        GameObject antennaLamp = AddEnvironmentV7SpriteChild(visual.transform, "Boxbot_AntennaLamp", "envv7_indicator_lamp_amber.png", new Vector2(0.02f, 0.66f), new Vector2(0.13f, 0.13f), new Color(1f, 0.62f, 0.14f, 0.96f), 31);
        antennaLamp.AddComponent<SpriteFlicker2D>();
        GameObject groundShadow = AddEnvironmentSpriteChild(player.transform, "Boxbot_GroundShadow", "env_dust_puff.png", new Vector2(0f, -0.82f), new Vector2(1.05f, 0.24f), new Color(0.025f, 0.018f, 0.012f, 0.34f), 17);
        groundShadow.transform.localRotation = Quaternion.Euler(0f, 0f, 2f);

        Transform groundCheck = NewChild(player.transform, "GroundCheck").transform;
        groundCheck.localPosition = new Vector3(0f, -0.78f, 0f);
        Transform attackPoint = NewChild(player.transform, "AttackPoint").transform;
        attackPoint.localPosition = new Vector3(0.16f, -0.02f, 0f);
        GameObject attackVisualRoot = NewChild(player.transform, "AttackVisualRoot");
        attackVisualRoot.transform.localPosition = new Vector3(0.78f, 0.02f, 0f);
        SpriteRenderer combo1Arc = AddEffectsV7SpriteChild(attackVisualRoot.transform, "AttackCombo1_AmberArc", "attack_combo_01_arc.png", new Vector2(0.14f, 0f), new Vector2(1.36f, 0.62f), new Color(1f, 0.86f, 0.42f, 0f), 31).GetComponent<SpriteRenderer>();
        GameObject combo2Object = AddEffectsV7SpriteChild(attackVisualRoot.transform, "AttackCombo2_UpperArc", "attack_combo_02_arc.png", new Vector2(0.28f, 0.16f), new Vector2(1.72f, 0.88f), new Color(1f, 0.76f, 0.24f, 0f), 32);
        combo2Object.transform.localRotation = Quaternion.Euler(0f, 0f, 22f);
        SpriteRenderer combo2Arc = combo2Object.GetComponent<SpriteRenderer>();
        GameObject combo3Object = AddEffectsV7SpriteChild(attackVisualRoot.transform, "AttackCombo3_HeavyCleave", "attack_combo_03_arc.png", new Vector2(0.48f, -0.02f), new Vector2(2.08f, 1.08f), new Color(1f, 0.62f, 0.12f, 0f), 33);
        combo3Object.transform.localRotation = Quaternion.Euler(0f, 0f, -12f);
        SpriteRenderer combo3Arc = combo3Object.GetComponent<SpriteRenderer>();
        GameObject airSlashObject = AddEffectsV7SpriteChild(attackVisualRoot.transform, "AttackAirSlash_DownArc", "attack_air_slash_arc.png", new Vector2(0.34f, -0.2f), new Vector2(1.78f, 1.08f), new Color(1f, 0.72f, 0.22f, 0f), 32);
        airSlashObject.transform.localRotation = Quaternion.Euler(0f, 0f, -34f);
        SpriteRenderer airSlashArc = airSlashObject.GetComponent<SpriteRenderer>();
        SpriteRenderer chargeFlash = AddEffectsV7SpriteChild(attackVisualRoot.transform, "AttackCombo3_ChargeFlash", "attack_charge_flash.png", new Vector2(-0.38f, 0.04f), new Vector2(0.72f, 0.46f), new Color(1f, 0.72f, 0.24f, 0f), 30).GetComponent<SpriteRenderer>();
        AttackSlashAnimator2D attackAnimator = attackVisualRoot.AddComponent<AttackSlashAnimator2D>();
        SetObject(attackAnimator, "combo1Arc", combo1Arc);
        SetObject(attackAnimator, "combo2Arc", combo2Arc);
        SetObject(attackAnimator, "combo3Arc", combo3Arc);
        SetObject(attackAnimator, "airSlashArc", airSlashArc);
        SetObject(attackAnimator, "chargeFlash", chargeFlash);
        SetFloat(attackAnimator, "maxAlpha", 1f);
        SetFloat(attackAnimator, "scalePulse", 0.2f);
        attackVisualRoot.SetActive(false);
        GameObject landingDust = AddEffectsV1SpriteChild(player.transform, "LandingDust_Puff", "fx_land_dust.png", new Vector2(0f, -0.78f), new Vector2(1.45f, 0.54f), new Color(0.86f, 0.78f, 0.62f, 0.34f), 18);
        landingDust.SetActive(false);
        GameObject landingSparks = AddEffectsV1SpriteChild(player.transform, "LandingSparks_Burst", "fx_hit_sparks.png", new Vector2(0.04f, -0.78f), new Vector2(0.72f, 0.58f), new Color(1f, 0.72f, 0.18f, 0.58f), 20);
        landingSparks.SetActive(false);
        GameObject runDust = AddEffectsV1SpriteChild(player.transform, "RunDust_Puff", "fx_run_dust.png", new Vector2(-0.42f, -0.78f), new Vector2(0.84f, 0.32f), new Color(0.84f, 0.78f, 0.62f, 0.26f), 18);
        runDust.SetActive(false);
        GameObject jumpBurst = AddEffectsV1SpriteChild(player.transform, "JumpBurst_Sparks", "fx_jump_sparks.png", new Vector2(-0.08f, -0.78f), new Vector2(0.7f, 0.58f), new Color(1f, 0.62f, 0.14f, 0.48f), 19);
        jumpBurst.SetActive(false);
        GameObject hitSpark = AddEffectsV1SpriteChild(player.transform, "Player_HitSpark_Burst", "fx_hit_sparks.png", new Vector2(0.8f, 0.05f), new Vector2(0.72f, 0.72f), new Color(1f, 0.78f, 0.26f, 0.85f), 34);
        OneShotSpriteBurst2D hitSparkBurst = hitSpark.AddComponent<OneShotSpriteBurst2D>();
        SetVector2(hitSparkBurst, "startScale", new Vector2(0.52f, 0.52f));
        SetVector2(hitSparkBurst, "endScale", new Vector2(1.05f, 1.05f));
        SetFloat(hitSparkBurst, "duration", 0.16f);
        GameObject heavyHitSpark = AddEffectsV7SpriteChild(player.transform, "Player_HitSpark_HeavyBurst", "attack_hit_spark_heavy.png", new Vector2(0.92f, 0.05f), new Vector2(1.12f, 0.68f), new Color(1f, 0.78f, 0.24f, 0.9f), 35);
        OneShotSpriteBurst2D heavyHitSparkBurst = heavyHitSpark.AddComponent<OneShotSpriteBurst2D>();
        SetVector2(heavyHitSparkBurst, "startScale", new Vector2(0.58f, 0.5f));
        SetVector2(heavyHitSparkBurst, "endScale", new Vector2(1.28f, 0.9f));
        SetFloat(heavyHitSparkBurst, "duration", 0.18f);
        SetObject(controller, "groundCheck", groundCheck);
        SetObject(controller, "attackPoint", attackPoint);
        SetObject(controller, "attackVisualRoot", attackVisualRoot);
        SetObject(controller, "attackVisualAnimator", attackAnimator);
        SetObject(controller, "hitSparkVisual", hitSparkBurst);
        SetObject(controller, "heavyHitSparkVisual", heavyHitSparkBurst);
        SetFloat(controller, "attackVisualSeconds", 0.22f);
        SetFloat(controller, "combo1Seconds", 0.22f);
        SetFloat(controller, "combo2Seconds", 0.24f);
        SetFloat(controller, "combo3Seconds", 0.32f);
        SetFloat(controller, "airSlashSeconds", 0.28f);
        SetFloat(controller, "comboInputBufferSeconds", 0.14f);
        SetFloat(controller, "comboContinueWindow", 0.34f);
        SetFloat(controller, "attackRange", 1.32f);
        SetFloat(controller, "attackRangeStartRatio", 0.22f);
        SetFloat(controller, "attackRangeGrowthPower", 1.18f);
        SetFloat(controller, "attackDamageStartTime", 0.12f);
        SetFloat(controller, "attackDamageEndTime", 0.92f);
        SetFloat(controller, "attackRecoveryRangeRatio", 0.82f);
        SetFloat(controller, "attackHitboxForwardOffset", -0.02f);
        SetFloat(controller, "attackHitboxVerticalOffset", 0.02f);
        SetFloat(controller, "attackHitboxHeight", 0.9f);
        SetFloat(controller, "airSlashHitboxVerticalOffset", -0.24f);
        SetFloat(controller, "airSlashHitboxHeight", 1.25f);
        SetFloat(controller, "combo2RangeBonus", 0.38f);
        SetFloat(controller, "combo3RangeBonus", 0.68f);
        SetFloat(controller, "airSlashRangeBonus", 0.43f);
        SetFloat(controller, "combo2HitboxVerticalBonus", 0.18f);
        SetFloat(controller, "combo2HitboxHeightBonus", 0.28f);
        SetFloat(controller, "combo3HitboxHeightBonus", 0.34f);
        SetLayerMask(controller, "groundLayers", LayerMask.GetMask("Default"));

        PlayerRobotVisualAnimator2D visualAnimator = player.AddComponent<PlayerRobotVisualAnimator2D>();
        SetObject(visualAnimator, "body", body);
        SetObject(visualAnimator, "controller", controller);
        SetObject(visualAnimator, "attackSlashVisual", attackVisualRoot);
        SetObject(visualAnimator, "landingDustVisual", landingDust);
        SetObject(visualAnimator, "landingSparkVisual", landingSparks);
        SetObject(visualAnimator, "runDustVisual", runDust);
        SetObject(visualAnimator, "jumpBurstVisual", jumpBurst);
        SetObject(visualAnimator, "visualRoot", visual.transform);
        SetObject(visualAnimator, "boxBody", boxBody.transform);
        SetObject(visualAnimator, "eyes", eyeLight.transform);
        SetObject(visualAnimator, "frontArm", frontArm.transform);
        SetObject(visualAnimator, "backArm", backArm.transform);
        SetObject(visualAnimator, "frontLeg", frontLeg.transform);
        SetObject(visualAnimator, "backLeg", backLeg.transform);
        SetObject(visualAnimator, "cableTail", cableTail.transform);
        SetObject(visualAnimator, "antennaStem", antennaStem.transform);
        SetObject(visualAnimator, "antennaTip", antennaLamp.transform);

        return player;
    }

    private static void CreateCamera(Transform target)
    {
        GameObject cameraObject = new GameObject("Main Camera");
        cameraObject.tag = "MainCamera";
        cameraObject.transform.position = new Vector3(5f, -0.25f, -10f);
        Camera camera = cameraObject.AddComponent<Camera>();
        camera.orthographic = true;
        camera.orthographicSize = 5f;
        camera.backgroundColor = new Color(0.05f, 0.045f, 0.04f);

        CameraFollow2D follow = cameraObject.AddComponent<CameraFollow2D>();
        SetObject(follow, "target", target);
        SetVector2(follow, "offset", new Vector2(2.3f, 0.85f));
        SetVector2(follow, "minBounds", new Vector2(-10f, -5.4f));
        SetVector2(follow, "maxBounds", new Vector2(176.8f, 6.8f));
        SetFloat(follow, "verticalDeadZone", 1.4f);
        SetFloat(follow, "verticalFollowStrength", 0.25f);
        SetFloat(follow, "horizontalLookahead", 1.15f);
        SetFloat(follow, "maxLookaheadSpeed", 7.2f);
        SetFloat(follow, "lookaheadSmoothTime", 0.24f);
    }

    private static void CreateLightingMood()
    {
        RenderSettings.ambientLight = new Color(1f, 0.78f, 0.48f);
        GameObject mood = new GameObject("Lighting_Mood_BuiltIn_Amber");
        mood.transform.position = Vector3.zero;
    }

    private static void CreateBackground(Transform parent)
    {
        GameObject panoramaRoot = NewChild(parent, "BG_PanoramaChunks_V15");
        string[] backgroundPaths = GetBackgroundV15Paths();
        for (int i = 0; i < backgroundPaths.Length; i++)
        {
            Sprite panoramaSprite = AssetDatabase.LoadAssetAtPath<Sprite>(backgroundPaths[i]);
            if (panoramaSprite == null)
            {
                Debug.LogWarning($"Missing generated V15 background chunk sprite: {backgroundPaths[i]}");
                continue;
            }

            float centerX = BackgroundV15MinX + BackgroundV15ChunkWorldWidth * 0.5f + i * (BackgroundV15ChunkWorldWidth - BackgroundV15OverlapWorld);
            string objectName = Path.GetFileNameWithoutExtension(backgroundPaths[i]);
            GameObject panorama = NewChild(panoramaRoot.transform, objectName);
            panorama.transform.position = new Vector3(centerX, BackgroundV15CenterY, 8f);
            panorama.transform.localScale = Vector3.one;
            SpriteRenderer renderer = panorama.AddComponent<SpriteRenderer>();
            renderer.sprite = panoramaSprite;
            renderer.color = new Color(0.74f, 0.82f, 0.76f, 1f);
            renderer.sortingOrder = -140;
        }

        CreateAnimatedBackgroundOverlaysV2(parent);
        CreateCleanDecorV8(parent);
        CreateDecorativePropsV9(parent);
        CreateLayeredDecorV10(parent);
        CreateLayeredDecorV11(parent);
        CreateProvidedLayeredDecorV2(parent);
        CreateProvidedLayeredDecorV3(parent);
        CreateDynamicDecorV13(parent);
        CreateLargeDecorV14(parent);
        CreateMapReadableDecorV15(parent);
        CreateBackgroundAtmosphereV12(parent);
        CreateSystemPolishReadability(parent);
        CreateDynamicPolishV16(parent);

        GameObject silhouettes = NewChild(parent, "BG_Parallax_FarArt_V15");
        ParallaxLayer2D parallax = silhouettes.AddComponent<ParallaxLayer2D>();
        SetVector2(parallax, "parallaxFactor", new Vector2(0.08f, 0.015f));

        float[] farXs = { 5f, 42f, 79f, 116f, 153f };
        for (int i = 0; i < farXs.Length; i++)
        {
            GameObject gear = AddEnvironmentV8SpriteChild(silhouettes.transform, $"FarArt_V15_GearSilhouette_{i + 1:00}", "envv8_far_gear_soft.png", new Vector2(farXs[i], 0.72f + (i % 2) * 0.16f), new Vector2(5.6f, 5.6f), new Color(0.58f, 0.68f, 0.56f, 0.10f), -92);
            gear.transform.rotation = Quaternion.Euler(0f, 0f, i % 2 == 0 ? 0f : -4f);
        }
    }

    private static void CreateAnimatedBackgroundOverlaysV2(Transform parent)
    {
        GameObject layers = NewChild(parent, "BG_AnimatedLayers_V2");

        AddEffectsV2SpriteChild(layers.transform, "DustVeil_V2_Awakening", "fxv2_dust_veil.png", new Vector2(9f, -0.85f), new Vector2(24f, 4.2f), new Color(0.72f, 0.64f, 0.48f, 0.085f), -34).AddComponent<AmbientDrift2D>();
        AddEffectsV2SpriteChild(layers.transform, "DustVeil_V2_Platforms", "fxv2_dust_veil.png", new Vector2(48f, -0.65f), new Vector2(30f, 4.0f), new Color(0.7f, 0.72f, 0.56f, 0.075f), -34).AddComponent<AmbientDrift2D>();
        AddEffectsV2SpriteChild(layers.transform, "DustVeil_V2_TrapHall", "fxv2_dust_veil.png", new Vector2(94f, -0.7f), new Vector2(34f, 4.2f), new Color(0.56f, 0.68f, 0.62f, 0.072f), -34).AddComponent<AmbientDrift2D>();
        AddEffectsV2SpriteChild(layers.transform, "DustVeil_V2_BossHall", "fxv2_dust_veil.png", new Vector2(139f, -0.85f), new Vector2(38f, 4.4f), new Color(0.72f, 0.6f, 0.46f, 0.08f), -34).AddComponent<AmbientDrift2D>();

        AddBackgroundSteamV2(layers.transform, "SteamV2_AwakeningBench", new Vector2(4.9f, -2.22f), -18);
        AddBackgroundSteamV2(layers.transform, "SteamV2_BrokenBridge", new Vector2(51.4f, -2.24f), -18);
        AddBackgroundSteamV2(layers.transform, "SteamV2_TrapCompressor", new Vector2(101.4f, -2.1f), -18);
        AddBackgroundSteamV2(layers.transform, "SteamV2_BossPipeA", new Vector2(131.2f, -1.92f), -18);
        AddBackgroundSteamV2(layers.transform, "SteamV2_BossPipeB", new Vector2(149.7f, -1.88f), -18);

        AddBackgroundRotatorV8(layers.transform, "FanV8_PlatformBackwall", new Vector2(47.2f, 1.65f), 1.7f, -96, 5.5f);
        AddBackgroundRotatorV8(layers.transform, "FanV8_BossBackwall", new Vector2(136.5f, 1.75f), 2.1f, -96, -3.6f);

        AddTransitionShadowV8(layers.transform, "TransitionShadowV8_AwakeningToTutorial", 16f);
        AddTransitionShadowV8(layers.transform, "TransitionShadowV8_PlatformToEnemy", 62f);
        AddTransitionShadowV8(layers.transform, "TransitionShadowV8_TrapToCharge", 106f);
        AddTransitionShadowV8(layers.transform, "TransitionShadowV8_BossToExit", 158f);
        AddSeamBreakerV8(layers.transform, "SeamBreakerV8_Chunk_01", 30.66f, new Color(0.18f, 0.24f, 0.2f, 0.28f));
        AddSeamBreakerV8(layers.transform, "SeamBreakerV8_Chunk_02", 68.33f, new Color(0.16f, 0.22f, 0.2f, 0.25f));
        AddSeamBreakerV8(layers.transform, "SeamBreakerV8_Chunk_03", 105.99f, new Color(0.17f, 0.24f, 0.21f, 0.3f));
        AddSeamBreakerV8(layers.transform, "SeamBreakerV8_Chunk_04", 143.66f, new Color(0.2f, 0.22f, 0.18f, 0.27f));

        AddSparkShowerV2(layers.transform, "SparkV2_AwakeningBrokenArm", new Vector2(6.3f, 1.22f), -12);
        AddSparkShowerV2(layers.transform, "SparkV2_TrapCable", new Vector2(88.2f, 0.62f), -12);
        AddSparkShowerV2(layers.transform, "SparkV2_BossDoor", new Vector2(155.2f, 0.65f), -12);

        AddEffectsV2SpriteChild(layers.transform, "ScanBeamV2_ChargeStation", "fxv2_scan_beam.png", new Vector2(114f, -0.45f), new Vector2(0.5f, 2.55f), new Color(0.4f, 1f, 0.78f, 0.18f), -14).AddComponent<LoopingBackgroundMotion2D>();
        AddEffectsV2SpriteChild(layers.transform, "ScanBeamV2_ExitGate", "fxv2_scan_beam.png", new Vector2(171.2f, -0.36f), new Vector2(0.62f, 3.0f), new Color(0.42f, 0.72f, 1f, 0.18f), -14).AddComponent<LoopingBackgroundMotion2D>();
    }

    private static void CreateCleanDecorV8(Transform parent)
    {
        GameObject root = NewChild(parent, "BG_CleanDecor_V8");

        AddEnvironmentV8SpriteChild(root.transform, "Awakening_V8_TopPipe_Soft", "envv8_top_pipe_soft.png", new Vector2(7.8f, 2.42f), new Vector2(10.6f, 0.72f), new Color(0.72f, 0.67f, 0.54f, 0.28f), -48);
        AddEnvironmentV8SpriteChild(root.transform, "Awakening_V8_ServicePanel_Dim", "envv8_service_panel_dim.png", new Vector2(10.4f, -0.35f), new Vector2(1.8f, 1.8f), new Color(0.66f, 0.72f, 0.6f, 0.22f), -50);

        AddEnvironmentV8SpriteChild(root.transform, "Tutorial_V8_TopPipe_Soft", "envv8_top_pipe_soft.png", new Vector2(25.2f, 2.46f), new Vector2(12.4f, 0.74f), new Color(0.72f, 0.67f, 0.54f, 0.24f), -48);
        AddEnvironmentV8SpriteChild(root.transform, "Jump_V8_FarGear", "envv8_far_gear_soft.png", new Vector2(52.5f, 1.08f), new Vector2(4.7f, 4.7f), new Color(0.62f, 0.7f, 0.58f, 0.16f), -88);
        AddEnvironmentV8SpriteChild(root.transform, "Enemy_V8_ServicePanel_Dim", "envv8_service_panel_dim.png", new Vector2(65.2f, -0.12f), new Vector2(1.7f, 1.7f), new Color(0.64f, 0.68f, 0.56f, 0.18f), -52);

        AddEnvironmentV8SpriteChild(root.transform, "Trap_V8_TopPipe_Soft", "envv8_top_pipe_soft.png", new Vector2(93.2f, 2.55f), new Vector2(14.2f, 0.78f), new Color(0.72f, 0.67f, 0.54f, 0.24f), -48);
        AddEnvironmentV8SpriteChild(root.transform, "Trap_V8_BackMachine_Dim", "envv8_service_panel_dim.png", new Vector2(98.6f, 0.26f), new Vector2(3.1f, 3.1f), new Color(0.62f, 0.66f, 0.54f, 0.18f), -58);
        AddEnvironmentV8SpriteChild(root.transform, "Charge_V8_ServiceWall_Dim", "envv8_service_panel_dim.png", new Vector2(121.4f, 0.42f), new Vector2(1.55f, 1.82f), new Color(0.52f, 0.78f, 0.58f, 0.08f), -70);

        AddEnvironmentV8SpriteChild(root.transform, "Boss_V8_HallPipe_Soft", "envv8_top_pipe_soft.png", new Vector2(141f, 2.42f), new Vector2(24.4f, 0.86f), new Color(0.72f, 0.66f, 0.52f, 0.25f), -48);
        AddEnvironmentV8SpriteChild(root.transform, "Boss_V8_FarGear", "envv8_far_gear_soft.png", new Vector2(139.2f, 1.1f), new Vector2(7.2f, 7.2f), new Color(0.62f, 0.68f, 0.54f, 0.14f), -90);
        AddEnvironmentV8SpriteChild(root.transform, "Exit_V8_TopPipe_Soft", "envv8_top_pipe_soft.png", new Vector2(166.2f, 2.42f), new Vector2(11.8f, 0.74f), new Color(0.72f, 0.66f, 0.52f, 0.23f), -48);
    }

    private static void CreateDecorativePropsV9(Transform parent)
    {
        GameObject root = NewChild(parent, "BG_DecorativeProps_V9");

        AddEnvironmentV9SpriteChild(root.transform, "Awakening_V9_WallSupportBracket", "envv9_wall_support_bracket.png", new Vector2(4.2f, 0.82f), new Vector2(3.3f, 2.05f), new Color(0.72f, 0.66f, 0.52f, 0.34f), -62);
        AddEnvironmentV9SpriteChild(root.transform, "Awakening_V9_ServicePanel", "envv9_service_panel_lit.png", new Vector2(10.2f, 0.08f), new Vector2(1.28f, 1.36f), new Color(0.66f, 0.72f, 0.6f, 0.34f), -54);

        AddEnvironmentV9SpriteChild(root.transform, "Tutorial_V9_ValvePipe_Back", "envv9_valve_pipe_cluster.png", new Vector2(25.3f, 2.18f), new Vector2(7.8f, 1.05f), new Color(0.72f, 0.66f, 0.52f, 0.36f), -56);
        GameObject tutorialCable = AddEnvironmentV9SpriteChild(root.transform, "Tutorial_V9_HangingCable_Sway", "envv9_hanging_cable_bundle.png", new Vector2(20.6f, 1.0f), new Vector2(2.35f, 1.35f), new Color(0.64f, 0.66f, 0.56f, 0.34f), -50);
        AddSway(tutorialCable, 1.8f, 4.8f, new Vector2(0.025f, 0.008f));
        AddEnvironmentV9SpriteChild(root.transform, "Tutorial_V9_GaugeCluster", "envv9_old_gauge_cluster.png", new Vector2(32.1f, 0.32f), new Vector2(1.2f, 0.88f), new Color(0.7f, 0.64f, 0.5f, 0.31f), -55);

        AddV9Steam(root.transform, "Jump_V9_ThinSteam", new Vector2(51.6f, -2.18f), new Color(0.72f, 0.78f, 0.68f, 0.16f), -20);

        AddEnvironmentV9SpriteChild(root.transform, "Enemy_V9_ServicePanel", "envv9_service_panel_lit.png", new Vector2(65.4f, 0.18f), new Vector2(1.18f, 1.26f), new Color(0.66f, 0.66f, 0.54f, 0.28f), -56);
        AddV9WarningLamp(root.transform, "Enemy_V9_WarningLamp", new Vector2(63.5f, 0.72f), new Vector2(0.42f, 0.62f), new Color(1f, 0.22f, 0.12f, 0.5f), -26);
        AddV9ElectricArc(root.transform, "Enemy_V9_CableArc", new Vector2(64.35f, 0.14f), new Vector2(0.82f, 0.28f), -22);

        AddEnvironmentV9SpriteChild(root.transform, "Trap_V9_BackFrameValvePipe", "envv9_valve_pipe_cluster.png", new Vector2(94.2f, 2.05f), new Vector2(8.2f, 1.12f), new Color(0.62f, 0.68f, 0.56f, 0.3f), -58);
        AddEnvironmentV9SpriteChild(root.transform, "Trap_V9_ServicePanel", "envv9_service_panel_lit.png", new Vector2(99.2f, 0.62f), new Vector2(1.55f, 1.65f), new Color(0.56f, 0.64f, 0.58f, 0.24f), -62);
        AddV9ElectricArc(root.transform, "Trap_V9_ElectricArc_Left", new Vector2(88.25f, 0.52f), new Vector2(1.05f, 0.34f), -22);
        AddV9ElectricArc(root.transform, "Trap_V9_ElectricArc_Right", new Vector2(96.4f, 0.68f), new Vector2(0.95f, 0.3f), -22);
        AddV9OilHaze(root.transform, "Trap_V9_OilHaze_Back", new Vector2(91.4f, -2.52f), new Vector2(8.2f, 0.62f), new Color(0.42f, 0.68f, 0.5f, 0.11f), -26);

        AddEnvironmentV9SpriteChild(root.transform, "Charge_V9_ServicePanel", "envv9_service_panel_lit.png", new Vector2(107.4f, 0.92f), new Vector2(0.82f, 0.95f), new Color(0.44f, 0.82f, 0.58f, 0.12f), -68);
        AddV9ChargePulse(root.transform, "Charge_V9_GreenChargePulse", new Vector2(114f, -0.44f), new Vector2(2.45f, 2.45f), -24);
        AddEffectsV3SpriteChild(root.transform, "Charge_V9_DustMotes", "fxv3_dust_motes.png", new Vector2(114f, 0.55f), new Vector2(10f, 4.0f), new Color(0.6f, 0.84f, 0.62f, 0.08f), -38).AddComponent<AmbientDrift2D>();

        GameObject bossFan = AddEnvironmentV9SpriteChild(root.transform, "Boss_V9_VentFan_Rotating", "envv9_ventilation_fan.png", new Vector2(136.4f, 1.82f), new Vector2(2.15f, 2.15f), new Color(0.58f, 0.64f, 0.54f, 0.28f), -84);
        SimpleRotator2D bossFanRotator = bossFan.AddComponent<SimpleRotator2D>();
        SetFloat(bossFanRotator, "degreesPerSecond", -5.2f);
        GameObject fanShadow = AddEffectsV3SpriteChild(root.transform, "Boss_V9_FanShadow", "fxv3_fan_shadow.png", new Vector2(136.4f, 1.82f), new Vector2(2.6f, 2.6f), new Color(0.2f, 0.18f, 0.14f, 0.34f), -83);
        SimpleRotator2D fanShadowRotator = fanShadow.AddComponent<SimpleRotator2D>();
        SetFloat(fanShadowRotator, "degreesPerSecond", 6.4f);
        AddV9Steam(root.transform, "Boss_V9_ThinSteam_Left", new Vector2(130.9f, -2.02f), new Color(0.78f, 0.72f, 0.62f, 0.15f), -20);
        AddV9Steam(root.transform, "Boss_V9_ThinSteam_Right", new Vector2(149.2f, -1.98f), new Color(0.78f, 0.72f, 0.62f, 0.15f), -20);

        AddEnvironmentV9SpriteChild(root.transform, "Exit_V9_DoorFramePipe", "envv9_door_frame_pipe.png", new Vector2(169.4f, 0.62f), new Vector2(2.6f, 2.75f), new Color(0.5f, 0.68f, 0.7f, 0.26f), -60);
        AddV9WarningLamp(root.transform, "Exit_V9_WarningLamp", new Vector2(170.85f, 0.92f), new Vector2(0.48f, 0.72f), new Color(1f, 0.24f, 0.12f, 0.52f), -26);
        AddV9ElectricArc(root.transform, "Exit_V9_DoorArc", new Vector2(171.1f, -0.2f), new Vector2(0.92f, 0.3f), -22);
    }

    private static void CreateLayeredDecorV10(Transform parent)
    {
        GameObject farRoot = NewChild(parent, "BG_FarDecor_V10");
        ParallaxLayer2D farParallax = farRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(farParallax, "parallaxFactor", new Vector2(0.045f, 0.01f));

        float[] farXs = { 14f, 47f, 81f, 118f, 151f };
        for (int i = 0; i < farXs.Length; i++)
        {
            GameObject fan = AddEnvironmentV9SpriteChild(farRoot.transform, $"FarDecor_V10_SlowFan_{i:00}", "envv9_ventilation_fan.png", new Vector2(farXs[i], 2.35f + (i % 2) * 0.35f), new Vector2(1.25f, 1.25f), new Color(0.48f, 0.58f, 0.5f, 0.12f), -104);
            SimpleRotator2D rotator = fan.AddComponent<SimpleRotator2D>();
            SetFloat(rotator, "degreesPerSecond", i % 2 == 0 ? 2.2f : -1.8f);
            AddV10WarningBlink(farRoot.transform, $"FarDecor_V10_WarningBlink_{i:00}", new Vector2(farXs[i] + 4.5f, 1.2f + (i % 2) * 0.28f), 0.34f, -88);
        }

        GameObject midRoot = NewChild(parent, "BG_MidDecor_V10");
        ParallaxLayer2D midParallax = midRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(midParallax, "parallaxFactor", new Vector2(0.09f, 0.018f));

        AddV10Steam(midRoot.transform, "MidDecor_V10_Steam_Awakening", new Vector2(6.2f, -2.05f), new Color(0.74f, 0.78f, 0.66f, 0.14f), -24);
        AddV10ElectricArc(midRoot.transform, "MidDecor_V10_Arc_TutorialCables", new Vector2(22.2f, 0.25f), new Vector2(0.86f, 0.28f), -24);
        AddEnvironmentV9SpriteChild(midRoot.transform, "MidDecor_V10_TutorialServicePanel", "envv9_service_panel_lit.png", new Vector2(29.8f, 0.45f), new Vector2(1.18f, 1.2f), new Color(0.58f, 0.62f, 0.52f, 0.25f), -58);
        AddV10Steam(midRoot.transform, "MidDecor_V10_Steam_JumpPipe", new Vector2(55.5f, -1.95f), new Color(0.64f, 0.78f, 0.66f, 0.13f), -24);
        AddEnvironmentV9SpriteChild(midRoot.transform, "MidDecor_V10_EnemyGaugeCluster", "envv9_old_gauge_cluster.png", new Vector2(75.5f, 0.58f), new Vector2(1.18f, 0.84f), new Color(0.68f, 0.6f, 0.48f, 0.28f), -56);
        AddV10ElectricArc(midRoot.transform, "MidDecor_V10_Arc_TrapBackwall", new Vector2(96.2f, 0.58f), new Vector2(1.05f, 0.32f), -24);
        AddV10Steam(midRoot.transform, "MidDecor_V10_Steam_BossPipeA", new Vector2(132f, -1.95f), new Color(0.78f, 0.72f, 0.6f, 0.15f), -24);
        AddV10Steam(midRoot.transform, "MidDecor_V10_Steam_BossPipeB", new Vector2(151f, -1.95f), new Color(0.78f, 0.72f, 0.6f, 0.15f), -24);
        AddV10ElectricArc(midRoot.transform, "MidDecor_V10_Arc_ExitGate", new Vector2(168.8f, 0.28f), new Vector2(0.95f, 0.3f), -24);

        GameObject nearRoot = NewChild(parent, "BG_NearDecor_V10");
        ParallaxLayer2D nearParallax = nearRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(nearParallax, "parallaxFactor", Vector2.zero);

        float[] chainXs = { 18f, 44f, 84f, 126f, 160f };
        for (int i = 0; i < chainXs.Length; i++)
        {
            GameObject chain = AddEnvironmentV9SpriteChild(nearRoot.transform, $"NearDecor_V10_TopChain_{i:00}", "envv9_far_crane_hook.png", new Vector2(chainXs[i], 3.35f), new Vector2(0.46f, 1.45f), new Color(0.62f, 0.56f, 0.46f, 0.28f), 18);
        }

        AddEffectsV4SpriteChild(nearRoot.transform, "NearDecor_V10_BottomScrapShadow_A", "fxv4_oil_haze.png", new Vector2(30f, -4.56f), new Vector2(28f, 0.7f), new Color(0.18f, 0.22f, 0.16f, 0.22f), 18);
        AddEffectsV4SpriteChild(nearRoot.transform, "NearDecor_V10_BottomScrapShadow_B", "fxv4_oil_haze.png", new Vector2(112f, -4.58f), new Vector2(38f, 0.7f), new Color(0.18f, 0.22f, 0.16f, 0.2f), 18);
        AddEffectsV4SpriteChild(nearRoot.transform, "NearDecor_V10_DustVeil_Long", "fxv4_dust_veil.png", new Vector2(88f, 2.2f), new Vector2(75f, 4.4f), new Color(0.78f, 0.74f, 0.48f, 0.06f), -32);
    }

    private static void CreateLayeredDecorV11(Transform parent)
    {
        GameObject farRoot = NewChild(parent, "BG_FarDecor_V11");
        ParallaxLayer2D farParallax = farRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(farParallax, "parallaxFactor", new Vector2(0.035f, 0.008f));

        AddEnvironmentV11SpriteChild(farRoot.transform, "FarDecor_V11_CrateStack_Awakening", "envv11_far_crate_stack.png", new Vector2(12f, -0.45f), new Vector2(3.4f, 2.55f), new Color(0.58f, 0.62f, 0.52f, 0.17f), -112);
        AddEnvironmentV11SpriteChild(farRoot.transform, "FarDecor_V11_Conveyor_AwakeningBack", "envv11_far_conveyor.png", new Vector2(8f, 0.12f), new Vector2(5.6f, 0.94f), new Color(0.52f, 0.58f, 0.5f, 0.13f), -116);
        AddEnvironmentV11SpriteChild(farRoot.transform, "FarDecor_V11_Conveyor_Tutorial", "envv11_far_conveyor.png", new Vector2(32f, -0.1f), new Vector2(6.4f, 1.1f), new Color(0.58f, 0.62f, 0.52f, 0.18f), -110);
        AddEnvironmentV11SpriteChild(farRoot.transform, "FarDecor_V11_CrateStack_Jump", "envv11_far_crate_stack.png", new Vector2(50.5f, -0.48f), new Vector2(2.6f, 1.95f), new Color(0.52f, 0.6f, 0.5f, 0.14f), -112);
        AddEnvironmentV11SpriteChild(farRoot.transform, "FarDecor_V11_CrateStack_Enemy", "envv11_far_crate_stack.png", new Vector2(73f, -0.42f), new Vector2(2.9f, 2.18f), new Color(0.56f, 0.6f, 0.5f, 0.16f), -112);
        AddEnvironmentV11SpriteChild(farRoot.transform, "FarDecor_V11_Conveyor_EnemyBack", "envv11_far_conveyor.png", new Vector2(70f, 0.04f), new Vector2(5.6f, 0.95f), new Color(0.5f, 0.58f, 0.5f, 0.13f), -116);
        AddEnvironmentV11SpriteChild(farRoot.transform, "FarDecor_V11_Conveyor_Trap", "envv11_far_conveyor.png", new Vector2(91.5f, -0.04f), new Vector2(7.8f, 1.22f), new Color(0.52f, 0.62f, 0.56f, 0.2f), -110);
        AddEnvironmentV11SpriteChild(farRoot.transform, "FarDecor_V11_CrateStack_Charge", "envv11_far_crate_stack.png", new Vector2(120.4f, -0.58f), new Vector2(2.15f, 1.62f), new Color(0.48f, 0.62f, 0.52f, 0.12f), -112);
        AddEnvironmentV11SpriteChild(farRoot.transform, "FarDecor_V11_Conveyor_Boss", "envv11_far_conveyor.png", new Vector2(147f, 0.04f), new Vector2(8.4f, 1.22f), new Color(0.58f, 0.58f, 0.48f, 0.17f), -110);
        AddEffectsV5SpriteChild(farRoot.transform, "FarDecor_V11_DustMotes_Long", "fxv5_fine_dust_motes.png", new Vector2(84f, 0.82f), new Vector2(52f, 12f), new Color(0.86f, 0.78f, 0.48f, 0.075f), -86).AddComponent<AmbientDrift2D>();

        GameObject midRoot = NewChild(parent, "BG_MidDecor_V11");
        ParallaxLayer2D midParallax = midRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(midParallax, "parallaxFactor", new Vector2(0.085f, 0.018f));

        AddEnvironmentV11SpriteChild(midRoot.transform, "MidDecor_V11_RightServicePanel_Awakening", "envv11_service_panel.png", new Vector2(6.3f, -0.68f), new Vector2(1.18f, 1.55f), new Color(0.78f, 0.88f, 0.68f, 0.42f), -44);
        AddEnvironmentV11SpriteChild(midRoot.transform, "MidDecor_V11_DanglingCable_Awakening", "envv11_broken_cable_bundle.png", new Vector2(7.85f, 0.55f), new Vector2(0.86f, 1.55f), new Color(0.82f, 0.78f, 0.64f, 0.36f), -32).AddComponent<SwayingDecor2D>();
        AddEnvironmentV11SpriteChild(midRoot.transform, "MidDecor_V11_ServicePanel_Tutorial", "envv11_service_panel.png", new Vector2(24.2f, 0.15f), new Vector2(0.92f, 1.24f), new Color(0.62f, 0.86f, 0.66f, 0.25f), -50);
        AddEnvironmentV11SpriteChild(midRoot.transform, "MidDecor_V11_WallPlate_Enemy", "envv11_repair_wall_plate.png", new Vector2(68.5f, 0.3f), new Vector2(2.4f, 1.58f), new Color(0.56f, 0.62f, 0.52f, 0.22f), -60);
        AddV11Lamp(midRoot.transform, "MidDecor_V11_Lamp_Awakening", new Vector2(4.9f, 1.15f), new Vector2(0.82f, 1.25f), -28);
        AddV11Lamp(midRoot.transform, "MidDecor_V11_Lamp_Jump", new Vector2(50f, 1.25f), new Vector2(0.72f, 1.08f), -30);
        AddV11Lamp(midRoot.transform, "MidDecor_V11_Lamp_Trap", new Vector2(93.5f, 1.35f), new Vector2(0.74f, 1.08f), -30);
        AddV11Lamp(midRoot.transform, "MidDecor_V11_Lamp_BossHall", new Vector2(135f, 1.35f), new Vector2(0.76f, 1.12f), -30);
        AddEnvironmentV11SpriteChild(midRoot.transform, "MidDecor_V11_RepairWall_Trap", "envv11_repair_wall_plate.png", new Vector2(98.6f, 0.25f), new Vector2(2.5f, 1.7f), new Color(0.58f, 0.64f, 0.54f, 0.26f), -58);
        AddEnvironmentV11SpriteChild(midRoot.transform, "MidDecor_V11_BrokenCable_Trap", "envv11_broken_cable_bundle.png", new Vector2(87.4f, 0.95f), new Vector2(0.78f, 1.28f), new Color(0.72f, 0.78f, 0.66f, 0.26f), -36).AddComponent<SwayingDecor2D>();
        AddEnvironmentV11SpriteChild(midRoot.transform, "MidDecor_V11_ServicePanel_Exit", "envv11_service_panel.png", new Vector2(166.6f, -0.3f), new Vector2(1.05f, 1.42f), new Color(0.58f, 0.88f, 0.72f, 0.28f), -46);
        AddV11ElectricSpark(midRoot.transform, "MidDecor_V11_ElectricSpark_TrapCable", new Vector2(88.8f, 0.32f), new Vector2(0.86f, 0.36f), -22);
        AddV11ElectricSpark(midRoot.transform, "MidDecor_V11_ElectricSpark_ExitDoor", new Vector2(170.4f, 0.18f), new Vector2(0.78f, 0.32f), -22);

        GameObject nearRoot = NewChild(parent, "BG_NearDecor_V11");
        ParallaxLayer2D nearParallax = nearRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(nearParallax, "parallaxFactor", Vector2.zero);

        float[] chainXs = { 16f, 39f, 64f, 92f, 121f, 154f };
        for (int i = 0; i < chainXs.Length; i++)
        {
            GameObject chain = AddEnvironmentV11SpriteChild(nearRoot.transform, $"NearDecor_V11_TopChain_{i:00}", "envv11_chain_hook.png", new Vector2(chainXs[i], 3.08f), new Vector2(0.46f, 1.78f), new Color(0.72f, 0.65f, 0.5f, 0.36f), 18);
        }

        AddV11StreetLamp(nearRoot.transform, "NearDecor_V11_StreetLamp_TrapEntry", new Vector2(84.6f, -1.48f), 18);
        AddV11StreetLamp(nearRoot.transform, "NearDecor_V11_StreetLamp_Charge", new Vector2(109.7f, -1.48f), 18);
        AddV11StreetLamp(nearRoot.transform, "NearDecor_V11_StreetLamp_Exit", new Vector2(164.3f, -1.48f), 18);
        AddEffectsV5SpriteChild(nearRoot.transform, "NearDecor_V11_FallDust_LeftPit", "fxv5_fall_fog_plume.png", new Vector2(48.5f, -4.72f), new Vector2(4.8f, 2.0f), new Color(0.48f, 0.56f, 0.44f, 0.2f), 18);
        AddEffectsV5SpriteChild(nearRoot.transform, "NearDecor_V11_FallDust_TrapPit", "fxv5_fall_fog_plume.png", new Vector2(91f, -4.78f), new Vector2(5.2f, 2.1f), new Color(0.44f, 0.58f, 0.5f, 0.18f), 18);
        AddEffectsV5SpriteChild(nearRoot.transform, "NearDecor_V11_LongFallingDust", "fxv5_long_falling_dust_veil.png", new Vector2(88f, -0.15f), new Vector2(150f, 8.0f), new Color(0.92f, 0.74f, 0.46f, 0.055f), -34);
    }

    private static void CreateProvidedLayeredDecorV2(Transform parent)
    {
        GameObject root = NewChild(parent, "BG_ProvidedDecor_V2");

        GameObject farRoot = NewChild(root.transform, "BG_ProvidedFarDecor_V2");
        ParallaxLayer2D farParallax = farRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(farParallax, "parallaxFactor", new Vector2(0.03f, 0.006f));

        AddProvidedEnvironmentV2SpriteChild(farRoot.transform, "ProvidedV2_FarCrate_Awakening", "provided_v2_storage_crate.png", new Vector2(13.2f, -0.7f), new Vector2(2.6f, 2.05f), new Color(0.48f, 0.58f, 0.48f, 0.18f), -118);
        AddProvidedEnvironmentV2SpriteChild(farRoot.transform, "ProvidedV2_FarCrate_PlatformBack", "provided_v2_storage_crate.png", new Vector2(54.2f, -0.74f), new Vector2(2.2f, 1.73f), new Color(0.46f, 0.58f, 0.48f, 0.15f), -118);
        AddProvidedEnvironmentV2SpriteChild(farRoot.transform, "ProvidedV2_FarTruss_BossBack", "provided_v2_truss_support.png", new Vector2(129.2f, 0.28f), new Vector2(1.18f, 2.55f), new Color(0.52f, 0.54f, 0.46f, 0.16f), -116);
        AddProvidedEnvironmentV2SpriteChild(farRoot.transform, "ProvidedV2_FarPipe_Exit", "provided_v2_pipe_vertical.png", new Vector2(162.2f, 0.38f), new Vector2(1.2f, 3.15f), new Color(0.48f, 0.58f, 0.54f, 0.14f), -118);

        GameObject midRoot = NewChild(root.transform, "BG_ProvidedMidDecor_V2");
        ParallaxLayer2D midParallax = midRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(midParallax, "parallaxFactor", new Vector2(0.07f, 0.014f));

        AddProvidedPipeWithSteam(midRoot.transform, "ProvidedV2_MidPipe_Tutorial", new Vector2(24.5f, 2.05f), new Vector2(3.2f, 1.16f), new Color(0.72f, 0.62f, 0.46f, 0.36f), -60);
        AddProvidedPipeWithSteam(midRoot.transform, "ProvidedV2_MidPipe_Trap", new Vector2(93.4f, 2.08f), new Vector2(3.8f, 1.36f), new Color(0.58f, 0.72f, 0.62f, 0.32f), -62);
        AddProvidedEnvironmentV2SpriteChild(midRoot.transform, "ProvidedV2_MidVerticalPipe_ChargeSide", "provided_v2_pipe_vertical.png", new Vector2(120.7f, 0.92f), new Vector2(0.78f, 2.2f), new Color(0.52f, 0.68f, 0.58f, 0.28f), -62);
        AddProvidedEnvironmentV2SpriteChild(midRoot.transform, "ProvidedV2_MidTruss_JumpWall", "provided_v2_truss_support.png", new Vector2(45.2f, 0.3f), new Vector2(0.95f, 2.05f), new Color(0.62f, 0.58f, 0.46f, 0.28f), -66);

        GameObject hangingLamp = AddProvidedEnvironmentV2SpriteChild(midRoot.transform, "ProvidedV2_MidHangingLamp_BossHall", "provided_v2_hanging_lamp.png", new Vector2(137.5f, 1.72f), new Vector2(1.4f, 1.1f), new Color(1f, 0.78f, 0.42f, 0.58f), -30);
        hangingLamp.AddComponent<SpriteFlicker2D>();
        AddV12FlickerHalo(midRoot.transform, "ProvidedV2_MidHangingLamp_BossHall_Halo", new Vector2(137.5f, 1.35f), new Vector2(2.4f, 1.75f), new Color(1f, 0.62f, 0.16f, 0.11f), -34, 2.1f);

        GameObject cableChain = AddProvidedEnvironmentV2SpriteChild(midRoot.transform, "ProvidedV2_MidChain_ChargeSide", "provided_v2_chain_long.png", new Vector2(111.4f, 1.55f), new Vector2(0.42f, 1.72f), new Color(0.72f, 0.64f, 0.48f, 0.28f), -42);
        AddSway(cableChain, 1.2f, 6.8f, new Vector2(0.012f, 0.006f));

        GameObject nearRoot = NewChild(root.transform, "BG_ProvidedNearDecor_V2");
        ParallaxLayer2D nearParallax = nearRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(nearParallax, "parallaxFactor", Vector2.zero);

        AddProvidedEnvironmentV2SpriteChild(nearRoot.transform, "ProvidedV2_NearTopChain_Enemy", "provided_v2_chain_long.png", new Vector2(74.8f, 3.18f), new Vector2(0.36f, 1.55f), new Color(0.72f, 0.62f, 0.46f, 0.34f), 17);
        AddProvidedEnvironmentV2SpriteChild(nearRoot.transform, "ProvidedV2_NearTopChain_Boss", "provided_v2_chain_long.png", new Vector2(145.8f, 3.16f), new Vector2(0.38f, 1.62f), new Color(0.72f, 0.62f, 0.46f, 0.32f), 17);
    }

    private static void CreateProvidedLayeredDecorV3(Transform parent)
    {
        GameObject root = NewChild(parent, "BG_ProvidedDecor_V3");

        GameObject farRoot = NewChild(root.transform, "BG_ProvidedFarDecor_V3");
        ParallaxLayer2D farParallax = farRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(farParallax, "parallaxFactor", new Vector2(0.025f, 0.006f));

        AddProvidedEnvironmentV3SpriteChild(farRoot.transform, "ProvidedV3_FarDoor_AwakeningWall", "provided_v3_double_door.png", new Vector2(12.4f, 0.35f), new Vector2(2.4f, 2.1f), new Color(0.46f, 0.52f, 0.44f, 0.15f), -124);
        AddProvidedEnvironmentV3SpriteChild(farRoot.transform, "ProvidedV3_FarFence_Tutorial", "provided_v3_chain_fence.png", new Vector2(30.66f, 0.58f), new Vector2(2.4f, 2.05f), new Color(0.44f, 0.55f, 0.48f, 0.2f), -122);
        AddProvidedEnvironmentV3SpriteChild(farRoot.transform, "ProvidedV3_FarBrokenWindow_Platform", "provided_v3_broken_window.png", new Vector2(58.7f, 0.9f), new Vector2(2.18f, 1.75f), new Color(0.48f, 0.58f, 0.5f, 0.16f), -124);
        AddProvidedEnvironmentV3SpriteChild(farRoot.transform, "ProvidedV3_FarTire_EnemyBack", "provided_v3_tire.png", new Vector2(74.3f, -0.62f), new Vector2(1.65f, 1.18f), new Color(0.4f, 0.46f, 0.42f, 0.16f), -120);
        AddProvidedEnvironmentV3SpriteChild(farRoot.transform, "ProvidedV3_FarScrapHeap_TrapBack", "provided_v3_scrap_heap.png", new Vector2(99.8f, -0.72f), new Vector2(2.35f, 1.38f), new Color(0.48f, 0.58f, 0.5f, 0.15f), -122);
        AddProvidedEnvironmentV3SpriteChild(farRoot.transform, "ProvidedV3_FarRobotWreck_BossBack", "provided_v3_robot_wreck.png", new Vector2(148.7f, -0.82f), new Vector2(3.3f, 1.42f), new Color(0.52f, 0.5f, 0.42f, 0.16f), -122);

        GameObject midRoot = NewChild(root.transform, "BG_ProvidedMidDecor_V3");
        ParallaxLayer2D midParallax = midRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(midParallax, "parallaxFactor", new Vector2(0.075f, 0.015f));

        AddProvidedEnvironmentV3SpriteChild(midRoot.transform, "ProvidedV3_MidElectricBox_Tutorial", "provided_v3_electric_box.png", new Vector2(33.4f, 0.72f), new Vector2(1.02f, 1.82f), new Color(0.64f, 0.7f, 0.56f, 0.34f), -54);
        AddV11ElectricSpark(midRoot.transform, "ProvidedV3_MidElectricBox_Tutorial_Arc", new Vector2(33.0f, 0.28f), new Vector2(0.72f, 0.26f), -24);
        AddProvidedEnvironmentV3SpriteChild(midRoot.transform, "ProvidedV3_MidVentGrate_Enemy", "provided_v3_vent_grate.png", new Vector2(68.33f, 0.82f), new Vector2(1.2f, 1.0f), new Color(0.62f, 0.64f, 0.52f, 0.28f), -58);
        GameObject trapFan = AddProvidedEnvironmentV3SpriteChild(midRoot.transform, "ProvidedV3_MidFan_TrapRotating", "provided_v3_vent_fan.png", new Vector2(105.99f, 1.35f), new Vector2(1.74f, 1.9f), new Color(0.58f, 0.64f, 0.54f, 0.36f), -82);
        SimpleRotator2D trapFanRotator = trapFan.AddComponent<SimpleRotator2D>();
        SetFloat(trapFanRotator, "degreesPerSecond", -10.5f);
        AddProvidedEnvironmentV3SpriteChild(midRoot.transform, "ProvidedV3_MidGenerator_ChargeSide", "provided_v3_generator.png", new Vector2(120.8f, -0.62f), new Vector2(1.8f, 1.48f), new Color(0.56f, 0.68f, 0.54f, 0.24f), -60);
        AddProvidedEnvironmentV3SpriteChild(midRoot.transform, "ProvidedV3_MidRedBeacon_BossHall", "provided_v3_red_beacon.png", new Vector2(146.35f, 1.2f), new Vector2(0.72f, 1.32f), new Color(1f, 0.48f, 0.28f, 0.48f), -28).AddComponent<SpriteFlicker2D>();
        AddV12FlickerHalo(midRoot.transform, "ProvidedV3_MidRedBeacon_BossHall_Halo", new Vector2(146.35f, 1.22f), new Vector2(1.8f, 1.7f), new Color(1f, 0.28f, 0.12f, 0.12f), -34, 3.4f);
        AddProvidedEnvironmentV3SpriteChild(midRoot.transform, "ProvidedV3_MidBurningBarrel_Exit", "provided_v3_burning_barrel.png", new Vector2(164.4f, -0.86f), new Vector2(0.92f, 1.5f), new Color(0.86f, 0.66f, 0.42f, 0.42f), -36);
        AddV12FlickerHalo(midRoot.transform, "ProvidedV3_MidBurningBarrel_Exit_Halo", new Vector2(164.4f, -0.35f), new Vector2(2.1f, 1.7f), new Color(1f, 0.42f, 0.08f, 0.13f), -38, 5.2f);
        AddV9Steam(midRoot.transform, "ProvidedV3_MidBurningBarrel_Exit_Smoke", new Vector2(164.4f, 0.2f), new Color(0.64f, 0.58f, 0.48f, 0.15f), -26);

        GameObject nearRoot = NewChild(root.transform, "BG_ProvidedNearDecor_V3");
        ParallaxLayer2D nearParallax = nearRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(nearParallax, "parallaxFactor", Vector2.zero);

        GameObject flag = AddProvidedEnvironmentV3SpriteChild(nearRoot.transform, "ProvidedV3_NearTornFlag_HighBoss", "provided_v3_torn_flag.png", new Vector2(132.3f, 3.0f), new Vector2(1.45f, 1.15f), new Color(0.8f, 0.52f, 0.34f, 0.36f), 16);
        AddSway(flag, 1.6f, 6.4f, new Vector2(0.012f, 0.004f));
        AddProvidedEnvironmentV3SpriteChild(nearRoot.transform, "ProvidedV3_NearCatwalk_SeamTop", "provided_v3_catwalk_grate.png", new Vector2(68.33f, 3.18f), new Vector2(2.4f, 0.78f), new Color(0.54f, 0.48f, 0.38f, 0.26f), 16);
    }

    private static void CreateDynamicDecorV13(Transform parent)
    {
        GameObject root = NewChild(parent, "BG_DynamicDecor_V13");

        GameObject farRoot = NewChild(root.transform, "BG_DynamicFarDecor_V13");
        ParallaxLayer2D farParallax = farRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(farParallax, "parallaxFactor", new Vector2(0.032f, 0.007f));

        AddV13RotatingFan(farRoot.transform, "V13_FarRotator_AwakeningGear", new Vector2(9.5f, 2.0f), 1.55f, -108, 4.2f);
        AddV13RotatingFan(farRoot.transform, "V13_FarRotator_PlatformFan", new Vector2(52.5f, 2.35f), 1.48f, -108, -5.4f);
        AddV13RotatingFan(farRoot.transform, "V13_FarRotator_TrapGear", new Vector2(96.4f, 2.25f), 1.72f, -108, 4.8f);
        AddV13RotatingFan(farRoot.transform, "V13_FarRotator_BossFan", new Vector2(139.6f, 2.45f), 2.05f, -108, -4.6f);
        AddV13RotatingFan(farRoot.transform, "V13_FarRotator_ExitGear", new Vector2(168.8f, 2.12f), 1.52f, -108, 5.0f);

        AddV10WarningBlink(farRoot.transform, "V13_FarWarning_Awakening", new Vector2(13.9f, 1.28f), 0.42f, -90);
        AddV10WarningBlink(farRoot.transform, "V13_FarWarning_Enemy", new Vector2(74.8f, 1.05f), 0.42f, -90);
        AddV10WarningBlink(farRoot.transform, "V13_FarWarning_Trap", new Vector2(101.2f, 1.42f), 0.46f, -90);
        AddV10WarningBlink(farRoot.transform, "V13_FarWarning_Boss", new Vector2(151.8f, 1.22f), 0.48f, -90);
        AddV10WarningBlink(farRoot.transform, "V13_FarWarning_Exit", new Vector2(170.4f, 1.45f), 0.42f, -90);

        AddV13DustBand(farRoot.transform, "V13_FarDust_Awakening", new Vector2(9f, 1.2f), new Vector2(20f, 6.4f), new Color(0.9f, 0.74f, 0.42f, 0.082f), -116, 0.22f, 0.08f, 14f);
        AddV13DustBand(farRoot.transform, "V13_FarDust_Platform", new Vector2(50f, 1.32f), new Vector2(24f, 6.2f), new Color(0.72f, 0.82f, 0.56f, 0.074f), -116, -0.18f, 0.08f, 15f);
        AddV13DustBand(farRoot.transform, "V13_FarDust_Trap", new Vector2(93f, 1.18f), new Vector2(24f, 6.0f), new Color(0.48f, 0.86f, 0.78f, 0.078f), -116, 0.2f, 0.07f, 13f);
        AddV13DustBand(farRoot.transform, "V13_FarDust_Boss", new Vector2(141f, 1.28f), new Vector2(34f, 6.6f), new Color(0.88f, 0.68f, 0.42f, 0.078f), -116, -0.2f, 0.08f, 16f);
        AddV13DustBand(farRoot.transform, "V13_FarDust_Exit", new Vector2(166f, 1.25f), new Vector2(18f, 5.8f), new Color(0.66f, 0.78f, 0.82f, 0.064f), -116, 0.16f, 0.06f, 14.5f);

        GameObject midRoot = NewChild(root.transform, "BG_DynamicMidDecor_V13");
        ParallaxLayer2D midParallax = midRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(midParallax, "parallaxFactor", new Vector2(0.078f, 0.016f));

        AddV13SteamJet(midRoot.transform, "V13_MidSteam_AwakeningVent", new Vector2(7.2f, -1.18f), -23);
        AddV13LightningBurst(midRoot.transform, "V13_MidLightning_TutorialElectricBox", new Vector2(33.35f, 0.42f), new Vector2(1.22f, 0.42f), -20, -6f, 1.05f);
        AddV13ConveyorPulse(midRoot.transform, "V13_MidConveyorPulse_Tutorial", new Vector2(31.2f, 0.02f), new Vector2(4.8f, 0.36f), new Vector2(2.1f, 0f), -68, 2.6f);

        GameObject platformChain = AddEnvironmentV11SpriteChild(midRoot.transform, "V13_MidSwayChain_PlatformGap", "envv11_chain_hook.png", new Vector2(44.6f, 2.35f), new Vector2(0.48f, 1.85f), new Color(0.78f, 0.68f, 0.5f, 0.46f), -34);
        AddSway(platformChain, 2.8f, 5.2f, new Vector2(0.035f, 0.012f));
        AddV13SteamJet(midRoot.transform, "V13_MidSteam_PlatformGap", new Vector2(54.4f, -1.18f), -23);
        AddV13LightningBurst(midRoot.transform, "V13_MidLightning_EnemyPanel", new Vector2(68.4f, 0.55f), new Vector2(0.96f, 0.32f), -22, 12f, 1.2f);

        AddV13LightningBurst(midRoot.transform, "V13_MidLightning_TrapFloorLeft", new Vector2(87.6f, -0.12f), new Vector2(1.35f, 0.42f), -18, 2f, 0.95f);
        AddV13LightningBurst(midRoot.transform, "V13_MidLightning_TrapWall", new Vector2(96.1f, 0.82f), new Vector2(1.55f, 0.5f), -20, -8f, 0.85f);
        AddV13LightningBurst(midRoot.transform, "V13_MidLightning_TrapCompressor", new Vector2(101.2f, 0.38f), new Vector2(1.12f, 0.38f), -20, 18f, 1.05f);
        AddV13ConveyorPulse(midRoot.transform, "V13_MidConveyorPulse_Trap", new Vector2(91.6f, 0.08f), new Vector2(6.4f, 0.4f), new Vector2(2.4f, 0f), -68, 2.2f);

        AddV9ChargePulse(midRoot.transform, "V13_MidChargePulse_SaveStation_A", new Vector2(114f, -0.38f), new Vector2(3.25f, 3.25f), -24);
        AddV13LightningBurst(midRoot.transform, "V13_MidLightning_SaveStationCable", new Vector2(117.1f, 0.78f), new Vector2(1.05f, 0.36f), -22, -10f, 1.1f);
        AddV13SteamJet(midRoot.transform, "V13_MidSteam_SaveStationPipe", new Vector2(120.7f, -1.1f), -23);

        GameObject bossFan = AddProvidedEnvironmentV3SpriteChild(midRoot.transform, "V13_MidRotator_BossWallFan", "provided_v3_vent_fan.png", new Vector2(136.2f, 1.62f), new Vector2(2.12f, 2.25f), new Color(0.64f, 0.7f, 0.54f, 0.44f), -78);
        SimpleRotator2D bossFanRotator = bossFan.AddComponent<SimpleRotator2D>();
        SetFloat(bossFanRotator, "degreesPerSecond", -14.0f);
        AddV12FlickerHalo(midRoot.transform, "V13_MidFlicker_BossRedHalo", new Vector2(143.7f, 1.15f), new Vector2(2.35f, 1.75f), new Color(1f, 0.26f, 0.12f, 0.18f), -33, 4.8f);
        AddV13SteamJet(midRoot.transform, "V13_MidSteam_BossPipeLeft", new Vector2(131.2f, -1.15f), -22);
        AddV13SteamJet(midRoot.transform, "V13_MidSteam_BossPipeRight", new Vector2(150.5f, -1.08f), -22);
        AddV13LightningBurst(midRoot.transform, "V13_MidLightning_BossDoor", new Vector2(155.3f, 0.72f), new Vector2(1.28f, 0.4f), -20, 8f, 1.1f);
        AddV13ConveyorPulse(midRoot.transform, "V13_MidConveyorPulse_BossBack", new Vector2(147.2f, 0.1f), new Vector2(7.2f, 0.42f), new Vector2(2.5f, 0f), -68, 2.75f);

        AddV12FlickerHalo(midRoot.transform, "V13_MidFlicker_ExitFireGlow", new Vector2(164.4f, -0.32f), new Vector2(2.7f, 2.05f), new Color(1f, 0.42f, 0.08f, 0.22f), -33, 5.8f);
        AddV13LightningBurst(midRoot.transform, "V13_MidLightning_ExitGate", new Vector2(170.8f, 0.2f), new Vector2(1.28f, 0.42f), -20, -12f, 1.2f);
        AddV13SteamJet(midRoot.transform, "V13_MidSteam_ExitDoor", new Vector2(168.5f, -1.05f), -23);

        GameObject nearRoot = NewChild(root.transform, "BG_DynamicNearDecor_V13");
        ParallaxLayer2D nearParallax = nearRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(nearParallax, "parallaxFactor", Vector2.zero);

        float[] chainXs = { 21f, 47.5f, 86.8f, 124.6f, 153.8f };
        for (int i = 0; i < chainXs.Length; i++)
        {
            GameObject chain = AddEnvironmentV11SpriteChild(nearRoot.transform, $"V13_NearSwayChain_High_{i:00}", "envv11_chain_hook.png", new Vector2(chainXs[i], 3.32f), new Vector2(0.42f, 1.62f), new Color(0.78f, 0.67f, 0.5f, 0.42f), 16);
            AddSway(chain, 1.4f + (i % 2) * 0.5f, 6.4f + i * 0.28f, new Vector2(0.014f, 0.006f));
        }

        GameObject flag = AddProvidedEnvironmentV3SpriteChild(nearRoot.transform, "V13_NearSwayFlag_BossHigh", "provided_v3_torn_flag.png", new Vector2(133.8f, 3.15f), new Vector2(1.28f, 1.02f), new Color(0.86f, 0.54f, 0.32f, 0.45f), 16);
        AddSway(flag, 2.0f, 5.8f, new Vector2(0.018f, 0.006f));

    }

    private static void CreateLargeDecorV14(Transform parent)
    {
        GameObject root = NewChild(parent, "BG_LargeDecor_V14");

        GameObject farRoot = NewChild(root.transform, "BG_LargeFarDecor_V14");
        ParallaxLayer2D farParallax = farRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(farParallax, "parallaxFactor", new Vector2(0.024f, 0.005f));

        AddProvidedEnvironmentV4SpriteChild(farRoot.transform, "V14_FarLargeMachineFrame_Awakening", "large_machine_frame_v4.png", new Vector2(8.5f, 1.32f), new Vector2(7.6f, 4.28f), new Color(0.42f, 0.52f, 0.42f, 0.22f), -128);
        AddProvidedEnvironmentV4SpriteChild(farRoot.transform, "V14_FarLargeConveyor_AwakeningBack", "large_conveyor_back_v4.png", new Vector2(7.6f, 1.18f), new Vector2(9.4f, 4.1f), new Color(0.42f, 0.5f, 0.42f, 0.13f), -132);
        AddProvidedEnvironmentV4SpriteChild(farRoot.transform, "V14_FarLargeConveyor_Tutorial", "large_conveyor_back_v4.png", new Vector2(31.5f, 1.18f), new Vector2(10.8f, 4.65f), new Color(0.45f, 0.52f, 0.46f, 0.16f), -130);
        AddProvidedEnvironmentV4SpriteChild(farRoot.transform, "V14_FarLargeGearWall_Platform", "large_gear_wall_v4.png", new Vector2(53.2f, 1.42f), new Vector2(5.9f, 3.92f), new Color(0.48f, 0.54f, 0.44f, 0.18f), -127);
        AddProvidedEnvironmentV4SpriteChild(farRoot.transform, "V14_FarLargeBoiler_EnemyBack", "large_boiler_tank_v4.png", new Vector2(73.6f, 1.08f), new Vector2(5.8f, 3.7f), new Color(0.42f, 0.56f, 0.46f, 0.15f), -129);
        AddProvidedEnvironmentV4SpriteChild(farRoot.transform, "V14_FarLargeBoiler_SaveBack", "large_boiler_tank_v4.png", new Vector2(113.8f, 1.16f), new Vector2(6.4f, 4.05f), new Color(0.44f, 0.58f, 0.46f, 0.17f), -128);
        AddProvidedEnvironmentV4SpriteChild(farRoot.transform, "V14_FarLargeMachineFrame_ExitVista", "large_machine_frame_v4.png", new Vector2(166.4f, 1.22f), new Vector2(7.2f, 4.18f), new Color(0.48f, 0.56f, 0.46f, 0.18f), -129);

        AddV13DustBand(farRoot.transform, "V14_FarDust_AwakeningLargeVista", new Vector2(8f, 1.7f), new Vector2(18f, 4.8f), new Color(0.82f, 0.76f, 0.58f, 0.05f), -124, 0.14f, 0.05f, 17f);
        AddV13DustBand(farRoot.transform, "V14_FarDust_LargeVista_A", new Vector2(30f, 1.6f), new Vector2(30f, 5.4f), new Color(0.82f, 0.74f, 0.56f, 0.065f), -124, 0.18f, 0.06f, 18f);
        AddV13DustBand(farRoot.transform, "V14_FarDust_LargeVista_B", new Vector2(130f, 1.55f), new Vector2(40f, 5.8f), new Color(0.72f, 0.78f, 0.62f, 0.062f), -124, -0.16f, 0.05f, 20f);
        AddV13ConveyorPulse(farRoot.transform, "V14_FarConveyorPulse_TutorialVista", new Vector2(32.3f, 1.02f), new Vector2(5.8f, 0.36f), new Vector2(2.8f, 0f), -121, 3.4f);
        AddV13RotatingFan(farRoot.transform, "V14_FarRotator_GearWall_Platform", new Vector2(53.2f, 1.55f), 1.55f, -120, 3.1f);
        AddV13SteamJet(farRoot.transform, "V14_FarSteam_Boiler_EnemyBack", new Vector2(73.8f, 2.28f), -119);
        AddV13SteamJet(farRoot.transform, "V14_FarSteam_Boiler_SaveBack", new Vector2(113.7f, 2.36f), -119);

        GameObject midRoot = NewChild(root.transform, "BG_LargeMidDecor_V14");
        ParallaxLayer2D midParallax = midRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(midParallax, "parallaxFactor", new Vector2(0.07f, 0.014f));

        AddProvidedEnvironmentV4SpriteChild(midRoot.transform, "V14_MidLargeMachineFrame_Tutorial", "large_machine_frame_v4.png", new Vector2(24.6f, 1.72f), new Vector2(6.6f, 3.84f), new Color(0.58f, 0.66f, 0.5f, 0.28f), -78);
        AddProvidedEnvironmentV4SpriteChild(midRoot.transform, "V14_MidLargeWallFan_Awakening", "large_wall_fan_v4.png", new Vector2(11.8f, 1.55f), new Vector2(3.9f, 2.6f), new Color(0.58f, 0.66f, 0.52f, 0.26f), -82);
        AddProvidedEnvironmentV4SpriteChild(midRoot.transform, "V14_MidLargeWallFan_Enemy", "large_wall_fan_v4.png", new Vector2(72.4f, 1.42f), new Vector2(4.9f, 3.25f), new Color(0.62f, 0.64f, 0.5f, 0.34f), -82);
        AddProvidedEnvironmentV4SpriteChild(midRoot.transform, "V14_MidLargeBoiler_Trap", "large_boiler_tank_v4.png", new Vector2(95.6f, 1.12f), new Vector2(6.1f, 3.74f), new Color(0.62f, 0.66f, 0.48f, 0.34f), -80);
        AddProvidedEnvironmentV4SpriteChild(midRoot.transform, "V14_MidLargeMachineFrame_Charge", "large_machine_frame_v4.png", new Vector2(116.5f, 1.35f), new Vector2(5.6f, 3.45f), new Color(0.52f, 0.68f, 0.5f, 0.24f), -84);
        AddProvidedEnvironmentV4SpriteChild(midRoot.transform, "V14_MidLargeGearWall_Boss", "large_gear_wall_v4.png", new Vector2(137.4f, 1.52f), new Vector2(7.1f, 4.25f), new Color(0.66f, 0.58f, 0.42f, 0.31f), -82);
        AddProvidedEnvironmentV4SpriteChild(midRoot.transform, "V14_MidLargeMachineFrame_Exit", "large_machine_frame_v4.png", new Vector2(165.0f, 1.18f), new Vector2(6.2f, 3.82f), new Color(0.58f, 0.64f, 0.48f, 0.28f), -83);

        AddV13LightningBurst(midRoot.transform, "V14_MidLightning_TutorialMachineFeed", new Vector2(22.2f, 2.28f), new Vector2(1.16f, 0.36f), -24, -8f, 0.86f);
        AddV13LightningBurst(midRoot.transform, "V14_MidLightning_TutorialMachineRelay", new Vector2(27.0f, 2.08f), new Vector2(1.02f, 0.32f), -24, 7f, 1.05f);
        AddV13RotatingFan(midRoot.transform, "V14_MidRotator_AwakeningWallFanBlade", new Vector2(11.8f, 1.58f), 1.28f, -28, 15f);
        AddV13RotatingFan(midRoot.transform, "V14_MidRotator_EnemyWallFanBlade", new Vector2(72.4f, 1.46f), 1.62f, -28, -22f);
        AddV13SteamJet(midRoot.transform, "V14_MidSteam_TrapBoiler_A", new Vector2(94.2f, 2.42f), -22);
        AddV13SteamJet(midRoot.transform, "V14_MidSteam_TrapBoiler_B", new Vector2(97.6f, 2.18f), -22);
        AddV13SteamJet(midRoot.transform, "V14_MidSteam_ChargeMachineFrame", new Vector2(117.4f, 2.18f), -22);
        AddV13RotatingFan(midRoot.transform, "V14_MidRotator_BossGearLeft", new Vector2(135.2f, 1.72f), 1.55f, -28, 12f);
        AddV13RotatingFan(midRoot.transform, "V14_MidRotator_BossGearRight", new Vector2(139.1f, 1.55f), 1.74f, -28, -9f);
        AddV13LightningBurst(midRoot.transform, "V14_MidLightning_BossGearFeed", new Vector2(141.4f, 2.25f), new Vector2(1.22f, 0.38f), -24, -14f, 1.0f);
        AddV12FlickerHalo(midRoot.transform, "V14_MidFlicker_MachineFrame_Exit", new Vector2(165.3f, 1.58f), new Vector2(2.8f, 1.8f), new Color(0.95f, 0.62f, 0.25f, 0.12f), -30, 4.6f);

        GameObject nearRoot = NewChild(root.transform, "BG_LargeNearDecor_V14");
        ParallaxLayer2D nearParallax = nearRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(nearParallax, "parallaxFactor", Vector2.zero);

        GameObject awakeningRail = AddProvidedEnvironmentV4SpriteChild(nearRoot.transform, "V14_NearOverheadCraneRail_Awakening", "large_overhead_crane_rail_v4.png", new Vector2(8.8f, 3.42f), new Vector2(9.4f, 1.58f), new Color(0.68f, 0.6f, 0.44f, 0.38f), 14);
        GameObject awakeningHook = AddProvidedEnvironmentV4SpriteChild(nearRoot.transform, "V14_NearCraneHook_Awakening", "large_crane_hook_v4.png", new Vector2(6.3f, 2.48f), new Vector2(0.96f, 2.45f), new Color(0.7f, 0.6f, 0.44f, 0.46f), 15);
        AddSway(awakeningHook, 2.0f, 6.5f, new Vector2(0.018f, 0.006f));

        GameObject platformRail = AddProvidedEnvironmentV4SpriteChild(nearRoot.transform, "V14_NearOverheadCraneRail_Platform", "large_overhead_crane_rail_v4.png", new Vector2(48.2f, 3.42f), new Vector2(12.2f, 1.82f), new Color(0.7f, 0.62f, 0.45f, 0.46f), 14);

        GameObject bossRail = AddProvidedEnvironmentV4SpriteChild(nearRoot.transform, "V14_NearOverheadCraneRail_Boss", "large_overhead_crane_rail_v4.png", new Vector2(136.2f, 3.46f), new Vector2(13.0f, 1.88f), new Color(0.72f, 0.6f, 0.42f, 0.43f), 14);
        GameObject bossHook = AddProvidedEnvironmentV4SpriteChild(nearRoot.transform, "V14_NearCraneHook_Boss", "large_crane_hook_v4.png", new Vector2(130.4f, 2.48f), new Vector2(1.10f, 2.78f), new Color(0.74f, 0.62f, 0.44f, 0.5f), 15);
        AddSway(bossHook, 2.25f, 6.8f, new Vector2(0.02f, 0.008f));

        AddV12FlickerHalo(nearRoot.transform, "V14_NearFlicker_RailLamp_Platform", new Vector2(49.4f, 3.07f), new Vector2(2.8f, 0.92f), new Color(1f, 0.62f, 0.24f, 0.12f), 13, 4.2f);
        AddV12FlickerHalo(nearRoot.transform, "V14_NearFlicker_RailLamp_Boss", new Vector2(137.7f, 3.08f), new Vector2(3.2f, 1.0f), new Color(1f, 0.48f, 0.18f, 0.13f), 13, 4.8f);
        AddV13DustBand(nearRoot.transform, "V14_NearDust_HighCrane", new Vector2(92f, 3.05f), new Vector2(120f, 1.2f), new Color(0.9f, 0.74f, 0.48f, 0.045f), 12, -0.12f, 0.03f, 19f);

        awakeningRail.transform.localRotation = Quaternion.Euler(0f, 0f, -0.35f);
        platformRail.transform.localRotation = Quaternion.Euler(0f, 0f, -0.4f);
        bossRail.transform.localRotation = Quaternion.Euler(0f, 0f, 0.35f);
    }

    private static void CreateMapReadableDecorV15(Transform parent)
    {
        GameObject root = NewChild(parent, "BG_MapReadableDecor_V15");

        GameObject farRoot = NewChild(root.transform, "BG_MapReadableFarDecor_V15");
        ParallaxLayer2D farParallax = farRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(farParallax, "parallaxFactor", new Vector2(0.018f, 0.004f));

        float[] seamXs = { 25.67f, 63.33f, 101f, 138.67f };
        for (int i = 0; i < seamXs.Length; i++)
        {
            AddEnvironmentV7SpriteChild(farRoot.transform, $"V15_SeamTruss_{i + 1:00}", "envv7_transition_truss.png", new Vector2(seamXs[i], 0.8f), new Vector2(1.38f, 8.6f), new Color(0.18f, 0.2f, 0.16f, 0.32f), -126);
            AddV13DustBand(farRoot.transform, $"V15_SeamDustVeil_{i + 1:00}", new Vector2(seamXs[i], 0.85f), new Vector2(4.4f, 8.2f), new Color(0.58f, 0.66f, 0.54f, 0.06f), -124, i % 2 == 0 ? 0.08f : -0.08f, 0.035f, 18f + i);
        }

        AddProvidedEnvironmentV4SpriteChild(farRoot.transform, "V15_FarMachineWall_Awakening", "large_machine_frame_v4.png", new Vector2(9.8f, 1.35f), new Vector2(8.4f, 4.25f), new Color(0.42f, 0.54f, 0.43f, 0.12f), -132);
        AddProvidedEnvironmentV4SpriteChild(farRoot.transform, "V15_FarBoiler_PlatformVista", "large_boiler_tank_v4.png", new Vector2(54.2f, 1.18f), new Vector2(6.8f, 4.05f), new Color(0.48f, 0.56f, 0.42f, 0.14f), -132);
        AddProvidedEnvironmentV4SpriteChild(farRoot.transform, "V15_FarConveyor_TrapVista", "large_conveyor_back_v4.png", new Vector2(99.2f, 1.25f), new Vector2(12.0f, 4.5f), new Color(0.42f, 0.56f, 0.5f, 0.13f), -132);
        AddProvidedEnvironmentV4SpriteChild(farRoot.transform, "V15_FarGearWall_BossVista", "large_gear_wall_v4.png", new Vector2(143.0f, 1.48f), new Vector2(8.2f, 4.65f), new Color(0.55f, 0.52f, 0.38f, 0.15f), -132);
        AddProvidedEnvironmentV4SpriteChild(farRoot.transform, "V15_FarMachineFrame_ExitVista", "large_machine_frame_v4.png", new Vector2(168.0f, 1.24f), new Vector2(7.2f, 4.08f), new Color(0.46f, 0.56f, 0.48f, 0.12f), -132);

        AddV13DustBand(farRoot.transform, "V15_FarDust_AwakeningReadable", new Vector2(9f, 1.55f), new Vector2(20f, 5.8f), new Color(0.78f, 0.72f, 0.54f, 0.05f), -123, 0.12f, 0.04f, 18f);
        AddV13DustBand(farRoot.transform, "V15_FarDust_TrapReadable", new Vector2(99f, 1.55f), new Vector2(26f, 5.8f), new Color(0.54f, 0.76f, 0.68f, 0.055f), -123, -0.1f, 0.04f, 17f);
        AddV13DustBand(farRoot.transform, "V15_FarDust_BossReadable", new Vector2(144f, 1.6f), new Vector2(36f, 6.2f), new Color(0.8f, 0.68f, 0.46f, 0.048f), -123, 0.1f, 0.035f, 20f);

        GameObject midRoot = NewChild(root.transform, "BG_MapReadableMidDecor_V15");
        ParallaxLayer2D midParallax = midRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(midParallax, "parallaxFactor", new Vector2(0.06f, 0.012f));

        AddV13RotatingFan(midRoot.transform, "V15_MidRotator_TutorialWallFan", new Vector2(32.5f, 1.72f), 1.35f, -52, 12f);
        AddV13RotatingFan(midRoot.transform, "V15_MidRotator_EnemyWallFan", new Vector2(77.0f, 1.6f), 1.55f, -52, -14f);
        AddV13RotatingFan(midRoot.transform, "V15_MidRotator_BossGear", new Vector2(144.6f, 1.86f), 2.0f, -52, 9f);
        AddV13SteamJet(midRoot.transform, "V15_MidSteam_PlatformDrop", new Vector2(56.6f, -1.08f), -24);
        AddV13SteamJet(midRoot.transform, "V15_MidSteam_TrapMachine", new Vector2(108.2f, -0.85f), -24);
        AddV13SteamJet(midRoot.transform, "V15_MidSteam_BossBackPipe", new Vector2(152.4f, -0.92f), -24);

        AddV13LightningBurst(midRoot.transform, "V15_MidLightning_TutorialPanel", new Vector2(34.8f, 0.8f), new Vector2(1.25f, 0.4f), -22, -9f, 1.05f);
        AddV13LightningBurst(midRoot.transform, "V15_MidLightning_TrapCable", new Vector2(99.0f, 0.75f), new Vector2(1.65f, 0.48f), -22, 7f, 0.82f);
        AddV13LightningBurst(midRoot.transform, "V15_MidLightning_ChargeRelay", new Vector2(121.7f, 0.85f), new Vector2(1.15f, 0.38f), -22, -12f, 1.15f);
        AddV13LightningBurst(midRoot.transform, "V15_MidLightning_BossDoorFeed", new Vector2(158.3f, 0.92f), new Vector2(1.25f, 0.42f), -22, 11f, 1.0f);

        AddV13ConveyorPulse(midRoot.transform, "V15_MidConveyorPulse_Tutorial", new Vector2(29.6f, 0.18f), new Vector2(5.2f, 0.36f), new Vector2(2.6f, 0f), -68, 2.7f);
        AddV13ConveyorPulse(midRoot.transform, "V15_MidConveyorPulse_Trap", new Vector2(99.4f, 0.22f), new Vector2(7.2f, 0.42f), new Vector2(2.8f, 0f), -68, 2.3f);
        AddV13ConveyorPulse(midRoot.transform, "V15_MidConveyorPulse_Boss", new Vector2(145.2f, 0.22f), new Vector2(7.0f, 0.42f), new Vector2(2.8f, 0f), -68, 2.6f);

        AddV12FlickerHalo(midRoot.transform, "V15_MidFlicker_AwakeningLampGlow", new Vector2(7.4f, 0.9f), new Vector2(3.2f, 1.8f), new Color(1f, 0.55f, 0.18f, 0.10f), -36, 2.0f);
        AddV12FlickerHalo(midRoot.transform, "V15_MidFlicker_SaveGreenPulse", new Vector2(120.0f, -0.1f), new Vector2(3.0f, 2.25f), new Color(0.35f, 1f, 0.62f, 0.12f), -36, 1.55f);
        AddV12FlickerHalo(midRoot.transform, "V15_MidFlicker_ExitDoorAmber", new Vector2(170.2f, 0.2f), new Vector2(3.1f, 2.0f), new Color(1f, 0.58f, 0.2f, 0.12f), -36, 2.4f);

        GameObject nearRoot = NewChild(root.transform, "BG_MapReadableNearDecor_V15");
        ParallaxLayer2D nearParallax = nearRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(nearParallax, "parallaxFactor", Vector2.zero);

        GameObject railTutorial = AddProvidedEnvironmentV4SpriteChild(nearRoot.transform, "V15_NearOverheadRail_Tutorial", "large_overhead_crane_rail_v4.png", new Vector2(28.0f, 3.45f), new Vector2(13.5f, 1.65f), new Color(0.72f, 0.62f, 0.44f, 0.34f), 14);
        GameObject hookTutorial = AddProvidedEnvironmentV4SpriteChild(nearRoot.transform, "V15_NearSwayHook_TutorialHigh", "large_crane_hook_v4.png", new Vector2(33.6f, 2.58f), new Vector2(0.95f, 2.3f), new Color(0.74f, 0.63f, 0.46f, 0.42f), 15);
        AddSway(hookTutorial, 1.8f, 6.2f, new Vector2(0.014f, 0.005f));

        GameObject railTrap = AddProvidedEnvironmentV4SpriteChild(nearRoot.transform, "V15_NearOverheadRail_Trap", "large_overhead_crane_rail_v4.png", new Vector2(99.0f, 3.42f), new Vector2(12.0f, 1.55f), new Color(0.68f, 0.62f, 0.46f, 0.32f), 14);
        GameObject hookTrap = AddProvidedEnvironmentV4SpriteChild(nearRoot.transform, "V15_NearSwayHook_TrapHigh", "large_crane_hook_v4.png", new Vector2(103.4f, 2.54f), new Vector2(0.9f, 2.22f), new Color(0.72f, 0.62f, 0.46f, 0.4f), 15);
        AddSway(hookTrap, 1.6f, 6.8f, new Vector2(0.012f, 0.005f));

        GameObject railBoss = AddProvidedEnvironmentV4SpriteChild(nearRoot.transform, "V15_NearOverheadRail_Boss", "large_overhead_crane_rail_v4.png", new Vector2(144.0f, 3.48f), new Vector2(14.0f, 1.72f), new Color(0.72f, 0.6f, 0.42f, 0.36f), 14);
        GameObject hookBoss = AddProvidedEnvironmentV4SpriteChild(nearRoot.transform, "V15_NearSwayHook_BossHigh", "large_crane_hook_v4.png", new Vector2(151.5f, 2.55f), new Vector2(1.0f, 2.48f), new Color(0.76f, 0.62f, 0.43f, 0.44f), 15);
        AddSway(hookBoss, 1.9f, 6.6f, new Vector2(0.016f, 0.006f));

        railTutorial.transform.localRotation = Quaternion.Euler(0f, 0f, -0.25f);
        railTrap.transform.localRotation = Quaternion.Euler(0f, 0f, 0.22f);
        railBoss.transform.localRotation = Quaternion.Euler(0f, 0f, 0.18f);
    }

    private static void CreateSystemPolishReadability(Transform parent)
    {
        GameObject root = NewChild(parent, "BG_SystemPolish_Readability");

        GameObject routeRoot = NewChild(root.transform, "BG_SystemPolish_RouteFocus");
        ParallaxLayer2D routeParallax = routeRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(routeParallax, "parallaxFactor", Vector2.zero);
        AddRouteFocusBand(routeRoot.transform, "SystemPolish_RouteFocus_Awakening", new Vector2(8.5f, -2.62f), new Vector2(16.8f, 0.72f), new Color(0.03f, 0.028f, 0.022f, 0.28f));
        AddRouteFocusBand(routeRoot.transform, "SystemPolish_RouteFocus_Movement", new Vector2(30.6f, -2.66f), new Vector2(25.2f, 0.72f), new Color(0.03f, 0.028f, 0.022f, 0.24f));
        AddRouteFocusBand(routeRoot.transform, "SystemPolish_RouteFocus_Jump", new Vector2(52.3f, -2.35f), new Vector2(18.4f, 0.64f), new Color(0.02f, 0.032f, 0.028f, 0.22f));
        AddRouteFocusBand(routeRoot.transform, "SystemPolish_RouteFocus_FirstEnemy", new Vector2(74.1f, -2.58f), new Vector2(23.8f, 0.72f), new Color(0.034f, 0.028f, 0.02f, 0.25f));
        AddRouteFocusBand(routeRoot.transform, "SystemPolish_RouteFocus_Trap", new Vector2(100.0f, -2.58f), new Vector2(27.2f, 0.72f), new Color(0.018f, 0.034f, 0.036f, 0.28f));
        AddRouteFocusBand(routeRoot.transform, "SystemPolish_RouteFocus_Charge", new Vector2(120.0f, -2.6f), new Vector2(12.0f, 0.68f), new Color(0.018f, 0.036f, 0.024f, 0.24f));
        AddRouteFocusBand(routeRoot.transform, "SystemPolish_RouteFocus_Boss", new Vector2(143.0f, -2.56f), new Vector2(34.0f, 0.78f), new Color(0.04f, 0.024f, 0.018f, 0.3f));
        AddRouteFocusBand(routeRoot.transform, "SystemPolish_RouteFocus_Exit", new Vector2(168.0f, -2.6f), new Vector2(16.0f, 0.7f), new Color(0.022f, 0.034f, 0.032f, 0.24f));

        GameObject landmarkRoot = NewChild(root.transform, "BG_SystemPolish_Landmarks");
        ParallaxLayer2D landmarkParallax = landmarkRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(landmarkParallax, "parallaxFactor", new Vector2(0.02f, 0.004f));
        AddSystemPolishHalo(landmarkRoot.transform, "SystemPolish_Landmark_AwakeningWarmStart", new Vector2(4.9f, -0.38f), new Vector2(4.4f, 2.7f), new Color(1f, 0.56f, 0.18f, 0.12f), -36, 1.7f);
        AddSystemPolishHalo(landmarkRoot.transform, "SystemPolish_Landmark_AttackCrateAmber", new Vector2(28.2f, -1.68f), new Vector2(3.4f, 1.35f), new Color(1f, 0.78f, 0.28f, 0.1f), -20, 2.1f);
        AddSystemPolishHalo(landmarkRoot.transform, "SystemPolish_Landmark_JumpLandingGuide", new Vector2(52.1f, -0.72f), new Vector2(7.6f, 3.1f), new Color(0.62f, 0.88f, 0.72f, 0.07f), -42, 1.9f);
        AddSystemPolishHalo(landmarkRoot.transform, "SystemPolish_Landmark_EnemyWarningRed", new Vector2(68.0f, -0.52f), new Vector2(2.9f, 2.0f), new Color(1f, 0.2f, 0.1f, 0.11f), -30, 3.4f);
        AddSystemPolishHalo(landmarkRoot.transform, "SystemPolish_Landmark_ChargeStationGreen", new Vector2(120.0f, -0.34f), new Vector2(4.8f, 3.0f), new Color(0.24f, 1f, 0.62f, 0.15f), -28, 1.45f);
        AddSystemPolishHalo(landmarkRoot.transform, "SystemPolish_Landmark_BossDoorWarning", new Vector2(127.0f, -0.7f), new Vector2(4.3f, 3.4f), new Color(1f, 0.32f, 0.12f, 0.12f), -26, 2.8f);
        AddSystemPolishHalo(landmarkRoot.transform, "SystemPolish_Landmark_ExitGateCool", new Vector2(171.5f, -0.68f), new Vector2(4.2f, 3.2f), new Color(0.34f, 0.74f, 1f, 0.11f), -28, 2.2f);

        GameObject hazardRoot = NewChild(root.transform, "BG_SystemPolish_HazardAndInteract");
        ParallaxLayer2D hazardParallax = hazardRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(hazardParallax, "parallaxFactor", Vector2.zero);
        AddHazardPolishArc(hazardRoot.transform, "SystemPolish_Hazard_ElectricFloorA", new Vector2(95.7f, -1.78f), new Vector2(5.5f, 0.44f), -18);
        AddHazardPolishArc(hazardRoot.transform, "SystemPolish_Hazard_ElectricFloorB", new Vector2(102.05f, -1.78f), new Vector2(5.5f, 0.44f), -18);
        AddSystemPolishHalo(hazardRoot.transform, "SystemPolish_Hazard_CompressorRed_A", new Vector2(106.2f, 0.28f), new Vector2(3.7f, 2.4f), new Color(1f, 0.2f, 0.08f, 0.09f), -24, 4.6f);
        AddSystemPolishHalo(hazardRoot.transform, "SystemPolish_Hazard_CompressorRed_B", new Vector2(111.0f, 0.28f), new Vector2(3.7f, 2.4f), new Color(1f, 0.2f, 0.08f, 0.08f), -24, 4.2f);
        AddSystemPolishHalo(hazardRoot.transform, "SystemPolish_Interact_LoreTerminalAmber", new Vector2(10.2f, -1.62f), new Vector2(2.0f, 1.35f), new Color(1f, 0.74f, 0.24f, 0.12f), -18, 1.8f);
        AddSystemPolishHalo(hazardRoot.transform, "SystemPolish_Interact_PowerSwitchCyan", new Vector2(92.55f, -1.25f), new Vector2(2.3f, 1.7f), new Color(0.36f, 0.96f, 1f, 0.11f), -18, 2.0f);
        AddSystemPolishHalo(hazardRoot.transform, "SystemPolish_Interact_SupplyCrateCyan", new Vector2(52.2f, 1.62f), new Vector2(2.4f, 1.2f), new Color(0.42f, 0.94f, 1f, 0.1f), -12, 1.7f);

        GameObject atmosphereRoot = NewChild(root.transform, "BG_SystemPolish_AtmosphereControl");
        ParallaxLayer2D atmosphereParallax = atmosphereRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(atmosphereParallax, "parallaxFactor", new Vector2(0.04f, 0.01f));
        AddV13DustBand(atmosphereRoot.transform, "SystemPolish_Atmosphere_TopDustWarm", new Vector2(42f, 2.7f), new Vector2(54f, 2.4f), new Color(0.88f, 0.66f, 0.38f, 0.055f), -66, 0.16f, 0.04f, 19f);
        AddV13DustBand(atmosphereRoot.transform, "SystemPolish_Atmosphere_TrapCoolVeil", new Vector2(99f, 0.45f), new Vector2(24f, 3.3f), new Color(0.42f, 0.86f, 0.8f, 0.05f), -40, -0.12f, 0.05f, 14f);
        AddV13DustBand(atmosphereRoot.transform, "SystemPolish_Atmosphere_BossHeatVeil", new Vector2(143f, 0.34f), new Vector2(34f, 3.2f), new Color(1f, 0.48f, 0.2f, 0.048f), -38, 0.1f, 0.045f, 15f);
        AddV13SteamJet(atmosphereRoot.transform, "SystemPolish_Atmosphere_BossSteamLeft", new Vector2(132.2f, -1.66f), -18);
        AddV13SteamJet(atmosphereRoot.transform, "SystemPolish_Atmosphere_BossSteamRight", new Vector2(151.0f, -1.62f), -18);
        AddV13LightningBurst(atmosphereRoot.transform, "SystemPolish_Atmosphere_BossCeilingArc", new Vector2(143.6f, 2.35f), new Vector2(1.8f, 0.5f), -24, -7f, 0.78f);
    }

    private static void CreateDynamicPolishV16(Transform parent)
    {
        GameObject root = NewChild(parent, "BG_DynamicPolish_V16");

        GameObject farRoot = NewChild(root.transform, "BG_DynamicPolish_V16_Far");
        ParallaxLayer2D farParallax = farRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(farParallax, "parallaxFactor", new Vector2(0.026f, 0.006f));
        AddV16DustDrift(farRoot.transform, "V16_FarDust_AwakeningWarm", new Vector2(7.8f, 1.55f), new Vector2(18f, 5.4f), new Color(0.9f, 0.68f, 0.38f, 0.052f), -126, 0.12f, 0.04f, 18f);
        AddV16DustDrift(farRoot.transform, "V16_FarDust_MoveAndJump", new Vector2(38.5f, 1.55f), new Vector2(34f, 5.2f), new Color(0.78f, 0.78f, 0.52f, 0.05f), -126, -0.14f, 0.04f, 17f);
        AddV16DustDrift(farRoot.transform, "V16_FarDust_TrapCool", new Vector2(99.2f, 1.45f), new Vector2(30f, 5.5f), new Color(0.45f, 0.82f, 0.76f, 0.056f), -126, 0.13f, 0.045f, 15f);
        AddV16DustDrift(farRoot.transform, "V16_FarDust_BossHeat", new Vector2(143.5f, 1.55f), new Vector2(38f, 5.8f), new Color(1f, 0.5f, 0.22f, 0.048f), -126, -0.12f, 0.035f, 16f);
        AddV16DustDrift(farRoot.transform, "V16_FarDust_ExitSmoke", new Vector2(168f, 1.35f), new Vector2(16f, 4.8f), new Color(0.74f, 0.72f, 0.58f, 0.048f), -126, 0.1f, 0.035f, 18f);
        AddV13ConveyorPulse(farRoot.transform, "V16_FarConveyorPulse_Move", new Vector2(29.5f, 0.74f), new Vector2(6.2f, 0.36f), new Vector2(2.8f, 0f), -118, 3.0f);
        AddV13ConveyorPulse(farRoot.transform, "V16_FarConveyorPulse_Trap", new Vector2(99.2f, 0.82f), new Vector2(7.4f, 0.38f), new Vector2(3.0f, 0f), -118, 2.45f);
        AddV13ConveyorPulse(farRoot.transform, "V16_FarConveyorPulse_Boss", new Vector2(145.4f, 0.82f), new Vector2(7.8f, 0.4f), new Vector2(3.1f, 0f), -118, 2.75f);
        AddV13RotatingFan(farRoot.transform, "V16_FarRotator_EnemyWallFan", new Vector2(73.2f, 2.22f), 1.42f, -110, -5.8f);
        AddV13RotatingFan(farRoot.transform, "V16_FarRotator_BossGearSlow", new Vector2(140.2f, 2.28f), 1.9f, -110, 4.4f);

        GameObject midRoot = NewChild(root.transform, "BG_DynamicPolish_V16_Mid");
        ParallaxLayer2D midParallax = midRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(midParallax, "parallaxFactor", new Vector2(0.062f, 0.012f));
        AddV16PulseHalo(midRoot.transform, "V16_MidPulse_AwakeningPanel", new Vector2(6.4f, -0.22f), new Vector2(2.5f, 1.65f), new Color(1f, 0.58f, 0.18f, 0.095f), -38, 1.8f);
        AddV16ElectricAccent(midRoot.transform, "V16_MidElectric_AwakeningTopCable", new Vector2(6.9f, 1.64f), new Vector2(0.72f, 0.28f), -20, -12f, 1.55f);
        AddV13SteamJet(midRoot.transform, "V16_MidSteam_JumpBrokenPipe", new Vector2(54.8f, -1.32f), -23);
        AddV16PulseHalo(midRoot.transform, "V16_MidPulse_EnemyRedLamp", new Vector2(67.7f, 0.78f), new Vector2(1.7f, 1.35f), new Color(1f, 0.22f, 0.1f, 0.105f), -32, 2.7f);
        AddV16ElectricAccent(midRoot.transform, "V16_MidElectric_EnemyWall", new Vector2(68.4f, 0.46f), new Vector2(0.84f, 0.28f), -22, 8f, 1.28f);
        AddV16ElectricAccent(midRoot.transform, "V16_MidElectric_TrapBlueArcA", new Vector2(95.0f, 0.72f), new Vector2(1.6f, 0.44f), -22, -7f, 0.88f);
        AddV16ElectricAccent(midRoot.transform, "V16_MidElectric_TrapBlueArcB", new Vector2(105.4f, 0.52f), new Vector2(1.34f, 0.38f), -22, 12f, 0.92f);
        AddV16DustDrift(midRoot.transform, "V16_MidHeatHaze_TrapCompressor", new Vector2(106.8f, -0.52f), new Vector2(8.8f, 2.25f), new Color(0.5f, 0.9f, 0.82f, 0.048f), -34, 0.06f, 0.05f, 10.5f);
        AddV16PulseHalo(midRoot.transform, "V16_MidPulse_ChargeStationCyan", new Vector2(116.9f, -0.32f), new Vector2(4.1f, 2.45f), new Color(0.28f, 1f, 0.72f, 0.14f), -34, 1.38f);
        AddV16ScanBeam(midRoot.transform, "V16_MidScan_ChargeStation", new Vector2(116.9f, -0.32f), new Vector2(0.42f, 2.4f), new Vector2(0.0f, 0.72f), -24, 2.6f, new Color(0.45f, 1f, 0.78f, 0.2f));
        AddV16PulseHalo(midRoot.transform, "V16_MidPulse_BossRedWarning", new Vector2(143.2f, 0.92f), new Vector2(5.6f, 2.35f), new Color(1f, 0.28f, 0.1f, 0.13f), -34, 3.2f);
        AddV13SteamJet(midRoot.transform, "V16_MidSteam_BossHeatLeft", new Vector2(132.0f, -1.28f), -22);
        AddV13SteamJet(midRoot.transform, "V16_MidSteam_BossHeatRight", new Vector2(151.6f, -1.22f), -22);
        AddV16ElectricAccent(midRoot.transform, "V16_MidElectric_BossCeilingArc", new Vector2(143.8f, 2.45f), new Vector2(1.8f, 0.48f), -22, -10f, 0.82f);
        AddV16ScanBeam(midRoot.transform, "V16_MidScan_ExitGate", new Vector2(170.8f, -0.2f), new Vector2(0.5f, 2.85f), new Vector2(0f, 0.86f), -24, 2.8f, new Color(0.5f, 0.82f, 1f, 0.18f));
        AddV16PulseHalo(midRoot.transform, "V16_MidPulse_ExitWarmFire", new Vector2(164.4f, -0.36f), new Vector2(2.5f, 1.8f), new Color(1f, 0.45f, 0.12f, 0.13f), -36, 4.8f);
        AddV13SteamJet(midRoot.transform, "V16_MidSteam_ExitDoorSmoke", new Vector2(168.2f, -1.28f), -23);

        GameObject nearRoot = NewChild(root.transform, "BG_DynamicPolish_V16_NearSafe");
        ParallaxLayer2D nearParallax = nearRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(nearParallax, "parallaxFactor", Vector2.zero);
        AddV16HighSwayDecor(nearRoot.transform, "V16_NearSwayChain_MoveHigh", new Vector2(22.4f, 3.34f), new Vector2(0.46f, 1.7f), 16, 1.2f, 6.2f);
        AddV16HighSwayDecor(nearRoot.transform, "V16_NearSwayChain_JumpHigh", new Vector2(48.4f, 3.38f), new Vector2(0.48f, 1.86f), 16, 1.5f, 6.6f);
        AddV16HighSwayDecor(nearRoot.transform, "V16_NearSwayChain_TrapHigh", new Vector2(91.8f, 3.42f), new Vector2(0.44f, 1.68f), 16, 1.3f, 6.9f);
        AddV16HighSwayDecor(nearRoot.transform, "V16_NearSwayChain_BossHigh", new Vector2(151.8f, 3.42f), new Vector2(0.52f, 1.95f), 16, 1.6f, 7.1f);
        GameObject flag = AddProvidedEnvironmentV3SpriteChild(nearRoot.transform, "V16_NearSwayFlag_BossHigh", "provided_v3_torn_flag.png", new Vector2(134.8f, 3.18f), new Vector2(1.25f, 1.0f), new Color(0.88f, 0.52f, 0.3f, 0.42f), 16);
        AddSway(flag, 1.8f, 6.0f, new Vector2(0.016f, 0.006f));
        AddV16DustDrift(nearRoot.transform, "V16_NearDust_HighRail", new Vector2(92f, 3.05f), new Vector2(126f, 1.05f), new Color(0.9f, 0.72f, 0.46f, 0.038f), 12, -0.1f, 0.025f, 20f);

        GameObject readableRoot = NewChild(root.transform, "BG_DynamicPolish_V16_GameplayReadable");
        ParallaxLayer2D readableParallax = readableRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(readableParallax, "parallaxFactor", Vector2.zero);
        AddV16DustDrift(readableRoot.transform, "V16_ReadableOilMist_TrapBelow", new Vector2(98.5f, -3.72f), new Vector2(21.0f, 0.62f), new Color(0.18f, 0.36f, 0.3f, 0.105f), -6, 0.18f, 0.025f, 12f);
        AddV16PulseHalo(readableRoot.transform, "V16_ReadableChargeSafeGlow", new Vector2(116.9f, -1.92f), new Vector2(3.1f, 0.86f), new Color(0.3f, 1f, 0.72f, 0.09f), -6, 1.25f);
        AddV16PulseHalo(readableRoot.transform, "V16_ReadableBossWarningFloorGlow", new Vector2(143.2f, -3.64f), new Vector2(23.5f, 0.58f), new Color(1f, 0.3f, 0.1f, 0.085f), -6, 2.6f);
        AddV16PulseHalo(readableRoot.transform, "V16_ReadableExitDoorGlow", new Vector2(170.8f, -1.74f), new Vector2(2.8f, 1.2f), new Color(0.52f, 0.82f, 1f, 0.09f), -8, 2.0f);
    }

    private static void AddV16DustDrift(Transform parent, string name, Vector2 position, Vector2 size, Color color, int order, float driftX, float driftY, float cycleSeconds)
    {
        AddV13DustBand(parent, name, position, size, color, order, driftX, driftY, cycleSeconds);
    }

    private static void AddV16PulseHalo(Transform parent, string name, Vector2 position, Vector2 size, Color color, int order, float flickerSpeed)
    {
        AddV12FlickerHalo(parent, name, position, size, color, order, flickerSpeed);
    }

    private static void AddV16ElectricAccent(Transform parent, string name, Vector2 position, Vector2 size, int order, float rotation, float maxDelay)
    {
        AddV13LightningBurst(parent, name, position, size, order, rotation, maxDelay);
    }

    private static void AddV16ScanBeam(Transform parent, string name, Vector2 position, Vector2 size, Vector2 travel, int order, float cycleSeconds, Color color)
    {
        GameObject scan = AddEffectsV2SpriteChild(parent, name, "fxv2_scan_beam.png", position, size, color, order);
        LoopingBackgroundMotion2D motion = scan.AddComponent<LoopingBackgroundMotion2D>();
        SetVector2(motion, "travel", travel);
        SetFloat(motion, "cycleSeconds", cycleSeconds);
        SetFloat(motion, "minAlpha", 0.01f);
        SetFloat(motion, "maxAlpha", color.a);
        SetFloat(motion, "scalePulse", 0.04f);
    }

    private static void AddV16HighSwayDecor(Transform parent, string name, Vector2 position, Vector2 size, int order, float amplitudeDegrees, float cycleSeconds)
    {
        GameObject chain = AddEnvironmentV11SpriteChild(parent, name, "envv11_chain_hook.png", position, size, new Color(0.76f, 0.64f, 0.48f, 0.38f), order);
        AddSway(chain, amplitudeDegrees, cycleSeconds, new Vector2(0.014f, 0.006f));
    }

    private static void AddRouteFocusBand(Transform parent, string name, Vector2 position, Vector2 size, Color color)
    {
        GameObject shadow = AddEffectsV4SpriteChild(parent, name, "fxv4_oil_haze.png", position, size, color, -8);
        AmbientDrift2D drift = shadow.AddComponent<AmbientDrift2D>();
        SetFloat(drift, "driftX", 0.04f);
        SetFloat(drift, "driftY", 0.01f);
        SetFloat(drift, "cycleSeconds", 18f);
        SetFloat(drift, "minAlpha", color.a * 0.72f);
        SetFloat(drift, "maxAlpha", color.a);
    }

    private static void AddSystemPolishHalo(Transform parent, string name, Vector2 position, Vector2 size, Color color, int order, float flickerSpeed)
    {
        GameObject halo = AddEffectsV5SpriteChild(parent, name, "fxv5_lamp_halo_amber.png", position, size, color, order);
        SpriteFlicker2D flicker = halo.AddComponent<SpriteFlicker2D>();
        SetFloat(flicker, "minAlpha", color.a * 0.35f);
        SetFloat(flicker, "maxAlpha", color.a);
        SetFloat(flicker, "flickerSpeed", flickerSpeed);
    }

    private static void AddHazardPolishArc(Transform parent, string name, Vector2 position, Vector2 size, int order)
    {
        GameObject arc = AddEffectsV5SpriteChild(parent, name, "fxv5_electric_spark_frame.png", position, size, new Color(0.48f, 0.94f, 1f, 0f), order);
        ElectricArcFlicker2D flicker = arc.AddComponent<ElectricArcFlicker2D>();
        SetFloat(flicker, "minDelay", 0.18f);
        SetFloat(flicker, "maxDelay", 0.86f);
        SetFloat(flicker, "flashSeconds", 0.055f);
        SetFloat(flicker, "maxAlpha", 0.42f);
        SetFloat(flicker, "scalePulse", 0.14f);
        SetVector2(flicker, "jitter", new Vector2(0.035f, 0.015f));
    }

    private static void AddProvidedPipeWithSteam(Transform parent, string name, Vector2 position, Vector2 size, Color color, int order)
    {
        AddProvidedEnvironmentV2SpriteChild(parent, name, "provided_v2_pipe_horizontal.png", position, size, color, order);
        AddV10Steam(parent, name + "_Steam", position + new Vector2(size.x * 0.28f, -0.58f), new Color(0.78f, 0.74f, 0.6f, 0.11f), order + 5);
        AddV12Lightning(parent, name + "_ElectricTick", position + new Vector2(-size.x * 0.24f, -0.04f), new Vector2(0.54f, 0.22f), order + 7, -6f, 2.1f);
    }

    private static void CreateJumpRouteBackgroundPolish(Transform parent)
    {
        GameObject root = NewChild(parent, "JumpRoute_BackgroundPolish_BrokenPlatforms");

        AddProvidedEnvironmentV4SpriteChild(root.transform, "JumpRoute_Background_MachineFrame", "large_machine_frame_v4.png", new Vector2(50.2f, 0.55f), new Vector2(8.2f, 4.1f), new Color(0.48f, 0.56f, 0.46f, 0.18f), -128);
        AddEnvironmentV11SpriteChild(root.transform, "JumpRoute_Background_RepairPlate", "envv11_repair_wall_plate.png", new Vector2(47.4f, -0.2f), new Vector2(2.4f, 1.55f), new Color(0.62f, 0.68f, 0.54f, 0.22f), -62);
        AddProvidedEnvironmentV3SpriteChild(root.transform, "JumpRoute_Background_RightBrokenWindow", "provided_v3_broken_window.png", new Vector2(59.2f, 0.82f), new Vector2(2.45f, 1.75f), new Color(0.48f, 0.58f, 0.5f, 0.2f), -118);
        AddProvidedEnvironmentV2SpriteChild(root.transform, "JumpRoute_Background_LeftTruss", "provided_v2_truss_support.png", new Vector2(42.7f, -0.45f), new Vector2(1.2f, 2.9f), new Color(0.58f, 0.55f, 0.44f, 0.34f), -48);

        AddV16PulseHalo(root.transform, "JumpRoute_Background_LowerRoadWarmBacklight", new Vector2(46.7f, -1.58f), new Vector2(4.2f, 1.8f), new Color(1f, 0.56f, 0.18f, 0.085f), -34, 1.65f);
        AddV16PulseHalo(root.transform, "JumpRoute_Background_UpperDeckSoftGlow", new Vector2(52.2f, 0.78f), new Vector2(4.6f, 1.45f), new Color(1f, 0.64f, 0.2f, 0.06f), -36, 1.9f);
        AddV16PulseHalo(root.transform, "JumpRoute_Background_RightRoadCoolGlow", new Vector2(56.6f, -1.64f), new Vector2(3.6f, 1.35f), new Color(0.42f, 0.95f, 0.86f, 0.06f), -36, 1.55f);
        AddV16DustDrift(root.transform, "JumpRoute_Background_LowOilMist", new Vector2(52.7f, -3.62f), new Vector2(16.4f, 0.86f), new Color(0.28f, 0.52f, 0.42f, 0.11f), -8, 0.16f, 0.025f, 13f);
        AddV16DustDrift(root.transform, "JumpRoute_Background_FineDust", new Vector2(51.5f, 0.42f), new Vector2(13.8f, 3.4f), new Color(0.76f, 0.7f, 0.46f, 0.06f), -70, -0.1f, 0.045f, 16f);
        AddV13SteamJet(root.transform, "JumpRoute_Background_SteamUnderLeft", new Vector2(45.1f, -2.7f), -22);
        AddV13SteamJet(root.transform, "JumpRoute_Background_SteamUnderRight", new Vector2(56.4f, -2.72f), -22);
        AddV16ElectricAccent(root.transform, "JumpRoute_Background_WallElectricTick", new Vector2(49.1f, 0.54f), new Vector2(0.82f, 0.28f), -22, -10f, 1.25f);
        AddV16ScanBeam(root.transform, "JumpRoute_Background_UpperScanBeam", new Vector2(52.2f, 0.18f), new Vector2(0.36f, 1.95f), new Vector2(0f, 0.52f), -24, 2.9f, new Color(0.46f, 0.96f, 1f, 0.14f));

        GameObject chainA = AddEnvironmentV11SpriteChild(root.transform, "JumpRoute_Background_HighChain_A", "envv11_chain_hook.png", new Vector2(43.2f, 2.98f), new Vector2(0.42f, 1.55f), new Color(0.76f, 0.64f, 0.46f, 0.42f), 16);
        AddSway(chainA, 1.4f, 6.4f, new Vector2(0.012f, 0.005f));
        GameObject chainB = AddEnvironmentV11SpriteChild(root.transform, "JumpRoute_Background_HighChain_B", "envv11_chain_hook.png", new Vector2(45.0f, 3.04f), new Vector2(0.4f, 1.65f), new Color(0.76f, 0.64f, 0.46f, 0.38f), 16);
        AddSway(chainB, 1.2f, 6.9f, new Vector2(0.01f, 0.005f));
        AddEnvironmentV7SpriteChild(root.transform, "JumpRoute_Background_SmallStatusLamp", "envv7_indicator_lamp_amber.png", new Vector2(48.4f, 0.52f), new Vector2(0.18f, 0.18f), new Color(1f, 0.62f, 0.16f, 0.76f), -18).AddComponent<SpriteFlicker2D>();
    }

    private static void CreateBackgroundAtmosphereV12(Transform parent)
    {
        GameObject root = NewChild(parent, "BG_BackgroundAtmosphere_V12");

        GameObject farRoot = NewChild(root.transform, "BG_FarAtmosphere_V12");
        ParallaxLayer2D farParallax = farRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(farParallax, "parallaxFactor", new Vector2(0.028f, 0.006f));

        AddV12DustBand(farRoot.transform, "V12_FarDust_Awakening", new Vector2(18f, 1.35f), new Vector2(30f, 7.2f), new Color(0.78f, 0.72f, 0.52f, 0.045f), -118, 0.16f, 0.05f, 17f);
        AddV12DustBand(farRoot.transform, "V12_FarDust_ChargeHall", new Vector2(113f, 1.45f), new Vector2(34f, 7.4f), new Color(0.58f, 0.78f, 0.58f, 0.052f), -118, -0.14f, 0.055f, 18f);
        AddV12DustBand(farRoot.transform, "V12_FarDust_BossHall", new Vector2(144f, 1.35f), new Vector2(42f, 7.4f), new Color(0.78f, 0.68f, 0.48f, 0.048f), -118, 0.12f, 0.045f, 19f);

        AddV12Lightning(farRoot.transform, "V12_FarLightning_TutorialWall", new Vector2(31.5f, 1.82f), new Vector2(1.45f, 0.42f), -76, 0.18f, 1.9f);
        AddV12Lightning(farRoot.transform, "V12_FarLightning_TrapWall", new Vector2(96.9f, 1.62f), new Vector2(1.75f, 0.48f), -76, -8f, 1.55f);
        AddV12Lightning(farRoot.transform, "V12_FarLightning_BossWall", new Vector2(136.8f, 1.9f), new Vector2(1.65f, 0.46f), -76, 12f, 2.2f);

        GameObject midRoot = NewChild(root.transform, "BG_MidAtmosphere_V12");
        ParallaxLayer2D midParallax = midRoot.AddComponent<ParallaxLayer2D>();
        SetVector2(midParallax, "parallaxFactor", new Vector2(0.075f, 0.015f));

        AddV12Lightning(midRoot.transform, "V12_MidLightning_ChargeCable_Left", new Vector2(110.1f, 0.92f), new Vector2(0.92f, 0.34f), -28, 18f, 1.15f);
        AddV12Lightning(midRoot.transform, "V12_MidLightning_ChargeCable_Right", new Vector2(117.3f, 0.72f), new Vector2(0.88f, 0.32f), -30, -14f, 1.35f);
        AddV12Lightning(midRoot.transform, "V12_MidLightning_ExitCable", new Vector2(167.4f, 0.88f), new Vector2(1.05f, 0.34f), -28, 6f, 1.3f);

        AddV12FlickerHalo(midRoot.transform, "V12_FlickerLight_AwakeningWall", new Vector2(8.8f, 0.92f), new Vector2(2.0f, 1.35f), new Color(1f, 0.56f, 0.18f, 0.11f), -34, 2.0f);
        AddV12FlickerHalo(midRoot.transform, "V12_FlickerLight_ChargeSide", new Vector2(118.1f, 0.74f), new Vector2(2.35f, 1.55f), new Color(0.42f, 1f, 0.66f, 0.105f), -36, 1.65f);
        AddV12FlickerHalo(midRoot.transform, "V12_FlickerLight_BossDoor", new Vector2(154.8f, 0.72f), new Vector2(2.6f, 1.45f), new Color(1f, 0.32f, 0.16f, 0.095f), -34, 2.7f);
        AddV12FlickerHalo(midRoot.transform, "V12_ColorDepth_Amber_Awakening", new Vector2(6.6f, -0.2f), new Vector2(4.8f, 2.6f), new Color(1f, 0.56f, 0.18f, 0.07f), -90, 1.35f);
        AddV12FlickerHalo(midRoot.transform, "V12_ColorDepth_Blue_Trap", new Vector2(90.4f, -0.36f), new Vector2(5.8f, 2.4f), new Color(0.18f, 0.7f, 1f, 0.06f), -90, 1.8f);
        AddV12FlickerHalo(midRoot.transform, "V12_ColorDepth_Green_Charge", new Vector2(114.0f, -0.22f), new Vector2(4.5f, 2.2f), new Color(0.22f, 1f, 0.62f, 0.055f), -92, 1.45f);

        AddV12DustBand(midRoot.transform, "V12_MidDust_Movement", new Vector2(35f, 0.1f), new Vector2(28f, 3.1f), new Color(0.84f, 0.72f, 0.48f, 0.055f), -42, 0.22f, 0.08f, 12f);
        AddV12DustBand(midRoot.transform, "V12_MidDust_Trap", new Vector2(92f, 0.0f), new Vector2(25f, 3.2f), new Color(0.54f, 0.78f, 0.66f, 0.06f), -42, -0.2f, 0.07f, 11f);
        AddV12DustBand(midRoot.transform, "V12_MidDust_Exit", new Vector2(164f, 0.12f), new Vector2(24f, 3.0f), new Color(0.78f, 0.68f, 0.48f, 0.05f), -42, 0.18f, 0.07f, 13f);
    }

    private static void AddBackgroundSteamV2(Transform parent, string name, Vector2 position, int order)
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject puff = AddEffectsV2SpriteChild(parent, name + $"_Puff_{i:00}", "fxv2_steam_puff.png", position + new Vector2(-0.22f + i * 0.22f, 0.08f + i * 0.08f), new Vector2(0.82f, 0.62f), new Color(0.82f, 0.78f, 0.66f, 0.15f), order);
            SteamPuff2D steam = puff.AddComponent<SteamPuff2D>();
            SetFloat(steam, "riseHeight", 0.35f + i * 0.08f);
            SetFloat(steam, "pulseSpeed", 0.8f + i * 0.16f);
            SetFloat(steam, "minAlpha", 0.02f);
            SetFloat(steam, "maxAlpha", 0.16f);
        }
    }

    private static void AddBackgroundRotatorV8(Transform parent, string name, Vector2 center, float size, int order, float speed)
    {
        GameObject fan = AddEnvironmentV8SpriteChild(parent, name, "envv8_far_gear_soft.png", center, new Vector2(size, size), new Color(0.58f, 0.68f, 0.56f, 0.18f), order);
        SimpleRotator2D rotator = fan.AddComponent<SimpleRotator2D>();
        SetFloat(rotator, "degreesPerSecond", speed);
    }

    private static void AddTransitionShadowV8(Transform parent, string name, float x)
    {
        AddEnvironmentV8SpriteChild(parent, name, "envv8_service_panel_dim.png", new Vector2(x, 0.35f), new Vector2(5.2f, 6.2f), new Color(0.18f, 0.24f, 0.2f, 0.055f), -118);
    }

    private static void AddSeamBreakerV8(Transform parent, string name, float x, Color shadowColor)
    {
        AddEnvironmentV8SpriteChild(parent, name + "_SoftBackPanel", "envv8_service_panel_dim.png", new Vector2(x, 0.46f), new Vector2(8.4f, 6.95f), new Color(shadowColor.r, shadowColor.g, shadowColor.b, 0.085f), -116);
        AddEnvironmentV8SpriteChild(parent, name + "_LeftFeather", "envv8_service_panel_dim.png", new Vector2(x - 2.65f, 0.44f), new Vector2(2.6f, 6.55f), new Color(0.12f, 0.17f, 0.14f, 0.042f), -114);
        AddEnvironmentV8SpriteChild(parent, name + "_RightFeather", "envv8_service_panel_dim.png", new Vector2(x + 2.65f, 0.44f), new Vector2(2.6f, 6.55f), new Color(0.12f, 0.17f, 0.14f, 0.042f), -114);
        AddEnvironmentV8SpriteChild(parent, name + "_CenterColorWash", "envv8_service_panel_dim.png", new Vector2(x, 0.42f), new Vector2(6.8f, 6.75f), new Color(0.2f, 0.28f, 0.22f, 0.05f), -86);
        GameObject leftRiser = AddEnvironmentV8SpriteChild(parent, name + "_VerticalPipe_Left", "envv8_top_pipe_soft.png", new Vector2(x - 0.62f, 0.7f), new Vector2(6.2f, 0.28f), new Color(0.46f, 0.44f, 0.34f, 0.16f), -74);
        leftRiser.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        GameObject rightRiser = AddEnvironmentV8SpriteChild(parent, name + "_VerticalPipe_Right", "envv8_top_pipe_soft.png", new Vector2(x + 0.62f, 0.7f), new Vector2(6.2f, 0.24f), new Color(0.34f, 0.42f, 0.32f, 0.11f), -75);
        rightRiser.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        AddEnvironmentV8SpriteChild(parent, name + "_TopPipe_Wide", "envv8_top_pipe_soft.png", new Vector2(x, 2.42f), new Vector2(9.1f, 0.64f), new Color(0.62f, 0.58f, 0.44f, 0.16f), -72);
        AddEnvironmentV8SpriteChild(parent, name + "_MidPipe_Faint", "envv8_top_pipe_soft.png", new Vector2(x, 0.52f), new Vector2(7.4f, 0.32f), new Color(0.42f, 0.48f, 0.34f, 0.09f), -84);
        AddEnvironmentV8SpriteChild(parent, name + "_LowPipe_Fade", "envv8_top_pipe_soft.png", new Vector2(x, -1.02f), new Vector2(7.8f, 0.42f), new Color(0.38f, 0.35f, 0.27f, 0.075f), -96);

        GameObject dust = AddEffectsV5SpriteChild(parent, name + "_DustBlend_Wide", "fxv5_fine_dust_motes.png", new Vector2(x, 0.55f), new Vector2(17.2f, 6.7f), new Color(0.72f, 0.66f, 0.48f, 0.09f), -82);
        AmbientDrift2D dustDrift = dust.AddComponent<AmbientDrift2D>();
        SetFloat(dustDrift, "driftX", 0.12f);
        SetFloat(dustDrift, "driftY", 0.045f);
        SetFloat(dustDrift, "cycleSeconds", 16.5f);
        SetFloat(dustDrift, "minAlpha", 0.012f);
        SetFloat(dustDrift, "maxAlpha", 0.08f);

        GameObject veil = AddEffectsV5SpriteChild(parent, name + "_VerticalDustVeil", "fxv5_long_falling_dust_veil.png", new Vector2(x + 0.18f, 0.62f), new Vector2(9.4f, 7.75f), new Color(0.78f, 0.7f, 0.5f, 0.095f), -80);
        AmbientDrift2D veilDrift = veil.AddComponent<AmbientDrift2D>();
        SetFloat(veilDrift, "driftX", -0.04f);
        SetFloat(veilDrift, "driftY", -0.12f);
        SetFloat(veilDrift, "cycleSeconds", 14.5f);
        SetFloat(veilDrift, "minAlpha", 0.01f);
        SetFloat(veilDrift, "maxAlpha", 0.095f);
    }

    private static void AddSparkShowerV2(Transform parent, string name, Vector2 origin, int order)
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject spark = AddEffectsV2SpriteChild(parent, name + $"_{i:00}", "fxv2_spark_shower.png", origin + new Vector2(i * 0.18f, -i * 0.06f), new Vector2(0.72f, 0.46f), new Color(1f, 0.64f, 0.18f, 0.32f), order);
            LoopingBackgroundMotion2D motion = spark.AddComponent<LoopingBackgroundMotion2D>();
            SetVector2(motion, "travel", new Vector2(0.2f + i * 0.12f, -0.7f - i * 0.16f));
            SetFloat(motion, "cycleSeconds", 1.6f + i * 0.24f);
            SetFloat(motion, "minAlpha", 0f);
            SetFloat(motion, "maxAlpha", 0.36f);
            SetFloat(motion, "scalePulse", 0.18f);
            SetFloat(motion, "phaseOffset", i * 0.22f);
        }
    }

    private static void AddV9LampHalo(Transform parent, string name, Vector2 position, Vector2 size, Color color, int order)
    {
        GameObject halo = AddEffectsV3SpriteChild(parent, name, "fxv3_lamp_halo_soft.png", position, size, color, order);
        SpriteFlicker2D flicker = halo.AddComponent<SpriteFlicker2D>();
        SetFloat(flicker, "minAlpha", 0.06f);
        SetFloat(flicker, "maxAlpha", Mathf.Max(0.08f, color.a));
        SetFloat(flicker, "flickerSpeed", 1.8f);
    }

    private static void AddV9WarningLamp(Transform parent, string name, Vector2 position, Vector2 size, Color color, int order)
    {
        GameObject lamp = AddEnvironmentV9SpriteChild(parent, name, "envv9_warning_lamp_shell.png", position, size, color, order);
        SpriteFlicker2D flicker = lamp.AddComponent<SpriteFlicker2D>();
        SetFloat(flicker, "minAlpha", 0.22f);
        SetFloat(flicker, "maxAlpha", color.a);
        SetFloat(flicker, "flickerSpeed", 4.8f);
    }

    private static void AddV9ElectricArc(Transform parent, string name, Vector2 position, Vector2 size, int order)
    {
        GameObject arc = AddEffectsV3SpriteChild(parent, name, "fxv3_electric_arc_small.png", position, size, new Color(0.5f, 0.9f, 1f, 0.0f), order);
        ElectricArcFlicker2D flicker = arc.AddComponent<ElectricArcFlicker2D>();
        SetFloat(flicker, "minDelay", 0.42f);
        SetFloat(flicker, "maxDelay", 1.9f);
        SetFloat(flicker, "flashSeconds", 0.07f);
        SetFloat(flicker, "maxAlpha", 0.42f);
        SetFloat(flicker, "scalePulse", 0.18f);
    }

    private static void AddV9Steam(Transform parent, string name, Vector2 position, Color color, int order)
    {
        GameObject steam = AddEffectsV3SpriteChild(parent, name, "fxv3_thin_steam.png", position, new Vector2(0.82f, 1.28f), color, order);
        SteamPuff2D puff = steam.AddComponent<SteamPuff2D>();
        SetFloat(puff, "riseHeight", 0.32f);
        SetFloat(puff, "pulseSpeed", 0.72f);
        SetFloat(puff, "minAlpha", 0.02f);
        SetFloat(puff, "maxAlpha", Mathf.Max(0.08f, color.a));
    }

    private static void AddV9OilHaze(Transform parent, string name, Vector2 position, Vector2 size, Color color, int order)
    {
        GameObject haze = AddEffectsV3SpriteChild(parent, name, "fxv3_oil_haze.png", position, size, color, order);
        AmbientDrift2D drift = haze.AddComponent<AmbientDrift2D>();
        SetFloat(drift, "driftX", 0.28f);
        SetFloat(drift, "driftY", 0.04f);
        SetFloat(drift, "cycleSeconds", 9f);
        SetFloat(drift, "minAlpha", 0.02f);
        SetFloat(drift, "maxAlpha", color.a);
    }

    private static void AddV9ChargePulse(Transform parent, string name, Vector2 position, Vector2 size, int order)
    {
        GameObject pulse = AddEffectsV3SpriteChild(parent, name, "fxv3_green_charge_pulse.png", position, size, new Color(0.4f, 1f, 0.72f, 0.12f), order);
        LoopingBackgroundMotion2D motion = pulse.AddComponent<LoopingBackgroundMotion2D>();
        SetVector2(motion, "travel", new Vector2(0f, 0.18f));
        SetFloat(motion, "cycleSeconds", 2.8f);
        SetFloat(motion, "minAlpha", 0.03f);
        SetFloat(motion, "maxAlpha", 0.16f);
        SetFloat(motion, "scalePulse", 0.08f);
    }

    private static void AddV10Steam(Transform parent, string name, Vector2 position, Color color, int order)
    {
        GameObject steam = AddEffectsV4SpriteChild(parent, name, "fxv4_thin_steam_wisp.png", position, new Vector2(0.78f, 1.25f), color, order);
        SteamPuff2D puff = steam.AddComponent<SteamPuff2D>();
        SetFloat(puff, "riseHeight", 0.42f);
        SetFloat(puff, "pulseSpeed", 0.62f);
        SetFloat(puff, "minAlpha", 0.02f);
        SetFloat(puff, "maxAlpha", Mathf.Max(0.08f, color.a));
    }

    private static void AddV10ElectricArc(Transform parent, string name, Vector2 position, Vector2 size, int order)
    {
        GameObject arc = AddEffectsV4SpriteChild(parent, name, "fxv4_electric_arc_frame.png", position, size, new Color(0.55f, 0.92f, 1f, 0f), order);
        ElectricArcFlicker2D flicker = arc.AddComponent<ElectricArcFlicker2D>();
        SetFloat(flicker, "minDelay", 0.32f);
        SetFloat(flicker, "maxDelay", 1.6f);
        SetFloat(flicker, "flashSeconds", 0.065f);
        SetFloat(flicker, "maxAlpha", 0.38f);
        SetFloat(flicker, "scalePulse", 0.16f);
    }

    private static void AddV10WarningBlink(Transform parent, string name, Vector2 position, float size, int order)
    {
        GameObject blink = AddEffectsV4SpriteChild(parent, name, "fxv4_warning_blink_red.png", position, new Vector2(size, size), new Color(1f, 0.24f, 0.1f, 0.22f), order);
        SpriteFlicker2D flicker = blink.AddComponent<SpriteFlicker2D>();
        SetFloat(flicker, "minAlpha", 0.04f);
        SetFloat(flicker, "maxAlpha", 0.22f);
        SetFloat(flicker, "flickerSpeed", 2.6f);
    }

    private static void AddV11Lamp(Transform parent, string name, Vector2 position, Vector2 size, int order)
    {
        GameObject halo = AddEffectsV5SpriteChild(parent, name + "_Halo", "fxv5_lamp_halo_amber.png", position + new Vector2(0f, -0.08f), size * 2.0f, new Color(1f, 0.68f, 0.2f, 0.16f), order - 1);
        SpriteFlicker2D haloFlicker = halo.AddComponent<SpriteFlicker2D>();
        SetFloat(haloFlicker, "minAlpha", 0.04f);
        SetFloat(haloFlicker, "maxAlpha", 0.16f);
        SetFloat(haloFlicker, "flickerSpeed", 2.2f);

        GameObject lamp = AddEnvironmentV11SpriteChild(parent, name, "envv11_hanging_lamp.png", position, size, new Color(0.95f, 0.84f, 0.62f, 0.72f), order);
        SpriteFlicker2D lampFlicker = lamp.AddComponent<SpriteFlicker2D>();
        SetFloat(lampFlicker, "minAlpha", 0.44f);
        SetFloat(lampFlicker, "maxAlpha", 0.76f);
        SetFloat(lampFlicker, "flickerSpeed", 1.65f);
    }

    private static void AddV11StreetLamp(Transform parent, string name, Vector2 position, int order)
    {
        GameObject halo = AddEffectsV5SpriteChild(parent, name + "_Halo", "fxv5_lamp_halo_amber.png", position + new Vector2(0.33f, 0.82f), new Vector2(1.5f, 1.5f), new Color(1f, 0.68f, 0.2f, 0.12f), order - 1);
        SpriteFlicker2D haloFlicker = halo.AddComponent<SpriteFlicker2D>();
        SetFloat(haloFlicker, "minAlpha", 0.035f);
        SetFloat(haloFlicker, "maxAlpha", 0.13f);
        SetFloat(haloFlicker, "flickerSpeed", 1.9f);

        GameObject lamp = AddEnvironmentV11SpriteChild(parent, name, "envv11_street_lamp.png", position, new Vector2(0.74f, 2.05f), new Color(0.9f, 0.78f, 0.56f, 0.62f), order);
        SpriteFlicker2D lampFlicker = lamp.AddComponent<SpriteFlicker2D>();
        SetFloat(lampFlicker, "minAlpha", 0.48f);
        SetFloat(lampFlicker, "maxAlpha", 0.66f);
        SetFloat(lampFlicker, "flickerSpeed", 1.35f);
    }

    private static void AddV11ElectricSpark(Transform parent, string name, Vector2 position, Vector2 size, int order)
    {
        GameObject spark = AddEffectsV5SpriteChild(parent, name, "fxv5_electric_spark_frame.png", position, size, new Color(0.55f, 0.95f, 1f, 0f), order);
        ElectricArcFlicker2D flicker = spark.AddComponent<ElectricArcFlicker2D>();
        SetFloat(flicker, "minDelay", 0.24f);
        SetFloat(flicker, "maxDelay", 1.35f);
        SetFloat(flicker, "flashSeconds", 0.055f);
        SetFloat(flicker, "maxAlpha", 0.5f);
        SetFloat(flicker, "scalePulse", 0.12f);
    }

    private static void AddV12Lightning(Transform parent, string name, Vector2 position, Vector2 size, int order, float rotation, float maxDelay)
    {
        GameObject lightning = AddEffectsV5SpriteChild(parent, name, "fxv5_electric_spark_frame.png", position, size, new Color(0.52f, 0.9f, 1f, 0f), order);
        lightning.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        ElectricArcFlicker2D flicker = lightning.AddComponent<ElectricArcFlicker2D>();
        SetFloat(flicker, "minDelay", 0.45f);
        SetFloat(flicker, "maxDelay", maxDelay);
        SetFloat(flicker, "flashSeconds", 0.06f);
        SetFloat(flicker, "maxAlpha", 0.36f);
        SetFloat(flicker, "scalePulse", 0.18f);
        SetVector2(flicker, "jitter", new Vector2(0.03f, 0.018f));
    }

    private static void AddV12DustBand(Transform parent, string name, Vector2 position, Vector2 size, Color color, int order, float driftX, float driftY, float cycleSeconds)
    {
        GameObject dust = AddEffectsV5SpriteChild(parent, name, "fxv5_fine_dust_motes.png", position, size, color, order);
        AmbientDrift2D drift = dust.AddComponent<AmbientDrift2D>();
        SetFloat(drift, "driftX", driftX);
        SetFloat(drift, "driftY", driftY);
        SetFloat(drift, "cycleSeconds", cycleSeconds);
        SetFloat(drift, "minAlpha", color.a * 0.35f);
        SetFloat(drift, "maxAlpha", color.a);
    }

    private static void AddV12FlickerHalo(Transform parent, string name, Vector2 position, Vector2 size, Color color, int order, float flickerSpeed)
    {
        GameObject halo = AddEffectsV5SpriteChild(parent, name, "fxv5_lamp_halo_amber.png", position, size, color, order);
        SpriteFlicker2D flicker = halo.AddComponent<SpriteFlicker2D>();
        SetFloat(flicker, "minAlpha", color.a * 0.28f);
        SetFloat(flicker, "maxAlpha", color.a);
        SetFloat(flicker, "flickerSpeed", flickerSpeed);
    }

    private static void AddV13RotatingFan(Transform parent, string name, Vector2 position, float size, int order, float speed)
    {
        GameObject fan = AddEnvironmentV9SpriteChild(parent, name, "envv9_ventilation_fan.png", position, new Vector2(size, size), new Color(0.58f, 0.68f, 0.54f, 0.2f), order);
        SimpleRotator2D rotator = fan.AddComponent<SimpleRotator2D>();
        SetFloat(rotator, "degreesPerSecond", speed);
    }

    private static void AddV13LightningBurst(Transform parent, string name, Vector2 position, Vector2 size, int order, float rotation, float maxDelay)
    {
        GameObject lightning = AddEffectsV5SpriteChild(parent, name, "fxv5_electric_spark_frame.png", position, size, new Color(0.52f, 0.92f, 1f, 0f), order);
        lightning.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        ElectricArcFlicker2D flicker = lightning.AddComponent<ElectricArcFlicker2D>();
        SetFloat(flicker, "minDelay", 0.2f);
        SetFloat(flicker, "maxDelay", maxDelay);
        SetFloat(flicker, "flashSeconds", 0.075f);
        SetFloat(flicker, "maxAlpha", 0.68f);
        SetFloat(flicker, "scalePulse", 0.32f);
        SetVector2(flicker, "jitter", new Vector2(0.05f, 0.028f));
    }

    private static void AddV13DustBand(Transform parent, string name, Vector2 position, Vector2 size, Color color, int order, float driftX, float driftY, float cycleSeconds)
    {
        GameObject dust = AddEffectsV5SpriteChild(parent, name, "fxv5_fine_dust_motes.png", position, size, color, order);
        AmbientDrift2D drift = dust.AddComponent<AmbientDrift2D>();
        SetFloat(drift, "driftX", driftX);
        SetFloat(drift, "driftY", driftY);
        SetFloat(drift, "cycleSeconds", cycleSeconds);
        SetFloat(drift, "minAlpha", color.a * 0.32f);
        SetFloat(drift, "maxAlpha", color.a);
    }

    private static void AddV13ConveyorPulse(Transform parent, string name, Vector2 position, Vector2 size, Vector2 travel, int order, float cycleSeconds)
    {
        GameObject pulse = AddEffectsV5SpriteChild(parent, name, "fxv5_lamp_halo_amber.png", position, size, new Color(0.45f, 0.95f, 1f, 0.16f), order);
        LoopingBackgroundMotion2D motion = pulse.AddComponent<LoopingBackgroundMotion2D>();
        SetVector2(motion, "travel", travel);
        SetFloat(motion, "cycleSeconds", cycleSeconds);
        SetFloat(motion, "minAlpha", 0.02f);
        SetFloat(motion, "maxAlpha", 0.24f);
        SetFloat(motion, "scalePulse", 0.035f);
    }

    private static void AddV13SteamJet(Transform parent, string name, Vector2 position, int order)
    {
        GameObject steam = AddEffectsV4SpriteChild(parent, name, "fxv4_thin_steam_wisp.png", position, new Vector2(0.96f, 1.5f), new Color(0.82f, 0.78f, 0.62f, 0.2f), order);
        SteamPuff2D puff = steam.AddComponent<SteamPuff2D>();
        SetFloat(puff, "riseHeight", 0.58f);
        SetFloat(puff, "pulseSpeed", 0.78f);
        SetFloat(puff, "minAlpha", 0.025f);
        SetFloat(puff, "maxAlpha", 0.22f);
    }

    private static void AddSway(GameObject target, float amplitudeDegrees, float cycleSeconds, Vector2 positionSway)
    {
        SwayingDecor2D sway = target.AddComponent<SwayingDecor2D>();
        SetFloat(sway, "amplitudeDegrees", amplitudeDegrees);
        SetFloat(sway, "cycleSeconds", cycleSeconds);
        SetVector2(sway, "positionSway", positionSway);
    }

    private static void CreateAnimatedBackgroundOverlays(Transform parent)
    {
        GameObject layers = NewChild(parent, "BG_AnimatedLayers");

        AddAmbientFog(layers.transform, "Animated_Fog_Low_01", new Vector2(18f, -1.45f), new Vector2(40f, 0.52f), 0.025f, 0.095f, 0.45f, 0.05f, 9f);
        AddAmbientFog(layers.transform, "Animated_Fog_Low_02", new Vector2(55f, -1.1f), new Vector2(48f, 0.42f), 0.02f, 0.085f, -0.38f, 0.04f, 11f);
        AddAmbientFog(layers.transform, "Animated_Fog_Low_03", new Vector2(98f, -1.35f), new Vector2(46f, 0.48f), 0.022f, 0.1f, 0.52f, 0.05f, 10f);
        AddAmbientFog(layers.transform, "Animated_High_Dust_Band", new Vector2(72f, 1.25f), new Vector2(76f, 0.34f), 0.012f, 0.05f, -0.28f, 0.08f, 14f);
        AddBackgroundSteam(layers.transform, "BG_Steam_Awakening", new Vector2(5.2f, -2.15f), -22);
        AddBackgroundSteam(layers.transform, "BG_Steam_OilPit", new Vector2(42.8f, -2.05f), -22);
        AddBackgroundSteam(layers.transform, "BG_Steam_EnergyExit", new Vector2(117.4f, -2.05f), -22);
        AddBackgroundRotator(layers.transform, "BG_Fan_MoveCorridor", new Vector2(23.5f, 1.45f), 1.25f, -55, 7f);
        AddBackgroundRotator(layers.transform, "BG_Fan_ChipTerminal", new Vector2(92.5f, 1.65f), 1.45f, -55, -5f);
        AddBackgroundPulley(layers.transform, "BG_Pulley_Awakening", new Vector2(11.5f, 1.05f), 0.72f, -54, 2.2f);
        AddBackgroundPulley(layers.transform, "BG_Pulley_Exit", new Vector2(126.8f, 1.1f), 0.82f, -54, -1.8f);
        AddTransitionMask(layers.transform, "BG_TransitionMask_01", 27.7f);
        AddTransitionMask(layers.transform, "BG_TransitionMask_02", 64.3f);
        AddTransitionMask(layers.transform, "BG_TransitionMask_03", 101f);
        AddSparkShower(layers.transform, "BG_Sparks_PodCeiling", new Vector2(8.4f, 1.85f), 9, 2.7f, -16);
        AddSparkShower(layers.transform, "BG_Sparks_AttackArena", new Vector2(58.6f, 1.4f), 12, 3.5f, -16);
        AddSparkShower(layers.transform, "BG_Sparks_EnergyGate", new Vector2(122.1f, 1.7f), 14, 4.2f, -16);
        AddScanBeam(layers.transform, "BG_ScanBeam_ChipTerminal", new Vector2(88.5f, -0.35f), new Vector2(0.18f, 2.6f), new Vector2(3.2f, 0f), 5.8f, -17);
        AddScanBeam(layers.transform, "BG_ScanBeam_EnergyGate", new Vector2(121.4f, -0.1f), new Vector2(0.22f, 3.2f), new Vector2(4.5f, 0f), 6.6f, -17);
        AddConveyorLightRun(layers.transform, "BG_ConveyorGlow_Movement", 16f, 30f, -2.18f);
        AddConveyorLightRun(layers.transform, "BG_ConveyorGlow_Exit", 105f, 124f, -2.18f);

        for (int i = 0; i < 6; i++)
        {
            float x = 7f + i * 20f;
            GameObject mote = AddEnvironmentV3SpriteChild(layers.transform, $"Animated_Dust_Mote_{i + 1:00}", "envv3_soft_glow_round_amber.png", new Vector2(x, -0.25f + (i % 3) * 0.52f), new Vector2(0.18f, 0.18f), new Color(1f, 0.62f, 0.16f, 0.035f), -34);
            AmbientDrift2D drift = mote.AddComponent<AmbientDrift2D>();
            SetFloat(drift, "driftX", 0.25f + (i % 3) * 0.06f);
            SetFloat(drift, "driftY", 0.18f);
            SetFloat(drift, "cycleSeconds", 6.8f + i * 0.5f);
            SetFloat(drift, "minAlpha", 0.015f);
            SetFloat(drift, "maxAlpha", 0.065f);
        }
    }

    private static void CreateOrganizedDecorV2(Transform parent)
    {
        GameObject root = NewChild(parent, "BG_OrganizedDecor_V2");

        AddOrderedBackwallPanelsV3(root.transform);

        AddOrganizedPipe(root.transform, "DecorV2_Awakening_TopPipe", new Vector2(7.6f, 2.48f), new Vector2(8.8f, 1.04f), -18);
        AddOrganizedTube(root.transform, "DecorV2_Awakening_TubeLight", new Vector2(7.6f, 2.2f), new Vector2(6.6f, 0.66f));
        AddOrganizedCrate(root.transform, "DecorV2_Awakening_Crate", new Vector2(10.7f, -2.38f), new Vector2(1.75f, 0.78f));
        AddOrganizedLantern(root.transform, "DecorV2_Awakening_Lantern", new Vector2(3.9f, 1.05f), new Vector2(0.58f, 0.94f));

        AddOrganizedPipe(root.transform, "DecorV2_Move_TopPipe", new Vector2(20.2f, 2.55f), new Vector2(10.2f, 1.08f), -18);
        AddOrganizedChain(root.transform, "DecorV2_Move_Chain_A", new Vector2(15.4f, 1.68f), new Vector2(0.52f, 2.35f));
        AddOrganizedValve(root.transform, "DecorV2_Move_Valve", new Vector2(27.8f, 0.48f), new Vector2(4.65f, 1.78f));
        AddOrganizedCable(root.transform, "DecorV2_Move_CableTrunk", new Vector2(22.2f, 0.58f), new Vector2(4.6f, 0.32f));
        AddOrganizedCrate(root.transform, "DecorV2_Move_Crate", new Vector2(30.8f, -2.4f), new Vector2(1.55f, 0.69f));

        AddOrganizedPipe(root.transform, "DecorV2_Gap_TopPipe", new Vector2(39.2f, 2.48f), new Vector2(8.9f, 1.04f), -18);
        AddOrganizedElbow(root.transform, "DecorV2_Gap_Elbow", new Vector2(39.5f, 0.48f), new Vector2(3.95f, 2.05f));
        AddOrganizedTube(root.transform, "DecorV2_Gap_TubeLight", new Vector2(44.8f, 2.16f), new Vector2(5.2f, 0.54f));
        AddOrganizedLantern(root.transform, "DecorV2_Gap_Lantern", new Vector2(48.2f, 1.22f), new Vector2(0.56f, 0.92f));
        AddOrganizedCable(root.transform, "DecorV2_Gap_Cable", new Vector2(36.5f, 0.74f), new Vector2(3.5f, 0.28f));

        AddOrganizedPipe(root.transform, "DecorV2_Attack_TopPipe", new Vector2(58.7f, 2.48f), new Vector2(10.1f, 1.08f), -18);
        AddOrganizedValve(root.transform, "DecorV2_Attack_Valve", new Vector2(58.2f, 0.62f), new Vector2(4.9f, 1.88f));
        AddOrganizedChain(root.transform, "DecorV2_Attack_Chain", new Vector2(64.5f, 1.55f), new Vector2(0.58f, 2.6f));
        AddOrganizedCrate(root.transform, "DecorV2_Attack_Crate_A", new Vector2(51.1f, -2.39f), new Vector2(1.45f, 0.65f));
        AddOrganizedCrate(root.transform, "DecorV2_Attack_Crate_B", new Vector2(66.7f, -2.38f), new Vector2(1.65f, 0.74f));

        AddOrganizedPipe(root.transform, "DecorV2_Door_TopPipe", new Vector2(73.6f, 2.52f), new Vector2(9.2f, 1.04f), -18);
        AddOrganizedLantern(root.transform, "DecorV2_Door_Lantern", new Vector2(66.5f, 1.28f), new Vector2(0.58f, 0.96f));
        AddOrganizedCable(root.transform, "DecorV2_Door_Cable", new Vector2(70.4f, 0.66f), new Vector2(4.4f, 0.32f));
        AddOrganizedChain(root.transform, "DecorV2_Door_Chain", new Vector2(77.6f, 1.46f), new Vector2(0.5f, 2.18f));
        AddOrganizedCrate(root.transform, "DecorV2_Door_Crate", new Vector2(76.9f, -2.38f), new Vector2(1.45f, 0.65f));

        AddOrganizedPipe(root.transform, "DecorV2_Chip_TopPipe", new Vector2(89.2f, 2.52f), new Vector2(10.4f, 1.08f), -18);
        AddOrganizedTube(root.transform, "DecorV2_Chip_TubeLight", new Vector2(89.2f, 2.18f), new Vector2(6.8f, 0.58f));
        AddOrganizedCable(root.transform, "DecorV2_Chip_CableTrunk", new Vector2(88.8f, 0.72f), new Vector2(5.3f, 0.35f));
        AddOrganizedLantern(root.transform, "DecorV2_Chip_Lantern", new Vector2(96.7f, 1.18f), new Vector2(0.56f, 0.92f));
        AddOrganizedCrate(root.transform, "DecorV2_Chip_Crate", new Vector2(91.8f, -2.39f), new Vector2(1.6f, 0.72f));

        AddOrganizedPipe(root.transform, "DecorV2_Exit_TopPipe", new Vector2(110.2f, 2.52f), new Vector2(10f, 1.08f), -18);
        AddOrganizedValve(root.transform, "DecorV2_Exit_Valve", new Vector2(117.2f, 0.62f), new Vector2(5f, 1.92f));
        AddOrganizedChain(root.transform, "DecorV2_Exit_Chain", new Vector2(124.3f, 1.42f), new Vector2(0.62f, 2.75f));
        AddOrganizedLantern(root.transform, "DecorV2_Exit_Lantern", new Vector2(112.6f, 1.25f), new Vector2(0.58f, 0.96f));
        AddOrganizedCrate(root.transform, "DecorV2_Exit_Crate", new Vector2(126.1f, -2.38f), new Vector2(1.85f, 0.82f));

        AddElectricArcCluster(root.transform, "DecorV2_ElectricArc_Awakening", new Vector2(8.4f, 1.78f), -12f);
        AddElectricArcCluster(root.transform, "DecorV2_ElectricArc_Gap", new Vector2(43.7f, 0.58f), -6f);
        AddElectricArcCluster(root.transform, "DecorV2_ElectricArc_Door", new Vector2(68.8f, 0.58f), -18f);
        AddElectricArcCluster(root.transform, "DecorV2_ElectricArc_Chip", new Vector2(88.6f, 1.1f), 9f);
        AddElectricArcCluster(root.transform, "DecorV2_ElectricArc_Exit", new Vector2(120.3f, 0.78f), 12f);

        AddOrganizedDustVeil(root.transform, "BG_DustVeil_OrganizedV2");
    }

    private static void AddOrderedBackwallPanelsV3(Transform parent)
    {
        float[] centers = { 7.8f, 24.2f, 43.8f, 69.6f, 90.6f, 117.8f };
        for (int i = 0; i < centers.Length; i++)
        {
            GameObject panel = AddEnvironmentV3SpriteChild(parent, $"BGV3_BackwallPanel_{i + 1:00}", "envv3_backwall_panel_clean.png", new Vector2(centers[i], 0.22f + (i % 2) * 0.12f), new Vector2(5.6f + (i % 3) * 0.65f, 2.1f), new Color(0.78f, 0.7f, 0.56f, 0.22f), -31);
            ParallaxLayer2D parallax = panel.AddComponent<ParallaxLayer2D>();
            SetVector2(parallax, "parallaxFactor", new Vector2(0.05f, 0.01f));
        }
    }

    private static void AddOrganizedPipe(Transform parent, string name, Vector2 position, Vector2 size, int order)
    {
        AddEnvironmentV2SpriteChild(parent, name, "envv2_pipe_straight_hd.png", position, size, new Color(0.92f, 0.86f, 0.74f, 0.72f), order);
    }

    private static void AddOrganizedTube(Transform parent, string name, Vector2 position, Vector2 size)
    {
        GameObject glow = AddEnvironmentV3SpriteChild(parent, name + "_SoftGlow", "envv3_tube_glow_amber.png", position, new Vector2(size.x * 0.92f, size.y * 0.9f), new Color(1f, 0.58f, 0.16f, 0.34f), -16);
        SpriteFlicker2D glowFlicker = glow.AddComponent<SpriteFlicker2D>();
        SetFloat(glowFlicker, "minAlpha", 0.2f);
        SetFloat(glowFlicker, "maxAlpha", 0.46f);
        SetFloat(glowFlicker, "flickerSpeed", 2.8f);
        GameObject tube = AddEnvironmentV2SpriteChild(parent, name, "envv2_tube_light_hd.png", position, size, new Color(1f, 0.92f, 0.74f, 0.8f), -8);
        tube.AddComponent<SpriteFlicker2D>();
    }

    private static void AddOrganizedValve(Transform parent, string name, Vector2 position, Vector2 size)
    {
        AddEnvironmentV2SpriteChild(parent, name, "envv2_valve_pipe_hd.png", position, size, new Color(0.9f, 0.82f, 0.68f, 0.72f), -10);
    }

    private static void AddOrganizedElbow(Transform parent, string name, Vector2 position, Vector2 size)
    {
        AddEnvironmentV2SpriteChild(parent, name, "envv2_pipe_elbow_hd.png", position, size, new Color(0.9f, 0.82f, 0.68f, 0.72f), -10);
    }

    private static void AddOrganizedChain(Transform parent, string name, Vector2 position, Vector2 size)
    {
        AddEnvironmentV2SpriteChild(parent, name, "envv2_hanging_chain_hd.png", position, size, new Color(0.82f, 0.76f, 0.64f, 0.68f), -7);
    }

    private static void AddOrganizedLantern(Transform parent, string name, Vector2 position, Vector2 size)
    {
        GameObject glow = AddEnvironmentV3SpriteChild(parent, name + "_SoftGlow", "envv3_soft_glow_round_amber.png", position + new Vector2(0f, -0.08f), new Vector2(size.x * 2.15f, size.y * 1.3f), new Color(1f, 0.52f, 0.12f, 0.28f), -16);
        SpriteFlicker2D glowFlicker = glow.AddComponent<SpriteFlicker2D>();
        SetFloat(glowFlicker, "minAlpha", 0.16f);
        SetFloat(glowFlicker, "maxAlpha", 0.38f);
        SetFloat(glowFlicker, "flickerSpeed", 3.2f);
        GameObject lantern = AddEnvironmentV2SpriteChild(parent, name, "envv2_hanging_lantern_hd.png", position, size, new Color(1f, 0.9f, 0.72f, 0.76f), -7);
        lantern.AddComponent<SpriteFlicker2D>();
    }

    private static void AddOrganizedCrate(Transform parent, string name, Vector2 position, Vector2 size)
    {
        AddEnvironmentV2SpriteChild(parent, name, "envv2_storage_crate_hd.png", position, size, new Color(0.84f, 0.78f, 0.66f, 0.82f), -4);
    }

    private static void AddOrganizedCable(Transform parent, string name, Vector2 position, Vector2 size)
    {
        AddEnvironmentV2SpriteChild(parent, name, "envv2_cable_bundle_hd.png", position, size, new Color(0.78f, 0.72f, 0.62f, 0.62f), -12);
    }

    private static void AddOrganizedDustVeil(Transform parent, string name)
    {
        for (int i = 0; i < 9; i++)
        {
            float x = 8f + i * 13.8f;
            float y = -1.62f + (i % 3) * 0.46f;
            GameObject dust = AddEnvironmentV3SpriteChild(parent, name + $"_{i:00}", "envv3_dust_wisp_wide.png", new Vector2(x, y), new Vector2(5.4f + (i % 3) * 0.75f, 0.68f), new Color(0.74f, 0.68f, 0.54f, 0.055f), -32);
            AmbientDrift2D drift = dust.AddComponent<AmbientDrift2D>();
            SetFloat(drift, "driftX", 0.18f + (i % 3) * 0.06f);
            SetFloat(drift, "driftY", 0.04f);
            SetFloat(drift, "cycleSeconds", 10f + i * 0.5f);
            SetFloat(drift, "minAlpha", 0.01f);
            SetFloat(drift, "maxAlpha", 0.055f);
        }
    }

    private static void AddElectricArcCluster(Transform parent, string name, Vector2 position, float baseRotation)
    {
        for (int i = 0; i < 3; i++)
        {
            Vector2 offset = new Vector2(-0.16f + i * 0.16f, Mathf.Sin(i * 1.9f) * 0.055f);
            string arcFile = $"envv3_electric_arc_{i + 1:00}.png";
            GameObject arc = AddEnvironmentV3SpriteChild(parent, name + $"_{i:00}", arcFile, position + offset, new Vector2(0.52f - i * 0.055f, 0.16f), new Color(0.78f, 0.94f, 1f, 0f), -2);
            arc.transform.rotation = Quaternion.Euler(0f, 0f, baseRotation + i * 18f);
            ElectricArcFlicker2D flicker = arc.AddComponent<ElectricArcFlicker2D>();
            SetFloat(flicker, "minDelay", 0.28f + i * 0.08f);
            SetFloat(flicker, "maxDelay", 1.15f + i * 0.18f);
            SetFloat(flicker, "flashSeconds", 0.075f + i * 0.01f);
            SetFloat(flicker, "maxAlpha", 0.72f);
            SetFloat(flicker, "scalePulse", 0.48f);
            SetVector2(flicker, "jitter", new Vector2(0.025f, 0.018f));
        }
    }

    private static void AddDustVeilCluster(Transform parent, string name)
    {
        for (int i = 0; i < 18; i++)
        {
            float x = 4.5f + i * 6.9f;
            float y = -1.55f + (i % 5) * 0.58f;
            Vector2 size = new Vector2(3.2f + (i % 4) * 0.7f, 0.58f + (i % 3) * 0.16f);
            GameObject dust = AddEnvironmentSpriteChild(parent, name + $"_{i:00}", "env_dust_puff.png", new Vector2(x, y), size, new Color(0.74f, 0.68f, 0.54f, 0.07f), -4);
            AmbientDrift2D drift = dust.AddComponent<AmbientDrift2D>();
            SetFloat(drift, "driftX", 0.25f + (i % 4) * 0.08f);
            SetFloat(drift, "driftY", 0.05f + (i % 3) * 0.025f);
            SetFloat(drift, "cycleSeconds", 8.5f + i * 0.37f);
            SetFloat(drift, "minAlpha", 0.015f);
            SetFloat(drift, "maxAlpha", 0.085f);
        }
    }

    private static void AddAmbientFog(Transform parent, string name, Vector2 position, Vector2 size, float minAlpha, float maxAlpha, float driftX, float driftY, float cycleSeconds)
    {
        GameObject fog = AddEnvironmentV3SpriteChild(parent, name, "envv3_dust_wisp_wide.png", position, size, new Color(0.82f, 0.76f, 0.64f, maxAlpha), -32);
        AmbientDrift2D drift = fog.AddComponent<AmbientDrift2D>();
        SetFloat(drift, "driftX", driftX);
        SetFloat(drift, "driftY", driftY);
        SetFloat(drift, "cycleSeconds", cycleSeconds);
        SetFloat(drift, "minAlpha", minAlpha);
        SetFloat(drift, "maxAlpha", maxAlpha);
    }

    private static void AddBackgroundSteam(Transform parent, string name, Vector2 position, int order)
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject puff = AddEnvironmentSpriteChild(parent, name + $"_Puff_{i:00}", "env_dust_puff.png", position + new Vector2(-0.36f + i * 0.24f, 0.14f + i * 0.09f), new Vector2(0.8f, 0.58f), new Color(0.76f, 0.72f, 0.62f, 0.13f), order);
            SteamPuff2D steam = puff.AddComponent<SteamPuff2D>();
            SetFloat(steam, "riseHeight", 0.35f + i * 0.08f);
            SetFloat(steam, "pulseSpeed", 0.82f + i * 0.17f);
            SetFloat(steam, "minAlpha", 0.02f);
            SetFloat(steam, "maxAlpha", 0.16f);
        }
    }

    private static void AddBackgroundRotator(Transform parent, string name, Vector2 center, float size, int order, float speed)
    {
        GameObject bladeA = AddRect(parent, name + "_Blade_A", center, new Vector2(size, size * 0.12f), new Color(0.1f, 0.085f, 0.07f, 0.5f), order, false);
        SimpleRotator2D rotatorA = bladeA.AddComponent<SimpleRotator2D>();
        SetFloat(rotatorA, "degreesPerSecond", speed);

        GameObject bladeB = AddRect(parent, name + "_Blade_B", center, new Vector2(size * 0.12f, size), new Color(0.1f, 0.085f, 0.07f, 0.5f), order, false);
        SimpleRotator2D rotatorB = bladeB.AddComponent<SimpleRotator2D>();
        SetFloat(rotatorB, "degreesPerSecond", speed);

        AddRect(parent, name + "_Hub", center, new Vector2(size * 0.18f, size * 0.18f), new Color(0.28f, 0.19f, 0.1f, 0.62f), order + 1, false);
    }

    private static void AddBackgroundPulley(Transform parent, string name, Vector2 center, float size, int order, float speed)
    {
        GameObject pulley = AddEnvironmentV3SpriteChild(parent, name, "envv3_pulley_wheel.png", center, new Vector2(size, size), new Color(0.82f, 0.72f, 0.58f, 0.38f), order);
        SimpleRotator2D rotator = pulley.AddComponent<SimpleRotator2D>();
        SetFloat(rotator, "degreesPerSecond", speed);
    }

    private static void AddSparkShower(Transform parent, string name, Vector2 origin, int count, float width, int order)
    {
        for (int i = 0; i < count; i++)
        {
            float normalized = count <= 1 ? 0.5f : i / (float)(count - 1);
            float x = origin.x - width * 0.5f + normalized * width + Mathf.Sin(i * 2.17f) * 0.16f;
            float y = origin.y + Mathf.Cos(i * 1.73f) * 0.22f;
            Vector2 size = new Vector2(0.08f + (i % 3) * 0.018f, 0.22f + (i % 4) * 0.055f);
            GameObject spark = AddEnvironmentSpriteChild(parent, name + $"_{i:00}", "env_spark.png", new Vector2(x, y), size, new Color(1f, 0.72f, 0.18f, 0.34f), order);
            spark.transform.rotation = Quaternion.Euler(0f, 0f, -18f + (i % 5) * 7f);

            LoopingBackgroundMotion2D motion = spark.AddComponent<LoopingBackgroundMotion2D>();
            SetVector2(motion, "travel", new Vector2(0.2f + (i % 4) * 0.09f, -1.1f - (i % 5) * 0.18f));
            SetFloat(motion, "cycleSeconds", 1.35f + (i % 6) * 0.24f);
            SetFloat(motion, "minAlpha", 0f);
            SetFloat(motion, "maxAlpha", 0.24f + (i % 3) * 0.08f);
            SetFloat(motion, "scalePulse", 0.25f);
            SetFloat(motion, "phaseOffset", normalized);
        }
    }

    private static void AddScanBeam(Transform parent, string name, Vector2 position, Vector2 size, Vector2 travel, float cycleSeconds, int order)
    {
        GameObject beam = AddEnvironmentV3SpriteChild(parent, name, "envv3_scan_beam_amber.png", position, size, new Color(1f, 0.62f, 0.16f, 0.18f), order);
        LoopingBackgroundMotion2D motion = beam.AddComponent<LoopingBackgroundMotion2D>();
        SetVector2(motion, "travel", travel);
        SetFloat(motion, "cycleSeconds", cycleSeconds);
        SetFloat(motion, "minAlpha", 0.015f);
        SetFloat(motion, "maxAlpha", 0.18f);
        SetFloat(motion, "scalePulse", 0.02f);
    }

    private static void AddConveyorLightRun(Transform parent, string name, float startX, float endX, float y)
    {
        int lightCount = Mathf.Max(4, Mathf.RoundToInt((endX - startX) / 2.4f));
        for (int i = 0; i < lightCount; i++)
        {
            float t = i / (float)lightCount;
            float x = Mathf.Lerp(startX, endX, t);
            GameObject light = AddEnvironmentV3SpriteChild(parent, name + $"_{i:00}", "envv3_indicator_pill_amber.png", new Vector2(x, y), new Vector2(0.46f, 0.09f), new Color(1f, 0.5f, 0.08f, 0.28f), -15);
            LoopingBackgroundMotion2D motion = light.AddComponent<LoopingBackgroundMotion2D>();
            SetVector2(motion, "travel", new Vector2(1.7f, 0f));
            SetFloat(motion, "cycleSeconds", 2.1f + (i % 3) * 0.2f);
            SetFloat(motion, "minAlpha", 0.02f);
            SetFloat(motion, "maxAlpha", 0.32f);
            SetFloat(motion, "scalePulse", 0.08f);
            SetFloat(motion, "phaseOffset", t);
        }
    }

    private static void AddBackwallServicePanels(Transform parent, string name, float startX, float endX, float y, int count)
    {
        float width = Mathf.Abs(endX - startX);
        for (int i = 0; i < count; i++)
        {
            float t = count <= 1 ? 0.5f : i / (float)(count - 1);
            float x = Mathf.Lerp(startX, endX, t);
            float panelWidth = width / Mathf.Max(1, count) * 0.72f;
            GameObject panel = AddRect(parent, name + $"_{i:00}_Plate", new Vector2(x, y + (i % 2) * 0.11f), new Vector2(panelWidth, 0.72f), new Color(0.08f, 0.07f, 0.058f, 0.42f), -28, false);
            AddRect(parent, name + $"_{i:00}_TopRib", new Vector2(x, panel.transform.position.y + 0.28f), new Vector2(panelWidth * 0.92f, 0.045f), new Color(0.2f, 0.14f, 0.08f, 0.34f), -27, false);
            AddRect(parent, name + $"_{i:00}_LowerRib", new Vector2(x, panel.transform.position.y - 0.25f), new Vector2(panelWidth * 0.82f, 0.04f), new Color(0.18f, 0.13f, 0.08f, 0.28f), -27, false);

            for (int r = 0; r < 4; r++)
            {
                float rivetX = x + (r < 2 ? -panelWidth * 0.42f : panelWidth * 0.42f);
                float rivetY = panel.transform.position.y + (r % 2 == 0 ? -0.25f : 0.25f);
                AddEnvironmentSpriteChild(parent, name + $"_{i:00}_Rivet_{r:00}", "env_rivet.png", new Vector2(rivetX, rivetY), new Vector2(0.07f, 0.07f), new Color(1f, 0.82f, 0.58f, 0.32f), -26);
            }
        }
    }

    private static void AddBackgroundCableBundle(Transform parent, string name, float startX, float endX, float y, int order)
    {
        float width = Mathf.Abs(endX - startX);
        float center = (startX + endX) * 0.5f;
        AddEnvironmentSpriteChild(parent, name + "_MainBundle", "env_cable_bundle.png", new Vector2(center, y), new Vector2(width, 0.24f), new Color(1f, 1f, 1f, 0.34f), order);

        int drops = Mathf.Max(3, Mathf.RoundToInt(width / 5f));
        for (int i = 0; i < drops; i++)
        {
            float t = (i + 0.5f) / drops;
            float x = Mathf.Lerp(startX, endX, t);
            float length = 0.52f + (i % 3) * 0.22f;
            GameObject drop = AddRect(parent, name + $"_HangingLoop_{i:00}", new Vector2(x, y - length * 0.5f), new Vector2(0.045f, length), new Color(0.035f, 0.03f, 0.026f, 0.5f), order + 1, false);
            drop.transform.rotation = Quaternion.Euler(0f, 0f, -7f + (i % 4) * 4f);
            LoopingBackgroundMotion2D motion = drop.AddComponent<LoopingBackgroundMotion2D>();
            SetVector2(motion, "travel", new Vector2(0.05f, 0.02f));
            SetFloat(motion, "cycleSeconds", 4.8f + i * 0.31f);
            SetFloat(motion, "minAlpha", 0.18f);
            SetFloat(motion, "maxAlpha", 0.48f);
            SetFloat(motion, "scalePulse", 0.025f);
            SetFloat(motion, "phaseOffset", t);
        }
    }

    private static void AddOilDripLine(Transform parent, string name, float startX, float endX, float y)
    {
        int drips = Mathf.Max(4, Mathf.RoundToInt((endX - startX) / 2.4f));
        for (int i = 0; i < drips; i++)
        {
            float t = drips <= 1 ? 0.5f : i / (float)(drips - 1);
            float x = Mathf.Lerp(startX, endX, t);
            GameObject stain = AddEnvironmentSpriteChild(parent, name + $"_Stain_{i:00}", "env_oil_stain.png", new Vector2(x, y - 1.25f), new Vector2(0.9f + (i % 3) * 0.18f, 0.28f), new Color(1f, 1f, 1f, 0.18f), -21);
            stain.transform.rotation = Quaternion.Euler(0f, 0f, -3f + (i % 4) * 2f);

            GameObject drop = AddRect(parent, name + $"_Drop_{i:00}", new Vector2(x + 0.08f * Mathf.Sin(i), y + 0.12f), new Vector2(0.055f, 0.14f), new Color(0.05f, 0.035f, 0.02f, 0.48f), -20, false);
            LoopingBackgroundMotion2D motion = drop.AddComponent<LoopingBackgroundMotion2D>();
            SetVector2(motion, "travel", new Vector2(0f, -1.05f));
            SetFloat(motion, "cycleSeconds", 1.8f + (i % 4) * 0.33f);
            SetFloat(motion, "minAlpha", 0f);
            SetFloat(motion, "maxAlpha", 0.38f);
            SetFloat(motion, "scalePulse", 0.12f);
            SetFloat(motion, "phaseOffset", t);
        }
    }

    private static void AddWarningBeaconRow(Transform parent, string name, float[] xs)
    {
        for (int i = 0; i < xs.Length; i++)
        {
            Vector2 position = new Vector2(xs[i], 1.05f + (i % 2) * 0.28f);
            AddRect(parent, name + $"_{i:00}_Stem", position + new Vector2(0f, -0.18f), new Vector2(0.055f, 0.38f), new Color(0.08f, 0.064f, 0.046f, 0.44f), -19, false);
            GameObject light = AddRect(parent, name + $"_{i:00}_Amber", position + new Vector2(0f, 0.05f), new Vector2(0.16f, 0.16f), new Color(1f, 0.54f, 0.08f, 0.42f), -18, false);
            light.AddComponent<SpriteFlicker2D>();
        }
    }

    private static void AddGaugeClusterRow(Transform parent, string name, float startX, float endX, float y, int count)
    {
        for (int i = 0; i < count; i++)
        {
            float t = count <= 1 ? 0.5f : i / (float)(count - 1);
            float x = Mathf.Lerp(startX, endX, t);
            GameObject gauge = AddEnvironmentSpriteChild(parent, name + $"_{i:00}_Gauge", "env_valve_wheel.png", new Vector2(x, y), new Vector2(0.32f, 0.32f), new Color(1f, 1f, 1f, 0.42f), -18);
            gauge.transform.rotation = Quaternion.Euler(0f, 0f, i * 17f);
            GameObject needle = AddRect(parent, name + $"_{i:00}_Needle", new Vector2(x, y), new Vector2(0.025f, 0.18f), new Color(1f, 0.62f, 0.16f, 0.38f), -17, false);
            needle.transform.rotation = Quaternion.Euler(0f, 0f, -45f + i * 22f);
        }
    }

    private static void AddDistantArmature(Transform parent, string name, Vector2 basePosition, float height)
    {
        AddRect(parent, name + "_MainStrut", basePosition + new Vector2(0f, height * 0.18f), new Vector2(0.18f, height), new Color(0.04f, 0.035f, 0.03f, 0.34f), -34, false);
        for (int i = 0; i < 5; i++)
        {
            float y = basePosition.y - height * 0.28f + i * height * 0.14f;
            GameObject cross = AddRect(parent, name + $"_CrossBrace_{i:00}", new Vector2(basePosition.x, y), new Vector2(2.1f + (i % 2) * 0.5f, 0.07f), new Color(0.07f, 0.06f, 0.05f, 0.28f), -33, false);
            cross.transform.rotation = Quaternion.Euler(0f, 0f, i % 2 == 0 ? 18f : -18f);
        }

        GameObject pulley = AddEnvironmentSpriteChild(parent, name + "_Pulley", "env_valve_wheel.png", basePosition + new Vector2(0.65f, height * 0.55f), new Vector2(0.62f, 0.62f), new Color(1f, 1f, 1f, 0.24f), -32);
        SimpleRotator2D rotator = pulley.AddComponent<SimpleRotator2D>();
        SetFloat(rotator, "degreesPerSecond", -3.2f);
    }

    private static void AddTransitionMask(Transform parent, string name, float x)
    {
        AddRect(parent, name + "_Vertical_Truss", new Vector2(x, 1.15f), new Vector2(0.58f, 8.8f), new Color(0.025f, 0.022f, 0.018f, 0.58f), -24, false);
        AddRect(parent, name + "_Warm_Edge_Left", new Vector2(x - 0.34f, 0.2f), new Vector2(0.06f, 7.2f), new Color(0.78f, 0.45f, 0.16f, 0.18f), -23, false);
        AddRect(parent, name + "_Warm_Edge_Right", new Vector2(x + 0.34f, 0.2f), new Vector2(0.06f, 7.2f), new Color(0.78f, 0.45f, 0.16f, 0.16f), -23, false);
        for (int i = 0; i < 4; i++)
        {
            AddRect(parent, name + $"_Crossbar_{i:00}", new Vector2(x, -2.2f + i * 1.55f), new Vector2(1.65f, 0.1f), new Color(0.11f, 0.09f, 0.07f, 0.52f), -22, false);
        }
    }

    private static void CreateGeometry(Transform parent)
    {
        GameObject section1 = NewChild(parent, "Section_01_AwakeningBench");
        AddPlatform(section1.transform, "Floor_AwakeningBench", new Vector2(9f, -3.1f), new Vector2(18f, 1.2f), PlatformVisualType.MainFloor, true, false);

        GameObject section2 = NewChild(parent, "Section_02_MovementTutorial");
        AddPlatform(section2.transform, "Floor_MovementTutorial_A", new Vector2(30.6f, -3.15f), new Vector2(25.2f, 1.1f), PlatformVisualType.MainFloor, false, false);
        AddPlatform(section2.transform, "Movement_LowObstacle", new Vector2(22.8f, -2.19f), new Vector2(1.35f, 0.82f), PlatformVisualType.LowObstacle);
        AddEnvironmentV7SpriteChild(section2.transform, "Wall_Arrow_Right_GeneratedSign", "envv7_sign_arrow.png", new Vector2(25.85f, -1.1f), new Vector2(1.55f, 0.58f), new Color(1f, 0.9f, 0.66f, 0.78f), 16);
        AddArrowFXPolish(section2.transform, "Wall_Arrow_Right_GeneratedSign", new Vector2(25.85f, -1.1f), new Vector2(1.55f, 0.58f), new Color(1f, 0.62f, 0.16f, 0.2f));

        GameObject section3 = NewChild(parent, "Section_03_BrokenPlatformJump");
        AddPlatform(section3.transform, "Jump_FirstGapPlatform", new Vector2(46.7f, -2.05f), new Vector2(2.4f, 0.5f), PlatformVisualType.ThinJumpDeck);
        AddPlatform(section3.transform, "Jump_MidPlatform", new Vector2(51.0f, -1.22f), new Vector2(3.0f, 0.58f), PlatformVisualType.ThinFloatingDeck);
        AddPlatform(section3.transform, "Jump_OptionalRewardPlatform", new Vector2(52.2f, 0.95f), new Vector2(2.8f, 0.42f), PlatformVisualType.ThinFloatingDeck);
        AddPlatform(section3.transform, "Jump_RightPlatform", new Vector2(56.6f, -2.18f), new Vector2(3.2f, 0.58f), PlatformVisualType.ThinJumpDeck);
        AddHazard(section3.transform, "Oil_Pit_Return_Training", new Vector2(53.2f, -3.86f), new Vector2(18.4f, 0.42f));

        GameObject section4 = NewChild(parent, "Section_04_FirstEnemy");
        AddPlatform(section4.transform, "Floor_FirstEnemyArena", new Vector2(74.1f, -3.05f), new Vector2(23.8f, 1.1f), PlatformVisualType.MainFloor, true, false);

        GameObject section5 = NewChild(parent, "Section_05_TrapMachineHall");
        AddPlatform(section5.transform, "Floor_TrapEntry", new Vector2(89.5f, -3.05f), new Vector2(7.0f, 1.1f), PlatformVisualType.MainFloor, false, true);
        AddInsulatedStep(section5.transform, "Trap_InsulatedStep_A", new Vector2(98.85f, -2.08f), new Vector2(1.1f, 0.32f));
        AddInsulatedStep(section5.transform, "Trap_InsulatedStep_B", new Vector2(105.0f, -2.08f), new Vector2(1.1f, 0.32f));
        AddPlatform(section5.transform, "Floor_TrapExit", new Vector2(109.8f, -3.05f), new Vector2(10.4f, 1.1f), PlatformVisualType.MainFloor, true, false);

        GameObject section6 = NewChild(parent, "Section_06_ChargingSavePoint");
        AddPlatform(section6.transform, "Floor_ChargingSavePoint", new Vector2(120f, -3.05f), new Vector2(12f, 1.1f), PlatformVisualType.MainFloor, false, false);

        GameObject section7 = NewChild(parent, "Section_07_BossRepairHall");
        AddPlatform(section7.transform, "Floor_BossRepairHall", new Vector2(143f, -3.05f), new Vector2(34f, 1.1f), PlatformVisualType.MainFloor, false, false);

        GameObject section8 = NewChild(parent, "Section_08_MechCityGate");
        AddPlatform(section8.transform, "Floor_MechCityGate", new Vector2(168f, -3.05f), new Vector2(16f, 1.1f), PlatformVisualType.MainFloor, false, true);
        AddEnvironmentV7SpriteChild(section8.transform, "MechCity_Entrance_SignPlate", "envv7_sign_arrow.png", new Vector2(164.8f, -0.55f), new Vector2(1.75f, 0.7f), new Color(1f, 0.84f, 0.62f, 0.78f), 16);
        AddArrowFXPolish(section8.transform, "MechCity_Entrance_SignPlate", new Vector2(164.8f, -0.55f), new Vector2(1.75f, 0.7f), new Color(0.52f, 1f, 0.82f, 0.18f));
    }

    private static GameObject CreateEnvironmentalDetails(Transform parent)
    {
        GameObject awakening = NewChild(parent, "Details_01_AwakeningBench");
        GameObject awakeningBenchAssembly = CreateAwakeningBenchRefinedAssembly(awakening.transform);
        AddEnvironmentV11SpriteChild(awakening.transform, "Awakening_V11_RightServicePanel", "envv11_service_panel.png", new Vector2(11.8f, 0.22f), new Vector2(0.82f, 1.12f), new Color(0.82f, 0.92f, 0.68f, 0.24f), -38);
        AddEffectsV5SpriteChild(awakening.transform, "Awakening_V11_FallingDust_RightWall", "fxv5_falling_dust_curtain.png", new Vector2(9.5f, -0.35f), new Vector2(1.25f, 2.1f), new Color(0.86f, 0.7f, 0.44f, 0.1f), -24).AddComponent<AmbientDrift2D>();
        AddEnvironmentV7SpriteChild(awakening.transform, "Awakening_V7_RedWarningLamp_Status", "envv7_warning_lamp.png", new Vector2(9.2f, 0.78f), new Vector2(0.55f, 0.55f), Color.white, 12).AddComponent<SpriteFlicker2D>();
        AddEnvironmentV7SpriteChild(awakening.transform, "Awakening_V7_ScrapPile", "envv7_scrap_pile.png", new Vector2(13.2f, -2.44f), new Vector2(2.3f, 0.82f), Color.white, 7);

        GameObject movement = NewChild(parent, "Details_02_MovementTutorial");
        AddEnvironmentV8SpriteChild(movement.transform, "Movement_V8_PipeGroup_Back", "envv8_top_pipe_soft.png", new Vector2(29f, 2.22f), new Vector2(12.0f, 0.64f), new Color(0.72f, 0.66f, 0.52f, 0.22f), -42);
        AddEnvironmentV7SpriteChild(movement.transform, "Movement_V7_CrateStack", "envv7_crate.png", new Vector2(28.2f, -2.35f), new Vector2(1.55f, 1.2f), Color.white, 8);
        AddEnvironmentV7SpriteChild(movement.transform, "Movement_V7_CrateStack_B", "envv7_crate.png", new Vector2(29.2f, -2.45f), new Vector2(1.15f, 0.9f), new Color(0.86f, 0.78f, 0.66f, 0.9f), 7);

        GameObject jump = NewChild(parent, "Details_03_BrokenPlatformJump");
        AddEnvironmentV8SpriteChild(jump.transform, "Jump_V8_BackCatwalk_Dim", "envv8_road_under_truss_subtle.png", new Vector2(53f, 0.92f), new Vector2(15.5f, 1.0f), new Color(0.58f, 0.62f, 0.52f, 0.16f), -46);
        AddEnvironmentV8SpriteChild(jump.transform, "Jump_V8_OilPoolVisible", "envv8_oil_pool_readable.png", new Vector2(53f, -3.42f), new Vector2(11.8f, 0.82f), new Color(0.68f, 0.82f, 0.64f, 0.74f), 6);
        AddEnvironmentV7SpriteChild(jump.transform, "Jump_V7_HangingChain_A", "envv7_hanging_chain.png", new Vector2(44f, 0.9f), new Vector2(0.46f, 1.45f), new Color(0.76f, 0.72f, 0.62f, 0.3f), -18);
        AddEnvironmentV7SpriteChild(jump.transform, "Jump_V7_HangingChain_B", "envv7_hanging_chain.png", new Vector2(63.5f, 0.95f), new Vector2(0.42f, 1.36f), new Color(0.76f, 0.72f, 0.62f, 0.28f), -18);
        CreateJumpRouteBackgroundPolish(jump.transform);

        GameObject enemyZone = NewChild(parent, "Details_04_FirstEnemy");
        AddEnvironmentV7SpriteChild(enemyZone.transform, "EnemyZone_V7_RobotCorpse", "envv7_robot_debris.png", new Vector2(83f, -2.32f), new Vector2(1.45f, 0.76f), new Color(0.88f, 0.82f, 0.68f, 0.82f), 6);
        AddEnvironmentV7SpriteChild(enemyZone.transform, "EnemyZone_V7_HangingChain", "envv7_hanging_chain.png", new Vector2(69.2f, 0.95f), new Vector2(0.46f, 1.52f), new Color(0.76f, 0.72f, 0.62f, 0.28f), -18);
        AddEnvironmentV7SpriteChild(enemyZone.transform, "EnemyZone_V7_RedWarningLamp", "envv7_warning_lamp.png", new Vector2(68f, -0.5f), new Vector2(0.44f, 0.44f), Color.white, 11).AddComponent<SpriteFlicker2D>();

        GameObject trap = NewChild(parent, "Details_05_TrapMachineHall");
        AddEnvironmentV8SpriteChild(trap.transform, "Trap_V8_ForePipe_Back", "envv8_top_pipe_soft.png", new Vector2(99f, 2.2f), new Vector2(16.0f, 0.68f), new Color(0.72f, 0.66f, 0.52f, 0.21f), -42);
        AddEnvironmentV7SpriteChild(trap.transform, "Trap_V7_CompressorFrame_A", "envv7_trap_machine_frame.png", new Vector2(106.2f, 0.02f), new Vector2(4.4f, 3.0f), new Color(0.82f, 0.84f, 0.7f, 0.28f), -36);
        AddEnvironmentV7SpriteChild(trap.transform, "Trap_V7_CompressorFrame_B", "envv7_trap_machine_frame.png", new Vector2(111.0f, 0.02f), new Vector2(4.4f, 3.0f), new Color(0.82f, 0.84f, 0.7f, 0.24f), -36);
        AddEffectsV2SpriteChild(trap.transform, "Trap_V7_BlueArc_Detail_A", "fxv2_electric_floor_02.png", new Vector2(95.7f, -1.9f), new Vector2(5.8f, 0.58f), new Color(0.46f, 0.9f, 1f, 0.58f), 16);
        AddEffectsV2SpriteChild(trap.transform, "Trap_V7_BlueArc_Detail_B", "fxv2_electric_floor_02.png", new Vector2(102.05f, -1.9f), new Vector2(5.8f, 0.58f), new Color(0.46f, 0.9f, 1f, 0.54f), 16);
        AddEffectsV5SpriteChild(trap.transform, "Trap_V11_FallDust_BelowCurrent", "fxv5_falling_dust_curtain.png", new Vector2(99.7f, -4.1f), new Vector2(14.4f, 1.45f), new Color(0.54f, 0.72f, 0.58f, 0.14f), 7).AddComponent<AmbientDrift2D>();

        GameObject charge = NewChild(parent, "Details_06_ChargingSavePoint");
        AddEnvironmentV8SpriteChild(charge.transform, "ChargeZone_V8_BackPanel_Dim", "envv8_service_panel_dim.png", new Vector2(123.8f, 0.35f), new Vector2(1.22f, 1.65f), new Color(0.52f, 0.86f, 0.62f, 0.08f), -66);
        AddEnvironmentV7SpriteChild(charge.transform, "ChargeZone_V7_CableBundle", "envv7_cable_bundle.png", new Vector2(116.6f, -0.78f), new Vector2(2.45f, 0.22f), new Color(0.7f, 0.68f, 0.56f, 0.32f), -12);

        GameObject boss = NewChild(parent, "Details_07_BossRepairHall");
        AddEnvironmentV7SpriteChild(boss.transform, "BossDoor_RepairHall_Visual", "envv7_boss_door.png", new Vector2(127f, -0.82f), new Vector2(2.65f, 4.1f), new Color(0.52f, 0.45f, 0.36f, 0.55f), 6);
        AddEnvironmentV20SpriteChild(boss.transform, "BossDoor_RepairHall_V20_BackdropOverlay", "boss_entry_lock_refined_overlay.png", new Vector2(127f, -0.82f), new Vector2(2.18f, 4.78f), new Color(0.78f, 0.68f, 0.56f, 0.66f), 7);
        AddEnvironmentV8SpriteChild(boss.transform, "BossHall_V8_OverheadPipe_Back", "envv8_top_pipe_soft.png", new Vector2(143f, 2.34f), new Vector2(22.0f, 0.8f), new Color(0.74f, 0.66f, 0.52f, 0.24f), -42);
        AddEnvironmentV7SpriteChild(boss.transform, "BossHall_V7_HangingChain_Left", "envv7_hanging_chain.png", new Vector2(130.5f, 0.92f), new Vector2(0.48f, 1.74f), new Color(0.76f, 0.72f, 0.62f, 0.3f), -18);
        AddEnvironmentV7SpriteChild(boss.transform, "BossHall_V7_HangingChain_Right", "envv7_hanging_chain.png", new Vector2(155f, 0.92f), new Vector2(0.48f, 1.7f), new Color(0.76f, 0.72f, 0.62f, 0.3f), -18);
        AddEnvironmentV7SpriteChild(boss.transform, "BossHall_V7_WarningLamp", "envv7_warning_lamp.png", new Vector2(158f, -0.55f), new Vector2(0.5f, 0.5f), Color.white, 13).AddComponent<SpriteFlicker2D>();

        GameObject exit = NewChild(parent, "Details_08_MechCityGate");
        AddEnvironmentV7SpriteChild(exit.transform, "MechCity_EntranceGate_Visual", "envv7_exit_door.png", new Vector2(171.5f, -0.86f), new Vector2(2.55f, 4.0f), Color.white, 6);
        AddEnvironmentV8SpriteChild(exit.transform, "Exit_V8_OverheadPipe_Back", "envv8_top_pipe_soft.png", new Vector2(168f, 2.18f), new Vector2(10.8f, 0.64f), new Color(0.74f, 0.66f, 0.52f, 0.22f), -42);
        AddEnvironmentV7SpriteChild(exit.transform, "Exit_V7_ScrapPile", "envv7_scrap_pile.png", new Vector2(162.8f, -2.48f), new Vector2(2.25f, 0.76f), new Color(0.9f, 0.82f, 0.66f, 0.78f), 6);
        return awakeningBenchAssembly;
    }

    private static GameObject CreateAwakeningBenchRefinedAssembly(Transform parent)
    {
        GameObject root = NewChild(parent, "AwakeningBench_RefinedAssembly");
        AddEnvironmentV12SpriteChild(root.transform, "AwakeningBench_RefinedBody", "spawn_repair_bed_refined.png", new Vector2(4.26f, -2.04f), new Vector2(6.05f, 2.55f), Color.white, 9);
        CreateAwakeningBenchTablePolish(root.transform);
        return root;
    }

    private static void CreateAwakeningBenchTablePolish(Transform parent)
    {
        GameObject root = NewChild(parent, "AwakeningBench_TablePolish");
        AddEffectsV4SpriteChild(root.transform, "AwakeningBench_Table_UnderShadow", "fxv4_oil_haze.png", new Vector2(4.25f, -2.78f), new Vector2(5.7f, 0.62f), new Color(0.05f, 0.04f, 0.03f, 0.28f), 4);
        AddEffectsV5SpriteChild(root.transform, "AwakeningBench_Table_UnderGlow", "fxv5_lamp_halo_amber.png", new Vector2(4.25f, -2.42f), new Vector2(5.6f, 0.72f), new Color(1f, 0.54f, 0.16f, 0.11f), 5).AddComponent<SpriteFlicker2D>();
        AddEnvironmentV8SpriteChild(root.transform, "AwakeningBench_Table_TopScuffs", "envv8_broken_platform_marks.png", new Vector2(4.35f, -1.35f), new Vector2(3.85f, 0.28f), new Color(0.9f, 0.76f, 0.46f, 0.18f), 12);
        AddEnvironmentV10SpriteChild(root.transform, "AwakeningBench_Table_FrontRivets", "envv10_rivet_strip.png", new Vector2(1.75f, -2.06f), new Vector2(0.12f, 0.66f), new Color(0.94f, 0.78f, 0.52f, 0.34f), 13);
        AddEnvironmentV10SpriteChild(root.transform, "AwakeningBench_Table_FrontRivets_B", "envv10_rivet_strip.png", new Vector2(6.75f, -2.06f), new Vector2(0.12f, 0.66f), new Color(0.94f, 0.78f, 0.52f, 0.32f), 13);
        AddEnvironmentV10SpriteChild(root.transform, "AwakeningBench_Table_OilStain", "envv10_oil_stain_overlay.png", new Vector2(4.52f, -2.03f), new Vector2(1.05f, 0.26f), new Color(0.36f, 0.48f, 0.32f, 0.2f), 13);
        AddEnvironmentV7SpriteChild(root.transform, "AwakeningBench_Table_CableUnderRun", "envv7_cable_bundle.png", new Vector2(4.35f, -2.52f), new Vector2(2.6f, 0.22f), new Color(0.5f, 0.54f, 0.46f, 0.38f), 13);
        AddEffectsV2SpriteChild(root.transform, "AwakeningBench_Table_ScanLine", "fxv2_scan_beam.png", new Vector2(3.12f, -1.45f), new Vector2(0.2f, 0.92f), new Color(0.48f, 0.95f, 1f, 0.12f), 14).AddComponent<LoopingBackgroundMotion2D>();
        AddEnvironmentV7SpriteChild(root.transform, "AwakeningBench_Table_StatusLamp", "envv7_indicator_lamp_amber.png", new Vector2(6.7f, -1.7f), new Vector2(0.15f, 0.15f), new Color(1f, 0.64f, 0.18f, 0.82f), 15).AddComponent<SpriteFlicker2D>();
    }

    private static void CreateTutorialTriggers(Transform parent)
    {
        AddTutorialTrigger(parent, "T01_RustRepairStation_Reboot", new Vector2(4.4f, -1.5f), new Vector2(2.4f, 3f), "A/D 移动，Shift 奔跑。", "离开维修台");
        AddTutorialTrigger(parent, "T02_RustRepairStation_Movement", new Vector2(19.2f, -1.5f), new Vector2(3f, 3f), "Space 跳跃，继续向右。", "通过移动区");
        AddTutorialTrigger(parent, "T03_RustRepairStation_AttackCrate", new Vector2(27.4f, -1.5f), new Vector2(2.8f, 3f), "J 攻击，打碎铁箱。", "打碎铁箱");
        AddTutorialTrigger(parent, "T04_RustRepairStation_Jump", new Vector2(40.8f, -1.4f), new Vector2(2.8f, 3f), "看准落点，掉落会回到平台。", "越过断层");
        AddTutorialTrigger(parent, "T05_RustRepairStation_FirstEnemy", new Vector2(66.8f, -1.4f), new Vector2(2.8f, 3f), "看巡逻节奏，用 J 攻击。", "击败巡逻机器人");
        AddTutorialTrigger(parent, "T06_RustRepairStation_Traps", new Vector2(87.4f, -1.4f), new Vector2(3f, 3f), "等蓝电熄灭，避开红灯压缩机。", "通过机关厅");
        AddTutorialTrigger(parent, "T07_RustRepairStation_Charge", new Vector2(114.6f, -1.4f), new Vector2(3f, 3f), "按 E 充能，记录检查点。", "启动充电站");
        AddTutorialTrigger(parent, "T08_RustRepairStation_Boss", new Vector2(126.6f, -1.4f), new Vector2(3.4f, 3f), "进厅锁门，等 Boss 收招反击。", "击败守卫者");
    }

    private static void CreateSpawnIntro(Transform parent, GameObject player, Transform benchShakeRoot)
    {
        GameObject root = NewChild(parent, "SpawnIntro_AwakeningPolish");
        PlayerController2D playerController = player != null ? player.GetComponent<PlayerController2D>() : null;
        Rigidbody2D playerBody = player != null ? player.GetComponent<Rigidbody2D>() : null;
        PlayerRobotVisualAnimator2D playerVisualAnimator = player != null ? player.GetComponent<PlayerRobotVisualAnimator2D>() : null;
        CameraFollow2D cameraFollow = Camera.main != null ? Camera.main.GetComponent<CameraFollow2D>() : null;

        Transform cameraFocus = NewChild(root.transform, "SpawnIntro_CameraFocus").transform;
        cameraFocus.position = new Vector3(2.35f, -1.85f, 0f);
        Transform narrativeCameraTarget = NewChild(root.transform, "SpawnIntro_NarrativeCameraTarget").transform;
        narrativeCameraTarget.position = new Vector3(10.15f, -0.25f, 0f);
        Transform narrativeScanStart = NewChild(root.transform, "SpawnIntro_NarrativeScanStart").transform;
        narrativeScanStart.position = new Vector3(10.15f, -0.25f, 0f);
        Transform narrativeScanEnd = NewChild(root.transform, "SpawnIntro_NarrativeScanEnd").transform;
        narrativeScanEnd.position = cameraFocus.position;

        SpriteRenderer warmHalo = AddEffectsV2SpriteChild(root.transform, "SpawnIntro_WarmBenchHalo", "fxv2_lamp_halo_amber.png", new Vector2(4.18f, -1.28f), new Vector2(5.4f, 2.85f), new Color(1f, 0.56f, 0.18f, 0.24f), 8).GetComponent<SpriteRenderer>();
        SpriteRenderer floorGlow = AddEffectsV5SpriteChild(root.transform, "SpawnIntro_LowFloorGlow", "fxv5_lamp_halo_amber.png", new Vector2(3.8f, -2.45f), new Vector2(4.8f, 0.75f), new Color(1f, 0.58f, 0.18f, 0.14f), 8).GetComponent<SpriteRenderer>();
        SpriteRenderer groundFog = AddEffectsV5SpriteChild(root.transform, "SpawnIntro_GroundFog", "fxv5_fall_fog_plume.png", new Vector2(4.35f, -2.42f), new Vector2(4.8f, 0.9f), new Color(0.78f, 0.72f, 0.56f, 0.16f), 10).GetComponent<SpriteRenderer>();
        SpriteRenderer fineDust = AddEffectsV5SpriteChild(root.transform, "SpawnIntro_TopFineDust", "fxv5_fine_dust_motes.png", new Vector2(5.1f, 0.6f), new Vector2(5.6f, 3.4f), new Color(0.9f, 0.74f, 0.42f, 0.08f), -20).GetComponent<SpriteRenderer>();
        SpriteRenderer serviceGlow = AddEffectsV5SpriteChild(root.transform, "SpawnIntro_ServicePanelGlow", "fxv5_lamp_halo_amber.png", new Vector2(10.25f, 0.08f), new Vector2(1.8f, 1.45f), new Color(0.56f, 1f, 0.74f, 0.13f), -35).GetComponent<SpriteRenderer>();
        SpriteRenderer narrativeBackHalo = AddEffectsV5SpriteChild(root.transform, "SpawnIntro_NarrativeBackHalo", "fxv5_lamp_halo_amber.png", new Vector2(10.12f, -0.08f), new Vector2(4.6f, 2.5f), new Color(1f, 0.54f, 0.16f, 0.05f), -18).GetComponent<SpriteRenderer>();
        SpriteRenderer narrativeScanLine = AddEffectsV2SpriteChild(root.transform, "SpawnIntro_NarrativeScanLine", "fxv2_scan_beam.png", new Vector2(9.35f, 0.05f), new Vector2(0.34f, 3.35f), new Color(0.48f, 0.95f, 1f, 0.1f), 17).GetComponent<SpriteRenderer>();
        SpriteRenderer narrativeWeakArc = AddEffectsV5SpriteChild(root.transform, "SpawnIntro_NarrativeWeakArc", "fxv5_electric_spark_frame.png", new Vector2(8.65f, 0.42f), new Vector2(0.82f, 0.42f), new Color(0.46f, 0.95f, 1f, 0.08f), -14).GetComponent<SpriteRenderer>();
        SpriteRenderer narrativeDust = AddEffectsV5SpriteChild(root.transform, "SpawnIntro_NarrativeFineDust", "fxv5_fine_dust_motes.png", new Vector2(7.1f, 0.18f), new Vector2(7.0f, 4.0f), new Color(0.9f, 0.74f, 0.42f, 0.07f), -21).GetComponent<SpriteRenderer>();
        SpriteRenderer narrativeLamp = AddEnvironmentV7SpriteChild(root.transform, "SpawnIntro_NarrativeStatusLamp", "envv7_warning_lamp.png", new Vector2(9.25f, 0.74f), new Vector2(0.36f, 0.36f), new Color(1f, 0.32f, 0.16f, 0.32f), -12).GetComponent<SpriteRenderer>();
        narrativeLamp.gameObject.AddComponent<SpriteFlicker2D>();

        GameObject scanBeamObject = AddEffectsV2SpriteChild(root.transform, "SpawnIntro_ScanBeam", "fxv2_scan_beam.png", new Vector2(4.08f, -0.72f), new Vector2(0.46f, 2.35f), new Color(0.5f, 0.95f, 1f, 0.4f), 15);
        SpriteRenderer scanBeam = scanBeamObject.GetComponent<SpriteRenderer>();
        SpriteRenderer scanHalo = AddEffectsV5SpriteChild(root.transform, "SpawnIntro_ScanHalo", "fxv5_lamp_halo_amber.png", new Vector2(4.2f, -0.86f), new Vector2(2.0f, 1.25f), new Color(0.48f, 0.92f, 1f, 0.18f), 14).GetComponent<SpriteRenderer>();
        SpriteRenderer armSpark = AddEffectsV5SpriteChild(root.transform, "SpawnIntro_ArmSpark", "fxv5_electric_spark_frame.png", new Vector2(2.22f, -0.98f), new Vector2(0.54f, 0.34f), new Color(1f, 0.7f, 0.2f, 0.62f), 18).GetComponent<SpriteRenderer>();
        SpriteRenderer cableSpark = AddEffectsV2SpriteChild(root.transform, "SpawnIntro_CableSpark", "fxv2_spark_shower.png", new Vector2(6.2f, 0.88f), new Vector2(0.68f, 0.88f), new Color(1f, 0.68f, 0.18f, 0.38f), -10).GetComponent<SpriteRenderer>();
        SpriteRenderer bedArcLeft = AddEffectsV5SpriteChild(root.transform, "SpawnIntro_BedArc_Left", "fxv5_electric_spark_frame.png", new Vector2(2.52f, -1.55f), new Vector2(0.62f, 0.36f), new Color(0.48f, 0.94f, 1f, 0.5f), 18).GetComponent<SpriteRenderer>();
        SpriteRenderer bedArcRight = AddEffectsV5SpriteChild(root.transform, "SpawnIntro_BedArc_Right", "fxv5_electric_spark_frame.png", new Vector2(6.02f, -1.72f), new Vector2(0.58f, 0.34f), new Color(0.48f, 0.94f, 1f, 0.46f), 18).GetComponent<SpriteRenderer>();
        SpriteRenderer steamLeft = AddEffectsV2SpriteChild(root.transform, "SpawnIntro_SteamLeft", "fxv2_steam_puff.png", new Vector2(3.25f, -2.22f), new Vector2(1.4f, 0.8f), new Color(0.72f, 0.74f, 0.62f, 0.22f), 13).GetComponent<SpriteRenderer>();
        SpriteRenderer steamRight = AddEffectsV2SpriteChild(root.transform, "SpawnIntro_SteamRight", "fxv2_steam_puff.png", new Vector2(5.2f, -2.17f), new Vector2(1.25f, 0.72f), new Color(0.72f, 0.74f, 0.62f, 0.2f), 13).GetComponent<SpriteRenderer>();
        SpriteRenderer statusLamp = AddEnvironmentV7SpriteChild(root.transform, "SpawnIntro_BenchStatusLamp", "envv7_indicator_lamp_amber.png", new Vector2(3.32f, -1.38f), new Vector2(0.18f, 0.18f), new Color(1f, 0.62f, 0.14f, 0.9f), 16).GetComponent<SpriteRenderer>();
        GameObject repairArm = AddEnvironmentV7SpriteChild(root.transform, "SpawnIntro_RepairArm", "envv7_repair_arm.png", new Vector2(1.62f, -1.02f), new Vector2(1.15f, 1.0f), new Color(0.88f, 0.78f, 0.62f, 0.82f), 14);
        repairArm.transform.rotation = Quaternion.Euler(0f, 0f, -8f);

        PlayerSpawnIntro2D intro = root.AddComponent<PlayerSpawnIntro2D>();
        SetObject(intro, "playerController", playerController);
        SetObject(intro, "playerBody", playerBody);
        SetObject(intro, "playerVisualAnimator", playerVisualAnimator);
        SetObject(intro, "cameraFollow", cameraFollow);
        SetObject(intro, "introCameraTarget", cameraFocus);
        SetObject(intro, "narrativeCameraTarget", narrativeCameraTarget);
        SetObject(intro, "narrativeScanStart", narrativeScanStart);
        SetObject(intro, "narrativeScanEnd", narrativeScanEnd);
        SetObject(intro, "scanBeamTransform", scanBeamObject.transform);
        SetObject(intro, "repairArmTransform", repairArm.transform);
        SetObject(intro, "benchShakeRoot", benchShakeRoot);
        SetFloat(intro, "introSeconds", 10f);
        SetBool(intro, "allowSkip", true);
        SetBool(intro, "narrativeIntroEnabled", true);
        SetFloat(intro, "terminalLogSeconds", 2f);
        SetFloat(intro, "environmentScanSeconds", 4f);
        SetFloat(intro, "awakeningSeconds", 4f);
        SetString(intro, "narrativeHeader", "A-07 // 离线重启");
        SetString(intro, "narrativeLineA", "系统恢复中...");
        SetString(intro, "narrativeLineB", "记忆核心缺失");
        SetString(intro, "narrativeLineC", "维修站信号: 废弃");
        SetString(intro, "narrativeLineD", "指令: 向右寻找记忆核心");
        SetFloat(intro, "benchShakeAmplitude", 0.034f);
        SetFloat(intro, "cameraFocusShakeAmplitude", 0.012f);
        SetVector2(intro, "cameraMinBounds", new Vector2(-10f, -5.4f));
        SetVector2(intro, "cameraMaxBounds", new Vector2(18f, 6.8f));
        SetObjectArray(intro, "warmGlowRenderers", new Object[] { warmHalo, floorGlow });
        SetObjectArray(intro, "scanRenderers", new Object[] { scanBeam, scanHalo });
        SetObjectArray(intro, "steamRenderers", new Object[] { steamLeft, steamRight, groundFog });
        SetObjectArray(intro, "sparkRenderers", new Object[] { armSpark, cableSpark });
        SetObjectArray(intro, "electricRenderers", new Object[] { bedArcLeft, bedArcRight });
        SetObjectArray(intro, "dustRenderers", new Object[] { fineDust });
        SetObjectArray(intro, "serviceLightRenderers", new Object[] { serviceGlow, statusLamp });
        SetObjectArray(intro, "narrativeScanRenderers", new Object[] { narrativeScanLine, narrativeBackHalo });
        SetObjectArray(intro, "narrativeEnvironmentRenderers", new Object[] { narrativeWeakArc, narrativeDust, narrativeLamp });
    }

    private static Health CreateGameplayObjects(Transform parent, ChipData repairChip)
    {
        CreateCheckpoint(parent, "Checkpoint_AwakeningBench", new Vector2(2.2f, -1.85f));
        CreateCheckpoint(parent, "Checkpoint_BeforeJump", new Vector2(40.5f, -1.85f));
        CreateCheckpoint(parent, "Checkpoint_ChargingStation", new Vector2(117f, -1.85f));
        CreateLoreTerminal(
            parent,
            "LoreTerminal_RebootLog",
            new Vector2(10.2f, -2.05f),
            "E 读日志",
            "维修站已废弃。\n沿维修灯向右。",
            "向右离开");

        CreateDestructibleCrate(parent, "DestructibleCrate_AttackTutorial", new Vector2(28.2f, -1.025f));
        CreateRepairPatrolEnemy(parent, "Enemy_FirstRepairPatrol", new Vector2(76f, -2.18f), new Vector2(68.5f, -2.18f), new Vector2(83.5f, -2.18f), 3, 1.65f);
        CreateRepairPatrolEnemy(parent, "Enemy_TrapGuardRepairPatrol", new Vector2(89.1f, -2.18f), new Vector2(86.7f, -2.18f), new Vector2(92.2f, -2.18f), 2, 1.25f);
        TimedElectricFloor2D electricA = CreateTimedElectricFloor(parent, "Trap_ElectricFloor_A", new Vector2(95.7f, -2.08f), new Vector2(5.8f, 0.56f), 0f);
        TimedElectricFloor2D electricB = CreateTimedElectricFloor(parent, "Trap_ElectricFloor_B", new Vector2(102.05f, -2.08f), new Vector2(5.8f, 0.56f), 1.1f);
        CreatePowerSwitch(parent, "PowerSwitch_TrapBreaker", new Vector2(92.55f, -1.78f), new[] { electricA, electricB });
        CreateCompressorTrap(parent, "Trap_Compressor_01", new Vector2(106.2f, -1.82f), 0f);
        CreateCompressorTrap(parent, "Trap_Compressor_02", new Vector2(111.0f, -1.82f), 1.35f);
        CreateFallDeathZone(parent, "FallDeathZone_Global", new Vector2(88f, -6.1f), new Vector2(188f, 1.0f));
        CreateChargingStation(parent, "ChargingStation_Temporary", new Vector2(120f, -1.78f), new Vector2(117f, -1.85f));
        CreateSupplyCrate(parent, "SupplyCrate_OptionalJumpCache", new Vector2(52.2f, 1.68f));

        Health bossHealth = CreateRepairStationBoss(parent, "Boss_RepairStationGuardian", new Vector2(143.5f, -1.75f));
        CreateDoor(parent, "Door_BossExit_MechCity", new Vector2(159.5f, -1.15f), DoorUnlockMode.EnemyClear, new[] { bossHealth }, "", "", "出口解锁。");
        CreateBossEncounter(parent, bossHealth);

        GameObject exit = NewChild(parent, "ExitGoal_MechCityEntrance");
        exit.transform.position = new Vector3(172f, -1.6f, 0f);
        BoxCollider2D exitTrigger = exit.AddComponent<BoxCollider2D>();
        exitTrigger.isTrigger = true;
        exitTrigger.size = new Vector2(1.5f, 3f);
        exit.AddComponent<SceneExitGoal>();

        return bossHealth;
    }

    private static void CreateRespawnPolish(Transform parent, GameObject player)
    {
        GameObject root = NewChild(parent, "RespawnPolish_Runtime");
        PlayerController2D playerController = player != null ? player.GetComponent<PlayerController2D>() : null;
        Rigidbody2D playerBody = player != null ? player.GetComponent<Rigidbody2D>() : null;
        Health playerHealth = player != null ? player.GetComponent<Health>() : null;
        PlayerRobotVisualAnimator2D playerVisualAnimator = player != null ? player.GetComponent<PlayerRobotVisualAnimator2D>() : null;
        CameraFollow2D cameraFollow = Camera.main != null ? Camera.main.GetComponent<CameraFollow2D>() : null;

        Transform cameraFocus = NewChild(root.transform, "RespawnPolish_CameraFocus").transform;
        Transform failureAnchor = NewChild(root.transform, "RespawnPolish_FailureAnchor").transform;
        Transform returnAnchor = NewChild(root.transform, "RespawnPolish_ReturnAnchor").transform;

        SpriteRenderer failureFlash = AddEffectsV5SpriteChild(failureAnchor, "RespawnPolish_FailureFlash", "fxv5_lamp_halo_amber.png", new Vector2(0f, 0.18f), new Vector2(2.2f, 1.34f), new Color(1f, 0.68f, 0.28f, 0.64f), 33).GetComponent<SpriteRenderer>();
        SpriteRenderer failureSpark = AddEffectsV2SpriteChild(failureAnchor, "RespawnPolish_FailureSpark", "fxv2_spark_shower.png", new Vector2(0.18f, 0.24f), new Vector2(0.72f, 0.86f), new Color(1f, 0.68f, 0.18f, 0.72f), 34).GetComponent<SpriteRenderer>();
        SpriteRenderer failureDust = AddEffectsV5SpriteChild(failureAnchor, "RespawnPolish_FailureDust", "fxv5_fall_fog_plume.png", new Vector2(0f, -0.28f), new Vector2(1.95f, 0.76f), new Color(0.86f, 0.74f, 0.56f, 0.34f), 32).GetComponent<SpriteRenderer>();

        SpriteRenderer returnHalo = AddEffectsV5SpriteChild(returnAnchor, "RespawnPolish_ReturnHalo", "fxv5_lamp_halo_amber.png", new Vector2(0f, -0.28f), new Vector2(2.45f, 0.82f), new Color(0.52f, 1f, 0.82f, 0.46f), 33).GetComponent<SpriteRenderer>();
        GameObject scanBeamObject = AddEffectsV2SpriteChild(returnAnchor, "RespawnPolish_ReturnScanBeam", "fxv2_scan_beam.png", new Vector2(0f, 0.55f), new Vector2(0.48f, 2.35f), new Color(0.52f, 0.95f, 1f, 0.52f), 35);
        SpriteRenderer returnScan = scanBeamObject.GetComponent<SpriteRenderer>();
        SpriteRenderer returnSteam = AddEffectsV2SpriteChild(returnAnchor, "RespawnPolish_ReturnSteam", "fxv2_steam_puff.png", new Vector2(-0.35f, -0.18f), new Vector2(1.28f, 0.78f), new Color(0.72f, 0.82f, 0.72f, 0.34f), 34).GetComponent<SpriteRenderer>();
        SpriteRenderer returnSpark = AddEffectsV5SpriteChild(returnAnchor, "RespawnPolish_ReturnSpark", "fxv5_electric_spark_frame.png", new Vector2(0.38f, 0.18f), new Vector2(0.72f, 0.42f), new Color(0.52f, 1f, 0.86f, 0.56f), 36).GetComponent<SpriteRenderer>();
        SpriteRenderer returnDust = AddEffectsV5SpriteChild(returnAnchor, "RespawnPolish_ReturnDust", "fxv5_fine_dust_motes.png", new Vector2(0f, 0.52f), new Vector2(2.2f, 1.85f), new Color(0.8f, 0.96f, 0.76f, 0.22f), 31).GetComponent<SpriteRenderer>();
        SpriteRenderer returnLight = AddEnvironmentV7SpriteChild(returnAnchor, "RespawnPolish_ReturnStatusLight", "envv7_indicator_lamp_amber.png", new Vector2(0f, 0.18f), new Vector2(0.16f, 0.16f), new Color(0.58f, 1f, 0.8f, 0.85f), 37).GetComponent<SpriteRenderer>();

        PlayerRespawnCinematic2D cinematic = root.AddComponent<PlayerRespawnCinematic2D>();
        SetObject(cinematic, "playerController", playerController);
        SetObject(cinematic, "playerBody", playerBody);
        SetObject(cinematic, "playerHealth", playerHealth);
        SetObject(cinematic, "playerVisualAnimator", playerVisualAnimator);
        SetObject(cinematic, "cameraFollow", cameraFollow);
        SetObject(cinematic, "cameraFocusTarget", cameraFocus);
        SetObject(cinematic, "failureAnchor", failureAnchor);
        SetObject(cinematic, "returnAnchor", returnAnchor);
        SetObject(cinematic, "respawnScanBeam", scanBeamObject.transform);
        SetFloat(cinematic, "cinematicSeconds", 1.25f);
        SetFloat(cinematic, "teleportAt", 0.38f);
        SetObjectArray(cinematic, "failureRenderers", new Object[] { failureFlash, failureSpark, failureDust });
        SetObjectArray(cinematic, "returnRenderers", new Object[] { returnHalo, returnScan, returnSteam, returnSpark, returnDust, returnLight });

        CreateRespawnPointPolish(parent, "AwakeningBench", "Checkpoint_AwakeningBench", new Vector2(2.2f, -1.85f), new Color(1f, 0.58f, 0.18f, 0.22f), new Color(1f, 0.62f, 0.14f, 0.62f));
        CreateRespawnPointPolish(parent, "BeforeJump", "Checkpoint_BeforeJump", new Vector2(40.5f, -1.85f), new Color(1f, 0.68f, 0.22f, 0.18f), new Color(1f, 0.64f, 0.18f, 0.54f));
        CreateRespawnPointPolish(parent, "ChargingStation", "Checkpoint_ChargingStation", new Vector2(117f, -1.85f), new Color(0.42f, 1f, 0.72f, 0.24f), new Color(0.5f, 1f, 0.78f, 0.68f));
    }

    private static void CreateRespawnPointPolish(Transform parent, string key, string checkpointName, Vector2 position, Color haloColor, Color lightColor)
    {
        GameObject root = NewChild(parent, "RespawnPoint_Polish_" + key);
        root.transform.position = new Vector3(position.x, position.y, 0f);

        AddEffectsV5SpriteChild(root.transform, root.name + "_Halo", "fxv5_lamp_halo_amber.png", new Vector2(0f, -0.22f), new Vector2(2.15f, 0.64f), haloColor, 17).AddComponent<SpriteFlicker2D>();
        AddEffectsV5SpriteChild(root.transform, root.name + "_Dust", "fxv5_fine_dust_motes.png", new Vector2(0f, 0.45f), new Vector2(2.4f, 1.85f), new Color(0.82f, 0.78f, 0.54f, 0.075f), 12).AddComponent<AmbientDrift2D>();
        SpriteRenderer statusLight = AddEnvironmentV7SpriteChild(root.transform, root.name + "_StatusLamp", "envv7_indicator_lamp_amber.png", new Vector2(0f, 0.1f), new Vector2(0.16f, 0.16f), lightColor, 18).GetComponent<SpriteRenderer>();
        GameObject refined = NewChild(root.transform, checkpointName + "_PolishRefined");
        SpriteRenderer statusCore = AddEnvironmentV19SpriteChild(refined.transform, checkpointName + "_PolishRefined_StatusCore", "component_status_core_overlay.png", new Vector2(0f, 0.12f), new Vector2(0.42f, 0.42f), new Color(lightColor.r, lightColor.g, lightColor.b, 0.54f), 21).GetComponent<SpriteRenderer>();
        AddEffectsV2SpriteChild(refined.transform, checkpointName + "_PolishRefined_ScanLine", "fxv2_scan_beam.png", new Vector2(0f, 0.18f), new Vector2(0.18f, 0.9f), new Color(0.48f, 0.95f, 1f, 0.1f), 20).AddComponent<LoopingBackgroundMotion2D>();
        AddCheckpointFXPolish(root.transform, checkpointName, lightColor, out SpriteRenderer idleGlow, out SpriteRenderer activatedGlow);
        SpriteRenderer pulseRenderer = AddEffectsV5SpriteChild(root.transform, root.name + "_ActivationPulse", "fxv5_lamp_halo_amber.png", Vector2.zero, new Vector2(1.25f, 0.72f), new Color(lightColor.r, lightColor.g, lightColor.b, 0f), 20).GetComponent<SpriteRenderer>();
        OneShotSpriteBurst2D pulse = pulseRenderer.gameObject.AddComponent<OneShotSpriteBurst2D>();
        SetObject(pulse, "targetRenderer", pulseRenderer);
        SetFloat(pulse, "duration", 0.38f);
        SetVector2(pulse, "startScale", new Vector2(0.55f, 0.32f));
        SetVector2(pulse, "endScale", new Vector2(2.6f, 1.15f));
        SetFloat(pulse, "startAlpha", 0.72f);
        SetFloat(pulse, "endAlpha", 0f);
        SetFloat(pulse, "rotateDegrees", 0f);

        GameObject checkpoint = GameObject.Find(checkpointName);
        Checkpoint2D checkpointComponent = checkpoint != null ? checkpoint.GetComponent<Checkpoint2D>() : null;
        if (checkpointComponent != null)
        {
            SetObject(checkpointComponent, "activationPulse", pulse);
            SetObject(checkpointComponent, "activeLight", statusCore != null ? statusCore : statusLight);
            SetObject(checkpointComponent, "idleGlow", idleGlow);
            SetObject(checkpointComponent, "activatedGlow", activatedGlow);
        }
    }

    private static void AddArrowFXPolish(Transform parent, string signName, Vector2 position, Vector2 size, Color glowColor)
    {
        GameObject root = NewChild(parent, signName + "_FXPolish");
        AddEffectsV5SpriteChild(root.transform, signName + "_FXPolish_BackHalo", "fxv5_lamp_halo_amber.png", position + new Vector2(0.12f, 0f), new Vector2(size.x * 1.42f, size.y * 1.55f), glowColor, 14).AddComponent<SpriteFlicker2D>();

        GameObject scan = AddEffectsV2SpriteChild(root.transform, signName + "_FXPolish_DirectionScan", "fxv2_scan_beam.png", position + new Vector2(-size.x * 0.36f, 0f), new Vector2(0.16f, size.y * 1.12f), new Color(0.48f, 0.95f, 1f, 0.12f), 18);
        LoopingBackgroundMotion2D scanMotion = scan.AddComponent<LoopingBackgroundMotion2D>();
        SetVector2(scanMotion, "travel", new Vector2(size.x * 0.7f, 0f));
        SetFloat(scanMotion, "cycleSeconds", 1.65f);
        SetFloat(scanMotion, "minAlpha", 0.03f);
        SetFloat(scanMotion, "maxAlpha", 0.3f);
        SetFloat(scanMotion, "scalePulse", 0.04f);

        AddEnvironmentV7SpriteChild(root.transform, signName + "_FXPolish_StatusLamp", "envv7_indicator_lamp_amber.png", position + new Vector2(-size.x * 0.62f, size.y * 0.28f), new Vector2(0.14f, 0.14f), new Color(1f, 0.62f, 0.16f, 0.74f), 19).AddComponent<SpriteFlicker2D>();
        AddEffectsV5SpriteChild(root.transform, signName + "_FXPolish_TinySpark", "fxv5_electric_spark_frame.png", position + new Vector2(size.x * 0.52f, size.y * 0.14f), new Vector2(0.18f, 0.12f), new Color(1f, 0.7f, 0.22f, 0.18f), 20).AddComponent<ElectricArcFlicker2D>();
    }

    private static void AddCheckpointFXPolish(Transform parent, string checkpointName, Color lightColor, out SpriteRenderer idleGlow, out SpriteRenderer activatedGlow)
    {
        GameObject root = NewChild(parent, checkpointName + "_FXPolish");
        idleGlow = AddEffectsV5SpriteChild(root.transform, checkpointName + "_FXPolish_IdleRing", "fxv5_lamp_halo_amber.png", new Vector2(0f, -0.26f), new Vector2(2.6f, 0.86f), new Color(1f, 0.62f, 0.18f, 0.16f), 18).GetComponent<SpriteRenderer>();
        activatedGlow = AddEffectsV5SpriteChild(root.transform, checkpointName + "_FXPolish_ActivatedRing", "fxv5_lamp_halo_amber.png", new Vector2(0f, -0.2f), new Vector2(2.75f, 0.94f), new Color(lightColor.r, lightColor.g, lightColor.b, 0.04f), 19).GetComponent<SpriteRenderer>();

        GameObject scan = AddEffectsV2SpriteChild(root.transform, checkpointName + "_FXPolish_VerticalScan", "fxv2_scan_beam.png", new Vector2(0f, 0.28f), new Vector2(0.22f, 1.45f), new Color(0.48f, 0.95f, 1f, 0.12f), 20);
        LoopingBackgroundMotion2D scanMotion = scan.AddComponent<LoopingBackgroundMotion2D>();
        SetVector2(scanMotion, "travel", new Vector2(0f, 0.42f));
        SetFloat(scanMotion, "cycleSeconds", 2.35f);
        SetFloat(scanMotion, "minAlpha", 0.02f);
        SetFloat(scanMotion, "maxAlpha", 0.24f);
        SetFloat(scanMotion, "scalePulse", 0.05f);

        AddEffectsV5SpriteChild(root.transform, checkpointName + "_FXPolish_FineDust", "fxv5_fine_dust_motes.png", new Vector2(0f, 0.42f), new Vector2(2.6f, 1.7f), new Color(0.82f, 0.78f, 0.54f, 0.08f), 13).AddComponent<AmbientDrift2D>();
    }

    private static void CreateLoreTerminalPolish(Transform parent, string name, LoreTerminal lore, SpriteRenderer baseGlow)
    {
        GameObject root = NewChild(parent, name + "_PolishRefined");
        AddEnvironmentV19SpriteChild(root.transform, name + "_PolishRefined_TerminalScreenOverlay", "component_terminal_screen_overlay.png", new Vector2(0f, 0.2f), new Vector2(1.15f, 0.86f), new Color(0.94f, 0.9f, 0.78f, 0.92f), 12);
        SpriteRenderer scan = AddEffectsV2SpriteChild(root.transform, name + "_PolishRefined_ReadScan", "fxv2_scan_beam.png", new Vector2(-0.34f, 0.2f), new Vector2(0.16f, 0.86f), new Color(0.48f, 0.95f, 1f, 0.06f), 13).GetComponent<SpriteRenderer>();
        LoopingBackgroundMotion2D scanMotion = scan.gameObject.AddComponent<LoopingBackgroundMotion2D>();
        SetVector2(scanMotion, "travel", new Vector2(0.68f, 0f));
        SetFloat(scanMotion, "cycleSeconds", 1.9f);
        SetFloat(scanMotion, "minAlpha", 0.02f);
        SetFloat(scanMotion, "maxAlpha", 0.22f);
        SetFloat(scanMotion, "scalePulse", 0.04f);
        SpriteRenderer readFlash = AddEffectsV5SpriteChild(root.transform, name + "_PolishRefined_ReadFlash", "fxv5_lamp_halo_amber.png", new Vector2(0f, 0.2f), new Vector2(1.2f, 0.72f), new Color(0.48f, 0.95f, 1f, 0.04f), 14).GetComponent<SpriteRenderer>();
        SpriteRenderer spark = AddEffectsV5SpriteChild(root.transform, name + "_PolishRefined_CableSpark", "fxv5_electric_spark_frame.png", new Vector2(0.46f, -0.15f), new Vector2(0.2f, 0.14f), new Color(1f, 0.68f, 0.22f, 0f), 15).GetComponent<SpriteRenderer>();
        spark.gameObject.AddComponent<ElectricArcFlicker2D>();

        SetObjectArray(lore, "idleGlowRenderers", new Object[] { baseGlow });
        SetObjectArray(lore, "readPulseRenderers", new Object[] { readFlash });
    }

    private static void CreateSupplyCratePolish(Transform parent, string name, out SpriteRenderer statusCore, out SpriteRenderer softGlow)
    {
        GameObject root = NewChild(parent, name + "_PolishRefined");
        softGlow = AddEffectsV5SpriteChild(root.transform, name + "_PolishRefined_SafeHalo", "fxv5_lamp_halo_amber.png", new Vector2(0f, 0.06f), new Vector2(1.55f, 0.92f), new Color(0.45f, 0.95f, 1f, 0.1f), 19).GetComponent<SpriteRenderer>();
        statusCore = AddEnvironmentV19SpriteChild(root.transform, name + "_PolishRefined_StatusCore", "component_status_core_overlay.png", new Vector2(0.28f, 0.23f), new Vector2(0.34f, 0.34f), new Color(0.5f, 1f, 0.78f, 0.64f), 21).GetComponent<SpriteRenderer>();
        AddEnvironmentV10SpriteChild(root.transform, name + "_PolishRefined_MetalScuff", "envv10_scratch_plate_overlay.png", new Vector2(-0.1f, 0.18f), new Vector2(0.9f, 0.26f), new Color(0.9f, 0.72f, 0.42f, 0.12f), 22);
    }

    private static void CreatePowerSwitchPolish(Transform parent, string name, out SpriteRenderer readyGlow, out SpriteRenderer activeGlow, out SpriteRenderer cooldownGlow)
    {
        GameObject root = NewChild(parent, name + "_PolishRefined");
        AddEnvironmentV19SpriteChild(root.transform, name + "_PolishRefined_ControlPlate", "component_control_lock_plate_overlay.png", new Vector2(0f, 0.02f), new Vector2(0.92f, 1.14f), new Color(0.94f, 0.88f, 0.72f, 0.92f), 19);
        readyGlow = AddEffectsV5SpriteChild(root.transform, name + "_PolishRefined_ReadyAmber", "fxv5_lamp_halo_amber.png", new Vector2(-0.22f, 0.32f), new Vector2(0.54f, 0.38f), new Color(1f, 0.64f, 0.18f, 0.2f), 20).GetComponent<SpriteRenderer>();
        activeGlow = AddEffectsV5SpriteChild(root.transform, name + "_PolishRefined_ActiveCyan", "fxv5_lamp_halo_amber.png", new Vector2(0f, 0.31f), new Vector2(0.66f, 0.42f), new Color(0.42f, 0.95f, 1f, 0.06f), 21).GetComponent<SpriteRenderer>();
        cooldownGlow = AddEffectsV2SpriteChild(root.transform, name + "_PolishRefined_CooldownRed", "fxv2_lamp_halo_red.png", new Vector2(0.24f, 0.32f), new Vector2(0.52f, 0.36f), new Color(1f, 0.18f, 0.12f, 0.04f), 21).GetComponent<SpriteRenderer>();
        AddEffectsV5SpriteChild(root.transform, name + "_PolishRefined_CableSpark", "fxv5_electric_spark_frame.png", new Vector2(0.42f, -0.28f), new Vector2(0.22f, 0.16f), new Color(0.58f, 0.96f, 1f, 0.24f), 22).AddComponent<ElectricArcFlicker2D>();
    }

    private static void CreateElectricFloorPolish(Transform parent, string name, Vector2 size, out SpriteRenderer warningLight, out SpriteRenderer safeLight, out SpriteRenderer[] accentArcs)
    {
        GameObject root = NewChild(parent, name + "_PolishRefined");
        AddEnvironmentV10SpriteChild(root.transform, name + "_PolishRefined_RivetRail", "envv10_rivet_strip.png", new Vector2(0f, 0.38f), new Vector2(size.x * 0.92f, 0.08f), new Color(0.85f, 0.72f, 0.48f, 0.24f), 16);
        warningLight = AddEffectsV2SpriteChild(root.transform, name + "_PolishRefined_WarningRed", "fxv2_lamp_halo_red.png", new Vector2(-size.x * 0.46f, 0.34f), new Vector2(0.62f, 0.38f), new Color(1f, 0.16f, 0.1f, 0.18f), 20).GetComponent<SpriteRenderer>();
        safeLight = AddEffectsV5SpriteChild(root.transform, name + "_PolishRefined_SafeCyan", "fxv5_lamp_halo_amber.png", new Vector2(size.x * 0.46f, 0.34f), new Vector2(0.58f, 0.34f), new Color(0.45f, 1f, 0.76f, 0.12f), 20).GetComponent<SpriteRenderer>();
        SpriteRenderer arcA = AddEffectsV2SpriteChild(root.transform, name + "_PolishRefined_ArcA", "fxv2_electric_floor_02.png", new Vector2(-size.x * 0.22f, 0.3f), new Vector2(size.x * 0.34f, 0.3f), new Color(0.62f, 0.94f, 1f, 0.28f), 21).GetComponent<SpriteRenderer>();
        SpriteRenderer arcB = AddEffectsV2SpriteChild(root.transform, name + "_PolishRefined_ArcB", "fxv2_electric_floor_03.png", new Vector2(size.x * 0.2f, 0.32f), new Vector2(size.x * 0.3f, 0.28f), new Color(0.62f, 0.94f, 1f, 0.22f), 21).GetComponent<SpriteRenderer>();
        accentArcs = new[] { arcA, arcB };
    }

    private static void CreateChargingStationPolish(Transform parent, string name, out SpriteRenderer statusCore, out SpriteRenderer safeGlow)
    {
        GameObject root = NewChild(parent, name + "_PolishRefined");
        safeGlow = AddEffectsV5SpriteChild(root.transform, name + "_PolishRefined_SafeHalo", "fxv5_lamp_halo_amber.png", new Vector2(0f, 0.08f), new Vector2(1.65f, 2.35f), new Color(0.42f, 1f, 0.72f, 0.11f), 15).GetComponent<SpriteRenderer>();
        statusCore = AddEnvironmentV19SpriteChild(root.transform, name + "_PolishRefined_StatusCore", "component_status_core_overlay.png", new Vector2(0.18f, 0.46f), new Vector2(0.46f, 0.46f), new Color(0.48f, 1f, 0.78f, 0.66f), 20).GetComponent<SpriteRenderer>();
        GameObject scan = AddEffectsV2SpriteChild(root.transform, name + "_PolishRefined_ChargeScan", "fxv2_scan_beam.png", new Vector2(-0.42f, 0.22f), new Vector2(0.18f, 1.8f), new Color(0.48f, 0.95f, 1f, 0.11f), 19);
        LoopingBackgroundMotion2D motion = scan.AddComponent<LoopingBackgroundMotion2D>();
        SetVector2(motion, "travel", new Vector2(0.84f, 0f));
        SetFloat(motion, "cycleSeconds", 2.1f);
        SetFloat(motion, "maxAlpha", 0.24f);
        SetFloat(motion, "scalePulse", 0.06f);
    }

    private static void CreateDoorPolish(Transform parent, string name, out SpriteRenderer lockLight, out SpriteRenderer openScan, out SpriteRenderer steamGlow, out SpriteRenderer openedGlow, out SpriteRenderer unlockSpark, out SpriteRenderer lockedWarningGlow, out SpriteRenderer openSeamGlow, out SpriteRenderer unlockSteamBurst)
    {
        GameObject root = NewChild(parent, name + "_PolishRefined");
        CreateBossDoorRefinedArtV20(root.transform, name, "boss_exit_gate_refined_overlay.png", Vector2.zero, new Vector2(2.35f, 4.95f), new Color(0.92f, 0.96f, 0.86f, 0.9f), 15);
        AddEnvironmentV9SpriteChild(root.transform, name + "_PolishRefined_FramePipeLeft", "envv9_door_frame_pipe.png", new Vector2(-0.78f, 0f), new Vector2(0.42f, 4.5f), new Color(0.84f, 0.76f, 0.62f, 0.62f), 11);
        AddEnvironmentV9SpriteChild(root.transform, name + "_PolishRefined_FramePipeRight", "envv9_door_frame_pipe.png", new Vector2(0.78f, 0f), new Vector2(0.42f, 4.5f), new Color(0.84f, 0.76f, 0.62f, 0.62f), 11);
        AddEnvironmentV19SpriteChild(root.transform, name + "_PolishRefined_LockPlate", "component_control_lock_plate_overlay.png", new Vector2(0f, 0.92f), new Vector2(0.62f, 0.62f), new Color(0.94f, 0.86f, 0.68f, 0.9f), 16);
        lockLight = AddEffectsV2SpriteChild(root.transform, name + "_PolishRefined_LockRed", "fxv2_lamp_halo_red.png", new Vector2(0f, 0.92f), new Vector2(0.72f, 0.62f), new Color(1f, 0.15f, 0.1f, 0.42f), 17).GetComponent<SpriteRenderer>();
        openScan = AddEffectsV2SpriteChild(root.transform, name + "_PolishRefined_OpenScan", "fxv2_scan_beam.png", new Vector2(0f, -0.1f), new Vector2(0.3f, 4.35f), new Color(0.48f, 0.95f, 1f, 0.03f), 18).GetComponent<SpriteRenderer>();
        steamGlow = AddEffectsV2SpriteChild(root.transform, name + "_PolishRefined_DoorSteam", "fxv2_steam_puff.png", new Vector2(-0.56f, -1.74f), new Vector2(0.9f, 0.54f), new Color(0.78f, 0.74f, 0.62f, 0.08f), 18).GetComponent<SpriteRenderer>();
        AddDoorFXPolish(root.transform, name, out openedGlow, out unlockSpark);
        AddBossExitGateFXV20(root.transform, name, out lockedWarningGlow, out openSeamGlow, out unlockSteamBurst);
    }

    private static SpriteRenderer CreateBossDoorRefinedArtV20(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        return AddEnvironmentV20SpriteChild(parent, name + "_V20_RefinedOverlay", fileName, localPosition, targetSize, color, order).GetComponent<SpriteRenderer>();
    }

    private static void AddDoorFXPolish(Transform parent, string name, out SpriteRenderer openedGlow, out SpriteRenderer unlockSpark)
    {
        AddEnvironmentV7SpriteChild(parent, name + "_PolishRefined_LeftStatusLamp", "envv7_indicator_lamp_amber.png", new Vector2(-0.62f, 1.48f), new Vector2(0.18f, 0.18f), new Color(0.52f, 1f, 0.82f, 0.72f), 20).AddComponent<SpriteFlicker2D>();
        AddEnvironmentV7SpriteChild(parent, name + "_PolishRefined_RightStatusLamp", "envv7_indicator_lamp_amber.png", new Vector2(0.62f, 1.48f), new Vector2(0.18f, 0.18f), new Color(0.52f, 1f, 0.82f, 0.66f), 20).AddComponent<SpriteFlicker2D>();
        openedGlow = AddEffectsV5SpriteChild(parent, name + "_PolishRefined_OpenedCyanGlow", "fxv5_lamp_halo_amber.png", new Vector2(0f, -0.4f), new Vector2(1.75f, 3.55f), new Color(0.42f, 1f, 0.78f, 0.04f), 10).GetComponent<SpriteRenderer>();
        unlockSpark = AddEffectsV5SpriteChild(parent, name + "_PolishRefined_UnlockSpark", "fxv5_electric_spark_frame.png", new Vector2(0.2f, 0.86f), new Vector2(0.46f, 0.32f), new Color(0.56f, 1f, 0.9f, 0f), 21).GetComponent<SpriteRenderer>();
    }

    private static void AddBossExitGateFXV20(Transform parent, string name, out SpriteRenderer lockedWarningGlow, out SpriteRenderer openSeamGlow, out SpriteRenderer unlockSteamBurst)
    {
        lockedWarningGlow = AddEffectsV2SpriteChild(parent, name + "_V20FX_LockedWarningGlow", "fxv2_lamp_halo_red.png", new Vector2(0f, 0.9f), new Vector2(1.45f, 1.15f), new Color(1f, 0.12f, 0.08f, 0.18f), 21).GetComponent<SpriteRenderer>();
        openSeamGlow = AddEffectsV2SpriteChild(parent, name + "_V20FX_OpenSeamCyan", "fxv2_scan_beam.png", new Vector2(0f, -0.08f), new Vector2(0.18f, 4.1f), new Color(0.42f, 1f, 0.78f, 0.02f), 23).GetComponent<SpriteRenderer>();
        unlockSteamBurst = AddEffectsV2SpriteChild(parent, name + "_V20FX_UnlockSteamBurst", "fxv2_steam_puff.png", new Vector2(0.45f, -1.74f), new Vector2(1.2f, 0.62f), new Color(0.72f, 0.78f, 0.66f, 0.03f), 22).GetComponent<SpriteRenderer>();
        AddEffectsV5SpriteChild(parent, name + "_V20FX_TopStatusHalo", "fxv5_lamp_halo_amber.png", new Vector2(0f, 1.58f), new Vector2(1.6f, 0.48f), new Color(1f, 0.62f, 0.16f, 0.08f), 19).AddComponent<SpriteFlicker2D>();
    }

    private static void AddEntryLockFXPolish(Transform parent, out SpriteRenderer unlockedGlow, out SpriteRenderer unlockSpark)
    {
        GameObject root = NewChild(parent, "BossArena_EntryLock_FXPolish");
        unlockedGlow = AddEffectsV5SpriteChild(root.transform, "BossArena_EntryLock_FXPolish_UnlockGlow", "fxv5_lamp_halo_amber.png", new Vector2(0f, 1.08f), new Vector2(2.2f, 1.72f), new Color(0.42f, 1f, 0.78f, 0.02f), 20).GetComponent<SpriteRenderer>();
        unlockSpark = AddEffectsV5SpriteChild(root.transform, "BossArena_EntryLock_FXPolish_UnlockSpark", "fxv5_electric_spark_frame.png", new Vector2(0.36f, 1.32f), new Vector2(0.5f, 0.32f), new Color(0.52f, 1f, 0.9f, 0f), 25).GetComponent<SpriteRenderer>();

        GameObject scan = AddEffectsV2SpriteChild(root.transform, "BossArena_EntryLock_FXPolish_CenterScan", "fxv2_scan_beam.png", new Vector2(0f, 0.34f), new Vector2(0.22f, 3.1f), new Color(0.48f, 0.95f, 1f, 0.055f), 23);
        LoopingBackgroundMotion2D scanMotion = scan.AddComponent<LoopingBackgroundMotion2D>();
        SetVector2(scanMotion, "travel", new Vector2(0f, 0.58f));
        SetFloat(scanMotion, "cycleSeconds", 2.2f);
        SetFloat(scanMotion, "minAlpha", 0.015f);
        SetFloat(scanMotion, "maxAlpha", 0.16f);
        SetFloat(scanMotion, "scalePulse", 0.035f);
    }

    private static void AddBossEntryGateFXV20(Transform parent, out SpriteRenderer pressureGlow, out SpriteRenderer sideArcLeft, out SpriteRenderer sideArcRight, out SpriteRenderer bottomSteam, out SpriteRenderer unlockScan)
    {
        GameObject root = NewChild(parent, "BossArena_EntryLock_V20FX");
        pressureGlow = AddEffectsV2SpriteChild(root.transform, "BossArena_EntryLock_V20FX_PressureRedGlow", "fxv2_lamp_halo_red.png", new Vector2(0f, 1.08f), new Vector2(2.45f, 2.05f), new Color(1f, 0.12f, 0.08f, 0.24f), 22).GetComponent<SpriteRenderer>();
        sideArcLeft = AddEffectsV5SpriteChild(root.transform, "BossArena_EntryLock_V20FX_LeftArc", "fxv5_electric_spark_frame.png", new Vector2(-0.72f, 0.36f), new Vector2(0.46f, 0.28f), new Color(0.56f, 0.92f, 1f, 0.22f), 26).GetComponent<SpriteRenderer>();
        sideArcRight = AddEffectsV5SpriteChild(root.transform, "BossArena_EntryLock_V20FX_RightArc", "fxv5_electric_spark_frame.png", new Vector2(0.72f, -0.44f), new Vector2(0.44f, 0.26f), new Color(0.56f, 0.92f, 1f, 0.18f), 26).GetComponent<SpriteRenderer>();
        sideArcRight.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        bottomSteam = AddEffectsV2SpriteChild(root.transform, "BossArena_EntryLock_V20FX_BottomSteam", "fxv2_steam_puff.png", new Vector2(0f, -1.78f), new Vector2(1.25f, 0.6f), new Color(0.76f, 0.72f, 0.62f, 0.08f), 25).GetComponent<SpriteRenderer>();
        unlockScan = AddEffectsV2SpriteChild(root.transform, "BossArena_EntryLock_V20FX_UnlockScan", "fxv2_scan_beam.png", new Vector2(0f, 0.18f), new Vector2(0.24f, 3.55f), new Color(0.42f, 1f, 0.78f, 0f), 27).GetComponent<SpriteRenderer>();
    }

    private static void CreateDestructibleCrate(Transform parent, string name, Vector2 position)
    {
        GameObject crate = NewChild(parent, name);
        crate.transform.position = new Vector3(position.x, position.y, 0f);
        BoxCollider2D collider = crate.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1.2f, 3.15f);
        Health health = crate.AddComponent<Health>();
        SetInt(health, "maxHealth", 2);
        crate.AddComponent<HitFlash2D>();
        crate.AddComponent<DestructibleCrate2D>();
        AddEnvironmentV7SpriteChild(crate.transform, name + "_Visual", "envv7_crate.png", Vector2.zero, new Vector2(1.35f, 3.15f), Color.white, 16);
    }

    private static void CreateSupplyCrate(Transform parent, string name, Vector2 position)
    {
        GameObject crate = NewChild(parent, name);
        crate.transform.position = new Vector3(position.x, position.y, 0f);
        BoxCollider2D trigger = crate.AddComponent<BoxCollider2D>();
        trigger.isTrigger = true;
        trigger.size = new Vector2(1.35f, 1.05f);

        SpriteRenderer visual = AddEnvironmentV7SpriteChild(crate.transform, name + "_Visual", "envv7_crate.png", Vector2.zero, new Vector2(1.22f, 0.92f), new Color(0.9f, 1f, 0.86f, 0.96f), 18).GetComponent<SpriteRenderer>();
        CreateSupplyCratePolish(crate.transform, name, out SpriteRenderer statusCore, out SpriteRenderer softGlow);

        SupplyCrate2D supply = crate.AddComponent<SupplyCrate2D>();
        SetObject(supply, "visual", visual);
        SetObject(supply, "statusCore", statusCore);
        SetObject(supply, "softGlow", softGlow);
        SetInt(supply, "healAmount", 1);
        SetString(supply, "promptText", "E 开补给箱");
        SetString(supply, "usedMessage", "耐久恢复。");
    }

    private static void CreatePowerSwitch(Transform parent, string name, Vector2 position, TimedElectricFloor2D[] targetFloors)
    {
        GameObject powerSwitch = NewChild(parent, name);
        powerSwitch.transform.position = new Vector3(position.x, position.y, 0f);
        BoxCollider2D trigger = powerSwitch.AddComponent<BoxCollider2D>();
        trigger.isTrigger = true;
        trigger.size = new Vector2(1.65f, 2.2f);

        AddEnvironmentV9SpriteChild(powerSwitch.transform, name + "_Panel", "envv9_service_panel_lit.png", new Vector2(0f, -0.05f), new Vector2(1.0f, 1.25f), new Color(0.9f, 0.98f, 0.82f, 0.92f), 17);
        SpriteRenderer light = AddEffectsV5SpriteChild(powerSwitch.transform, name + "_BreakerLight", "fxv5_electric_spark_frame.png", new Vector2(0.08f, 0.24f), new Vector2(0.42f, 0.32f), new Color(1f, 0.68f, 0.24f, 0.45f), 20).GetComponent<SpriteRenderer>();
        AddEffectsV5SpriteChild(powerSwitch.transform, name + "_BreakerHalo", "fxv5_lamp_halo_amber.png", new Vector2(0.02f, 0.18f), new Vector2(1.55f, 1.0f), new Color(0.45f, 0.95f, 1f, 0.08f), 16).AddComponent<SpriteFlicker2D>();
        CreatePowerSwitchPolish(powerSwitch.transform, name, out SpriteRenderer readyGlow, out SpriteRenderer activeGlow, out SpriteRenderer cooldownGlow);

        PowerSwitch2D switchComponent = powerSwitch.AddComponent<PowerSwitch2D>();
        Object[] targetObjects = new Object[targetFloors != null ? targetFloors.Length : 0];
        for (int i = 0; i < targetObjects.Length; i++)
        {
            targetObjects[i] = targetFloors[i];
        }

        SetObjectArray(switchComponent, "targetFloors", targetObjects);
        SetObject(switchComponent, "switchLight", light);
        SetObject(switchComponent, "readyGlow", readyGlow);
        SetObject(switchComponent, "activeGlow", activeGlow);
        SetObject(switchComponent, "cooldownGlow", cooldownGlow);
        SetFloat(switchComponent, "safetySeconds", 5.2f);
        SetFloat(switchComponent, "cooldownSeconds", 7.0f);
        SetString(switchComponent, "promptText", "E 拉电闸");
    }

    private static Health CreateRepairPatrolEnemy(Transform parent, string name, Vector2 position, Vector2 pointA, Vector2 pointB, int healthValue, float speed)
    {
        return CreateRepairDroneObject(parent, name, position, pointA, pointB, healthValue, speed).GetComponent<Health>();
    }

    private static GameObject CreateRepairDroneObject(Transform parent, string name, Vector2 position, Vector2 pointA, Vector2 pointB, int healthValue, float speed)
    {
        GameObject enemy = NewChild(parent, name);
        enemy.transform.position = position;
        Rigidbody2D body = enemy.AddComponent<Rigidbody2D>();
        body.bodyType = RigidbodyType2D.Kinematic;
        BoxCollider2D collider = enemy.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(1.1f, 0.9f);

        Health health = enemy.AddComponent<Health>();
        SetInt(health, "maxHealth", healthValue);
        health.DisableOnDeath = false;
        EnemyPatrol2D patrol = enemy.AddComponent<EnemyPatrol2D>();
        SetFloat(patrol, "moveSpeed", speed);
        enemy.AddComponent<HitFlash2D>();

        GameObject visualRoot = NewChild(enemy.transform, name + "_VisualRoot");
        SpriteRenderer bodyVisual = AddEnemyV2SpriteChild(visualRoot.transform, name + "_Visual", "enemyv2_patrol_repair_bot.png", Vector2.zero, new Vector2(1.25f, 0.9f), Color.white, 18).GetComponent<SpriteRenderer>();
        SpriteRenderer eyeVisual = AddEnemyV2SpriteChild(visualRoot.transform, name + "_EyeLight_Red", "enemyv2_patrol_repair_bot_eye.png", new Vector2(0.08f, 0.06f), new Vector2(0.22f, 0.22f), new Color(1f, 0.58f, 0.42f, 0.85f), 21).GetComponent<SpriteRenderer>();
        SpriteRenderer wreckVisual = AddEnemyV2SpriteChild(enemy.transform, name + "_WreckVisual", "enemyv2_repair_bot_wreck.png", new Vector2(0f, -0.08f), new Vector2(1.15f, 0.72f), Color.white, 17).GetComponent<SpriteRenderer>();
        wreckVisual.enabled = false;
        GameObject hitSpark = AddEffectsV1SpriteChild(enemy.transform, name + "_HitSpark", "fx_hit_sparks.png", new Vector2(0f, 0.18f), new Vector2(0.58f, 0.58f), new Color(1f, 0.7f, 0.22f, 0.78f), 26);
        OneShotSpriteBurst2D hitSparkBurst = hitSpark.AddComponent<OneShotSpriteBurst2D>();
        SetVector2(hitSparkBurst, "startScale", new Vector2(0.45f, 0.45f));
        SetVector2(hitSparkBurst, "endScale", new Vector2(0.9f, 0.9f));
        GameObject deathSmoke = AddEffectsV1SpriteChild(enemy.transform, name + "_DeathSmoke", "fx_smoke_puff.png", new Vector2(0f, 0.18f), new Vector2(0.95f, 0.95f), new Color(0.78f, 0.72f, 0.62f, 0.58f), 25);
        OneShotSpriteBurst2D deathSmokeBurst = deathSmoke.AddComponent<OneShotSpriteBurst2D>();
        SetVector2(deathSmokeBurst, "startScale", new Vector2(0.55f, 0.55f));
        SetVector2(deathSmokeBurst, "endScale", new Vector2(1.35f, 1.35f));
        SetFloat(deathSmokeBurst, "duration", 0.55f);
        EnemyVisualAnimator2D visualAnimator = enemy.AddComponent<EnemyVisualAnimator2D>();
        SetObject(visualAnimator, "body", body);
        SetObject(visualAnimator, "visualRoot", visualRoot.transform);
        SetObject(visualAnimator, "bodyVisual", bodyVisual);
        SetObject(visualAnimator, "eyeLight", eyeVisual);
        SetObject(visualAnimator, "wreckVisual", wreckVisual);
        SetObject(visualAnimator, "hitSparkVisual", hitSparkBurst);
        SetObject(visualAnimator, "deathSmokeVisual", deathSmokeBurst);

        Transform a = NewChild(parent, name + "_PointA").transform;
        a.position = pointA;
        Transform b = NewChild(parent, name + "_PointB").transform;
        b.position = pointB;
        SetObject(patrol, "pointA", a);
        SetObject(patrol, "pointB", b);
        return enemy;
    }

    private static void CreateMovingPlatform(Transform parent, string name, Vector2 pointA, Vector2 pointB, Vector2 size)
    {
        GameObject platform = NewChild(parent, name);
        platform.transform.position = new Vector3(pointA.x, pointA.y, 0f);
        Rigidbody2D body = platform.AddComponent<Rigidbody2D>();
        body.bodyType = RigidbodyType2D.Kinematic;
        body.interpolation = RigidbodyInterpolation2D.Interpolate;
        BoxCollider2D collider = platform.AddComponent<BoxCollider2D>();
        collider.size = size;

        AddEnvironmentV8SpriteChild(platform.transform, name + "_V8_MovingDeck", "envv8_road_dark_steel_module.png", new Vector2(0f, 0.04f), new Vector2(size.x, Mathf.Max(0.18f, size.y * 0.62f)), Color.white, 12);
        AddEnvironmentV8SpriteChild(platform.transform, name + "_V8_AmberWalkableLip_00", "envv8_road_amber_top_lip.png", new Vector2(0f, size.y * 0.5f + 0.028f), new Vector2(size.x * 0.96f, 0.11f), new Color(1f, 0.94f, 0.72f, 0.9f), 16);
        AddEnvironmentV10SpriteChild(platform.transform, name + "_V10_MovingDeckShadow", "envv10_shallow_front_shadow.png", new Vector2(0f, -size.y * 0.32f), new Vector2(size.x * 0.64f, 0.07f), new Color(0.76f, 0.72f, 0.56f, 0.32f), 10);
        AddEffectsV5SpriteChild(platform.transform, name + "_V5_SafeMotorSpark", "fxv5_electric_spark_frame.png", new Vector2(size.x * 0.34f, -0.02f), new Vector2(0.38f, 0.18f), new Color(0.58f, 0.92f, 1f, 0.13f), 17).AddComponent<ElectricArcFlicker2D>();

        Transform pointATransform = NewChild(parent, name + "_PointA").transform;
        pointATransform.position = pointA;
        Transform pointBTransform = NewChild(parent, name + "_PointB").transform;
        pointBTransform.position = pointB;

        MovingPlatform2D movingPlatform = platform.AddComponent<MovingPlatform2D>();
        SetObject(movingPlatform, "pointA", pointATransform);
        SetObject(movingPlatform, "pointB", pointBTransform);
        SetFloat(movingPlatform, "moveSpeed", 1.6f);
        SetFloat(movingPlatform, "waitSeconds", 0.35f);
        SetBool(movingPlatform, "carryPlayer", true);
    }

    private static TimedElectricFloor2D CreateTimedElectricFloor(Transform parent, string name, Vector2 position, Vector2 size, float cycleOffsetSeconds = 0f)
    {
        GameObject floor = NewChild(parent, name);
        floor.transform.position = new Vector3(position.x, position.y, 0f);
        BoxCollider2D collider = floor.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(size.x, 0.52f);
        GameObject conductiveDeck = NewChild(floor.transform, name + "_ConductiveDeck");
        conductiveDeck.transform.localPosition = new Vector3(0f, 0f, 0f);
        BoxCollider2D deckCollider = conductiveDeck.AddComponent<BoxCollider2D>();
        deckCollider.size = new Vector2(size.x, 0.32f);
        AddEnvironmentV11SpriteChild(conductiveDeck.transform, name + "_ConductiveDeckVisual", "envv11_repair_wall_plate.png", Vector2.zero, new Vector2(size.x + 0.12f, 0.38f), new Color(0.54f, 0.66f, 0.64f, 0.86f), 13);
        SpriteRenderer visual = AddEffectsV2SpriteChild(floor.transform, name + "_BlueCurrentVisual", "fxv2_electric_floor_01.png", new Vector2(0f, 0.16f), new Vector2(size.x + 0.16f, 0.5f), new Color(0.76f, 0.94f, 1f, 0.9f), 18).GetComponent<SpriteRenderer>();
        CreateElectricFloorPolish(floor.transform, name, size, out SpriteRenderer warningLight, out SpriteRenderer safeLight, out SpriteRenderer[] accentArcs);
        TimedElectricFloor2D electricFloor = floor.AddComponent<TimedElectricFloor2D>();
        SetObject(electricFloor, "electricVisual", visual);
        SetObject(electricFloor, "warningLight", warningLight);
        SetObject(electricFloor, "safeLight", safeLight);
        SetObjectArray(electricFloor, "accentArcs", accentArcs);
        SetFloat(electricFloor, "activeSeconds", 1.05f);
        SetFloat(electricFloor, "inactiveSeconds", 1.25f);
        SetFloat(electricFloor, "cycleOffsetSeconds", cycleOffsetSeconds);
        SetInt(electricFloor, "damage", 1);
        return electricFloor;
    }

    private static void CreateCompressorTrap(Transform parent, string name, Vector2 position, float cycleOffsetSeconds = 0f)
    {
        GameObject compressor = NewChild(parent, name);
        compressor.transform.position = new Vector3(position.x, position.y, 0f);
        BoxCollider2D collider = compressor.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(3.6f, 3.0f);

        GameObject plate = AddEnvironmentV7SpriteChild(compressor.transform, name + "_MovingPlate", "envv7_compressor_shell.png", new Vector2(0f, 1.45f), new Vector2(3.4f, 2.05f), Color.white, 16);
        GameObject warning = AddEnvironmentV7SpriteChild(compressor.transform, name + "_WarningLamp_Red", "envv7_warning_lamp.png", new Vector2(0f, 1.62f), new Vector2(0.38f, 0.38f), new Color(1f, 0.82f, 0.76f, 0.92f), 19);

        CompressorTrap2D trap = compressor.AddComponent<CompressorTrap2D>();
        SetObject(trap, "movingPlate", plate.transform);
        SetObject(trap, "warningLight", warning.GetComponent<SpriteRenderer>());
        SetFloat(trap, "topLocalY", 1.45f);
        SetFloat(trap, "bottomLocalY", -0.36f);
        SetFloat(trap, "waitSeconds", 1.15f);
        SetFloat(trap, "warningSeconds", 0.65f);
        SetFloat(trap, "slamSeconds", 0.32f);
        SetFloat(trap, "holdSeconds", 0.38f);
        SetFloat(trap, "returnSeconds", 0.72f);
        SetFloat(trap, "cycleOffsetSeconds", cycleOffsetSeconds);
    }

    private static void CreateChargingStation(Transform parent, string name, Vector2 position, Vector2 respawnPosition)
    {
        GameObject station = NewChild(parent, name);
        station.transform.position = new Vector3(position.x, position.y, 0f);
        BoxCollider2D trigger = station.AddComponent<BoxCollider2D>();
        trigger.isTrigger = true;
        trigger.size = new Vector2(2.2f, 2.8f);
        SpriteRenderer visual = AddEnvironmentV7SpriteChild(station.transform, name + "_Visual", "envv7_charging_station.png", Vector2.zero, new Vector2(1.45f, 2.26f), Color.white, 16).GetComponent<SpriteRenderer>();
        CreateChargingStationPolish(station.transform, name, out SpriteRenderer statusCore, out SpriteRenderer safeGlow);
        Transform respawn = NewChild(station.transform, name + "_RespawnPoint").transform;
        respawn.position = respawnPosition;
        ChargingStation2D charging = station.AddComponent<ChargingStation2D>();
        SetObject(charging, "respawnPoint", respawn);
        SetObject(charging, "chargeLight", statusCore != null ? statusCore : visual);
        SetObject(charging, "statusCore", statusCore);
        SetObject(charging, "safeGlow", safeGlow);
    }

    private static Health CreateRepairStationBoss(Transform parent, string name, Vector2 position)
    {
        GameObject boss = NewChild(parent, name);
        boss.transform.position = new Vector3(position.x, position.y, 0f);
        Rigidbody2D body = boss.AddComponent<Rigidbody2D>();
        body.bodyType = RigidbodyType2D.Kinematic;
        BoxCollider2D collider = boss.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(4.2f, 3.2f);

        Health health = boss.AddComponent<Health>();
        SetInt(health, "maxHealth", 14);
        health.DisableOnDeath = false;
        boss.AddComponent<HitFlash2D>();
        RepairStationBoss2D bossAi = boss.AddComponent<RepairStationBoss2D>();

        GameObject bodyVisual = AddEnemyV2SpriteChild(boss.transform, name + "_BodyVisual", "bossv2_guardian_body.png", new Vector2(0f, 0.05f), new Vector2(5.45f, 5.05f), Color.white, 18);
        SpriteRenderer bodyRenderer = bodyVisual.GetComponent<SpriteRenderer>();
        GameObject eye = AddEnemyV2SpriteChild(boss.transform, name + "_EyeLight_Red", "bossv2_guardian_eye.png", new Vector2(0.03f, 0.55f), new Vector2(0.72f, 0.72f), new Color(1f, 0.58f, 0.44f, 0.86f), 24);
        eye.AddComponent<SpriteFlicker2D>();
        CreateBossVisualRefinementV3(
            boss.transform,
            name,
            out SpriteRenderer refinedOverlay,
            out SpriteRenderer coreLight,
            out SpriteRenderer crackGlow,
            out SpriteRenderer deathCoreFlash,
            out SpriteRenderer deathDustVeil,
            out SpriteRenderer deathSparkBurst,
            out SpriteRenderer[] deathFragments);
        SpriteRenderer overloadOverlay = CreateBossOverloadVisualV4(boss.transform, name);

        Transform armPivot = NewChild(boss.transform, name + "_ArmPivot").transform;
        armPivot.localPosition = new Vector3(-0.55f, 0.05f, 0f);
        AddEnvironmentV7SpriteChild(armPivot, name + "_SweepArmVisual", "envv7_mechanical_arm.png", new Vector2(-1.55f, -0.15f), new Vector2(3.35f, 0.86f), Color.white, 22);

        BoxCollider2D sweep = NewChild(boss.transform, name + "_SweepHitbox").AddComponent<BoxCollider2D>();
        sweep.transform.localPosition = new Vector3(-2.15f, -0.28f, 0f);
        sweep.isTrigger = true;
        sweep.size = new Vector2(3.8f, 1.0f);
        BossDamageHitbox2D sweepDamage = sweep.gameObject.AddComponent<BossDamageHitbox2D>();

        BoxCollider2D smash = NewChild(boss.transform, name + "_SmashHitbox").AddComponent<BoxCollider2D>();
        smash.transform.localPosition = new Vector3(-1.05f, -1.25f, 0f);
        smash.isTrigger = true;
        smash.size = new Vector2(2.4f, 1.2f);
        BossDamageHitbox2D smashDamage = smash.gameObject.AddComponent<BossDamageHitbox2D>();

        BoxCollider2D shockwave = NewChild(boss.transform, name + "_ShockwaveHitbox").AddComponent<BoxCollider2D>();
        shockwave.transform.localPosition = new Vector3(-2.15f, -1.25f, 0f);
        shockwave.isTrigger = true;
        shockwave.size = new Vector2(2.35f, 0.72f);
        BossDamageHitbox2D shockwaveDamage = shockwave.gameObject.AddComponent<BossDamageHitbox2D>();

        BoxCollider2D arcBurst = NewChild(boss.transform, name + "_ArcBurstHitbox").AddComponent<BoxCollider2D>();
        arcBurst.transform.localPosition = new Vector3(0f, -1.26f, 0f);
        arcBurst.isTrigger = true;
        arcBurst.size = new Vector2(2.45f, 1.12f);
        BossDamageHitbox2D arcBurstDamage = arcBurst.gameObject.AddComponent<BossDamageHitbox2D>();

        BoxCollider2D coreBeam = NewChild(boss.transform, name + "_CoreBeamHitbox").AddComponent<BoxCollider2D>();
        coreBeam.transform.localPosition = new Vector3(-3.35f, -0.68f, 0f);
        coreBeam.isTrigger = true;
        coreBeam.enabled = false;
        coreBeam.size = new Vector2(4.2f, 0.72f);
        BossDamageHitbox2D coreBeamDamage = coreBeam.gameObject.AddComponent<BossDamageHitbox2D>();

        BoxCollider2D[] ceilingSparkHitboxes = new BoxCollider2D[3];
        BossDamageHitbox2D[] ceilingSparkDamage = new BossDamageHitbox2D[3];
        string[] ceilingSparkSuffixes = { "A", "B", "C" };
        float[] ceilingSparkX = { -2.65f, 0f, 2.65f };
        for (int i = 0; i < ceilingSparkHitboxes.Length; i++)
        {
            BoxCollider2D sparkHitbox = NewChild(boss.transform, name + "_CeilingSparkHitbox_" + ceilingSparkSuffixes[i]).AddComponent<BoxCollider2D>();
            sparkHitbox.transform.localPosition = new Vector3(ceilingSparkX[i], -0.57f, 0f);
            sparkHitbox.isTrigger = true;
            sparkHitbox.enabled = false;
            sparkHitbox.size = new Vector2(0.85f, 2.35f);
            ceilingSparkHitboxes[i] = sparkHitbox;
            ceilingSparkDamage[i] = sparkHitbox.gameObject.AddComponent<BossDamageHitbox2D>();
        }

        BoxCollider2D finalPulseLeft = NewChild(boss.transform, name + "_FinalPulseHitbox_Left").AddComponent<BoxCollider2D>();
        finalPulseLeft.transform.localPosition = new Vector3(-3.1f, -1.18f, 0f);
        finalPulseLeft.isTrigger = true;
        finalPulseLeft.enabled = false;
        finalPulseLeft.size = new Vector2(4.15f, 0.68f);
        BossDamageHitbox2D finalPulseLeftDamage = finalPulseLeft.gameObject.AddComponent<BossDamageHitbox2D>();

        BoxCollider2D finalPulseRight = NewChild(boss.transform, name + "_FinalPulseHitbox_Right").AddComponent<BoxCollider2D>();
        finalPulseRight.transform.localPosition = new Vector3(3.1f, -1.18f, 0f);
        finalPulseRight.isTrigger = true;
        finalPulseRight.enabled = false;
        finalPulseRight.size = new Vector2(4.15f, 0.68f);
        BossDamageHitbox2D finalPulseRightDamage = finalPulseRight.gameObject.AddComponent<BossDamageHitbox2D>();

        SpriteRenderer smashWarning = AddEffectsV2SpriteChild(boss.transform, name + "_SmashWarning_Dust", "fxv2_boss_smash_dust.png", new Vector2(-1.05f, -1.48f), new Vector2(3.2f, 0.78f), new Color(1f, 0.7f, 0.18f, 0f), 20).GetComponent<SpriteRenderer>();
        SpriteRenderer sweepTrailVisual = AddEffectsV5SpriteChild(boss.transform, name + "_SweepTrail_ElectricDrag", "fxv5_electric_spark_frame.png", new Vector2(-1.55f, -0.24f), new Vector2(2.6f, 0.42f), new Color(1f, 0.66f, 0.2f, 0f), 23).GetComponent<SpriteRenderer>();
        SpriteRenderer smashDustRingVisual = AddEffectsV2SpriteChild(boss.transform, name + "_SmashDustRing", "fxv2_boss_smash_dust.png", new Vector2(-1.05f, -1.36f), new Vector2(3.6f, 0.95f), new Color(1f, 0.62f, 0.16f, 0f), 21).GetComponent<SpriteRenderer>();
        SpriteRenderer shockwaveVisual = AddEffectsV5SpriteChild(boss.transform, name + "_ShockwaveVisual", "fxv5_electric_spark_frame.png", new Vector2(-2.0f, -1.22f), new Vector2(2.2f, 0.42f), new Color(0.52f, 0.92f, 1f, 0f), 21).GetComponent<SpriteRenderer>();
        SpriteRenderer shockwaveSparkTrail = AddEffectsV2SpriteChild(boss.transform, name + "_ShockwaveSparkTrail", "fxv2_electric_floor_03.png", new Vector2(-2.0f, -1.22f), new Vector2(2.6f, 0.46f), new Color(0.48f, 0.96f, 1f, 0f), 22).GetComponent<SpriteRenderer>();
        SpriteRenderer arcBurstWarning = AddEffectsV5SpriteChild(boss.transform, name + "_ArcBurstWarning", "fxv5_electric_spark_frame.png", new Vector2(0f, -1.42f), new Vector2(2.65f, 0.48f), new Color(0.45f, 0.96f, 1f, 0f), 22).GetComponent<SpriteRenderer>();
        SpriteRenderer arcBurstVisual = AddEffectsV2SpriteChild(boss.transform, name + "_ArcBurstVisual", "fxv2_electric_floor_02.png", new Vector2(0f, -1.36f), new Vector2(2.35f, 0.66f), new Color(0.58f, 0.96f, 1f, 0f), 23).GetComponent<SpriteRenderer>();
        SpriteRenderer coreBeamWarning = AddEffectsV2SpriteChild(boss.transform, name + "_CoreBeamWarningScan", "fxv2_scan_beam.png", new Vector2(-3.35f, -0.72f), new Vector2(4.2f, 0.24f), new Color(1f, 0.28f, 0.12f, 0f), 24).GetComponent<SpriteRenderer>();
        SpriteRenderer coreBeamVisual = AddEffectsV2SpriteChild(boss.transform, name + "_CoreBeamVisual", "fxv2_electric_floor_01.png", new Vector2(-3.35f, -0.62f), new Vector2(4.35f, 0.46f), new Color(0.5f, 0.95f, 1f, 0f), 25).GetComponent<SpriteRenderer>();
        SpriteRenderer[] ceilingWarnings = new SpriteRenderer[3];
        SpriteRenderer[] ceilingVisuals = new SpriteRenderer[3];
        for (int i = 0; i < ceilingSparkX.Length; i++)
        {
            ceilingWarnings[i] = AddEffectsV2SpriteChild(boss.transform, name + "_CeilingSparkWarning_" + ceilingSparkSuffixes[i], "fxv2_scan_beam.png", new Vector2(ceilingSparkX[i], 0.22f), new Vector2(0.3f, 3.8f), new Color(1f, 0.34f, 0.1f, 0f), 24).GetComponent<SpriteRenderer>();
            ceilingVisuals[i] = AddEffectsV5SpriteChild(boss.transform, name + "_CeilingSparkVisual_" + ceilingSparkSuffixes[i], "fxv5_electric_spark_frame.png", new Vector2(ceilingSparkX[i], -0.95f), new Vector2(0.75f, 1.2f), new Color(0.52f, 0.95f, 1f, 0f), 26).GetComponent<SpriteRenderer>();
        }
        SpriteRenderer finalPulseWarning = AddEffectsV5SpriteChild(boss.transform, name + "_FinalPulseWarning", "fxv5_electric_spark_frame.png", new Vector2(0f, -1.24f), new Vector2(6.2f, 0.5f), new Color(1f, 0.22f, 0.08f, 0f), 24).GetComponent<SpriteRenderer>();
        SpriteRenderer finalPulseLeftVisual = AddEffectsV2SpriteChild(boss.transform, name + "_FinalPulseLeftVisual", "fxv2_electric_floor_03.png", new Vector2(-3.1f, -1.18f), new Vector2(3.9f, 0.48f), new Color(0.48f, 0.96f, 1f, 0f), 26).GetComponent<SpriteRenderer>();
        SpriteRenderer finalPulseRightVisual = AddEffectsV2SpriteChild(boss.transform, name + "_FinalPulseRightVisual", "fxv2_electric_floor_03.png", new Vector2(3.1f, -1.18f), new Vector2(3.9f, 0.48f), new Color(0.48f, 0.96f, 1f, 0f), 26).GetComponent<SpriteRenderer>();
        SpriteRenderer hitSparkVisual = AddEffectsV2SpriteChild(boss.transform, name + "_HitSpark", "fxv2_spark_shower.png", new Vector2(-0.42f, 0.78f), new Vector2(1.25f, 0.72f), new Color(1f, 0.66f, 0.24f, 0f), 27).GetComponent<SpriteRenderer>();
        SpriteRenderer phaseSteamBoost = AddEffectsV2SpriteChild(boss.transform, name + "_PhaseSteamBoost", "fxv2_steam_puff.png", new Vector2(0f, 1.3f), new Vector2(2.85f, 2.15f), new Color(0.82f, 0.76f, 0.66f, 0f), 25).GetComponent<SpriteRenderer>();
        SpriteRenderer steamA = AddEffectsV2SpriteChild(boss.transform, name + "_SteamPuff_A", "fxv2_steam_puff.png", new Vector2(-1.6f, 1.7f), new Vector2(0.9f, 0.9f), new Color(0.82f, 0.74f, 0.62f, 0.1f), 23).GetComponent<SpriteRenderer>();
        SpriteRenderer steamB = AddEffectsV2SpriteChild(boss.transform, name + "_SteamPuff_B", "fxv2_steam_puff.png", new Vector2(1.15f, 1.45f), new Vector2(1.05f, 1.0f), new Color(0.82f, 0.74f, 0.62f, 0.08f), 23).GetComponent<SpriteRenderer>();
        SpriteRenderer steamC = AddEffectsV2SpriteChild(boss.transform, name + "_SteamPuff_C", "fxv2_steam_puff.png", new Vector2(0.35f, 1.95f), new Vector2(0.72f, 0.72f), new Color(0.82f, 0.74f, 0.62f, 0.07f), 23).GetComponent<SpriteRenderer>();
        GameObject deathSmoke = AddEffectsV2SpriteChild(boss.transform, name + "_DeathSmoke", "fxv2_steam_puff.png", new Vector2(0f, 0.7f), new Vector2(2.4f, 2.2f), new Color(0.76f, 0.7f, 0.62f, 0.62f), 26);
        OneShotSpriteBurst2D deathSmokeBurst = deathSmoke.AddComponent<OneShotSpriteBurst2D>();
        SetVector2(deathSmokeBurst, "startScale", new Vector2(0.85f, 0.72f));
        SetVector2(deathSmokeBurst, "endScale", new Vector2(2.45f, 1.8f));
        SetFloat(deathSmokeBurst, "duration", 0.82f);

        GameObject minionPrefab = CreateRepairDroneObject(parent, "Boss_MinionPrefab_RepairDrone", new Vector2(-80f, -80f), new Vector2(135f, -2.18f), new Vector2(151f, -2.18f), 1, 2.15f);
        minionPrefab.SetActive(false);

        Transform summonA = NewChild(boss.transform, name + "_SummonPointA").transform;
        summonA.position = new Vector3(135f, -2.18f, 0f);
        Transform summonB = NewChild(boss.transform, name + "_SummonPointB").transform;
        summonB.position = new Vector3(151f, -2.18f, 0f);

        SetObject(bossAi, "armPivot", armPivot);
        SetObject(bossAi, "bodyVisual", bodyVisual.transform);
        SetObject(bossAi, "bodyRenderer", bodyRenderer);
        SetObject(bossAi, "refinedOverlay", refinedOverlay);
        SetObject(bossAi, "overloadOverlay", overloadOverlay);
        SetObject(bossAi, "eyeLight", eye.GetComponent<SpriteRenderer>());
        SetObject(bossAi, "coreLight", coreLight);
        SetObject(bossAi, "crackGlow", crackGlow);
        SetObject(bossAi, "smashWarning", smashWarning);
        SetObject(bossAi, "sweepTrailVisual", sweepTrailVisual);
        SetObject(bossAi, "smashDustRingVisual", smashDustRingVisual);
        SetObject(bossAi, "shockwaveVisual", shockwaveVisual);
        SetObject(bossAi, "shockwaveSparkTrail", shockwaveSparkTrail);
        SetObject(bossAi, "arcBurstWarning", arcBurstWarning);
        SetObject(bossAi, "arcBurstVisual", arcBurstVisual);
        SetObject(bossAi, "coreBeamWarning", coreBeamWarning);
        SetObject(bossAi, "coreBeamVisual", coreBeamVisual);
        SetObjectArray(bossAi, "ceilingSparkWarnings", ceilingWarnings);
        SetObjectArray(bossAi, "ceilingSparkVisuals", ceilingVisuals);
        SetObject(bossAi, "finalPulseWarning", finalPulseWarning);
        SetObject(bossAi, "finalPulseLeftVisual", finalPulseLeftVisual);
        SetObject(bossAi, "finalPulseRightVisual", finalPulseRightVisual);
        SetObject(bossAi, "hitSparkVisual", hitSparkVisual);
        SetObject(bossAi, "phaseSteamBoost", phaseSteamBoost);
        SetObjectArray(bossAi, "steamPuffs", new Object[] { steamA, steamB, steamC });
        SetObject(bossAi, "deathSmokeVisual", deathSmokeBurst);
        SetObject(bossAi, "deathCoreFlash", deathCoreFlash);
        SetObject(bossAi, "deathDustVeil", deathDustVeil);
        SetObject(bossAi, "deathSparkBurst", deathSparkBurst);
        SetObjectArray(bossAi, "deathFragments", deathFragments);
        SetObject(bossAi, "sweepHitbox", sweep);
        SetObject(bossAi, "smashHitbox", smash);
        SetObject(bossAi, "shockwaveHitbox", shockwave);
        SetObject(bossAi, "arcBurstHitbox", arcBurst);
        SetObject(bossAi, "coreBeamHitbox", coreBeam);
        SetObjectArray(bossAi, "ceilingSparkHitboxes", ceilingSparkHitboxes);
        SetObject(bossAi, "finalPulseLeftHitbox", finalPulseLeft);
        SetObject(bossAi, "finalPulseRightHitbox", finalPulseRight);
        SetObject(bossAi, "minionPrefab", minionPrefab);
        SetObject(bossAi, "summonPointA", summonA);
        SetObject(bossAi, "summonPointB", summonB);
        SetFloat(bossAi, "introSeconds", 0.58f);
        SetFloat(bossAi, "windupSeconds", 0.82f);
        SetFloat(bossAi, "actionSeconds", 0.76f);
        SetFloat(bossAi, "recoverSeconds", 0.55f);
        SetFloat(bossAi, "sweepRecoverSeconds", 0.55f);
        SetFloat(bossAi, "smashRecoverSeconds", 0.75f);
        SetFloat(bossAi, "shockwaveRecoverSeconds", 0.85f);
        SetFloat(bossAi, "shockwaveWindupSeconds", 0.72f);
        SetFloat(bossAi, "shockwaveSeconds", 0.62f);
        SetFloat(bossAi, "arcBurstWindupSeconds", 0.9f);
        SetFloat(bossAi, "arcBurstSeconds", 0.58f);
        SetFloat(bossAi, "arcBurstRecoverSeconds", 0.78f);
        SetFloat(bossAi, "coreBeamWindupSeconds", 0.75f);
        SetFloat(bossAi, "coreBeamSeconds", 0.52f);
        SetFloat(bossAi, "coreBeamRecoverSeconds", 0.86f);
        SetFloat(bossAi, "ceilingSparkWindupSeconds", 0.82f);
        SetFloat(bossAi, "ceilingSparkSeconds", 0.68f);
        SetFloat(bossAi, "ceilingSparkRecoverSeconds", 0.88f);
        SetFloat(bossAi, "finalCorePulseWindupSeconds", 0.9f);
        SetFloat(bossAi, "finalCorePulseSeconds", 0.72f);
        SetFloat(bossAi, "finalCorePulseRecoverSeconds", 0.95f);
        SetFloat(bossAi, "comboWindupSeconds", 0.72f);
        SetFloat(bossAi, "sweepShockComboSeconds", 1.48f);
        SetFloat(bossAi, "smashArcComboSeconds", 1.56f);
        SetFloat(bossAi, "finalComboRecoverSeconds", 0.95f);
        SetFloat(bossAi, "phaseTwoHealthRatio", 0.5f);
        SetFloat(bossAi, "deathShowSeconds", 1.8f);
        SetInt(bossAi, "lowHealthSummonThreshold", 7);
        SetInt(bossAi, "finalPhaseHealthThreshold", 4);
        SetVector2(bossAi, "sweepHitboxOffset", new Vector2(-2.15f, -0.28f));
        SetVector2(bossAi, "smashHitboxOffset", new Vector2(-1.05f, -1.25f));
        SetVector2(bossAi, "smashWarningOffset", new Vector2(-1.05f, -1.48f));
        SetVector2(bossAi, "shockwaveHitboxStartOffset", new Vector2(-2.15f, -1.25f));
        SetVector2(bossAi, "shockwaveHitboxEndOffset", new Vector2(-4.3f, -1.25f));
        SetVector2(bossAi, "shockwaveVisualStartOffset", new Vector2(-2.0f, -1.22f));
        SetVector2(bossAi, "shockwaveVisualEndOffset", new Vector2(-4.65f, -1.22f));
        SetVector2(bossAi, "arcBurstLocalXRange", new Vector2(-7.2f, 7.2f));
        SetFloat(bossAi, "arcBurstGroundY", -1.36f);
        SetVector2(bossAi, "coreBeamHitboxOffset", new Vector2(-3.35f, -0.68f));
        SetVector2(bossAi, "coreBeamVisualOffset", new Vector2(-3.35f, -0.62f));
        SetVector2(bossAi, "ceilingSparkLocalXRange", new Vector2(-6.4f, 6.4f));
        SetFloat(bossAi, "ceilingSparkGroundY", -0.95f);
        SetVector2(bossAi, "finalPulseLeftHitboxOffset", new Vector2(-3.1f, -1.18f));
        SetVector2(bossAi, "finalPulseRightHitboxOffset", new Vector2(3.1f, -1.18f));
        SetObject(sweepDamage, "owner", bossAi);
        SetObject(smashDamage, "owner", bossAi);
        SetObject(shockwaveDamage, "owner", bossAi);
        SetObject(arcBurstDamage, "owner", bossAi);
        SetObject(coreBeamDamage, "owner", bossAi);
        for (int i = 0; i < ceilingSparkDamage.Length; i++)
        {
            SetObject(ceilingSparkDamage[i], "owner", bossAi);
        }
        SetObject(finalPulseLeftDamage, "owner", bossAi);
        SetObject(finalPulseRightDamage, "owner", bossAi);

        return health;
    }

    private static void CreateBossVisualRefinementV3(
        Transform parent,
        string name,
        out SpriteRenderer refinedOverlay,
        out SpriteRenderer coreLight,
        out SpriteRenderer crackGlow,
        out SpriteRenderer deathCoreFlash,
        out SpriteRenderer deathDustVeil,
        out SpriteRenderer deathSparkBurst,
        out SpriteRenderer[] deathFragments)
    {
        GameObject root = NewChild(parent, name + "_V3RefinedAssembly");
        refinedOverlay = AddEnemyV3SpriteChild(root.transform, name + "_V3RefinedOverlay", "bossv3_guardian_refined_overlay.png", new Vector2(0f, 0.22f), new Vector2(5.95f, 4.72f), new Color(1f, 0.94f, 0.86f, 0.86f), 19).GetComponent<SpriteRenderer>();
        coreLight = AddEffectsV2SpriteChild(root.transform, name + "_CoreLight_Red", "fxv2_lamp_halo_red.png", new Vector2(0.02f, 0.55f), new Vector2(1.25f, 0.95f), new Color(1f, 0.16f, 0.08f, 0.22f), 25).GetComponent<SpriteRenderer>();
        crackGlow = AddEnvironmentV10SpriteChild(root.transform, name + "_CrackGlow_Overload", "envv10_cracked_metal_overlay.png", new Vector2(0.05f, 0.3f), new Vector2(2.4f, 1.6f), new Color(1f, 0.34f, 0.08f, 0.04f), 26).GetComponent<SpriteRenderer>();
        deathCoreFlash = AddEffectsV2SpriteChild(root.transform, name + "_DeathCoreFlash", "fxv2_lamp_halo_red.png", new Vector2(0.03f, 0.55f), new Vector2(2.0f, 1.5f), new Color(1f, 0.18f, 0.08f, 0f), 28).GetComponent<SpriteRenderer>();
        deathDustVeil = AddEffectsV5SpriteChild(root.transform, name + "_DeathDustVeil", "fxv5_falling_dust_curtain.png", new Vector2(0f, 0.65f), new Vector2(5.6f, 2.5f), new Color(0.82f, 0.74f, 0.58f, 0f), 27).GetComponent<SpriteRenderer>();
        deathSparkBurst = AddEffectsV2SpriteChild(root.transform, name + "_DeathSparkBurst", "fxv2_spark_shower.png", new Vector2(0.05f, 0.72f), new Vector2(2.2f, 1.35f), new Color(1f, 0.62f, 0.18f, 0f), 29).GetComponent<SpriteRenderer>();

        deathFragments = new[]
        {
            AddEnvironmentV10SpriteChild(root.transform, name + "_DeathFragment_A", "envv10_broken_corner_cap.png", new Vector2(-1.25f, 0.85f), new Vector2(0.62f, 0.42f), new Color(0.88f, 0.72f, 0.5f, 0f), 30).GetComponent<SpriteRenderer>(),
            AddEnvironmentV10SpriteChild(root.transform, name + "_DeathFragment_B", "envv10_broken_corner_cap.png", new Vector2(1.12f, 0.66f), new Vector2(0.58f, 0.38f), new Color(0.78f, 0.64f, 0.46f, 0f), 30).GetComponent<SpriteRenderer>(),
            AddEnvironmentV7SpriteChild(root.transform, name + "_DeathFragment_C", "envv7_robot_debris.png", new Vector2(-0.45f, -0.24f), new Vector2(0.72f, 0.38f), new Color(0.8f, 0.7f, 0.56f, 0f), 30).GetComponent<SpriteRenderer>(),
            AddEnvironmentV7SpriteChild(root.transform, name + "_DeathFragment_D", "envv7_scrap_pile.png", new Vector2(0.82f, -0.35f), new Vector2(0.84f, 0.34f), new Color(0.78f, 0.66f, 0.48f, 0f), 30).GetComponent<SpriteRenderer>(),
            AddEnvironmentV10SpriteChild(root.transform, name + "_DeathFragment_E", "envv10_rivet_strip.png", new Vector2(0f, -0.72f), new Vector2(0.18f, 0.72f), new Color(0.95f, 0.75f, 0.44f, 0f), 30).GetComponent<SpriteRenderer>(),
        };

        deathFragments[1].transform.rotation = Quaternion.Euler(0f, 0f, 18f);
        deathFragments[2].transform.rotation = Quaternion.Euler(0f, 0f, -12f);
        deathFragments[3].transform.rotation = Quaternion.Euler(0f, 0f, 9f);
        deathFragments[4].transform.rotation = Quaternion.Euler(0f, 0f, 72f);
    }

    private static SpriteRenderer CreateBossOverloadVisualV4(Transform parent, string name)
    {
        GameObject root = NewChild(parent, name + "_V4OverloadAssembly");
        return AddEnemyV4SpriteChild(
            root.transform,
            name + "_V4OverloadOverlay",
            "bossv4_guardian_overload_overlay.png",
            new Vector2(0f, 0.22f),
            new Vector2(6.05f, 4.78f),
            new Color(1f, 0.92f, 0.82f, 0f),
            27).GetComponent<SpriteRenderer>();
    }

    private static void CreateBossEncounter(Transform parent, Health bossHealth)
    {
        RepairStationBoss2D bossAi = bossHealth != null ? bossHealth.GetComponent<RepairStationBoss2D>() : null;

        GameObject entryLock = NewChild(parent, "BossArena_EntryLock");
        entryLock.transform.position = new Vector3(127.0f, -0.95f, 0f);
        BoxCollider2D lockCollider = entryLock.AddComponent<BoxCollider2D>();
        lockCollider.size = new Vector2(1.05f, 4.15f);
        lockCollider.enabled = false;

        SpriteRenderer lockVisual = AddEnvironmentV7SpriteChild(entryLock.transform, "BossArena_EntryLock_Slab", "envv7_boss_door.png", Vector2.zero, new Vector2(1.1f, 4.25f), new Color(1f, 0.48f, 0.3f, 0.74f), 21).GetComponent<SpriteRenderer>();
        SpriteRenderer lockOverlay = CreateBossDoorRefinedArtV20(entryLock.transform, "BossArena_EntryLock", "boss_entry_lock_refined_overlay.png", Vector2.zero, new Vector2(2.18f, 4.78f), new Color(1f, 0.92f, 0.82f, 0.92f), 23);
        SpriteRenderer lockLight = AddEnvironmentV7SpriteChild(entryLock.transform, "BossArena_EntryLock_RedLamp", "envv7_warning_lamp.png", new Vector2(0f, 1.35f), new Vector2(0.62f, 0.62f), new Color(1f, 0.26f, 0.14f, 0.88f), 24).GetComponent<SpriteRenderer>();
        lockLight.gameObject.AddComponent<SpriteFlicker2D>();
        SpriteRenderer lockHalo = AddEffectsV2SpriteChild(entryLock.transform, "BossArena_EntryLock_RedHalo", "fxv2_lamp_halo_red.png", new Vector2(0f, 1.22f), new Vector2(2.0f, 1.8f), new Color(1f, 0.16f, 0.08f, 0.24f), 20).GetComponent<SpriteRenderer>();
        AddEntryLockFXPolish(entryLock.transform, out SpriteRenderer entryUnlockGlow, out SpriteRenderer entryUnlockSpark);
        AddBossEntryGateFXV20(entryLock.transform, out SpriteRenderer entryPressureGlow, out SpriteRenderer entrySideArcLeft, out SpriteRenderer entrySideArcRight, out SpriteRenderer entryBottomSteam, out SpriteRenderer entryUnlockScan);

        GameObject trigger = NewChild(parent, "BossEncounter_StartTrigger");
        trigger.transform.position = new Vector3(129.35f, -1.35f, 0f);
        BoxCollider2D triggerCollider = trigger.AddComponent<BoxCollider2D>();
        triggerCollider.isTrigger = true;
        triggerCollider.size = new Vector2(3.0f, 3.2f);

        BossEncounterController2D encounter = trigger.AddComponent<BossEncounterController2D>();
        SetObject(encounter, "boss", bossAi);
        SetObject(encounter, "bossHealth", bossHealth);
        SetObject(encounter, "entryLockRoot", entryLock);
        SetObject(encounter, "entryLockCollider", lockCollider);
        SetObject(encounter, "entryLockVisual", lockVisual);
        SetObject(encounter, "entryLockLight", lockLight);
        SetObject(encounter, "entryLockEngagedHalo", lockHalo);
        SetObject(encounter, "entryLockUnlockedGlow", entryUnlockGlow);
        SetObject(encounter, "entryLockUnlockSpark", entryUnlockSpark);
        SetObject(encounter, "entryLockRefinedOverlay", lockOverlay);
        SetObject(encounter, "entryLockPressureGlow", entryPressureGlow);
        SetObject(encounter, "entryLockSideArcLeft", entrySideArcLeft);
        SetObject(encounter, "entryLockSideArcRight", entrySideArcRight);
        SetObject(encounter, "entryLockBottomSteam", entryBottomSteam);
        SetObject(encounter, "entryLockUnlockScan", entryUnlockScan);
        SetVector2(encounter, "cameraMinBounds", new Vector2(124f, -5.4f));
        SetVector2(encounter, "cameraMaxBounds", new Vector2(162.2f, 6.8f));
        SetString(encounter, "bossDisplayName", "维修站守卫者");
        SetString(encounter, "startObjective", "击败守卫者");
        SetString(encounter, "startHint", "入口锁闭，等收招反击。");
        entryLock.SetActive(false);
    }

    private static void CreateFinalDoorWatcher(Health sentinelHealth)
    {
        if (sentinelHealth == null)
        {
            return;
        }

        DoorLock[] doors = GameObject.FindObjectsOfType<DoorLock>();
        foreach (DoorLock door in doors)
        {
            SerializedObject so = new SerializedObject(door);
            SerializedProperty mode = so.FindProperty("unlockMode");
            if (mode != null && mode.enumValueIndex == (int)DoorUnlockMode.EnemyClear)
            {
                SetHealthArray(door, "watchedEnemies", new[] { sentinelHealth });
                break;
            }
        }
    }

    private static ChipData CreateRepairChipAsset()
    {
        string chipPath = "Assets/Data/RepairChip.asset";
        ChipData chip = AssetDatabase.LoadAssetAtPath<ChipData>(chipPath);
        if (chip == null)
        {
            chip = ScriptableObject.CreateInstance<ChipData>();
            AssetDatabase.CreateAsset(chip, chipPath);
        }

        chip.chipId = "repair_chip";
        chip.displayName = "修复芯片";
        chip.description = "击败敌人时恢复 1 耐久。";
        chip.healOnEnemyKill = true;
        chip.healAmount = 1;
        EditorUtility.SetDirty(chip);
        return chip;
    }

    private static void CreateChipTerminal(Transform parent, ChipData repairChip)
    {
        GameObject terminal = NewChild(parent, "ChipTerminal_RepairChip");
        terminal.transform.position = new Vector3(88.4f, -2.0f, 0f);
        AddRectChild(terminal.transform, "Terminal_Base", new Vector2(0f, -0.35f), new Vector2(1.1f, 0.7f), new Color(0.18f, 0.17f, 0.15f), 7);
        AddEnvironmentV3SpriteChild(terminal.transform, "Terminal_SoftGlow", "envv3_soft_glow_round_amber.png", new Vector2(0f, 0.12f), new Vector2(0.68f, 0.38f), new Color(1f, 0.58f, 0.08f, 0.5f), 9).AddComponent<SpriteFlicker2D>();

        GameObject pickup = NewChild(terminal.transform, "Pickup_RepairChip");
        pickup.transform.localPosition = new Vector3(0f, 0.85f, 0f);
        SpriteRenderer renderer = pickup.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEnvironmentV3SpritePath("envv3_repair_chip_pickup.png"));
        renderer.color = new Color(1f, 0.86f, 0.42f, 0.96f);
        renderer.sortingOrder = 15;
        ScaleSpriteToWidth(renderer, 0.48f);
        CircleCollider2D trigger = pickup.AddComponent<CircleCollider2D>();
        trigger.isTrigger = true;
        trigger.radius = 2.2f;
        ChipPickup chipPickup = pickup.AddComponent<ChipPickup>();
        pickup.AddComponent<BobAndPulse2D>();
        SetObject(chipPickup, "chip", repairChip);
    }

    private static Health CreateEnemy(Transform parent, string name, Vector2 position, Vector2 pointA, Vector2 pointB, int healthValue, float speed, Color color)
    {
        GameObject enemy = NewChild(parent, name);
        enemy.transform.position = position;
        Rigidbody2D body = enemy.AddComponent<Rigidbody2D>();
        body.bodyType = RigidbodyType2D.Kinematic;
        BoxCollider2D collider = enemy.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = name.Contains("Sentinel") ? new Vector2(1.25f, 1.55f) : new Vector2(0.9f, 0.55f);

        Health health = enemy.AddComponent<Health>();
        SetInt(health, "maxHealth", healthValue);
        EnemyPatrol2D patrol = enemy.AddComponent<EnemyPatrol2D>();
        SetFloat(patrol, "moveSpeed", speed);
        enemy.AddComponent<HitFlash2D>();

        AddRectChild(enemy.transform, "Hull", new Vector2(0f, 0f), name.Contains("Sentinel") ? new Vector2(1.2f, 1.1f) : new Vector2(0.82f, 0.38f), color, 18);
        AddRectChild(enemy.transform, "Amber_Sensor", new Vector2(0.28f, 0.12f), new Vector2(0.18f, 0.14f), new Color(1f, 0.55f, 0.06f), 19);
        if (name.Contains("Sentinel"))
        {
            AddRectChild(enemy.transform, "Old_Cannon", new Vector2(0.72f, -0.1f), new Vector2(0.65f, 0.18f), new Color(0.25f, 0.24f, 0.22f), 18);
            SetInt(patrol, "contactDamage", 1);
        }

        Transform a = NewChild(parent, name + "_PointA").transform;
        a.position = pointA;
        Transform b = NewChild(parent, name + "_PointB").transform;
        b.position = pointB;
        SetObject(patrol, "pointA", a);
        SetObject(patrol, "pointB", b);

        return health;
    }

    private static void CreateLoreTerminal(Transform parent, string name, Vector2 position, string prompt, string message, string objective)
    {
        GameObject terminal = NewChild(parent, name);
        terminal.transform.position = new Vector3(position.x, position.y, 0f);
        BoxCollider2D trigger = terminal.AddComponent<BoxCollider2D>();
        trigger.isTrigger = true;
        trigger.size = new Vector2(2.1f, 2.4f);

        LoreTerminal lore = terminal.AddComponent<LoreTerminal>();
        SetString(lore, "promptText", prompt);
        SetString(lore, "message", message);
        SetString(lore, "objectiveAfterRead", objective);

        AddEnvironmentV7SpriteChild(terminal.transform, name + "_TerminalVisual", "envv7_terminal_console.png", new Vector2(0f, -0.1f), new Vector2(1.28f, 1.55f), Color.white, 10);
        SpriteRenderer screenGlow = AddEffectsV2SpriteChild(terminal.transform, name + "_ScreenGlow", "fxv2_lamp_halo_amber.png", new Vector2(0f, 0.27f), new Vector2(0.82f, 0.46f), new Color(1f, 0.58f, 0.08f, 0.28f), 11).GetComponent<SpriteRenderer>();
        CreateLoreTerminalPolish(terminal.transform, name, lore, screenGlow);
    }

    private static DoorLock CreateDoor(Transform parent, string name, Vector2 position, DoorUnlockMode mode, Health[] watchedEnemies, string prompt, string locked, string opened)
    {
        GameObject door = NewChild(parent, name);
        door.transform.position = position;
        BoxCollider2D trigger = door.AddComponent<BoxCollider2D>();
        trigger.isTrigger = true;
        trigger.size = new Vector2(4f, 4.8f);

        DoorLock lockComponent = door.AddComponent<DoorLock>();
        SetEnum(lockComponent, "unlockMode", (int)mode);
        SetString(lockComponent, "promptText", prompt);
        SetString(lockComponent, "lockedMessage", locked);
        SetString(lockComponent, "openMessage", opened);

        GameObject slab = AddEnvironmentV7SpriteChild(door.transform, name + "_SlabVisual", "envv7_exit_door.png", Vector2.zero, new Vector2(1.35f, 4.8f), Color.white, 12);
        BoxCollider2D solid = slab.AddComponent<BoxCollider2D>();
        solid.size = Vector2.one;
        SetObject(lockComponent, "solidCollider", solid);

        AddEnvironmentV7SpriteChild(door.transform, name + "_AmberLock_Socket", "envv7_warning_lamp.png", new Vector2(0f, 0.9f), new Vector2(0.46f, 0.46f), new Color(1f, 0.66f, 0.22f, 0.92f), 13).AddComponent<SpriteFlicker2D>();
        CreateDoorPolish(door.transform, name, out SpriteRenderer lockLight, out SpriteRenderer openScan, out SpriteRenderer steamGlow, out SpriteRenderer openedGlow, out SpriteRenderer unlockSpark, out SpriteRenderer lockedWarningGlow, out SpriteRenderer openSeamGlow, out SpriteRenderer unlockSteamBurst);
        SetObject(lockComponent, "lockLight", lockLight);
        SetObject(lockComponent, "openScan", openScan);
        SetObject(lockComponent, "steamGlow", steamGlow);
        SetObject(lockComponent, "openedGlow", openedGlow);
        SetObject(lockComponent, "unlockSpark", unlockSpark);
        SetObject(lockComponent, "lockedWarningGlow", lockedWarningGlow);
        SetObject(lockComponent, "openSeamGlow", openSeamGlow);
        SetObject(lockComponent, "unlockSteamBurst", unlockSteamBurst);
        if (watchedEnemies != null)
        {
            SetHealthArray(lockComponent, "watchedEnemies", watchedEnemies);
        }

        return lockComponent;
    }

    private static void CreateCheckpoint(Transform parent, string name, Vector2 position)
    {
        GameObject checkpoint = NewChild(parent, name);
        checkpoint.transform.position = position;
        BoxCollider2D trigger = checkpoint.AddComponent<BoxCollider2D>();
        trigger.isTrigger = true;
        trigger.size = new Vector2(2.4f, 3f);
        checkpoint.AddComponent<Checkpoint2D>();
    }

    private static void AddTutorialTrigger(Transform parent, string name, Vector2 position, Vector2 size, string hint, string objective)
    {
        GameObject trigger = NewChild(parent, name);
        trigger.transform.position = position;
        BoxCollider2D collider = trigger.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = size;
        TutorialTrigger tutorial = trigger.AddComponent<TutorialTrigger>();
        SetString(tutorial, "hintMessage", hint);
        SetString(tutorial, "objectiveText", objective);
    }

    private static void AddInsulatedStep(Transform parent, string name, Vector2 position, Vector2 size)
    {
        GameObject step = NewChild(parent, name);
        step.transform.position = new Vector3(position.x, position.y, 0f);
        BoxCollider2D collider = step.AddComponent<BoxCollider2D>();
        collider.size = size;

        GameObject visualRoot = NewChild(step.transform, name + "_Visual");
        AddEnvironmentV10SpriteChild(visualRoot.transform, name + "_V10_InsulatedPadShadow", "envv10_shallow_front_shadow.png", new Vector2(0f, -size.y * 0.42f), new Vector2(size.x * 0.68f, 0.065f), new Color(0.62f, 0.72f, 0.58f, 0.34f), 12);
        AddEnvironmentV11SpriteChild(visualRoot.transform, name + "_V11_InsulatedPad", "envv11_repair_wall_plate.png", new Vector2(0f, size.y * 0.04f), new Vector2(size.x + 0.04f, Mathf.Max(0.2f, size.y + 0.04f)), new Color(0.72f, 0.82f, 0.7f, 0.86f), 14);
        AddEffectsV5SpriteChild(visualRoot.transform, name + "_V11_SafeSpark", "fxv5_electric_spark_frame.png", new Vector2(0f, size.y * 0.28f), new Vector2(size.x * 0.48f, 0.2f), new Color(0.55f, 0.9f, 1f, 0.16f), 15).AddComponent<ElectricArcFlicker2D>();
    }

    private static void AddHazard(Transform parent, string name, Vector2 position, Vector2 size)
    {
        AddEnvironmentV8SpriteChild(parent, name + "_DarkOilSurface_V8", "envv8_oil_pool_readable.png", position + new Vector2(0f, -0.18f), new Vector2(size.x, Mathf.Max(0.42f, size.y * 1.05f)), new Color(0.62f, 0.78f, 0.58f, 0.72f), 6);
        GameObject trigger = NewChild(parent, name);
        trigger.transform.position = new Vector3(position.x, position.y + 0.3f, 0f);
        BoxCollider2D collider = trigger.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(size.x, 1.4f);
        trigger.AddComponent<HazardRespawn2D>();
    }

    private static void CreateFallDeathZone(Transform parent, string name, Vector2 position, Vector2 size)
    {
        GameObject trigger = NewChild(parent, name);
        trigger.transform.position = new Vector3(position.x, position.y, 0f);
        BoxCollider2D collider = trigger.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = size;
        trigger.AddComponent<HazardRespawn2D>();
    }

    private static void AddPlatform(Transform parent, string name, Vector2 position, Vector2 size, PlatformVisualType visualType = PlatformVisualType.MainFloor, bool drawLeftCap = true, bool drawRightCap = true)
    {
        GameObject colliderObject = NewChild(parent, name);
        colliderObject.transform.position = new Vector3(position.x, position.y, 0f);
        BoxCollider2D box = colliderObject.AddComponent<BoxCollider2D>();
        box.size = size;

        GameObject visual = NewChild(parent, name + "_Visual_V8Road");
        visual.transform.position = new Vector3(position.x, position.y, 0f);

        if (visualType == PlatformVisualType.LowObstacle)
        {
            AddEnvironmentV10SpriteChild(visual.transform, name + "_V10_LowObstacleShell", "envv10_low_obstacle_shell.png", Vector2.zero, new Vector2(size.x + 0.1f, size.y + 0.12f), new Color(0.96f, 0.94f, 0.86f, 0.94f), 13);
            AddPlatformDetailV10(visual.transform, name, size, visualType);
            return;
        }

        if (visualType == PlatformVisualType.BlockStep)
        {
            float visualWidth = Mathf.Max(0.8f, size.x - 0.16f);
            float topPlateHeight = Mathf.Min(0.32f, Mathf.Max(0.22f, size.y * 0.42f));
            float topPlateY = size.y * 0.5f - topPlateHeight * 0.5f;
            AddEnvironmentV8SpriteChild(visual.transform, name + "_V8_BlockStepDeck", "envv8_road_dark_steel_module.png", new Vector2(0f, topPlateY), new Vector2(visualWidth, topPlateHeight), Color.white, 10);
            AddEnvironmentV8SpriteChild(visual.transform, name + "_V8_AmberWalkableLip_00", "envv8_road_amber_top_lip.png", new Vector2(0f, size.y * 0.5f + 0.026f), new Vector2(visualWidth * 0.94f, 0.11f), new Color(1f, 0.94f, 0.72f, 0.9f), 14);
            AddEnvironmentV10SpriteChild(visual.transform, name + "_V10_BlockStepShadow", "envv10_shallow_front_shadow.png", new Vector2(0f, topPlateY - topPlateHeight * 0.55f), new Vector2(visualWidth * 0.72f, 0.085f), new Color(0.76f, 0.72f, 0.56f, 0.3f), 8);
            AddThinPlatformDetailV10(visual.transform, name, new Vector2(visualWidth, topPlateHeight), 0f);
            return;
        }

        if (visualType == PlatformVisualType.ThinJumpDeck)
        {
            float visualWidth = Mathf.Max(0.8f, size.x - 0.74f);
            float topPlateHeight = 0.26f;
            float topPlateY = size.y * 0.5f - topPlateHeight * 0.5f;
            AddEnvironmentV8SpriteChild(visual.transform, name + "_V8_ThinJumpDeck", "envv8_road_dark_steel_module.png", new Vector2(0f, topPlateY), new Vector2(visualWidth, topPlateHeight), Color.white, 10);
            AddEnvironmentV8SpriteChild(visual.transform, name + "_V8_AmberWalkableLip_00", "envv8_road_amber_top_lip.png", new Vector2(0f, size.y * 0.5f + 0.026f), new Vector2(visualWidth * 0.94f, 0.105f), new Color(1f, 0.94f, 0.72f, 0.88f), 14);
            AddEnvironmentV10SpriteChild(visual.transform, name + "_V10_ThinSupportShadow", "envv10_shallow_front_shadow.png", new Vector2(0f, topPlateY - 0.15f), new Vector2(visualWidth * 0.68f, 0.075f), new Color(0.76f, 0.72f, 0.56f, 0.26f), 8);
            AddThinPlatformDetailV10(visual.transform, name, new Vector2(visualWidth, topPlateHeight), 0f);
            return;
        }

        if (visualType == PlatformVisualType.ThinFloatingDeck)
        {
            float visualWidth = Mathf.Max(0.8f, size.x - 0.58f);
            float topPlateHeight = 0.24f;
            float topPlateY = size.y * 0.5f - topPlateHeight * 0.5f;
            AddEnvironmentV8SpriteChild(visual.transform, name + "_V8_ThinFloatingDeck", "envv8_road_dark_steel_module.png", new Vector2(0f, topPlateY), new Vector2(visualWidth, topPlateHeight), Color.white, 10);
            AddEnvironmentV8SpriteChild(visual.transform, name + "_V8_AmberWalkableLip_00", "envv8_road_amber_top_lip.png", new Vector2(0f, size.y * 0.5f + 0.026f), new Vector2(visualWidth * 0.94f, 0.1f), new Color(1f, 0.94f, 0.72f, 0.86f), 14);
            AddEnvironmentV10SpriteChild(visual.transform, name + "_V10_ThinSupportShadow", "envv10_shallow_front_shadow.png", new Vector2(0f, topPlateY - 0.13f), new Vector2(visualWidth * 0.64f, 0.072f), new Color(0.76f, 0.72f, 0.56f, 0.24f), 8);
            AddThinPlatformDetailV10(visual.transform, name, new Vector2(visualWidth, topPlateHeight), 0f);
            return;
        }

        float leftInset = drawLeftCap ? 0f : 0.06f;
        float rightInset = drawRightCap ? 0f : 0.06f;
        float visibleWidth = Mathf.Max(0.2f, size.x - leftInset - rightInset);
        float visibleCenterX = (leftInset - rightInset) * 0.5f;

        int panelCount = Mathf.Max(1, Mathf.CeilToInt(visibleWidth / 7.8f));
        float panelWidth = visibleWidth / panelCount;
        for (int i = 0; i < panelCount; i++)
        {
            float x = visibleCenterX - visibleWidth * 0.5f + panelWidth * (i + 0.5f);
            AddEnvironmentV8SpriteChild(visual.transform, name + $"_V8_RoadModule_{i:00}", "envv8_road_dark_steel_module.png", new Vector2(x, 0f), new Vector2(panelWidth, size.y), Color.white, 10);
        }

        int edgeCount = Mathf.Max(1, Mathf.CeilToInt(visibleWidth / 7.8f));
        float edgeWidth = visibleWidth / edgeCount;
        for (int i = 0; i < edgeCount; i++)
        {
            float x = visibleCenterX - visibleWidth * 0.5f + edgeWidth * (i + 0.5f);
            AddEnvironmentV8SpriteChild(visual.transform, name + $"_V8_AmberWalkableLip_{i:00}", "envv8_road_amber_top_lip.png", new Vector2(x, size.y * 0.5f + 0.026f), new Vector2(edgeWidth * 0.98f, 0.125f), new Color(1f, 0.94f, 0.72f, 0.9f), 14);
        }

        AddEnvironmentV8SpriteChild(visual.transform, name + "_V8_FrontShadow", "envv8_road_front_shadow.png", new Vector2(visibleCenterX, -size.y * 0.16f), new Vector2(visibleWidth * 0.99f, Mathf.Max(0.52f, size.y * 0.82f)), new Color(0.96f, 0.93f, 0.84f, 0.92f), 9);
        AddEnvironmentV8SpriteChild(visual.transform, name + "_V8_UnderTruss_Subtle", "envv8_road_under_truss_subtle.png", new Vector2(visibleCenterX, -size.y * 0.54f), new Vector2(visibleWidth * 0.96f, Mathf.Max(0.42f, size.y * 0.44f)), new Color(0.78f, 0.68f, 0.52f, 0.78f), 7);
        if (drawLeftCap)
        {
            AddEnvironmentV8SpriteChild(visual.transform, name + "_V8_EndCap_Left", "envv8_platform_end_cap_left.png", new Vector2(-size.x * 0.5f, 0f), new Vector2(0.42f, size.y + 0.08f), Color.white, 15);
        }

        if (drawRightCap)
        {
            AddEnvironmentV8SpriteChild(visual.transform, name + "_V8_EndCap_Right", "envv8_platform_end_cap_right.png", new Vector2(size.x * 0.5f, 0f), new Vector2(0.42f, size.y + 0.08f), Color.white, 15);
        }
        AddEnvironmentV10SpriteChild(visual.transform, name + "_V10_ShallowFrontShadow", "envv10_shallow_front_shadow.png", new Vector2(visibleCenterX, -size.y * 0.58f), new Vector2(visibleWidth * 0.9f, 0.15f), new Color(0.76f, 0.72f, 0.56f, 0.36f), 11);
        AddPlatformDetailV10(visual.transform, name, new Vector2(visibleWidth, size.y), visualType, visibleCenterX);
    }

    private static void AddPlatformDetailV10(Transform parent, string platformName, Vector2 size, PlatformVisualType visualType, float centerX = 0f)
    {
        float topY = size.y * 0.5f + 0.05f;
        AddEnvironmentV10SpriteChild(parent, platformName + "_V10_AmberEdgeScuff", "envv10_amber_edge_scuff.png", new Vector2(centerX, topY - 0.01f), new Vector2(Mathf.Max(0.9f, size.x * 0.84f), 0.12f), new Color(1f, 0.88f, 0.58f, visualType == PlatformVisualType.MainFloor ? 0.34f : 0.44f), 16);

        if (size.x > 2.2f)
        {
            AddEnvironmentV10SpriteChild(parent, platformName + "_V10_MetalSeamOverlay", "envv10_metal_seam_strip.png", new Vector2(centerX, size.y * 0.12f), new Vector2(size.x * 0.78f, Mathf.Max(0.2f, size.y * 0.28f)), new Color(1f, 1f, 1f, 0.18f), 15);
            AddEnvironmentV10SpriteChild(parent, platformName + "_V10_RivetStrip", "envv10_rivet_strip.png", new Vector2(centerX - size.x * 0.32f, -size.y * 0.04f), new Vector2(0.15f, Mathf.Max(0.36f, size.y * 0.62f)), new Color(0.9f, 0.82f, 0.66f, 0.36f), 16);
            AddEnvironmentV10SpriteChild(parent, platformName + "_V10_ScratchOverlay", "envv10_scratch_plate_overlay.png", new Vector2(centerX + size.x * 0.24f, size.y * 0.04f), new Vector2(Mathf.Min(1.0f, size.x * 0.22f), Mathf.Max(0.28f, size.y * 0.36f)), new Color(1f, 1f, 1f, 0.18f), 16);
        }

        if (platformName.Contains("Jump") || platformName.Contains("Step") || platformName.Contains("Obstacle"))
        {
            AddEnvironmentV10SpriteChild(parent, platformName + "_V10_CrackOverlay", "envv10_cracked_metal_overlay.png", new Vector2(centerX - size.x * 0.08f, size.y * 0.02f), new Vector2(Mathf.Min(0.9f, size.x * 0.34f), Mathf.Max(0.28f, size.y * 0.42f)), new Color(1f, 0.92f, 0.78f, 0.24f), 16);
        }

        if (platformName.Contains("Trap") || platformName.Contains("Oil") || platformName.Contains("Boss"))
        {
            AddEnvironmentV10SpriteChild(parent, platformName + "_V10_OilStainOverlay", "envv10_oil_stain_overlay.png", new Vector2(centerX + size.x * 0.18f, -size.y * 0.08f), new Vector2(Mathf.Min(1.2f, size.x * 0.22f), Mathf.Max(0.22f, size.y * 0.26f)), new Color(0.72f, 0.82f, 0.56f, 0.2f), 16);
        }
    }

    private static void AddThinPlatformDetailV10(Transform parent, string platformName, Vector2 size, float centerX = 0f)
    {
        float topY = size.y * 0.5f + 0.05f;
        AddEnvironmentV10SpriteChild(parent, platformName + "_V10_AmberEdgeScuff", "envv10_amber_edge_scuff.png", new Vector2(centerX, topY - 0.01f), new Vector2(Mathf.Max(0.72f, size.x * 0.76f), 0.095f), new Color(1f, 0.88f, 0.58f, 0.34f), 16);

        if (size.x > 1.6f)
        {
            AddEnvironmentV10SpriteChild(parent, platformName + "_V10_ScratchOverlay", "envv10_scratch_plate_overlay.png", new Vector2(centerX + size.x * 0.16f, size.y * 0.05f), new Vector2(Mathf.Min(0.72f, size.x * 0.2f), Mathf.Max(0.14f, size.y * 0.32f)), new Color(1f, 1f, 1f, 0.13f), 16);
        }

        if (platformName.Contains("Jump") || platformName.Contains("Step"))
        {
            AddEnvironmentV10SpriteChild(parent, platformName + "_V10_CrackOverlay", "envv10_cracked_metal_overlay.png", new Vector2(centerX - size.x * 0.08f, size.y * 0.02f), new Vector2(Mathf.Min(0.52f, size.x * 0.24f), Mathf.Max(0.14f, size.y * 0.28f)), new Color(1f, 0.92f, 0.78f, 0.16f), 16);
        }
    }

    private static void AddPipeRun(Transform parent, string name, float startX, float endX, float y, float thickness, int order)
    {
        float width = Mathf.Abs(endX - startX);
        float center = (startX + endX) * 0.5f;
        GameObject pipe = NewChild(parent, name + "_DetailedPipe");
        pipe.transform.position = new Vector3(center, y, 0f);

        float pipeHeight = Mathf.Max(0.24f, thickness * 3.6f);
        int pipeSegmentCount = Mathf.Max(1, Mathf.CeilToInt(width / 3.7f));
        float pipeSegmentWidth = width / pipeSegmentCount;
        for (int i = 0; i < pipeSegmentCount; i++)
        {
            float x = -width * 0.5f + pipeSegmentWidth * (i + 0.5f);
            AddEnvironmentSpriteChild(pipe.transform, name + $"_Pipe_Body_{i:00}", "env_pipe_straight.png", new Vector2(x, 0f), new Vector2(pipeSegmentWidth + 0.04f, pipeHeight), new Color(1f, 1f, 1f, 0.92f), order);
        }

        int cableSegmentCount = Mathf.Max(1, Mathf.CeilToInt(width / 3.2f));
        float cableSegmentWidth = width * 0.96f / cableSegmentCount;
        for (int i = 0; i < cableSegmentCount; i++)
        {
            float x = -width * 0.48f + cableSegmentWidth * (i + 0.5f);
            AddEnvironmentSpriteChild(pipe.transform, name + $"_Cable_Bundle_{i:00}", "env_cable_bundle.png", new Vector2(x, -pipeHeight * 0.48f), new Vector2(cableSegmentWidth + 0.04f, pipeHeight * 0.42f), new Color(1f, 1f, 1f, 0.62f), order + 1);
        }

        int flangeCount = Mathf.Max(2, Mathf.RoundToInt(width / 3.6f));
        for (int i = 0; i <= flangeCount; i++)
        {
            float x = Mathf.Lerp(-width * 0.5f, width * 0.5f, i / (float)flangeCount);
            AddEnvironmentSpriteChild(pipe.transform, name + $"_Flange_{i:00}", "env_pipe_flange.png", new Vector2(x, 0f), new Vector2(pipeHeight * 0.34f, pipeHeight * 1.34f), new Color(1f, 1f, 1f, 0.9f), order + 3);
        }

        int clampCount = Mathf.Max(2, Mathf.RoundToInt(width / 2.4f));
        for (int i = 0; i <= clampCount; i++)
        {
            float x = Mathf.Lerp(-width * 0.48f, width * 0.48f, i / (float)clampCount);
            AddEnvironmentSpriteChild(pipe.transform, name + $"_Clamp_{i:00}", "env_pipe_clamp.png", new Vector2(x, 0f), new Vector2(pipeHeight * 0.22f, pipeHeight * 1.12f), new Color(1f, 1f, 1f, 0.78f), order + 4);
        }

        if (width > 5f)
        {
            float valveX = -width * 0.22f;
            GameObject valve = AddEnvironmentSpriteChild(pipe.transform, name + "_Valve_Wheel", "env_valve_wheel.png", new Vector2(valveX, pipeHeight * 0.78f), new Vector2(pipeHeight * 1.15f, pipeHeight * 1.15f), new Color(1f, 1f, 1f, 0.9f), order + 5);
            SimpleRotator2D rotator = valve.AddComponent<SimpleRotator2D>();
            SetFloat(rotator, "degreesPerSecond", name.Contains("Near_") ? 4f : 8f);
        }

        if (width > 6f && (name.Contains("Oil") || name.Contains("Steam") || name.Contains("Exit") || name.Contains("Chip") || name.Contains("Near") || name.Contains("Trap") || name.Contains("BossHall") || name.Contains("Broken") || name.Contains("ChargeStation")))
        {
            AddEnvironmentSpriteChild(pipe.transform, name + "_SteamVent_Detail", "env_steam_vent.png", new Vector2(width * 0.28f, -pipeHeight * 0.78f), new Vector2(pipeHeight * 1.55f, pipeHeight * 0.75f), new Color(1f, 1f, 1f, 0.82f), order + 6);
            for (int i = 0; i < 3; i++)
            {
                GameObject puff = AddEnvironmentSpriteChild(pipe.transform, name + $"_LeakSteam_{i:00}", "env_dust_puff.png", new Vector2(width * 0.28f - 0.26f + i * 0.24f, -pipeHeight * 0.22f + i * 0.08f), new Vector2(pipeHeight * 1.05f, pipeHeight * 0.72f), new Color(0.86f, 0.82f, 0.7f, 0.22f), order + 7);
                SteamPuff2D steam = puff.AddComponent<SteamPuff2D>();
                SetFloat(steam, "riseHeight", 0.22f + i * 0.08f);
                SetFloat(steam, "pulseSpeed", 0.72f + i * 0.18f);
                SetFloat(steam, "minAlpha", 0.02f);
                SetFloat(steam, "maxAlpha", 0.18f);
            }
        }
    }

    private static void AddHangingCable(Transform parent, string name, Vector2 top, float length, float angle)
    {
        GameObject cable = AddEnvironmentV5SpriteChild(parent, name, "envv5_hanging_cable_bundle.png", top + Vector2.down * (length * 0.5f), new Vector2(0.34f, length), new Color(1f, 1f, 1f, 0.88f), 16);
        cable.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        AddEnvironmentV3SpriteChild(parent, name + "_Tip_Lamp", "envv3_status_lamp_round.png", top + Vector2.down * length + new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad) * 0.12f, 0f), new Vector2(0.22f, 0.22f), new Color(1f, 0.64f, 0.16f, 0.62f), 17).AddComponent<SpriteFlicker2D>();
    }

    private static void AddHangingChain(Transform parent, string name, Vector2 top, float length)
    {
        AddEnvironmentV5SpriteChild(parent, name, "envv5_hanging_chain_detailed.png", top + Vector2.down * (length * 0.5f), new Vector2(0.38f, length), new Color(1f, 1f, 1f, 0.92f), 15);
    }

    private static void AddRailing(Transform parent, string name, float startX, float endX, float y)
    {
        float width = Mathf.Abs(endX - startX);
        float center = (startX + endX) * 0.5f;
        AddEnvironmentV5SpriteChild(parent, name, "envv5_railing_segment.png", new Vector2(center, y + 0.18f), new Vector2(width, 0.82f), new Color(1f, 1f, 1f, 0.9f), 6);
    }

    private static void AddCrateStack(Transform parent, string name, Vector2 basePosition, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 offset = new Vector2((i % 2) * 0.7f, (i / 2) * 0.62f);
            GameObject crate = AddEnvironmentV5SpriteChild(parent, name + $"_Crate_{i:00}", "envv5_crate_detailed.png", basePosition + offset, new Vector2(0.86f, 0.68f), Color.white, 8);
            crate.transform.rotation = Quaternion.Euler(0f, 0f, i % 2 == 0 ? 0f : 2.5f);
        }
    }

    private static void AddScrapPile(Transform parent, string name, Vector2 basePosition, int pieces)
    {
        float width = Mathf.Clamp(pieces * 0.32f, 1.25f, 2.8f);
        AddEnvironmentV5SpriteChild(parent, name, "envv5_scrap_pile.png", basePosition, new Vector2(width, 0.72f), Color.white, 7);
        AddEnvironmentV5SpriteChild(parent, name + "_SmallOffcut", "envv5_scrap_pile.png", basePosition + new Vector2(width * 0.28f, 0.06f), new Vector2(width * 0.48f, 0.42f), new Color(1f, 1f, 1f, 0.72f), 8);
    }

    private static void AddForegroundScrap(Transform parent, string name, Vector2 position, float width)
    {
        AddEnvironmentV5SpriteChild(parent, name, "envv5_scrap_pile.png", position + new Vector2(-width * 0.22f, -0.18f), new Vector2(width * 0.42f, 1.35f), new Color(0.22f, 0.18f, 0.14f, 0.68f), 40);
        AddEnvironmentV5SpriteChild(parent, name + "_Layer_B", "envv5_scrap_pile.png", position + new Vector2(width * 0.24f, -0.2f), new Vector2(width * 0.36f, 1.05f), new Color(0.18f, 0.15f, 0.12f, 0.55f), 41);
    }

    private static void AddWarningSign(Transform parent, string name, Vector2 position, string mark)
    {
        AddEnvironmentV5SpriteChild(parent, name + "_Plate", "envv5_warning_sign.png", position, new Vector2(0.86f, 0.52f), Color.white, 9);
        AddWorldText(parent, name + "_Mark", mark, position + new Vector2(0f, -0.01f), 0.16f);
    }

    private static void AddDetailLight(Transform parent, string name, Vector2 position, float size, int order)
    {
        AddEnvironmentV3SpriteChild(parent, name + "_SoftHalo", "envv3_soft_glow_round_amber.png", position, new Vector2(size * 2.5f, size * 2.5f), new Color(1f, 0.54f, 0.08f, 0.2f), order - 1);
        GameObject light = AddEnvironmentV3SpriteChild(parent, name, "envv3_status_lamp_round.png", position, new Vector2(size * 1.25f, size * 1.25f), new Color(1f, 0.68f, 0.22f, 0.72f), order);
        light.AddComponent<SpriteFlicker2D>();
    }

    private static void AddSteamVent(Transform parent, string name, Vector2 position)
    {
        GameObject root = NewChild(parent, name + "_SteamVent_Detail");
        root.transform.position = new Vector3(position.x, position.y - 0.12f, 0f);
        AddEnvironmentSpriteChild(root.transform, name + "_Grate", "env_steam_vent.png", Vector2.zero, new Vector2(1.25f, 0.48f), Color.white, 10);
        for (int i = 0; i < 3; i++)
        {
            GameObject puff = AddEnvironmentSpriteChild(root.transform, name + $"_Puff_{i:00}", "env_dust_puff.png", new Vector2(-0.25f + i * 0.25f, 0.28f + i * 0.08f), new Vector2(0.55f, 0.42f), new Color(0.8f, 0.76f, 0.66f, 0.24f), 11);
            SteamPuff2D steam = puff.AddComponent<SteamPuff2D>();
            SetFloat(steam, "riseHeight", 0.32f + i * 0.07f);
            SetFloat(steam, "pulseSpeed", 0.95f + i * 0.2f);
        }
    }

    private static void AddGearCluster(Transform parent, string name, Vector2 center)
    {
        GameObject big = AddEnvironmentV5SpriteChild(parent, name + "_Big_Gear", "envv5_gear_cluster.png", center, new Vector2(1.42f, 1.15f), new Color(1f, 1f, 1f, 0.9f), 11);
        SimpleRotator2D bigRotator = big.AddComponent<SimpleRotator2D>();
        SetFloat(bigRotator, "degreesPerSecond", 5.5f);

        GameObject small = AddEnvironmentV5SpriteChild(parent, name + "_Small_Gear", "envv5_gear_cluster.png", center + new Vector2(0.72f, 0.38f), new Vector2(0.88f, 0.72f), new Color(0.86f, 0.78f, 0.68f, 0.72f), 12);
        SimpleRotator2D smallRotator = small.AddComponent<SimpleRotator2D>();
        SetFloat(smallRotator, "degreesPerSecond", -8.5f);

        GameObject tiny = AddEnvironmentV5SpriteChild(parent, name + "_Tiny_Gear", "envv5_gear_cluster.png", center + new Vector2(-0.68f, 0.28f), new Vector2(0.62f, 0.5f), new Color(0.86f, 0.78f, 0.68f, 0.58f), 10);
        SimpleRotator2D tinyRotator = tiny.AddComponent<SimpleRotator2D>();
        SetFloat(tinyRotator, "degreesPerSecond", 11f);
    }

    private static void CreateUi(Transform parent)
    {
        GameObject hud = NewChild(parent, "OnGUI_TutorialHUD");
        hud.AddComponent<LevelObjectiveUI>();
    }

    private static GameObject AddRect(Transform parent, string name, Vector2 position, Vector2 size, Color color, int order, bool collider)
    {
        GameObject rect = NewChild(parent, name);
        rect.transform.position = new Vector3(position.x, position.y, 0f);
        rect.transform.localScale = new Vector3(size.x, size.y, 1f);
        SpriteRenderer renderer = rect.AddComponent<SpriteRenderer>();
        renderer.sprite = whiteSprite;
        renderer.color = color;
        renderer.sortingOrder = order;

        if (collider)
        {
            BoxCollider2D box = rect.AddComponent<BoxCollider2D>();
            box.size = Vector2.one;
        }

        return rect;
    }

    private static GameObject AddRectChild(Transform parent, string name, Vector2 localPosition, Vector2 size, Color color, int order)
    {
        GameObject rect = NewChild(parent, name);
        rect.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);
        rect.transform.localScale = new Vector3(size.x, size.y, 1f);
        SpriteRenderer renderer = rect.AddComponent<SpriteRenderer>();
        renderer.sprite = whiteSprite;
        renderer.color = color;
        renderer.sortingOrder = order;
        return rect;
    }

    private static GameObject AddEnvironmentSpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEnvironmentSpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing environment sprite: {GetEnvironmentSpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddProvidedDecorSpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetProvidedEnvironmentSpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing provided environment sprite: {GetProvidedEnvironmentSpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddProvidedEnvironmentV2SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetProvidedEnvironmentV2SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing provided V2 environment sprite: {GetProvidedEnvironmentV2SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddProvidedEnvironmentV3SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetProvidedEnvironmentV3SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing provided V3 environment sprite: {GetProvidedEnvironmentV3SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddProvidedEnvironmentV4SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetProvidedEnvironmentV4SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing provided V4 environment sprite: {GetProvidedEnvironmentV4SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEnvironmentV2SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEnvironmentV2SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V2 environment sprite: {GetEnvironmentV2SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEnvironmentV3SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEnvironmentV3SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V3 environment sprite: {GetEnvironmentV3SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEnvironmentV4SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEnvironmentV4SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V4 environment sprite: {GetEnvironmentV4SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEnvironmentV5SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEnvironmentV5SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V5 environment sprite: {GetEnvironmentV5SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEnvironmentV7SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEnvironmentV7SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V7 environment sprite: {GetEnvironmentV7SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEnvironmentV8SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEnvironmentV8SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V8 environment sprite: {GetEnvironmentV8SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEnvironmentV9SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEnvironmentV9SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V9 environment sprite: {GetEnvironmentV9SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEnvironmentV10SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEnvironmentV10SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V10 environment sprite: {GetEnvironmentV10SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEnvironmentV11SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEnvironmentV11SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V11 environment sprite: {GetEnvironmentV11SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEnvironmentV12SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEnvironmentV12SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V12 environment sprite: {GetEnvironmentV12SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEnvironmentV19SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEnvironmentV19SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V19 environment sprite: {GetEnvironmentV19SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEnvironmentV20SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEnvironmentV20SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V20 environment sprite: {GetEnvironmentV20SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEffectsV1SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEffectsV1SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V1 effect sprite: {GetEffectsV1SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEffectsV2SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEffectsV2SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V2 effect sprite: {GetEffectsV2SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEffectsV3SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEffectsV3SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V3 effect sprite: {GetEffectsV3SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEffectsV4SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEffectsV4SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V4 effect sprite: {GetEffectsV4SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEffectsV5SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEffectsV5SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V5 effect sprite: {GetEffectsV5SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEffectsV7SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEffectsV7SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V7 effect sprite: {GetEffectsV7SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEnemyV1SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEnemyV1SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing enemy sprite: {GetEnemyV1SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEnemyV2SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEnemyV2SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V2 enemy sprite: {GetEnemyV2SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEnemyV3SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEnemyV3SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V3 enemy sprite: {GetEnemyV3SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddEnemyV4SpriteChild(Transform parent, string name, string fileName, Vector2 localPosition, Vector2 targetSize, Color color, int order)
    {
        GameObject spriteObject = NewChild(parent, name);
        spriteObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetEnemyV4SpritePath(fileName));
        renderer.color = color;
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing V4 enemy sprite: {GetEnemyV4SpritePath(fileName)}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
            spriteObject.transform.localScale = new Vector3(targetSize.x, targetSize.y, 1f);
            return spriteObject;
        }

        Vector2 spriteSize = renderer.sprite.bounds.size;
        if (spriteSize.x <= 0f || spriteSize.y <= 0f)
        {
            spriteObject.transform.localScale = Vector3.one;
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(targetSize.x / spriteSize.x, targetSize.y / spriteSize.y, 1f);
        }

        return spriteObject;
    }

    private static GameObject AddRobotSpritePart(Transform parent, string name, string fileName, Vector2 localPosition, float targetSize, bool targetSizeIsWidth, int order)
    {
        GameObject part = NewChild(parent, name);
        part.transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);

        SpriteRenderer renderer = part.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>($"{GeneratedRobotV3Path}/{fileName}");
        renderer.sortingOrder = order;

        if (renderer.sprite == null)
        {
            Debug.LogWarning($"Missing generated robot part sprite: {GeneratedRobotV3Path}/{fileName}");
            renderer.sprite = whiteSprite;
            renderer.color = Color.magenta;
        }

        if (targetSizeIsWidth)
        {
            ScaleSpriteToWidth(renderer, targetSize);
        }
        else
        {
            ScaleSpriteToHeight(renderer, targetSize);
        }

        return part;
    }

    private static void AddRobotRimPart(GameObject sourcePart, string rimName, float scale, float alpha)
    {
        SpriteRenderer sourceRenderer = sourcePart != null ? sourcePart.GetComponent<SpriteRenderer>() : null;
        if (sourceRenderer == null || sourceRenderer.sprite == null)
        {
            return;
        }

        GameObject rim = NewChild(sourcePart.transform, rimName);
        rim.transform.localPosition = Vector3.zero;
        rim.transform.localRotation = Quaternion.identity;
        rim.transform.localScale = new Vector3(scale, scale, 1f);
        SpriteRenderer renderer = rim.AddComponent<SpriteRenderer>();
        renderer.sprite = sourceRenderer.sprite;
        renderer.color = new Color(1f, 0.54f, 0.12f, alpha);
        renderer.sortingOrder = sourceRenderer.sortingOrder - 1;
    }

    private static void TintSpriteRenderer(GameObject target, Color color)
    {
        SpriteRenderer renderer = target != null ? target.GetComponent<SpriteRenderer>() : null;
        if (renderer != null)
        {
            renderer.color = color;
        }
    }

    private static void AddWorldText(Transform parent, string name, string text, Vector2 position, float characterSize)
    {
        GameObject label = NewChild(parent, name);
        label.transform.position = new Vector3(position.x, position.y, 0f);
        TextMesh mesh = label.AddComponent<TextMesh>();
        mesh.text = text;
        mesh.anchor = TextAnchor.MiddleCenter;
        mesh.alignment = TextAlignment.Center;
        mesh.characterSize = characterSize;
        mesh.fontSize = 48;
        mesh.color = new Color(1f, 0.62f, 0.12f);
        MeshRenderer renderer = label.GetComponent<MeshRenderer>();
        renderer.sortingOrder = 9;
    }

    private static GameObject NewChild(Transform parent, string name)
    {
        GameObject child = new GameObject(name);
        child.transform.SetParent(parent);
        child.transform.localPosition = Vector3.zero;
        return child;
    }

    private static GameObject NewChild(GameObject parent, string name)
    {
        return NewChild(parent.transform, name);
    }

    private static void EnsureFolder(string path)
    {
        if (AssetDatabase.IsValidFolder(path))
        {
            return;
        }

        string parent = Path.GetDirectoryName(path).Replace("\\", "/");
        string name = Path.GetFileName(path);
        if (!AssetDatabase.IsValidFolder(parent))
        {
            EnsureFolder(parent);
        }

        AssetDatabase.CreateFolder(parent, name);
    }

    private static Sprite GetOrCreateSolidSprite(string name, Color color)
    {
        string path = $"{GeneratedArtPath}/{name}.png";
        if (!File.Exists(path))
        {
            Texture2D texture = new Texture2D(32, 32, TextureFormat.RGBA32, false);
            Color[] pixels = new Color[32 * 32];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color;
            }

            texture.SetPixels(pixels);
            texture.Apply();
            File.WriteAllBytes(path, texture.EncodeToPNG());
            Object.DestroyImmediate(texture);
        }

        ConfigureImportedSprite(path, 32);
        return AssetDatabase.LoadAssetAtPath<Sprite>(path);
    }

    private static void ConfigureImportedSprite(string path, int pixelsPerUnit, int maxTextureSize = 4096, FilterMode filterMode = FilterMode.Bilinear)
    {
        TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
        if (importer == null)
        {
            return;
        }

        importer.textureType = TextureImporterType.Sprite;
        importer.spriteImportMode = SpriteImportMode.Single;
        importer.spritePixelsPerUnit = pixelsPerUnit;
        importer.alphaIsTransparency = true;
        importer.mipmapEnabled = false;
        importer.filterMode = filterMode;
        importer.wrapMode = TextureWrapMode.Clamp;
        importer.maxTextureSize = maxTextureSize;
        importer.textureCompression = TextureImporterCompression.Uncompressed;
        importer.crunchedCompression = false;
        importer.compressionQuality = 100;
        TextureImporterPlatformSettings defaultSettings = importer.GetDefaultPlatformTextureSettings();
        defaultSettings.maxTextureSize = maxTextureSize;
        defaultSettings.textureCompression = TextureImporterCompression.Uncompressed;
        defaultSettings.compressionQuality = 100;
        defaultSettings.crunchedCompression = false;
        importer.SetPlatformTextureSettings(defaultSettings);

        TextureImporterPlatformSettings standaloneSettings = importer.GetPlatformTextureSettings("Standalone");
        standaloneSettings.overridden = true;
        standaloneSettings.maxTextureSize = maxTextureSize;
        standaloneSettings.textureCompression = TextureImporterCompression.Uncompressed;
        standaloneSettings.compressionQuality = 100;
        standaloneSettings.crunchedCompression = false;
        importer.SetPlatformTextureSettings(standaloneSettings);
        importer.SaveAndReimport();
    }

    private static void ScaleSpriteToWidth(SpriteRenderer renderer, float targetWidth)
    {
        if (renderer == null || renderer.sprite == null)
        {
            return;
        }

        float width = renderer.sprite.bounds.size.x;
        if (width <= 0f)
        {
            return;
        }

        float scale = targetWidth / width;
        renderer.transform.localScale = new Vector3(scale, scale, 1f);
    }

    private static void ScaleSpriteToHeight(SpriteRenderer renderer, float targetHeight)
    {
        if (renderer == null || renderer.sprite == null)
        {
            return;
        }

        float height = renderer.sprite.bounds.size.y;
        if (height <= 0f)
        {
            return;
        }

        float scale = targetHeight / height;
        renderer.transform.localScale = new Vector3(scale, scale, 1f);
    }

    private static string[] GetRobotPartPaths()
    {
        string[] paths = new string[RobotPartFiles.Length];
        for (int i = 0; i < RobotPartFiles.Length; i++)
        {
            paths[i] = $"{GeneratedRobotV3Path}/{RobotPartFiles[i]}";
        }

        return paths;
    }

    private static string[] GetBackgroundV6Paths()
    {
        string[] paths = new string[BackgroundV6Files.Length];
        for (int i = 0; i < BackgroundV6Files.Length; i++)
        {
            paths[i] = $"{GeneratedBackgroundV6Path}/{BackgroundV6Files[i]}";
        }

        return paths;
    }

    private static string[] GetBackgroundV7Paths()
    {
        string[] paths = new string[BackgroundV7Files.Length];
        for (int i = 0; i < BackgroundV7Files.Length; i++)
        {
            paths[i] = $"{GeneratedBackgroundV7Path}/{BackgroundV7Files[i]}";
        }

        return paths;
    }

    private static string[] GetBackgroundV8Paths()
    {
        string[] paths = new string[BackgroundV8Files.Length];
        for (int i = 0; i < BackgroundV8Files.Length; i++)
        {
            paths[i] = $"{GeneratedBackgroundV8Path}/{BackgroundV8Files[i]}";
        }

        return paths;
    }

    private static string[] GetBackgroundV15Paths()
    {
        string[] paths = new string[BackgroundV15Files.Length];
        for (int i = 0; i < BackgroundV15Files.Length; i++)
        {
            paths[i] = $"{GeneratedBackgroundV15Path}/{BackgroundV15Files[i]}";
        }

        return paths;
    }

    private static string[] GetEnvironmentSpritePaths()
    {
        string[] paths = new string[EnvironmentSpriteFiles.Length];
        for (int i = 0; i < EnvironmentSpriteFiles.Length; i++)
        {
            paths[i] = GetEnvironmentSpritePath(EnvironmentSpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetProvidedEnvironmentSpritePaths()
    {
        string[] paths = new string[ProvidedEnvironmentSpriteFiles.Length];
        for (int i = 0; i < ProvidedEnvironmentSpriteFiles.Length; i++)
        {
            paths[i] = GetProvidedEnvironmentSpritePath(ProvidedEnvironmentSpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetProvidedEnvironmentV2SpritePaths()
    {
        string[] paths = new string[ProvidedEnvironmentV2SpriteFiles.Length];
        for (int i = 0; i < ProvidedEnvironmentV2SpriteFiles.Length; i++)
        {
            paths[i] = GetProvidedEnvironmentV2SpritePath(ProvidedEnvironmentV2SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetProvidedEnvironmentV3SpritePaths()
    {
        string[] paths = new string[ProvidedEnvironmentV3SpriteFiles.Length];
        for (int i = 0; i < ProvidedEnvironmentV3SpriteFiles.Length; i++)
        {
            paths[i] = GetProvidedEnvironmentV3SpritePath(ProvidedEnvironmentV3SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetProvidedEnvironmentV4SpritePaths()
    {
        string[] paths = new string[ProvidedEnvironmentV4SpriteFiles.Length];
        for (int i = 0; i < ProvidedEnvironmentV4SpriteFiles.Length; i++)
        {
            paths[i] = GetProvidedEnvironmentV4SpritePath(ProvidedEnvironmentV4SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEnvironmentV2SpritePaths()
    {
        string[] paths = new string[EnvironmentV2SpriteFiles.Length];
        for (int i = 0; i < EnvironmentV2SpriteFiles.Length; i++)
        {
            paths[i] = GetEnvironmentV2SpritePath(EnvironmentV2SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEnvironmentV3SpritePaths()
    {
        string[] paths = new string[EnvironmentV3SpriteFiles.Length];
        for (int i = 0; i < EnvironmentV3SpriteFiles.Length; i++)
        {
            paths[i] = GetEnvironmentV3SpritePath(EnvironmentV3SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEnvironmentV4SpritePaths()
    {
        string[] paths = new string[EnvironmentV4SpriteFiles.Length];
        for (int i = 0; i < EnvironmentV4SpriteFiles.Length; i++)
        {
            paths[i] = GetEnvironmentV4SpritePath(EnvironmentV4SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEnvironmentV5SpritePaths()
    {
        string[] paths = new string[EnvironmentV5SpriteFiles.Length];
        for (int i = 0; i < EnvironmentV5SpriteFiles.Length; i++)
        {
            paths[i] = GetEnvironmentV5SpritePath(EnvironmentV5SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEnvironmentV7SpritePaths()
    {
        string[] paths = new string[EnvironmentV7SpriteFiles.Length];
        for (int i = 0; i < EnvironmentV7SpriteFiles.Length; i++)
        {
            paths[i] = GetEnvironmentV7SpritePath(EnvironmentV7SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEnvironmentV8SpritePaths()
    {
        string[] paths = new string[EnvironmentV8SpriteFiles.Length];
        for (int i = 0; i < EnvironmentV8SpriteFiles.Length; i++)
        {
            paths[i] = GetEnvironmentV8SpritePath(EnvironmentV8SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEnvironmentV9SpritePaths()
    {
        string[] paths = new string[EnvironmentV9SpriteFiles.Length];
        for (int i = 0; i < EnvironmentV9SpriteFiles.Length; i++)
        {
            paths[i] = GetEnvironmentV9SpritePath(EnvironmentV9SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEnvironmentV10SpritePaths()
    {
        string[] paths = new string[EnvironmentV10SpriteFiles.Length];
        for (int i = 0; i < EnvironmentV10SpriteFiles.Length; i++)
        {
            paths[i] = GetEnvironmentV10SpritePath(EnvironmentV10SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEnvironmentV11SpritePaths()
    {
        string[] paths = new string[EnvironmentV11SpriteFiles.Length];
        for (int i = 0; i < EnvironmentV11SpriteFiles.Length; i++)
        {
            paths[i] = GetEnvironmentV11SpritePath(EnvironmentV11SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEnvironmentV12SpritePaths()
    {
        string[] paths = new string[EnvironmentV12SpriteFiles.Length];
        for (int i = 0; i < EnvironmentV12SpriteFiles.Length; i++)
        {
            paths[i] = GetEnvironmentV12SpritePath(EnvironmentV12SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEnvironmentV19SpritePaths()
    {
        string[] paths = new string[EnvironmentV19SpriteFiles.Length];
        for (int i = 0; i < EnvironmentV19SpriteFiles.Length; i++)
        {
            paths[i] = GetEnvironmentV19SpritePath(EnvironmentV19SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEnvironmentV20SpritePaths()
    {
        string[] paths = new string[EnvironmentV20SpriteFiles.Length];
        for (int i = 0; i < EnvironmentV20SpriteFiles.Length; i++)
        {
            paths[i] = GetEnvironmentV20SpritePath(EnvironmentV20SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEffectsV1SpritePaths()
    {
        string[] paths = new string[EffectsV1SpriteFiles.Length];
        for (int i = 0; i < EffectsV1SpriteFiles.Length; i++)
        {
            paths[i] = GetEffectsV1SpritePath(EffectsV1SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEffectsV2SpritePaths()
    {
        string[] paths = new string[EffectsV2SpriteFiles.Length];
        for (int i = 0; i < EffectsV2SpriteFiles.Length; i++)
        {
            paths[i] = GetEffectsV2SpritePath(EffectsV2SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEffectsV3SpritePaths()
    {
        string[] paths = new string[EffectsV3SpriteFiles.Length];
        for (int i = 0; i < EffectsV3SpriteFiles.Length; i++)
        {
            paths[i] = GetEffectsV3SpritePath(EffectsV3SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEffectsV4SpritePaths()
    {
        string[] paths = new string[EffectsV4SpriteFiles.Length];
        for (int i = 0; i < EffectsV4SpriteFiles.Length; i++)
        {
            paths[i] = GetEffectsV4SpritePath(EffectsV4SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEffectsV5SpritePaths()
    {
        string[] paths = new string[EffectsV5SpriteFiles.Length];
        for (int i = 0; i < EffectsV5SpriteFiles.Length; i++)
        {
            paths[i] = GetEffectsV5SpritePath(EffectsV5SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEffectsV7SpritePaths()
    {
        string[] paths = new string[EffectsV7SpriteFiles.Length];
        for (int i = 0; i < EffectsV7SpriteFiles.Length; i++)
        {
            paths[i] = GetEffectsV7SpritePath(EffectsV7SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEnemyV1SpritePaths()
    {
        string[] paths = new string[EnemyV1SpriteFiles.Length];
        for (int i = 0; i < EnemyV1SpriteFiles.Length; i++)
        {
            paths[i] = GetEnemyV1SpritePath(EnemyV1SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEnemyV2SpritePaths()
    {
        string[] paths = new string[EnemyV2SpriteFiles.Length];
        for (int i = 0; i < EnemyV2SpriteFiles.Length; i++)
        {
            paths[i] = GetEnemyV2SpritePath(EnemyV2SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEnemyV3SpritePaths()
    {
        string[] paths = new string[EnemyV3SpriteFiles.Length];
        for (int i = 0; i < EnemyV3SpriteFiles.Length; i++)
        {
            paths[i] = GetEnemyV3SpritePath(EnemyV3SpriteFiles[i]);
        }

        return paths;
    }

    private static string[] GetEnemyV4SpritePaths()
    {
        string[] paths = new string[EnemyV4SpriteFiles.Length];
        for (int i = 0; i < EnemyV4SpriteFiles.Length; i++)
        {
            paths[i] = GetEnemyV4SpritePath(EnemyV4SpriteFiles[i]);
        }

        return paths;
    }

    private static string GetEnvironmentSpritePath(string fileName)
    {
        return $"{GeneratedEnvironmentV1Path}/{fileName}";
    }

    private static string GetEnvironmentV2SpritePath(string fileName)
    {
        return $"{GeneratedEnvironmentV2Path}/{fileName}";
    }

    private static string GetEnvironmentV3SpritePath(string fileName)
    {
        return $"{GeneratedEnvironmentV3Path}/{fileName}";
    }

    private static string GetEnvironmentV4SpritePath(string fileName)
    {
        return $"{GeneratedEnvironmentV4Path}/{fileName}";
    }

    private static string GetEnvironmentV5SpritePath(string fileName)
    {
        return $"{GeneratedEnvironmentV5Path}/{fileName}";
    }

    private static string GetEnvironmentV7SpritePath(string fileName)
    {
        return $"{GeneratedEnvironmentV7Path}/{fileName}";
    }

    private static string GetEnvironmentV8SpritePath(string fileName)
    {
        return $"{GeneratedEnvironmentV8Path}/{fileName}";
    }

    private static string GetEnvironmentV9SpritePath(string fileName)
    {
        return $"{GeneratedEnvironmentV9Path}/{fileName}";
    }

    private static string GetEnvironmentV10SpritePath(string fileName)
    {
        return $"{GeneratedEnvironmentV10Path}/{fileName}";
    }

    private static string GetEnvironmentV11SpritePath(string fileName)
    {
        return $"{GeneratedEnvironmentV11Path}/{fileName}";
    }

    private static string GetEnvironmentV12SpritePath(string fileName)
    {
        return $"{GeneratedEnvironmentV12Path}/{fileName}";
    }

    private static string GetEnvironmentV19SpritePath(string fileName)
    {
        return $"{GeneratedEnvironmentV19Path}/{fileName}";
    }

    private static string GetEnvironmentV20SpritePath(string fileName)
    {
        return $"{GeneratedEnvironmentV20Path}/{fileName}";
    }

    private static string GetEffectsV1SpritePath(string fileName)
    {
        return $"{GeneratedEffectsV1Path}/{fileName}";
    }

    private static string GetEffectsV2SpritePath(string fileName)
    {
        return $"{GeneratedEffectsV2Path}/{fileName}";
    }

    private static string GetEffectsV3SpritePath(string fileName)
    {
        return $"{GeneratedEffectsV3Path}/{fileName}";
    }

    private static string GetEffectsV4SpritePath(string fileName)
    {
        return $"{GeneratedEffectsV4Path}/{fileName}";
    }

    private static string GetEffectsV5SpritePath(string fileName)
    {
        return $"{GeneratedEffectsV5Path}/{fileName}";
    }

    private static string GetEffectsV7SpritePath(string fileName)
    {
        return $"{GeneratedEffectsV7Path}/{fileName}";
    }

    private static string GetEnemyV1SpritePath(string fileName)
    {
        return $"{GeneratedEnemiesV1Path}/{fileName}";
    }

    private static string GetEnemyV2SpritePath(string fileName)
    {
        return $"{GeneratedEnemiesV2Path}/{fileName}";
    }

    private static string GetEnemyV3SpritePath(string fileName)
    {
        return $"{GeneratedEnemiesV3Path}/{fileName}";
    }

    private static string GetEnemyV4SpritePath(string fileName)
    {
        return $"{GeneratedEnemiesV4Path}/{fileName}";
    }

    private static string GetProvidedEnvironmentSpritePath(string fileName)
    {
        return $"{ProvidedEnvironmentV1Path}/{fileName}";
    }

    private static string GetProvidedEnvironmentV2SpritePath(string fileName)
    {
        return $"{ProvidedEnvironmentV2Path}/{fileName}";
    }

    private static string GetProvidedEnvironmentV3SpritePath(string fileName)
    {
        return $"{ProvidedEnvironmentV3Path}/{fileName}";
    }

    private static string GetProvidedEnvironmentV4SpritePath(string fileName)
    {
        return $"{ProvidedEnvironmentV4Path}/{fileName}";
    }

    private static void SetObject(Object target, string propertyName, Object value)
    {
        SerializedObject so = new SerializedObject(target);
        SerializedProperty property = so.FindProperty(propertyName);
        if (property != null)
        {
            property.objectReferenceValue = value;
            so.ApplyModifiedPropertiesWithoutUndo();
        }
    }

    private static void SetObjectArray(Object target, string propertyName, Object[] values)
    {
        SerializedObject so = new SerializedObject(target);
        SerializedProperty property = so.FindProperty(propertyName);
        if (property != null && property.isArray)
        {
            property.arraySize = values.Length;
            for (int i = 0; i < values.Length; i++)
            {
                property.GetArrayElementAtIndex(i).objectReferenceValue = values[i];
            }

            so.ApplyModifiedPropertiesWithoutUndo();
        }
    }

    private static void SetString(Object target, string propertyName, string value)
    {
        SerializedObject so = new SerializedObject(target);
        SerializedProperty property = so.FindProperty(propertyName);
        if (property != null)
        {
            property.stringValue = value;
            so.ApplyModifiedPropertiesWithoutUndo();
        }
    }

    private static void SetFloat(Object target, string propertyName, float value)
    {
        SerializedObject so = new SerializedObject(target);
        SerializedProperty property = so.FindProperty(propertyName);
        if (property != null)
        {
            property.floatValue = value;
            so.ApplyModifiedPropertiesWithoutUndo();
        }
    }

    private static void SetInt(Object target, string propertyName, int value)
    {
        SerializedObject so = new SerializedObject(target);
        SerializedProperty property = so.FindProperty(propertyName);
        if (property != null)
        {
            property.intValue = value;
            so.ApplyModifiedPropertiesWithoutUndo();
        }
    }

    private static void SetBool(Object target, string propertyName, bool value)
    {
        SerializedObject so = new SerializedObject(target);
        SerializedProperty property = so.FindProperty(propertyName);
        if (property != null)
        {
            property.boolValue = value;
            so.ApplyModifiedPropertiesWithoutUndo();
        }
    }

    private static void SetEnum(Object target, string propertyName, int value)
    {
        SerializedObject so = new SerializedObject(target);
        SerializedProperty property = so.FindProperty(propertyName);
        if (property != null)
        {
            property.enumValueIndex = value;
            so.ApplyModifiedPropertiesWithoutUndo();
        }
    }

    private static void SetVector2(Object target, string propertyName, Vector2 value)
    {
        SerializedObject so = new SerializedObject(target);
        SerializedProperty property = so.FindProperty(propertyName);
        if (property != null)
        {
            property.vector2Value = value;
            so.ApplyModifiedPropertiesWithoutUndo();
        }
    }

    private static void SetLayerMask(Object target, string propertyName, LayerMask value)
    {
        SerializedObject so = new SerializedObject(target);
        SerializedProperty property = so.FindProperty(propertyName);
        if (property != null)
        {
            property.intValue = value.value;
            so.ApplyModifiedPropertiesWithoutUndo();
        }
    }

    private static void SetHealthArray(Object target, string propertyName, Health[] values)
    {
        SerializedObject so = new SerializedObject(target);
        SerializedProperty property = so.FindProperty(propertyName);
        if (property != null && property.isArray)
        {
            property.arraySize = values.Length;
            for (int i = 0; i < values.Length; i++)
            {
                property.GetArrayElementAtIndex(i).objectReferenceValue = values[i];
            }

            so.ApplyModifiedPropertiesWithoutUndo();
        }
    }

}
