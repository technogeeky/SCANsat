### [**SCANsat**][top]: Real Scanning, Real Science, Warp Speed!
[![][shield:license-bsd]][SCANsat:rel-license]&nbsp;
[![][shield:jenkins-rel]][SCANsat:rel-jenkins]&nbsp;
[![][shield:support-ksp]][KSP:developers]&nbsp;
[![][shield:support-rpm]][RPM:release]&nbsp;
[![][shield:support-ket]][Kethane:release]&nbsp;
[![][shield:support-ors]][ORS:release]

![scan your planetoid like the big boys do][bigmap-scan-10000x]
> ###### **Example SAR scan of Kerbin at 1000x and then 10,000x warp**

**Table of Contents**
------------------------------------------

* [0. People, Facts, and FAQs][0]
  * [a. Facts][0a]
  * [b. FAQs][0b]
* [1. Installation and Interoperability][1]
  * [a. Installation][1a]
  * [b. GameData Layout][1b]
  * [c. Other Add-Ons][1c]
* [2. Types of Scans][2]
  * [a. Altimetry][2a]
  * [b. Biome][2b]
  * [c. Anomaly][2c]
  * [d. Resources][2d]
    * [1. Kethane][2d1]
    * [2. ORS][2d2]
* [3. Basic Usage][3]
  * [a. FAQ: Finding a Good Altitude][3a]
  * [b. Mismatched Scanners][3b]
* [4. Big Map][4]
* [5. Parts and Sensors Types][5]
  * [a. RADAR][5a]
  * [b. SAR][5b]
  * [c. Multi][5c]
  * [d. BTDT][5d]
  * [e. MapTraq (deprecated)][5e]
* [6. (Career Mode) Research and Development][6]
  * [a. Minimum Scan for Science (30%)][6a]
  * [b. Getting Maximum Science][6b]
* [7. Background Scanning][7]
* [8. Time Warp][8]
* [9. Note: Data Sources][9]

**WARNING**:

This add-on is a **work**-in-**progress**.

This means you should expect that it **may not work**, and you should be unsurprised if it **does not progress**.

Disclaimer aside, this add-on is widely used and it **usually** works just fine.

### [:top:][top] 0. People, Facts, and FAQs
------------------------------------------
#### Maintainers 
The current maintainer is:
  + [technogeeky][technogeeky] \<<technogeeky@gmail.com>\>

Maintainers are the people who you should complain to if there is something wrong.

Complaints in various forms are prioritized as follows:

  1. [Pull Requests][SCANsat:pulls] are given the **highest** priority possible. ~ 24 hour response
  2. [Issues][SCANsat:issues] are given *higher* priority than other complaints. ~ 2 day response
  3. [E-Mails][SCANsat:email] will be answered as soon as possible (it's a forwarded list) ~ 3 day response
  4. [Forum Posts][SCANsat:rel-thread] are given a medium priority. ~ 1 week response
  5. [Forum Private Messages](http://forum.kerbalspaceprogram.com/private.php) are given a low priority. We might forget!
  6. [Reddit Posts and PMs][KSP:reddit] are the lowest priority. We often lurk and don't login!

If you submit a well-reasoned pull request, you may even trigger a new release!

#### Authors
The current authors include:
  + [technogeeky][technogeeky] \<<technogeeky@gmail.com>\>
  + [DMagic][DMagic] \<<david.grandy@gmail.com>\>

Past authors include:
  + [damny][damny] \<<missing-in-action@nowhere-to-be-found.com>\>

As of August 2014, the vast majority of code is damny's and DMagic's; and technogeeky and is slowly helping out here and there.

#### Contributors

In addition to the authors, the following people have contributed:
  + (Models, Graphics, Textures) [Milkshakefiend][Milkshakefiend]

#### Licenses

For licensing information, please see the [included LICENSE.txt][SCANsat:rel-license] file.

[Source Code][SCANsat:source] is available, as some licenses may require.

### [:top:][top] a. Facts


### [:top:][top] b. FAQs

  * What does SCANsat do?
    * It allows you to scan planetary bodies for terrain, biome, and resource information and generate various kinds of maps.
  * How does SCANsat affect gameplay?
    * It allows you to see surface details from orbit from an interactive, zoom-able map. This will help you plan your missions (for example, landing near a divider between two or three biomes) and provide critical information you need to attempt a safe landing (for instance, the slope map will help you avoid treacherous hills)
  * Will this version break my existing scans from older versions of SCANsat?
    * **No!** This version is completely backwards compatible, and you current scanning state (which is stored in persistent.sfs) will be safe and sound. Nevertheless, you should make a backup copy of your game before upgading any mod.
  * Do I need to attach a part to my vessel to use SCANsat?
    * **No, but...**. You can view existing maps from any vessel, but you need to attach a scanner to add new data to the maps.
  * [Career Mode] Does SCANsat give us science points?
    * **Yes!** For each type of map, if you scan at least 30% of the surface, you can yse Data for partial science points; up until the maximum value at 95% map coverage.
  * [Career Mode] Is it integrated into the tech tree?
    * **Yes!** This link tells you which nodes unlock which parts in the tech tree.
  * [Contracts] Does SCANsat offer contracts to complete?
    * **No.** This is a planned feature.
  * Can you add <some feature or change> to SCANsat?
    * **Probably!** First, check the issues page to see if it's already been requested. If not, add a new issue. Even better, attempt to add the feature yourself and submit a pull request. We'll catch the bugs for you!



### [:top:][top] 1. Installation and Interoperability 
------------------------------------------
:heavy_exclamation_mark:

#### [:top:][top] a. Installation 
:heavy_exclamation_mark:

  1. Put the SCANsat folder in your KSP installation's GameData folder.
  2. (Optional) Place the SCANsatRPM folder in your KSP installation's GameData folder.

#### [:top:][top] b. GameData Layout 
:heavy_exclamation_mark:

#### [:top:][top] c. Other Add-Ons 

S.C.A.N. is proud to collaborate with other KSP mods and modding teams. Following is a table of all of the mods, add-ons, or software that we interoperate with.

**Built Using** | **Supported By**
:---: | :---:
[![Support for Kethane][kethane:logo]][kethane:release] | [![Support for MKS][usi:logo]][usi:release]
[**OpenResourceSystem**][ors:release] | [![Support for ALCOR][alcor:logo]][alcor:release]
[**RasterPropMonitor**][rpm:release]  | [![Support for Karbonite][karbonite:logo]][karbonite:release]
[**Blizzy78's Toolbar**][toolbar:release] |  [**KSP: Interstellar**][kspi:release]
[**ModuleManager**][mm:release] | [**Extraplanetary Launchpads**][epl:release]


As of the following versions:
* **SCANsat**
  * [x] [**v8.0**][SCANsat:rel-thread] SCANsat Release **version: v8.0**
  * [x] [**v9.0**][SCANsat:dev-thread] SCANsat Dev **version: v9.0**

 **MM**, **RPM**, and **Toolbar** are all **soft** dependencies. This means your experience with SCANsat will be enhanced if you are using these mods, but they are not necessary.

**SCANsat** is built against the following mods:
  * [x] [**MM**][mm:release]: ModuleManager [![][shield:support-mm]][mm:release]
  * [x] [**RPM**][rpm:release]: RasterPropMonitor [![][shield:support-rpm]][rpm:release]
  * [x] via (RPM) <- [**ALCOR**][alcor:release]: ALCOR [![][shield:support-alcor]][alcor:release]
  * [x] [**Toolbar**][toolbar:release] Blizzy's Toolbar [shield:support-toolbar]: http://img.shields.io/badge/for%20Blizzy's%20Toolbar-1.7.6-7c69c0.svg

**SCANsat** generically supports scanning for resources. These resources are powered by **ORS** (included with ZIP) and/or **Kethane** (available [here][kethane:release], patch [here][kethane:patch01]). By working with these two mods, all other resource mod support follows.

* **Resource** Scanning Support
  * [x] [**ORS**][ors:release]: OpenResourceSystem [![][shield:support-ors]][ORS:release]
  * [x] via (ORS) <- [**KSPI**][kspi:release]: Interstellar [![][shield:support-kspi]][kspi:release]
  * [x] via (ORS) <- [**MKS**][usi:release]: USI (MKS/OKS) [![][shield:support-usi]][usi:release]
  * [ ] via (ORS) <- [**OKS**][usi:release]: USI (MKS/OKS) [![][shield:support-usi]][usi:release]
  * [x] via (ORS) <- [**Karbonite**][karbonite:release] (Release) [![][shield:support-karbonite]][karbonite:release]
  * [x] via (ORS) <- [**Karbonite**][karbonite:dev] (Dev) [![][shield:support-karbonite]][karbonite:release]
  * [ ] [**Kethane**][kethane:release] Kethane [![][shield:support-ket-no]][kethane:release]
  * [x] [**Kethane**][kethane:release] Kethane ([**patch**][kethane:patch01] by taniwha) [![][shield:support-ket]][kethane:patch01]
  * [x] via (Kethane) <- [**EPL**][epl:release]: Extra Planetary Launchpads [shield:support-epl]: http://img.shields.io/badge/for%EPL-4.2.3-ff8c00.svg9b9015

> Notes
> + **BOLD**: is there to identify (b-)acronyms we endure
> + **bold** versions are those who we directly match
> + *italics* versions are those which use a mod we support
> + [x] or (checked) means that we build against, test with, inter-operate with, a particular version of this mod.
> + [ ] or (unchecked) means that it may work, but we S.C.A.N. didn't verify


### [:top:][top] 2. Types of Scans 
------------------------------------------
SCANsat supports several different kinds of scans (as opposed to
scanning modules or parts).

As of **v8.0** these include:
  * **RadarLo**: Basic, Low-Resolution RADAR Altimetry (b&w, limited zoom)
  * **RadarHi**: Advanced, High-Resolution RADAR Altimetry (in color, unlimited zoom)
  * **Slope**: Slope Data converted from RADAR data
  * **Biome**: Biome Detection and Classification (in color, unlimited zoom)
  * **Anomaly**: Anomaly Detection and Labeling
  * **Resource**: Scan for chemical or mineral resource on the surface.

Other parts and add-ons are free to include one or more of these kinds of scans. In general,
we would request that similar (same order of magitude) scanning paramters and limitations are used
on custom parts, but this is not a requirement.

#### [:top:][top] a. Altimetry 
:heavy_exclamation_mark:

#### [:top:][top] b. Biome 
:heavy_exclamation_mark:

#### [:top:][top] c. Anomaly 
:heavy_exclamation_mark:

#### [:top:][top] d. Resources 
:heavy_exclamation_mark:

##### [:top:][top] 1. Kethane 
:heavy_exclamation_mark:

##### [:top:][top] 2. ORS 
:heavy_exclamation_mark:



### [:top:][top] 3. Basic Usage
------------------------------------------

Put scanner part on rocket, aim rocket at sky, launch. If your rocket is not pointing at the sky, you are probably not going to map today, because most sensors only work above 5 km.

You can start scanning by selecting a SCANsat part's context menu, enabling the part. Here, you will find a **small map**.

#### [:top:][top] 3a. FAQ: Finding a Good Altitude

Watch the data indicators on the small map to determine how well your scanners are performing.


###### too high
Solid ORANGE means you're too high (and therefore no data is being recorded):
![][small-toohigh]

###### too low
Flashing ORANGE/GREEN means you're too low (and therefore you have a FOV penalty):
![][small-toolow]

###### just right
Solid GREEN means you're in an ideal orbit. Notice the larger swath width on the right:
![][small-justright]

#### [:top:][top] 3b. Mismatched Scanners

In these examples, the SAR and Multi sensors are not very well matched. Because the SAR sensors is ideal above 750km, and becuase it has a large field of view penalty if it's down near the ideal for Multi (250km), these sensors probably should not be used on the same scanner.

BIO and ANOM are ideal, but HI is not! | HI is ideal, but BIO and ANOM are off!
---|---
![][small-mismatch1] | ![][small-mismatch2]

SAR (HI) has thin swaths due to low alt. | Multi and RADAR have similar ideal swaths
--- | ---
![][small-scan-color] | ![][small-scan-bw]

The mapping interface consists of a small-ish map of the planet, as far as it has been scanned in your current game. It scans and updates quickly and shows positions of the active vessel, as well as other scanning vessels in orbit around the same planet. Orbital information is also provided. For a slower but more detailed view, see the **[big map][4]**.

Be sure to remember to pack enough batteries, radioisotope generators, and solar panels. If you forget, you'll run out of electricity, you'll stop recording data, and you'll see useless static:

###### Static! Oh no, adjust the rabbit ears!
![][small-static]

### [:top:][top] 4. Big Map
------------------------------------------
![A Big Big Map][bigmap-anim]

A bigger map can be rendered on demand. Rendered maps are automatically
saved to GameData/SCANsat/PluginData. Note that position indicators for
vessels or anomalies are not visible on exported images (but they may be a future release).

You can mouse over the big map to see what sensors have data for the location, as well as terrain elevation, and other details.

Right-clicking on the big map shows a magnified view around the position where you clicked. Mouse operations work inside this magnified view just like they work outside, meaning the data displayed at the bottom window applies to your position inside the magnified view, and right-clicking inside it will increase magnification. This can be useful to find landing spots which won't kill your kerbals.

### [:top:][top] 5. Parts and Sensor Types
------------------------------------------

| **Part** | **Scan Type** | **FOV** | Altitude (**Min**) | (**Ideal**) | (**Max**) 
|:--- | --- | --- | --- | --- | --- |
| [RADAR Altimetry Sensor][5a] | **RadarLo** / **Slope**| 5 | 5000 m | 5000 m | 500 km
| [SAR Altimetry Sensor][5b] | **RadarHi** | 2 | 5000 m | 750 km | 800 km 
| [Multispectral Sensor][5c] | **Biome** **ANOM** | 4 | 5000 m | 250 km | 500 km 
| [Been There Done That®][5d] | **Anomaly** | 1 | 0 m | 0 m | 2 km
| [MapTraq® (deprecated)][5e] | **None** | N/A | N/A | N/A | N/A 

#### [:top:][top] a. The RADAR Altimetry Sensor
![RADAR][vab-radar]
#### [:top:][top] b. The SAR Altimetry Sensor
![SAR][vab-sar]
#### [:top:][top] c. The Multispectral Sensor
![Multi][vab-multi]
#### [:top:][top] d. Been There Done That
![BTDT][vab-btdt]
#### [:top:][top] e. MapTraq (deprecated)
![MapTraq][vab-maptraq]
 


### [:top:][top] 6. (Career Mode) Research and Development
------------------------------------------

The **RADAR Altimetry** sensor can be unlocked in **Science Tech**.

The **SAR Altimetry** sensor can be unlocked in **Experimental Science**.

The **Multispectral** sensor can be unlocked in **Advanced Exploration**.

The **BTDT** sensor can be unlocked in **Field Science**.


##### [:top:][top] 6a. Minimum Scan for Science
Once you scan at least 30% of a particular map, you can use **Analyze Data** to get delicious science:

![30% is your minimum][science-min]

##### [:top:][top] 6b. Getting Maximum Science
Between 30% and 100%, you will get a number of science points proportional to the percentage. Really,
the upper cutoff is 95% in case you didn't scan the whole map.

![Scan 95% to get all science][science-max]

### [:top:][top] 7. Background Scanning
------------------------------------------

![Note the background scanning (non-active vessels are scanning)][small-scan]

Unlike some other KSP scanning systems, SCANsat allows scanning with multiple
vessels.  All online scanners scan at the same time, but only when your *active vessel* has
**at least one** of the parts included in this mod equipped and the mapping interface is open. 

### [:top:][top] 8. Time Warp
------------------------------------------
SCANsat does not interpolate satellite paths during time warp; nevertheless, due to the relatively large field of view
of each sensor, it's still possible to acquire data faster by time warping. The maximum recommended time warp speed
is currently **10,000x**. Scanning at this warp factor should allow identical scanning performance 
(in terms of [swath width](http://en.wikipedia.org/wiki/Swath_width)) as scanning at *1x*.

As an example of speed, here is a BigMap rendering of a scan at **100x**:
![this is pretty peaceful][bigmap-scan-100x]

And this is a BigMap rendering of the same orbit, but later in the scan. 
It starts at **1000x** and then speeds up to **10,000x**:
![this makes my OCD happy][bigmap-scan-10000x]

Notice that the only gaps in coverage are those at the poles (ie, the selected inclination was not high enough to capture the poles).

### [:top:][top] 9. Note Concerning Data Sources
------------------------------------------
All data this mod shows you is pulled from your game as you play. This
includes:
  * terrain elevation
  * biomes
  * anomiles

SCANsat can't guarantee that all anomalies will be found; in particular, some are so close
to others that they don't show up on their own, and if the [developers][KSP:developers] want to be
sneaky then they can of course be sneaky.

------------------------------------------






[technogeeky]: http://forum.kerbalspaceprogram.com/members/110153-technogeeky
[DMagic]: http://forum.kerbalspaceprogram.com/members/59127-DMagic
[damny]: http://forum.kerbalspaceprogram.com/members/80692-damny
[Milkshakefiend]: http://forum.kerbalspaceprogram.com/members/72507-Milkshakefiend

[KSP:developers]: https://kerbalspaceprogram.com/index.php
[KSP:reddit]: http://www.reddit.com/r/KerbalSpaceProgram


[vab-radar-thumb]: http://i.imgur.com/PrRIcYvs.png 
[vab-sar-thumb]: http://i.imgur.com/4aTTVfWs.png
[vab-multi-thumb]: http://i.imgur.com/byIYXP9s.png
[vab-maptraq-thumb]: http://i.imgur.com/Skrqc8Cs.png
[vab-btdt-thumb]:  http://i.imgur.com/zUmj6USs.png

[vab-radar]: http://i.imgur.com/PrRIcYv.png
[vab-sar]: http://i.imgur.com/4aTTVfW.png
[vab-multi]: http://i.imgur.com/byIYXP9.png
[vab-maptraq]: http://i.imgur.com/Skrqc8C.png
[vab-btdt]:  http://i.imgur.com/zUmj6US.png

[science-min]: http://i.imgur.com/kEj4fz0.gif
[science-max]: http://i.imgur.com/eMtIL5H.gif

[small-scan]: http://i.imgur.com/uVP6Ujs.gif
[small-scan-bw]: http://i.imgur.com/0AbDwKL.gif
[small-scan-color]:  http://i.imgur.com/dlRckBl.gif
[small-static]: http://i.imgur.com/oPN2qIR.gif
[small-nodata]: http://i.imgur.com/0ArIcqj.png

[small-toolow]: https://i.imgur.com/fTDLvw0.gif
[small-toohigh]: https://i.imgur.com/a8YKkXH.gif
[small-justright]: https://i.imgur.com/Oft4xXP.gif
[small-mismatch1]: https://i.imgur.com/fNztoUN.gif
[small-mismatch2]: https://i.imgur.com/aQtTGvV.gif

[bigmap-scan-10000x]: http://i.imgur.com/VEPL3oN.gif
[bigmap-scan-100x]: http://i.imgur.com/bcht47p.gif
[bigmap-anim]: http://i.imgur.com/kxyl8xR.gif

[resource-kethane]: http://i.imgur.com/naJIsvB.gif
[resource-kethane2]: http://i.imgur.com/AT2b4G7.jpg?1
[resource-ors]: http://i.imgur.com/wzhhPRS.png?2
[resource-ors-karbonite]: http://i.imgur.com/Sge2OGH.png?1
[resource-iva]: http://i.imgur.com/iRo4kSA.png
[resource-walkthrough]: http://i.imgur.com/HJLK1yi.gif


[top]: #table-of-contents
[0]: #top-0-people-facts-and-faqs
[0a]: #top-a-facts
[0b]: #top-b-faqs
[1]: #top-1-installation-and-interoperability
[1a]: #top-a-installation
[1b]: #top-b-gamedata-layout
[1c]: #top-c-other-add-ons
[2]: #top-2-types-of-scans
[2a]: #top-a-altimetry
[2b]: #top-b-biome
[2c]: #top-c-anomaly
[2d]: #top-d-resources
[2d1]: #top-1-kethane
[2d2]: #top-2-ors
[3]: #top-3-basic-usage
[3a]: #top-3a-faq-finding-a-good-altitude
[3b]: #top-3b-mismatched-scanners
[4]: #top-4-big-map
[5]: #top-5-parts-and-sensor-types
[5a]: #top-a-the-radar-altimetry-sensor
[5b]: #top-b-the-sar-altimetry-sensor
[5c]: #top-c-the-multispectral-sensor
[5d]: #top-d-been-there-done-that
[5e]: #top-e-maptraq-deprecated
[6]: #top-6-career-mode-research-and-development
[6a]: #top-6aminimum-scan-for-science
[6b]: #top-6b-getting-maximum-science
[7]: #top-7-background-scanning
[8]: #top-8-time-warp
[9]: #top-9-note-concerning-data-sources

[shield:license-bsd]: http://img.shields.io/:license-bsd-blue.svg
[shield:license-mit]: http://img.shields.io/:license-mit-a31f34.svg
 
[shield:jenkins-dev]: http://img.shields.io/jenkins/s/https/ksp.sarbian.com/jenkins/SCANsat-dev.svg
[shield:jenkins-rel]: http://img.shields.io/jenkins/s/https/ksp.sarbian.com/jenkins/SCANsat-release.svg
[shield:jenkins-ket]: http://img.shields.io/jenkins/s/https/ksp.sarbian.com/jenkins/SCANsat-kethane.svg
[shield:jenkins-ors]: http://img.shields.io/jenkins/s/https/ksp.sarbian.com/jenkins/SCANsat-openresourcesystem.svg
[shield:support-ksp]: http://img.shields.io/badge/for%20KSP-v0.23.5%20--%20v0.24.2-bad455.svg
[shield:support-rpm]: http://img.shields.io/badge/for%20RPM-v0.18.2-a31f34.svg
[shield:support-ket]: http://img.shields.io/badge/for%20Kethane-v0.8.8.1-brightgreen.svg
[shield:support-ors]: http://img.shields.io/badge/for%20ORS-v1.1.0-000000.svg
[shield:support-mm]: http://img.shields.io/badge/for%20MM-v2.2.1-40b7c0.svg
[shield:support-toolbar]: http://img.shields.io/badge/for%20Blizzy's%20Toolbar-1.7.6-7c69c0.svg
[shield:support-alcor]: http://img.shields.io/badge/for%20ALCOR-0.9-299bc7.svg
[shield:support-kspi]: http://img.shields.io/badge/for%20Interstellar-0.11-a62374.svg
[shield:support-usi]:http://img.shields.io/badge/for%20USI-0.19.3-34c566.svg
[shield:support-karbonite]: http://img.shields.io/badge/for%20Karbonite-0.1.1-ff8c00.svg
[shield:support-epl]: http://img.shields.io/badge/for%EPL-4.2.3-ff8c00.svg9b9015
[shield:support-ket-no]: http://img.shields.io/badge/for%20Kethane-v0.8.8-red.svg


[shield:gittip-tg-img]: http://img.shields.io/gittip/technogeeky.png
[shield:gittip-tg]: https://www.gittip.com/technogeeky/
[shield:github-issues]: http://img.shields.io/github/issues/technogeeky/SCANsat.svg

[SCANsat:organization]: https://github.com/S-C-A-N
[SCANsat:logo]: http://i.imgur.com/GArPFFB.png
[SCANsat:logo-square]: http://i.imgur.com/GArPFFB.png?1
[SCANsat:issues]: https://github.com/S-C-A-N/SCANsat/issues
[SCANsat:pulls]: https://github.com/S-C-A-N/SCANsat/pulls
[SCANsat:imgur-albums]: https://scansat.imgur.com
[SCANsat:best-orbits-table]: https://www.example.com
[SCANsat:email]: mailto:SCANscansat@gmail.com

[SCANsat:dev-readme]: https://github.com/S-C-A-N/SCANsat/tree/dev/#table-of-contents
[SCANsat:rel-readme]: https://github.com/S-C-A-N/SCANsat/#table-of-contents

[SCANsat:rel-thread]: http://forum.kerbalspaceprogram.com/threads/80369
[SCANsat:dev-thread]: http://forum.kerbalspaceprogram.com/threads/80661

[SCANsat:dev-source]: https://github.com/S-C-A-N/SCANsat/tree/dev
[SCANsat:rel-source]: https://github.com/S-C-A-N/SCANsat

[SCANsat:dev-jenkins]: https://ksp.sarbian.com/jenkins/job/SCANsat-dev/
[SCANsat:rel-jenkins]: https://ksp.sarbian.com/jenkins/job/SCANsat-release/

[SCANsat:dev-jenkins-build-status]: https://ksp.sarbian.com/jenkins/buildStatus/icon?job=SCANsat-dev
[SCANsat:rel-jenkins-build-status]: https://ksp.sarbian.com/jenkins/buildStatus/icon?job=SCANsat-release

[SCANsat:dev-license]: https://github.com/S-C-A-N/SCANsat/blob/dev/SCANsat/LICENSE.txt
[SCANsat:rel-license]: https://github.com/S-C-A-N/SCANsat/blob/release/SCANsat/LICENSE.txt

[SCANsat:rel-dist-curseforge]: http://kerbal.curseforge.com/ksp-mods/www.example.com-SCANsat
[SCANsat:rel-dist-curseforge-zip]: http://kerbal.curseforge.com/ksp-mods/www.example.com-SCANsat/files/latest

[SCANsat:rel-dist-github]: https://github.com/S-C-A-N/SCANsat/releases/tag/www.example.com
[SCANsat:rel-dist-github-zip]: https://github.com/S-C-A-N/SCANsat/releases/download/www.example.com/SCANsat.zip

[SCANsat:rel-dist-kerbalstuff]: http://beta.kerbalstuff.com/mod/www.example.com/SCANsat
[SCANsat:rel-dist-kerbalstuff-zip]: http://beta.kerbalstuff.com/mod/www.example.com/SCANsat/download/www.example.com

[SCANsat:dev-dist-curseforge]: https://www.example.com
[SCANsat:dev-dist-curseforge-zip]: https://www.example.com

[SCANsat:dev-dist-github]: https://github.com/S-C-A-N/SCANsat/releases/tag/www.example.com
[SCANsat:dev-dist-github-zip]: https://github.com/S-C-A-N/SCANsat/releases/download/www.example.com/SCANsat.zip

[SCANsat:dev-dist-kerbalstuff]: http://beta.kerbalstuff.com/mod/www.example.com/SCANsat
[SCANsat:dev-dist-kerbalstuff-zip]: http://beta.kerbalstuff.com/mod/www.example.com/SCANsat/download/www.example.com

[IAT]: http://forum.kerbalspaceprogram.com/threads/75854
[IAT:kerbin-system]: http://forum.kerbalspaceprogram.com/threads/75854?p=#1
[IAT:inner-systems]: http://forum.kerbalspaceprogram.com/threads/75854?p=#2
[IAT:duna-dres]: http://forum.kerbalspaceprogram.com/threads/75854?p=#3
[IAT:jool-system]: http://forum.kerbalspaceprogram.com/threads/75854?p=#4
[IAT:eeloo]: http://forum.kerbalspaceprogram.com/threads/75854?p=#7
[IAT:earth-system]: http://forum.kerbalspaceprogram.com/threads/75854?p=#9

[karbonite:release]: http://forum.kerbalspaceprogram.com/threads/89401
[karbonite:dev]: http://forum.kerbalspaceprogram.com/threads/87335
[karbonite:logo]: http://i.imgur.com/PkewuRD.png

[kethane:release]: http://forum.kerbalspaceprogram.com/threads/23979
[kethane:patch01]: http://forum.kerbalspaceprogram.com/threads/23979?p=1313690&viewfull=1#post1313690
[kethane:logo]: http://i.imgur.com/u952LjP.png?1

[usi:release]: http://forum.kerbalspaceprogram.com/threads/79588
[usi:dev]: http://forum.kerbalspaceprogram.com/threads/72706
[usi:logo]: http://i.imgur.com/aimhLzU.png

[alcor:release]: http://forum.kerbalspaceprogram.com/threads/54925
[alcor:logo]: http://i.imgur.com/7eJ3IFC.jpg

[mm:release]: http://forum.kerbalspaceprogram.com/threads/55219

[epl:release]: http://forum.kerbalspaceprogram.com/threads/59545

[kspi:release]: http://forum.kerbalspaceprogram.com/threads/43839

[ors:release]: http://forum.kerbalspaceprogram.com/threads/64595

[toolbar:release]: http://forum.kerbalspaceprogram.com/threads/60863

[rpm:release]: http://forum.kerbalspaceprogram.com/threads/57603
