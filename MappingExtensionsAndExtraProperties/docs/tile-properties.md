# Mapping Extensions and Extra Properties (MEEP) Documentation
**Important**: Remember to read the section on [adding the correct keys to your manifest](../readme.md#Adding-MEEP-feature-keys-to-your-manifest) for the specific features you intend to use first.

## Using the CloseupInteraction tile properties
The basic format for `CloseupInteraction` is in the following snippet of an `EditMap` patch using Content Patcher.

```json
{
    "Format": "1.28.0",
    "Changes": [
        {
            // Apply the tile property
            "Action": "EditMap",
            "Target": "Maps/SeedShop",
            "MapTiles": [
                {
                    "Position": {
                        "X": 8,
                        "Y": 18
                    },
                    "Layer": "Buildings",
                    "SetProperties": {
                        "Action": "MEEP_CloseupInteraction_Image LooseSprites/Cursors 540 305 42 28",
                        "MEEP_CloseupInteraction_Text": "The spirits tell me you're learning how to use a new mod..."
                    }
                }
            ]
        }
    ]
}
```
`MEEP_CloseupInteraction_Image` takes a few "arguments". The first, and only mandatory one, is the asset name (which can be a built-in Stardew image).
The second is the region of the specified image you want to be displayed.


For example...
```json
"Action": "MEEP_CloseupInteraction_Image LooseSprites/Cursors 540 305 42 28",
"MEEP_CloseupInteraction_Text": "The spirits tell me you're learning how to use a new mod..."
```
Will display the fortune teller, and a message reading "The spirits tell me you're learning how to use a new mod...".

In `540 305 42 28`, `540` is the x co-ordinate of the top-left corner of the region of the specified image you want to display,
`305` is the y co-ordinate, `42` is the width, and `28` is the height.

**Warning**: It's worth keeping in mind the size of the image, and whether or not it will interfere with Stardew Valley
when running at lower resolutions when combined with the text display option. I recommend you **always test your images
at a varying UI scale settings and window sizes** if you want to play it safe.

### Using the CloseupInteraction reel tile properties
An example of a closeup interaction reel looks like so:
```json
{
    "Changes": [
        {
            // Demonstration of a multiple image reel (option one).
            "Action": "EditMap",
            "Target": "Maps/Town",
            "Update": "OnTimeChange",
            "MapTiles": [
                {
                    "Position": {
                        "X": 24,
                        "Y": 53
                    },
                    "Layer": "Buildings",
                    "SetProperties": {
                        "Action": "MEEP_CloseupInteractionReel",
                        "MEEP_CloseupInteraction_Image_1": "LooseSprites/Cursors 540 305 42 28",
                        "MEEP_CloseupInteraction_Text_1": "The spirits tell me you're learning how to use a new mod...",
                        "MEEP_CloseupInteraction_Image_2": "LooseSprites/Cursors 644 361 42 28",
                        "MEEP_CloseupInteraction_Text_2": "FOOD!",
                        "MEEP_CloseupInteraction_Image_3": "LooseSprites/Cursors 112 656 16 64",
                        "MEEP_CloseupInteraction_Image_4": "LooseSprites/Cursors 160 660 32 60",
                        "MEEP_CloseupInteraction_Text_4": "Weird, pink tentacle spiral thing?"
                    }
                }
            ]
        }
    ]
}
```
The `Action` `MEEP_CloseupInteraction_Image_1`, `MEEP_CloseupInteraction_Image_2`, etc. properties are required, and you cannot
have more `MEEP_CloseupInteraction_Text_1` properties than image ones, as they'll simply be ignored.

In this example, there are four images:

(`MEEP_CloseupInteraction_Image_1`, `MEEP_CloseupInteraction_Image_2` `MEEP_CloseupInteraction_Image_3` `MEEP_CloseupInteraction_Image_3`)

and three descriptions

(`MEEP_CloseupInteraction_Text_1`, `MEEP_CloseupInteraction_Text_2`, `MEEP_CloseupInteraction_Text_4`)

Note how there is no `MEEP_CloseupInteraction_Text_3`.This simply means that when the user switches to the third page,
the image on that page won't have any text beneath it.

Finally, and most importantly, the `Action` tile property with the value `MEEP_CloseupInteractionReel`. This tells MEEP
to treat this like a closeup interaction reel.

### Using the MEEP_CloseupInteraction_Sound tile property
You can spice up your closeup interactions by specifying that a given sound cue be played when
the interaction is opened, or the page is turned in the reel.

```json
{
    "Position": {
        "X": 40,
        "Y": 22
    },
    "Layer": "Buildings",
    "SetProperties": {
        "Action": "MEEP_CloseupInteraction_Image Mods/DecidedlyHuman/MaruRobot",
        "MEEP_CloseupInteraction_Text": "It's Maru's robot! Did someone copy the design?",
        "MEEP_CloseupInteraction_Sound": "dog_bark"
    }
}
```

The sound cue must be valid, or MEEP will log an error every time the property is interacted with, and no sound will be played.

## Using the MEEP_SetMailFlag tile property
This one is fairly self-explanatory. You would add the tile property `DHSetMailFlag`, and the value for it is the mail flag you want to be set. for example:

```json
{
    "Format": "1.28.0",
    "Changes": [
        {
            // Apply the tile property
            "Action": "EditMap",
            "Target": "Maps/SeedShop",
            "When": {
                "HasFlag |contains=DHSeenFortuneTellerImage": false
            },
            "MapTiles": [
                {
                    "Position": {
                        "X": 8,
                        "Y": 18
                    },
                    "Layer": "Buildings",
                    "SetProperties": {
                        "Action": "MEEP_CloseupInteraction_Image LooseSprites/Cursors 540 305 42 28",
                        "MEEP_CloseupInteraction_Text": "The spirits tell me you're learning how to use a new mod...",
                        "MEEP_SetMailFlag": "DHSeenFortuneTellerImage"
                    }
                }
            ]
        }
    ]
}
```

With this example, interacting with the tile will bring up the fortune teller image and message, and set the mail flag `DHSeenFortuneTellerImage`. Whenever Content Patcher refreshes its patches, the interaction to bring up the image and description will vanish. You can specify a custom update rate [as seen here](https://github.com/Pathoschild/StardewMods/blob/develop/ContentPatcher/docs/author-guide.md#how-often-are-patch-changes-applied).

You could also use this for any kind of conditional patch that checks for a mail flag.

## Using the MEEP_AddConversationTopic tile property
This one is also fairly self-explanatory. It sets a conversation topic whenever a tile is interacted with!
```json
{
    "Format": "1.28.0",
    "Changes": [
        {
            // Apply the tile property
            "Action": "EditMap",
            "Target": "Maps/SeedShop",
            "MapTiles": [
                {
                    "Position": {
                        "X": 13,
                        "Y": 18
                    },
                    "Layer": "Buildings",
                    "SetProperties": {
                        "Action": "MEEP_Letter Super Evil Joja Plans^^Ha, fooled you! No plans here.",
                        "MEEP_Letter_Type": "Mods/DecidedlyHuman/JojaLetterBG",
                        "MEEP_AddConversationTopic": "DH.MEEP.SeenEvilJojaLetter 7"
                    }
                }
            ]
        }
    ]
}
```
All you do is add the `MEEP_AddConversationTopic` tile property as you see above. It takes one or two arguments. In:
```
"MEEP_AddConversationTopic": "DH.MEEP.SeenEvilJojaLetter 7"
```

`DH.MEEP.SeenEvilJojaLetter`: This is the conversation topic you want to add.
`7`: This is the number of days the conversation topic will remain active.

## Using the MEEP_FakeNPC tile property
This tile property will allow you to spawn a "fake" NPC on a given tile. Unlike a "real" NPC, which needs a disposition, and lots of setup, a "fake" NPC needs very, very little. Fake NPCs cannot receive gifts, don't have a schedule, and won't move around. They're intended to be a middle ground between a simple static NPC sprite, and a fully-fledged NPC.

The most basic setup is as follows:
```json
{
    "Format": "1.28.0",
    "Changes": [
        {
            "Action": "Load",
            "Target": "Portraits/NotAbigail",
            "FromFile": "assets/NotAbigail/NotAbigailPortrait.png"
        },
        {
            "Action": "Load",
            "Target": "Characters/NotAbigail",
            "FromFile": "assets/NotAbigail/NotAbigail.png"
        },
        {
            "Action": "EditData",
            "Target": "MEEP/FakeNPC/Dialogue/NotAbigail",
            "Entries": {
                "DialogueOne": "Hey @, you think you could build a raft?#$e#I saw a few cool islands on the way here I want to visit."
            }
        },
        {
            "Action": "EditMap",
            "Target": "Maps/Town",
            "MapTiles": [
                {
                    "Position": {
                        "X": 29,
                        "Y": 56
                    },
                    "Layer": "Back",
                    "SetProperties": {
                        "MEEP_FakeNPC": "NotAbigail"
                    }
                }
            ]
        }
    ]
}
```

Firstly, we need to load in a portrait for the NPC. You'll need to set the `FromFile` to point to wherever you have the source image.
```json
{
    "Action": "Load",
    "Target": "Portraits/NotAbigail",
    "FromFile": "assets/NotAbigail/NotAbigailPortrait.png"
}
```
Secondly, we need to load a spritesheet for the NPC.
```json
{
    "Action": "Load",
    "Target": "Characters/NotAbigail",
    "FromFile": "assets/NotAbigail/NotAbigail.png"
}
```

Thirdly, and optionally, we can add some dialogue if we want the NPC to speak. Most dialogue commands should work, but let me know if you run into anything that doesn't work as expected.

For dialogue keys, location-specific keys should work, but keep in mind that the NPC exists only where you put it. You could have the location of the NPC set via the tile property conditional, and have a different dialogue key for each location.
```json
{
    "Action": "EditData",
    "Target": "MEEP/FakeNPC/Dialogue/NotAbigail",
    "Entries": {
        "DialogueOne": "Hey @, you think you could build a raft?#$e#I saw a few cool islands on the way here I want to visit."
    }
}
```

Finally, we need to add a tile property to specify where we want the NPC to spawn.
```json
{
    "Action": "EditMap",
    "Target": "Maps/Town",
    "MapTiles": [
        {
            "Position": {
                "X": 29,
                "Y": 56
            },
            "Layer": "Back",
            "SetProperties": {
                "MEEP_FakeNPC": "NotAbigail"
            }
        }
    ]
}
```

### Optional argument: NPC sprite size
We can also, however, specify a custom NPC sprite size. Yes, that means you can have an NPC larger than 16x32. How to do that is very simple:
```json
{
    "Action": "EditMap",
    "Target": "Maps/Town",
    "MapTiles": [
        {
            "Position": {
                "X": 29,
                "Y": 56
            },
            "Layer": "Back",
            "SetProperties": {
                "MEEP_FakeNPC": "NotAbigail 32 32"
            }
        }
    ]
}
```

This will create a fake NPC with a size of 32x32. Currently, I've only tested 32x32, and 64x64.

Do note, however, that using a custom size for your NPC will disable the breathing animation and shadow. This is primarily because the vanilla shadow is very specifically designed for a 16x32 NPC, and looks very bad on different sizes of NPC.

Instead, you can just draw the shadows into the NPC sprite. This allows for much nicer looking shadows, too!

## Using the MEEP Letter tile property
The `Letter` tile properties are fairly simple. There are two of them -- `MEEP_Letter`, and `MEEP_Letter_Type`.

### MEEP_Letter
This is the mandatory property, and is fairly simple:
```json
{
    "Changes": [
        {
            "Action": "EditMap",
            "Target": "Maps/Custom_MEEP_Station",
            "MapTiles": [
                {
                    "Position": {
                        "X": 13,
                        "Y": 13
                    },
                    "Layer": "Buildings",
                    "SetProperties": {
                        "Action": "MEEP_Letter Super Evil Joja Plans^^Ha, fooled you! No plans here.^^Have some wood, though.%item object 388 50 %%",
                        "MEEP_Letter_Type": "2"
                    }
                }
            ]
        }
    ]
}
```

You just need to specify the tile property on the desired tile, and format the letter contents as you would for any mail/letter.

**Important note**: Most, if not all vanilla mail commands should work here. However, given that the letter can be opened as many times as the player desires as long as the patch remains in place, you want to be very, very careful not to allow for infinite items, and other weird issues.

### MEEP_Letter_Type
There are two ways to use `MEEP_Letter_Type`. One is to simply specify the vanilla letter background type (seen [here](https://i.imgur.com/llJupGQ.png)), *or* specify your own custom background image.

Your custom image *must* follow the vanilla size, however. The file should be 320x180 pixels, but *visually* does not need to fill up the entire image.

#### Vanilla background example
```json
{
    "Position": {
        "X": 13,
        "Y": 12
    },
    "Layer": "Buildings",
    "SetProperties": {
        "Action": "MEEP_Letter Pretend this is a very, very long letter with multiple evil Joja-related plans in it.",
        "MEEP_Letter_Type": "2"
    }
}
```

Note nothing but the specific vanilla letter background ID in the property.

#### Custom background example
Firstly, you need to load your image somewhere in your Content Patcher mod as demonstrated below.
```json
{
    "Action": "Load",
    "Target": "Mods/DecidedlyHuman/JojaLetterBG",
    "FromFile": "assets/letter-bg.png"
}
```

Then, as seen the example below, specify the loaded image in the property.

```json
{
    "Position": {
        "X": 13,
        "Y": 18
    },
    "Layer": "Buildings",
    "SetProperties": {
        "Action": "MEEP_Letter Super Evil Joja Plans^^Ha, fooled you! No plans here.",
        "MEEP_Letter_Type": "Mods/DecidedlyHuman/JojaLetterBG"
    }
}
```
