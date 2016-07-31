#!/bin/bash

FILES=
FILES="${FILES} Assembly-CSharp.csproj "
FILES="${FILES} Assembly-CSharp-firstpass.csproj"
FILES="${FILES} Assembly-CSharp-firstpass-vs.csproj"
FILES="${FILES} Assembly-CSharp-vs.csproj"
FILES="${FILES} GamuxCampjam2016.sln"
FILES="${FILES} GamuxCampjam2016.userprefs"
FILES="${FILES} GamuxCampjam2016-csharp.sln"
FILES="${FILES} Library/"

rm -rf ${FILES}
git checkout ProjectSettings/ProjectSettings.asset

