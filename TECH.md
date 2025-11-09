# 架构说明

主要以 面向对象 为基础进行架构，包含了主要系统：

    AoE系统

    时间轴系统

    数据管理系统

    事件系统

    UI系统（基于UGUI）

    资源管理系统

    对象池系统

    总系统管理器

    通过 门面模式 对各个系统的功能进行封装，每个系统功能封装为对应的ToolKit

    

    游戏对象逻辑通过 修饰器模式  进行。将各个具体功能抽象为 效果器 与 触发时机，在对应的触发时机调用效果器。如Buff、塔防、敌人都是通过一个Agent或者Entity进行管理，在适当时机调用对应的所有触发器 



自定义编辑器功能主要通过Odin进行实现

    可以对Json进行可视化编辑，生成一个新的单位或buff只需要配置自身的数值属性和对应的效果器就可以了，因此新功能只需要编写新的效果器就可以了，而不用重新编辑一个完整的单位或buff

    效果器通过自动化脚本可以自动生成模板



动画效果通过Dotween实现



一些等待逻辑通过UnTask实现



# Buff伤害计算顺序

添加：调用Agent的buffHandler进行Buff添加，同时检测自身是否免疫该buff(buffID）是否在list配置列表里。添加成功则进入待增加列表，等待下一帧添加该buff。如果有该buff则会检测自身是否可以叠层，如果可以则直接叠层，并告诉效果器加层数了，刷新自身的效果。

移除：检测是否真的有buff，如果存在则加入待删除列表，等待下一帧删除buff，并触发对应的效果器进行回收。

更新：在Agent的update中调用buffHandler的刷新方法，进行时间计算。

伤害逻辑：基于buff的效果器触发，如增加了一个燃烧buff，燃烧buff本质是拥有一个造成伤害的效果器，如tick的时机拥有效果器造成伤害那么就调用一次，直到等到下一次tick时机再调用。

回收：对于数值影响的buff如速度，拥有效果器 添加属性修改器，在OnTrigger中进行修改器添加，buff在OnStart的时候添加，在效果器的OnEnd中进行修改器移除，在buff的OnEnd中对OnStart的效果器的OnEnd进行触发移除。

伤害触发：敌人拥有一个伤害计算器的List<Func<float float>>，在受到伤害的时候传入实际伤害，计算额外的增伤。



# 数据格式样例

BuffData 减速Buff

  {
    "ID": "buff_1",
    "Name": "slow",
    "Type": 1,
    "IconPath": null,
    "Description": null,
    "MaxStack": 1,
    "Duration": 10.0,
    "TickInterval": -1.0,
    "Effects": {
      "Start": [
        {
          "effectType": "AttriModifier",
          "param": {
            "$type": "Tower.Runtime.Gameplay.EffectParam_AttriModifier, Assembly-CSharp",
            "attrForge": 4,
            "value": -0.2
          }
        }
      ]
    }
  }



EnemyData 红色幽灵

  {
    "ID": "enemy_1",
    "Name": "red",
    "IconPath": "enemy_1",
    "Des": "Ordinary red ghost, harmless",
    "Health": 2.0,
    "Speed": 1.0,
    "Money": 4,
    "ImmuneBuff": [],
    "DefaultEffects": {}
  },



TowerData 一级箭塔

{
    "ID": "tower_1_level_1",
    "Name": "tower_1",
    "IconPath": "Target_level_1",
    "Des": "Single target damage, fast attack speed, long range",
    "Attack": 1.0,
    "HitSpeed": 2.0,
    "Range": 2.0,
    "Cost": 20,
    "NextID": "tower_1_level_2",
    "DefaultEffects": {
      "Action": [
        {
          "effectType": "FireBullet",
          "param": {
            "$type": "Tower.Runtime.Gameplay.EffectParam_FireBullet, Assembly-CSharp",
            "bulletName": "target_bullet",
            "targetCount": 1,
            "takeDamage": true,
            "boom": false,
            "boomRadius": 0.0,
            "boomRate": 0.0,
            "boomVFX": null
          }
        }
      ]
    }
  }



# 扩展点

资源管理：

    在MyTools中进行资源的管理

    使用AB包进行资源压缩

    分为路径读取和AB读取两种模式进行资源加载

    在对应的文件夹右键进行AB标记，自动添加AB包名称标记，同时将路径记录

key = abName value = path

    在进行AB包获取资源的时候，根据模式来进行，在编辑器中，可以自行选择，打包后则必须进行AB包读取

    路径读取根据路径映射，通过Assets.LoadPath进行读取对应的资源

    AB包通过直接和异步两种方式获取，通过可以自动计数进行卸载



基于Odin的编辑器：

    在MyTools可以编辑数据或自动生成效果器脚本模板

    对于数据，在打开窗口后自动读取Json，加载进窗口，可以进行可视化编辑，保存json。

    可以进行关卡的配置，并在场景中拖拽路径点进行一键的路径配置

    对于效果器：输入指定名称，生成效果器，自动加入到效果器的字典中，

    key = effectName value = IEffectParam

    



# 
