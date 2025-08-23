# ChatApp: SignalR + ASP.NET Core MVC

## Introduction
This is a small application that enables live chat between users using SignalR and ASP.NET Core MVC.  
Users can send public messages, private messages, and even create or join groups for group chat.

---

## 🛠️ Features
- ✅ Real-time chat using SignalR
- ✅ Public messages (broadcast to all users)
- ✅ Private messaging between two users
- ✅ Group chat support (join/leave groups and send messages)
- ✅ List of online users (updated dynamically)

---

## 📂 Project Structure
ChatApp/  
│── Controllers/  
│── Hubs/  
│   └── ChatHub.cs  
│── Models/  
│── Views/  
│── wwwroot/  
│── Program.cs  
│── Startup.cs  
│── ChatApp.csproj  

---

## ⚡ How It Works?
- Users connect to the chat by entering their username.  
- SignalR keeps track of connected users in memory.  
- Messages can be sent:  
  - To everyone (public chat)  
  - To a specific user (private chat)  
  - To a group (group chat)  
- The list of online users is updated in real-time.  

---

## ▶️ Getting Started

### Prerequisites
- .NET 7 (or newer)  
- Visual Studio 2022 (or JetBrains Rider / VS Code with C# extension)  

### Steps
1. Clone this repository:  
   `git clone https://github.com/Alimobinifar/RealTimeChatApp`  

2. Navigate into the project folder:  
   `cd RealTimeChatApp`  

3. Run the application:  
   - Using Visual Studio → Press **F5** (IIS Express)  
   - Or using dotnet CLI → `dotnet run`  

4. Open your browser and go to:  
   `https://localhost:5001`  

5. Open multiple browser tabs (or devices) and start chatting 🎉  

---

## 💡 Future Improvements
- Add user authentication (register & login system)  
- Save chat history into a database  
- Improve UI with Bootstrap / Tailwind  
- Add support for file sharing / emojis  

---

## 🤝 Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.  

---

## 📜 License
This project is licensed under the MIT License.  
