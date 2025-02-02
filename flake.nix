{
  description = "DnStrings - a .NET string finder tool";

  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs/nixos-unstable";
    flake-utils.url = "github:numtide/flake-utils";
  };

  outputs = { nixpkgs, flake-utils, ... }:
    flake-utils.lib.eachDefaultSystem (system:
      let
        pkgs = import nixpkgs { inherit system; };
      in
      {
        packages.default = pkgs.buildDotnetModule {
          name = "dnstrings";
          src = ./.;

          dotnet-sdk = pkgs.dotnetCorePackages.sdk_9_0;
          dotnet-runtime = pkgs.dotnetCorePackages.runtime_9_0;
          # dotnet restore --packages out && nix run nixpkgs#nuget-to-json out > deps.json && rm -r out
          nugetDeps = ./deps.json;
        };

        # apps.default = {
        #   type = "app";
        #   program = self'.packages.default;
        # };
      }
    );
}
