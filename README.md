
<div style="border: 2px solid grey; padding: 10px;">


# **OpenXbl Discord Bot**

A Discord bot that retrieves Xbox Live data using the [**OpenXbl API**](https://xbl.io/).

---

## 🚀 **Overview**
This bot connects to Xbox Live through the [**OpenXbl API**](https://xbl.io/) and provides Xbox player and game information directly in Discord.

---

## 🧰 **Requirements**

- [**Visual Studio 2017**](https://visualstudio.microsoft.com/vs/older-downloads/)  
- [**.NET Core 2.1**](https://dotnet.microsoft.com/en-us/download/dotnet/2.1)  
- [**PHP**](https://www.php.net/downloads) (for the web integration)
- [**OpenXbl PHPWrapper**](https://github.com/OpenXBL/OpenXBL-PHP) (for the web integration)

---

## ⚙️ **Setup Instructions**

### **1. Create an OpenXbl Account**
- Sign up at [**xbl.io**](https://xbl.io/) and generate your **API key**.
- Setup WebServer on your VPS or PC. A popular one to use is [**XAMPP**](https://www.apachefriends.org/)
- Copy the entire XBLIO folder into your html WebServer Root folder(eg. /var/www/html for linux or C:\xampp\htdocs for Windows)
- Follow OpenXbl Instructions to [**install**](https://github.com/OpenXBL/OpenXBL-PHP)(Use their PHPWrapper, install in the XBLIO folder)

  
  

### **2. Configure the Project**
Update the following files with your settings:

| **File** | **Description** |
|-----------|----------------|
| `config.php` | Set your web server root folder(Where all your html files go) |
| `stealth/sql/Conn.php` | Add your SQL database connection details |
| `xbox.php` | Insert your [**OpenXbl API**](https://xbl.io/) key |
| `discord_bot/config.ini` | Complete your bot configuration |

### **3. Fixing Composer PHP Version Errors**
If you encounter the error:

Could not find package matching minimum PHP version:

Follow this guide to resolve it:  
👉 [**Composer: Ignore Platform Requirements**](https://php.watch/articles/composer-ignore-platform-req)

---

## 🧩 **Notes**
- A **proxy option** is available but currently has limited functionality.  
- Make sure all configuration files are properly filled before running the bot.
- If you don't want to setup a webserver to host the php files you dont need to. You can still use the bot without them but some functions may not work. 

---

## 📜 **License**
This project is provided as-is for educational and community use.  
Please review and comply with the [**OpenXbl API Terms of Use**](https://xbl.io/).

---

## 💬 **Support**
If you run into issues or want to contribute, feel free to open an **issue** or **pull request** on GitHub.



</div>

![Friendlist](https://user-images.githubusercontent.com/44829491/192215488-402ef189-315e-4dc3-9c05-c55ba90c2c1f.PNG)
![ProfileScreenshot](https://user-images.githubusercontent.com/44829491/192215490-2cbb1dee-a050-460c-a82f-edeb27d35102.PNG)
