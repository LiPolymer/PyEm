# PythonEntanglement

> 警告: 由于 Pythonnet 的不稳定性与抽风特点, 请勿将此插件直接用于生产用途
>
> 如发生 崩溃/冻结/卡死 导致事故 概不负责

> 已测试 Python3.8 可用, 其他版本不保证正常工作
> 
> 由于使用外置解释器, 理论上支持 pip 上的轮子

> 使用前请到对应设置页面选定 Python 库文件
> 
> e.g. `python38.dll`

在ClassIsland中使用Python进行自动化!

example:
```python
def action(ci: Interface): # 行动中调用的方法无需返回值
    print("Loaded in ClassIsland v" + ci.version)
    print("Codename " + ci.code_name)
    print("设置项 Scale: " + str(ci.app.Settings.Scale))
    #print("正在写入配置")
    #ci.app.Settings.Scale = 1.5
    print("当前课程: " + str(ci.lessonsService.CurrentSubject.Name))
    #ci.showInfo("Hello!")
    #ci.action.sendSignal("hello")
    ci.action.invoke("""
            [
                {
                    "Id": "classisland.showNotification",
                    "Settings": {
                        "Content": "Hello from Python!",
                        "Mask": "\u6D4B\u8BD5",
                        "IsContentSpeechEnabled": true,
                        "IsMaskSpeechEnabled": true,
                        "IsAdvancedSettingsEnabled": false,
                        "IsSoundEffectEnabled": true,
                        "IsTopmostEnabled": true,
                        "CustomSoundEffectPath": "",
                        "IsEffectEnabled": true,
                        "MaskDurationSeconds": 5,
                        "ContentDurationSeconds": 10,
                        "IsActive": false
                    },
                    "IsActive": false
                }
            ]
    """)


def rule(ci): # 规则里面调用的方法需要返回bool值
    print("Loaded in ClassIsland v" + ci.version)
    print("Hello From RuleSet")
    return True
```