<h1>Unity project for VR application</h1>
This repository contains unity project with all assets,  which will be used in VR application

<h2>Table of contents</h2>

- [Requirements](#requirements)
- [Setup](#setup)
  * [Fork  setup](#fork--setup)
  * [Cloning repository](#cloning-repository-fork)
  * [LFS Setup (Fork)](#lfs-setup-fork)
  * [LFS Setup (Console)](#lfs-setup-console)
  * [Adding project to unity](#adding-project-to-unity)
- [Workflow](#workflow)
  * [File locking](#file-locking)
    + [Check list of locked files](#check-list-of-locked-files)
    + [Lock/Unlock file](#lockunlock-file)
- [Troubleshooting](#troubleshooting)

## Requirements
* [Blender](https://www.blender.org/download/) is required, because without it unity won`t be able to open .blend files.
* [Unity 2020.1.15f1](https://unity3d.com/get-unity/download) - Unity version we lock to. If you have different version you can download this one through unity hub and delete the old one.
* Git-fork  [Windows](https://git-fork.com/update/win/ForkInstaller.exe) / [Mac](https://git-fork.com/update/files/Fork.dmg) - this is friendly for beginners git client.
* [Git-LFS](https://docs.github.com/en/free-pro-team@latest/github/managing-large-files/installing-git-large-file-storage) - for keeping track of large files, while keeping them out of repository.
* [Python 3.x](https://www.python.org/downloads/) needed to run pre commit hook. 

## Setup
### Fork  setup


First off we need to install and configure Fork. To do this follow images:

Here you enter your name and email, these don`t have to match github ones. This will be used for signing your commits.

![fork-setup](https://i.imgur.com/KCusEEX.png)

Then we need to set custom merger to UnityYAMLMerge which is shipped with unity.
We open preferences window by selecting File > Preferences.

![fork-unityyamlmerge-setup](https://i.imgur.com/y0PBw9C.png)

Here, we switch to the Integration tab and we set values of Merge Tool section.
* Merger Path: we set it to UnityYAMLMerge which is shipped  with unity and is probably located at: (can be somewhere else, depends on where you chose to install - You can't paste this directly, you have to click folder icon and then paste it in explorer path field)
 - **Windows:** C:\Program Files\Unity\Hub\Editor\2020.1.15f1\Editor\Data\Tools\UnityYAMLMerge.exe
 - **Mac:**  /Applications/Unity/Hub/Editor/2020.1.15f1/Unity.app/Contents/Tools/UnityYAMLMerge
* Arguments: merge -p $BASE $REMOTE $LOCAL $MERGED

![fork-unityyamlmerge-setup](https://i.imgur.com/RZhSGdZ.png)

### Cloning repository (Fork)

To clone repository go to File > Clone:

Here enter repository URL: https://github.com/JPsiInVR/UnityProject.git and select where you want it to be downloaded. After that press clone and wait for process to finish.

![fork-cloning-repository](https://i.imgur.com/S9mAI12.png)

### LFS Setup (Fork)
If you are using git from console you can skip this section.

To initialize lfs go to Repository > Git LFS > Initialize Git LFS

![fork-lfs-setup](https://i.imgur.com/l6LvKcJ.png)

Git LFS keeps old files in local storage,  so they don`t have to be redownloaded when switching commits. In case it gets too large use Repository > Git LFS > Prune

### LFS Setup (Console)
If you are using fork you can skip this section.

To initialize lfs go to console and use command:

```bash
git lfs install
```

### Adding pre-commit hook for file locking 
Now we will add pre commit hook which will prevent us from committing files which are locked and will save us the hassle of reverting it, because we won`t be able to push it anyway.
Detailed instruction on how to do that is here:

.git is hidden folder inside root of our project so to access it we may need to view hidden folders or access it directly through explorer path.

Download whole repository as ZIP, unpack it, copy precommit file to .git/hooks and then delete rest of the files.

https://github.com/wanadev/git-lfs-lock-pre-commit-hook
![add-precommit-hook](https://i.imgur.com/cMbMSvh.png)


### Adding project to unity 

Open unity hub, click Add button and locate cloned project. You are ready to go.

## Workflow

### File locking
You can lock a file to prevent others from editing it while you`re working on it. They wont be able to push their changes unitl you release the lock. File lockiing has a few rules:
File locking follows a few rules:
* Each file can only be locked by one person at a time.
* Locked files can only be unlocked by the person who locked them (see below for how to force unlock files).
* If your push contains locked files that you didn`t lock it will be rejected.
* If your merge contains locked files that you didn`t lock it will be blocked.

#### Check list of locked files
Before working on a project you should check the list of locked files so that you avoid wasting time working on them. To do this go into console.

![fork-opening-console](https://i.imgur.com/aZIeyvH.png)

And use command: 
```bash
git lfs locks
```

#### Lock/Unlock file
When you begin work on binary file (image, audio, video, 3D models) you should first lock this file and after you are done unlock it, because merging of binary files is impossible.

To lock/unlock a file go to File Tree tab, find the file you want, right click on it and select LFS > Lock / Unlock

![fork-locking-files](https://i.imgur.com/m9Q85Bf.png)

If you are using git from console you can use commands

```bash
git lfs lock ...
git lfs unlock ...
```

## Troubleshooting
If your unity project have some broken things like missing objects or broken prefabs you can try to reimport to force reload. First off I suggest reimporting models and prefabs folders. To do this right click on these folders and select reimport.

![unity-reimporting](https://i.imgur.com/b8uNWSi.png)

If this  doesn`t work you can try reimporting whole assets folder.
