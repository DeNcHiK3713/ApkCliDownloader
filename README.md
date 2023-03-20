# ApkCliDownloader

## Download latest [release](https://github.com/DeNcHiK3713/ApkCliDownloader/releases/latest)

## Setup
Get auth_token from this [URL](https://accounts.google.com/EmbeddedSetup)
![](https://i.imgur.com/80MLpoR.png)

## Usage

```bash
ApkCliDownloader

Description:
  Sample app for downloading apk files

Usage:
  ApkCliDownloader [options]

Options:
  -e, --email <email> (REQUIRED)                              Email address
  -d, --device-id <device-id> (REQUIRED)                      Android device id
  -ua, --user-agent <user-agent> (REQUIRED)                   User agent for request
  -c, --country                                               County
  <Brazil|Bulgarian|China|Czech|Denmark|Finland|France|Germa
  ny|Greece|Hungary|Indonesia|Italy|Japan|Korea|Lithuania|Ne
  therlands|Norway|Philippines|Poland|Portugal|Russia|Slovak
  ia|Spain|Sweden|Taiwan|Thailand|Turkey|UnitedKingdom|Unite
  dStates|Vietnam>
  -t, --token <token> (REQUIRED)                              Authentication token
  -a, --package-name <package-name> (REQUIRED)                Package name (can be multiply)
  -o, --output <output>                                       Output dir
  --version                                                   Show version information
  -?, -h, --help                                              Show help and usage information
```


## Credits
https://github.com/kagasu/GooglePlayStoreApi/