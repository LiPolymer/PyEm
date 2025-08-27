class AppSettings:
    def __init__(self):
        self.Scale = 1.0

class AppBase:
    def __init__(self):
        self.Settings = AppSettings()

class Subject:
    def __init__(self, name):
        self.Name = name

class LessonsService:
    def __init__(self):
        self.CurrentSubject = Subject("数学")

class ActionSetManagerClass:
    def invoke(self, json_str):
        print(f"调用动作集: {json_str}")

    def sendSignal(self, signal):
        actions = f"""[
    {{
        "Id": "classisland.broadcastSignal",
        "Settings": {{
            "SignalName": "{signal}",
            "IsRevert": false,
            "IsActive": false
        }},
        "IsActive": false
    }}
]"""
        print(actions)
        self.invoke(actions)

class Interface:
    def __init__(self):
        self.app = AppBase()
        self.code_name = "ClassIsland"
        self.version = "1.0.0"
        self.lessonsService = LessonsService()
        self.actionService = None
        self.action = ActionSetManagerClass()

    def showInfo(self, msg):
        print(f"信息: {msg}")
