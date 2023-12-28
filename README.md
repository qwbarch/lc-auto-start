# lc-auto-start

A **Lethal Company** plugin that automatically starts a LAN game, for development purposes.

## How does it work?

Simply open multiple instances of the game (via the excecutable file, not steam).  
The first client will automatically host a game, while subsequent clients will join the game hosted on the first client.  

Note: You will have to wait 5-10 seconds after launching the first client for the other clients to not get stuck while joining.

## I'm not a mod/plugin developer. Is this still useful for me?

No. If you're a regular player, you'll probably want [SkipToMultiplayerMenu](https://thunderstore.io/c/lethal-company/p/FlipMods/SkipToMultiplayerMenu).

## Configuration

| Name                | Type   | Default value | Description                                     |
|---------------------|--------|---------------|-------------------------------------------------|
| AutoPullLever       | bool   | false         | Automatically pull the lever when hosting games |
| ValidatorServerHost | string | 127.0.0.1     | Server address to host the network validator on |
| ValidatorServerPort | int    | 7777          | Server port to host the network validator on    |

## What is this Network Validator?

In order to check if a LAN game is already hosted, we must ping the server to see if it exists.  
Lethal Company runs on UDP port 7777. Since it's UDP, there's no connection handshake for me to verify a connection.  
This class is a workaround where we host our own TCP server to decide whether we need to host or join the game.

## Does LateCompany work with this plugin?

Unfortunately no. LateCompany doesn't seem to support late joining LAN games.  
If you want to auto-start multiple clients, you will have to keep ``AutoPullLever`` set to ``false``.
