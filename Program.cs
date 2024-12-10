// See https://aka.ms/new-console-template for more information

using AdventOfCode2024;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
IConfiguration config = builder.Build();

var inputPath = config["input_directory"]!;

new AdventRunner(inputPath, 10);