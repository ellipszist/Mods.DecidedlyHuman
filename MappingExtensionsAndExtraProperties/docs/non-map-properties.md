# Mapping Extensions and Extra Properties (MEEP) Documentation
**Important**: Remember to read the section on [adding the correct keys to your manifest](../readme.md#Adding-MEEP-feature-keys-to-your-manifest) for the specific features you intend to use first.

Note that some features (currently event commands) do *not* require any keys in your manifest.

### Spawning farm animals

To spawn farm animals with MEEP, you'll be editing one of MEEP's data models using CP's `EditData` property. If you're unsure of how to do this, you can find a link to the `EditData` section of CP's documentation near the top of the [main readme here](../readme.md#Using-the-features).

Here's an example of an edit that will spawn two farm animals without portraits, and one with a portrait:

```json
{
    "Changes": [
        {
            "Action": "EditData",
            "Target": "MEEP/FarmAnimals/SpawnData",
            "Entries": {
                "DH.TilePropertyTestMod.BrownCowFarmHouse-LinusPortrait": {
                  "AnimalId": "Brown Cow",
                  "LocationId": "FarmHouse",
                  "DisplayName": "Animal Two Name",
                  "PortraitTexture": "Portraits/Linus",
                  "PetMessage": [
                      "Sad Linus!$s#$b#",
                      "Happy Linus!$h"
                  ],
                  "HomeTileX": 6,
                  "HomeTileY": 6,
                  "Condition": ""
                },
                "DH.TilePropertyTestMod.WhiteChickenFarmHouse": {
                    "AnimalId": "White Chicken",
                    "Age": 100,
                    "LocationId": "FarmHouse",
                    "DisplayName": "Animal One Name",
                    "PetMessage": [
                        "I'm a very old chicken, so you won't get an UwU from me!"
                    ],
                    "HomeTileX": 4,
                    "HomeTileY": 4,
                    "Condition": ""
                },
                "DH.TilePropertyTestMod.BrownCowFarmHouse": {
                    "AnimalId": "Brown Cow",
                    "LocationId": "FarmHouse",
                    "Age": 0,
                    "DisplayName": "Animal Two Name",
                    "PetMessage": [
                        "MUwU",
                        "What? I'm a cow, what else would I say?",
                        "Certainly not \"moo\"!"
                    ],
                    "HomeTileX": 5,
                    "HomeTileY": 5,
                    "Condition": ""
                }
            }
        }
    ]
}
```

#### A closer look at a farm animal without a portrait
In this case, we're adding three animals to `MEEP/FarmAnimals/SpawnData`. Each one is separated with a comma like many different things you would patch with CP.
Let's look at one in isolation.

```json
"DH.TilePropertyTestMod.BrownCowFarmHouse": {
    "AnimalId": "Brown Cow",
    "LocationId": "FarmHouse",
    "Age": 0,
    "DisplayName": "Animal Two Name",
    "PetMessage": [
        "MUwU",
        "What? I'm a cow, what else would I say?",
        "Certainly not \"moo\"!"
    ],
    "HomeTileX": 5,
    "HomeTileY": 5,
    "Condition": ""
}
```

* `"DH.TilePropertyTestMod.BrownCowFarmHouse"` is the spawn ID for the animal. This needs to be 100% unique per animal spawn, so it's recommended that you use the format `YourName.YourMod.AnimalType`. You can also do, for example, `YourName.YourMod.Animal1` if you plan on spawning multiple of the same animal. Just increment the number at the end!
* `"AnimalId": "Brown Cow"`: This is the internal animal ID of the animal. In this case, we're spawning a vanilla white chicken, and its internal ID is `White Chicken`.
* `"Age": 0`: This is the age of the animal!
* `"LocationId": "FarmHouse"`: This is the location name. For example, `SeedShop` is Pierre's shop, `JoshHouse` is Alex's house, and `ScienceHouse` is Robin's house.
* `"DisplayName": "Animal Two Name"`: This is currently unused, but feel free to add it for the future when the name will be displayed alongside the pet message.
```json
"PetMessage": [
    "MUwU",
    "What? I'm a cow, what else would I say?",
    "Certainly not \"moo\"!"
]
```

This is a JSON list of messages to be displayed when the player interacts with a farm animal. In the above example, there are two entries. Just one entry will work, but you can have as many as you like.

```json
"HomeTileX": 5,
"HomeTileY": 5
```

These are the tiles the animal spawns on. They will wander around as usual, however.

```json
"Condition": ""
```

The condition field is a [Game State Query](https://stardewvalleywiki.com/Modding:Migrate_to_Stardew_Valley_1.6#Game_state_queries). The animal will only spawn if this condition is true, so you can have animals that only spawn in the sun, in the rain, or any other number of things supported by the game.

#### A closer look at the changes for farm animal with a portrait
For a farm animal with a portrait, there are a few primary things to note.
1) You need to specify a portrait via `"PortraitTexture": "Portait/Image/Key/Here"`. In this case, it gets Linus's portrait, but you can use any loaded image that matches the format of an NPC portrait.
2) You can use *some* dialogue commands in farm animals with a portrait. Portrait commands *do* work. Questions *absolutely **do not** work currently*. **DO NOT** use them. Players will need to restart the day if they get a dialogue question in your farm animal's dialogue.

```json
"DH.TilePropertyTestMod.BrownCowFarmHouse-LinusPortrait": {
    "AnimalId": "Brown Cow",
    "LocationId": "FarmHouse",
    "DisplayName": "Animal Two Name",
    "PortraitTexture": "Portraits/Linus",
    "PetMessage": [
        "Sad Linus!$s#$b#",
        "Happy Linus!$h"
    ],
    "HomeTileX": 6,
    "HomeTileY": 6,
    "Condition": ""
}
```
### Making trees invulnerable

This allows you to mark your own custom wild trees or fruit trees as invulnerable, so they can't be destroyed by axes or bombs.

**Be extremely careful with this**. If you make a custom wild tree or fruit tree invulnerable and plantable, you could end up getting the player in a bad situation that could involve you helping them edit your mod to temporarily disable the tree invulnerability. **This is exclusively intended for trees that spawn naturally on a map that the player won't be able to plant**. You have been warned.

Now we have that warning out of the way, let's go over the basic syntax for it.

You need to add a custom field to your tree/fruit tree model as follows:

```json
"CustomFields": {"DH_MEEP_Invulnerable_Tree": ""}
```

That's really all there is to it. Since it's so highly recommended you **only** add this to a custom tree designed purely to be spawned in a map, I won't be going over how to edit this custom field into an existing tree. I don't want to encourage or help with something that could lead to problems for users!

### Spawning coloured slimes in events

This event command will allow you to spawn coloured slimes in your events. The syntax is fairly simple, though every argument is required.

This assumes familiarity with event commands in Stardew Valley. To get up to speed, consider reading through the [event page](<https://stardewvalleywiki.com/Modding:Event_data>) on the official Stardew Valley wiki. For more of a walkthrough style, [this guide by Thylak](<https://stardewmodding.wiki.gg/wiki/Events_for_Everyone>) will give you all you need.

In the following example:
`addColouredSlime 14 12 255 89 7`

* `14` is the X tile position you want the slime to spawn on.
* `12` is the Y tile position you want the slime to spawn on.
* `255` is the red hex value for the slime's colour.
* `89` is the green hex value for the slime's colour.
* `7` is the blue hex value for the slime's colour.

So, as a template:

`addColouredSlime tile_x tile_y red_value green_value blue_value`
#### Caveats

It's important to note that the slimes *do* have their typical AI, meaning they will try to jump towards the player if they get close enough. They won't be able to harm the player during the event, but it's something to keep in mind for your map/event design.
