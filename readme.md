# Emotion

a rogue-like card game

- 删除了CardLibrary和CardDiscard脚本，直接在Role中用List<>表示
* 每张卡牌都从基类Card派生,添加卡牌方式从 Add(Card.NewCard(CardName))
* Card类中写卡牌效果, card.TakeEffect()
- 敌人AI: 待补充
- 卡牌、BUFF效果通过传递名称(枚举类型)调用相应方法
- 基本框架, 可以在Inspector面板中进行一场完整的游戏
- 基本对局流程,包括抽牌,选牌,打出,造成效果,游戏状态判断与切换
- 操作方式:AD选择牌,K打出,打出后当前手牌置空,需要再次选择,J结束自己的回合

## 图

  ![](https://github.com/Nagisa3113/Emotion/blob/Liu/Emotion.jpg)

  ![](https://github.com/Nagisa3113/Emotion/blob/Liu/Card.jpg) 
