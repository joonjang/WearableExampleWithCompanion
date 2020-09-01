# WearableExampleWithCompanion
Check out my video about this app here: https://www.youtube.com/watch?v=7rmYD3PQv24


FAQ:

When you run the Android app you run into this excpetion:
```
ITouchExplorationStateChangeListenerImplementor due to MAX_PATH: System.IO.DirectoryNotFoundException: Could not find a part of the path
```
The filepath to the solution is too long, relocate the repo.
Refer to: https://forums.xamarin.com/discussion/156224/xamarin-android-javatypeinfo-build-error  


"Should I be able to test and debug it on emulators"
I used a physical phone and watch to test and debug as I couldn't get the watch emulator paired with the Android companion app


"It gives me Segmentation Fault exception when I try to connect to the service but maybe this is expected"
What I did to overcome that issue was by using the device logs and Console.WriteLine to see if my functions were entering and exiting correctly.


"I’m stuck with inability to use my watch with the debugger. The watch is connected to the network"
Did you create a certificate for the watch? Visual Studio has a toolbar menu that links to the Tizen Certificate Manager program but it does nothing. The bug is that they used the wrong application. 
Go to: .tizen\tizenSDK\tools\certificate-manager
"certificate-manager.exe" is the bug, the real program is "eclipse.exe"
