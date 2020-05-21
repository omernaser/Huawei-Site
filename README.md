![enter image description here](https://raw.githubusercontent.com/omernaser/Huawei-MAP/master/huaweiicon.png)
## Xamarin.Android.HMSSite

**Introduction**
With HUAWEI Site Kit, your app can provide users with convenient and secure access to diverse, place-related services.

#  Use Cases

HUAWEI Site Kit provides the following core capabilities you need to quickly build apps with which your users can explore the world around them:

-   Place search: Returns a place list based on keywords entered by the user.
    
-   Nearby place search: Searches for nearby places based on the current location of the user's device.
    
-   Place details: Searches for details about a place.
    
-   Search suggestion: Returns a list of place suggestions.



### Setup

-   Available on NuGet:  [https://www.nuget.org/packages/Xamarin.Android.HMSSite](https://www.nuget.org/packages/Xamarin.Android.HMSSite/4.0.2.301)
-   Install into your .NETStandard project and Client projects.
**Platform Support**
Xamarin.Android


**How To Use**
1. You should add these lines to your MainActivity.cs

>          var config = AGConnectServicesConfig.FromContext(this);
            config.OverlayWith(new HmsLazyInputStream(this));
            Com.Huawei.Agconnect.AGConnectInstance.Initialize(this);

2. You should add agconnect-services.json to the assets Folder 
refer to the following link to get it [https://developer.huawei.com/consumer/en/codelab/HMSPreparation/index.html#0](https://developer.huawei.com/consumer/en/codelab/HMSPreparation/index.html#0)

## ***Setup SHA256 Key***

. Create an App in AppGallery Connect by following instructions in [Creating an App in AppGallery Connect](https://developer.huawei.com/consumer/en/codelab/HMSPreparation/index.html#0) page.

. Add package name to your app by following instructions in [Add Package Name](https://developer.huawei.com/consumer/en/codelab/HMSPreparation/index.html#1) page.

. Generate a signing certificate file(aka keystore file)
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

. Generate a signing certificate fingerprint
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

. Add fingerprint certificate to App in AppGallery Connect by following instructions in [Add fingerprint certificate to AppGallery Connect](https://developer.huawei.com/consumer/en/codelab/HMSPreparation/index.html#5) page.
    * After completing the last step on that page, download **agconnect-services.json** file from **App information** section that shown at the end of the page.

. After completing **step 9** copy **agconnect-services.json** file to Assets directory of the project.

. Right click to project and click to **Properties** button in the menu.

. In the window; click to **Android Manifest** menu item and change **Package name** according to your app's package name that defined in the **agconnect-services.json** file(It is the same package name that is mentioned in the **step 6**).

. In the same window; click to **Android Package Signing** menu item and set your keystore file information mentioned in **step 7**.
    * Check the option "**Sign the .APK file using the following keystore details.**"
    * Give correct file path of your keystore in Keystore section. For example; D:\Android\keystore.jks
    * Use the same keystore information when creating keystore file in **step 7**.
    * **Note:** You should perform these steps for both **Debug** and **Release** build configurations.

. Right click to project and click to **Build** button in the opened menu.

. Check the logs in the **Output** tab to check that solution built successfully.

. Run the application.

note you should add SHA256 as following

![enter image description here](https://raw.githubusercontent.com/omernaser/Huawei-Site/master/Capture.PNG)
## Features

# # Keyword Search
With this function, users can specify keywords, coordinate bounds, and other information to search for places such as tourist attractions, enterprises, and schools.

# # Nearby Place Search
With this function, your app can return a list of nearby places based on the current location of a user. When the user selects a place, the app obtains the place ID and searches for details about the place.

# # Place Details
This function can be used to search for details about a place based on the unique ID of the place.

# # Place Search Suggestion
This function can be used to return search suggestions during the user input.

**if you have the  error related to BridgeActivity you need to add the following code at manifest file inside the application tag**
BridgeActivity is responsible about the update 

   

    <activity
               android:name="com.huawei.hms.activity.BridgeActivity"
               />



## Sample :

![enter image description here](https://raw.githubusercontent.com/omernaser/Huawei-Site/master/Screenshot_20200521_131634_com.companyname.hms_map_demo.jpg)


![enter image description here](https://raw.githubusercontent.com/omernaser/Huawei-Site/master/Screenshot_20200521_131656_com.companyname.hms_map_demo.jpg)


![enter image description here](https://raw.githubusercontent.com/omernaser/Huawei-Site/master/Screenshot_20200521_131719_com.companyname.hms_map_demo.jpg)

## Reference
[https://developer.huawei.com/consumer/en/doc/development/HMS-Guides/hms-site-business-introduction](https://developer.huawei.com/consumer/en/doc/development/HMS-Guides/hms-site-business-introduction)

[https://developer.huawei.com/consumer/en/doc/development/HMS-Guides/hms-site-configuringagc](https://developer.huawei.com/consumer/en/doc/development/HMS-Guides/hms-site-configuringagc)
