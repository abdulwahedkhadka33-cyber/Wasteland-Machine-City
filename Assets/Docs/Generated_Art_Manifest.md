# Generated Art Manifest

## Current Kept Version

当前保存版本是「锈铁维修站」首关，场景文件为 `Assets/Scenes/Tutorial_01_AwakeningCorridor.unity`。

为减少项目体积，旧主角部件、V1 敌人参考和生成源图已经清理。当前场景默认引用 V15 背景；V8 背景保留为备份，但构建器和验证器会禁止它作为大背景进入当前场景。

## Active Art Sets

- 背景：`Assets/Art/Generated/Backgrounds/V15/`
- 主角 A-07：`Assets/Art/Generated/Characters/PlayerRobot/V3/`
- 主要环境件：`Assets/Art/Generated/Environment/V7/`
- 清爽道路与背景件：`Assets/Art/Generated/Environment/V8/`
- 背景装饰：`Assets/Art/Generated/Environment/V9/`、`Assets/Art/Generated/Environment/V11/`
- 用户提供背景装饰切片：`Assets/Art/Provided/Environment/V2/`、`Assets/Art/Provided/Environment/V3/`、`Assets/Art/Provided/Environment/V4/`
- 道路细节：`Assets/Art/Generated/Environment/V10/`
- 组件精细化覆盖件：`Assets/Art/Generated/Environment/V19/`
- Boss 门精细覆盖件：`Assets/Art/Generated/Environment/V20/`
- 动态与特效：`Assets/Art/Generated/Effects/V1/` 到 `Assets/Art/Generated/Effects/V5/`
- 敌人与 Boss：`Assets/Art/Generated/Enemies/V2/`
- Boss 精细覆盖件：`Assets/Art/Generated/Enemies/V3/`
- Boss 暴走覆盖件：`Assets/Art/Generated/Enemies/V4/`

当前道路精修仍只使用 V8/V10：V8 负责暗钢道路本体、琥珀可踩边线、前侧阴影和下方支架；V10 只做低透明接缝、铆钉、划痕、裂纹、油污和浅阴影补充，不新增道路 PNG。

## System Polish Readability

`BG_SystemPolish_Readability` 是当前首关的系统级美化层，不新增 PNG、不参与碰撞，只复用 Effects/V4、Effects/V5 和已有动态脚本。

| Layer | Scene Usage |
| --- | --- |
| `BG_SystemPolish_RouteFocus` | 八个区域的低透明路线暗带，压低道路附近背景噪声，不作为可踩平台语言 |
| `BG_SystemPolish_Landmarks` | 苏醒台、攻击箱、跳跃区、敌人区、充电站、Boss 门和出口门的柔光地标 |
| `BG_SystemPolish_HazardAndInteract` | 电流、压缩机、电闸、终端和补给箱的低侵入色彩提示 |
| `BG_SystemPolish_AtmosphereControl` | 顶部落尘、陷阱冷雾、Boss 热雾、蒸汽和顶部电弧 |

Boss 遭遇新增的入口锁和过载电弧都不新增资源：`BossArena_EntryLock` 复用 V7 Boss 门、V7 警示灯和 Effects/V2 红色 halo；`Boss_RepairStationGuardian_ArcBurstWarning` / `Boss_RepairStationGuardian_ArcBurstVisual` 复用 Effects/V5 电弧框和 Effects/V2 电流地板图，运行时由 Boss 状态机锁定位置、预警并启闭伤害。当前 Boss 本体额外使用 Enemy/V3 精细覆盖件和 Enemy/V4 暴走覆盖件；核心光束、顶部落雷、终段核心脉冲和死亡破碎仍复用 Effects/V2/V5 与 Environment/V7/V10。

## Boss Visual Refinement V3

`Assets/Art/Generated/Enemies/V3/` 只新增 1 张透明 PNG，不参与碰撞，不改变 Boss 血量、招式伤害、门逻辑或 HUD 接口。

| File | Scene Usage |
| --- | --- |
| `bossv3_guardian_refined_overlay.png` | `Boss_RepairStationGuardian_V3RefinedOverlay`，叠加在 V2 Boss 身体上，提供暗钢外甲、锈蚀液压件、红色核心、线缆、铆钉和破损边 |

二阶段技能的视觉语言：核心光束使用红橙低位扫描线加青白短光束，顶部落雷使用三段竖向警示柱和蓝白电弧；Boss 死亡演出使用核心红闪、裂纹光、火花、烟雾和 V7/V10 废铁碎片飞散后隐藏。

## Boss Overload Refinement V4

`Assets/Art/Generated/Enemies/V4/` 只新增 1 张透明 PNG，不参与碰撞，不改变 Boss 门逻辑、HUD 接口或单次伤害。

| File | Scene Usage |
| --- | --- |
| `bossv4_guardian_overload_overlay.png` | `Boss_RepairStationGuardian_V4OverloadOverlay`，只在 P3 核心暴走、受击高亮和死亡演出中显现，提供破损暗钢外甲、外露线缆、红热裂纹和焦黑边 |

三阶段技能的视觉语言：终段核心脉冲使用红橙低位预警加两侧蓝白电流；横扫接冲击波使用琥珀拖影转蓝白地火；砸地接电弧先给尘圈再给蓝白电弧圈。

## Gameplay Component Refinement V19

`Assets/Art/Generated/Environment/V19/` 是首关组件精细化覆盖件批次，只新增 3 张透明 PNG，不参与碰撞，不替换道路、背景或角色。

本轮组件特效增强不新增 PNG：`*_FXPolish` 节点复用 Effects/V2/V5 与 Environment/V7，为 Boss 入口锁、Boss 出口门、记录点、移动区箭头和出口箭头补充状态灯、halo、扫描线和短火花。

| File | Scene Usage |
| --- | --- |
| `component_terminal_screen_overlay.png` | `LoreTerminal_RebootLog_PolishRefined` 的终端屏幕和边框细节，读取时配合扫描线和短亮反馈 |
| `component_control_lock_plate_overlay.png` | `PowerSwitch_TrapBreaker_PolishRefined` 和 `Door_BossExit_MechCity_PolishRefined` 的控制/门锁板 |
| `component_status_core_overlay.png` | `Checkpoint_*_PolishRefined`、`ChargingStation_Temporary_PolishRefined` 和 `SupplyCrate_OptionalJumpCache_PolishRefined` 的小型状态核心 |

颜色规则保持系统可读性语言：琥珀为待机，青绿为安全/补给，红灯为危险/锁定，蓝白为电流和扫描。运行时只通过私有可选 SpriteRenderer 引用驱动灯光、扫描和电弧透明度，public gameplay API 不变。

## Spawn Intro Awakening

`SpawnIntro_AwakeningPolish` 是出生维修台叙事苏醒层，不新增 PNG、不参与碰撞，只复用 Environment/V7、Effects/V2 和 Effects/V5 资源。`PlayerSpawnIntro2D` 在场景开始时临时锁定输入和相机约 10 秒：先黑屏显示 A-07 故障日志，再慢速扫过废弃维修站，最后回到维修台苏醒；按方向键 / Space / J / E 可跳过，结束后恢复正常跟随，不影响检查点复活。

| Scene Object | Source Set | Purpose |
| --- | --- | --- |
| `SpawnIntro_NarrativeBackHalo` / `SpawnIntro_NarrativeScanLine` | Effects/V5, Effects/V2 | 环境扫镜阶段的远景弱光和扫描线 |
| `SpawnIntro_NarrativeWeakArc` / `SpawnIntro_NarrativeFineDust` / `SpawnIntro_NarrativeStatusLamp` | Effects/V5, Environment/V7 | 废弃维修站暗处的弱电弧、细尘和状态灯 |
| `SpawnIntro_WarmBenchHalo` / `SpawnIntro_LowFloorGlow` | Effects/V2, Effects/V5 | 维修台暖启动光和地面低亮焦点 |
| `SpawnIntro_ScanBeam` / `SpawnIntro_ScanHalo` | Effects/V2, Effects/V5 | 扫描光扫过主角，提示苏醒节奏 |
| `SpawnIntro_GroundFog` / `SpawnIntro_TopFineDust` | Effects/V5 | 低雾和顶部细尘，只做氛围，不进入玩法碰撞 |
| `SpawnIntro_ArmSpark` / `SpawnIntro_CableSpark` / `SpawnIntro_SteamLeft` / `SpawnIntro_SteamRight` | Effects/V2, Effects/V5 | 维修臂火花、线缆火花和蒸汽脉冲 |
| `SpawnIntro_BenchStatusLamp` / `SpawnIntro_RepairArm` | Environment/V7 | 状态灯和短动画维修臂，开场后保持低侵入 |

## Respawn Cinematic Polish

`RespawnPolish_Runtime` 是当前首关的复活电影感运行层，不新增 PNG、不参与碰撞，只复用 Environment/V7、Effects/V2 和 Effects/V5 资源。`PlayerRespawnCinematic2D` 覆盖玩家死亡、掉坑、油坑和电地板回传：先在失败点短闪，再用扫描光遮住传送，最后在检查点用 halo、蒸汽、火花和细尘重组。

| Scene Object | Source Set | Purpose |
| --- | --- | --- |
| `RespawnPolish_FailureFlash` / `RespawnPolish_FailureSpark` / `RespawnPolish_FailureDust` | Effects/V2, Effects/V5 | 失败点短闪、火花和尘雾 |
| `RespawnPolish_ReturnScanBeam` / `RespawnPolish_ReturnHalo` | Effects/V2, Effects/V5 | 检查点重组扫描和地面柔光 |
| `RespawnPolish_ReturnSteam` / `RespawnPolish_ReturnSpark` / `RespawnPolish_ReturnDust` | Effects/V2, Effects/V5 | 重组阶段的蒸汽、火花和细尘 |
| `RespawnPolish_ReturnStatusLight` | Environment/V7 | 重组完成时的低侵入状态灯 |
| `RespawnPoint_Polish_*` | Environment/V7, Environment/V19, Effects/V5 | 三个检查点的常驻柔光、状态核心、状态灯和首次激活脉冲 |
| `*_FXPolish` | Environment/V7, Effects/V2, Effects/V5 | 大门、记录点和箭头的特效增强层，负责锁定红光、解锁青绿闪烁、路线扫描和短火花 |

## Boss Door Refinement V20

`Assets/Art/Generated/Environment/V20/` 是 Boss 战门体精细覆盖件批次，只新增 2 张透明 PNG，不参与碰撞，不替换 Boss 机制、HUD 或道路。

| File | Scene Usage |
| --- | --- |
| `boss_entry_lock_refined_overlay.png` | `BossArena_EntryLock_V20_RefinedOverlay`，入口锁门的厚重锈铁门框、红色锁芯、液压夹具和线缆 |
| `boss_exit_gate_refined_overlay.png` | `Door_BossExit_MechCity_V20_RefinedOverlay`，Boss 出口门的双层暗钢门框、青绿通行槽和琥珀状态灯 |

## Background Atmosphere V12

`BG_BackgroundAtmosphere_V12` 是当前用于补充氛围的背景层，不参与碰撞。它只复用 `Effects/V5` 的透明 PNG，包含：

- `V12_FarLightning_*` / `V12_MidLightning_*`：墙面线缆与远景设备上的短促蓝白电弧。
- `V12_FarDust_*` / `V12_MidDust_*`：低透明浮尘与落灰层，放在中远景。
- `V12_FlickerLight_*`：维修站墙面、存档区侧墙和 Boss 门附近的弱闪烁灯光。

存档点附近的旧服务面板已从充电桩正后方移开，避免绿色组件互相叠在一起。

## Dynamic Decor V13

`BG_DynamicDecor_V13` 是当前“明显动态”背景层，仍然只复用现有 V9/V11、Provided/V3 与 Effects/V3-V5 资源，不新增碰撞，也不使用道路的琥珀可站立边线。

| Layer | Scene Usage |
| --- | --- |
| `BG_DynamicFarDecor_V13` | 低透明慢转风扇/齿轮、远景红灯、横跨区域的灰尘层 |
| `BG_DynamicMidDecor_V13` | 教学/陷阱/存档/Boss/出口附近的蓝白电弧、蒸汽、传送带光带、Boss 红灯和火光 |
| `BG_DynamicNearDecor_V13` | 零视差高空锁链和 Boss 区破旗；道路路灯只保留在 V11 层，避免重复叠放 |

近景层 `parallaxFactor` 固定为 `Vector2.zero`。锁链和破旗允许轻微摆动，路灯只由 V11 道路层负责闪烁，不做位移。开局区域只保留 `MidDecor_V11_Lamp_Awakening` 一盏主吊灯，V8/V9/V13 旧开局灯源不再生成。

## Map Readable Background V15

`Assets/Art/Generated/Backgrounds/V15/` 是当前现用大背景。它由 5 张 `4096x2048` 连续切片组成，目标是低噪声、低对比、道路高度留白，并在切片边缘预置暗柱和雾带来消除明显竖向分界线。

`BG_MapReadableDecor_V15` 是当前可读性优先的三层大组件装饰层，全部无碰撞、不使用琥珀可站立边线。

| Layer | Scene Usage |
| --- | --- |
| `BG_MapReadableFarDecor_V15` | 低透明大型机械墙、锅炉、传送带、齿轮墙和接缝遮挡 truss/尘雾 |
| `BG_MapReadableMidDecor_V15` | 风扇慢转、蓝白电弧、蒸汽、灯光 halo 和传送带扫描光 |
| `BG_MapReadableNearDecor_V15` | 零视差高空吊轨和吊钩，吊钩轻摆；不进入道路核心区域 |

## Large Decor V14

`Assets/Art/Provided/Environment/V4/` 来自用户提供的大背景图，已处理为透明/软边 PNG 并接入 `BG_LargeDecor_V14`。第一张大电缆桥图已按反馈从项目现用资源和场景引用中删除。这一层专门放更明显的大件背景，仍然无碰撞、不使用道路琥珀可站立边线。

| Layer | Scene Usage |
| --- | --- |
| `BG_LargeFarDecor_V14` | 低透明远景机械墙、锅炉、长桥和城市桥景，配合大范围灰尘、弱传送带光带和远处蒸汽 |
| `BG_LargeMidDecor_V14` | 中景机械框架、大风扇、锅炉、齿轮墙和出口机械框架；包含蓝白电弧、蒸汽、慢转风扇/齿轮和局部闪光 |
| `BG_LargeNearDecor_V14` | 零视差高空吊轨和吊钩，吊钩轻摆；开局、平台区和 Boss 区都有明显高空大件，位置保持在道路上方，避免和可踩地面混淆 |

## Provided Environment V2

`Assets/Art/Provided/Environment/V2/` 来自用户提供的工业组件图，已切成透明 PNG 并接入 `BG_ProvidedDecor_V2`。这些对象全部是背景装饰，无碰撞，不使用道路的琥珀可站立边线。

| File | Scene Usage |
| --- | --- |
| `provided_v2_chain_long.png` | 高空近景链条、中景充电区垂链 |
| `provided_v2_storage_crate.png` | 远景箱体堆，低透明度 |
| `provided_v2_pipe_horizontal.png` | 中景横向管道，带蒸汽和短促电弧 |
| `provided_v2_pipe_vertical.png` | 中/远景竖管，避开道路高度 |
| `provided_v2_truss_support.png` | 平台区和 Boss 区后景支架 |
| `provided_v2_hanging_lamp.png` | Boss 厅中景吊灯，带闪烁 halo |

## Provided Environment V3

`Assets/Art/Provided/Environment/V3/` 来自新增两张工业组件图，已切成透明 PNG 并接入 `BG_ProvidedDecor_V3`。这些对象全部是背景装饰，无碰撞，不使用道路的琥珀可站立边线。

| File | Scene Usage |
| --- | --- |
| `provided_v3_double_door.png` / `provided_v3_chain_fence.png` / `provided_v3_broken_window.png` | 远景墙面遮挡与背景切片接缝弱化 |
| `provided_v3_electric_box.png` / `provided_v3_vent_grate.png` | 中景墙面设备，配电箱带电弧 |
| `provided_v3_vent_fan.png` | 陷阱区中景慢转风扇 |
| `provided_v3_red_beacon.png` / `provided_v3_burning_barrel.png` | Boss/出口区闪烁灯光和火光 |
| `provided_v3_tire.png` / `provided_v3_scrap_heap.png` / `provided_v3_robot_wreck.png` | 远景废料剪影，低透明度 |
| `provided_v3_torn_flag.png` / `provided_v3_catwalk_grate.png` | 高空近景装饰，不进入道路高度 |

`provided_v3_broken_robot_arm.png` 已保留为切片资产，但当前不放入场景，以避免再次出现臂手抢画面的问题。

## Player Robot V3

V3 主角位于 `Assets/Art/Generated/Characters/PlayerRobot/V3/`。造型方向为普通开局小铁盒机器人：铁盒身体、两个琥珀眼、小短腿、小机械臂和尾部线缆。

| Scene Object | File | Purpose |
| --- | --- | --- |
| `Boxbot_BackArm` | `boxbot_back_arm.png` | 后臂 |
| `Boxbot_BackLeg` | `boxbot_back_leg.png` | 后腿 |
| `Boxbot_Body` | `boxbot_body.png` | 铁盒身体/头部合一 |
| `Boxbot_FrontLeg` | `boxbot_front_leg.png` | 前腿 |
| `Boxbot_FrontArm` | `boxbot_front_arm.png` | 前臂和小机械手 |
| `Boxbot_CableTail` | `boxbot_cable_tail.png` | 尾部线缆 |
| `Boxbot_Eyes` | `boxbot_eyes.png` | 双琥珀眼灯，带 `SpriteFlicker2D` |

## Import Settings

- V15 背景切片：Sprite, 96 pixels per unit, 4096 max texture size, uncompressed, no mipmaps, bilinear filter, clamp wrap.
- 主角部件：Sprite, 512 pixels per unit, alpha transparency, single sprite, uncompressed, no mipmaps, bilinear filter, clamp wrap.
- 环境件与特效：Sprite, 256 pixels per unit where applicable, uncompressed, no mipmaps, clamp wrap.

## Animation

主角动作由 `PlayerRobotVisualAnimator2D` 驱动。当前覆盖待机、慢走、Shift 奔跑、起跳、空中、下落、落地尘雾、攻击前臂前伸、天线晃动和眼灯闪烁。
