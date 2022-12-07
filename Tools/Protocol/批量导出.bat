echo on


set Path=ProtoGen\protogen.exe

del ..\..\LockstepClient\Assets\Shared\ProtocolDef\*.* /f/s/q/a 




echo wait...

%Path%  -i:testabc.proto    -o:..\..\LockstepClient\Assets\Shared\ProtocolDef\testabc.cs

%Path%  -i:Battle.proto    -o:..\..\LockstepClient\Assets\Shared\ProtocolDef\Battle.cs

echo ...Completed

pause
