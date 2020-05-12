echo off
rem %~dp0  variabile ambiente con percorso completo posizione del file

robocopy %~dp0  %1 /XF db.lock Syncsolution.log /XD bin obj  /R:2 /W:2 /S /NP /LOG+:%~dp0SyncSolution.log
