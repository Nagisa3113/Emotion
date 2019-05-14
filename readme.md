# Emotion

a rogue-like card game



## 5.12

* 重写了卡牌的管理方式,现在每张卡牌都从基类Card派生
* eg: 添加卡牌方式从 Add(new Card(CardName)) 改为 Add(CardManager.GetNewCard(CardName))
* 删除Effect类,直接在Card类中写卡牌效果
* eg: 从 Effect.TakeEffect(CardName) 改为 card.TakeEffect()
* 修改了UML图等

## 5.11

* 完成紫色卡牌的ai
* 有些疑问，待交流后进一步实施
* 暂未改动view上的问题


# 5.9

* 对接代码，将view与代码对接
* 完成enemyAI的大致思路

## 5.6

- 完成指定的一套卡牌
- 完成Buff效果
- 更新了UML图等,在此页面最后
- 部分卡牌效果需要等待敌人AI完成后才能看到效果
- 修改框架:卡牌、BUFF效果通过传递名称(枚举类型)调用相应方法
- 待完成:
  - 敌人AI
  - Buff UI

## 4.27

- 都是在view里修改，加入了一个卷牌的效果，然后打完牌将牌显示在中间
- 把敌人的牌显示了一下，不知道怎么去显示敌人出牌的情况，暂定
- 添加了暂停的效果 
- 修了一下selectCard的bug

## 4.26

- Cardmanager中加入了CurrentInde和CurrentCard方法，获得当前突出的卡牌和索引
- 加入了GetCards方法，获得当前手牌。
- 在view中实现了ShowCard（不完整，tedbu'nen）不能发生变化，暂时未想到好的显示卡牌的方法现了select的显示，可以随着AD显示。
- 修复了敌人可以无限手牌的效果
- 修复了玩家第一回合开始不能抽牌
- 完成ShowCard的一个显示
- 显示血量（这个我觉得不适合view里视图来写，但是硬写也可以）
- 暂未实现卷轴效果

## 4.24

- 基本框架, 可以在Inspector面板中进行一场完整的游戏
- 基本对局流程,包括抽牌,选牌,打出,造成效果,游戏状态判断与切换
- 基本的人物、敌人属性
- 两种卡牌“怒气”、“治愈”
- 简单的敌人AI:随机打出一张牌
- 操作方式:AD选择牌,K打出,打出后当前手牌置空,需要再次选择,J结束自己的回合

## 图

  ![](https://github.com/Nagisa3113/Emotion/blob/Liu/Emotion.jpg)

  ![](https://github.com/Nagisa3113/Emotion/blob/Liu/Card.jpg) 
