# SteamLib
Simple Steam library for C#

## Features
* Checks if Steam is installed;
* Checks if the application is installed by APPID;
* Get users logged in steam;
* Get steam id by username (logged in steam); 

## API

### new Steam() : steam

Checks if Steam is installed.

### steam.GetAppPathById(appId) : string

Checks if the application is installed by APPID and return install path.

### steam.GetUsersName(username) : string[]

Get users logged in steam.

### steam.GetSteamIdByUserName(username) : string

Get steam id by username (logged in steam); 

## Exceptions

### AppNotInstalledException
### ConfigNotFoundException
### SteamNotInstalledException
### UserNotFoundException
