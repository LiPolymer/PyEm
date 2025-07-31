def main(ci):
    print("Loaded in ClassIsland v" + ci.ver)
    print("Codename " + ci.code_name)
    print("设置项 Scale: " + str(ci.app.Settings.Scale))
    print("正在写入配置")
    ci.app.Settings.Scale = 1.5
    print("当前课程: " + str(ci.lessonsService.CurrentSubject.Name))
    #ci.showInfo("Hello!")

def rule(ci):
    print("Loaded in ClassIsland v" + ci.ver)
    print("Hello From RuleSet")
    return True