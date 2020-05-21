# Huawei Xamarin Site Demo

## Table of Contents
* [Introduction](#introduction)
* [Installation](#installation)
* [Configuration](#configuration)
* [Licensing and Terms](#licensing-and-terms)

## Introduction
This is a demo application that prepared to use HUAWEI Site Kit Xamarin SDK.

## Installation
Before using HUAWEI Site Kit Xamarin Demo, ensure that the **Visual Studio** has been installed with **Mobile development with .NET** option.

To use HUAWEI Site Kit Xamarin Demo, you need to generate and import library files from HUAWEI Site Kit Xamarin SDK. To download the SDK and see the instructions, please refer to [developer.huawei.com](https://developer.huawei.com/consumer/en/).

If you already have library files please follow the instructions below:

1. Clone this repository.

2. Copy HUAWEI Site Kit Xamarin SDK library files(.dll files) to **_LibDlls** folder of the project.

3. Open **Xamarin Hms Site Demo** project by double clicking **Xamarin Hms Site Demo.sln** file.

4. Wait for 1-2 mins to restore project operation to be completed.

5. Create an App in AppGallery Connect by following instructions in [Creating an App in AppGallery Connect](https://developer.huawei.com/consumer/en/codelab/HMSPreparation/index.html#0) page.

6. Add package name to your app by following instructions in [Add Package Name](https://developer.huawei.com/consumer/en/codelab/HMSPreparation/index.html#1) page.

7. Generate a signing certificate file(aka keystore file)
    * Open the command line tool (using the **cmd** command) and run the **cd** command to go to the directory where **keytool.exe** is located. The **bin** directory of the **JDK**. (For example; **C:\Program Files\Android\jdk\microsoft_dist_openjdk_1.8.0.25\bin**).

        **Note:** Visual Studio comes with OpenJDK installed.

    * Run **keytool -genkey -keystore** *\<keystore-file\>* -storepass *\<keystore-pass\>* **-alias** *\<key-alias\>* **-keypass** *\<key-pass\>* **-dname** *\<dname\>* **-keysize** 2048 **-keyalg** RSA **-validity** *\<validity-period\>*
    * In the preceding command:
        * *\<keystore-file\>* is the complete path to the app's signature file. File extension must be .jks or .keystore. For example; **D:\Android\mykeystore.jks**
        * *\<keystore-pass\>* is the password of your keystore. Requires minimum 6 characters. For example; **123456**
        * *\<key-alias\>* is the alias name of key that will be stored in your keystore. For example; **sitekitdemo**
        * *\<key-pass\>* is the password of your key. Requires minimum 6 characters. For example; **123456**
        * *\<dname\>* is a unique identifier for the application in the keystore. For example; **"o=Huawei"**
        * *\<validity-period\>* Amout of days the key will be valid with this keystore. For example; **36500**
    * Example command:
        ```cmd
        keytool -genkey -keystore D:\Android\keystore.jks -storepass 123456 -alias sitekitdemo -keypass 123456 -dname "o=Huawei" -keysize 2048 -keyalg RSA -validity 36500
        ```

8. Generate a signing certificate fingerprint
    * Open the command line tool (using the **cmd** command) and run the **cd** command to go to the directory where **keytool.exe** is located. The **bin** directory of the **JDK**. (For example; **C:\Program Files\Android\jdk\microsoft_dist_openjdk_1.8.0.25\bin**).

        **Note:** Visual Studio comes with OpenJDK installed.

    * Run **keytool -list -v -keystore** *\<keystore-file\>*
    * In the preceding command; *\<keystore-file\>* is the complete path to the app's signature file. For example; **D:\Android\mykeystore.jks**
    * Example command:
        ```cmd
        keytool -list -v -keystore D:\Android\keystore.jks
        ```
    * You will be asked to enter your keystore password. Please enter your keystore password that created in **step 7**.
    * Obtain the **SHA256** fingerprint from the result.
    * For more details please refer to [Generating a Signing Certificate Fingerprint](https://developer.huawei.com/consumer/en/codelab/HMSPreparation/index.html#4) page.

9. Add fingerprint certificate to App in AppGallery Connect by following instructions in [Add fingerprint certificate to AppGallery Connect](https://developer.huawei.com/consumer/en/codelab/HMSPreparation/index.html#5) page.
    * After completing the last step on that page, download **agconnect-services.json** file from **App information** section that shown at the end of the page.

10. After completing **step 9** copy **agconnect-services.json** file to Assets directory of the project.

11. Right click to project and click to **Properties** button in the menu.

12. In the window; click to **Android Manifest** menu item and change **Package name** according to your app's package name that defined in the **agconnect-services.json** file(It is the same package name that is mentioned in the **step 6**).

13. In the same window; click to **Android Package Signing** menu item and set your keystore file information mentioned in **step 7**.
    * Check the option "**Sign the .APK file using the following keystore details.**"
    * Give correct file path of your keystore in Keystore section. For example; D:\Android\keystore.jks
    * Use the same keystore information when creating keystore file in **step 7**.
    * **Note:** You should perform these steps for both **Debug** and **Release** build configurations.

14. Right click to project and click to **Build** button in the opened menu.

15. Check the logs in the **Output** tab to check that solution built successfully.

16. Run the application.

## Configuration
No.

##  Licensing and Terms
Huawei Xamarin SDK uses the Apache 2.0 license.
