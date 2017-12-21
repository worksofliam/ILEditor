# [ILEditor](http://worksofbarry.com/ileditor/)

![Welcome Screen](https://i.imgur.com/UQdSjut.png)

ILEditor is an editor for development of ILE applications on IBM i. ILEditor will support development with any ILE language, be it CL, RPG, COBOL, C or C++. Error listing and syntax highlighting is available for all ILE languages.

## Features

* Source member editing (+ browsing & diff view)
* Inline compiling & error listing
* Multiple system configurations
* Basic RPG fixed-to-free & CL Formatting
* ILE syntax highlighting
* Store members locally & search locally
* Integrated spool file listing
* Light and dark modes (dark mode in the image above)

## Installation

* You can install from the ClickOnce installer which will also prompt you when an update is available. [Download here](http://worksofbarry.com/ileditor/installer/setup.exe).
* Build from source. Clone from GitHub, open the project into Visual Studio and build from there.

## Libraries used

* [flaticon](https://www.flaticon.com/authors/simpleicon)
* [AvalonEdit](https://github.com/icsharpcode/AvalonEdit)
* [tabcontrol-extra](https://github.com/tradewright/tabcontrol-extra)

## Notes

This does manipulate source members. Currently, saving a source member will set every record's SRCDAT value to `0`. Please raise an issue if this is a major stopping point for you using ILEditor.