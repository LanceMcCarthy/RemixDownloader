# RemixDownloader
An application that lets you quickly download 3D models in bulk from Remix 3D. This functionality is not available via the website.


| Project  | Build Status | Release Status |
|----------|--------------| -------------- |
| .NET Core Console App | [![Console](https://dev.azure.com/lance/RemixDownloader/_apis/build/status/RemixDownloader%20Console)](https://dev.azure.com/lance/RemixDownloader/_build/latest?definitionId=22) | ![](https://vsrm.dev.azure.com/lance/_apis/public/Release/badge/1fb26b14-bf7c-446d-9ce2-f817fdc1756b/2/2) |
| UWP App | [![Build status](https://dev.azure.com/lance/RemixDownloader/_apis/build/status/RemixDownloader%20UWP)](https://dev.azure.com/lance/RemixDownloader/_build/latest?definitionId=23) | ![](https://vsrm.dev.azure.com/lance/_apis/public/Release/badge/1fb26b14-bf7c-446d-9ce2-f817fdc1756b/1/1) |



### First Step - Getting the User ID

To browse models, you'll need a Remix3D.com user ID code. You can easily get that code from the Remix3D website in the web browser address bar. Here are the steps to load models.

1. Go to a user's page in [Remix3D.com](https://www.remix3d.com)
2. Look in the address bar and get the UserID code, it is between `user/` and the question mark (see screenshot below).
3. Use that code in the app when you're asked for a User ID.

![image](https://user-images.githubusercontent.com/3520532/61412729-9f80a300-a8b7-11e9-912f-c899c6889b0e.png)

> There is a default User ID `46rbnCYv5fy` in the app to help you get started, it's the **Xbox** account. Another example is `46reU3-wFPz`, this is the **Microsoft** account seen in the screenshot above (2,139 models).

### Console App - Automatic bulk downloader

Run the console app, and take the following steps. 

1. Enter the **User ID** code.
2. Enter the **folder path** you want to save the files in (e.g. `C:\Users\Lance\Downloads\`). *The app will automatically organize subfolders for you*.
3. Enter **Y** or **N** if you want the HoloLens and Mixed Reality files, too.


![image](https://dvlup.blob.core.windows.net/general-app-files/GIFs/Remix3DDownloaderConsoleBetter.gif)

> The app will download the entire model library for that user. In my tests, it took ~3 hours to backup all **2664 models** from the XBox account which resulted in **7992** files (I enabled the HoloLens and WinMR option).

### UWP App - Selection and Download


If you would prefer to see the models first, the UWP app is the better option for you. You can either select a few to save, or bulk download everything, here are the steps to follow:

Step 1 - Enter the User ID in the UserID box, then click either **Load User Models** or **Download All Models** button.

*If you chose List User Models:*

1. Select models in the GridView, multiple selection is enabled (CTRL + Click). You will see your selections populate the ListView on the right.
2. When you're ready to download, choose a `Level of Detail` and a `Download folder` location
3. Click the `Start Download` button, the app will download all of the selected models to the selected folder.

*If you choose Download All Models:*

This is a long running process and <u>could take hours</u> depending on how many models that user has. For example, the Microsoft and XBox users have more than 2,000 models! Please be patient, the process will stop once there are no more items for that user.

![image](https://user-images.githubusercontent.com/3520532/62094015-92fe3200-b249-11e9-8cc8-e5982308906b.png)


> This is an early beta and I'm the only developer. Please report any issues in <a href="https://github.com/LanceMcCarthy/RemixDownloader/issues" target="_blank">the Issues tab</a> and I will get to them as soon as possible.

### Printing Model Files

Keep in mind that these models were designed for use in `Mixed Reality`, `HoloLens` and `Paint3D`. If you open a model file for 3D printing, you will commonly see that the model is not valid. 

This can be easily fixed by opening the model in the Windows 10 **Print 3D** app. 

1. Open the file in the Print 3D app
2. Once it loads, you will see a red line around the model if it is not valid for printing. 
3. Select the Repair model option and let it run until it's done (it can take a while)

![image](https://user-images.githubusercontent.com/3520532/61412378-b7a3f280-a8b6-11e9-9018-477d593d993a.png)