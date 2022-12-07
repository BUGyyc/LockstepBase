echo on


set Path=ProtoGen\protogen.exe

del ..\..\LockstepClient\Assets\Shared\ProtocolDef\*.* /f/s/q/a 




echo wait...

%Path%  -i:testabc.proto    -o:..\..\LockstepClient\Assets\Shared\ProtocolDef\testabc.cs

%Path%  -i:Common.proto    -o:..\..\LockstepClient\Assets\Shared\ProtocolDef\Common.cs

%Path%  -i:Battle.proto    -o:..\..\LockstepClient\Assets\Shared\ProtocolDef\Battle.cs

%Path%  -i:PID.proto    -o:..\..\LockstepClient\Assets\Shared\ProtocolDef\PID.cs

echo ...Completed

pause
