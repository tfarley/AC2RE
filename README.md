# AC2RE

## Info

- This is an RE effort, and not intended or designed to be an industrial-grade server emulator at this point. The server is meant for testing and validating behavior and RE assumptions.
- All RE is based on the latest AC2 retail client and pcaps as of official shutdown in 2017.
- Everything here was built from scratch as a personal project for fun/learning using the AC2 client, PDB, retail pcaps from the community, traditional RE tools, and a lot of persistence. Since this is a hobby project, it's not following the most rigorous coding or VCS practices, but things are kept as tidy as possible.

## Features

- (Nearly) complete class definitions and parsing for the network protocol and data files including game data, art assets, WSL data structures and scripts with disassembly, etc.
- Tools for extracting parsed DAT files, viewing/searching/processing packet captures, dumping PDB information in a more processable format, simple model viewing, and rendering a terrain heightmap.
- Barebones server emulator with database persistence, character creation, inventory management, chat, and extremely rudimentary skills and attack actions.

## Overview of Projects

| Project | Purpose |
| ------ | ------ |
| AC2RE.Definitions | Data access and mapping classes for AC2. This project is intended to be reusable across different tools and programs as a common interface to AC2 serialized and runtime data formats. |
| AC2RE.Server | An AC2 server emulator. |
| AC2RE.DatTool | Tool for inspecting and extracting AC2 DAT files. |
| AC2RE.PacketTool | Tool for inspecting and processing AC2 packet captures. |
| AC2RE.UICommon | Generic UI utilities that tools depend on. |
| AC2RE.Renderer | Renderer for AC2 models. |
| AC2RE.RenderLib | Generic rendering utilities that the renderer depends on. |
| AC2RE.Utils | Generic utilities that most projects depend on. |

## Building

Building the solution with Visual Studio 2022 should work out of the box, though most projects depend on a few NuGet packages being installed.

###### For PdbTool specifically

Both building and running requires global registration of the DIA SDK provided with Visual Studio, or else you will get "Warning MSB3284":
```
regsvr32 "C:\Program Files\Microsoft Visual Studio\2022\Community\DIA SDK\bin\amd64\msdia140.dll"
```

## Server Setup

###### Set paths to copies of client DAT files

The hardcoded file paths inside the "ContentManager" constructor must be updated to point to _copies_ of the DAT files that the client uses.

> Note: The AC2 client opens DAT files in exclusive mode, so separate DAT files are needed for the server and each client process, unless you hack the client to open in non-exclusive mode.

###### Setup MySQL

A convenient batch script to run the MySQL daemon on Windows is provided below, which can be saved and run within the MySQL installation directory. This configuration is insecure and should only be used for local testing of databases containing only non-sensitive AC2RE data.
```
@echo off

rem Prepend bin directory to the path
@set PATH=%~dp0bin;%PATH%

rem Set the data directory
rem An optional parameter can be supplied to the batch file that is appended as a suffix, e.g. "db.bat 2" to use "data2", which allows for easy switching between isolated data sets
@set DATADIR=%~dp0data%1

rem Initialize data directory if it doesn't exist
if not exist "%DATADIR%\" mysqld --datadir="%DATADIR%\\" --initialize-insecure --console

rem Start MySQL
mysqld --datadir="%DATADIR%\\" --console --secure-file-priv="" --general-log=ON
```

> Note: The server currently assumes a MySQL database is running on localhost, with a user named "root" having Read/Write/DDL permissions - this is the default configuration for MySQL installs. This user is also assumed to not have a password, but if there is one it can be provided in the "MySqlConnectionStringBuilder".

> Note: The necessary databases and tables will be automatically created by migration scripts in the server after the server connects to the database. This also means you can drop all databases manually and simply restart the server for a clean slate.

## Client Setup

There is currently no secure authentication or external configuration support in the server, so for now you can connect to the hardcoded port 7777 via the simple command line:
```
AC2client.exe --host 127.0.0.1 --port 7777 --rodat on --account myuser --glsticketdirect mypassword
```

Accounts are automatically created on first-use.

## Populating the Map Database

The server migration task "LoadMapMigration" will load an optional CSV containing static map object data into the database.

To export these objects from pcaps, use PacketTool. Within the Tools menu option, choose or enter the path to the top level folder of pcaps. Then run the "GenerateMapDb" tool. This will take a bit to scan the pcaps and generate a "map_obj.csv" file which can be placed in the MySQL data directory (if using the batch script above, this would be mysql/data#). On server startup, "LoadMapMigration" will attempt to load this CSV file into the database.

If you have already run the server/migrations beforehand without the CSV, you can use a MySQL session to manually remove the "LoadMapMigration" row from the ac2re_migration/migration table and restart the server to reattempt the load.

## Hints

**Preventing world save:** The server automatically saves the world to the database on process close, but for certain testing it is often desirable to not save. You can avoid saving by running the server via the Visual Studio debugger and closing it via the Stop Debugging button.

**In-game chat commands:** See "CommunicationMessageProcessor".

**Fast iteration:** Visual Studio hot reload is extremely useful for iteration, since starting the server/client(s) is currently somewhat slow.

## Major TODOs

###### Project

- Very little documentation at this point

###### Definitions

- A general sweep of the code to double-check the accuracy of all names and parsing logic
- Many WSL heap objects' fields have more specific types, but are just using the underlying primitive type (e.g. uint) in the definition classes

###### Server

- External configuration loading
- Secure account management/logins
- Periodic world saving
- Improved startup time (content caching)

###### Database

- Likely needs an overall schema redesign
- Persistence of more runtime data
- Loading of world NPCs

###### Tools

- Verifying WSL opcode disassembly accuracy
- WSL decompilation for readability
- Better DAT browsing/extracting/caching
- More tools for processing pcaps

###### Gameplay

- More useful chat commands
- Landblock timeout/cleanup
- Combat
- Enemies
- Quests
- Fellowships/etc.
- Trading
- Pretty much everything else
