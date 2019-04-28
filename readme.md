# Emotion
a rouge-like card game

## 4/26 修改
 - ardmanager中加入了CurrentInde和CurrentCard方法，获得当前突出的卡牌和索引
 -              加入了GetCards方法，获得当前手牌。
 - 在view中实现了ShowCard（不完整，tedbu'nen）不能发生变化，暂时未想到好的显示卡牌的方法现了select的显示，可以随着AD显示。
 -  修复了敌人可以无限手牌的效果
 - 修复了玩家第一回合开始不能抽牌
 - 完成ShowCard的一个显示
 - 显示血量（这个我觉得不适合view里视图来写，但是硬写也可以）
 - 暂未实现卷轴效果

## 4/27修改
 -  都是在view里修改，加入了一个卷牌的效果，然后打完牌将牌显示在中间
 - 把敌人的牌显示了一下，不知道怎么去显示敌人出牌的情况，暂定
 - 添加了暂停的效果 
 - 修了一下selectCard的bug
## 4.24
 - 基本框架, 可以在Inspector面板中进行一场完整的游戏
 - 基本对局流程,包括抽牌,选牌,打出,造成效果,游戏状态判断与切换
 - 基本的人物、敌人属性
 - 基本卡牌属性,两种卡牌“怒气”、“治愈”
 - 基本卡牌效果,造成伤害和回血
 - 简单的敌人AI:随机打出一张牌
 - 操作方式:AD选择牌,K打出,打出后当前手牌置空,需要再次选择,J结束自己的回合,AI随机出牌,然后又是自己的
 - 回合

 - 待完成
 	- 更多的卡牌
 	- Buff效果
 	- 道具效果
 	- UI 
 - UML
 
  ![](https://raw.githubusercontent.com/Nagisa3113/Emotion/master/Emotion.jpg)
 	
 - 效果图
 
 ![](https://raw.githubusercontent.com/Nagisa3113/Emotion/master/inspector.png) 



    
    
                
