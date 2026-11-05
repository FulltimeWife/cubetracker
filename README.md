# Cube Tracker

A lightweight overlay for **Torchlight: Infinite** that reads the game's log file and displays various stuff related to Cubert:

- Per Loot Tile Tracking (mostly, R & SR, SSR & UR are flagged the same (from what I could see))
- How many times your pet has proc'd
- Greed Success/Fail Rate
- Per Greed Stats (single greed -> claim; double greed -> claim; single greed -> fail)
- Total Cubes done

With the **per greed stats** currently it works like so:

Single Greed (win) -> claim: Single Greed +1, Cubes done +1 <br>
Single Greed (win), Double Greed (fail) -> Single Greed +1, Greed Failure +1 <br>
Single Greed (win), Double Greed (win) -> Single Greed +1, Double Greed +1 <br>
Single Greed (fail) -> Greed Failure +1 <br>

The reason this is designed this way, is so that people can see how many times they've actually won in their respective greeds.

Even though a double greed implies the existence of a single greed win, I've opted for the tracker to show it as is.

---

## Download and Install

1. Download `CubeTracker.exe` from the [latest release](../../releases/latest).
2. Save the file into its own folder, e.g `D:\TLITools\ORTracker`.

- **Do not save the folder into `C:\Program Files\`.**

3. Run the executable.

- If windows says "this file is untrusted", it's because it's not signed, just click run anyway.

4. Upon launch, the following is generated:

- `config.json` This file controls the config of the tracker & is what determines what you see, it's also what the context menu reads from (right click the tracker).
- When the tracker detects the first cube interaction, then `cube_counts.json` is generated, this file contains the actual statistics recorded thus far.

## Configuration

By default the tracker displays all of the information available, you can right click to bring up a context menu which displays the following:

- `View Layout`, this allows you to control which sections you see, All, Greeds Only or Loot Only.
- `Loot Visibility`, this allows you to pick & choose which loot tiles get shown, so you can show/hide which ones you care about/don't
- `Statistics Visibility`, this allows you to toggle the statistics on/off, currently there's only the greed average, but when more are added, it'll function the same as Loot Visibility.
- `Always on Top`, this allows the tracker to stay ontop of windows rather than going behind them when the application behind is clicked.

## FAQ

### Where are the log files stored?

By default the log files are stored `C:\Program Files (x86)\Steam\steamapps\common\Torchlight Infinite\UE_game\TorchLight\Saved\Logs\UE_game.log`

### Is this safe to use?

There's two questions here, to which both answers are yes

- If you are asking is this bannable? No, it's not, it just reads from the log files.
- If you are asking if your data is being sent somewhere? No, it's not, the entire tracker is offline, there's no telemetry or user data being sent anywhere, I cannot see anything you do with the tracker, nor does the tracker report any data to me.

# Disclaimers

**This is a work in progress, for fun program, if stuff doesn't work or there's bugs or something, don't pitchfork me over it**

**I am not affiliated with XD Entertainment in any way, I do not own the rights for the assets used in the tool. All rights belong to XD Entertainment**
