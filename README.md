# com.cobalt910.core
Core package for new Unity projects.

For properly working:
- Add this two lines into Packages/mainfest.json file in **dependencies** block.
```javascript
{
    "dependencies": 
    {
        "com.coffee.git-dependency-resolver": "https://github.com/mob-sakai/GitDependencyResolverForUnity.git",
        "com.unity.naughtyattributes": "https://github.com/cobalt910/NaughtyAttributes.git#2.0.1",
        ...
    }
}
```
- You should see compile errors in console after resolving packages.
![](https://image.prntscr.com/image/rfclsMIoTBSWldrOQmWvUw.png)


- Restart Unity.
- After restarting unity display warning message, just ignore it and press **continue** button.
![Image description](https://image.prntscr.com/image/MX0fxBPBQhye6kjBg7fgwA.png)
