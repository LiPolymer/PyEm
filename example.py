def main(ci):
    print("Loaded in ClassIsland v" + ci.version)
    print("Codename " + ci.code_name)
    print("设置项 Scale: " + str(ci.app.Settings.Scale))
    #print("正在写入配置")
    #ci.app.Settings.Scale = 1.5
    print("当前课程: " + str(ci.lessonsService.CurrentSubject.Name))
    #ci.showInfo("Hello!")
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

def rule(ci):
    print("Loaded in ClassIsland v" + ci.version)
    print("Hello From RuleSet")
    return True