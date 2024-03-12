# AutoStart

A **Lethal Company** plugin that automatically starts a LAN game, for development purposes.

## What does this do?

This plugin is made to improve the developer experience for Lethal Company modders.  
All you simply need to do is start up the number of clients you want to test with, and they'll all join the same lobby!

To watch a preview of how it works, click on the video below:  
[![AutoStart Preview](https://img.youtube.com/vi/F15LVSVIWNU/0.jpg)](https://www.youtube.com/watch?v=F15LVSVIWNU)

## I'm not a mod/plugin developer. Is this still useful for me?

No. If you're a regular player, you'll probably want [SkipToMultiplayerMenu](https://thunderstore.io/c/lethal-company/p/FlipMods/SkipToMultiplayerMenu).

## Recommended development tools

- [ricky-davis.LC_LaunchProfile](https://github.com/ricky-davis/LC_LaunchProfile) - Makes it easier to launch a specific profile on your mod manager for multiple clients.
- [Hamunii.DevTools](https://thunderstore.io/c/lethal-company/p/Hamunii/DevTools/) - Lots of convenience features such as spawning enemies, teleporting to entrance, etc.
- [dancemoon.DanceTools](https://thunderstore.io/c/lethal-company/p/dancemoon/DanceTools/) - Easy to use enemy/scrap spawning with a built-in console.

## Configuration

| Section       | Key             | Type   | Default value | Description                                                            |
|---------------|-----------------|--------|---------------|------------------------------------------------------------------------|
| AutoStart     | Enabled         | bool   | true          | Whether or not the plugin should be enabled.                           |
| AutoStart     | SaveFile        | string | LCSaveFile1   | The save file to use.                                                  |
| AutoPullLever | Enabled         | bool   | true          | Whether or not the start lever should be automatically pulled.         |
| AutoPullLever | PlayersRequired | int    | 0             | Players required to automatically pull the lever (excluding the host). |

## Changelog

### v1.1.1

- Use FastStartup to skip the initial loading screen animations
- Prevent the config and save file from loading at the same time on multiple processes

### v1.1.0

- Fixed the fullscreen [issue](https://github.com/qwbarch/lc-auto-start/issues/2).
- Fixed the bug where starting multiple clients too fast can cause them to infinitely hang.
- AutoPullLever can now be used for multiple clients.
- Save file can now be configured.

### v1.0.1

- Initial release.
