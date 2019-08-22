## UWP App

You only need to double click the appxbundle file to install the UWP app to your Windows 10 PC.

1. Download the **AppxPackages** folder.
2. Open the **Test** folder and notice the only `Remix.Uwp_xxxx.appxbundle` file (it should have a little 'open box' icon).
3. Double click the appxbundle to install on Windows 10.

## Console App
Self-contained .NETCore 2.2 Console application. See runtime result here: https://imgur.com/gallery/FouC8Pr

### Download
Download and extract the appropriate build for your platform.

* Windows
  * x64
  * ARM
* Mac OS
  * x64
* Linux
  * x64
  * ARM

### Operation

**Windows**

  1. Run (or double click) the `RemixDownloader.Console.exe`.

**MacOS or Linux**

  1. Open Terminal or Bash
  2. Navigate to the folder you extracted the download.
  3. Enter the following command `./RemixDownloader.Console`.

### Important
Depending on your platform, you *may* need to first "unblock" the downloaded ZIP before extracting it. This is a security measure by your operating system to prevent internet-based assemblies from running without your permission.

Here's what the looks like on Windows:

![image](https://user-images.githubusercontent.com/3520532/62377346-92e38800-b510-11e9-88fa-6c05912b9e6a.png)
