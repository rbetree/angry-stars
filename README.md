# 愤怒全明星 (Angry stars)

一个基于Unity引擎开发的愤怒的小鸟二创版本，采用C# + Rust混合架构，提供高性能的物理计算和完整的游戏体验。

## 🎮 游戏特色

- **经典愤怒的小鸟玩法**：弹弓发射英雄，摧毁建筑物，消灭所有敌人
- **多种英雄类型**：每种英雄都有独特的特殊技能
- **物理驱动**：基于Unity 2D物理系统的真实碰撞检测
- **高性能计算**：使用Rust实现精确的弹簧关节物理计算
- **完整游戏循环**：关卡选择、游戏进行、胜负判定、星级评分
- **数据持久化**：游戏进度和星级自动保存

## 🐦 英雄类型

| 英雄类型 | 特殊技能 | 使用方法 |
|---------|---------|---------|
| ggband | 无特殊技能 | 基础英雄 |
| 哈吉米 | 加速冲刺 | 飞行中点击鼠标，速度翻倍 |
| 曼波 | 爆炸攻击 | 飞行中点击鼠标，炸死范围内所有敌人 |
| 香蕉咪咪 | 回旋镖 | 飞行中点击鼠标，水平方向反转 |

## 🛠️ 技术架构

### 核心系统
- **游戏管理器 (GameManager)**：控制游戏流程、胜负判定、星级计算
- **英雄系统 (Bird)**：弹弓机制、拖拽控制、飞行物理、技能系统
- **敌人系统 (Pig)**：基于碰撞速度的伤害判定
- **物理增强 (SpringJoint2DProxy)**：高精度弹簧关节计算

### 混合语言架构

- **Unity C#**：游戏逻辑、UI界面、资源管理
- **Rust**：高性能物理计算、弹簧关节模拟

## 📋 系统要求

### 开发环境
- **Unity版本**：2022.3.58f1 或更高版本
- **Rust工具链**：1.70+ (用于编译物理计算模块)
- **操作系统**：Windows 10/11, macOS 10.15+, Ubuntu 18.04+

## 🎯 游戏玩法

### 基本操作
1. **瞄准**：鼠标拖拽英雄进行瞄准
2. **发射**：松开鼠标发射英雄
3. **技能**：英雄飞行过程中点击鼠标使用特殊技能
4. **目标**：消灭所有敌人获得胜利

### 评分系统

- 剩余英雄越多，获得星星越多
- 最多可获得3颗星
- 星级会自动保存到本地

## 📁 项目结构

```
AngryBirdTest/
├── Assets/
│   ├── Scripts/           # C# 游戏脚本
│   │   ├── GameManager.cs # 游戏管理器
│   │   ├── Bird.cs        # 英雄基类
│   │   ├── *Bird.cs       # 各种英雄实现
│   │   ├── Pig.cs         # 敌人脚本
│   │   └── ...
│   ├── Scenes/            # 游戏场景
│   │   ├── 00-Loading.unity
│   │   ├── 01-level.unity
│   │   └── 02-game.unity
│   ├── Prefabs/           # 预制体资源
│   ├── Image/             # 图片资源
│   ├── Music/             # 音频资源
│   └── Materials/         # 材质资源
├── rust_game_logic/       # Rust 物理计算模块
│   ├── src/
│   │   └── lib.rs         # 弹簧关节物理计算
│   ├── Cargo.toml
│   └── Cargo.lock
├── ProjectSettings/       # Unity 项目设置
└── README.md             # 项目说明文档
```

## 🔧 开发指南

### 添加新的英雄类型

1. 继承 `Bird` 基类
2. 重写 `ShowSkill()` 方法实现特殊技能
3. 创建对应的预制体
4. 在GameManager中注册新英雄

### 自定义关卡
1. 在 `Assets/Scenes/` 中创建新场景
2. 放置建筑物、敌人和英雄
3. 配置GameManager的英雄和敌人列表
4. 更新关卡选择界面

### 物理参数调整
- 修改 `SpringJoint2DProxy.cs` 中的物理参数
- 或者直接编辑Rust模块 `rust_game_logic/src/lib.rs`

## 📦 依赖包

### Unity Packages
- Unity 2D Sprite (1.0.0)
- Unity 2D Tilemap (1.0.0)
- TextMeshPro (3.0.7)
- Unity Analytics (3.8.1)
- Unity Ads (4.4.2)

### Rust Dependencies
- serde (1.0) - JSON序列化
- serde_json (1.0) - JSON处理

## 📄 许可证

本项目采用 MIT 许可证 - 查看 [LICENSE](LICENSE) 文件了解详情

## 🙏 致谢

- Unity Technologies - 游戏引擎
- Rust Foundation - 高性能计算语言
- 原版愤怒的小鸟 - 游戏设计灵感
