# SignalR.API

### POSTMAN REQUEST
- connect: wss://localhost:7017/chat-hub
- param connect: {"protocol":"json","version":1}
- param join room: {"arguments":[{"UserName":"Jane", "ChatRoom":"Room 1"}],"invocationId":"0","target":"JoinSpecificChatRoom","type":1}
- param send message: {"arguments":["Hello!"],"invocationId":"0","target":"SendMessage","type":1}
