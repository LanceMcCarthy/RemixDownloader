# RemixDownloader
An application that lets you quickly download 3D models in bulk from Remix 3D. This functionality is not available via the website.

### Browsing and Downloading

To browse models, you'll need a Remix3D.com user ID code. You can easily get that code from the Remix3D website in the web browser address bar. Here are the steps to load models.

1. Go to a user's page in [Remix3D.com](https://www.remix3d.com)
2. Look in the address bar and get the UserID code, it is between `user/` and the question mark (see screenshot below).
3. In the app, paste that code into the `User ID` box and click `Get User Models` button.
4. It will automatically load the models for that user as you scroll.

![image](https://user-images.githubusercontent.com/3520532/61412729-9f80a300-a8b7-11e9-912f-c899c6889b0e.png)

> There is a default User ID `46rbnCYv5fy` in the app to help you get started, it's the **Xbox** account. Another example is `46reU3-wFPz`, this is the **Microsoft** account seen in the screenshot above (2,139 models).

#### Selection and Download

Here are the steps to select and download model files.

1. Select models in the list, multiple selection is enabled (CTRL + Click, Shift + Click, etc). You will see your selections populate the list on the right.
2. When you're ready to download, choose a `Level of Detail` and a `Download folder` location
3. Click the `Start Download` button, the app will download all of the selected models to the selected folder.

![image](https://user-images.githubusercontent.com/3520532/61333383-bdd19a80-a7f4-11e9-89e9-5b0b7faf01c1.png)


> This is an early alpha. Only `User ID` is working. Support for Board collections support will be added soon.

### Printing Model Files

Keep in mind that these models were designed for use in `Mixed Reality`, `HoloLens` and `Paint3D`. If you open a model file for 3D printing, you will commonly see that the model is not valid. 

This can be easily fixed by opening the model in the Windows 10 **Print 3D** app. 

1. Open the file in the Print 3D app
2. Once it loads, you will see a red line around the model if it is not valid for printing. 
3. Select the Repair model option and let it run until it's done (it can take a while)

![image](https://user-images.githubusercontent.com/3520532/61412378-b7a3f280-a8b6-11e9-9018-477d593d993a.png)