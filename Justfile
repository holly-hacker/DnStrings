default:
    @just --list

gen-deps:
    dotnet restore --packages out && nix run nixpkgs#nuget-to-json out > deps.json && rm -r out
