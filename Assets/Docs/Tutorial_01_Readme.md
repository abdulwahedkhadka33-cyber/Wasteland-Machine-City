# 锈铁维修站

首张正式教学地图已生成到 `Assets/Scenes/Tutorial_01_AwakeningCorridor.unity`，并加入 Build Settings。场景名沿用旧文件名，地图内容已改为「锈铁维修站」。

## Controls

- A / D: 行走
- Shift + A / D: 奔跑
- Space: 跳跃
- J: 普通攻击
- E: 互动、读取日志、启动充电站

## Implemented Gameplay

- 约 176 Unity units 的横版第一关流程。
- 8 个区域：出生维修台、移动教学、小平台跳跃、第一个敌人、电流/压缩机陷阱、存档充电桩、Boss 维修大厅、机械城入口门。
- 基础教学：行走、奔跑、跳跃、攻击、交互、检查点、观察机关节奏。
- 出生点由 `PlayerSpawnIntro2D` 播放约 10 秒可跳过电影感叙事片头：先用居中琥珀单色工业启动终端逐行显示 A-07 故障日志，再慢速扫过废弃维修站，最后回到维修台完成更完整的扫描光、维修臂轻摆、蒸汽、火花和主角苏醒。
- 玩家死亡、油坑/掉落和电地板回传统一由 `PlayerRespawnCinematic2D` 播放约 1.25 秒系统重组：失败点闪烁，传送时扫描遮罩，检查点处用 halo、蒸汽、火花和细尘重组。
- 三倍高可破坏铁箱用于攻击教学，阻止玩家轻松跳过；第一个巡逻维修机器人用于低压战斗教学。
- 间歇电流地板和上下压缩机用于第一关机关教学，不直接改变地图路线。
- 临时充电站可交互，恢复生命并记录检查点。
- 记录点、日志终端、电流地板、电闸、Boss 出口门、充电站和补给箱都新增 `*_PolishRefined` 视觉节点：复用 V19 透明覆盖件和现有 halo/scan/electric/steam 素材，状态更清楚但不新增碰撞。
- Boss 入口锁、Boss 出口门、记录点、移动区箭头和出口箭头新增 `*_FXPolish` 特效节点：红色表示锁定/危险，青绿表示解锁/安全，琥珀扫描表示路线引导，全部无碰撞。
- Boss 入口锁门和 Boss 出口门新增 V20 精细覆盖件：入口偏红色锁闭压迫，出口偏青绿通行反馈，门体更厚重但不改 Boss 战触发、碰撞或开门条件。
- 结尾 Boss「维修站守卫者」由 `BossEncounter_StartTrigger` 启动：入口红灯锁闭、顶部 Boss 血条显示、镜头临时收紧到 Boss 厅；击败后入口锁解除，`Door_BossExit_MechCity` 继续自动打开。
- Boss 数值中等强化：14 耐久，每招 1 点伤害；P1 横扫/砸地，P2 半血过载后只召唤一次 2 台 1 耐久小维修机，之后轮换贴地冲击波、锁定脚下的过载电弧、低位核心光束和三段顶部落雷。
- Boss 低血进入 P3 核心暴走：V4 破损覆盖层显现，并加入双向核心脉冲、横扫接冲击波、砸地接电弧连携；所有强招会先显示蓝白/红橙/琥珀预警再出伤害，死亡演出约 1.8 秒，包含核心爆闪、外甲裂纹、碎片飞散、火花烟雾和最终隐藏。
- 墙面裸红点已替换为语义明确的红色警示灯/状态灯 sprite，只放在维修台、敌人区、陷阱区、Boss 门和出口附近。
- 主角仍为精细小铁盒机器人，带琥珀眼灯、天线灯、轮廓光和脚底阴影，已支持待机、走路、奔跑、跳跃、落地和攻击动作。
- 当前版本使用 V15 清爽连续背景、V8/V10 道路组件、V9/V11/V13/V14/V15/V16 背景装饰和 Effects/V2-V5 动态氛围；可见环境不再使用裸色块或无意义小点。
- 可踩道路保持 V8/V10 体系，只做小幅精修：琥珀边线更细，主路侧面和支架层次更清楚，划痕、裂纹和油污透明度降低，跳跃平台落点更干净。
- 新增 `BG_SystemPolish_Readability` 四层系统美化：路线暗带压住道路附近噪声，区域地标柔光提示推进方向，危险/交互物按蓝电、红灯、绿色充能和青色补给统一语言，Boss 厅增加热雾和顶部电弧。
- 用户提供的链条、箱体、管道、支架和吊灯已切成透明 PNG，作为 `BG_ProvidedDecor_V2` 三层背景装饰接入。
- 新增双开门、铁丝网、配电箱、通风格栅、破窗、风扇、油桶/废料、燃烧桶、破旗等切片，作为 `BG_ProvidedDecor_V3` 三层背景装饰接入。
- V15 背景切片已重做为低噪声、低对比连续背景；切片边缘预留暗柱和雾带，再由 V15 大型结构、尘雾和高空吊轨遮挡，避免再出现明显硬竖线。
- 新增 `BG_DynamicDecor_V13` 三层动态背景：远景慢转机械、远处红灯和灰尘，中景蓝白闪电/蒸汽/传送带光带，近景高空锁链与道路后方闪烁路灯。
- V13 中原本额外补的近景路灯已移除，只保留 V11 道路路灯闪烁，避免路灯重复叠放。
- 开局灯光已去重：只保留 `MidDecor_V11_Lamp_Awakening` 这一盏精细吊灯和它的 halo，V8/V9/V13 的开局重复灯不再生成。
- 新增 `BG_LargeDecor_V14` 三层大件动态背景：远景机械墙/锅炉/长桥，中景大风扇/齿轮墙/机械框架，近景高空吊轨和吊钩；开局上缘新增零视差吊轨和轻摆吊钩，风扇/齿轮慢转，机械框架有蓝白闪电，锅炉带蒸汽和浮尘。第一张大电缆桥图已从场景中删除。
- 新增 `BG_MapReadableDecor_V15` 三层可读性增强背景：远景低透明大机械墙、锅炉、传送带和齿轮墙；中景风扇、蓝白电弧、蒸汽、灯光 halo 和扫描光；近景只放高空吊轨/吊钩，近景零视差且不进入道路核心区域。
- 新增 `BG_DynamicPolish_V16` 四层清晰增强动态美化：远景尘雾/传送带/慢转机械，中景区域灯光、电弧、蒸汽和扫描光，近景只放高处链条/旗帜，GameplayReadable 层只做低透明地面下方雾光，避免遮挡玩家和平台边线。
- 出生维修台本体改用 `Environment/V12/spawn_repair_bed_refined.png`；`AwakeningBench_RefinedAssembly` 统一管理精细维修台本体和局部细节，`AwakeningBench_TablePolish` 只补台体磨损、铆钉、油污、底部暖光、线缆、扫描线和状态灯；不改全局道路，不新增碰撞。
- V19 组件覆盖件只用于组件精细化：终端屏幕、控制/门锁板和状态核心分别服务日志、开关/门锁、记录点/充电/补给反馈；琥珀为待机，青绿为安全/补给，红灯为锁定/危险，蓝白为电流。
- `*_FXPolish` 不新增图片资源，只复用 Effects/V2/V5 和 Environment/V7 做状态灯、halo、扫描线与短火花；它只强化大门、记录点、箭头和出口引导，不改变触发器、碰撞或路线。
- V20 Boss 门覆盖件只用于 `BossArena_EntryLock` 和 `Door_BossExit_MechCity`：透明 PNG 叠加在旧 V7 门体上，配合红色锁闭、蓝白电弧、底部蒸汽和青绿解锁扫描形成压轴门体语言。
- 出生区大型旧维修臂仍不进场；`SpawnIntro_RepairArm` 只作为开场短动画件使用，并由 `SpawnIntro_AwakeningPolish` 统一管理；开场使用居中琥珀单色启动日志和环境扫镜，随后维修台轻微震动并短闪蓝白电弧，避免长期压住玩家轮廓。
- 截图中的跳跃路段新增 `JumpRoute_BackgroundPolish_BrokenPlatforms`：背景机械框、修理墙板、破窗、支架、下层平台后方暖 halo、上层平台柔光、右侧平台青色引导、下方油雾、蒸汽、电弧、扫描光和高处轻摆链条，全部无碰撞且不进入玩家跳跃核心空间。
- 平台、维修台、管道、铁链、灯具、箱子、陷阱、充电桩、Boss 门、出口门都以生成 PNG 为主，碰撞体保留为隐藏逻辑层。
- 巡逻维修机器人和 Boss 已切到 Enemies/V2 视觉：红眼、受击火花、死亡残骸/烟雾，以及 Boss 横扫臂精细图。
- 背景使用 V15 连续切片覆盖 x=-12 到 x=181 左右；相机横向跟随、纵向稳定，跳跃不露边。
- 场景动态层包含落灰雾、灯光 halo、蓝色电流、蒸汽、火花、慢转风扇/齿轮、扫描光、V12/V13/V16 背景闪电、燃烧桶火光、旗帜/锁链轻摆和 Boss 砸击尘浪。
- HUD 已升级为废土工业终端 IMGUI 游戏界面，目标固定左上安全区，耐久、提示、交互和 Boss 血条使用小字号、短文案、暗钢锈铁面板、角部铆钉、状态灯、机械分段条、轻量扫描线和受击闪烁；镜头增加轻微横向 lookahead，Boss 战时可临时收紧边界，战后恢复原边界。
- 三个检查点新增 `RespawnPoint_Polish_*` 低侵入光效；首次激活时播放短脉冲，后续复活时作为系统重组落点，不改变路线和碰撞。

## Generated Art

- V15 连续背景切片：`Assets/Art/Generated/Backgrounds/V15/`
- V7 精细环境组件：`Assets/Art/Generated/Environment/V7/`
- V8 道路与清爽背景组件：`Assets/Art/Generated/Environment/V8/`
- V9/V11 背景装饰组件：`Assets/Art/Generated/Environment/V9/`、`Assets/Art/Generated/Environment/V11/`
- 用户提供切片背景组件：`Assets/Art/Provided/Environment/V2/`、`Assets/Art/Provided/Environment/V3/`、`Assets/Art/Provided/Environment/V4/`
- V10 道路细节组件：`Assets/Art/Generated/Environment/V10/`
- V19 组件精细化覆盖件：`Assets/Art/Generated/Environment/V19/`
- V2 动态与氛围特效：`Assets/Art/Generated/Effects/V2/`
- V1 动作与命中特效：`Assets/Art/Generated/Effects/V1/`
- V2 敌人与 Boss：`Assets/Art/Generated/Enemies/V2/`
- V3 Boss 精细覆盖件：`Assets/Art/Generated/Enemies/V3/bossv3_guardian_refined_overlay.png`
- V4 Boss 暴走覆盖件：`Assets/Art/Generated/Enemies/V4/bossv4_guardian_overload_overlay.png`
- V3 主角部件：`Assets/Art/Generated/Characters/PlayerRobot/V3/`
- V3 柔光与动态特效：`Assets/Art/Generated/Environment/V3/`

构建器默认引用 V15 背景；旧 V8 背景保留作备份但不再进场景，旧主角部件、V1 敌人参考和生成源图继续保持清理后的状态。

## Validation

重新生成场景：`Tools/Wasteland Mech City/Build Tutorial Scene`

验证关键对象：`Tools/Wasteland Mech City/Validate Tutorial Scene`

当前验证覆盖：8 个区域、V15 背景、V7/V8/V9/V10/V11/V19/V20 环境组件、Enemy/V3/V4 Boss 覆盖件、Provided/V2/V3/V4 背景装饰、开局单灯、10 秒叙事片头、出生苏醒控制器、输入锁接口、出生 polish 根节点、叙事扫镜目标和特效引用、跳跃路段背景层、复活电影感控制器、三个检查点 polish 根节点、检查点 FXPolish、箭头 FXPolish、检查点激活脉冲、V19 状态核心、V20 Boss 门覆盖件、复活扫描/蒸汽/火花引用、V13 明显动态层、V14 大件动态层、V15 可读性动态层、V16 清晰增强动态层、`BG_SystemPolish_Readability` 系统美化层、Effects/V2-V5 氛围、无可见 `white_pixel`、破箱、巡逻敌人、电流地板、精细电闸、精细日志终端、精细出口门、出口门 FXPolish/V20FX、压缩机、充电桩、补给箱、Boss 遭遇控制器、入口锁、入口锁 FXPolish/V20FX、Boss 血条接口、Boss 方向化 hitbox 参数、过载电弧/核心光束/顶部落雷/终段核心脉冲/连携技能引用、二/三阶段数值、死亡破碎演出引用、临时镜头边界接口、Boss 死亡开门、主角轮廓光、动作特效与背景动态组件。

V12/V13 氛围层会额外检查背景闪电、浮尘、闪烁灯光、近景零视差和无碰撞背景装饰，避免再次出现小组件堆叠到玩法物件上的问题。
