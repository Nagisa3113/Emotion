# Emotion

a rogue-like card game

- 基本对局流程,包括抽牌,选牌,打出,造成效果,游戏状态判断与切换
- UI: 待补充
- 敌人AI: 待补充
- 键盘操作方式:AD选择牌,K打出,打出后当前手牌置空,需要再次选择,J结束自己的回合
- 卡牌、BUFF效果通过传递名称(枚举类型)调用相应方法
- 在Role中用List<Card>表示牌库
- 每张卡牌都从基类Card派生,添加卡牌方式从 Add(Card.NewCard(CardName))
- 使用反射创建Card类对象，卡牌效果为TakeEffect(self,target)


## 图

  ![](https://github.com/Nagisa3113/Emotion/blob/Liu/Emotion.jpg)

  ![](https://github.com/Nagisa3113/Emotion/blob/Liu/Card.jpg) 
