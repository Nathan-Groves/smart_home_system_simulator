from kivy.app import App
from kivy.uix.widget import Widget
from kivy.lang import Builder
##imports the App class from the kivy.app module and the Widget class from the kivy.uix.widget module.

Builder.load_file('smarthome.kv')

##creates a new class called MainWidget that inherits from the Widget class. This class will be the root widget of the application.
class SmartHome(Widget):
    pass

##creates a new class called MainWidget that inherits from the Widget class. This class will be the root widget of the application.
class MainApp(App):
    def build(self):
        return SmartHome()

MainApp().run()
