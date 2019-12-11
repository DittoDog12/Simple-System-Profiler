@echo off

:set vars
  Set wkdir=%~dp0
  Set wkdir=%wkdir:~0,-1%
  
  call "%wkdir%\Simple System Profiler.exe" /t c:\test
  