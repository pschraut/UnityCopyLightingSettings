# Changelog
All notable changes to this package are documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.5.0] - 2020-12-11
### Added
 - Added support to "copy" the "Sun Source" setting too.
   "Sun Source" is a reference to a Light Component in the scene, 
   but Unity does not support cross scene references. 
   Therefore the tool does not copy&paste the reference, 
   but it looks for a Light with the same name (upper/lower-case ignored) in the target scene. 
   It looks first for active lights, then inactive lights.
   If a Light with the same name exists in the target scene and there is 
   currently no "Sun Source" assigned, then the tool sets it to the Light found in that scene.

## [1.4.0] - 2020-12-10
### Added
 - Support for Unity 2020.1 and newer. In 1.3.0 I removed Unity 2020 support on purpose, because they added the "Lighting Settings" asset. However, there are still some settings that have not been moved to the new lighting settings asset (see [this forum post](https://forum.unity.com/threads/copy-lighting-settings-from-scene-to-scene.308634/page-2#post-6606286)). This this package is still relevant, even for Unity 2020 and newer. This package copies the "Lighting Settings" asset reference and the "Environment" settings which are still part of the scene.

## [1.3.0] - 2020-07-05
### Added
 - Availability as an Unity package.
 - Moved code from bitbucket snippet to github repo.

## [1.2.0] - 2019-06-29
### Added
 - Added functionality to paste settings in all open scene, rather than the active scene only.

## [1.1.0] - 2018-07-06
### Fixed
 - Fixed compile error when using Unity 2018.2

## [1.0.0] - 2017-10-30
 - First public release
