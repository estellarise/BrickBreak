## Notes for Dev: Release Instructions
1. Navigate to folder in command window
2. Run ```dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true --self-contained```
- Note single file publish not recommended for large games. See [MonoGame Documentation](https://monogame.net/articles/packaging_games.html).
- For alternatives, see discussion [here](https://www.reddit.com/r/monogame/comments/p2fa03/monogame_deployment_with_tidy_file_structure). Look into [NetBeauty](https://github.com/nulastudio/NetBeauty2).
3. Zip C:\Users\tsaim\source\repos\\\<GameTitle>\\\<GameTitle>\bin\Release\net6.0\win-x64\publish
4. Upload zipped folder to Itch!
