# Change Log
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/) 
and this project adheres to [Semantic Versioning](http://semver.org/).

## Unreleased
- Migrate to .NET Core, but only NON-Desktop modules.

## [5.0.0.0] - 2019-Q1
### Added
- Restrictions and limitations when you sync files and folders based on 
[the one from MS](https://support.office.com/en-us/article/restrictions-and-limitations-when-you-sync-files-and-folders-7787566e-c352-4bd4-9409-fd100a0165f6)
and [eFolder Anchor](https://support.efolder.net/hc/en-us/articles/115010459367-Anchor-What-Files-and-Extensions-are-Excluded-from-the-Sync-Process)
and [Synology](https://forum.synology.com/enu/viewtopic.php?t=135386).
- Troubleshooter tab features.
- Extensions:
  - File content cache.
  - Node metadata cache.

### Changed
- User env: .NET framework 4.7.2 is required now
- Dokany driver usage instead of CBFS

### Fixed
- Save documents flow heisenbug for 3rd party apps, e.g. MS Office files.

### Removed
- ``Overall cleanup``
