![TycoonKitAd](https://github.com/zSkull162/TycoonKit/assets/70001936/8879a608-e16a-4599-8f8a-35af5c9dbafe)

# Welcome to VRChat TycoonKit!
### A prefab designed to allow world creators to make Roblox-like tycoons more easily in VRChat.
### ---------------------------------------------------------------------------------------------
As the description above suggests, this is a prefab for VRChat which replicates the behaviour and systems found in Roblox Tycoon games. This prefab includes Droppers, Upgraders, Collectors, Buttons, Conveyors, and a very simple way to create collectable objects. The only required packages for this system are the VRCSDK3 and UdonSharp, but U# comes with the SDK now, so you don't need to install it separately.

This system is the same one found in [my own world, Polytycoon!](https://vrchat.com/home/world/wrld_5e16723e-b5c9-4683-9757-557e69805316) This means that the unlock buttons are fully synced between in-instance players, and late-joiners. While the droppers, upgraders, and currency objects themselves aren't synced, they'll be dropping at about the same time for every player anyways, so you should hardly notice.

The unitypackage includes Mochie's standard shader, as i used it for the materials the prefabs and example scenes use. If you already have Mochie's shaders installed, the unitypackage won't install a second copy.

## Important:
This package was made on <b>Unity 2022.3.6</b>, so if you are trying to use it on <b>Unity 2019.4.31</b>, you will have some minor issues.
When I attempted to use this package in Unity 2019, everything does work, but there's some collider scaling issues for the prefabs. To fix this, simply go through each prefab, and make sure none of the colliders are massive or super small. Otherwise, this package works fine on Unity 2019. This system should also work for Unity 2022.3.22, but I haven't tested that yet.

### ---------------------------------------------------------------------------------------------
# Features
### 1. Droppers
Your basic tycoon dropper. Drops any object you select at a rate you choose.

You can also set the Spawn Delay to 0 if you want to disable the automatic spawning. This is mainly done if you want to make a manual dropper.

![Screenshot 2024-07-10 182119](https://github.com/zSkull162/TycoonKit/assets/70001936/b0918dc1-ebaf-44f2-8d31-aff3186b439c)


### 2. Upgraders
The upgraders have 2 options for upgrading, Add and Multiply. If set to add, they will add the Upgrade Amount to the value of whatever passes through the upgrader. If set to multiply, they multiply a passing object's value by the Upgrade Amount.

You will also notice there's a particle system slot. It's there so that you can have a particle system play whenever an object is upgraded, for some player feedback. It isn't required however, so you can leave the field blank if you don't want particles to show up.

![Screenshot 2024-07-10 182136](https://github.com/zSkull162/TycoonKit/assets/70001936/cdf71761-78a5-4289-a790-e7e4ea49ab3c)


### 3. Unlock Buttons
Unlock buttons are the most important part of a tycoon, besides your money. These unlock buttons allow the player to purchase an object in the world, if they have enough money. The money is also synced between players, therefore somewhat shared, but more on that in the Money Manager section.

These unlock buttons have a few too many features to explain here, but I can assure that they have everything you'll need. There's a full explanation in the "How to Use Scripts" text file, which is at "- zSkull162/TycoonKit/Prefabs"

![Screenshot 2024-08-22 150433](https://github.com/user-attachments/assets/a41c8224-7763-4a63-9767-59f7812c5fed)


### 4. Money Manager
This is another very important part of the tycoon systems. The Money Manager stores a Money value, and will update the selected "Display text" every time the money value changes.

The Money Manager is also referenced by Collectors and Unlock Buttons as you may have noticed, because an unlock button checks the current Money value of the referenced Money Manager to decide whether or not you can buy something, and a collector references a Money Manager so it can add the value of collected currency objects to the Money value.

One of the cool things about this system, is that if you want multilpe tycoons in one world, you can just duplicate the Money Manager and have different buttons reference different Money Managers! The only thing you may want to do, is modify the Unlock Buttons to have some sort of Tycoon Ownership system. Although that may be a bit difficult depending on the user's experience with coding and Udon, so it's not necessary, but know that anyone can buy any button if there isn't an ownership system.

There's also a field for Starting Money. This simply adds whatever you input to the Money value on start.

The Display Text field is also not <i>required</i>, but it wouldn't be that fun to play a tycoon where you can't see how much money you have.

![Screenshot 2024-07-10 182220](https://github.com/zSkull162/TycoonKit/assets/70001936/d20b714b-8973-4545-a1c0-e105658f59da)


### 5. Currency Objects
The final building block to making a roblox-like tycoon game, the currency objects. These are the objects that will roll on conveyors, and be collected by collectors.

To create a currency object is very simple. Get a primitive like a sphere, cube, or get/make a 3D model, whatever. Then add a collider to it, and a Rigidbody. If you're using a box collider for your model, I would reccommend giving the collider the "NoFriction" physics material so that it slides on the conveyor instead of rolls. (This physics material is found in "- zSkull162/TycoonKit")
(I also reccommend you put the obect onto the Pickup or Walkthrough layers, so that players can't interact with them and get spun around.)

![Screenshot 2024-03-18 213315](https://github.com/zSkull162/TycoonKit/assets/70001936/8ff5ee95-9b9d-4512-aa04-aa864b0b36b7)

Finally, you add the Currency Tag script. This is the component that stores a Value variable, which is read by Collectors, so they can add the Value to the money from a money manager. You can actually add this Currency Tag script to anything you want, and if it touches the trigger of a Collector, its Value will be added to the money value of the money manager referenced by the collector.
(Note: This value cannot be negative. If it is, it will be reset to 0 when the game starts.)

![Screenshot 2024-07-10 182250](https://github.com/zSkull162/TycoonKit/assets/70001936/af0385f6-9af9-4b88-aba7-0f3b932e5e15)


### 6. SimpleHUD
This tycoon system also includes a version of [SimpleHUD](https://github.com/zSkull162/SimpleHUD/tree/main), a HUD system made by me. If you want to use this HUD system by itself, it can be downloaded by clicking the previous blue text, or from here: https://github.com/zSkull162/SimpleHUD/tree/main


### ---------------------------------------------------------------------------------------------
## That's all for this ReadMe.
There's more features in the prefab that weren't mentioned fully here of course, like the Object Cleanup script, or Conveyors, but this isn't the end of the documentation. Within the unitypackage, there's 3 more text files with even more information.
Every variable also has tooltips, so you can hover over something if you need a quick explanation of what it's for.

There's a Credits file, which states where I found help, or certain assets included in the package.
an Instructions file, which explains what prefabs to drag into your scene first, and how to set them up.
and a Script Explanations file, which explains exactly what each script does, and what you could use it for.

I hope to see more fun tycoons on VRChat soon! And I hope it's super easy to use my prefab in your world. If you need help with something, you can message my discord at: <b>skull_z162</b>
<i>(Don't friend request and wait for me to accept it, my DMs are completely open, so message any time if you need help.)</i>
