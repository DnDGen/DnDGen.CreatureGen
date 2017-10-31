 
echo "Deploying CreatureGen to NuGet"

ApiKey=$1
Source=$2

echo "Nuget Source is $Source"
echo "Nuget API Key is $ApiKey (should be secure)"

echo "Listing bin directory"
for entry in "./CreatureGen/bin"/*
do
  echo "$entry"
done

echo "Packing CreatureGen"
nuget pack ./CreatureGen/CreatureGen.nuspec -Verbosity detailed

echo "Pushing CreatureGen"
nuget push ./CreatureGen.*.nupkg -Verbosity detailed -ApiKey $ApiKey -Source $Source
