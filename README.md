# PVZ 函数API(重制版API)
## 枚举
```cs
enum Direction;
```
#### 枚举Direction扩展
```cs
public static class DirectionExtensions;
```
##### 方法1: 
```cs
public static Vector3 vector3(this Direction direction);
// 返回一个相应方向的Vector3
```
## GameManager.cs类: 继承MonoBehaviour.cs
```cs
public class GameManager: MonoBehaviour;
```
##### 核心值:
```cs
public GameManager _instance;
```
如果要访问这个值，则使用`GameManager.instance`，他代表着`GameManager`的唯一性。当你需要访问`GameManager`的`非静态方法`时，需要使用`instance`来调用，例如：
`GameManager.instance.Method();`
`GameManager.instance.Values;`
此方法更像是`GameObject.Find("GameManager").GetComponent<GameManager>().Method();`这样导致代码可读性变差，并且有一定的风险。
具体初始化方法如下：
```cs
public GameManager instance {
    get {
        if(_instance == null)
            _instance = this;
        return _instance;
    }
    // !set 禁止设置和修改此值
}
```
