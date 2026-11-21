# TheRandomChat

It is a RandomChat , with **ASP.NET Core** and **SignalR**. 
It allows users to connect randomly and chat in real-time. 

---

## Features
- Anonymous connection: no account required
- Random matching: users are connected randomly for 1-to-1 chats 
- Real-time text chat via SignalR  
- Multi-port support: default ports are `5041` and `5042`

---

## Installation

Follow these steps to run the project locally:

### 1. Clone the repository
```bash
gh repo clone MOHAMMADREZAMI/RandomChat
cd TheRandomChat
```
### 2. Install prerequisites 

Make sure you have [.NET SDK](https://dotnet.microsoft.com/en-us/download) installed.

### 3. Install SignalR (if not already installed)
```bash
dotnet add package Microsoft.AspNetCore.SignalR
dotnet add package Microsoft.AspNetCore.SignalR.Client
```
### 4. Run the project

```bash
dotnet run
```

## License
This project is open-source and licensed under the [MIT License](LICENSE).
