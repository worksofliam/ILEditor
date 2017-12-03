# [ILEditor](http://worksofbarry.com/ileditor/)

![Welcome Screen](https://i.imgur.com/oIzFcDD.png)

ILEditor is an editor for development of ILE applications on IBM i. ILEditor will support development with any ILE language, be it CL, RPG, COBOL, C or C++. Error listing and syntax highlighting is available for all ILE languages.

## Features

* Easy UI
* Source member editing (+ browsing & diff view)
* Inline compiling
* Inline error listing
* Multiple system configurations
* Basic RPG fixed-to-free
* ILE syntax highlighting
* Store members locally & search locally

## Installation

* You'll be able to install from the ClickOnce installer, which will also prompt you when an update is available. [Download here](http://worksofbarry.com/ileditor/installer/setup.exe).
* Build from source. Clone from GitHub, open the project into Visual Studio and build from there.

## Notes

This does manipulate source members. Currently, saving a source member will set every record's SRCDAT value to `0`. Please raise an issue if this is a major stopping point for you using ILEditor.