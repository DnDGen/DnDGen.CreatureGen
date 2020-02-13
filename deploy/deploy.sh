 
echo "Deploying DnDGen.CreatureGen to NuGet"

ApiKey=$1
Source=$2

echo "Nuget Source is $Source"
echo "Nuget API Key is $ApiKey (should be secure)"

echo "Pushing DnDGen.CreatureGen"
dotnet nuget push ./DnDGen.CreatureGen/bin/Release/DnDGen.CreatureGen.*.nupkg --api-key $ApiKey --source $Source --skip-duplicate
