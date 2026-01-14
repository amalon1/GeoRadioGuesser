# GeoRadioGuesser

## What is it?

GeoRadioGuesser is a web based game built in Blazor with .NET 9.0 where the objective is
to guess where a random unidentified radio station from all around the world comes from as
quickly as possible.

## How do you play?

Click start game then you should see your current score, a timer and audio controls for the
station playing as well as a map. Listen to the station for as long as you wish then click a
country on the map and click submit to make your guess. More points are awarded to fast guesses and the top 10 scores appear on the leaderboard!

## How do you run it?

Make sure you have these downloaded:

[.NET 9.0 sdk](https://dotnet.microsoft.com/en-us/download/dotnet/9.0),
[PostgreSQL](https://www.postgresql.org/download/),

Then, for the first time, open this repo and follow these instructions:

1.  Update appsettings.json with your database connection
    e.g. (Host=localhost;Port=5432;Username=postgres;Password=1234)

    Then run the following commands in the terminal:
2. `dotnet tool install --global dotnet-ef`
3. `cd geo-radio`
4. `dotnet ef database update`

Then you can start the game by running `dotnet run`!

## Made with

.NET 9.0
Blazor
PostgreSQL
