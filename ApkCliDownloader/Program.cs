using GooglePlayStoreApi;
using GooglePlayStoreApi.Model;
using System;
using System.CommandLine;
using System.Reflection;
using System.Xml.Linq;

// https://github.com/kagasu/GooglePlayStoreApi/blob/87e4cf9eedd0cbec2e8b7b90959f69edacddf7df/GooglePlayStoreApi/GooglePlayStoreClient.cs#L18
static void SetTimeout(TimeSpan timeout)
{
    var httpClient = (HttpClient)typeof(GooglePlayStoreClient).GetField("client", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
    httpClient.Timeout = timeout;
}

var emailOption = new Option<string>(new[] { "-e", "--email" }, "Email address") { IsRequired = true };
var deviceOption = new Option<string>(new[] { "-d", "--device-id" }, "Android device id") { IsRequired = true };
var userAgentOption = new Option<string>(new[] { "-ua", "--user-agent" }, "User agent for request") { IsRequired = true };
var countryOption = new Option<CountryCode>(new[] { "-c", "--country" }, "County");
var tokenOption = new Option<string>(new[] { "-t", "--token" }, "Authentication token") { IsRequired = true };
var packageNameOption = new Option<string[]>(new[] { "-a", "--package-name" }, "Package name (can be multiply)") { IsRequired = true, AllowMultipleArgumentsPerToken = true };
var outputPathOption = new Option<DirectoryInfo>(new[] { "-o", "--output" }, "Output dir");

var rootCommand = new RootCommand("Sample app for downloading apk files")
{
    emailOption,
    deviceOption,
    userAgentOption,
    countryOption,
    tokenOption,
    packageNameOption,
    outputPathOption
};

rootCommand.SetHandler(async (email, device, userAgent, country, token, packageNames, outputPath) =>
    {
        var client = new GooglePlayStoreClient(email, device, userAgent);
        SetTimeout(Timeout.InfiniteTimeSpan);
        client.Country = country;
        await client.GetGoogleAuth(token);

        foreach (var packageName in packageNames)
        {
            var appDetail = await client.AppDetail(packageName);
            var versionCode = appDetail.Item.Details.AppDetails.VersionCode;
            var versionString = appDetail.Item.Details.AppDetails.VersionString;
            var offerType = appDetail.Item.Offer[0].OfferType;

            await client.Purchase(packageName, offerType, versionCode);
            var bytes = await client.DownloadApk(packageName);

            var apkName = $"{packageName}_{versionString}.apk";

            var path = Path.Combine(outputPath.FullName, apkName);

            File.WriteAllBytes(path, bytes);
            await Console.Out.WriteLineAsync(apkName);
        }
    },
    emailOption, deviceOption, userAgentOption, countryOption, tokenOption, packageNameOption, outputPathOption);

return await rootCommand.InvokeAsync(args);