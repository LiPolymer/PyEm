from dummy import Interface
def main(ci: Interface):
    print("Loaded in ClassIsland v" + ci.version)
    print("Codename " + ci.code_name)
    print("设置项 Scale: " + str(ci.app.Settings.Scale))
    print("正在写入配置")
    ci.app.Settings.Scale = 1.5
    print("当前课程: " + str(ci.lessonsService.CurrentSubject.Name))
    ci.action.sendSignal("hello")
    #ci.action.invoke("""
    #        [
    #            {
    #                "Id": "classisland.showNotification",
    #                "Settings": {
    #                    "Content": "Hello from Python!",
    #                    "Mask": "\u6D4B\u8BD5",
    #                    "IsContentSpeechEnabled": true,
    #                    "IsMaskSpeechEnabled": true,
    #                    "IsAdvancedSettingsEnabled": false,
    #                    "IsSoundEffectEnabled": true,
    #                    "IsTopmostEnabled": true,
    #                    "CustomSoundEffectPath": "",
    #                    "IsEffectEnabled": true,
    #                    "MaskDurationSeconds": 5,
    #                    "ContentDurationSeconds": 10,
    #                    "IsActive": false
    #                },
    #                "IsActive": false
    #            }
    #        ]
    #""")


def rule(ci):
    print("Loaded in ClassIsland v" + ci.version)
    print("Hello From RuleSet")
    return True

def boring(ci):
    import time
    import random
    
    print("耐心测试（linux上制作）")
    print("版本 v0.02beta - 心理承受力评估系统")
    print("=" * 40)
    time.sleep(1)
    
    print("\n开始测试你的耐心等级...")
    time.sleep(2)
    
    for i in range(100):
        print(f"\n第 {i+1}/100 轮耐心挑战")
        print("正在分析你的脑电波", end="")
        for _ in range(3):
            print(".", end="", flush=True)
            time.sleep(0.5)
        
        wait_time = random.randint(1, 3)
        print(f"\n请等待 {wait_time} 秒...")
        time.sleep(wait_time)
        
        print("\n选项：")
        print("1. exit - 我认输了！")
        print("2. 任意键 - 继续挑战（获得+1耐心值）")
        
        choice = input("你的选择: ").lower().strip()
        
        if choice == "exit":
            print("\n" + "=" * 40)
            print("测试结果：耐心不足！")
            print("你的耐心等级：幼儿园水平")
            print("心理承受力：需要加强")
            print("建议：多玩这个游戏锻炼耐心")
            exit()
        else:
            if random.random() < 0.3:  # 30%几率嘲讽
                print("居然还在坚持？意外意外...")
            else:
                print("继续挑战！耐心值+1")
    
    print("\n" + "=" * 40)
    print("不可思议！你完成了100轮挑战！")
    print("称号：耐心之神")
    print("耐心等级：MAX")
    print("证书：授予「最能忍」荣誉称号")
    print("\n但是...游戏刚刚热身结束！")
    print("接下来是真正的挑战...")
    time.sleep(3)
    print("开个玩笑啦！恭喜你通关！")