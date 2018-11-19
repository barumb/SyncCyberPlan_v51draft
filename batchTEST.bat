rem cd C:\SAGE\SAGEX3V6\scriputility\SyncX3As400_ArticoliNOPF
Console.exe STD LAST=5 IM RI articoliNOPF.txt
Console.exe STD LAST=5 MP RI articoliNOPF.txt

rem Console.exe STD LAST=30 UT RI articoliNOPF.txt

Console.exe STD LAST=5 MR RI           articoliNOPF.txt
Console.exe STD LAST=5 ME RI           articoliNOPF.txt
Console.exe STD LAST=5 MK RI           articoliNOPF.txt

rem ATTENZIONE il segno percentuale va raddoppiato per escaping
Console.exe STD LAST=5 SL RI COD=WM%% articoliNOPF.txt
Console.exe STD LAST=5 SL RI COD=WP%% articoliNOPF.txt
Console.exe STD LAST=5 SL RI COD=WN%% articoliNOPF.txt
Console.exe STD LAST=5 SL RI COD=WV%% articoliNOPF.txt
Console.exe STD LAST=5 SL RI COD=WI%% articoliNOPF.txt
rem Console.exe STD LAST=5 SL RI COD=TC%% articoliNOPF.txt
Console.exe STD LAST=5 SL RI COD=S%% articoliNOPF.txt
rem semilavorati composti
rem Console.exe STD LAST=5 SL RI COD=WS%% articoliNOPF.txt
rem Console.exe STD LAST=5 SL RI COD=WC%% articoliNOPF.txt
rem Console.exe STD LAST=5 SL RI COD=WA%% articoliNOPF.txt
rem Console.exe STD LAST=5 SL RI COD=WH%% articoliNOPF.txt

rem Console.exe STD LAST=5 SL IN COD=WS%% articoliNOPF.txt
rem Console.exe STD LAST=5 SL IN COD=WC%% articoliNOPF.txt
rem Console.exe STD LAST=5 SL IN COD=WA%% articoliNOPF.txt
rem Console.exe STD LAST=5 SL IN COD=WH%% articoliNOPF.txt